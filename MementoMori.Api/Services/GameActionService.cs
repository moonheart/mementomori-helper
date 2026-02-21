using MementoMori.Api.Infrastructure;
using MementoMori.Api.Handlers;
using MementoMori.Api.Handlers.LocalRaid;

namespace MementoMori.Api.Services;

/// <summary>
/// 游戏业务动作服务 - 协调各种 Handler 的执行
/// </summary>
[RegisterSingleton]
[AutoConstructor]
public partial class GameActionService
{
    private readonly ILogger<GameActionService> _logger;
    private readonly AccountManager _accountManager;
    private readonly JobLogger _jobLogger;
    private readonly IServiceProvider _serviceProvider;
    private readonly ActionExecutor _executor;

    /// <summary>
    /// 执行所有每日自动化任务（DailyJob 使用）
    /// </summary>
    public async Task ExecuteDailyActionAsync(long userId)
    {
        var context = await _accountManager.GetOrCreateAsync(userId);
        await _jobLogger.LogAsync(userId, "开始执行全量自动化任务...");

        var handlers = new List<IGameActionHandler>
        {
            // ===== 按原始代码顺序排列 =====
            // 1-4: 登录与每日奖励
            _serviceProvider.GetRequiredService<DailyLoginBonusHandler>(),        // GetLoginBonus()
            _serviceProvider.GetRequiredService<VipDailyGiftHandler>(),           // GetVipGift()
            _serviceProvider.GetRequiredService<MonthlyBoostHandler>(),           // ReceiveMonthlyBoost()
            _serviceProvider.GetRequiredService<AutoBattleRewardHandler>(),       // GetAutoBattleReward()
            
            // 5-6: 社交资源
            _serviceProvider.GetRequiredService<FriendPointTransferHandler>(),    // BulkTransferFriendPoint()
            _serviceProvider.GetRequiredService<PresentReceiveHandler>(),         // PresentReceiveItem()
            
            // 7: 装备强化 (条件执行)
            _serviceProvider.GetRequiredService<EquipmentReinforcementHandler>(), // ReinforcementEquipmentOneTime()
            
            // 8-10: Boss战斗
            _serviceProvider.GetRequiredService<BossQuickBattleHandler>(),        // BattleBossQuick()
            _serviceProvider.GetRequiredService<InfiniteTowerHandler>(),          // InfiniteTowerQuick()
            _serviceProvider.GetRequiredService<BossHighSpeedBattleHandler>(),    // BossHishSpeedBattle()
            
            // 11-14: 公会相关
            _serviceProvider.GetRequiredService<GvgRewardHandler>(),              // ReceiveGvgReward()
            _serviceProvider.GetRequiredService<GuildCheckinHandler>(),           // GuildCheckin()
            _serviceProvider.GetRequiredService<GuildRaidBattleHandler>(),        // GuildRaid() ⚠️ 新增
            _serviceProvider.GetRequiredService<GuildTowerHandler>(),             // AutoGuildTower()
            
            // 15: 好友管理
            _serviceProvider.GetRequiredService<FriendManageHandler>(),           // AutoFriendManage()
            
            // 16: 成就奖励
            _serviceProvider.GetRequiredService<AchievementRewardHandler>(),      // ReceiveAchievementReward()
            
            // 17-18: 赏金任务
            _serviceProvider.GetRequiredService<BountyQuestRewardHandler>(),      // BountyQuestRewardAuto()
            _serviceProvider.GetRequiredService<BountyQuestDispatchHandler>(),    // BountyQuestStartAuto()
            
            // 19: 地下城 (条件执行)
            _serviceProvider.GetRequiredService<DungeonBattleHandler>(),          // AutoDungeonBattle()
            
            // 20-21: 任务奖励
            _serviceProvider.GetRequiredService<MissionRewardHandler>(),          // CompleteMissions()
            
            // 22-25: 道具使用和角色升级
            _serviceProvider.GetRequiredService<AutoUseItemsHandler>(),           // AutoUseItems() (第1次)
            _serviceProvider.GetRequiredService<FreeGachaHandler>(),              // FreeGacha()
            _serviceProvider.GetRequiredService<AutoUseItemsHandler>(),           // AutoUseItems() (第2次 - FreeGacha后获得新道具)
            _serviceProvider.GetRequiredService<CharacterRankUpHandler>()         // AutoRankUpCharacter()
        };

        await _executor.ExecuteActionsAsync(context, handlers);

        await _jobLogger.LogAsync(userId, "全量自动化任务执行完毕。");
    }

    /// <summary>
    /// 执行每小时自动化任务（HourlyJob 使用的轻量版本）
    /// </summary>
    public async Task ExecuteHourlyActionAsync(long userId)
    {
        var context = await _accountManager.GetOrCreateAsync(userId);
        await _jobLogger.LogAsync(userId, "开始执行每小时自动化任务...");

        var handlers = new List<IGameActionHandler>
        {
            // ===== 每小时任务包含的14个动作（原始 HourlyJob 的动作列表）=====
            _serviceProvider.GetRequiredService<DailyLoginBonusHandler>(),        // GetLoginBonus()
            _serviceProvider.GetRequiredService<BountyQuestDispatchHandler>(),    // BountyQuestStartAuto()
            _serviceProvider.GetRequiredService<PresentReceiveHandler>(),         // PresentReceiveItem()
            _serviceProvider.GetRequiredService<AutoBattleRewardHandler>(),       // GetAutoBattleReward()
            _serviceProvider.GetRequiredService<GuildRaidBattleHandler>(),        // GuildRaid()
            _serviceProvider.GetRequiredService<GuildTowerHandler>(),             // AutoGuildTower()
            _serviceProvider.GetRequiredService<GvgRewardHandler>(),              // ReceiveGvgReward()
            _serviceProvider.GetRequiredService<FriendPointTransferHandler>(),    // BulkTransferFriendPoint()
            _serviceProvider.GetRequiredService<BountyQuestRewardHandler>(),      // BountyQuestRewardAuto()
            _serviceProvider.GetRequiredService<MissionRewardHandler>(),          // CompleteMissions()
            _serviceProvider.GetRequiredService<FreeGachaHandler>(),              // FreeGacha() (条件执行)
            _serviceProvider.GetRequiredService<AutoUseItemsHandler>(),           // AutoUseItems() (条件执行)
            _serviceProvider.GetRequiredService<BookSortAutoHandler>()            // 书库大扫除 (条件执行)
        };

        await _executor.ExecuteActionsAsync(context, handlers);

        await _jobLogger.LogAsync(userId, "每小时自动化任务执行完毕。");
    }

    /// <summary>
    /// 仅执行自动购买商店
    /// </summary>
    public async Task AutoBuyShopItemAsync(long userId)
    {
        var context = await _accountManager.GetOrCreateAsync(userId);
        var handler = _serviceProvider.GetRequiredService<ShopAutoBuyHandler>();

        await _executor.ExecuteActionsAsync(context, [handler]);
    }

    /// <summary>
    /// 仅执行自动更换圣装目标
    /// </summary>
    public async Task AutoSetGachaRelicAsync(long userId)
    {
        var context = await _accountManager.GetOrCreateAsync(userId);
        var handler = _serviceProvider.GetRequiredService<GachaRelicChangeHandler>();

        await _executor.ExecuteActionsAsync(context, [handler]);
    }

    /// <summary>
    /// 仅执行自动抽取圣装
    /// </summary>
    public async Task AutoDrawGachaRelicAsync(long userId)
    {
        var context = await _accountManager.GetOrCreateAsync(userId);
        var handler = _serviceProvider.GetRequiredService<GachaRelicDrawHandler>();

        await _executor.ExecuteActionsAsync(context, [handler]);
    }

    /// <summary>
    /// 仅执行自动开启公会副本
    /// </summary>
    public async Task OpenGuildRaidAsync(long userId)
    {
        var context = await _accountManager.GetOrCreateAsync(userId);
        var handler = _serviceProvider.GetRequiredService<GuildRaidOpenHandler>();

        await _executor.ExecuteActionsAsync(context, [handler]);
    }

    /// <summary>
    /// 仅执行自动传奇竞技场对战
    /// </summary>
    public async Task AutoLegendLeagueAsync(long userId)
    {
        var context = await _accountManager.GetOrCreateAsync(userId);
        var handler = _serviceProvider.GetRequiredService<LegendLeagueHandler>();

        await _executor.ExecuteActionsAsync(context, [handler]);
    }

    /// <summary>
    /// 仅执行自动时空洞窟
    /// </summary>
    public async Task AutoLocalRaidAsync(long userId)
    {
        var context = await _accountManager.GetOrCreateAsync(userId);
        var handler = _serviceProvider.GetRequiredService<LocalRaidHandler>();

        await _executor.ExecuteActionsAsync(context, [handler]);
    }

    /// <summary>
    /// 仅执行自动竞技场对战
    /// </summary>
    public async Task AutoArenaPvpAsync(long userId)
    {
        var context = await _accountManager.GetOrCreateAsync(userId);
        var handler = _serviceProvider.GetRequiredService<ArenaPvpHandler>();

        await _executor.ExecuteActionsAsync(context, [handler]);
    }

    /// <summary>
    /// 领取所有任务奖励
    /// </summary>
    public async Task ClaimMissionRewardsAsync(long userId)
    {
        var context = await _accountManager.GetOrCreateAsync(userId);
        var handler = _serviceProvider.GetRequiredService<MissionRewardHandler>();

        await _executor.ExecuteActionsAsync(context, [handler]);
    }

    /// <summary>
    /// 领取自动战斗奖励
    /// </summary>
    public async Task GetAutoBattleRewardAsync(long userId)
    {
        var context = await _accountManager.GetOrCreateAsync(userId);
        var handler = _serviceProvider.GetRequiredService<AutoBattleRewardHandler>();

        await _executor.ExecuteActionsAsync(context, [handler]);
    }

    /// <summary>
    /// 执行BOSS快速战斗
    /// </summary>
    public async Task BossQuickBattleAsync(long userId)
    {
        var context = await _accountManager.GetOrCreateAsync(userId);
        var handler = _serviceProvider.GetRequiredService<BossQuickBattleHandler>();

        await _executor.ExecuteActionsAsync(context, [handler]);
    }

    /// <summary>
    /// 执行BOSS高速战斗
    /// </summary>
    public async Task BossHighSpeedBattleAsync(long userId)
    {
        var context = await _accountManager.GetOrCreateAsync(userId);
        var handler = _serviceProvider.GetRequiredService<BossHighSpeedBattleHandler>();

        await _executor.ExecuteActionsAsync(context, [handler]);
    }

    /// <summary>
    /// 执行自动BOSS挑战
    /// </summary>
    public async Task AutoBossChallengeAsync(long userId)
    {
        var context = await _accountManager.GetOrCreateAsync(userId);
        var handler = _serviceProvider.GetRequiredService<AutoBossChallengeHandler>();

        await _executor.ExecuteActionsAsync(context, [handler]);
    }
}
