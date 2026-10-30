using MementoMori.Api.Services;
using Quartz;

namespace MementoMori.Api.Jobs;

/// <summary>
/// 每日任务 Job
/// </summary>
public class DailyJob : AccountJobBase
{
    private readonly GameActionService _gameActionService;

    public DailyJob(
        AccountManager accountManager,
        ILogger<DailyJob> logger,
        IServiceProvider serviceProvider,
        GameActionService gameActionService)
        : base(accountManager, logger, serviceProvider)
    {
        _gameActionService = gameActionService;
    }

    protected override async Task ExecuteAsync(IJobExecutionContext context, AccountContext accountContext)
    {
        await _gameActionService.ExecuteDailyActionAsync(accountContext.AccountInfo.UserId);
    }
}
