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

    public string ActionName => "领取自动战斗奖励";

    public async Task ExecuteAsync(AccountContext context)
    {
        var userId = context.AccountInfo.UserId;
        var nm = context.NetworkManager;

        await _jobLogger.LogAsync(userId, "正在领取自动战斗奖励...");

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
                $"自动战斗奖励领取完成 - 胜利: {result.BattleCountWin}, 失败: {result.BattleCountAll - result.BattleCountWin}, " +
                $"总战斗时间: {TimeSpan.FromMilliseconds(result.BattleTotalTime)}");

            // 记录收益
            if (result.GoldByPopulation > 0)
            {
                await _jobLogger.LogAsync(userId, $"金币收益: {result.GoldByPopulation}");
            }
            if (result.PotentialJewelByPopulation > 0)
            {
                await _jobLogger.LogAsync(userId, $"潜能宝石收益: {result.PotentialJewelByPopulation}");
            }
            if (result.BattleRewardResult?.FixedItemList?.Count > 0)
            {
                var items = string.Join(", ", result.BattleRewardResult.FixedItemList.Select(i => $"{i.ItemType}:{i.ItemId} x{i.ItemCount}"));
                await _jobLogger.LogAsync(userId, $"固定掉落: {items}");
            }
            if (result.BattleRewardResult?.DropItemList?.Count > 0)
            {
                var items = string.Join(", ", result.BattleRewardResult.DropItemList.Select(i => $"{i.ItemType}:{i.ItemId} x{i.ItemCount}"));
                await _jobLogger.LogAsync(userId, $"随机掉落: {items}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in AutoBattleRewardHandler for user {UserId}", userId);
            await _jobLogger.LogAsync(userId, $"领取自动战斗奖励失败: {ex.Message}", "Error");
            throw;
        }
    }
}
