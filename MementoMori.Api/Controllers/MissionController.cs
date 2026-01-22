using Microsoft.AspNetCore.Mvc;
using MementoMori.Api.Models;
using MementoMori.Api.Services;
using MementoMori.Ortega.Share.Enums;

namespace MementoMori.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MissionController : ControllerBase
{
    private readonly ILogger<MissionController> _logger;
    private readonly MissionService _missionService;

    public MissionController(
        ILogger<MissionController> logger,
        MissionService missionService)
    {
        _logger = logger;
        _missionService = missionService;
    }

    /// <summary>
    /// 获取任务信息
    /// </summary>
    /// <param name="missionGroupTypes">任务组类型列表（可选，为空则获取所有）</param>
    [HttpGet("info")]
    public async Task<ActionResult<ApiGetMissionInfoResponse>> GetMissionInfo(
        [FromQuery] List<MissionGroupType>? missionGroupTypes = null)
    {
        try
        {
            // Get user ID from HttpContext (set by middleware)
            var userId = (long)HttpContext.Items["UserId"]!;
            
            // 如果未指定类型，默认获取主要任务类型
            var types = missionGroupTypes ?? new List<MissionGroupType>
            {
                MissionGroupType.Daily,
                MissionGroupType.Weekly,
                MissionGroupType.Main
            };

            var result = await _missionService.GetMissionInfoAsync(userId, types);
            
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get mission info for user {UserId}");
            return BadRequest(new ApiGetMissionInfoResponse
            {
                Success = false,
                Message = ex.Message
            });
        }
    }

    /// <summary>
    /// 领取任务奖励
    /// </summary>
    [HttpPost("claim")]
    public async Task<ActionResult<ApiClaimMissionResponse>> ClaimMissionRewards(
        [FromBody] ApiClaimMissionRequest request)
    {
        try
        {
            // Get user ID from HttpContext (set by middleware)
            var userId = (long)HttpContext.Items["UserId"]!;
            
            if (request.MissionIds == null || request.MissionIds.Count == 0)
            {
                return BadRequest(new ApiClaimMissionResponse
                {
                    Success = false,
                    Message = "任务ID列表不能为空"
                });
            }

            var result = await _missionService.ClaimMissionRewardsAsync(
                userId, 
                request.MissionIds);
            
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            var userId = HttpContext.Items["UserId"] as long? ?? 0;
            _logger.LogError(ex, "Failed to claim mission rewards for user {UserId}", userId);
            return BadRequest(new ApiClaimMissionResponse
            {
                Success = false,
                Message = ex.Message
            });
        }
    }

    /// <summary>
    /// 领取活跃度/功勋奖励
    /// </summary>
    [HttpPost("claim-activity")]
    public async Task<ActionResult<ApiClaimActivityRewardResponse>> ClaimActivityReward(
        [FromBody] ApiClaimActivityRewardRequest request)
    {
        try
        {
            // Get user ID from HttpContext (set by middleware)
            var userId = (long)HttpContext.Items["UserId"]!;
            
            var result = await _missionService.ClaimActivityRewardAsync(
                userId,
                request.MissionGroupType,
                request.RequiredCount);
            
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            var userId = HttpContext.Items["UserId"] as long? ?? 0;
            _logger.LogError(ex, "Failed to claim activity reward for user {UserId}", userId);
            return BadRequest(new ApiClaimActivityRewardResponse
            {
                Success = false,
                Message = ex.Message
            });
        }
    }
}
