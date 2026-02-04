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
        // 每小时任务执行14个特定的轻量动作（原始 HourlyJob 的子集）
        // 不包括：VIP礼物、月卡、装备强化、BOSS战斗、无限塔、公会签到、好友管理、成就、地下城、角色升阶
        await _gameActionService.ExecuteHourlyActionAsync(accountContext.AccountInfo.UserId);
    }
}
