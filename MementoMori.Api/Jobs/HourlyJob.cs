using MementoMori.Api.Services;
using Quartz;

namespace MementoMori.Api.Jobs;

/// <summary>
/// 每小时奖励/体力任务 Job
/// </summary>
public class HourlyJob : AccountJobBase
{
    private readonly GameActionService _gameActionService;

    public HourlyJob(
        AccountManager accountManager,
        ILogger<HourlyJob> logger,
        IServiceProvider serviceProvider,
        GameActionService gameActionService)
        : base(accountManager, logger, serviceProvider)
    {
        _gameActionService = gameActionService;
    }

    protected override async Task ExecuteAsync(IJobExecutionContext context, AccountContext accountContext)
    {
        // 每小时任务通常也包含大部分快速动作，或者可以定制一套更轻量的动作
        // 在原版中，HourlyJob 包含了大部分每日动作
        await _gameActionService.ExecuteAllQuickActionAsync(accountContext.AccountInfo.UserId);
    }
}
