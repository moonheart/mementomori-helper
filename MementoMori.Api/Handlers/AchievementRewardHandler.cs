using MementoMori.Api.Infrastructure;
using MementoMori.Api.Services;
using MementoMori.Ortega.Share;
using MementoMori.Ortega.Share.Data.ApiInterface.Ranking;

namespace MementoMori.Api.Handlers;

/// <summary>
/// 成就奖励领取处理器
/// </summary>
[RegisterTransient]
[AutoConstructor]
public partial class AchievementRewardHandler : IGameActionHandler
{
    private readonly ILogger<AchievementRewardHandler> _logger;
    private readonly JobLogger _jobLogger;

    public string ActionName => "领取成就奖励";

    public async Task ExecuteAsync(AccountContext context)
    {
        var userId = context.AccountInfo.UserId;
        var nm = context.NetworkManager;

        await _jobLogger.LogAsync(userId, "正在检查成就奖励...");

        try
        {
            await nm.SendRequest<GetPlayerRankingRequest, GetPlayerRankingResponse>(new GetPlayerRankingRequest());

            var claimedCount = 0;
            if (nm.UserSyncData.ReceivableAchieveRankingRewardIdMap != null)
            {
                foreach (var (rankingDataType, mbId) in nm.UserSyncData.ReceivableAchieveRankingRewardIdMap)
                {
                    foreach (var mb in Masters.AchieveRankingRewardTable.GetByRankingDataType(rankingDataType))
                    {
                        if (mb.Id > mbId || nm.UserSyncData.ReceivedAchieveRankingRewardIdList.Contains(mb.Id))
                            continue;

                        try
                        {
                            var response = await nm.SendRequest<ReceiveAchieveRankingRewardRequest, ReceiveAchieveRankingRewardResponse>(
                                new ReceiveAchieveRankingRewardRequest { AchieveRankingRewardMBId = mb.Id });
                            claimedCount++;
                        }
                        catch (Exception ex)
                        {
                            _logger.LogWarning(ex, "领取成就奖励 {MbId} 失败 for user {UserId}", mb.Id, userId);
                        }
                    }
                }
            }

            if (claimedCount > 0)
            {
                await _jobLogger.LogAsync(userId, $"成就奖励已领取 {claimedCount} 项。");
            }
            else
            {
                await _jobLogger.LogAsync(userId, "暂无可领取的成就奖励。");
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "检查成就奖励失败 for user {UserId}", userId);
        }
    }
}
