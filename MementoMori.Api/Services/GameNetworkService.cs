using MementoMori.Api.Models;

namespace MementoMori.Api.Services;

/// <summary>
/// 游戏网络服务 - 简化版，暂时使用占位逻辑
/// 后续可以逐步集成 MementoMori.Ortega
/// </summary>
[RegisterScoped]
[AutoConstructor]
public partial class GameNetworkService
{
    private readonly ILogger<GameNetworkService> _logger;
    private readonly IHttpClientFactory _httpClientFactory;

    /// <summary>
    /// 获取玩家数据信息（可用世界列表）- 占位实现
    /// TODO: 集成真实的 Ortega API
    /// </summary>
    public async Task<List<WorldDto>> GetPlayerWorldsAsync(long userId)
    {
        try
        {
            // TODO: 实际调用 Auth API
            await Task.Delay(100); // 模拟网络延迟
            
            return new List<WorldDto>
            {
                new WorldDto
                {
                    WorldId = 1,
                    WorldName = "World 1",
                    LastLoginTime = DateTimeOffset.Now.ToUnixTimeSeconds()
                }
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get player worlds for user {UserId}", userId);
            throw;
        }
    }

    /// <summary>
    /// 登录到指定世界 - 占位实现
    /// TODO: 集成真实的 Ortega API
    /// </summary>
    public async Task<bool> LoginToWorldAsync(
        long userId, 
        string clientKey, 
        long worldId)
    {
        try
        {
            // TODO: 实际调用 Auth API
            await Task.Delay(100); // 模拟网络延迟
            
            _logger.LogInformation("Login successful for user {UserId} to world {WorldId}", userId, worldId);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to login for user {UserId} to world {WorldId}", userId, worldId);
            return false;
        }
    }
}
