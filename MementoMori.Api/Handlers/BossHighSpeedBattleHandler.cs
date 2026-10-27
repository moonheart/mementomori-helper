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

    public string ActionName => Text.T("ActionBossHighSpeedBattle");

    public async Task ExecuteAsync(AccountContext context)
    {
        var userId = context.AccountInfo.UserId;
        var nm = context.NetworkManager;

        await _jobLogger.LogAsync(userId, Text.T("BossHighSpeedStarting"));

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
                    await _jobLogger.LogAsync(userId, Text.T("BossHighSpeedUsingMonthlyBoost", availableCount));
                    await ExecuteBattleQuick(userId, nm, QuestQuickExecuteType.Privilege, (int)availableCount);
                }
                else
                {
                    await _jobLogger.LogAsync(userId, Text.T("BossHighSpeedMonthlyBoostUsedUp"));
                }
            }

            // 每天有一次免费高速战斗
            if (autoResponse.UserBattleAuto.QuickTodayUseCurrencyCount >= 1)
            {
                await _jobLogger.LogAsync(userId, Text.T("BossHighSpeedFreeUsedUp"));
            }
            else
            {
                await _jobLogger.LogAsync(userId, Text.T("BossHighSpeedUsingFree"));
                await ExecuteBattleQuick(userId, nm, QuestQuickExecuteType.Currency, 1);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in BossHighSpeedBattleHandler for user {UserId}", userId);
            await _jobLogger.LogAsync(userId, Text.T("BossHighSpeedFailed", ex.Message), "Error");
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
            Text.T("BossHighSpeedSummary", result.GoldByPopulation, result.PotentialJewelByPopulation, result.BattleRewardResult?.CharacterExp ?? 0, result.BattleRewardResult?.ExtraGold ?? 0, result.BattleRewardResult?.PlayerExp ?? 0, result.BattleRewardResult?.RankUp ?? 0));

        // 记录物品奖励
        if (result.BattleRewardResult?.FixedItemList?.Count > 0)
        {
            var items = string.Join(", ", result.BattleRewardResult.FixedItemList.Select(i => $"{i.ItemType}:{i.ItemId} x{i.ItemCount}"));
            await _jobLogger.LogAsync(userId, Text.T("BossFixedItems", items));
        }
        if (result.BattleRewardResult?.DropItemList?.Count > 0)
        {
            var items = string.Join(", ", result.BattleRewardResult.DropItemList.Select(i => $"{i.ItemType}:{i.ItemId} x{i.ItemCount}"));
            await _jobLogger.LogAsync(userId, Text.T("BossDropItems", items));
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
