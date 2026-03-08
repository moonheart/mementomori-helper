using System.Collections.Concurrent;
using Injectio.Attributes;
using MementoMori.Api.Infrastructure;
using MementoMori.Api.Models;
using MementoMori.Ortega.Share.Enums;

namespace MementoMori.Api.Services;

/// <summary>
/// 后台任务类型
/// </summary>
public enum BackgroundTaskType
{
    AutoBoss,
    AutoTower
}

/// <summary>
/// 后台任务会话
/// </summary>
public class BackgroundTaskSession : IDisposable
{
    public long UserId { get; }
    public BackgroundTaskType TaskType { get; }
    public CancellationTokenSource CancellationTokenSource { get; }
    public Task? RunningTask { get; set; }
    public bool IsRunning => RunningTask != null && !RunningTask.IsCompleted;

    /// <summary>
    /// 进度信息
    /// </summary>
    public object? Progress { get; set; }

    public BackgroundTaskSession(long userId, BackgroundTaskType taskType)
    {
        UserId = userId;
        TaskType = taskType;
        CancellationTokenSource = new CancellationTokenSource();
    }

    public void Dispose()
    {
        CancellationTokenSource.Cancel();
        CancellationTokenSource.Dispose();
    }
}

/// <summary>
/// 后台任务管理器
/// 管理用户的长时间运行任务（自动刷主线、自动爬塔等）
/// </summary>
[RegisterSingleton]
[AutoConstructor]
public partial class BackgroundTaskManager
{
    private readonly IServiceProvider _serviceProvider;
    private readonly AccountManager _accountManager;
    private readonly ILogger<BackgroundTaskManager> _logger;
    private readonly ILoggerFactory _loggerFactory;
    private readonly JobLogger _jobLogger;

    // 用户ID + 任务类型 -> 会话
    private readonly ConcurrentDictionary<(long, BackgroundTaskType), BackgroundTaskSession> _sessions = new();

    /// <summary>
    /// 启动自动刷主线任务
    /// </summary>
    public async Task<bool> StartAutoBossChallengeAsync(long userId, long? targetQuestId = null)
    {
        var key = (userId, BackgroundTaskType.AutoBoss);

        // 检查是否已有任务在运行
        if (_sessions.TryGetValue(key, out var existingSession) && existingSession.IsRunning)
        {
            _logger.LogWarning("Auto boss challenge already running for user {UserId}", userId);
            return false;
        }

        // 清理旧会话
        if (existingSession != null)
        {
            existingSession.Dispose();
            _sessions.TryRemove(key, out _);
        }

        // 创建新会话
        var session = new BackgroundTaskSession(userId, BackgroundTaskType.AutoBoss);
        session.Progress = new AutoBossProgress();
        _sessions[key] = session;

        // 获取账户上下文
        var context = await _accountManager.GetOrCreateAsync(userId);

        // 创建并启动任务
        var worker = new AutoBossChallengeWorker(
            _loggerFactory.CreateLogger<AutoBossChallengeWorker>());

        session.RunningTask = Task.Run(async () =>
        {
            try
            {
                await worker.ExecuteAsync(
                    context,
                    targetQuestId,
                    (AutoBossProgress)session.Progress!,
                    msg => UpdateProgress(session, msg),
                    session.CancellationTokenSource.Token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Auto boss challenge failed for user {UserId}", userId);
                _ = _jobLogger.LogAsync(userId, $"任务失败: {ex.Message}");
            }
            finally
            {
                if (session.Progress is AutoBossProgress progress)
                {
                    progress.IsCompleted = true;
                }
            }
        }, session.CancellationTokenSource.Token);

        _logger.LogInformation("Started auto boss challenge for user {UserId}", userId);
        return true;
    }

    /// <summary>
    /// 启动自动爬塔任务
    /// </summary>
    public async Task<bool> StartAutoTowerChallengeAsync(long userId, TowerType towerType, long? targetFloor = null)
    {
        var key = (userId, BackgroundTaskType.AutoTower);

        // 检查是否已有任务在运行
        if (_sessions.TryGetValue(key, out var existingSession) && existingSession.IsRunning)
        {
            _logger.LogWarning("Auto tower challenge already running for user {UserId}", userId);
            return false;
        }

        // 清理旧会话
        if (existingSession != null)
        {
            existingSession.Dispose();
            _sessions.TryRemove(key, out _);
        }

        // 创建新会话
        var session = new BackgroundTaskSession(userId, BackgroundTaskType.AutoTower);
        session.Progress = new AutoTowerProgress { TowerType = towerType };
        _sessions[key] = session;

        // 获取账户上下文
        var context = await _accountManager.GetOrCreateAsync(userId);

        // 创建并启动任务
        var worker = new AutoTowerChallengeWorker(
            _loggerFactory.CreateLogger<AutoTowerChallengeWorker>());

        session.RunningTask = Task.Run(async () =>
        {
            try
            {
                await worker.ExecuteAsync(
                    context,
                    towerType,
                    targetFloor,
                    (AutoTowerProgress)session.Progress!,
                    msg => UpdateProgress(session, msg),
                    session.CancellationTokenSource.Token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Auto tower challenge failed for user {UserId}", userId);
                _ = _jobLogger.LogAsync(userId, $"任务失败: {ex.Message}");
            }
            finally
            {
                if (session.Progress is AutoTowerProgress progress)
                {
                    progress.IsCompleted = true;
                }
            }
        }, session.CancellationTokenSource.Token);

        _logger.LogInformation("Started auto tower challenge for user {UserId}, tower type: {TowerType}", userId, towerType);
        return true;
    }

    /// <summary>
    /// 停止任务
    /// </summary>
    public bool StopTask(long userId, BackgroundTaskType taskType)
    {
        var key = (userId, taskType);

        if (!_sessions.TryGetValue(key, out var session))
        {
            return false;
        }

        if (!session.IsRunning)
        {
            return false;
        }

        session.CancellationTokenSource.Cancel();
        _ = _jobLogger.LogAsync(userId, "任务已停止");

        _logger.LogInformation("Stopped {TaskType} for user {UserId}", taskType, userId);
        return true;
    }

    /// <summary>
    /// 获取任务状态
    /// </summary>
    public BackgroundTaskStatus GetStatus(long userId)
    {
        var status = new BackgroundTaskStatus();

        // 自动刷主线状态
        if (_sessions.TryGetValue((userId, BackgroundTaskType.AutoBoss), out var bossSession))
        {
            status.IsAutoBossRunning = bossSession.IsRunning;
            if (bossSession.Progress is AutoBossProgress bossProgress)
            {
                status.AutoBossProgress = bossProgress;
            }
        }

        // 自动爬塔状态
        if (_sessions.TryGetValue((userId, BackgroundTaskType.AutoTower), out var towerSession))
        {
            status.IsAutoTowerRunning = towerSession.IsRunning;
            if (towerSession.Progress is AutoTowerProgress towerProgress)
            {
                status.AutoTowerProgress = towerProgress;
            }
        }

        return status;
    }

    /// <summary>
    /// 清理已完成的任务会话
    /// </summary>
    public void CleanupCompletedSessions()
    {
        foreach (var kvp in _sessions)
        {
            if (!kvp.Value.IsRunning)
            {
                kvp.Value.Dispose();
                _sessions.TryRemove(kvp.Key, out _);
            }
        }
    }

    private void UpdateProgress(BackgroundTaskSession session, string message)
    {
        // 推送到 JobLogger (SignalR)
        _ = _jobLogger.LogAsync(session.UserId, message);

        // 更新进度对象
        if (session.Progress is AutoBossProgress bossProgress)
        {
            bossProgress.LastMessage = message;
        }
        else if (session.Progress is AutoTowerProgress towerProgress)
        {
            towerProgress.LastMessage = message;
        }
    }
}
