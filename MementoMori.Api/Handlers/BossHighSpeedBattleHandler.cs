using MementoMori.Api.Infrastructure;
using MementoMori.Api.Services;
using MementoMori.Ortega.Share;
using MementoMori.Ortega.Share.Data.ApiInterface.Battle;
using MementoMori.Ortega.Share.Enums;

namespace MementoMori.Api.Handlers;

/// <summary>
/// BOSS高速战斗处理器（使用月卡特权或免费次数）
/// </summary>
[RegisterTransient]
[AutoConstructor]
public partial class BossHighSpeedBattleHandler : IGameActionHandler
{
    private readonly ILogger<BossHighSpeedBattleHandler> _logger;
    private readonly JobLogger _jobLogger;

    public string ActionName => "BOSS高速战斗";

    public async Task ExecuteAsync(AccountContext context)
    {
        var userId = context.AccountInfo.UserId;
        var nm = context.NetworkManager;

        await _jobLogger.LogAsync(userId, "开始执行BOSS高速战斗...");

        try
        {
            // 先执行自动战斗请求获取当前状态
            var autoResponse = await nm.SendRequest<AutoRequest, AutoResponse>(
                new AutoRequest());

            // 检查是否有月卡特权
            if (IsValidMonthlyBoost(nm))
            {
                var availableCount = OrtegaConst.Shop.MonthlyBoostBattleQuickBonus - autoResponse.UserBattleAuto.QuickTodayUsePrivilegeCount;
                if (availableCount > 0)
                {
                    await _jobLogger.LogAsync(userId, $"使用月卡特权进行高速战斗，可用次数: {availableCount}");
                    await ExecuteBattleQuick(userId, nm, QuestQuickExecuteType.Privilege, (int)availableCount);
                }
                else
                {
                    await _jobLogger.LogAsync(userId, "月卡特权次数已用完");
                }
            }

            // 每天有一次免费高速战斗
            if (autoResponse.UserBattleAuto.QuickTodayUseCurrencyCount >= 1)
            {
                await _jobLogger.LogAsync(userId, "今日免费高速战斗次数已用完");
            }
            else
            {
                await _jobLogger.LogAsync(userId, "使用免费高速战斗次数...");
                await ExecuteBattleQuick(userId, nm, QuestQuickExecuteType.Currency, 1);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in BossHighSpeedBattleHandler for user {UserId}", userId);
            await _jobLogger.LogAsync(userId, $"BOSS高速战斗失败: {ex.Message}", "Error");
            throw;
        }
    }

    private async Task ExecuteBattleQuick(long userId, NetworkManager nm, QuestQuickExecuteType type, int count)
    {
        var req = new QuickRequest 
        { 
            QuestQuickExecuteType = type, 
            QuickCount = count 
        };
        
        var quickResponse = await nm.SendRequest<QuickRequest, QuickResponse>(req);

        var result = quickResponse.AutoBattleRewardResult;
        
        await _jobLogger.LogAsync(userId, 
            $"高速战斗完成 - 人口金币: {result.GoldByPopulation}, " +
            $"人口潜能宝石: {result.PotentialJewelByPopulation}, " +
            $"角色经验: {result.BattleRewardResult?.CharacterExp ?? 0}, " +
            $"金币: {result.BattleRewardResult?.ExtraGold ?? 0}, " +
            $"玩家经验: {result.BattleRewardResult?.PlayerExp ?? 0}, " +
            $"Rank提升: {result.BattleRewardResult?.RankUp ?? 0}");

        // 记录物品奖励
        if (result.BattleRewardResult?.FixedItemList?.Count > 0)
        {
            var items = string.Join(", ", result.BattleRewardResult.FixedItemList.Select(i => $"{i.ItemType}:{i.ItemId} x{i.ItemCount}"));
            await _jobLogger.LogAsync(userId, $"固定物品: {items}");
        }
        if (result.BattleRewardResult?.DropItemList?.Count > 0)
        {
            var items = string.Join(", ", result.BattleRewardResult.DropItemList.Select(i => $"{i.ItemType}:{i.ItemId} x{i.ItemCount}"));
            await _jobLogger.LogAsync(userId, $"掉落物品: {items}");
        }
    }

    private bool IsValidMonthlyBoost(NetworkManager nm)
    {
        // 检查是否有有效的月卡
        var monthlyBoostInfos = nm.UserSyncData?.UserShopMonthlyBoostDtoInfos;
        if (monthlyBoostInfos == null || monthlyBoostInfos.Count == 0) return false;

        var now = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        return monthlyBoostInfos.Exists(d => d.ExpirationTimeStamp > now);
    }
}
