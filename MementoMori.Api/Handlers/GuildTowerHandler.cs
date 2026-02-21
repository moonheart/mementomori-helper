using MementoMori.Api.Extensions;
using MementoMori.Api.Infrastructure;
using MementoMori.Api.Services;
using MementoMori.Api.Utils;
using MementoMori.Ortega.Share;
using MementoMori.Ortega.Share.Data.ApiInterface.Guild;
using MementoMori.Ortega.Share.Data.ApiInterface.GuildTower;
using MementoMori.Ortega.Share.Enums;

namespace MementoMori.Api.Handlers;

/// <summary>
/// 公会塔自动战斗处理器
/// </summary>
[RegisterTransient]
[AutoConstructor]
public partial class GuildTowerHandler : IGameActionHandler
{
    private readonly ILogger<GuildTowerHandler> _logger;
    private readonly JobLogger _jobLogger;
    private readonly PlayerSettingService _playerSettingService;

    public string ActionName => "公会塔自动战斗";

    public async Task ExecuteAsync(AccountContext context)
    {
        var userId = context.AccountInfo.UserId;
        var nm = context.NetworkManager;
        var timeManager = context.TimeManager;

        await _jobLogger.LogAsync(userId, "正在检查公会塔...");

        try
        {
            var guildTowerSettings = await _playerSettingService.GetGuildTowerSettingsAsync(userId);

            var guildTowerEventMb = Masters.GuildTowerEventTable.GetByInTime(timeManager.IsInTime);
            if (guildTowerEventMb == null)
            {
                await _jobLogger.LogAsync(userId, "当前无公会塔活动。");
                return;
            }

            // 获取公会ID
            var guildId = await nm.SendRequest<GetGuildIdRequest, GetGuildIdResponse>(new GetGuildIdRequest());
            if (guildId.GuildId == 0)
            {
                await _jobLogger.LogAsync(userId, "未加入公会，跳过公会塔。");
                return;
            }

            var guildTowerInfo = await nm.SendRequest<GetGuildTowerInfoRequest, GetGuildTowerInfoResponse>(
                new GetGuildTowerInfoRequest());

            // 自动报名
            if (!guildTowerInfo.IsAlreadyEntryToday)
            {
                if (!guildTowerSettings.AutoEntry)
                {
                    await _jobLogger.LogAsync(userId, "公会塔自动报名未开启，跳过。");
                    return;
                }

                // 按战斗力排序选择前20个角色
                var guids = nm.UserSyncData.UserCharacterDtoInfos
                    .Select(d => new { Dto = d, Power = BattlePowerCalculatorUtil.GetUserCharacterBattlePower(nm, d) })
                    .OrderByDescending(d => d.Power)
                    .Take(20)
                    .Select(d => d.Dto.Guid)
                    .ToList();

                await nm.SendRequest<EntryCharacterRequest, EntryCharacterResponse>(
                    new EntryCharacterRequest
                    {
                        CharacterGuidList = guids,
                        GuildTowerEntryType = GuildTowerEntryType.Entry,
                        IsContinueEntry = false
                    });

                await _jobLogger.LogAsync(userId, "公会塔已报名。");
                guildTowerInfo = await nm.SendRequest<GetGuildTowerInfoRequest, GetGuildTowerInfoResponse>(
                    new GetGuildTowerInfoRequest());
            }

            // 自动挑战
            if (guildTowerSettings.AutoChallenge)
            {
                var winCount = 0;
                while (guildTowerInfo.TodayWinCount < 3)
                {
                    var nextFloor = Masters.GuildTowerFloorTable.GetById(guildTowerInfo.CurrentFloorMBId);
                    if (nextFloor == null) break;

                    // 获取最少使用的职业
                    var entryCharactersWithInfo = guildTowerInfo.GuildTowerEntryCharacterList
                        .Select(d => new
                        {
                            EntryChar = d,
                            CharInfo = nm.UserSyncData.GetUserCharacterInfoByUserCharacterGuid(d.CharacterGuid)
                        })
                        .Where(d => d.CharInfo != null)
                        .ToList();

                    var jobFlag = entryCharactersWithInfo
                        .GroupBy(d => Masters.CharacterTable.GetById(d.CharInfo.CharacterId).JobFlags)
                        .OrderBy(d => d.Sum(x => x.EntryChar.TodayUseCount))
                        .FirstOrDefault()?.Key ?? JobFlags.Warrior;

                    var guids = entryCharactersWithInfo
                        .Where(d => Masters.CharacterTable.GetById(d.CharInfo.CharacterId).JobFlags == jobFlag)
                        .Select(d => new { d.CharInfo, Power = BattlePowerCalculatorUtil.GetUserCharacterBattlePower(nm, d.CharInfo) })
                        .OrderByDescending(d => d.Power)
                        .Take(5)
                        .Select(d => d.CharInfo.Guid)
                        .ToList();

                    if (guids.Count == 0) break;

                    // 从最高难度开始尝试
                    var isWin = false;
                    for (var i = nextFloor.SelectableDifficultyList.Count - 1; i >= 0 && !isWin; i--)
                    {
                        var retryCount = Math.Max(1, guildTowerSettings.AutoChallengeRetryCount);
                        retryCount = Math.Min(retryCount, 10); // 限制最大重试次数

                        for (var j = 0; j < retryCount; j++)
                        {
                            try
                            {
                                var battleResponse = await nm.SendRequest<BattleRequest, BattleResponse>(
                                    new BattleRequest
                                    {
                                        CharacterGuidList = guids,
                                        Difficulty = nextFloor.SelectableDifficultyList[i],
                                        GuildTowerFloor = nextFloor.FloorCount
                                    });

                                if (battleResponse.BattleResult.SimulationResult.BattleEndInfo.IsWinAttacker())
                                {
                                    isWin = true;
                                    winCount++;
                                    break;
                                }
                            }
                            catch (NetworkManager.ApiErrorException ex) when (ex.Message.Contains("AlreadyClearFloor"))
                            {
                                isWin = true;
                                break;
                            }
                            catch
                            {
                                // 继续尝试
                            }
                        }
                    }

                    guildTowerInfo = await nm.SendRequest<GetGuildTowerInfoRequest, GetGuildTowerInfoResponse>(
                        new GetGuildTowerInfoRequest());
                }

                await _jobLogger.LogAsync(userId, $"公会塔挑战完成，胜利 {winCount} 场。");
            }

            // 领取层奖励
            if (guildTowerSettings.AutoReceiveReward)
            {
                guildTowerInfo = await nm.SendRequest<GetGuildTowerInfoRequest, GetGuildTowerInfoResponse>(
                    new GetGuildTowerInfoRequest());

                var notRewardedIds = Masters.GuildTowerFloorTable.GetListByEventId(guildTowerEventMb.EventNo)
                    .Where(d =>
                        d.Id < guildTowerInfo.CurrentFloorMBId &&
                        d.FloorRewardList != null &&
                        d.FloorRewardList.Count > 0 &&
                        !nm.UserSyncData.ReceivedGuildTowerFloorRewardIdList.Contains(d.Id))
                    .Select(d => d.Id)
                    .ToList();

                if (notRewardedIds.Count > 0)
                {
                    await nm.SendRequest<ReceiveFloorRewardRequest, ReceiveFloorRewardResponse>(
                        new ReceiveFloorRewardRequest { FloorRewardMBIdList = notRewardedIds });
                    await _jobLogger.LogAsync(userId, $"公会塔层奖励已领取 {notRewardedIds.Count} 项。");
                }
            }

            await _jobLogger.LogAsync(userId, "公会塔处理完毕。");
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "公会塔处理失败 for user {UserId}", userId);
        }
    }
}
