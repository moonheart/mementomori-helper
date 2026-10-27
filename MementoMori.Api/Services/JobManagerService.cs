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
    private readonly IConfiguration _configuration;

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
        var config = await settingService.GetSettingAsync<GameConfig.AutoJobModel>(userId, SettingKeys.AutoJob);

        // Jobs:Enabled=false: 本地开发时不跑定时任务, 免得和服务器上的同一账号抢
        if (config == null || config.DisableAll || !_configuration.GetValue("Jobs:Enabled", true))
        {
            await UnregisterJobsAsync(userId);
            return;
        }

        // 获取时区偏移
        var offset = accountContext.TimeManager.DiffFromUtc; 

        _logger.LogInformation("Registering jobs for user {UserId} with offset {Offset}", userId, offset);

        // 先删除所有旧任务，确保配置变更时能正确移除不需要的任务
        var jobKeys = await scheduler.GetJobKeys(Quartz.Impl.Matchers.GroupMatcher<JobKey>.GroupEquals(userId.ToString()));
        if (jobKeys.Count > 0)
        {
            await scheduler.DeleteJobs(jobKeys);
            _logger.LogInformation("Removed {Count} existing jobs for user {UserId}", jobKeys.Count, userId);
        }

        // 注册核心任务（这些任务总是需要的）
        await RegisterJobAsync<Jobs.DailyJob>(scheduler, userId, config.DailyJobCron, Text.T("JobDescDaily"), offset);
        await RegisterJobAsync<Jobs.HourlyJob>(scheduler, userId, config.HourlyJobCron, Text.T("JobDescHourly"), offset);

        // 自动购买商店物品
        if (config.AutoBuyShopItem)
        {
            await RegisterJobAsync<Jobs.AutoBuyShopItemJob>(scheduler, userId, config.AutoBuyShopItemJobCron, Text.T("ActionShopAutoBuy"), offset);
        }

        // 自动更换圣装目标
        if (config.AutoChangeGachaRelic)
        {
            await RegisterJobAsync<Jobs.AutoChangeGachaRelicJob>(scheduler, userId, config.AutoChangeGachaRelicJobCron, Text.T("ActionGachaRelicChange"), offset);
        }

        // 自动抽取圣装
        if (config.AutoDrawGachaRelic)
        {
            await RegisterJobAsync<Jobs.AutoDrawGachaRelicJob>(scheduler, userId, config.AutoDrawGachaRelicJobCron, Text.T("ActionGachaRelicDraw"), offset);
        }

        // 自动开启公会副本
        if (config.AutoOpenGuildRaid)
        {
            await RegisterJobAsync<Jobs.GuildRaidBossReleaseJob>(scheduler, userId, config.GuildRaidBossReleaseCron, Text.T("ActionGuildRaidOpen"), offset);
        }

        // 传奇竞技场自动对战
        if (config.AutoLegendLeague)
        {
            await RegisterJobAsync<Jobs.LegendLeagueJob>(scheduler, userId, config.LegendLeagueJobCron, Text.T("ActionLegendLeague"), offset);
        }

        // 自动时空洞窟
        if (config.AutoLocalRaid)
        {
            await RegisterJobAsync<Jobs.AutoLocalRaidJob>(scheduler, userId, config.AutoLocalRaidJobCron, Text.T("ActionLocalRaid"), offset);
        }

        // 竞技场自动对战
        if (config.AutoPvp)
        {
            await RegisterJobAsync<Jobs.PvpJob>(scheduler, userId, config.PvpJobCron, Text.T("JobDescPvp"), offset);
        }
        
        // Autopilot (自动成长/推图/抽卡, 单独的配置)
        var autopilot = await settingService.GetSettingAsync<Autopilot.AutopilotConfig>(userId, Autopilot.AutopilotJob.SettingKey);
        if (autopilot is {Enabled: true})
        {
            await RegisterJobAsync<Autopilot.AutopilotJob>(scheduler, userId, autopilot.Cron, "Autopilot", offset);
        }

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
            throw new InvalidOperationException(Text.T("ApiJobNotRegistered", jobTypeName));
        }
    }

    /// <summary>
    /// 不管有没有注册定时, 立即跑一次
    /// </summary>
    public async Task RunOnceAsync<T>(long userId, IDictionary<string, string>? data = null) where T : IJob
    {
        var scheduler = await _schedulerFactory.GetScheduler();
        var builder = JobBuilder.Create<T>()
            .WithIdentity($"{typeof(T).Name}-once-{Guid.NewGuid():N}", userId.ToString())
            .UsingJobData("userId", userId);
        foreach (var (k, v) in data ?? new Dictionary<string, string>()) builder.UsingJobData(k, v);
        var job = builder.Build();
        await scheduler.ScheduleJob(job, TriggerBuilder.Create().StartNow().Build());
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
