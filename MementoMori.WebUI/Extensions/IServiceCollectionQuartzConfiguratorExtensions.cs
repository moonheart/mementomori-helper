using System.Reflection;
using Quartz;

namespace MementoMori.WebUI.Extensions;

public static class IServiceCollectionQuartzConfiguratorExtensions
{
    public static IServiceCollectionQuartzConfigurator AddJob<T>(this IServiceCollectionQuartzConfigurator q) where T : IJob
    {
        var type = typeof(T);
        var cronAttribute = type.GetCustomAttribute(typeof(CronAttribute)) as CronAttribute;
        if (cronAttribute == null)
        {
            return q;
        }
        var jobKey = new JobKey(type.FullName!);
        q.AddJob<T>(opts => opts.WithIdentity(jobKey));
    
        q.AddTrigger(opts => opts
            .ForJob(jobKey)
            .WithIdentity($"{type.FullName}-trigger")
            //This Cron interval can be described as "run every minute" (when second is zero)
            .WithCronSchedule(cronAttribute.Cron)
        );
        return q;
    }
}

[AttributeUsage(AttributeTargets.Class)]
public class CronAttribute: Attribute
{
    public CronAttribute(string cron)
    {
        Cron = cron;
    }

    public string Cron { get; set; }
}