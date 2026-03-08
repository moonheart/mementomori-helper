using MementoMori.Api.Infrastructure;
using MementoMori.Api.Models;
using MementoMori.Ortega.Share;
using MementoMori.Ortega.Share.Data.ApiInterface.Battle;
using MementoMori.Ortega.Share.Data.ApiInterface.Quest;
using MementoMori.Ortega.Share.Data.ApiInterface.User;
using static MementoMori.Ortega.Share.Masters;

namespace MementoMori.Api.Services;

/// <summary>
/// 自动刷主线工作器
/// 自动推进主线Boss战斗，直到失败或达到目标关卡
/// </summary>
public class AutoBossChallengeWorker
{
    private readonly ILogger<AutoBossChallengeWorker> _logger;
    private const int MaxErrorCount = 5;

    public AutoBossChallengeWorker(ILogger<AutoBossChallengeWorker> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// 执行自动刷主线
    /// </summary>
    /// <param name="context">账户上下文</param>
    /// <param name="targetQuestId">目标关卡ID（可选，null表示无限制）</param>
    /// <param name="progress">进度对象</param>
    /// <param name="log">日志输出</param>
    /// <param name="token">取消令牌</param>
    public async Task ExecuteAsync(
        AccountContext context,
        long? targetQuestId,
        AutoBossProgress progress,
        Action<string> log,
        CancellationToken token)
    {
        var userId = context.AccountInfo.UserId;
        var nm = context.NetworkManager;

        var totalCount = 0;
        var winCount = 0;
        var errCount = 0;

        log("开始自动刷主线...");

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

            // 获取地图信息
            await nm.SendRequest<MapInfoRequest, MapInfoResponse>(
                new MapInfoRequest { IsUpdateOtherPlayerInfo = true });

            // 尝试获取下一个任务
            try
            {
                await nm.SendRequest<NextQuestRequest, NextQuestResponse>(new NextQuestRequest());
            }
            catch (NetworkManager.ApiErrorException ex) when (ex.ErrorCode == ErrorCode.BattleAutoNextQuestNotFound)
            {
                // 忽略这个错误，继续执行
            }

            // 持续挑战直到失败或达到目标
            while (!token.IsCancellationRequested)
            {
                try
                {
                    var questId = nm.UserSyncData.UserBattleBossDtoInfo.BossClearMaxQuestId + 1;

                    // 检查是否达到目标
                    if (targetQuestId.HasValue && questId > targetQuestId.Value)
                    {
                        log($"已达到目标关卡 {targetQuestId.Value}，停止挑战");
                        break;
                    }

                    // 获取任务信息
                    await nm.SendRequest<GetQuestInfoRequest, GetQuestInfoResponse>(
                        new GetQuestInfoRequest { TargetQuestId = questId });

                    // 执行BOSS战斗
                    var bossResponse = await nm.SendRequest<BossRequest, BossResponse>(
                        new BossRequest { QuestId = questId });

                    var win = bossResponse.BattleResult.SimulationResult.BattleEndInfo.IsWinAttacker();
                    totalCount++;

                    if (win)
                    {
                        winCount++;

                        // 更新进度
                        progress.CurrentQuestId = questId;
                        progress.TotalCount = totalCount;
                        progress.WinCount = winCount;
                        progress.ErrorCount = errCount;

                        // 获取任务信息用于日志
                        var questInfo = QuestTable.GetById(questId);
                        log($"关卡 {questInfo?.Memo ?? questId.ToString()}: 胜利 (总{totalCount}次, 胜{winCount}次, 错{errCount}次)");

                        // 胜利后更新地图信息
                        await nm.SendRequest<MapInfoRequest, MapInfoResponse>(
                            new MapInfoRequest { IsUpdateOtherPlayerInfo = true });

                        // 检查是否达到目标关卡
                        if (targetQuestId.HasValue && questId >= targetQuestId.Value)
                        {
                            log($"已达到目标关卡 {targetQuestId.Value}，停止挑战");
                            break;
                        }

                        // 尝试进入下一个任务
                        try
                        {
                            await nm.SendRequest<NextQuestRequest, NextQuestResponse>(new NextQuestRequest());
                        }
                        catch (NetworkManager.ApiErrorException ex) when (ex.ErrorCode == ErrorCode.BattleAutoNextQuestNotFound)
                        {
                            log("已到达当前最大可挑战关卡");
                            break;
                        }
                    }
                    else
                    {
                        // 更新进度（失败也要更新）
                        progress.CurrentQuestId = questId;
                        progress.TotalCount = totalCount;
                        progress.WinCount = winCount;
                        progress.ErrorCount = errCount;

                        // 失败不停止，继续挑战同一关卡
                        var questInfo = QuestTable.GetById(questId);
                        log($"关卡 {questInfo?.Memo ?? questId.ToString()}: 失败 (总{totalCount}次, 胜{winCount}次, 错{errCount}次)");
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

                    // 如果是API错误，尝试重新登录
                    if (ex is NetworkManager.ApiErrorException)
                    {
                        // 这里可以添加重新登录逻辑
                        await Task.Delay(3000, token);
                    }
                }
            }

            log($"自动刷主线结束 - 总计{totalCount}次，胜利{winCount}次");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in AutoBossChallengeWorker for user {UserId}", userId);
            log($"自动刷主线失败: {ex.Message}");
            throw;
        }
    }
}
