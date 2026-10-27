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

    private static readonly MissionGroupType[] ExtraGroups =
    {
        MissionGroupType.Limited, MissionGroupType.Collab, MissionGroupType.Comeback, MissionGroupType.Beginner, MissionGroupType.NewCharacter,
        MissionGroupType.Panel, MissionGroupType.GuildTower, MissionGroupType.PopularityVote, MissionGroupType.BookSort, MissionGroupType.RentalRaid,
    };

    public async Task ExecuteAsync(AccountContext context)
    {
        var userId = context.AccountInfo.UserId;
        var nm = context.NetworkManager;

        await _jobLogger.LogAsync(userId, Text.T("MissionChecking"));

        // 1. 获取任务信息
        var missionGroupTypes = new List<MissionGroupType> { MissionGroupType.Daily, MissionGroupType.Weekly, MissionGroupType.Main };
        
        // 检查公会任务
        var guildIdResp = await nm.SendRequest<GetGuildIdRequest, GetGuildIdResponse>(new GetGuildIdRequest());
        if (guildIdResp.GuildId > 0)
        {
            missionGroupTypes.Add(MissionGroupType.Guild);
        }

        var missionInfoResp = await nm.SendRequest<GetMissionInfoRequest, GetMissionInfoResponse>(
            new GetMissionInfoRequest { TargetMissionGroupList = missionGroupTypes });

        if (missionInfoResp.MissionInfoDict == null) return;

        // 活动/联动/回归/新角色等任务: 一组一组请求, 没开的活动报错就跳过, 不影响上面的主线/每日/每周
        foreach (var extra in ExtraGroups)
        {
            try
            {
                var extraResp = await nm.SendRequest<GetMissionInfoRequest, GetMissionInfoResponse>(
                    new GetMissionInfoRequest { TargetMissionGroupList = new List<MissionGroupType> { extra } });
                foreach (var (groupType, info) in extraResp.MissionInfoDict ?? new())
                    missionInfoResp.MissionInfoDict[groupType] = info;
            }
            catch (NetworkManager.ApiErrorException) { }
        }

        // 2. 领取普通任务奖励
        var allMissionIds = new List<long>();
        foreach (var group in missionInfoResp.MissionInfoDict.Values)
        {
            if (group.UserMissionDtoInfoDict == null) continue;
            foreach (var missionList in group.UserMissionDtoInfoDict.Values)
            {
                foreach (var mission in missionList)
                {
                    if (mission.MissionStatusHistory != null && 
                        mission.MissionStatusHistory.TryGetValue(MissionStatusType.NotReceived, out var ids))
                    {
                        allMissionIds.AddRange(ids);
                    }
                }
            }
        }

        if (allMissionIds.Count > 0)
        {
            var rewardResp = await nm.SendRequest<RewardMissionRequest, RewardMissionResponse>(
                new RewardMissionRequest { TargetMissionIdList = allMissionIds });
            
            await _jobLogger.LogAsync(userId, Text.T("MissionRewardsClaimed", allMissionIds.Count));
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
