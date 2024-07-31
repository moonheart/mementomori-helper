using MementoMori.Exceptions;
using MementoMori.Ortega.Common.Utils;
using MementoMori.Ortega.Custom;
using MementoMori.Ortega.Share.Data.ApiInterface.Guild;
using MementoMori.Ortega.Share.Data.ApiInterface.GuildTower;
using MementoMori.Ortega.Share.Data.Item;

namespace MementoMori.Funcs;

public partial class MementoMoriFuncs
{
    public async Task AutoGuildTower()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            var guildTowerOption = PlayerOption.GuildTower;

            var guildTowerEventMb = GuildTowerEventTable.GetByInTime(TimeManager.IsInTime);
            if (guildTowerEventMb == null)
            {
                log(TextResourceTable.Get("[GuildTowerEndEventDialogMessage]"));
                return;
            }

            // get guild tower info
            var guildId = await GetResponse<GetGuildIdRequest, GetGuildIdResponse>(new GetGuildIdRequest());
            if (guildId.GuildId == 0)
            {
                log(TextResourceTable.Get("[RankingNotGuild]"));
                return;
            }

            var guildTowerInfo = await GetResponse<GetGuildTowerInfoRequest, GetGuildTowerInfoResponse>(new GetGuildTowerInfoRequest());
            if (!guildTowerInfo.IsAlreadyEntryToday)
            {
                if (!guildTowerOption.AutoEntry) return;

                var guids = UserSyncData.UserCharacterDtoInfos.OrderByDescending(d => BattlePowerCalculatorUtil.GetUserCharacterBattlePower(UserId, d.Guid)).Take(20).Select(d => d.Guid).ToList();
                var entryCharacterResponse =
                    await GetResponse<EntryCharacterRequest, EntryCharacterResponse>(new EntryCharacterRequest
                        {CharacterGuidList = guids, GuildTowerEntryType = GuildTowerEntryType.Entry, IsContinueEntry = false});
                log(TextResourceTable.Get("[GuildTowerEntryToastMessage]"));
                guildTowerInfo = await GetResponse<GetGuildTowerInfoRequest, GetGuildTowerInfoResponse>(new GetGuildTowerInfoRequest());
            }

            // chanllenge guild tower for each job
            if (guildTowerOption.AutoChallenge)
            {
                while (guildTowerInfo.TodayWinCount < 3)
                {
                    var nextFloor = GuildTowerFloorTable.GetById(guildTowerInfo.CurrentFloorMBId);
                    // var nextFloor = GuildTowerFloorTable.GetByEventIdAndFloor(guildTowerFloorMb.EventNo, guildTowerFloorMb.FloorCount + 1);
                    if (nextFloor == null) break;

                    // get the least used job
                    var jobFlag = guildTowerInfo.GuildTowerEntryCharacterList.Select(d => new {tc = d, cha = UserSyncData.GetUserCharacterInfoByUserCharacterGuid(d.CharacterGuid)})
                        .GroupBy(d => CharacterTable.GetById(d.cha.CharacterId).JobFlags)
                        .OrderBy(d => d // order by used count ascending
                                .Select(x => new {cha = x, bp = BattlePowerCalculatorUtil.GetUserCharacterBattlePower(UserId, x.cha)})
                                .OrderByDescending(x => x.bp) // get the most powerful character
                                .Take(5) // get the top 5
                                .Sum(x => x.cha.tc.TodayUseCount) // get used count
                        ).First().Key;

                    var isWin = false;
                    var isFloorClearedByOther = false;
                    // from max difficulty to min difficulty
                    for (var i = nextFloor.SelectableDifficultyList.Count - 1; i >= 0; i--)
                    {
                        var guids = guildTowerInfo.GuildTowerEntryCharacterList.Select(d => UserSyncData.GetUserCharacterInfoByUserCharacterGuid(d.CharacterGuid))
                            .Where(d => CharacterTable.GetById(d.CharacterId).JobFlags == jobFlag)
                            .OrderByDescending(d => BattlePowerCalculatorUtil.GetUserCharacterBattlePower(UserId, d))
                            .Take(5)
                            .Select(d => d.Guid);

                        var battleRequest = new BattleRequest
                        {
                            CharacterGuidList = guids.ToList(), Difficulty = nextFloor.SelectableDifficultyList[i], GuildTowerFloor = nextFloor.FloorCount
                        };

                        var retryCount = Math.Max(1, guildTowerOption.AutoChallengeRetryCount);
                        retryCount = Math.Min(retryCount, 1000);
                        for (var j = 0; j < retryCount; j++)
                        {
                            try
                            {
                                var battleResponse = await GetResponse<BattleRequest, BattleResponse>(battleRequest);
                                var msg =
                                    $"{TextResourceTable.Get("[GuildTowerStageFormat]", nextFloor.FloorCount)} {TextResourceTable.Get("[GuildTowerDifficultyLabel]")} {nextFloor.SelectableDifficultyList[i]}";
                                if (battleResponse.BattleResult.SimulationResult.BattleEndInfo.IsWinAttacker())
                                {
                                    isWin = true;
                                    log($"{TextResourceTable.Get("[GuildTowerTitle]")} {msg} {TextResourceTable.Get("[LocalRaidBattleWinMessage]")}");
                                    battleResponse.BattleRewardResult.DropItemList.PrintUserItems(log);
                                    battleResponse.BattleRewardResult.FixedItemList.PrintUserItems(log);
                                    break;
                                }

                                log($"{TextResourceTable.Get("[GuildTowerTitle]")} {msg} {TextResourceTable.Get("[LocalRaidBattleLoseMessage]")}");
                            }
                            catch (ApiErrorException e) when (e.ErrorCode == ErrorCode.GuildTowerAlreadyClearFloor)
                            {
                                isFloorClearedByOther = true;
                                break;
                            }
                        }

                        if (isWin || isFloorClearedByOther) break;
                    }

                    guildTowerInfo = await GetResponse<GetGuildTowerInfoRequest, GetGuildTowerInfoResponse>(new GetGuildTowerInfoRequest());
                }
            }

            // job reinforcement
            if (guildTowerOption.AutoReinforcement)
            {
                var getReinforcementJobDataResponse = await GetResponse<GetReinforcementJobDataRequest, GetReinforcementJobDataResponse>(new GetReinforcementJobDataRequest
                {
                    JobLevelMap = guildTowerInfo.ReinforcementJobDataList.ToDictionary(d => d.JobFlags, d => d.Level)
                });
                foreach (var guildTowerReinforcementJobData in getReinforcementJobDataResponse.ReinforcementJobDataList)
                {
                    var reinforcementJobData = guildTowerReinforcementJobData;
                    var guildTowerReinforcementJobLevelMb =
                        GuildTowerReinforcementJobLevelTable.GetByEventNoJobFlagsLevel(guildTowerEventMb.EventNo, reinforcementJobData.JobFlags, reinforcementJobData.Level);
                    var userItem = guildTowerReinforcementJobLevelMb.RequiredMaterialList.OrderByDescending(d => d.ItemCount).First();
                    long count;
                    while ((count = UserSyncData.GetUserItemCount(userItem.ItemType, userItem.ItemId)) > 0)
                    {
                        var consumedMaterialCount = reinforcementJobData.GetConsumedMaterialCount(userItem.ItemType, userItem.ItemId);
                        var toConsumeCount = Math.Min(userItem.ItemCount - consumedMaterialCount, count);
                        if (toConsumeCount == 0) break;

                        log($"{TextResourceTable.Get("[GuildTowerJobReinforceLabel]")} {TextResourceTable.Get(reinforcementJobData.JobFlags)}");
                        var reinforceJobResponse = await GetResponse<ReinforceJobRequest, ReinforceJobResponse>(new ReinforceJobRequest
                        {
                            JobFlags = reinforcementJobData.JobFlags,
                            Level = reinforcementJobData.Level,
                            MaterialItemList = [new UserItem {ItemType = userItem.ItemType, ItemId = userItem.ItemId, ItemCount = toConsumeCount}]
                        });
                        reinforcementJobData = reinforceJobResponse.ReinforcementJobData;
                    }
                }
            }

            // receive floor reward
            if (guildTowerOption.AutoReceiveReward)
            {
                guildTowerInfo = await GetResponse<GetGuildTowerInfoRequest, GetGuildTowerInfoResponse>(new GetGuildTowerInfoRequest());
                var notRewardedIds = GuildTowerFloorTable.GetListByEventId(guildTowerEventMb.EventNo).Where(d =>
                        d.Id < guildTowerInfo.CurrentFloorMBId &&
                        d.FloorRewardList != null &&
                        d.FloorRewardList.Count > 0 &&
                        !UserSyncData.ReceivedGuildTowerFloorRewardIdList.Contains(d.Id))
                    .Select(d => d.Id).ToList();
                if (notRewardedIds.Count > 0)
                {
                    log($"{TextResourceTable.Get("[GuildTowerTitle]")} {TextResourceTable.Get("[GuildTowerStageRewardLabel]")}");
                    var receiveFloorRewardResponse = await GetResponse<ReceiveFloorRewardRequest, ReceiveFloorRewardResponse>(new ReceiveFloorRewardRequest {FloorRewardMBIdList = notRewardedIds});
                    receiveFloorRewardResponse.RewardItemList.PrintUserItems(log);
                }
            }
        });
    }
}