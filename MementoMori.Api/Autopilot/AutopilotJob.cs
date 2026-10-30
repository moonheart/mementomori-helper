using MementoMori.Api.Handlers;
using MementoMori.Api.Jobs;
using MementoMori.Api.Services;
using Quartz;

namespace MementoMori.Api.Autopilot;

/// <summary>
/// Autopilot: 抽卡 → 升星 → 组队 → 等级链接 → 成长 → 推图/爬塔 → 再成长 → 快照
/// 只跑用户勾选的步骤
/// </summary>
[DisallowConcurrentExecution]
public class AutopilotJob : AccountJobBase
{
    public const string SettingKey = "autopilot";

    /// <summary>步骤的执行顺序</summary>
    public static readonly Type[] Steps =
    {
        typeof(AutoPullGachaHandler), typeof(SmartRankUpHandler), typeof(AutoBuildTeamHandler), typeof(AutoFillLevelLinkHandler),
        typeof(AutoLevelUpHandler), typeof(AutoEquipBestHandler), typeof(AutoReinforceTeamHandler), typeof(AutoSphereHandler),
        typeof(AutoClimbQuestHandler), typeof(AutoClimbTowerHandler)
    };

    // 推图/爬塔拿到的资源再成长一次
    private static readonly Type[] GrowAgain = {typeof(AutoLevelUpHandler), typeof(AutoEquipBestHandler), typeof(AutoReinforceTeamHandler)};
    private static readonly Type[] Climbs = {typeof(AutoClimbQuestHandler), typeof(AutoClimbTowerHandler)};

    public static string Key(Type t) => GameActionRegistry.GetActionKey(t);

    private readonly PlayerSettingService _settings;
    private readonly ActionExecutor _executor;
    private readonly GrowthTracker _tracker;

    public AutopilotJob(AccountManager accountManager, ILogger<AutopilotJob> logger, IServiceProvider serviceProvider,
        PlayerSettingService settings, ActionExecutor executor, GrowthTracker tracker)
        : base(accountManager, logger, serviceProvider)
    {
        _settings = settings;
        _executor = executor;
        _tracker = tracker;
    }

    protected override async Task ExecuteAsync(IJobExecutionContext context, AccountContext accountContext)
    {
        var userId = accountContext.AccountInfo.UserId;
        var config = await _settings.GetSettingAsync<AutopilotConfig>(userId, SettingKey) ?? new();
        // 界面上单独点某一步时只跑那一步
        context.MergedJobDataMap.TryGetString("step", out var only);
        var enabled = Steps.Where(t => only != null ? Key(t) == only : config.Steps.Contains(Key(t))).ToList();
        var handlers = enabled.ToList();
        if (enabled.Intersect(Climbs).Any()) handlers.AddRange(GrowAgain.Where(enabled.Contains));

        await _executor.ExecuteActionsAsync(accountContext, handlers.Select(t => (IGameActionHandler) ServiceProvider.GetRequiredService(t)));
        await _tracker.SnapshotAsync(userId, accountContext.NetworkManager.UserSyncData);
    }
}
