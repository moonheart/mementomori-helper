using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using MementoMori.Api.Services;
using MementoMori;
using MementoMori.Api.Models;

namespace MementoMori.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[AutoConstructor]
public partial class SettingsController : ControllerBase
{
    private readonly PlayerSettingService _settingService;
    private readonly JobManagerService _jobManager;
    private readonly ILogger<SettingsController> _logger;

    /// <summary>
    /// 获取玩家所有的配置项
    /// </summary>
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetAllSettings(long userId)
    {
        var settings = await _settingService.GetAllSettingsAsync(userId);
        return Ok(settings);
    }

    /// <summary>
    /// 获取玩家特定的配置子项 (例如 AutoJob)
    /// </summary>
    [HttpGet("{userId}/{key}")]
    public async Task<IActionResult> GetSetting(long userId, string key)
    {
        key = key.ToLowerInvariant();
        _logger.LogInformation("Getting setting {Key} for user {UserId}", key, userId);
        
        // 统一走 PlayerSettingService 的便捷方法: 新键优先, 同时兼容旧键 (ShopConfig / GachaConfig / PlayerOption)
        object? result = key switch
        {
            SettingKeys.AutoJob => await _settingService.GetAutoJobSettingsAsync(userId),
            SettingKeys.Shop => await _settingService.GetShopSettingsAsync(userId),
            SettingKeys.DungeonBattle => await _settingService.GetDungeonBattleSettingsAsync(userId),
            SettingKeys.Items => await _settingService.GetItemSettingsAsync(userId),
            SettingKeys.LocalRaid => await _settingService.GetLocalRaidSettingsAsync(userId),
            SettingKeys.BountyQuestAuto => await _settingService.GetBountyQuestAutoSettingsAsync(userId),
            SettingKeys.FriendManage => await _settingService.GetFriendManageSettingsAsync(userId),
            SettingKeys.GuildTower => await _settingService.GetGuildTowerSettingsAsync(userId),
            SettingKeys.Gacha => await _settingService.GetGachaSettingsAsync(userId),
            SettingKeys.BattleLeague => await _settingService.GetBattleLeagueSettingsAsync(userId),
            SettingKeys.LegendLeague => await _settingService.GetLegendLeagueSettingsAsync(userId),
            SettingKeys.BountyQuest => await _settingService.GetBountyQuestSettingsAsync(userId),
            _ => await _settingService.GetSettingAsync<object>(userId, key)
        };

        _logger.LogInformation("Returning setting {Key} for user {UserId}: {@Result}", key, userId, result);
        return Ok(result);
    }

    /// <summary>
    /// 保存玩家特定的配置子项
    /// </summary>
    [HttpPost("{userId}/{key}")]
    public async Task<IActionResult> SaveSetting(long userId, string key, [FromBody] JsonElement value)
    {
        try
        {
            key = key.ToLowerInvariant();

            // 将 JsonElement 转换为 JSON 字符串，然后保存
            var jsonString = JsonSerializer.Serialize(value, JsonSerializerOptions.Web);
            _logger.LogInformation("Saving setting {Key} for user {UserId}: {Json}", key, userId, jsonString);
            
            // 使用反序列化+序列化的方式确保数据格式正确
            // 根据 key 确定正确的类型
            object? typedValue = key switch
            {
                SettingKeys.AutoJob => JsonSerializer.Deserialize<GameConfig.AutoJobModel>(jsonString, JsonSerializerOptions.Web),
                SettingKeys.Shop => JsonSerializer.Deserialize<GameConfig.ShopConfig>(jsonString, JsonSerializerOptions.Web),
                SettingKeys.DungeonBattle => JsonSerializer.Deserialize<GameConfig.DungeonBattleConfig>(jsonString, JsonSerializerOptions.Web),
                SettingKeys.Items => JsonSerializer.Deserialize<GameConfig.ItemsConfig>(jsonString, JsonSerializerOptions.Web),
                SettingKeys.LocalRaid => JsonSerializer.Deserialize<GameConfig.LocalRaidConfig>(jsonString, JsonSerializerOptions.Web),
                SettingKeys.BountyQuestAuto => JsonSerializer.Deserialize<GameConfig.BountyQuestAutoConfig>(jsonString, JsonSerializerOptions.Web),
                SettingKeys.FriendManage => JsonSerializer.Deserialize<FriendManageOption>(jsonString, JsonSerializerOptions.Web),
                SettingKeys.GuildTower => JsonSerializer.Deserialize<GuildTowerOption>(jsonString, JsonSerializerOptions.Web),
                SettingKeys.Gacha => JsonSerializer.Deserialize<GameConfig.GachaConfigModel>(jsonString, JsonSerializerOptions.Web),
                SettingKeys.BattleLeague => JsonSerializer.Deserialize<PvpOption>(jsonString, JsonSerializerOptions.Web),
                SettingKeys.LegendLeague => JsonSerializer.Deserialize<PvpOption>(jsonString, JsonSerializerOptions.Web),
                SettingKeys.BountyQuest => JsonSerializer.Deserialize<BountyQuestOption>(jsonString, JsonSerializerOptions.Web),
                _ => JsonSerializer.Deserialize<object>(jsonString, JsonSerializerOptions.Web)
            };
            
            if (typedValue == null)
            {
                return BadRequest(new { error = "Invalid JSON data" });
            }
            
            // 保存到数据库 (统一使用小写键名)
            await _settingService.SaveSettingAsync(userId, key, typedValue);
            
            // 如果更新的是 autojob 配置，需要刷新任务调度器
            if (key.Equals(SettingKeys.AutoJob, StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogInformation("AutoJob config updated for user {UserId}, refreshing job scheduler", userId);
                await _jobManager.RegisterJobsAsync(userId);
            }
            
            return Ok(new { success = true });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to save setting {Key} for user {UserId}", key, userId);
            return BadRequest(new { error = ex.Message });
        }
    }
}
