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

        log(Text.T("BossMainQuestStarting"));

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
                        log(Text.T("BossReachedTargetQuest", targetQuestId.Value));
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
                        log(Text.T("BossQuestVictory", questInfo?.Memo ?? questId.ToString(), totalCount, winCount, errCount));

                        // 胜利后更新地图信息
                        await nm.SendRequest<MapInfoRequest, MapInfoResponse>(
                            new MapInfoRequest { IsUpdateOtherPlayerInfo = true });

                        // 检查是否达到目标关卡
                        if (targetQuestId.HasValue && questId >= targetQuestId.Value)
                        {
                            log(Text.T("BossReachedTargetQuest", targetQuestId.Value));
                            break;
                        }

                        // 尝试进入下一个任务
                        try
                        {
                            await nm.SendRequest<NextQuestRequest, NextQuestResponse>(new NextQuestRequest());
                        }
                        catch (NetworkManager.ApiErrorException ex) when (ex.ErrorCode == ErrorCode.BattleAutoNextQuestNotFound)
                        {
                            log(Text.T("BossReachedMaxStage"));
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
                        log(Text.T("BossQuestDefeat", questInfo?.Memo ?? questId.ToString(), totalCount, winCount, errCount));
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

                    // 如果是API错误，尝试重新登录
                    if (ex is NetworkManager.ApiErrorException)
                    {
                        // 这里可以添加重新登录逻辑
                        await Task.Delay(3000, token);
                    }
                }
            }

            log(Text.T("BossMainQuestSummary", totalCount, winCount));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in AutoBossChallengeWorker for user {UserId}", userId);
            log(Text.T("BossMainQuestFailed", ex.Message));
            throw;
        }
    }
}
