using MementoMori.Api.Infrastructure;
using MementoMori.Api.Services;
using MementoMori.Ortega.Share;
using MementoMori.Ortega.Share.Data.ApiInterface.Guild;
using MementoMori.Ortega.Share.Data.ApiInterface.Mission;
using MementoMori.Ortega.Share.Enums;

namespace MementoMori.Api.Handlers;

/// <summary>
/// 任务活动奖励领取处理器
/// </summary>
[RegisterTransient]
[AutoConstructor]
public partial class MissionActivityRewardHandler : IGameActionHandler
{
    private readonly ILogger<MissionActivityRewardHandler> _logger;
    private readonly JobLogger _jobLogger;

    public string ActionName => "领取任务活动奖励";

    public async Task ExecuteAsync(AccountContext context)
    {
        var userId = context.AccountInfo.UserId;
        var nm = context.NetworkManager;

        await _jobLogger.LogAsync(userId, "正在检查任务活动奖励...");

        try
        {
            var missionGroupTypes = new List<MissionGroupType> { MissionGroupType.Daily, MissionGroupType.Weekly, MissionGroupType.Main };

            // 检查公会任务
            var guildIdResp = await nm.SendRequest<GetGuildIdRequest, GetGuildIdResponse>(new GetGuildIdRequest(), false);
            if (guildIdResp.GuildId > 0)
            {
                missionGroupTypes.Add(MissionGroupType.Guild);
            }

            var missionInfoResp = await nm.SendRequest<GetMissionInfoRequest, GetMissionInfoResponse>(
                new GetMissionInfoRequest { TargetMissionGroupList = missionGroupTypes }, false);

            if (missionInfoResp.MissionInfoDict == null)
            {
                await _jobLogger.LogAsync(userId, "暂无任务活动信息。");
                return;
            }

            var claimedCount = 0;
            foreach (var pair in missionInfoResp.MissionInfoDict)
            {
                if (pair.Value.UserMissionActivityDtoInfo?.RewardStatusDict == null) continue;

                foreach (var (rewardId, statusType) in pair.Value.UserMissionActivityDtoInfo.RewardStatusDict)
                {
                    if (statusType == MissionActivityRewardStatusType.NotReceived)
                    {
                        var rewardMb = Masters.TotalActivityMedalRewardTable.GetById(rewardId);
                        if (rewardMb == null) continue;

                        try
                        {
                            await nm.SendRequest<RewardMissionActivityRequest, RewardMissionActivityResponse>(
                                new RewardMissionActivityRequest
                                {
                                    MissionGroupType = pair.Key,
                                    RequiredCount = rewardMb.RequiredActivityMedalCount
                                }, false);
                            claimedCount++;
                        }
                        catch (Exception ex)
                        {
                            _logger.LogWarning(ex, "领取任务活动奖励失败 for user {UserId}", userId);
                        }
                    }
                }
            }

            if (claimedCount > 0)
            {
                await _jobLogger.LogAsync(userId, $"任务活动奖励已领取 {claimedCount} 项。");
            }
            else
            {
                await _jobLogger.LogAsync(userId, "暂无可领取的任务活动奖励。");
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "检查任务活动奖励失败 for user {UserId}", userId);
        }
    }
}
