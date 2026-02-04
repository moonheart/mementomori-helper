using MementoMori.Api.Infrastructure.Database;
using MementoMori.Api.Models;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Enums;
using Newtonsoft.Json;

namespace MementoMori.Api.Services;

/// <summary>
/// 玩家设置服务 - 处理按子类型分开存储的持久化设置
/// </summary>
[RegisterSingleton]
[AutoConstructor]
public partial class PlayerSettingService
{
    private readonly IFreeSql _fsql;
    private readonly ILogger<PlayerSettingService> _logger;

    /// <summary>
    /// 获取指定的设置项
    /// </summary>
    /// <typeparam name="T">设置项的类型</typeparam>
    /// <param name="userId">玩家ID</param>
    /// <param name="key">设置项键名 (如 'AutoJob')</param>
    /// <returns>设置项实例或默认值</returns>
    public async Task<T?> GetSettingAsync<T>(long userId, string key) where T : class, new()
    {
        var entity = await _fsql.Select<PlayerSettingEntity>()
            .Where(s => s.UserId == userId && s.SettingKey == key)
            .FirstAsync();

        if (entity == null || string.IsNullOrEmpty(entity.JsonValue))
        {
            return new T();
        }

        try
        {
            return JsonConvert.DeserializeObject<T>(entity.JsonValue);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to deserialize setting {Key} for user {UserId}", key, userId);
            return new T();
        }
    }

    /// <summary>
    /// 保存指定的设置项
    /// </summary>
    /// <typeparam name="T">设置项的类型</typeparam>
    /// <param name="userId">玩家ID</param>
    /// <param name="key">设置项键名</param>
    /// <param name="value">设置项实例</param>
    public async Task SaveSettingAsync<T>(long userId, string key, T value) where T : class
    {
        var jsonValue = JsonConvert.SerializeObject(value);

        var entity = new PlayerSettingEntity
        {
            UserId = userId,
            SettingKey = key,
            JsonValue = jsonValue,
            UpdatedAt = DateTime.Now
        };

        await _fsql.InsertOrUpdate<PlayerSettingEntity>()
            .SetSource(entity)
            .ExecuteAffrowsAsync();
    }

    /// <summary>
    /// 批量获取玩家的所有设置项（返回 Key-Value 字典）
    /// </summary>
    public async Task<Dictionary<string, object?>> GetAllSettingsAsync(long userId)
    {
        var entities = await _fsql.Select<PlayerSettingEntity>()
            .Where(s => s.UserId == userId)
            .ToListAsync();

        return entities.ToDictionary(
            e => e.SettingKey,
            e => (object?) e.JsonValue // 返回原始 JSON，由控制器或调用者决定如何解析
        );
    }

    /// <summary>
    /// 初始化玩家默认配置
    /// </summary>
    public async Task InitializeDefaultSettingsAsync(long userId)
    {
        _logger.LogInformation("Initializing default settings for user {UserId}", userId);

        // 1. Local Raid
        var localRaid = new GameConfig.LocalRaidConfig
        {
            RewardItems = new List<GameConfig.WeightedItem>
            {
                new() { ItemType = ItemType.ExchangePlaceItem, ItemId = 4, Weight = 4.0 },
                new() { ItemType = ItemType.CharacterTrainingMaterial, ItemId = 2, Weight = 3.0 },
                new() { ItemType = ItemType.EquipmentReinforcementItem, ItemId = 2, Weight = 2.5 },
                new() { ItemType = ItemType.CharacterTrainingMaterial, ItemId = 1, Weight = 2.0 },
                new() { ItemType = ItemType.EquipmentReinforcementItem, ItemId = 1, Weight = 1.0 }
            },
            SelfCreateRoom = false,
            WaitSeconds = 3
        };
        await SaveSettingAsync(userId, SettingKeys.LocalRaid, localRaid);

        // 2. Bounty Quest Auto
        var bountyQuestAuto = new GameConfig.BountyQuestAutoConfig
        {
            TargetItems = new List<UserItem>
            {
                new() { ItemType = ItemType.Gold, ItemId = 1 },
                new() { ItemType = ItemType.CurrencyFree, ItemId = 1 },
                new() { ItemType = ItemType.CharacterTrainingMaterial, ItemId = 2 },
                new() { ItemType = ItemType.TreasureChest, ItemId = 4 },
                new() { ItemType = ItemType.TreasureChest, ItemId = 5 },
                new() { ItemType = ItemType.TreasureChest, ItemId = 6 },
                new() { ItemType = ItemType.TreasureChest, ItemId = 7 },
                new() { ItemType = ItemType.TreasureChest, ItemId = 8 },
                new() { ItemType = ItemType.TreasureChest, ItemId = 9 },
                new() { ItemType = ItemType.TreasureChest, ItemId = 10 },
                new() { ItemType = ItemType.TreasureChest, ItemId = 27 },
                new() { ItemType = ItemType.TreasureChest, ItemId = 28 }
            }
        };
        await SaveSettingAsync(userId, SettingKeys.BountyQuestAuto, bountyQuestAuto);

        // 3. AutoJob (部分开关默认开启)
        var autoJob = new GameConfig.AutoJobModel
        {
            AutoLocalRaid = true,
            AutoPvp = true,
            AutoLegendLeague = true,
            AutoDungeonBattle = true,
            AutoUseItems = true,
            AutoFreeGacha = true,
            AutoBuyShopItem = true,
            AutoReceiveAutoBattleReward = true,
            AutoBossQuickBattle = true,
            AutoBossHighSpeedBattle = true,
            AutoBountyQuestDispatch = true,
            AutoBountyQuestReward = true
        };
        await SaveSettingAsync(userId, SettingKeys.AutoJob, autoJob);
    }

    #region 便捷方法

    /// <summary>
    /// 获取好友管理设置
    /// </summary>
    public async Task<FriendManageOption> GetFriendManageSettingsAsync(long userId)
    {
        return await GetSettingAsync<FriendManageOption>(userId, SettingKeys.FriendManage) ?? new FriendManageOption();
    }

    /// <summary>
    /// 获取公会塔设置
    /// </summary>
    public async Task<GuildTowerOption> GetGuildTowerSettingsAsync(long userId)
    {
        return await GetSettingAsync<GuildTowerOption>(userId, SettingKeys.GuildTower) ?? new GuildTowerOption();
    }

    /// <summary>
    /// 获取地下城战斗设置
    /// </summary>
    public async Task<GameConfig.DungeonBattleConfig> GetDungeonBattleSettingsAsync(long userId)
    {
        return await GetSettingAsync<GameConfig.DungeonBattleConfig>(userId, SettingKeys.DungeonBattle) ?? new GameConfig.DungeonBattleConfig();
    }

    /// <summary>
    /// 获取自动任务设置
    /// </summary>
    public async Task<GameConfig.AutoJobModel> GetAutoJobSettingsAsync(long userId)
    {
        return await GetSettingAsync<GameConfig.AutoJobModel>(userId, SettingKeys.AutoJob) ?? new GameConfig.AutoJobModel();
    }

    /// <summary>
    /// 获取道具设置
    /// </summary>
    public async Task<GameConfig.ItemsConfig> GetItemSettingsAsync(long userId)
    {
        return await GetSettingAsync<GameConfig.ItemsConfig>(userId, SettingKeys.Items) ?? new GameConfig.ItemsConfig();
    }

    /// <summary>
    /// 获取扭蛋设置
    /// </summary>
    public async Task<GameConfig.GachaConfigModel> GetGachaSettingsAsync(long userId)
    {
        return await GetSettingAsync<GameConfig.GachaConfigModel>(userId, SettingKeys.Gacha) ?? new GameConfig.GachaConfigModel();
    }

    #endregion
}
