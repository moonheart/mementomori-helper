using MementoMori.Api.Infrastructure;
using MementoMori.Api.Services;
using MementoMori.Ortega.Share.Data.ApiInterface.BountyQuest;

namespace MementoMori.Api.Handlers;

/// <summary>
/// 悬赏任务奖励领取处理器
/// </summary>
[RegisterTransient]
[AutoConstructor]
public partial class BountyQuestRewardHandler : IGameActionHandler
{
    private readonly ILogger<BountyQuestRewardHandler> _logger;
    private readonly JobLogger _jobLogger;

    public string ActionName => Text.T("ActionBountyQuestReward");

    public async Task ExecuteAsync(AccountContext context)
    {
        var userId = context.AccountInfo.UserId;
        var nm = context.NetworkManager;
        var timeManager = context.TimeManager;

        // 检查悬赏任务功能是否已解锁
        var userSyncData = nm.UserSyncData;
        var openContentMb = OpenContentTable.GetByOpenCommandType(OpenCommandType.BountyQuest);
        var isAvailable = userSyncData?.UserBattleBossDtoInfo?.BossClearMaxQuestId >= openContentMb.OpenContentValue;
        
        if (!isAvailable)
        {
            return;
        }

        await _jobLogger.LogAsync(userId, Text.T("BountyRewardStarting"));

        // 获取悬赏任务列表
        var getListResponse = await nm.SendRequest<GetListRequest, GetListResponse>(
            new GetListRequest());

        // 找出已完成且未领取奖励的任务
        var serverNow = timeManager.ServerNow;
        var currentTimestamp = new DateTimeOffset(serverNow).ToUnixTimeMilliseconds();

        var questIds = getListResponse.UserBountyQuestDtoInfos
            .Where(d => d.BountyQuestEndTime > 0 && !d.IsReward && currentTimestamp > d.BountyQuestEndTime)
            .Select(d => d.BountyQuestId)
            .ToList();

        if (questIds.Count > 0)
        {
            // 领取奖励
            var rewardResponse = await nm.SendRequest<RewardRequest, RewardResponse>(
                new RewardRequest 
                { 
                    BountyQuestIds = questIds, 
                    ConsumeCurrency = 0, 
                    IsQuick = false 
                });

            // 记录奖励
            if (rewardResponse.RewardItems?.Count > 0)
            {
                foreach (var item in rewardResponse.RewardItems)
                {
                    await _jobLogger.LogAsync(userId, Text.T("BountyRewardItem", item.ItemType, item.ItemCount));
                }
            }

            // 刷新任务列表
            await nm.SendRequest<GetListRequest, GetListResponse>(new GetListRequest());
            
            await _jobLogger.LogAsync(userId, Text.T("BountyRewardsClaimed", questIds.Count));
        }
        else
        {
            await _jobLogger.LogAsync(userId, Text.T("BountyNoRewards"));
        }

        // 最后获取一次任务列表更新状态
        await nm.SendRequest<GetListRequest, GetListResponse>(new GetListRequest());
    }
}
