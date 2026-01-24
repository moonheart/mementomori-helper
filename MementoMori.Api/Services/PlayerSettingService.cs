using MementoMori.Api.Infrastructure.Database;
using Newtonsoft.Json;

namespace MementoMori.Api.Services;

/// <summary>
/// 玩家设置服务 - 处理按子类型分开存储的持久化设置
/// </summary>
public class PlayerSettingService
{
    private readonly IFreeSql _fsql;
    private readonly ILogger<PlayerSettingService> _logger;

    public PlayerSettingService(IFreeSql fsql, ILogger<PlayerSettingService> logger)
    {
        _fsql = fsql;
        _logger = logger;
    }

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
            e => (object?)e.JsonValue // 返回原始 JSON，由控制器或调用者决定如何解析
        );
    }
}
