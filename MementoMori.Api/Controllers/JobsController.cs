using Microsoft.AspNetCore.Mvc;
using MementoMori.Api.Services;
using MementoMori.Api.Models;

namespace MementoMori.Api.Controllers;

/// <summary>
/// 定时任务管理控制器
/// </summary>
[ApiController]
[Route("api/[controller]")]
[AutoConstructor]
public partial class JobsController : ControllerBase
{
    private readonly JobManagerService _jobManager;
    private readonly PlayerSettingService _settingService;
    private readonly GameActionService _gameActionService;

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
        var config = await _settingService.GetSettingAsync<GameConfig.AutoJobModel>(userId, SettingKeys.AutoJob);
        return Ok(config ?? new GameConfig.AutoJobModel());
    }

    /// <summary>
    /// 更新账户的自动化配置并刷新任务
    /// </summary>
    [HttpPost("{userId}/config")]
    public async Task<ActionResult> UpdateJobConfig(long userId, [FromBody] GameConfig.AutoJobModel config)
    {
        await _settingService.SaveSettingAsync(userId, SettingKeys.AutoJob, config);
        
        // 刷新 Quartz 任务
        await _jobManager.RegisterJobsAsync(userId);
        
        return Ok(new { Message = Text.T("ApiJobConfigSaved") });
    }

    public record PauseRequest(double? Hours);

    [HttpGet("{userId}/pause")]
    public Task<JobManagerService.PauseState> GetPause(long userId) => _jobManager.GetPauseAsync(userId);

    /// <summary>暂停所有定时任务; hours 为空 = 一直暂停到手动恢复</summary>
    [HttpPost("{userId}/pause")]
    public async Task<ActionResult> Pause(long userId, [FromBody] PauseRequest request)
    {
        await _jobManager.SetPauseAsync(userId, new JobManagerService.PauseState
        {
            Paused = true, Until = request.Hours is > 0 ? DateTime.Now.AddHours(request.Hours.Value) : null
        });
        return Ok();
    }

    [HttpDelete("{userId}/pause")]
    public async Task<ActionResult> Resume(long userId)
    {
        await _jobManager.SetPauseAsync(userId, new JobManagerService.PauseState());
        return Ok();
    }

    /// <summary>
    /// 立即执行一个动作 (例如 CharacterStoryReward), 等它跑完再返回
    /// </summary>
    [HttpPost("{userId}/action/{actionKey}")]
    public async Task<ActionResult> RunAction(long userId, string actionKey) =>
        await _gameActionService.ExecuteSingleActionAsync(userId, actionKey) ? Ok() : NotFound($"unknown action {actionKey}");

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
        return Ok(new { Message = Text.T("ApiJobTriggered", request.JobType) });
    }
}
