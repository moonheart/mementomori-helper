using MementoMori.Api.Services;
using Quartz;

namespace MementoMori.Api.Jobs;

/// <summary>
/// 暂停到期: 清掉暂停, 重新注册所有定时任务
/// </summary>
public class ResumeJob : IJob
{
    private readonly PlayerSettingService _settings;
    private readonly JobManagerService _jobs;

    public ResumeJob(PlayerSettingService settings, JobManagerService jobs)
    {
        _settings = settings;
        _jobs = jobs;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var userId = context.MergedJobDataMap.GetLongValue("userId");
        await _settings.SaveSettingAsync(userId, JobManagerService.PauseKey, new JobManagerService.PauseState());
        await _jobs.RegisterJobsAsync(userId);
    }
}
