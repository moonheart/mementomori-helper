using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using MementoMori.Api.Services;
using MementoMori;

namespace MementoMori.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SettingsController : ControllerBase
{
    private readonly PlayerSettingService _settingService;
    private readonly ILogger<SettingsController> _logger;

    public SettingsController(PlayerSettingService settingService, ILogger<SettingsController> logger)
    {
        _settingService = settingService;
        _logger = logger;
    }

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
        // 根据 key 选择对应的类型。这里可以根据需要添加更多类型。
        // 目前参考 GameConfig.cs 中的主要子类
        object? result = key.ToLower() switch
        {
            "autojob" => await _settingService.GetSettingAsync<GameConfig.AutoJobModel>(userId, key),
            "shop" => await _settingService.GetSettingAsync<GameConfig.ShopConfig>(userId, key),
            "dungeonbattle" => await _settingService.GetSettingAsync<GameConfig.DungeonBattleConfig>(userId, key),
            "items" => await _settingService.GetSettingAsync<GameConfig.ItemsConfig>(userId, key),
            _ => await _settingService.GetSettingAsync<object>(userId, key)
        };

        return Ok(result);
    }

    /// <summary>
    /// 保存玩家特定的配置子项
    /// </summary>
    [HttpPost("{userId}/{key}")]
    public async Task<IActionResult> SaveSetting(long userId, string key, [FromBody] object value)
    {
        try
        {
            // 注意：这里传入的是 object，SaveSettingAsync 内部会将其序列化为 JSON
            await _settingService.SaveSettingAsync(userId, key, value);
            return Ok(new { success = true });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to save setting {Key} for user {UserId}", key, userId);
            return BadRequest(new { error = ex.Message });
        }
    }
}
