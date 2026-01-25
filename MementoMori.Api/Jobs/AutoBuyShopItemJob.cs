using Quartz;
using MementoMori.Api.Services;
using MementoMori.Api.Infrastructure;

namespace MementoMori.Api.Jobs;

/// <summary>
/// 自动购买商店物品任务
/// </summary>
public class AutoBuyShopItemJob : AccountJobBase
{
    private readonly GameActionService _gameActionService;

    public AutoBuyShopItemJob(
        AccountManager accountManager, 
        ILogger<AutoBuyShopItemJob> logger,
        GameActionService gameActionService) 
        : base(accountManager, logger)
    {
        _gameActionService = gameActionService;
    }

    protected override async Task ExecuteAsync(IJobExecutionContext context, AccountContext accountContext)
    {
        var userId = accountContext.AccountInfo.UserId;
        Logger.LogInformation("Starting AutoBuyShopItemJob for user {UserId}", userId);
        
        await _gameActionService.AutoBuyShopItemAsync(userId);
    }
}
