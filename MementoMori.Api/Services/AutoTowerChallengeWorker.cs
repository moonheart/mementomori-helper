using MementoMori.Api.Infrastructure;
using MementoMori.Api.Models;
using MementoMori.Ortega.Share;
using MementoMori.Ortega.Share.Data.ApiInterface.TowerBattle;
using MementoMori.Ortega.Share.Data.ApiInterface.User;
using MementoMori.Ortega.Share.Enums;
using static MementoMori.Ortega.Share.Masters;

namespace MementoMori.Api.Services;

/// <summary>
/// 自动爬塔工作器
/// 自动推进塔楼层挑战，直到失败或达到目标层数
/// </summary>
public class AutoTowerChallengeWorker
{
    private readonly ILogger<AutoTowerChallengeWorker> _logger;
    private const int MaxErrorCount = 5;
    private const int ElementalTowerDailyLimit = 10;

    public AutoTowerChallengeWorker(ILogger<AutoTowerChallengeWorker> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// 执行自动爬塔
    /// </summary>
    /// <param name="context">账户上下文</param>
    /// <param name="towerType">塔类型</param>
    /// <param name="targetFloor">目标层数（可选，null表示无限制）</param>
    /// <param name="progress">进度对象</param>
    /// <param name="log">日志输出</param>
    /// <param name="token">取消令牌</param>
    public async Task ExecuteAsync(
        AccountContext context,
        TowerType towerType,
        long? targetFloor,
        AutoTowerProgress progress,
        Action<string> log,
        CancellationToken token)
    {
        var userId = context.AccountInfo.UserId;
        var nm = context.NetworkManager;

        progress.TowerType = towerType;
        var totalCount = 0;
        var winCount = 0;
        var errCount = 0;

        var towerTypeName = towerType switch
        {
            TowerType.Infinite => Text.T("TowerNameInfinite"),
            TowerType.Blue => Text.T("TowerNameBlue"),
            TowerType.Red => Text.T("TowerNameRed"),
            TowerType.Green => Text.T("TowerNameGreen"),
            TowerType.Yellow => Text.T("TowerNameYellow"),
            _ => Text.T("TowerNameUnknown")
        };

        log(Text.T("TowerAutoStarting", towerTypeName));

        try
        {
            // 先同步用户数据
            log(Text.T("JobSyncingUserData"));
            try
            {
                await nm.SendRequest<GetUserDataRequest, GetUserDataResponse>(new GetUserDataRequest());
                log(Text.T("JobUserDataSynced"));
            }
            catch (Exception ex)
            {
                log(Text.T("JobUserDataSyncFailed", ex.Message));
                return;
            }

            // 检查塔是否开放
            if (towerType != TowerType.Infinite && !IsTowerAvailable(towerType, context))
            {
                log(Text.T("TowerNotOpenToday", towerTypeName));
                return;
            }

            while (!token.IsCancellationRequested)
            {
                try
                {
                    // 获取塔进度数据
                    var towerBattleDtoInfos = nm.UserSyncData?.UserTowerBattleDtoInfos;
                    if (towerBattleDtoInfos == null || !towerBattleDtoInfos.Any())
                    {
                        log(Text.T("TowerNoData"));
                        break;
                    }

                    var towerBattleDtoInfo = towerBattleDtoInfos.FirstOrDefault(d => d.TowerType == towerType);

                    if (towerBattleDtoInfo == null)
                    {
                        log(Text.T("TowerDataNotFound", towerTypeName));
                        break;
                    }

                    // 检查元素塔每日限制
                    if (towerType != TowerType.Infinite)
                    {
                        if (towerBattleDtoInfo.TodayClearNewFloorCount >= ElementalTowerDailyLimit)
                        {
                            log(Text.T("TowerDailyLimitReached", towerTypeName, towerBattleDtoInfo.TodayClearNewFloorCount));
                            break;
                        }
                    }

                    var currentFloor = towerBattleDtoInfo.MaxTowerBattleId;
                    var nextFloor = currentFloor + 1;

                    // 检查是否达到目标
                    if (targetFloor.HasValue && nextFloor > targetFloor.Value)
                    {
                        log(Text.T("TowerReachedTargetFloor", targetFloor.Value));
                        break;
                    }

                    // 执行塔战斗
                    var response = await nm.SendRequest<StartRequest, StartResponse>(
                        new StartRequest
                        {
                            TargetTowerType = towerType,
                            TowerBattleQuestId = nextFloor
                        });

                    var win = response.BattleResult.SimulationResult.BattleEndInfo.IsWinAttacker();
                    totalCount++;

                    // 更新进度
                    progress.CurrentFloor = nextFloor;
                    progress.TotalCount = totalCount;
                    progress.WinCount = winCount;
                    progress.ErrorCount = errCount;
                    progress.TodayClearCount = towerBattleDtoInfo.TodayClearNewFloorCount;

                    if (win)
                    {
                        winCount++;
                        progress.WinCount = winCount;
                        log(Text.T("TowerFloorVictory", nextFloor, totalCount, winCount, errCount));

                        // 达到目标层数才停止
                        if (targetFloor.HasValue && nextFloor >= targetFloor.Value)
                        {
                            log(Text.T("TowerReachedTargetFloor", targetFloor.Value));
                            break;
                        }
                    }
                    else
                    {
                        log(Text.T("TowerFloorDefeat", nextFloor, totalCount, winCount, errCount));
                        // 失败不停止，继续挑战
                    }
                }
                catch (OperationCanceledException)
                {
                    log(Text.T("JobTaskCancelled"));
                    break;
                }
                catch (Exception ex)
                {
                    errCount++;
                    progress.TotalCount = totalCount;
                    progress.WinCount = winCount;
                    progress.ErrorCount = errCount;
                    log(Text.T("JobChallengeError", ex.Message));

                    if (errCount > MaxErrorCount)
                    {
                        log(Text.T("JobTooManyErrorsStopping", MaxErrorCount));
                        break;
                    }

                    // 如果是API错误，可能需要重新登录
                    if (ex is NetworkManager.ApiErrorException apiEx)
                    {
                        // 检查是否是挑战次数不足
                        if (apiEx.ErrorCode == ErrorCode.TowerBattleNotEnoughChallengeCount)
                        {
                            log(Text.T("TowerNotEnoughAttempts"));
                            break;
                        }

                        await Task.Delay(3000, token);
                    }
                }
            }

            log(Text.T("TowerAutoSummary", towerTypeName, totalCount, winCount));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in AutoTowerChallengeWorker for user {UserId}", userId);
            log(Text.T("TowerAutoFailed", ex.Message));
            throw;
        }
    }

    /// <summary>
    /// 检查元素塔今日是否开放
    /// </summary>
    private bool IsTowerAvailable(TowerType towerType, AccountContext context)
    {
        // 参考原有代码中的 GetAvailableTower 逻辑
        var now = DateTimeOffset.UtcNow.ToOffset(context.TimeManager.DiffFromUtc) - TimeSpan.FromHours(4);
        var dayOfWeek = now.DayOfWeek;

        return dayOfWeek switch
        {
            DayOfWeek.Sunday => true, // 所有塔开放
            DayOfWeek.Monday => towerType == TowerType.Blue,
            DayOfWeek.Tuesday => towerType == TowerType.Red,
            DayOfWeek.Wednesday => towerType == TowerType.Green,
            DayOfWeek.Thursday => towerType == TowerType.Yellow,
            DayOfWeek.Friday => towerType == TowerType.Blue || towerType == TowerType.Red,
            DayOfWeek.Saturday => towerType == TowerType.Yellow || towerType == TowerType.Green,
            _ => false
        };
    }
}
