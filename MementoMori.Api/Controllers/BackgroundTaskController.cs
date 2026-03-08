using Microsoft.AspNetCore.Mvc;
using MementoMori.Api.Models;
using MementoMori.Api.Services;

namespace MementoMori.Api.Controllers;

/// <summary>
/// 后台任务控制器
/// 管理用户的长时间运行任务（自动刷主线、自动爬塔等）
/// </summary>
[ApiController]
[Route("api/[controller]")]
[AutoConstructor]
public partial class BackgroundTaskController : ControllerBase
{
    private readonly BackgroundTaskManager _taskManager;

    /// <summary>
    /// 启动自动刷主线任务
    /// </summary>
    [HttpPost("{userId}/auto-boss/start")]
    public async Task<ActionResult> StartAutoBoss(long userId, [FromBody] StartAutoBossRequest? request = null)
    {
        var started = await _taskManager.StartAutoBossChallengeAsync(userId, request?.TargetQuestId);

        if (!started)
        {
            return BadRequest(new { Message = "任务已在运行中" });
        }

        return Ok(new { Message = "自动刷主线任务已启动" });
    }

    /// <summary>
    /// 停止自动刷主线任务
    /// </summary>
    [HttpPost("{userId}/auto-boss/stop")]
    public ActionResult StopAutoBoss(long userId)
    {
        var stopped = _taskManager.StopTask(userId, BackgroundTaskType.AutoBoss);

        if (!stopped)
        {
            return BadRequest(new { Message = "没有正在运行的任务" });
        }

        return Ok(new { Message = "自动刷主线任务已停止" });
    }

    /// <summary>
    /// 启动自动爬塔任务
    /// </summary>
    [HttpPost("{userId}/auto-tower/start")]
    public async Task<ActionResult> StartAutoTower(long userId, [FromBody] StartAutoTowerRequest? request = null)
    {
        var towerType = request?.TowerType ?? Ortega.Share.Enums.TowerType.Infinite;
        var started = await _taskManager.StartAutoTowerChallengeAsync(userId, towerType, request?.TargetFloor);

        if (!started)
        {
            return BadRequest(new { Message = "任务已在运行中" });
        }

        return Ok(new { Message = $"自动爬塔任务已启动 (塔类型: {towerType})" });
    }

    /// <summary>
    /// 停止自动爬塔任务
    /// </summary>
    [HttpPost("{userId}/auto-tower/stop")]
    public ActionResult StopAutoTower(long userId)
    {
        var stopped = _taskManager.StopTask(userId, BackgroundTaskType.AutoTower);

        if (!stopped)
        {
            return BadRequest(new { Message = "没有正在运行的任务" });
        }

        return Ok(new { Message = "自动爬塔任务已停止" });
    }

    /// <summary>
    /// 获取后台任务状态
    /// </summary>
    [HttpGet("{userId}/status")]
    public ActionResult<BackgroundTaskStatus> GetStatus(long userId)
    {
        var status = _taskManager.GetStatus(userId);
        return Ok(status);
    }
}
