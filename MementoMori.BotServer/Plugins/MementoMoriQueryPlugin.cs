using System.Text;
using System.Text.RegularExpressions;
using AutoCtor;
using EleCho.GoCqHttpSdk;
using EleCho.GoCqHttpSdk.Message;
using EleCho.GoCqHttpSdk.MessageMatching;
using EleCho.GoCqHttpSdk.Post;
using HtmlConverter.Configurations;
using HtmlConverter.Options;
using LiteDB;
using MementoMori.BotServer.Api;
using MementoMori.BotServer.Options;
using MementoMori.BotServer.Utils;
using MementoMori.Extensions;
using MementoMori.Option;
using MementoMori.Ortega;
using MementoMori.Ortega.Share;
using MementoMori.Ortega.Share.Data.ApiInterface.Notice;
using MementoMori.Ortega.Share.Data.Notice;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Data;
using Newtonsoft.Json.Linq;
using ReactiveUI;
using Refit;
using static MementoMori.Ortega.Share.Masters;

namespace MementoMori.BotServer.Plugins;

[InjectSingleton]
[AutoConstruct]
public partial class MementoMoriQueryPlugin : CqMessageMatchPostPlugin
{
    private readonly IWritableOptions<BotOptions> _botOptions;
    private readonly SessionAccessor _sessionAccessor;
    private IMentemoriIcu _mentemoriIcu;
    private readonly MementoNetworkManager _networkManager;
    private readonly ILogger<MementoMoriQueryPlugin> _logger;
    private readonly LiteDbAccessor _dbAccessor;
    private readonly IHttpClientFactory _httpClientFactory;

    [AutoPostConstruct]
    public void MementoMoriQueryPlugin1()
    {
        _mentemoriIcu = RestService.For<IMentemoriIcu>(_botOptions.Value.MentemoriIcuUri);
        _ = AutoNotice();
        _ = AutoDmmVersionCheck();
        // _networkManager.MoriHttpClientHandler.WhenAnyValue(d => d.OrtegaMasterVersion).Subscribe(NotifyNewMasterVersion);
        // _networkManager.MoriHttpClientHandler.WhenAnyValue(d => d.OrtegaAssetVersion).Subscribe(NotifyNewAssetVersion);
    }

    private void NotifyNewMasterVersion(string masterVersion)
    {
        while (!_sessionAccessor.Session.IsConnected)
        {
            Thread.Sleep(TimeSpan.FromSeconds(1));
        }

        var msg = $"发现新的 Ortega Master 版本 {masterVersion}";
        foreach (var group in _botOptions.Value.OpenedGroups)
        {
            _sessionAccessor.Session.SendGroupMessage(group, new CqMessage(msg));
        }
    }

    private void NotifyNewAssetVersion(string assetVersion)
    {
        while (!_sessionAccessor.Session.IsConnected)
        {
            Thread.Sleep(TimeSpan.FromSeconds(1));
        }

        var msg = $"发现新的 Ortega Asset 版本 {assetVersion}";
        foreach (var group in _botOptions.Value.OpenedGroups)
        {
            _sessionAccessor.Session.SendGroupMessage(group, new CqMessage(msg));
        }
    }

    private async Task AutoNotice()
    {
        await Task.Delay(TimeSpan.FromSeconds(10));
        while (true)
        {
            try
            {
                await GetNotice(NoticeAccessType.Title, NoticeCategoryType.NoticeTab, option => option.LastNotices);
                await GetNotice(NoticeAccessType.MyPage, NoticeCategoryType.EventTab, option => option.LastEvents);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "get notice failed");
            }
            finally
            {
                await Task.Delay(TimeSpan.FromMinutes(10));
            }
        }

        async Task GetNotice(NoticeAccessType noticeAccessType, NoticeCategoryType noticeCategoryType, Func<BotOptions, List<long>> getList)
        {
            var apiAuth = _botOptions.Value.NoticeApiAuth;
            _logger.LogInformation("start retrieve notices");
            var listResponse = await _networkManager.GetResponse<GetNoticeInfoListRequest, GetNoticeInfoListResponse>(new GetNoticeInfoListRequest()
            {
                AccessType = noticeAccessType,
                CategoryType = noticeCategoryType,
                CountryCode = OrtegaConst.Addressable.LanguageNameDictionary[_networkManager.LanguageType],
                LanguageType = _networkManager.LanguageType,
                UserId = 0
            }, apiAuth: new Uri(apiAuth));

            var noticeInfos = listResponse.NoticeInfoList.OrderByDescending(d => d.Id).ToList();
            var noticeToPush = new List<NoticeInfo>();
            using (var db = _dbAccessor.GetDb())
            {
                foreach (var noticeInfo in noticeInfos)
                {
                    if (db.GetCollection<NoticeInfo>().Exists(d => d.Id == noticeInfo.Id))
                    {
                        continue;
                    }

                    noticeToPush.Add(noticeInfo);
                    db.GetCollection<NoticeInfo>().Insert(noticeInfo);
                }
            }

            // only send latest 5
            foreach (var noticeInfo in noticeToPush.Where(d => d.Id % 10 != 6).Take(5))
            {
                var msg = new StringBuilder();
                msg.AppendLine($"<h1>{noticeInfo.Title}({noticeInfo.Id})</h1>");
                msg.AppendLine();
                var mainText = $"<div>{noticeInfo.MainText}</div>"; //.Replace("<br>", "\r\n");
                msg.AppendLine(mainText);
                var bytes = ImageUtil.HtmlToImage(msg.ToString());
                foreach (var group in _botOptions.Value.OpenedGroups)
                {
                    // await Task.Delay(TimeSpan.FromSeconds(1));
                    _logger.LogInformation($"send notice{noticeInfo.Title} to group {group}");
                    var cqImageMsg = CqImageMsg.FromBytes(bytes);
                    await _sessionAccessor.Session.SendGroupMessageAsync(group, new CqMessage(cqImageMsg, new CqTextMsg(noticeInfo.Title)));
                }
            }
        }
    }

    private async Task AutoDmmVersionCheck()
    {
        await Task.Delay(TimeSpan.FromSeconds(10));
        while (true)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                while (true)
                {
                    await Task.Delay(1000);
                    var dmmGameId = _botOptions.Value.LastDmmGameId + 1;
                    var dmmUrl = $"{_botOptions.Value.DmmApiUrl}/gameplayer/filelist/{dmmGameId}";
                    var json = await httpClient.GetStringAsync(dmmUrl);
                    var jObject = JObject.Parse(json);
                    if (jObject["result_code"]?.Value<long>() != 100) break;
                    if (jObject["data"]?["file_list"] is not JArray jArray || jArray.Count == 0) break;

                    _botOptions.Update(d => d.LastDmmGameId = dmmGameId);
                    // product/mementomori/MementoMori/content/win/2.3.0/data/GameAssembly.dll
                    var match = Regex.Match(jArray[0]["path"]?.ToString() ?? "", @"product/mementomori/MementoMori/content/win/(?<version>.+?)/data/");
                    if (!match.Success) continue;

                    var version = match.Groups["version"].Value;
                    foreach (var group in _botOptions.Value.OpenedGroups)
                    {
                        await _sessionAccessor.Session.SendGroupMessageAsync(group, new CqMessage($"DMM 版本监测：发现新版 {version} ({dmmGameId})"));
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "get dmm version failed");
            }
            finally
            {
                await Task.Delay(TimeSpan.FromMinutes(10));
            }
        }
    }

    private bool IsGroupAllowed(CqGroupMessagePostContext context)
    {
        return _botOptions.Value.OpenedGroups.Contains(context.GroupId);
    }

    [CqMessageMatch("^/命令列表$")]
    public async Task FunctionList(CqGroupMessagePostContext context)
    {
        if (!IsGroupAllowed(context)) return;
        _logger.LogInformation(nameof(FunctionList));
        var msg = new StringBuilder();
        msg.AppendLine("命令列表");
        msg.AppendLine("/命令列表");
        msg.AppendLine("/角色ID列表");
        msg.AppendLine("/速度列表");
        msg.AppendLine("/技能 角色ID (示例 /技能 1)");
        msg.AppendLine("/角色 角色ID (示例 /角色 1)");
        msg.AppendLine("/主线 关卡 (示例 /主线 12-28)");
        msg.AppendLine("/(无穷|红|黄|绿|蓝)塔 关卡 (示例 /绿塔 499)");
        msg.AppendLine("/(战力|等级|主线|塔|竞技场)排名 (日|韩|亚|美|欧|国际)1 (示例 /战力排名 日10)");
        msg.AppendLine("/公告 [ID] (示例 /公告 123 ，/公告)");
        await _sessionAccessor.Session.SendGroupMessageAsync(context.GroupId, new CqMessage(msg.ToString()));
    }

    // 通过 CqMessageMatch 来指定匹配规则 (例如这里非贪婪匹配两个中括号之间的任意内容, 并命名为 content 组)
    [CqMessageMatch(@"^/角色ID列表$")]
    public async Task QueryCharacterIds(CqGroupMessagePostContext context)
    {
        if (!IsGroupAllowed(context)) return;
        _logger.LogInformation(nameof(QueryCharacterIds));
        var msg = new StringBuilder();
        var i = 1;
        foreach (var characterMb in CharacterTable.GetArray())
        {
            var name = CharacterTable.GetCharacterName(characterMb.Id);
            msg.AppendLine($"{characterMb.Id:000}: {name}");
        }

        await _sessionAccessor.Session.SendGroupMessageAsync(context.GroupId, new CqMessage(msg.ToString()));
    }

    [CqMessageMatch(@"^/技能\s*(?<idStr>\d+)$")]
    public async Task QueryCharacterSkills(CqGroupMessagePostContext context, string idStr)
    {
        if (!IsGroupAllowed(context)) return;
        _logger.LogInformation($"{nameof(QueryCharacterSkills)} {idStr}");
        var id = long.Parse(idStr);
        var characterMb = CharacterTable.GetById(id);
        if (characterMb == null)
        {
            await _sessionAccessor.Session.SendGroupMessageAsync(context.GroupId, new CqMessage("未查询到此角色"));
            return;
        }

        var msg = new StringBuilder(tableStyle);
        var chaName = CharacterTable.GetCharacterName(characterMb.Id);
        msg.AppendLine($"<h1>{chaName}的技能</h1><table>");
        foreach (var skillId in characterMb.ActiveSkillIds)
        {
            var skillMb = ActiveSkillTable.GetById(skillId);
            var name = TextResourceTable.Get(skillMb.NameKey);
            msg.AppendFormat($"<tr><th width=\"150px\">{name}</th><th>冷却 {skillMb.SkillMaxCoolTime}</th></tr>");
            foreach (var skillInfo in skillMb.ActiveSkillInfos)
            {
                var description = TextResourceTable.Get(skillInfo.DescriptionKey);
                if (skillInfo.EquipmentRarityFlags != 0 || string.IsNullOrEmpty(description))
                {
                    continue;
                }

                msg.AppendFormat($"<tr><td>{GetSkillDesc(skillInfo.OrderNumber, skillInfo.CharacterLevel, skillInfo.EquipmentRarityFlags)}</td><td>{description}</td></tr>");
            }
        }

        foreach (var skillId in characterMb.PassiveSkillIds)
        {
            var skillMb = PassiveSkillTable.GetById(skillId);
            var name = TextResourceTable.Get(skillMb.NameKey);
            msg.AppendFormat($"<tr><th>{name}</th><th></th></tr>");
            foreach (var skillInfo in skillMb.PassiveSkillInfos)
            {
                var description = TextResourceTable.Get(skillInfo.DescriptionKey);
                if (skillInfo.EquipmentRarityFlags != 0 || string.IsNullOrEmpty(description))
                {
                    continue;
                }

                msg.AppendFormat($"<tr><td>{GetSkillDesc(skillInfo.OrderNumber, skillInfo.CharacterLevel, skillInfo.EquipmentRarityFlags)}</td><td>{description}</td></tr>");
            }
        }

        var equipmentMbs = EquipmentTable.GetArray().Where(d => d.Category == EquipmentCategory.Exclusive && (d.RarityFlags & EquipmentRarityFlags.SSR) != 0 && d.EquipmentLv == 180).ToList();
        foreach (var equipmentMb in equipmentMbs)
        {
            if (EquipmentExclusiveEffectTable.GetById(equipmentMb.ExclusiveEffectId).CharacterId == id)
            {
                msg.AppendFormat($"<tr><th>专属装备</th><th></th></tr>");
                var descriptionMb = EquipmentExclusiveSkillDescriptionTable.GetById(equipmentMb.EquipmentExclusiveSkillDescriptionId);
                msg.AppendFormat(@$"<tr><td>Ex.1</td><td>{TextResourceTable.Get(descriptionMb.Description1Key)}</td></tr>");
                msg.AppendFormat(@$"<tr><td>Ex.2</td><td>{TextResourceTable.Get(descriptionMb.Description2Key)}</td></tr>");
                msg.AppendFormat(@$"<tr><td>Ex.3</td><td>{TextResourceTable.Get(descriptionMb.Description3Key)}</td></tr>");
                break;
            }
        }

        msg.AppendLine("</table>");

        var bytes = ImageUtil.HtmlToImage(msg.ToString(), 1200);

        var cqImageMsg = CqImageMsg.FromBytes(bytes);
        await _sessionAccessor.Session.SendGroupMessageAsync(context.GroupId, new CqMessage(cqImageMsg));

        string GetSkillDesc(int orderNumber, long characterLevel, EquipmentRarityFlags equipmentRarityFlags)
        {
            if (equipmentRarityFlags == 0)
            {
                return $"Lv.{orderNumber}";
            }

            var lvl = equipmentRarityFlags switch
            {
                EquipmentRarityFlags.SSR => "Ex.1",
                EquipmentRarityFlags.UR => "Ex.2",
                EquipmentRarityFlags.LR => "Ex.3",
                _ => "未知"
            };
            return $"{lvl}";
        }
    }

    [CqMessageMatch(@"^/速度列表$")]
    public async Task QuerySpeedList(CqGroupMessagePostContext context)
    {
        if (!IsGroupAllowed(context)) return;
        _logger.LogInformation($"{nameof(QuerySpeedList)}");

        var msg = new StringBuilder();
        foreach (var d in CharacterTable.GetArray().Select(characterMb =>
                 {
                     var chaName = CharacterTable.GetCharacterName(characterMb.Id);
                     var speed = characterMb.InitialBattleParameter.Speed;
                     return new {chaName, speed};
                 }).OrderByDescending(d => d.speed))
        {
            msg.AppendLine($"{d.speed}: {d.chaName}");
        }

        await _sessionAccessor.Session.SendGroupMessageAsync(context.GroupId, new CqMessage(msg.ToString()));
    }

    [CqMessageMatch(@"^/角色\s*(?<idStr>\d+)$")]
    public async Task QueryCharacter(CqGroupMessagePostContext context, string idStr)
    {
        if (!IsGroupAllowed(context)) return;
        _logger.LogInformation($"{nameof(QueryCharacter)} {idStr}");
        var id = long.Parse(idStr);
        var msg = new StringBuilder();
        var profile = CharacterProfileTable.GetById(id);
        if (profile == null)
        {
            await _sessionAccessor.Session.SendGroupMessageAsync(context.GroupId, new CqMessage("未找到角色"));
            return;
        }

        var characterMb = CharacterTable.GetById(id);

        var characterName = CharacterTable.GetCharacterName(id);
        msg.AppendLine($"{characterName} (ID: {id})");
        msg.AppendLine($"元素: {TextResourceTable.Get(characterMb.ElementType)}");
        msg.AppendLine($"职业: {TextResourceTable.Get(characterMb.JobFlags)}");
        msg.AppendLine($"速度: {characterMb.InitialBattleParameter.Speed}");
        msg.AppendLine($"身高: {profile.Height}cm");
        msg.AppendLine($"体重: {profile.Weight}kg");
        msg.AppendLine($"血型: {profile.BloodType}");
        var birthDay = profile.Birthday % 100;
        var birthMonth = profile.Birthday / 100;
        msg.AppendLine($"生日: {birthMonth}月{birthDay}日");
        msg.AppendLine($"声优: {TextResourceTable.Get(profile.CharacterVoiceJPKey)}");
        msg.AppendLine($"演唱: {TextResourceTable.Get(profile.VocalJPKey)}");
        msg.AppendLine($"抒情诗: {TextResourceTable.Get(profile.LamentJPKey)}");
        msg.AppendLine($"{TextResourceTable.Get(profile.DescriptionKey)}");

        await _sessionAccessor.Session.SendGroupMessageAsync(context.GroupId, new CqMessage(msg.ToString()));
    }

    private const string tableStyle = """
                                      <style>
                                      table {
                                        font-family: arial, sans-serif;
                                        border-collapse: collapse;
                                        width: 100%;
                                      }

                                      td, th {
                                        border: 1px solid #dddddd;
                                        text-align: left;
                                        padding: 4px;
                                      }
                                      </style>
                                      """;

    [CqMessageMatch(@"^/主线\s*(?<quest>\d+-\d+)$")]
    public async Task QueryMainQuerst(CqGroupMessagePostContext context, string quest)
    {
        if (!IsGroupAllowed(context)) return;
        _logger.LogInformation($"{nameof(QueryMainQuerst)} {quest}");

        var questMb = QuestTable.GetArray().FirstOrDefault(d => d.Memo == quest);
        if (questMb == null)
        {
            await _sessionAccessor.Session.SendGroupMessageAsync(context.GroupId, new CqMessage("未找到关卡"));
            return;
        }

        var msg = new StringBuilder(tableStyle);
        msg.AppendLine($"<h1>主线 {quest}</h1> <span>战斗力: {CalcNumber(questMb.BaseBattlePower)}</span> <span>潜能宝珠: {CalcNumber(questMb.PotentialJewelPerDay)}</span>");
        var enemies = new List<BossBattleEnemyMB>();
        for (var i = 1; i < 6; i++)
        {
            var enemyId = 20000000 + questMb.Id * 100 + i;
            enemies.Add(BossBattleEnemyTable.GetById(enemyId));
        }

        BuildEnemyInfo(enemies, msg);

        var bytes = ImageUtil.HtmlToImage(msg.ToString(), 1200);

        var cqImageMsg = CqImageMsg.FromBytes(bytes);
        await _sessionAccessor.Session.SendGroupMessageAsync(context.GroupId, new CqMessage(cqImageMsg));
    }

    private static void BuildEnemyInfo(IReadOnlyList<IBattleEnemy> enemies, StringBuilder msg)
    {
        msg.AppendLine(@"<table><tr>
<th>稀有</th>
<th>Lv</th>
<th>名称</th>
<th>素</th>
<th>速度</th>
<th>共鸣</th>
<th>攻击</th>
<th>防御</th>
<th>血量</th>
<th>力量</th>
<th>技力</th>
<th>魔力</th>
<th>耐力</th>
</tr>");
        foreach (var enemyMb in enemies)
        {
            // msg.AppendLine();
            var rarity = enemyMb.CharacterRarityFlags.GetDesc();
            var lv = enemyMb.EnemyRank;
            var name = TextResourceTable.Get(enemyMb.NameKey);
            var ele = enemyMb.ElementType.GetDesc();
            var connect = "";
            if (enemies.MaxBy(d => d.BattleParameter.Defense) == enemyMb)
            {
                connect = $"高";
            }
            else if (enemies.MinBy(d => d.BattleParameter.Defense) == enemyMb)
            {
                connect = $"低";
            }

            msg.AppendLine(@$"<tr>
<td>{rarity}</td><td>{lv}</td>
<td>{name}</td><td>{ele}</td>
<td>{enemyMb.BattleParameter.Speed}</td>
<td>{connect}</td>
<td>{CalcNumber(enemyMb.BattleParameter.AttackPower)}</td>
<td>{CalcNumber(enemyMb.BattleParameter.Defense)}</td>
<td>{CalcNumber(enemyMb.BattleParameter.HP)}</td>
<td>{CalcNumber(enemyMb.BaseParameter.Muscle)}</td>
<td>{CalcNumber(enemyMb.BaseParameter.Energy)}</td>
<td>{CalcNumber(enemyMb.BaseParameter.Intelligence)}</td>
<td>{CalcNumber(enemyMb.BaseParameter.Health)}</td>
</tr>");
        }

        msg.AppendLine("</table>");
    }

    /// <summary>
    /// 将一个数字转换为可读的字符串，如 1000000 -> 100万，1000000000 -> 10亿，可以设置精确到的小数点位数
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    private static string CalcNumber(long value, int precision = 0)
    {
        var unit = new[] {"", "万", "亿", "万亿"};
        var index = 0;
        var d = (double) value;
        while (d >= 10000)
        {
            d /= 10000;
            index++;
        }

        var num = d.ToString($"F7");
        num = num.Substring(0, 5);
        // 去掉最后的小数点
        if (num.EndsWith("."))
        {
            num = num.Substring(0, num.Length - 1);
        }

        return $"{num}{unit[index]}";
    }

    [CqMessageMatch(@"^/(?<towerTypeStr>(无穷|红|黄|金|绿|翠|蓝))塔\s*(?<quest>\d+)$")]
    public async Task QueryTowerInfo(CqGroupMessagePostContext context, string towerTypeStr, string quest)
    {
        if (!IsGroupAllowed(context)) return;
        _logger.LogInformation($"{nameof(QueryTowerInfo)} {towerTypeStr} {quest}");

        var towerType = towerTypeStr switch
        {
            "无穷" => TowerType.Infinite,
            "红" => TowerType.Red,
            "黄" => TowerType.Yellow,
            "金" => TowerType.Yellow,
            "绿" => TowerType.Green,
            "翠" => TowerType.Green,
            "蓝" => TowerType.Blue,
        };
        var questId = long.Parse(quest);

        var questMb = TowerBattleQuestTable.GetByTowerTypeAndFloor(questId, towerType);
        if (questMb == null)
        {
            await _sessionAccessor.Session.SendGroupMessageAsync(context.GroupId, new CqMessage("未找到关卡"));
            return;
        }

        var msg = new StringBuilder(tableStyle);
        msg.AppendLine($"<h1>{towerTypeStr}塔 {quest}</h1>");
        var enemies = new List<TowerBattleEnemyMB>();
        foreach (var enemyId in questMb.EnemyIds)
        {
            enemies.Add(TowerBattleEnemyTable.GetById(enemyId));
        }

        BuildEnemyInfo(enemies, msg);

        var bytes = ImageUtil.HtmlToImage(msg.ToString(), 1200);

        var cqImageMsg = CqImageMsg.FromBytes(bytes);
        await _sessionAccessor.Session.SendGroupMessageAsync(context.GroupId, new CqMessage(cqImageMsg));
    }

    [CqMessageMatch(@"^/(?<rankType>战力|等级|主线|塔)排名\s*(?<server>日|韩|亚|美|欧|国际)(?<worldStr>\d+)$")]
    public async Task GetPlayerRanking(CqGroupMessagePostContext context, string rankType, string server, string worldStr)
    {
        if (!IsGroupAllowed(context)) return;
        _logger.LogInformation($"{nameof(GetPlayerRanking)} {rankType} {server} {worldStr}");

        var world = int.Parse(worldStr);
        var worldId = server switch
        {
            "日" => 1000 + world,
            "韩" => 2000 + world,
            "亚" => 3000 + world,
            "美" => 4000 + world,
            "欧" => 5000 + world,
            "国际" => 6000 + world,
            _ => 1000 + world,
        };

        var playerRanking = await _mentemoriIcu.PlayerRanking(worldId);
        if (playerRanking.status != 200)
        {
            await _sessionAccessor.Session.SendGroupMessageAsync(context.GroupId, new CqMessage($"获取失败：{playerRanking.status}"));
            return;
        }

        var msg = new StringBuilder(tableStyle);
        msg.AppendLine($"<h1>{server}{world} {rankType}排名</h1>");
        var infos = rankType switch
        {
            "战力" => playerRanking.data.rankings.bp,
            "等级" => playerRanking.data.rankings.rank,
            "主线" => playerRanking.data.rankings.quest,
            "塔" => playerRanking.data.rankings.tower,
            _ => playerRanking.data.rankings.bp
        };
        Func<PlayerInfo, string> selector = rankType switch
        {
            "战力" => p => p.bp.ToString("N0"),
            "等级" => p => p.rank.ToString(),
            "主线" => p => QuestTable.GetById(p.quest_id).Memo,
            "塔" => p => p.tower_id.ToString(),
            _ => p => p.bp.ToString("N0")
        };
        msg.AppendLine("<table><tbody>");
        for (var i = 0; i < infos.Count; i++)
        {
            var playerInfo = infos[i];
            msg.AppendLine($"<tr><td>No.{i + 1:00}</td><td>{playerInfo.name}</td><td>{selector(playerInfo)}</td></tr>");
        }

        msg.AppendLine("</tbody></table>");

        var bytes = ImageUtil.HtmlToImage(msg.ToString(), null);

        var cqImageMsg = CqImageMsg.FromBytes(bytes);
        await _sessionAccessor.Session.SendGroupMessageAsync(context.GroupId, new CqMessage(cqImageMsg));
        // await _sessionAccessor.Session.SendGroupMessageAsync(context.GroupId, new CqMessage(msg.ToString()));
    }

    [CqMessageMatch(@"^/竞技场排名\s*(?<server>日|韩|亚|美|欧|国际)(?<worldStr>\d+)$")]
    public async Task GetArenaRanking(CqGroupMessagePostContext context, string server, string worldStr)
    {
        if (!IsGroupAllowed(context)) return;
        _logger.LogInformation($"{nameof(GetArenaRanking)} {server} {worldStr}");

        var world = int.Parse(worldStr);
        var worldId = server switch
        {
            "日" => 1000 + world,
            "韩" => 2000 + world,
            "亚" => 3000 + world,
            "美" => 4000 + world,
            "欧" => 5000 + world,
            "国际" => 6000 + world,
            _ => 1000 + world,
        };

        var arena = await _mentemoriIcu.Arena(worldId);

        var msg = new StringBuilder();
        msg.AppendLine($"{server}{worldStr} 竞技场排名");

        for (var i = 0; i < arena.data.Count; i++)
        {
            var arenaInfo = arena.data[i];
            msg.AppendLine($"No.{i + 1:000}\t{arenaInfo.PlayerName}(Lv.{arenaInfo.PlayerLevel})");
        }

        await _sessionAccessor.Session.SendGroupMessageAsync(context.GroupId, new CqMessage(msg.ToString()));
    }

    [CqMessageMatch(@"^/公告\s*?(?<noticeIdStr>\d+)?$")]
    public async Task RecentNotice(CqGroupMessagePostContext context, string? noticeIdStr)
    {
        if (!IsGroupAllowed(context)) return;
        _logger.LogInformation($"{nameof(RecentNotice)} {noticeIdStr}");
        CqMessage msg;
        if (int.TryParse(noticeIdStr, out var noticeId))
        {
            using var db = _dbAccessor.GetDb();
            var noticeInfo = noticeId is > 0 and <= 20
                ? db.GetCollection<NoticeInfo>().FindAll().OrderByDescending(d=>d.Id).Take(30).ToList().Where(d => d.Id % 10 != 6).Skip(noticeId - 1).FirstOrDefault()
                : db.GetCollection<NoticeInfo>().FindById(noticeId);
            if (noticeInfo == null)
            {
                msg = new CqMessage("未找到此公告");
            }
            else
            {
                var notice = new StringBuilder();
                notice.AppendLine($"<h1>{noticeInfo.Title}({noticeInfo.Id})</h1>");
                notice.AppendLine();
                var mainText = $"<div>{noticeInfo.MainText}</div>";
                notice.AppendLine(mainText);
                var bytes = ImageUtil.HtmlToImage(notice.ToString());
                var cqImageMsg = CqImageMsg.FromBytes(bytes);
                msg = new CqMessage(cqImageMsg, new CqTextMsg(noticeInfo.Title));
            }
        }
        else
        {
            using var db = _dbAccessor.GetDb();
            var noticeInfos = db.GetCollection<NoticeInfo>().Query().OrderByDescending(d => d.Id).Limit(30).ToList();
            var list = new StringBuilder();
            for (var i = 0; i < noticeInfos.Where(d => d.Id % 10 != 6).Take(15).ToArray().Length; i++)
            {
                var noticeInfo = noticeInfos.Where(d => d.Id % 10 != 6).Take(15).ToArray()[i];
                list.AppendLine($"({i + 1}) {noticeInfo.Title}");
            }

            msg = new CqMessage(list.ToString());
        }

        await _sessionAccessor.Session.SendGroupMessageAsync(context.GroupId, msg);
    }

    [CqMessageMatch(@"^/更新主数据$")]
    public async Task UpdateMasterData(CqGroupMessagePostContext context)
    {
        if (!IsGroupAllowed(context)) return;
        if (!IsSenderAdmin(context)) return;
        _logger.LogInformation($"{nameof(UpdateMasterData)}");
        var msg = "";
        if (await _networkManager.DownloadMasterCatalog())
        {
            _networkManager.SetCultureInfo(_networkManager.CultureInfo);
            msg = "更新完成";
        }
        else
        {
            msg = "暂无更新";
        }

        await _sessionAccessor.Session.SendGroupMessageAsync(context.GroupId, new CqMessage(msg));
    }

    private bool IsSenderAdmin(CqGroupMessagePostContext context)
    {
        return _botOptions.Value.AdminIds.Contains(context.Sender.UserId);
    }
}