using MementoMori.Api.Infrastructure;
using MementoMori.Api.Services;
using MementoMori.Ortega.Share.Data.ApiInterface.Battle;
using MementoMori.Ortega.Share.Data.ApiInterface.Quest;
using MementoMori.Ortega.Share.Data.Battle.Result;

namespace MementoMori.Api.Handlers;

/// <summary>
/// BOSS快速战斗处理器（快速战斗3次）
/// </summary>
[RegisterTransient]
[AutoConstructor]
public partial class BossQuickBattleHandler : IGameActionHandler
{
    private readonly ILogger<BossQuickBattleHandler> _logger;
    private readonly JobLogger _jobLogger;

    public string ActionName => Text.T("ActionBossQuickBattle");

    public async Task ExecuteAsync(AccountContext context)
    {
        var userId = context.AccountInfo.UserId;
        var nm = context.NetworkManager;

        await _jobLogger.LogAsync(userId, Text.T("BossQuickStarting"));

        try
        {
            // 获取当前BOSS关卡
            var bossClearMaxQuestId = nm.UserSyncData.UserBattleBossDtoInfo.BossClearMaxQuestId;
            
            // 检查是否可用快速战斗功能
            bool isQuickAvailable = await IsBossBattleQuickAvailable(nm);

            if (isQuickAvailable)
            {
                // 使用快速战斗功能（一次执行3次）
                var bossQuickResponse = await nm.SendRequest<BossQuickRequest, BossQuickResponse>(
                    new BossQuickRequest
                    {
                        QuestId = bossClearMaxQuestId,
                        QuickCount = 3
                    });

                if (bossQuickResponse.BattleRewardResult != null)
                {
                    await _jobLogger.LogAsync(userId, Text.T("BossQuickDone"));
                    await LogBattleReward(userId, bossQuickResponse.BattleRewardResult);
                }
            }
            else
            {
                // 普通模式，执行3次单独战斗
                await _jobLogger.LogAsync(userId, Text.T("BossQuickUsingNormal"));
                
                for (var i = 0; i < 3; i++)
                {
                    try
                    {
                        var bossResponse = await nm.SendRequest<BossRequest, BossResponse>(
                            new BossRequest
                            {
                                QuestId = bossClearMaxQuestId
                            });

                        if (bossResponse.BattleRewardResult == null)
                        {
                            await _jobLogger.LogAsync(userId, Text.T("BossQuickNoReward", i + 1));
                            continue;
                        }

                        await _jobLogger.LogAsync(userId, Text.T("BossQuickBattleDone", i + 1));
                        await LogBattleReward(userId, bossResponse.BattleRewardResult);

                        // 延迟一下，避免请求过快
                        if (i < 2) await Task.Delay(500);
                    }
                    catch (Exception ex) when (ex.Message.Contains("NotEnoughBossChallengeCount"))
                    {
                        await _jobLogger.LogAsync(userId, Text.T("BossNotEnoughAttempts"));
                        break;
                    }
                }
            }
        }
        catch (Exception ex) when (ex.Message.Contains("BattleBossNotEnoughBossChallengeCount"))
        {
            await _jobLogger.LogAsync(userId, Text.T("BossNotEnoughAttempts"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in BossQuickBattleHandler for user {UserId}", userId);
            await _jobLogger.LogAsync(userId, Text.T("BossQuickFailed", ex.Message), "Error");
            throw;
        }
    }

    private async Task<bool> IsBossBattleQuickAvailable(NetworkManager nm)
    {
        // 检查VIP是否解锁快速战斗功能
        var vipLevel = nm.UserSyncData.UserStatusDtoInfo.Vip;
        var vipTable = MementoMori.Ortega.Share.Masters.VipTable.GetByLevel(vipLevel);
        
        if (vipTable.IsQuickBossBattleAvailable)
        {
            return true;
        }

        // 检查是否通过关卡进度解锁
        var bossClearMaxQuestId = nm.UserSyncData.UserBattleBossDtoInfo.BossClearMaxQuestId;
        var openContent = MementoMori.Ortega.Share.Masters.OpenContentTable.GetByOpenCommandType(
            MementoMori.Ortega.Share.Enums.OpenCommandType.BossBattleQuick);
        
        return bossClearMaxQuestId >= openContent.OpenContentValue;
    }

    private async Task LogBattleReward(long userId, BattleRewardResult rewardResult)
    {
        if (rewardResult.FixedItemList?.Count > 0)
        {
            var items = string.Join(", ", rewardResult.FixedItemList.Select(i => $"{i.ItemType}:{i.ItemId} x{i.ItemCount}"));
            await _jobLogger.LogAsync(userId, Text.T("BossFixedRewards", items));
        }
        if (rewardResult.DropItemList?.Count > 0)
        {
            var items = string.Join(", ", rewardResult.DropItemList.Select(i => $"{i.ItemType}:{i.ItemId} x{i.ItemCount}"));
            await _jobLogger.LogAsync(userId, Text.T("BossDropRewards", items));
        }
    }
}
