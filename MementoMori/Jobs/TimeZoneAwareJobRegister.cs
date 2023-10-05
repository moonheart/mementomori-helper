using System.Reflection;
using Microsoft.Extensions.Options;
using Quartz;

namespace MementoMori.Jobs;

public class TimeZoneAwareJobRegister
{
    private readonly ISchedulerFactory _schedulerFactory;
    private readonly TimeManager _timeManager;
    private readonly GameConfig _gameConfig;

    public TimeZoneAwareJobRegister(ISchedulerFactory schedulerFactory, TimeManager timeManager, IOptions<GameConfig> gameOptions)
    {
        _schedulerFactory = schedulerFactory;
        _timeManager = timeManager;
        _gameConfig = gameOptions.Value;
    }

    public async Task RegisterJobs()
    {
        if (_gameConfig.AutoJob.DisableAll)
        {
            return;
        }

        var scheduler = await _schedulerFactory.GetScheduler();
        AddJob<DailyJob>(scheduler, _gameConfig.AutoJob.DailyJobCron);
        AddJob<HourlyJob>(scheduler, _gameConfig.AutoJob.HourlyJobCron);
        AddJob<PvpJob>(scheduler, _gameConfig.AutoJob.PvpJobCron);
    }

    private void AddJob<T>(IScheduler scheduler, string cron) where T : IJob
    {
        var type = typeof(T);
        var jobKey = new JobKey(type.FullName!);
        var jobDetail = JobBuilder.Create<T>().WithIdentity(jobKey).Build();

        var customTimeZone = TimeZoneInfo.CreateCustomTimeZone(_timeManager.DiffFromUtc.ToString(), _timeManager.DiffFromUtc, null, null);
        var trigger = TriggerBuilder.Create()
            .ForJob(jobKey)
            .WithIdentity($"{type.FullName}-trigger")
            .WithCronSchedule(cron, builer => builer.InTimeZone(customTimeZone))
            .Build();
        scheduler.UnscheduleJob(trigger.Key);
        scheduler.AddJob(jobDetail, true);
        scheduler.ScheduleJob(jobDetail, trigger);
    }
}

[AttributeUsage(AttributeTargets.Class)]
public class CronAttribute : Attribute
{
    public CronAttribute(string cron)
    {
        Cron = cron;
    }

    public string Cron { get; set; }
}