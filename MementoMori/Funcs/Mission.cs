using MementoMori.Ortega.Share.Data.ApiInterface.Guild;
using MementoMori.Ortega.Share.Data.ApiInterface.Mission;
using MementoMori.Ortega.Share.Data.ApiInterface.Ranking;

namespace MementoMori.Funcs;

public partial class MementoMoriFuncs
{
    public async Task GetMissionInfo()
    {
        var missionGroupTypes = new List<MissionGroupType> {MissionGroupType.Daily, MissionGroupType.Weekly, MissionGroupType.Main};
        var response1 = await GetResponse<GetGuildIdRequest, GetGuildIdResponse>(new GetGuildIdRequest());
        if (response1.GuildId > 0)
        {
            var currentGuildMissionMb = GuildMissionTable.GetCurrentGuildMissionMB(TimeManager.ServerNow);
            if (currentGuildMissionMb != null) missionGroupTypes.Add(MissionGroupType.Guild);

            var guildTowerEventMb = GuildTowerEventTable.GetByInTime(TimeManager.IsInTime);
            if (guildTowerEventMb != null) missionGroupTypes.Add(MissionGroupType.GuildTower);
        }

        var panelMission = Mypage.MypageInfo.MypageIconInfos.FirstOrDefault(d => d.TransferDetailInfo.TransferSpotType == TransferSpotType.PanelMission);
        if (panelMission != null) missionGroupTypes.Add(MissionGroupType.Panel);
        var response = await GetResponse<GetMissionInfoRequest, GetMissionInfoResponse>(new GetMissionInfoRequest {TargetMissionGroupList = missionGroupTypes});
        MissionInfoDict = response.MissionInfoDict;
    }

    public async Task CompleteMissions()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            await GetMissionInfo();
            var missionIds = new List<long>();
            foreach (var (missionGroupType, missionInfo) in MissionInfoDict)
            {
                if (missionGroupType == MissionGroupType.Panel && missionInfo.UserMissionDtoInfoDict.TryGetValue(MissionType.PanelSheet1, out var info1))
                {
                    var notReceived1 = info1.SelectMany(x => x.MissionStatusHistory[MissionStatusType.NotReceived]).ToList();
                    missionIds.AddRange(notReceived1);
                    await RewardMission();

                    var unfinishedIds1 = info1
                        .SelectMany(d => d.MissionStatusHistory)
                        .Where(d => d.Key == MissionStatusType.Locked || d.Key == MissionStatusType.Progress)
                        .SelectMany(d => d.Value).ToList();
                    // 检查是否所有任务都已经完成
                    if (unfinishedIds1.Count == 0 && missionInfo.UserMissionDtoInfoDict.TryGetValue(MissionType.PanelSheet2, out var info2))
                    {
                        var notReceived2 = info2.SelectMany(x => x.MissionStatusHistory[MissionStatusType.NotReceived]).ToList();
                        missionIds.AddRange(notReceived2);
                        await RewardMission();

                        var unfinishedIds2 = info2
                            .SelectMany(d => d.MissionStatusHistory)
                            .Where(d => d.Key == MissionStatusType.Locked || d.Key == MissionStatusType.Progress)
                            .SelectMany(d => d.Value).ToList();

                        if (unfinishedIds2.Count == 0 && missionInfo.UserMissionDtoInfoDict.TryGetValue(MissionType.PanelSheet3, out var info3))
                        {
                            var notReceived3 = info3.SelectMany(x => x.MissionStatusHistory[MissionStatusType.NotReceived]).ToList();
                            missionIds.AddRange(notReceived3);
                            await RewardMission();
                        }
                    }
                }
                else
                {
                    var notReceived = missionInfo.UserMissionDtoInfoDict.Values.SelectMany(d => d.SelectMany(x => x.GetNotReceivedIdList()));
                    missionIds.AddRange(notReceived);
                    await RewardMission();
                }
            }


            async Task RewardMission()
            {
                if (missionIds.Count == 0) return;

                var rewardMissionResponse = await GetResponse<RewardMissionRequest, RewardMissionResponse>(new RewardMissionRequest {TargetMissionIdList = missionIds});
                rewardMissionResponse.RewardInfo.ItemList.PrintUserItems(log);
                rewardMissionResponse.RewardInfo.CharacterList.PrintCharacterDtos(log);
                missionIds.Clear();
            }
        });
    }

    public async Task RewardMissonActivity()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            await GetMissionInfo();
            foreach (var pair in MissionInfoDict)
            {
                if (pair.Value.UserMissionActivityDtoInfo == null) continue;

                foreach (var (rewardId, statusType) in pair.Value.UserMissionActivityDtoInfo.RewardStatusDict)
                {
                    if (statusType == MissionActivityRewardStatusType.NotReceived)
                    {
                        var rewardMb = TotalActivityMedalRewardTable.GetById(rewardId);
                        log(string.Format(ResourceStrings.RewardMissionMsg, pair.Key, rewardMb.RequiredActivityMedalCount));
                        var response = await GetResponse<RewardMissionActivityRequest, RewardMissionActivityResponse>(new RewardMissionActivityRequest
                            {MissionGroupType = pair.Key, RequiredCount = rewardMb.RequiredActivityMedalCount});
                        response.RewardInfo.ItemList.PrintUserItems(log);
                        response.RewardInfo.CharacterList.PrintCharacterDtos(log);
                    }
                }
            }
        });
    }

    public async Task ReceiveAchievementReward()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            await GetResponse<GetPlayerRankingRequest, GetPlayerRankingResponse>(new GetPlayerRankingRequest());
            foreach (var (rankingDataType, mbId) in UserSyncData.ReceivableAchieveRankingRewardIdMap)
            {
                foreach (var mb in AchieveRankingRewardTable.GetByRankingDataType(rankingDataType))
                {
                    if (mb.Id > mbId || UserSyncData.ReceivedAchieveRankingRewardIdList.Contains(mb.Id)) continue;

                    var response = await GetResponse<ReceiveAchieveRankingRewardRequest, ReceiveAchieveRankingRewardResponse>(new ReceiveAchieveRankingRewardRequest
                        {AchieveRankingRewardMBId = mb.Id});
                    log($"{TextResourceTable.Get(mb.AchieveTargetDescriptionKey)}");
                    response.RewardItemList.PrintUserItems(log);
                }
            }
        });
    }
}