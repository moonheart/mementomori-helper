using MementoMori.Api.Extensions;
using MementoMori.Api.Infrastructure;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Api.Services;
using MementoMori.Ortega.Share;
using MementoMori.Ortega.Share.Data;
using MementoMori.Ortega.Share.Data.ApiInterface.TowerBattle;
using MementoMori.Ortega.Share.Enums;

namespace MementoMori.Api.Handlers;

/// <summary>
/// 无限塔快速战斗处理器
/// </summary>
[RegisterTransient]
[AutoConstructor]
public partial class InfiniteTowerHandler : IGameActionHandler
{
    private readonly ILogger<InfiniteTowerHandler> _logger;
    private readonly JobLogger _jobLogger;

    public string ActionName => Text.T("ActionInfiniteTower");

    private bool IsBossBattleQuickAvailable(UserSyncData syncData)
    {
        var vip = Masters.VipTable.GetByLevel(syncData.UserStatusDtoInfo.Vip);
        var isQuickAvailable = vip.IsQuickBossBattleAvailable;
        if (!isQuickAvailable) isQuickAvailable = syncData.UserBattleBossDtoInfo.BossClearMaxQuestId >= Masters.OpenContentTable.GetByOpenCommandType(OpenCommandType.BossBattleQuick).OpenContentValue;

        return isQuickAvailable;
    }


    public async Task ExecuteAsync(AccountContext context)
    {
        var userId = context.AccountInfo.UserId;
        var nm = context.NetworkManager;

        await _jobLogger.LogAsync(userId, Text.T("TowerInfiniteQuickStarting"));

        try
        {
            var tower = nm.UserSyncData.UserTowerBattleDtoInfos.FirstOrDefault(d => d.TowerType == TowerType.Infinite);
            if (tower == null)
            {
                await _jobLogger.LogAsync(userId, Text.T("TowerInfiniteNoData"));
                return;
            }

            // 检查是否有快速战斗资格
            var isBossBattleQuickAvailable = IsBossBattleQuickAvailable(nm.UserSyncData);
            if (!isBossBattleQuickAvailable)
            {
                isBossBattleQuickAvailable = nm.UserSyncData.UserBattleBossDtoInfo.BossClearMaxQuestId >=
                    Masters.OpenContentTable.GetByOpenCommandType(OpenCommandType.BossBattleQuick).OpenContentValue;
            }

            // 每天 3 次免费, 只用剩下的 (以前固定传 3, 当天打过就报错)
            var remaining = 3 - tower.TodayBattleCount;
            if (remaining <= 0)
            {
                await _jobLogger.LogAsync(userId, Text.T("TowerInfiniteUsedUp"));
            }
            else if (isBossBattleQuickAvailable)
            {
                var diamonds = nm.UserSyncData.GetUserItemCount(ItemType.CurrencyFree, isAnyCurrency: true);
                var bossQuickResponse = await nm.SendRequest<TowerBattleQuickRequest, TowerBattleQuickResponse>(
                    new TowerBattleQuickRequest
                    {
                        TargetTowerType = TowerType.Infinite,
                        TowerBattleQuestId = tower.MaxTowerBattleId,
                        QuickCount = remaining
                    });
                var spent = diamonds - nm.UserSyncData.GetUserItemCount(ItemType.CurrencyFree, isAnyCurrency: true);
                await _jobLogger.LogAsync(userId, spent > 0 ? Text.T("TowerInfiniteSpentDiamonds", spent) : Text.T("TowerInfiniteQuickDone"), spent > 0 ? "Error" : "Info");
            }
            else
            {
                // 非快速模式，执行3次普通战斗
                for (var i = 0; i < remaining; i++)
                {
                    var bossQuickResponse = await nm.SendRequest<StartRequest, StartResponse>(
                        new StartRequest
                        {
                            TargetTowerType = TowerType.Infinite,
                            TowerBattleQuestId = tower.MaxTowerBattleId
                        });
                }
                await _jobLogger.LogAsync(userId, Text.T("TowerInfiniteNormalDone"));
            }
        }
        catch (NetworkManager.ApiErrorException ex) when (ex.Message.Contains("NotEnoughChallengeCount"))
        {
            await _jobLogger.LogAsync(userId, Text.T("TowerInfiniteNotEnoughAttempts"));
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "无限塔战斗失败 for user {UserId}", userId);
        }
    }
}
