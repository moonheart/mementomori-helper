namespace MementoMori.Api.Models;

/// <summary>
/// 玩家配置项键名常量
/// </summary>
public static class SettingKeys
{
    /// <summary>
    /// 自动任务配置
    /// </summary>
    public const string AutoJob = "autojob";

    /// <summary>
    /// 商店配置
    /// </summary>
    public const string Shop = "shop";

    /// <summary>
    /// 地下城战斗配置
    /// </summary>
    public const string DungeonBattle = "dungeonbattle";

    /// <summary>
    /// 物品配置
    /// </summary>
    public const string Items = "items";

    /// <summary>
    /// 时空洞窟配置
    /// </summary>
    public const string LocalRaid = "localraid";

    /// <summary>
    /// 赏金任务自动配置
    /// </summary>
    public const string BountyQuestAuto = "bountyquestauto";

    /// <summary>
    /// 好友管理配置
    /// </summary>
    public const string FriendManage = "friendmanage";

    /// <summary>
    /// 公会塔配置
    /// </summary>
    public const string GuildTower = "guildtower";

    /// <summary>
    /// 扭蛋配置 (沿用前端已使用的键名, 避免旧数据失效)
    /// </summary>
    public const string Gacha = "gachaconfig";

    /// <summary>
    /// 竞技场 (Arena) 配置
    /// </summary>
    public const string BattleLeague = "battleleague";

    /// <summary>
    /// 传奇竞技场配置
    /// </summary>
    public const string LegendLeague = "legendleague";

    /// <summary>
    /// 悬赏任务配置 (刷新次数等)
    /// </summary>
    public const string BountyQuest = "bountyquest";

    /// <summary>
    /// 旧版聚合配置键, 仅用于读取旧数据; 新代码按上面的分项键读写
    /// </summary>
    public const string LegacyPlayerOption = "PlayerOption";
}
