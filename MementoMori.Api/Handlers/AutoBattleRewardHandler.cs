using MementoMori.Api.Infrastructure;
using MementoMori.Api.Services;
using MementoMori.Ortega.Share.Data.ApiInterface.Battle;
using MementoMori.Ortega.Share.Data.ApiInterface.Quest;

namespace MementoMori.Api.Handlers;

/// <summary>
/// 自动战斗奖励领取处理器（挂机收益）
/// </summary>
[RegisterTransient]
[AutoConstructor]
public partial class AutoBattleRewardHandler : IGameActionHandler
{
    private readonly ILogger<AutoBattleRewardHandler> _logger;
    private readonly JobLogger _jobLogger;

    public string ActionName => Text.T("ActionAutoBattleReward");

    public async Task ExecuteAsync(AccountContext context)
    {
        var userId = context.AccountInfo.UserId;
        var nm = context.NetworkManager;

        await _jobLogger.LogAsync(userId, Text.T("JobClaimingAutoBattleRewards"));

        try
        {
            // 1. 获取地图信息
            await nm.SendRequest<MapInfoRequest, MapInfoResponse>(
                new MapInfoRequest { IsUpdateOtherPlayerInfo = true });

            // 2. 执行自动战斗请求
            var autoResponse = await nm.SendRequest<AutoRequest, AutoResponse>(
                new AutoRequest());

            // 3. 领取奖励
            var bonus = await nm.SendRequest<RewardAutoBattleRequest, RewardAutoBattleResponse>(
                new RewardAutoBattleRequest());

            var result = bonus.AutoBattleRewardResult;
            
            await _jobLogger.LogAsync(userId, 
                Text.T("JobAutoBattleRewardSummary", result.BattleCountWin, result.BattleCountAll - result.BattleCountWin, TimeSpan.FromMilliseconds(result.BattleTotalTime)));

            // 记录收益
            if (result.GoldByPopulation > 0)
            {
                await _jobLogger.LogAsync(userId, Text.T("JobGoldIncome", result.GoldByPopulation));
            }
            if (result.PotentialJewelByPopulation > 0)
            {
                await _jobLogger.LogAsync(userId, Text.T("JobPotentialJewelIncome", result.PotentialJewelByPopulation));
            }
            if (result.BattleRewardResult?.FixedItemList?.Count > 0)
            {
                var items = string.Join(", ", result.BattleRewardResult.FixedItemList.Select(i => $"{i.ItemType}:{i.ItemId} x{i.ItemCount}"));
                await _jobLogger.LogAsync(userId, Text.T("JobFixedDrops", items));
            }
            if (result.BattleRewardResult?.DropItemList?.Count > 0)
            {
                var items = string.Join(", ", result.BattleRewardResult.DropItemList.Select(i => $"{i.ItemType}:{i.ItemId} x{i.ItemCount}"));
                await _jobLogger.LogAsync(userId, Text.T("JobRandomDrops", items));
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in AutoBattleRewardHandler for user {UserId}", userId);
            await _jobLogger.LogAsync(userId, Text.T("JobAutoBattleRewardFailed", ex.Message), "Error");
            throw;
        }
    }
}
