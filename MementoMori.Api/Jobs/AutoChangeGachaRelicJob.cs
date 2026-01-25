using Quartz;
using MementoMori.Api.Services;
using MementoMori.Api.Infrastructure;

namespace MementoMori.Api.Jobs;

/// <summary>
/// 自动更换圣装目标任务
/// </summary>
public class AutoChangeGachaRelicJob : AccountJobBase
{
    private readonly GameActionService _gameActionService;

    public AutoChangeGachaRelicJob(
        AccountManager accountManager,
        ILogger<AutoChangeGachaRelicJob> logger,
        IServiceProvider serviceProvider,
        GameActionService gameActionService)
        : base(accountManager, logger, serviceProvider)
    {
        _gameActionService = gameActionService;
    }

    protected override async Task ExecuteAsync(IJobExecutionContext context, AccountContext accountContext)
    {
        var userId = accountContext.AccountInfo.UserId;
        Logger.LogInformation("Starting AutoChangeGachaRelicJob for user {UserId}", userId);
        
        await _gameActionService.AutoSetGachaRelicAsync(userId);
    }
}
