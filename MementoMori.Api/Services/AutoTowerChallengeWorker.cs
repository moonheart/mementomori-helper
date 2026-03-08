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
            TowerType.Infinite => "无限塔",
            TowerType.Blue => "水塔",
            TowerType.Red => "火塔",
            TowerType.Green => "风塔",
            TowerType.Yellow => "地塔",
            _ => "未知塔"
        };

        log($"开始自动爬{towerTypeName}...");

        try
        {
            // 先同步用户数据
            log("正在同步用户数据...");
            try
            {
                await nm.SendRequest<GetUserDataRequest, GetUserDataResponse>(new GetUserDataRequest());
                log("用户数据同步完成");
            }
            catch (Exception ex)
            {
                log($"同步用户数据失败: {ex.Message}");
                return;
            }

            // 检查塔是否开放
            if (towerType != TowerType.Infinite && !IsTowerAvailable(towerType, context))
            {
                log($"{towerTypeName}今日未开放");
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
                        log("塔数据不可用，请先同步用户数据");
                        break;
                    }

                    var towerBattleDtoInfo = towerBattleDtoInfos.FirstOrDefault(d => d.TowerType == towerType);

                    if (towerBattleDtoInfo == null)
                    {
                        log($"未找到{towerTypeName}的数据");
                        break;
                    }

                    // 检查元素塔每日限制
                    if (towerType != TowerType.Infinite)
                    {
                        if (towerBattleDtoInfo.TodayClearNewFloorCount >= ElementalTowerDailyLimit)
                        {
                            log($"{towerTypeName}今日已通关{towerBattleDtoInfo.TodayClearNewFloorCount}层，达到每日上限");
                            break;
                        }
                    }

                    var currentFloor = towerBattleDtoInfo.MaxTowerBattleId;
                    var nextFloor = currentFloor + 1;

                    // 检查是否达到目标
                    if (targetFloor.HasValue && nextFloor > targetFloor.Value)
                    {
                        log($"已达到目标层数 {targetFloor.Value}，停止挑战");
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
                        log($"第 {nextFloor} 层: 胜利 (总{totalCount}次, 胜{winCount}次, 错{errCount}次)");

                        // 达到目标层数才停止
                        if (targetFloor.HasValue && nextFloor >= targetFloor.Value)
                        {
                            log($"已达到目标层数 {targetFloor.Value}，停止挑战");
                            break;
                        }
                    }
                    else
                    {
                        log($"第 {nextFloor} 层: 失败 (总{totalCount}次, 胜{winCount}次, 错{errCount}次)");
                        // 失败不停止，继续挑战
                    }
                }
                catch (OperationCanceledException)
                {
                    log("任务已取消");
                    break;
                }
                catch (Exception ex)
                {
                    errCount++;
                    progress.TotalCount = totalCount;
                    progress.WinCount = winCount;
                    progress.ErrorCount = errCount;
                    log($"挑战出错: {ex.Message}");

                    if (errCount > MaxErrorCount)
                    {
                        log($"错误次数超过{MaxErrorCount}次，停止挑战");
                        break;
                    }

                    // 如果是API错误，可能需要重新登录
                    if (ex is NetworkManager.ApiErrorException apiEx)
                    {
                        // 检查是否是挑战次数不足
                        if (apiEx.ErrorCode == ErrorCode.TowerBattleNotEnoughChallengeCount)
                        {
                            log("挑战次数不足");
                            break;
                        }

                        await Task.Delay(3000, token);
                    }
                }
            }

            log($"自动爬{towerTypeName}结束 - 总计{totalCount}次，胜利{winCount}次");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in AutoTowerChallengeWorker for user {UserId}", userId);
            log($"自动爬塔失败: {ex.Message}");
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
