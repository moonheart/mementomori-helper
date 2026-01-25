using Quartz;
using Quartz.Spi;
using Quartz.Impl.Matchers;
using MementoMori.Api.Infrastructure;
using MementoMori.Api.Models;

namespace MementoMori.Api.Services;

/// <summary>
/// 定时任务管理服务 - 账户级独立调度
/// </summary>
[RegisterSingleton]
[AutoConstructor]
public partial class JobManagerService
{
    private readonly ILogger<JobManagerService> _logger;
    private readonly ISchedulerFactory _schedulerFactory;
    private readonly AccountManager _accountManager;
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// 为指定用户注册/刷新所有定时任务
    /// </summary>
    public async Task RegisterJobsAsync(long userId)
    {
        var scheduler = await _schedulerFactory.GetScheduler();
        var accountContext = await _accountManager.GetOrCreateAsync(userId);
        
        // 获取用户的 AutoJob 配置
        using var scope = _serviceProvider.CreateScope();
        var settingService = scope.ServiceProvider.GetRequiredService<PlayerSettingService>();
        var config = await settingService.GetSettingAsync<GameConfig.AutoJobModel>(userId, "AutoJob");

        if (config == null || config.DisableAll)
        {
            await UnregisterJobsAsync(userId);
            return;
        }

        // 获取时区偏移
        var offset = accountContext.NetworkManager.MoriHttpClientHandler.TimeZoneOffset; 
        // 注意：原版代码中 offset 是从 networkManager.TimeManager 获取的，
        // 在新版中我们需要确保 NetworkManager 有这个能力。

        _logger.LogInformation("Registering jobs for user {UserId} with offset {Offset}", userId, offset);

        // 注册核心任务
        await RegisterJobAsync<Jobs.DailyJob>(scheduler, userId, config.DailyJobCron, "每日任务", offset);
        await RegisterJobAsync<Jobs.HourlyJob>(scheduler, userId, config.HourlyJobCron, "每小时奖励/体力任务", offset);

        // 自动购买商店物品
        if (config.AutoBuyShopItem)
        {
            await RegisterJobAsync<Jobs.AutoBuyShopItemJob>(scheduler, userId, config.AutoBuyShopItemJobCron, "商店自动购买", offset);
        }

        // if (config.AutoPvp)
        //    await RegisterJobAsync<Jobs.PvpJob>(scheduler, config.PvpJobCron, "PVP 竞技场任务", userId, offset);
        
        _logger.LogInformation("Successfully registered jobs for user {UserId}", userId);
    }

    /// <summary>
    /// 获取用户的所有任务状态
    /// </summary>
    public async Task<List<JobStatusDto>> GetJobStatusAsync(long userId)
    {
        var scheduler = await _schedulerFactory.GetScheduler();
        var triggerKeys = await scheduler.GetTriggerKeys(GroupMatcher<TriggerKey>.GroupEquals(userId.ToString()));
        
        var result = new List<JobStatusDto>();
        foreach (var triggerKey in triggerKeys)
        {
            var trigger = await scheduler.GetTrigger(triggerKey);
            if (trigger == null) continue;

            var jobDetail = await scheduler.GetJobDetail(trigger.JobKey);
            
            result.Add(new JobStatusDto
            {
                JobType = trigger.JobKey.Name,
                Description = jobDetail?.Description ?? trigger.JobKey.Name,
                NextRunTime = trigger.GetNextFireTimeUtc()?.LocalDateTime,
                LastRunTime = trigger.GetPreviousFireTimeUtc()?.LocalDateTime,
                IsActive = true
            });
        }
        return result;
    }

    /// <summary>
    /// 注销指定用户的所有任务
    /// </summary>
    public async Task UnregisterJobsAsync(long userId)
    {
        var scheduler = await _schedulerFactory.GetScheduler();
        var jobKeys = await scheduler.GetJobKeys(Quartz.Impl.Matchers.GroupMatcher<JobKey>.GroupEquals(userId.ToString()));
        await scheduler.DeleteJobs(jobKeys);
        _logger.LogInformation("Unregistered all jobs for user {UserId}", userId);
    }

    /// <summary>
    /// 立即触发一个任务
    /// </summary>
    public async Task TriggerJobAsync(long userId, string jobTypeName)
    {
        var scheduler = await _schedulerFactory.GetScheduler();
        var jobKey = new JobKey(jobTypeName, userId.ToString());
        
        if (await scheduler.CheckExists(jobKey))
        {
            await scheduler.TriggerJob(jobKey);
            _logger.LogInformation("Manually triggered job {JobKey}", jobKey);
        }
        else
        {
            _logger.LogWarning("Job {JobTypeName} not registered for user {UserId}, cannot trigger", jobTypeName, userId);
            throw new InvalidOperationException($"任务 {jobTypeName} 未注册或已被禁用");
        }
    }

    private async Task RegisterJobAsync<T>(IScheduler scheduler, long userId, string cron, string description, TimeSpan offset) where T : IJob
    {
        if (string.IsNullOrEmpty(cron)) return;

        var jobKey = new JobKey($"{typeof(T).Name}", userId.ToString());
        var triggerKey = new TriggerKey($"{typeof(T).Name}-trigger", userId.ToString());

        var jobDetail = JobBuilder.Create<T>()
            .WithIdentity(jobKey)
            .WithDescription(description)
            .UsingJobData("userId", userId)
            .Build();

        // 创建自定义时区
        var customTimeZone = TimeZoneInfo.CreateCustomTimeZone(offset.ToString(), offset, null, null);

        var trigger = TriggerBuilder.Create()
            .WithIdentity(triggerKey)
            .ForJob(jobKey)
            .WithCronSchedule(cron, builder => builder.InTimeZone(customTimeZone))
            .Build();

        // 如果已存在则先删除
        if (await scheduler.CheckExists(jobKey))
        {
            await scheduler.DeleteJob(jobKey);
        }

        await scheduler.ScheduleJob(jobDetail, trigger);
    }
    
    // 兼容 PvpJob 的参数顺序
    private async Task RegisterJobAsync<T>(IScheduler scheduler, string cron, string description, long userId, TimeSpan offset) where T : IJob
    {
        await RegisterJobAsync<T>(scheduler, userId, cron, description, offset);
    }
}
