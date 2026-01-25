using MementoMori.Api.Models;
using MementoMori.Ortega.Share.Data.ApiInterface.Mission;
using MementoMori.Ortega.Share.Enums;

namespace MementoMori.Api.Services;

/// <summary>
/// 任务服务 - 处理任务相关的业务逻辑
/// </summary>
[RegisterScoped]
[AutoConstructor]
public partial class MissionService
{
    private readonly ILogger<MissionService> _logger;
    private readonly AccountManager _accountManager;

    /// <summary>
    /// 获取任务信息
    /// </summary>
    public async Task<ApiGetMissionInfoResponse> GetMissionInfoAsync(long userId, List<MissionGroupType> missionGroupTypes)
    {
        try
        {
            // 获取账户上下文
            var context = await _accountManager.GetOrCreateAsync(userId);
            var networkManager = context.NetworkManager;

            _logger.LogInformation("Getting mission info for user {UserId}", userId);

            // 构造请求
            var request = new GetMissionInfoRequest
            {
                TargetMissionGroupList = missionGroupTypes
            };

            // 发送请求
            var response = await networkManager.SendRequest<GetMissionInfoRequest, GetMissionInfoResponse>(
                request, useAuthApi: false);

            _logger.LogInformation("Successfully retrieved mission info for user {UserId}", userId);

            return new ApiGetMissionInfoResponse
            {
                Success = true,
                Message = "获取任务信息成功",
                MissionInfoDict = response.MissionInfoDict
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get mission info for user {UserId}", userId);
            return new ApiGetMissionInfoResponse
            {
                Success = false,
                Message = $"获取任务信息失败: {ex.Message}"
            };
        }
    }

    /// <summary>
    /// 领取任务奖励
    /// </summary>
    public async Task<ApiClaimMissionResponse> ClaimMissionRewardsAsync(long userId, List<long> missionIds)
    {
        try
        {
            // 获取账户上下文
            var context = await _accountManager.GetOrCreateAsync(userId);
            var networkManager = context.NetworkManager;

            _logger.LogInformation("Claiming mission rewards for user {UserId}, mission IDs: {MissionIds}", 
                userId, string.Join(", ", missionIds));

            // 构造请求
            var request = new RewardMissionRequest
            {
                TargetMissionIdList = missionIds
            };

            // 发送请求
            var response = await networkManager.SendRequest<RewardMissionRequest, RewardMissionResponse>(
                request, useAuthApi: false);

            _logger.LogInformation("Successfully claimed mission rewards for user {UserId}", userId);

            return new ApiClaimMissionResponse
            {
                Success = true,
                Message = "领取任务奖励成功",
                RewardInfo = response.RewardInfo
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to claim mission rewards for user {UserId}", userId);
            return new ApiClaimMissionResponse
            {
                Success = false,
                Message = $"领取任务奖励失败: {ex.Message}"
            };
        }
    }

    /// <summary>
    /// 领取活跃度/功勋奖励
    /// </summary>
    public async Task<ApiClaimActivityRewardResponse> ClaimActivityRewardAsync(
        long userId, 
        MissionGroupType missionGroupType, 
        long requiredCount)
    {
        try
        {
            // 获取账户上下文
            var context = await _accountManager.GetOrCreateAsync(userId);
            var networkManager = context.NetworkManager;

            _logger.LogInformation("Claiming activity reward for user {UserId}, type: {Type}, count: {Count}", 
                userId, missionGroupType, requiredCount);

            // 构造请求
            var request = new RewardMissionActivityRequest
            {
                MissionGroupType = missionGroupType,
                RequiredCount = requiredCount
            };

            // 发送请求
            var response = await networkManager.SendRequest<RewardMissionActivityRequest, RewardMissionActivityResponse>(
                request, useAuthApi: false);

            _logger.LogInformation("Successfully claimed activity reward for user {UserId}", userId);

            return new ApiClaimActivityRewardResponse
            {
                Success = true,
                Message = "领取功勋奖励成功",
                RewardInfo = response.RewardInfo
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to claim activity reward for user {UserId}", userId);
            return new ApiClaimActivityRewardResponse
            {
                Success = false,
                Message = $"领取功勋奖励失败: {ex.Message}"
            };
        }
    }
}
