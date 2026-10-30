using Quartz;
using MementoMori.Api.Services;
using MementoMori.Api.Infrastructure;

namespace MementoMori.Api.Jobs;

/// <summary>
/// 自动开启公会副本任务
/// </summary>
public class GuildRaidBossReleaseJob : AccountJobBase
{
    private readonly GameActionService _gameActionService;

    public GuildRaidBossReleaseJob(
        AccountManager accountManager,
        ILogger<GuildRaidBossReleaseJob> logger,
        IServiceProvider serviceProvider,
        GameActionService gameActionService)
        : base(accountManager, logger, serviceProvider)
    {
        _gameActionService = gameActionService;
    }

    protected override async Task ExecuteAsync(IJobExecutionContext context, AccountContext accountContext)
    {
        var userId = accountContext.AccountInfo.UserId;
        Logger.LogInformation("Starting GuildRaidBossReleaseJob for user {UserId}", userId);
        
        await _gameActionService.OpenGuildRaidAsync(userId);
    }
}
