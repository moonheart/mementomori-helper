using MementoMori.Exceptions;
using MementoMori.Ortega.Share.Data.ApiInterface.Battle;
using MementoMori.Ortega.Share.Data.ApiInterface.Quest;

namespace MementoMori.Funcs;

public partial class MementoMoriFuncs
{
    private bool IsBossBattleQuickAvailable
    {
        get
        {
            var vip = VipTable.GetByLevel(UserSyncData.UserStatusDtoInfo.Vip);
            var isQuickAvailable = vip.IsQuickBossBattleAvailable;
            if (!isQuickAvailable) isQuickAvailable = UserSyncData.UserBattleBossDtoInfo.BossClearMaxQuestId >= OpenContentTable.GetByOpenCommandType(OpenCommandType.BossBattleQuick).OpenContentValue;

            return isQuickAvailable;
        }
    }

    public async Task GetAutoBattleReward()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            var mapInfoResponse = await GetResponse<MapInfoRequest, MapInfoResponse>(new MapInfoRequest {IsUpdateOtherPlayerInfo = true});
            var autoResponse = await GetResponse<AutoRequest, AutoResponse>(new AutoRequest());
            var bonus = await GetResponse<RewardAutoBattleRequest, RewardAutoBattleResponse>(new RewardAutoBattleRequest());
            log(TextResourceTable.Get("[AutoBattleRewardInfoTitle]"));
            log($"{TextResourceTable.Get("[CommonWinLabel]")} {bonus.AutoBattleRewardResult.BattleCountWin}");
            log($"{TextResourceTable.Get("[CommonLoseLabel]")} {bonus.AutoBattleRewardResult.BattleCountAll - bonus.AutoBattleRewardResult.BattleCountWin}");
            log($"{TextResourceTable.Get("[AutoBattleRewardInfoBattleTotalTimeLabel]")} {TimeSpan.FromMilliseconds(bonus.AutoBattleRewardResult.BattleTotalTime)}");
            log(TextResourceTable.Get("[AutoBattleRewardInfoPopulationLabel]"));
            log($"{TextResourceTable.Get("[ItemName5]")} {bonus.AutoBattleRewardResult.GoldByPopulation}");
            log($"{TextResourceTable.Get("[ItemName11]")} {bonus.AutoBattleRewardResult.PotentialJewelByPopulation}");
            log(TextResourceTable.Get("[AutoBattleRewardInfoRewardLabel]"));
            bonus.AutoBattleRewardResult.BattleRewardResult.FixedItemList.PrintUserItems(log);
            bonus.AutoBattleRewardResult.BattleRewardResult.DropItemList.PrintUserItems(log);
        });
    }

    public async Task BattleBossQuick()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            try
            {
                if (IsBossBattleQuickAvailable)
                {
                    var bossQuickResponse = await GetResponse<BossQuickRequest, BossQuickResponse>(
                        new BossQuickRequest
                        {
                            QuestId = UserSyncData.UserBattleBossDtoInfo.BossClearMaxQuestId,
                            QuickCount = 3
                        });
                    if (bossQuickResponse.BattleRewardResult == null) return;

                    log($"{TextResourceTable.Get("[AutoBattleButtonQuickForward]")}:\n");
                    bossQuickResponse.BattleRewardResult.FixedItemList.PrintUserItems(log);
                    bossQuickResponse.BattleRewardResult.DropItemList.PrintUserItems(log);
                }
                else
                {
                    for (var i = 0; i < 3; i++)
                    {
                        var bossQuickResponse = await GetResponse<BossRequest, BossResponse>(
                            new BossRequest
                            {
                                QuestId = UserSyncData.UserBattleBossDtoInfo.BossClearMaxQuestId
                            });
                        if (bossQuickResponse.BattleRewardResult == null) return;

                        log($"{TextResourceTable.Get("[AutoBattleButtonQuickForward]")}:\n");
                        bossQuickResponse.BattleRewardResult.FixedItemList.PrintUserItems(log);
                        bossQuickResponse.BattleRewardResult.DropItemList.PrintUserItems(log);
                    }
                }
            }
            catch (ApiErrorException e) when (e.ErrorCode == ErrorCode.BattleBossNotEnoughBossChallengeCount)
            {
                log(e.Message);
            }
        });
    }

    public async Task BossHishSpeedBattle()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            var autoResponse = await GetResponse<AutoRequest, AutoResponse>(new AutoRequest());
            if (await IsValidMonthlyBoost())
            {
                var availableCount = OrtegaConst.Shop.MonthlyBoostBattleQuickBonus - autoResponse.UserBattleAuto.QuickTodayUsePrivilegeCount;
                if (availableCount > 0)
                {
                    log(TextResourceTable.Get("[MonthlyBoostPrivilegeDescription3]"));
                    await BattleQuick(log, QuestQuickExecuteType.Privilege, (int) availableCount);
                }
            }

            // 每天有一次免费
            if (autoResponse.UserBattleAuto.QuickTodayUseCurrencyCount >= 1)
                log(TextResourceTable.GetErrorCodeMessage(ErrorCode.BattleAutoNotEnoughPrivilegeCount));
            else
            {
                log($"{TextResourceTable.Get("[AutoBattleButtonQuickForward]")}{TextResourceTable.Get("[CommonRewardLabel]")}:\n");
                await BattleQuick(log, QuestQuickExecuteType.Currency, 1);
            }
        });

        async Task BattleQuick(Action<string> log, QuestQuickExecuteType type, int count)
        {
            var req = new QuickRequest {QuestQuickExecuteType = type, QuickCount = count};
            var quickResponse = await GetResponse<QuickRequest, QuickResponse>(req);

            log($"{TextResourceTable.Get("[AutoBattleRewardInfoPopulationLabel]")} {TextResourceTable.Get("[ItemName5]")} {quickResponse.AutoBattleRewardResult.GoldByPopulation}");
            log($"{TextResourceTable.Get("[AutoBattleRewardInfoPopulationLabel]")} {TextResourceTable.Get("[ItemName11]")} {quickResponse.AutoBattleRewardResult.PotentialJewelByPopulation}");
            log($"{TextResourceTable.Get("[ItemName6]")} {quickResponse.AutoBattleRewardResult.BattleRewardResult.CharacterExp}");
            log($"{TextResourceTable.Get("[ItemName5]")} {quickResponse.AutoBattleRewardResult.BattleRewardResult.ExtraGold}");
            log($"{TextResourceTable.Get("[ItemName11]")} {quickResponse.AutoBattleRewardResult.BattleRewardResult.PlayerExp}");
            log($"{TextResourceTable.Get("[CharacterLevelUpLabel]")} {quickResponse.AutoBattleRewardResult.BattleRewardResult.RankUp}");

            quickResponse.AutoBattleRewardResult.BattleRewardResult.FixedItemList.PrintUserItems(log);
            quickResponse.AutoBattleRewardResult.BattleRewardResult.DropItemList.PrintUserItems(log);
        }
    }

    public async Task AutoBossRequest(long selectedTargetQuerstId = 0)
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            var totalCount = 0;
            var winCount = 0;
            var errCount = 0;

            await GetResponse<MapInfoRequest, MapInfoResponse>(new MapInfoRequest() {IsUpdateOtherPlayerInfo = true});

            try
            {
                await GetResponse<NextQuestRequest, NextQuestResponse>(new NextQuestRequest());
            }
            catch (ApiErrorException e) when (e.ErrorCode == ErrorCode.BattleAutoNextQuestNotFound)
            {
            }

            while (!token.IsCancellationRequested)
            {
                try
                {
                    var targetQuestId = UserSyncData.UserBattleBossDtoInfo.BossClearMaxQuestId + 1;
                    
                    await GetResponse<GetQuestInfoRequest, GetQuestInfoResponse>(new GetQuestInfoRequest() { TargetQuestId = targetQuestId});
                    var bossResponse = await GetResponse<BossRequest, BossResponse>(new BossRequest {QuestId = targetQuestId});
                    var win = bossResponse.BattleResult.SimulationResult.BattleEndInfo.IsWinAttacker();
                    totalCount++;
                    if (win) winCount++;
                    var info = QuestTable.GetById(targetQuestId).Memo;
                    var result = win ? TextResourceTable.Get("[LocalRaidBattleWinMessage]") : TextResourceTable.Get("[LocalRaidBattleLoseMessage]");
                    log(string.Format(ResourceStrings.AutoBossExecMessage, info, result, totalCount, winCount, errCount));

                    await _battleLogManager.SaveBattleLog(bossResponse.BattleResult, "main", bossResponse.BattleResult.QuestId.ToString(), "main-*lose");

                    if (win)
                    {
                        await GetResponse<MapInfoRequest, MapInfoResponse>(new MapInfoRequest() { IsUpdateOtherPlayerInfo = true });
                        if (selectedTargetQuerstId > 0 && selectedTargetQuerstId == targetQuestId) break;
                        var nextQuestResponse = await GetResponse<NextQuestRequest, NextQuestResponse>(new NextQuestRequest());
                    }
                }
                catch (Exception e)
                {
                    log(e.Message);
                    errCount++;
                    if (errCount > Max_Err_Count)
                    {
                        log(string.Format(ResourceStrings.AutoBossErrorMessage, Max_Err_Count));
                        return;
                    }

                    if (e is ApiErrorException) await AuthLogin(_lastPlayerDataInfo);
                }
            }
        });
    }
}