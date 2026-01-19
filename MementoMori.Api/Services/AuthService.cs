using MementoMori.Api.Infrastructure;
using MementoMori.Ortega.Share.Data.ApiInterface.Auth;
using MementoMori.Ortega.Share.Data.Auth;

namespace MementoMori.Api.Services;

/// <summary>
/// 认证服务 - 使用 NetworkManager
/// </summary>
public class AuthService : IDisposable
{
    private readonly ILogger<AuthService> _logger;
    private readonly NetworkManager _networkManager;

    public AuthService(ILogger<AuthService> logger, NetworkManager networkManager)
    {
        _logger = logger;
        _networkManager = networkManager;
    }

    /// <summary>
    /// 获取玩家数据列表
    /// </summary>
    public async Task<List<PlayerDataInfo>> GetPlayerDataAsync(long userId, string clientKey)
    {
        try
        {
            var request = new LoginRequest
            {
                UserId = userId,
                ClientKey = clientKey
            };

            _logger.LogInformation("Getting player data for user {UserId}", userId);
            
            var response = await _networkManager.SendRequest<LoginRequest, LoginResponse>(request, useAuthApi: true);
            
            if (response?.PlayerDataInfoList != null)
            {
                _logger.LogInformation("Found {Count} worlds", response.PlayerDataInfoList.Count);
                return response.PlayerDataInfoList.ToList();
            }

            return new List<PlayerDataInfo>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get player data");
            throw;
        }
    }

    public void Dispose()
    {
        // NetworkManager 由 DI 管理
    }
}
