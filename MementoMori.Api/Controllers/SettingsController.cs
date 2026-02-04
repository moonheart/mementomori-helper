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
        _logger.LogInformation("Getting setting {Key} for user {UserId}", key, userId);
        
        // 根据 key 选择对应的类型。这里可以根据需要添加更多类型。
        // 目前参考 GameConfig.cs 中的主要子类
        object? result = key.ToLower() switch
        {
            SettingKeys.AutoJob => await _settingService.GetSettingAsync<GameConfig.AutoJobModel>(userId, key),
            SettingKeys.Shop => await _settingService.GetSettingAsync<GameConfig.ShopConfig>(userId, key),
            SettingKeys.DungeonBattle => await _settingService.GetSettingAsync<GameConfig.DungeonBattleConfig>(userId, key),
            SettingKeys.Items => await _settingService.GetSettingAsync<GameConfig.ItemsConfig>(userId, key),
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
            // 将 JsonElement 转换为 JSON 字符串，然后保存
            var jsonString = JsonSerializer.Serialize(value, JsonSerializerOptions.Web);
            _logger.LogInformation("Saving setting {Key} for user {UserId}: {Json}", key, userId, jsonString);
            
            // 使用反序列化+序列化的方式确保数据格式正确
            // 根据 key 确定正确的类型
            object? typedValue = key.ToLower() switch
            {
                SettingKeys.AutoJob => JsonSerializer.Deserialize<GameConfig.AutoJobModel>(jsonString, JsonSerializerOptions.Web),
                SettingKeys.Shop => JsonSerializer.Deserialize<GameConfig.ShopConfig>(jsonString, JsonSerializerOptions.Web),
                SettingKeys.DungeonBattle => JsonSerializer.Deserialize<GameConfig.DungeonBattleConfig>(jsonString, JsonSerializerOptions.Web),
                SettingKeys.Items => JsonSerializer.Deserialize<GameConfig.ItemsConfig>(jsonString, JsonSerializerOptions.Web),
                _ => JsonSerializer.Deserialize<object>(jsonString, JsonSerializerOptions.Web)
            };
            
            if (typedValue == null)
            {
                return BadRequest(new { error = "Invalid JSON data" });
            }
            
            // 保存到数据库
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
