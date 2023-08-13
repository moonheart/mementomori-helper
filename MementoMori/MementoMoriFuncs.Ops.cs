using System.Collections.ObjectModel;
using MementoMori.Extensions;
using MementoMori.Ortega.Share;
using MementoMori.Ortega.Share.Data.ApiInterface.Battle;
using MementoMori.Ortega.Share.Data.ApiInterface.BountyQuest;
using MementoMori.Ortega.Share.Data.ApiInterface.Equipment;
using MementoMori.Ortega.Share.Data.ApiInterface.Friend;
using MementoMori.Ortega.Share.Data.ApiInterface.Gacha;
using MementoMori.Ortega.Share.Data.ApiInterface.Guild;
using MementoMori.Ortega.Share.Data.ApiInterface.GuildRaid;
using MementoMori.Ortega.Share.Data.ApiInterface.LoginBonus;
using MementoMori.Ortega.Share.Data.ApiInterface.Mission;
using MementoMori.Ortega.Share.Data.ApiInterface.Present;
using MementoMori.Ortega.Share.Data.ApiInterface.TowerBattle;
using MementoMori.Ortega.Share.Data.ApiInterface.User;
using MementoMori.Ortega.Share.Data.ApiInterface.Vip;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Utils;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using TowerBattleStartRequest = MementoMori.Ortega.Share.Data.ApiInterface.TowerBattle.StartRequest;
using TowerBattleStartResponse = MementoMori.Ortega.Share.Data.ApiInterface.TowerBattle.StartResponse;
using BountyQuestStartRequest = MementoMori.Ortega.Share.Data.ApiInterface.BountyQuest.StartRequest;
using BountyQuestStartResponse = MementoMori.Ortega.Share.Data.ApiInterface.BountyQuest.StartResponse;
using BountyQuestGetListRequest = MementoMori.Ortega.Share.Data.ApiInterface.BountyQuest.GetListRequest;
using BountyQuestGetListResponse = MementoMori.Ortega.Share.Data.ApiInterface.BountyQuest.GetListResponse;
using GachaGetListRequest = MementoMori.Ortega.Share.Data.ApiInterface.Gacha.GetListRequest;
using GachaGetListResponse = MementoMori.Ortega.Share.Data.ApiInterface.Gacha.GetListResponse;

namespace MementoMori;

public partial class MementoMoriFuncs: ReactiveObject
{
    [Reactive]
    public bool Logining { get; private set; }
    [Reactive]
    public bool IsQuickActionExecuting { get; private set; }
    [Reactive]
    public string EquipmentId { get; set; }
    [Reactive]
    public string EquipmentTrainingTargetType { get; set; }
    [Reactive]
    public long EquipmentTrainingTargetValue { get; set; }

    private CancellationTokenSource _cancellationTokenSource;

    public ObservableCollection<string> MesssageList { get; } = new();

    public async Task Login()
    {
        Logining = true;
        await AuthLogin();
        var userData = await UserGetUserData();
        Logining = false;
    }

    public async Task SyncUserData()
    {
        await UserGetUserData();
    }

    private void AddLog(string message)
    {
        MesssageList.Insert(0, message);
        if (MesssageList.Count > 10000)
        {
            MesssageList.RemoveAt(MesssageList.Count - 1);
        }
    }

    public async Task ExecuteQuickAction(Func<Action<string>, CancellationToken, Task> func)
    {
        IsQuickActionExecuting = true;
        _cancellationTokenSource = new CancellationTokenSource();
        MesssageList.Clear();
        try
        {
            await func(AddLog, _cancellationTokenSource.Token);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            AddLog(e.ToString());
        }
        finally
        {
            IsQuickActionExecuting = false;
        }
    }

    public void CancelQuickAction()
    {
        if (_cancellationTokenSource == null) return;
        _cancellationTokenSource.Cancel();
    }

    public async Task GetLoginBonus()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            var bonus = await GetResponse<ReceiveDailyLoginBonusRequest, ReceiveDailyLoginBonusResponse>(new ReceiveDailyLoginBonusRequest() {ReceiveDay = DateTime.Now.Day});
            log("领取的奖励：\n");
            bonus.RewardItemList.PrintUserItems(log);
        });
    }

    public async Task GetVipGift()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            var bonus = await GetResponse<GetDailyGiftRequest, GetDailyGiftResponse>(new GetDailyGiftRequest());
            log("领取的奖励：");
            bonus.ItemList.PrintUserItems(log);
        });
    }

    public async Task GetAutoBattleReward()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            var bonus = await GetResponse<RewardAutoBattleRequest, RewardAutoBattleResponse>(new RewardAutoBattleRequest());
            log("领取的奖励：");

            log($"战斗次数 {bonus.AutoBattleRewardResult.BattleCountAll}");
            log($"胜利次数 {bonus.AutoBattleRewardResult.BattleCountWin}");
            log($"总时间 {TimeSpan.FromMilliseconds(bonus.AutoBattleRewardResult.BattleTotalTime)}");
            log($"领民金币 {bonus.AutoBattleRewardResult.GoldByPopulation}");
            log($"领民潜能珠宝 {bonus.AutoBattleRewardResult.PotentialJewelByPopulation}");
        });
    }

    public async Task BulkTransferFriendPoint()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            var resp = await GetResponse<BulkTransferFriendPointRequest, BulkTransferFriendPointResponse>(new BulkTransferFriendPointRequest());
            log("成功");
        });
    }

    public async Task PresentReceiveItem()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            var resp = await GetResponse<ReceiveItemRequest, ReceiveItemResponse>(new ReceiveItemRequest() {LanguageType = LanguageType.zhTW});
            ;
            log("领取的奖励：");
            resp.ResultItems.Select(d => d.Item).PrintUserItems(log);
        });
    }

    public async Task BattleBossQuick()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            log("领取的奖励：\n");
            var bossQuickResponse = await GetResponse<BossQuickRequest, BossQuickResponse>(
                new BossQuickRequest()
                {
                    QuestId = UserSyncData.UserBattleBossDtoInfo.BossClearMaxQuestId,
                    QuickCount = 3
                });
            if (bossQuickResponse.BattleRewardResult == null) return;


            bossQuickResponse.BattleRewardResult.FixedItemList.PrintUserItems(log);
            bossQuickResponse.BattleRewardResult.DropItemList.PrintUserItems(log);
        });
    }

    public async Task InfiniteTowerQuick()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            var tower = UserSyncData.UserTowerBattleDtoInfos.First(d => d.TowerType == TowerType.Infinite);
            log("领取的奖励：\n");

            var bossQuickResponse = await GetResponse<TowerBattleQuickRequest, TowerBattleQuickResponse>(
                new TowerBattleQuickRequest()
                {
                    TargetTowerType = TowerType.Infinite, TowerBattleQuestId = tower.MaxTowerBattleId, QuickCount = 3
                });
            if (bossQuickResponse.BattleRewardResult != null)
            {
                bossQuickResponse.BattleRewardResult.FixedItemList.PrintUserItems(log);
                bossQuickResponse.BattleRewardResult.DropItemList.PrintUserItems(log);
            }
        });
    }

    public async Task PvpAuto()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            log("领取的奖励：\n");
            for (int i = 0; i < 5; i++)
            {
                var pvpInfoResponse = await GetResponse<GetPvpInfoRequest, GetPvpInfoResponse>(
                    new GetPvpInfoRequest());

                var pvpRankingPlayerInfo = pvpInfoResponse.MatchingRivalList.OrderBy(d => d.DefenseBattlePower).First();

                var pvpStartResponse = await GetResponse<PvpStartRequest, PvpStartResponse>(new PvpStartRequest()
                {
                    RivalPlayerRank = pvpRankingPlayerInfo.CurrentRank,
                    RivalPlayerId = pvpRankingPlayerInfo.PlayerInfo.PlayerId
                });

                pvpStartResponse.BattleRewardResult.FixedItemList.PrintUserItems(log);
                pvpStartResponse.BattleRewardResult.DropItemList.PrintUserItems(log);
            }
        });
    }

    public async Task BountyQuestRewardAuto()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            log("领取的奖励：\n");
            var getListResponse = await GetResponse<BountyQuestGetListRequest, BountyQuestGetListResponse>(
                new BountyQuestGetListRequest());

            var questIds = getListResponse.UserBountyQuestDtoInfos
                .Where(d => d.BountyQuestEndTime > 0)
                .Select(d => d.BountyQuestId).ToList();

            if (questIds.Count > 0)
            {
                var rewardResponse = await GetResponse<RewardRequest, RewardResponse>(new RewardRequest() {BountyQuestIds = questIds, IsQuick = false});
                rewardResponse.RewardItems.PrintUserItems(log);
            }
        });
    }

    public async Task BountyQuestStartAuto()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            var response1 = await GetResponse<BountyQuestGetListRequest, BountyQuestGetListResponse>(new());
            var bountyQuestStartInfos = BountyQuestAutoFormationUtil.CalcAutoFormation(response1, UserSyncData);
            var startResponse = await GetResponse<BountyQuestStartRequest, BountyQuestStartResponse>(new(){BountyQuestStartInfos = bountyQuestStartInfos});
            log(startResponse.ToJson());
        });
    }

    public async Task AutoEquipmentInheritance()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            await AutoEquipmentInheritance(log);
            // var msg = new StringBuilder("角色列表：\n");
            //
            // foreach (var userCharacterDtoInfo in _userSyncData.UserCharacterDtoInfos)
            // {
            //     var characterMb = Masters.CharacterTable.GetById(userCharacterDtoInfo.CharacterId);
            //     var name = Masters.TextResourceTable.Get(characterMb.NameKey);
            //     msg.AppendLine($"名称：{name} 等级： {userCharacterDtoInfo.Level} 稀有度：{characterMb.RarityFlags}");
            // }
            log("完成");
        });
    }

    public async Task BossHishSpeedBattle()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            log("领取的奖励：\n");

            var req = new QuickRequest() {QuestQuickExecuteType = QuestQuickExecuteType.Currency, QuickCount = 1};
            var quickResponse = await GetResponse<QuickRequest, QuickResponse>(req);

            log($"金币 {quickResponse.AutoBattleRewardResult.GoldByPopulation}");
            log($"潜能宝珠 {quickResponse.AutoBattleRewardResult.PotentialJewelByPopulation}");
            log($"角色经验 {quickResponse.AutoBattleRewardResult.BattleRewardResult.CharacterExp}");
            log($"额外金币 {quickResponse.AutoBattleRewardResult.BattleRewardResult.ExtraGold}");
            log($"用户经验 {quickResponse.AutoBattleRewardResult.BattleRewardResult.PlayerExp}");
            log($"升级 {quickResponse.AutoBattleRewardResult.BattleRewardResult.RankUp}");

            quickResponse.AutoBattleRewardResult.BattleRewardResult.FixedItemList.PrintUserItems(log);
            quickResponse.AutoBattleRewardResult.BattleRewardResult.DropItemList.PrintUserItems(log);
        });
    }

    public async Task AutoDungeonBattle()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            await AutoDungeonBattle(log);
            log("完成");
        });
    }

    public async Task GetMyPage()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            Mypage = await GetResponse<GetMypageRequest, GetMypageResponse>(new GetMypageRequest());
            log("完成");
        });
    }

    public async Task Debug()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            var giveAllSrCharacterResponse = await GetResponse<GetMypageRequest, GetMypageResponse>(new GetMypageRequest());
            log(giveAllSrCharacterResponse.ToJson(true));
        });
    }

    public async Task LogDebug()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            var counter = 0;
            while (!token.IsCancellationRequested)
            {
                log($"Message {counter++}");
                await Task.Delay(TimeSpan.FromMilliseconds(500));
            }
            Console.WriteLine("DDDDD");
        });
    }

    public async Task UseFriendCode()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            var useFriendCodeResponse = await GetResponse<UseFriendCodeRequest, UseFriendCodeResponse>(new UseFriendCodeRequest() {FriendCode = ""});
        });
    }

    public async Task FreeGacha()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            while (await DoFreeGacha())
            {
            }
            
            async Task<bool> DoFreeGacha()
            {
                var gachaListResponse = await GetResponse<GachaGetListRequest, GachaGetListResponse>(new GachaGetListRequest());
                foreach (var gachaCaseInfo in gachaListResponse.GachaCaseInfoList)
                {
                    var buttonInfo = gachaCaseInfo.GachaButtonInfoList.OrderByDescending(d => d.LotteryCount).FirstOrDefault(d =>
                        d.ConsumeUserItem == null ||
                        ((d.ConsumeUserItem.ItemType == ItemType.CurrencyFree || d.ConsumeUserItem.ItemType == ItemType.CurrencyPaid) && d.ConsumeUserItem.ItemCount == 0) ||
                        d.ConsumeUserItem.ItemType == ItemType.Gold);
                    if (buttonInfo == null)
                    {
                        continue;
                    }
                    var gachaCaseMb = Masters.GachaCaseTable.GetById(gachaCaseInfo.GachaCaseId);
                    log($"抽卡 {gachaCaseMb.Memo} {buttonInfo.LotteryCount}次 消耗 {buttonInfo.ConsumeUserItem.ItemCount}个 {buttonInfo.ConsumeUserItem.ItemType}");
                    var response = await GetResponse<DrawRequest, DrawResponse>(new DrawRequest() {GachaButtonId = buttonInfo.GachaButtonId});
                    response.GachaRewardItemList.PrintUserItems(log);
                    response.BonusRewardItemList.PrintUserItems(log);
                    return true;
                }

                return false;
            }
        });

        
    }

    public async Task GuildCheckin()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            var response1 = await GetResponse<GetGuildIdRequest, GetGuildIdResponse>(new GetGuildIdRequest());
            log($"公会 Id {response1.GuildId}");
            var response2 = await GetResponse<GetGuildBaseInfoRequest, GetGuildBaseInfoResponse>(new GetGuildBaseInfoRequest() {BelongGuildId = response1.GuildId});
            log("签到成功");
            response2.UserSyncData.GivenItemCountInfoList.PrintUserItems(log);
        });
    }

    public async Task GuildRaid()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            var response1 = await GetResponse<GetGuildIdRequest, GetGuildIdResponse>(new GetGuildIdRequest());
            log($"公会 Id {response1.GuildId}");
            while (true)
            {
                var response2 = await GetResponse<GetGuildRaidInfoRequest, GetGuildRaidInfoResponse>(new GetGuildRaidInfoRequest() {BelongGuildId = response1.GuildId});
                if (response2.GuildRaidInfos.All(d => !d.IsOpen || d.UserGuildRaidDtoInfo is {ChallengeCount: >= 2}))
                {
                    log("没有可扫荡的讨伐战");
                    break;
                }

                foreach (var info in response2.GuildRaidInfos)
                {
                    if (info.IsOpen && (info.UserGuildRaidDtoInfo == null || info.UserGuildRaidDtoInfo is {ChallengeCount: < 2}))
                    {
                        var response3 = await GetResponse<QuickStartGuildRaidRequest, QuickStartGuildRaidResponse>(new QuickStartGuildRaidRequest()
                            {BelongGuildId = response1.GuildId, GuildRaidBossType = info.GuildRaidDtoInfo.BossType});
                        log($"战斗结果: {response3.BattleSimulationResult.BattleEndInfo.IsWinAttacker()}");
                        log("固定掉落");
                        response3.BattleRewardResult.FixedItemList.PrintUserItems(log);
                        log("随机掉落");
                        response3.BattleRewardResult.DropItemList.PrintUserItems(log);
                    }
                }
            }
        });
    }

    public async Task AutoBossRequest()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            int totalCount = 0;
            int winCount = 0;
            int errCount = 0;
            while (!token.IsCancellationRequested)
            {
                try
                {
                    await UserGetUserData();
                    var bossResponse = await GetResponse<BossRequest, BossResponse>(new BossRequest() {QuestId = UserSyncData.UserBattleBossDtoInfo.BossClearMaxQuestId + 1});
                    var win = bossResponse.BattleResult.SimulationResult.BattleEndInfo.IsWinAttacker();
                    totalCount++;
                    if (win)
                    {
                        var nextQuestResponse = await GetResponse<NextQuestRequest, NextQuestResponse>(new NextQuestRequest());
                        winCount++;
                    }

                    var m = $"挑战 boss 一次：{win} 总次数：{totalCount} 胜利次数：{winCount}, Err: {errCount}";
                    Console.WriteLine(m);
                    log(m);

                    var t = 1;
                    await Task.Delay(t);
                }
                catch (Exception e)
                {
                    errCount++;
                    if (errCount > 10)
                    {
                        return;
                    }

                    while (true)
                    {
                        try
                        {
                            await AuthLogin();
                            break;
                        }
                        catch (Exception)
                        {
                            await Task.Delay(1000);
                        }
                    }
                }
            }
        });
    }

    public async Task AutoInfiniteTowerRequest()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            int totalCount = 0;
            int winCount = 0;
            int errCount = 0;
            while (!token.IsCancellationRequested)
            {
                try
                {
                    await UserGetUserData();
                    var tower = UserSyncData.UserTowerBattleDtoInfos.First(d => d.TowerType == TowerType.Infinite);
                    var bossQuickResponse = await GetResponse<TowerBattleStartRequest, TowerBattleStartResponse>(
                        new TowerBattleStartRequest()
                        {
                            TargetTowerType = TowerType.Infinite, TowerBattleQuestId = tower.MaxTowerBattleId + 1
                        });
                    var win = bossQuickResponse.BattleResult.SimulationResult.BattleEndInfo.IsWinAttacker();
                    totalCount++;
                    if (win)
                    {
                        winCount++;
                    }

                    var m = $"挑战无穷之塔一次：{win} 总次数：{totalCount} 胜利次数：{winCount}, Err: {errCount}";
                    Console.WriteLine(m);
                    log(m);

                    var t = 1;
                    await Task.Delay(t);
                }
                catch (Exception e)
                {
                    errCount++;
                    if (errCount > 10)
                    {
                        return;
                    }

                    while (true)
                    {
                        try
                        {
                            await AuthLogin();
                            break;
                        }
                        catch (Exception)
                        {
                            await Task.Delay(1000);
                        }
                    }
                }
            }
        });
    }

    public async Task AutoEquipmentTraning()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            int totalCount = 0;
            while (!token.IsCancellationRequested)
            {
                await UserGetUserData();
                var equipment = UserSyncData.UserEquipmentDtoInfos.First(d => d.Guid == EquipmentId);
                var m =
                    $"打磨装备 {totalCount} 耐力 {equipment.AdditionalParameterEnergy} 魔力 {equipment.AdditionalParameterIntelligence} 力量 {equipment.AdditionalParameterMuscle} 战技 {equipment.AdditionalParameterEnergy}";
                Console.WriteLine(m);
                log(m);
                switch (EquipmentTrainingTargetType)
                {
                    case "Health" when equipment.AdditionalParameterHealth >= EquipmentTrainingTargetValue:
                        return;
                    case "Energy" when equipment.AdditionalParameterEnergy >= EquipmentTrainingTargetValue:
                        return;
                    case "Intelligence" when equipment.AdditionalParameterIntelligence >= EquipmentTrainingTargetValue:
                        return;
                    case "Muscle" when equipment.AdditionalParameterMuscle >= EquipmentTrainingTargetValue:
                        return;
                }

                var response = await GetResponse<TrainingRequest, TrainingResponse>(new TrainingRequest() {EquipmentGuid = EquipmentId, ParameterLockedList = new List<BaseParameterType>()});
                totalCount++;
                var t = 1;
                await Task.Delay(t);
            }
        });
    }

    public async Task GetMissionInfo()
    {
        var response = await GetResponse<GetMissionInfoRequest, GetMissionInfoResponse>(new GetMissionInfoRequest()
            {TargetMissionGroupList = new List<MissionGroupType>() {MissionGroupType.Daily, MissionGroupType.Weekly, MissionGroupType.Main}});
        MissionInfoDict = response.MissionInfoDict;
    }

    public async Task ReinforcementEquipmentOneTime()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            var equipmentDtoInfo = UserSyncData.UserEquipmentDtoInfos.OrderBy(d=>d.ReinforcementLv)
                .FirstOrDefault(d=>!string.IsNullOrEmpty(d.CharacterGuid) && Masters.EquipmentTable.GetById(d.EquipmentId).EquipmentLv > d.ReinforcementLv);
            if (equipmentDtoInfo != null)
            {
                var response = await GetResponse<ReinforcementRequest, ReinforcementResponse>(new ReinforcementRequest(){EquipmentGuid = equipmentDtoInfo.Guid, NumberOfTimes = 1});
                log($"强化一次完成");
            }
        });
    }

    public async Task CompleteMissions()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            await GetMissionInfo();
            var missionIds = MissionInfoDict.Values.SelectMany(d=>
                d.UserMissionDtoInfoDict.Values.SelectMany(x=>
                    x.SelectMany(f=>
                        f.MissionStatusHistory[MissionStatusType.NotReceived]))).ToList();
            var rewardMissionResponse = await GetResponse<RewardMissionRequest, RewardMissionResponse>(new RewardMissionRequest(){TargetMissionIdList = missionIds});
            rewardMissionResponse.RewardInfo.ItemList.PrintUserItems(log);
            rewardMissionResponse.RewardInfo.CharacterList.PrintCharacterDtos(log);
        });
    }

    public async Task RewardMissonActivity()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            await GetMissionInfo();
            foreach (var pair in MissionInfoDict)
            {
                if (pair.Value.UserMissionActivityDtoInfo == null)
                {
                    continue;
                }

                foreach (var (rewardId, statusType) in pair.Value.UserMissionActivityDtoInfo.RewardStatusDict)
                {
                    if (statusType == MissionActivityRewardStatusType.NotReceived)
                    {
                        var rewardMb = Masters.TotalActivityMedalRewardTable.GetById(rewardId);
                        log($"领取 {pair.Key} 的 {rewardMb.RequiredActivityMedalCount} 奖励");
                        var response = await GetResponse<RewardMissionActivityRequest, RewardMissionActivityResponse>(
                            new RewardMissionActivityRequest(){MissionGroupType = pair.Key, RequiredCount = rewardMb.RequiredActivityMedalCount});
                        response.RewardInfo.ItemList.PrintUserItems(log);
                        response.RewardInfo.CharacterList.PrintCharacterDtos(log);
                    }
                }
                
            }
            
        });
    }
}