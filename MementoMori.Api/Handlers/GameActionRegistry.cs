using System.Collections.ObjectModel;

namespace MementoMori.Api.Handlers;

/// <summary>
/// 游戏动作注册表 - 管理所有可用的游戏动作及其元数据
/// </summary>
public static class GameActionRegistry
{
    /// <summary>
    /// 动作元数据
    /// </summary>
    public class GameActionMetadata
    {
        public string ActionKey { get; set; } = string.Empty;
        public Type HandlerType { get; set; } = typeof(IGameActionHandler);
        public string LocalizationKey { get; set; } = string.Empty;
    }

    public static string GetActionKey(Type t) => t.Name.EndsWith("Handler") ? t.Name[..^7] : t.Name;

    private static readonly Dictionary<string, GameActionMetadata> _actions = typeof(GameActionRegistry).Assembly
        .GetTypes()
        .Where(t => typeof(IGameActionHandler).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
        .Select(t =>
        {
            var actionKey = GetActionKey(t);
            return new GameActionMetadata
            {
                ActionKey = actionKey,
                HandlerType = t,
                LocalizationKey = $"ACTION_{actionKey}"
            };
        })
        .ToDictionary(m => m.ActionKey);

    /// <summary>
    /// 获取所有动作
    /// </summary>
    public static ReadOnlyDictionary<string, GameActionMetadata> AllActions => new(_actions);

    /// <summary>
    /// 每日任务的默认动作列表（按原始顺序）
    /// </summary>
    public static readonly List<string> DefaultDailyActionKeys = new List<Type>
    {
        typeof(DailyLoginBonusHandler),
        typeof(VipDailyGiftHandler),
        typeof(MonthlyBoostHandler),
        typeof(AutoBattleRewardHandler),
        typeof(FriendPointTransferHandler),
        typeof(PresentReceiveHandler),
        typeof(EquipmentReinforcementHandler),
        typeof(BossQuickBattleHandler),
        typeof(InfiniteTowerHandler),
        typeof(BossHighSpeedBattleHandler),
        typeof(GvgRewardHandler),
        typeof(GuildCheckinHandler),
        typeof(GuildRaidBattleHandler),
        typeof(GuildTowerHandler),
        typeof(FriendManageHandler),
        typeof(AchievementRewardHandler),
        typeof(BountyQuestRewardHandler),
        typeof(BountyQuestDispatchHandler),
        typeof(DungeonBattleHandler),
        typeof(MissionRewardHandler),
        typeof(AutoUseItemsHandler),
        typeof(FreeGachaHandler),
        typeof(AutoUseItemsHandler),
        typeof(CharacterRankUpHandler)
    }.Select(GetActionKey).ToList();

    /// <summary>
    /// 每小时任务的默认动作列表（按原始顺序）
    /// </summary>
    public static readonly List<string> DefaultHourlyActionKeys = new List<Type>
    {
        typeof(DailyLoginBonusHandler),
        typeof(BountyQuestDispatchHandler),
        typeof(PresentReceiveHandler),
        typeof(AutoBattleRewardHandler),
        typeof(GuildRaidBattleHandler),
        typeof(GuildTowerHandler),
        typeof(GvgRewardHandler),
        typeof(FriendPointTransferHandler),
        typeof(BountyQuestRewardHandler),
        typeof(MissionRewardHandler),
        typeof(FreeGachaHandler),
        typeof(AutoUseItemsHandler),
        typeof(BookSortAutoHandler)
    }.Select(GetActionKey).ToList();

    /// <summary>
    /// 根据动作键获取元数据
    /// </summary>
    public static GameActionMetadata? GetMetadata(string actionKey)
    {
        return _actions.TryGetValue(actionKey, out var metadata) ? metadata : null;
    }
}
