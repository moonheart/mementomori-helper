using Microsoft.AspNetCore.Mvc;
using MementoMori.Api.Services;
using MementoMori.Api.Models;

namespace MementoMori.Api.Controllers;

/// <summary>
/// 定时任务管理控制器
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class JobsController : ControllerBase
{
    private readonly JobManagerService _jobManager;
    private readonly PlayerSettingService _settingService;

    public JobsController(
        JobManagerService jobManager,
        PlayerSettingService settingService)
    {
        _jobManager = jobManager;
        _settingService = settingService;
    }

    /// <summary>
    /// 获取账户的所有任务状态
    /// </summary>
    [HttpGet("{userId}/status")]
    public async Task<ActionResult<List<JobStatusDto>>> GetJobStatus(long userId)
    {
        var status = await _jobManager.GetJobStatusAsync(userId);
        return Ok(status);
    }

    /// <summary>
    /// 获取账户的自动化配置
    /// </summary>
    [HttpGet("{userId}/config")]
    public async Task<ActionResult<GameConfig.AutoJobModel>> GetJobConfig(long userId)
    {
        var config = await _settingService.GetSettingAsync<GameConfig.AutoJobModel>(userId, "AutoJob");
        return Ok(config ?? new GameConfig.AutoJobModel());
    }

    /// <summary>
    /// 更新账户的自动化配置并刷新任务
    /// </summary>
    [HttpPost("{userId}/config")]
    public async Task<ActionResult> UpdateJobConfig(long userId, [FromBody] GameConfig.AutoJobModel config)
    {
        await _settingService.SaveSettingAsync(userId, "AutoJob", config);
        
        // 刷新 Quartz 任务
        await _jobManager.RegisterJobsAsync(userId);
        
        return Ok(new { Message = "配置已保存并刷新任务" });
    }

    /// <summary>
    /// 立即触发某个任务
    /// </summary>
    [HttpPost("{userId}/trigger")]
    public async Task<ActionResult> TriggerJob(long userId, [FromBody] TriggerJobRequest request)
    {
        // 这里的立即触发可以通过 Quartz 的 TriggerJob 实现，
        // 或者直接调用 GameActionService 的相应方法。
        // 为了保持 Quartz 状态一致，推荐使用 TriggerJob。
        
        // 立即触发 Quartz 任务
        await _jobManager.TriggerJobAsync(userId, request.JobType);
        return Ok(new { Message = $"任务 {request.JobType} 已手动触发" });
    }
}
