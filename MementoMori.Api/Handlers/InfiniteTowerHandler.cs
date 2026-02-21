using MementoMori.Api.Infrastructure;
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

    public string ActionName => "无限塔快速战斗";

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

        await _jobLogger.LogAsync(userId, "正在执行无限塔快速战斗...");

        try
        {
            var tower = nm.UserSyncData.UserTowerBattleDtoInfos.FirstOrDefault(d => d.TowerType == TowerType.Infinite);
            if (tower == null)
            {
                await _jobLogger.LogAsync(userId, "无限塔数据不可用。");
                return;
            }

            // 检查是否有快速战斗资格
            var isBossBattleQuickAvailable = IsBossBattleQuickAvailable(nm.UserSyncData);
            if (!isBossBattleQuickAvailable)
            {
                isBossBattleQuickAvailable = nm.UserSyncData.UserBattleBossDtoInfo.BossClearMaxQuestId >=
                    Masters.OpenContentTable.GetByOpenCommandType(OpenCommandType.BossBattleQuick).OpenContentValue;
            }

            if (isBossBattleQuickAvailable)
            {
                var bossQuickResponse = await nm.SendRequest<TowerBattleQuickRequest, TowerBattleQuickResponse>(
                    new TowerBattleQuickRequest
                    {
                        TargetTowerType = TowerType.Infinite,
                        TowerBattleQuestId = tower.MaxTowerBattleId,
                        QuickCount = 3
                    });
                await _jobLogger.LogAsync(userId, "无限塔快速战斗完成。");
            }
            else
            {
                // 非快速模式，执行3次普通战斗
                for (var i = 0; i < 3; i++)
                {
                    var bossQuickResponse = await nm.SendRequest<StartRequest, StartResponse>(
                        new StartRequest
                        {
                            TargetTowerType = TowerType.Infinite,
                            TowerBattleQuestId = tower.MaxTowerBattleId
                        });
                }
                await _jobLogger.LogAsync(userId, "无限塔战斗完成（普通模式）。");
            }
        }
        catch (NetworkManager.ApiErrorException ex) when (ex.Message.Contains("NotEnoughChallengeCount"))
        {
            await _jobLogger.LogAsync(userId, "无限塔挑战次数不足。");
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "无限塔战斗失败 for user {UserId}", userId);
        }
    }
}
