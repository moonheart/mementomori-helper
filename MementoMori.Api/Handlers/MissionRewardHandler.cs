using MementoMori.Api.Extensions;
using MementoMori.Api.Infrastructure;
using MementoMori.Api.Models;
using MementoMori.Api.Services;
using MementoMori.Ortega.Share.Data.ApiInterface.Guild;
using MementoMori.Ortega.Share.Data.ApiInterface.Mission;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Extensions;
using MementoMori.Ortega.Share;

namespace MementoMori.Api.Handlers;

/// <summary>
/// 任务领取（领取奖励与活跃度）处理器
/// </summary>
[RegisterTransient]
[AutoConstructor]
public partial class MissionRewardHandler : IGameActionHandler
{
    private readonly ILogger<MissionRewardHandler> _logger;
    private readonly JobLogger _jobLogger;
    private readonly IServiceProvider _serviceProvider;

    public string ActionName => Text.T("ActionMissionReward");

    // 每组单独请求: 一组没开 (MissionNotOpenMission) 会让整个请求失败, 连活动任务都领不到
    private static readonly MissionGroupType[] Groups =
    {
        MissionGroupType.Daily, MissionGroupType.Weekly, MissionGroupType.Main, MissionGroupType.Guild,
        MissionGroupType.Limited, MissionGroupType.Collab, MissionGroupType.Comeback, MissionGroupType.Beginner, MissionGroupType.NewCharacter,
        MissionGroupType.Panel, MissionGroupType.GuildTower, MissionGroupType.PopularityVote, MissionGroupType.BookSort, MissionGroupType.RentalRaid,
    };

    public async Task ExecuteAsync(AccountContext context)
    {
        var userId = context.AccountInfo.UserId;
        var nm = context.NetworkManager;

        await _jobLogger.LogAsync(userId, Text.T("MissionChecking"));

        // 1. 获取任务信息
        var missionInfoResp = new GetMissionInfoResponse { MissionInfoDict = new() };
        foreach (var group in Groups)
        {
            try
            {
                var resp = await nm.SendRequest<GetMissionInfoRequest, GetMissionInfoResponse>(
                    new GetMissionInfoRequest { TargetMissionGroupList = new List<MissionGroupType> { group } });
                foreach (var (groupType, info) in resp.MissionInfoDict ?? new())
                    missionInfoResp.MissionInfoDict[groupType] = info;
            }
            catch (NetworkManager.ApiErrorException) { }
        }

        // 2. 领取普通任务奖励: 按组领, 面板任务前一张没完成会报 MissionNotClearedPrevSheetMission, 别连累其他组
        foreach (var (groupType, group) in missionInfoResp.MissionInfoDict)
        {
            if (group.UserMissionDtoInfoDict == null) continue;
            var ids = group.UserMissionDtoInfoDict.Values.SelectMany(l => l)
                .SelectMany(m => m.MissionStatusHistory != null && m.MissionStatusHistory.TryGetValue(MissionStatusType.NotReceived, out var v) ? v : new List<long>())
                .ToList();
            if (ids.Count == 0) continue;
            try
            {
                await nm.SendRequest<RewardMissionRequest, RewardMissionResponse>(new RewardMissionRequest { TargetMissionIdList = ids });
                await _jobLogger.LogAsync(userId, Text.T("MissionRewardsClaimed", ids.Count));
            }
            catch (NetworkManager.ApiErrorException e)
            {
                _logger.LogWarning("Mission claim {Group} skipped: {Error}", groupType, e.Message);
            }
        }

        // 3. 领取活跃度奖励 (Activity Rewards)
        foreach (var pair in missionInfoResp.MissionInfoDict)
        {
            var activityInfo = pair.Value.UserMissionActivityDtoInfo;
            if (activityInfo?.RewardStatusDict == null) continue;

            foreach (var reward in activityInfo.RewardStatusDict)
            {
                if (reward.Value == MissionActivityRewardStatusType.NotReceived)
                {
                    var rewardMb = Masters.TotalActivityMedalRewardTable.GetById(reward.Key);
                    if (rewardMb == null) continue;

                    try
                    {
                        await nm.SendRequest<RewardMissionActivityRequest, RewardMissionActivityResponse>(new RewardMissionActivityRequest
                        {
                            MissionGroupType = pair.Key,
                            RequiredCount = rewardMb.RequiredActivityMedalCount
                        });

                        await _jobLogger.LogAsync(userId, Text.T("MissionActivityClaimed", pair.Key, rewardMb.RequiredActivityMedalCount));
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "Failed to reward mission activity for user {UserId}", userId);
                    }
                }
            }
        }

        await _jobLogger.LogAsync(userId, Text.T("MissionCheckDone"));
    }
}
