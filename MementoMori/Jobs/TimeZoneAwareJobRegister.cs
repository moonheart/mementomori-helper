using System.Reflection;
using MementoMori.Common.Localization;
using MementoMori.Option;
using MementoMori.Ortega.Share;
using Microsoft.Extensions.Options;
using Quartz;

namespace MementoMori.Jobs;

public class TimeZoneAwareJobRegister
{
    private readonly ISchedulerFactory _schedulerFactory;
    private readonly IWritableOptions<GameConfig> _gameConfig;
    private readonly AccountManager _accountManager;

    public TimeZoneAwareJobRegister(ISchedulerFactory schedulerFactory, IWritableOptions<GameConfig> gameOptions, AccountManager accountManager)
    {
        _schedulerFactory = schedulerFactory;
        _gameConfig = gameOptions;
        _accountManager = accountManager;
    }

    public async Task RegisterAllJobs()
    {
        foreach (var account in _accountManager.GetAll())
        {
            await RegisterJobs(account.Key);
        }
    }

    public async Task DeregisterJobs(long userId)
    {
        var scheduler = await _schedulerFactory.GetScheduler();
        RemoveJob<DailyJob>(scheduler, userId);
        RemoveJob<HourlyJob>(scheduler, userId);
        RemoveJob<PvpJob>(scheduler, userId);
        RemoveJob<GuildRaidBossReleaseJob>(scheduler, userId);
        RemoveJob<AutoBuyShopItemJob>(scheduler, userId);
        RemoveJob<LocalRaidJob>(scheduler, userId);
    }

    public async Task RegisterJobs(long userId)
    {
        var account = _accountManager.Get(userId);
        var networkManager = account.NetworkManager;
        var scheduler = await _schedulerFactory.GetScheduler();
        if (_gameConfig.Value.AutoJob.DisableAll)
        {
            await DeregisterJobs(userId);
            return;
        }

        try
        {
            AddJob<DailyJob>(scheduler, _gameConfig.Value.AutoJob.DailyJobCron, ResourceStrings.DailyJob, userId, networkManager.TimeManager.DiffFromUtc);
            AddJob<HourlyJob>(scheduler, _gameConfig.Value.AutoJob.HourlyJobCron, ResourceStrings.RewardClaimJob, userId, networkManager.TimeManager.DiffFromUtc);
            AddJob<PvpJob>(scheduler, _gameConfig.Value.AutoJob.PvpJobCron, Masters.TextResourceTable.Get("[CommonHeaderLocalPvpLabel]"), userId, networkManager.TimeManager.DiffFromUtc);
            AddJob<GuildRaidBossReleaseJob>(scheduler, _gameConfig.Value.AutoJob.GuildRaidBossReleaseCron, Masters.TextResourceTable.Get("[GuildRaidReleaseConfirmTitle]"), userId,
                networkManager.TimeManager.DiffFromUtc);
            AddJob<AutoBuyShopItemJob>(scheduler, _gameConfig.Value.AutoJob.AutoBuyShopItemJobCron, ResourceStrings.ShopAutoBuyItems, userId, networkManager.TimeManager.DiffFromUtc);
            AddJob<LocalRaidJob>(scheduler, _gameConfig.Value.AutoJob.AutoLocalRaidJobCron, Masters.TextResourceTable.Get("[CommonHeaderLocalRaidLabel]"), userId, networkManager.TimeManager.DiffFromUtc);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    private void RemoveJob<T>(IScheduler scheduler, long userId) where T : IJob
    {
        var type = typeof(T);
        var jobKey = new JobKey($"{userId}-{type.FullName!}");
        scheduler.DeleteJob(jobKey);
    }

    private void AddJob<T>(IScheduler scheduler, string cron, string description, long userId, TimeSpan offset) where T : IJob
    {
        var type = typeof(T);
        var jobKey = new JobKey($"{userId}-{type.FullName!}");
        var jobDetail = JobBuilder.Create<T>().WithIdentity(jobKey).WithDescription(description).UsingJobData("userId", userId).Build();

        var customTimeZone = TimeZoneInfo.CreateCustomTimeZone(offset.ToString(), offset, null, null);
        var trigger = TriggerBuilder.Create()
            .ForJob(jobKey)
            .WithIdentity($"{userId}-{type.FullName}-trigger")
            .WithCronSchedule(cron, builer => builer.InTimeZone(customTimeZone))
            .Build();
        scheduler.UnscheduleJob(trigger.Key);
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