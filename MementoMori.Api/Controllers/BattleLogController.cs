using Microsoft.AspNetCore.Mvc;
using MementoMori.Api.Infrastructure.Database;
using MementoMori.Api.Services;

namespace MementoMori.Api.Controllers;

/// <summary>
/// 战斗日志 API 控制器
/// </summary>
[ApiController]
[Route("api/battle-logs")]
public class BattleLogController : ControllerBase
{
    private readonly BattleLogService _battleLogService;
    private readonly ILogger<BattleLogController> _logger;

    public BattleLogController(BattleLogService battleLogService, ILogger<BattleLogController> logger)
    {
        _battleLogService = battleLogService;
        _logger = logger;
    }

    /// <summary>
    /// 获取当前用户的战斗日志列表
    /// </summary>
    /// <param name="battleType">战斗类型筛选 (可选)</param>
    /// <param name="startDate">开始日期 (可选)</param>
    /// <param name="endDate">结束日期 (可选)</param>
    /// <param name="page">页码 (默认 1)</param>
    /// <param name="pageSize">每页数量 (默认 20)</param>
    [HttpGet]
    public async Task<IActionResult> GetLogs(
        [FromQuery] int? battleType = null,
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        try
        {
            if (!HttpContext.Items.TryGetValue("UserId", out var userIdObj) || userIdObj is not long userId)
            {
                return Unauthorized(new { error = "User ID not found in request" });
            }

            page = Math.Max(1, page);
            pageSize = Math.Clamp(pageSize, 1, 100);

            var logs = await _battleLogService.GetUserBattleLogsAsync(
                userId, battleType, startDate, endDate, page, pageSize);

            var total = await _battleLogService.GetUserBattleLogCountAsync(
                userId, battleType, startDate, endDate);

            return Ok(new
            {
                data = logs.Select(l => new
                {
                    l.BattleToken,
                    l.CreatedAt,
                    l.BattleType,
                    l.QuestId,
                    l.IsWin,
                    l.EndTurn,
                    l.BattleDurationMs,
                    l.ApiUri
                }),
                pagination = new
                {
                    page,
                    pageSize,
                    total,
                    totalPages = (int)Math.Ceiling((double)total / pageSize)
                }
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "获取战斗日志列表失败");
            return StatusCode(500, new { error = "Internal server error", details = ex.Message });
        }
    }

    /// <summary>
    /// 获取单条战斗日志详情
    /// </summary>
    /// <param name="battleToken">战斗 Token</param>
    [HttpGet("{battleToken}")]
    public async Task<IActionResult> GetDetail(string battleToken)
    {
        try
        {
            if (!HttpContext.Items.TryGetValue("UserId", out var userIdObj) || userIdObj is not long userId)
            {
                return Unauthorized(new { error = "User ID not found in request" });
            }

            var log = await _battleLogService.GetByTokenAsync(battleToken, userId);
            if (log == null)
            {
                return NotFound(new { error = "Battle log not found" });
            }

            return Ok(new
            {
                log.BattleToken,
                log.CreatedAt,
                log.BattleType,
                log.QuestId,
                log.IsWin,
                log.EndTurn,
                log.BattleDurationMs,
                log.ApiUri,
                log.BattleDataJson
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "获取战斗日志详情失败: {BattleToken}", battleToken);
            return StatusCode(500, new { error = "Internal server error", details = ex.Message });
        }
    }

    /// <summary>
    /// 删除战斗日志
    /// </summary>
    /// <param name="battleToken">战斗 Token</param>
    [HttpDelete("{battleToken}")]
    public async Task<IActionResult> Delete(string battleToken)
    {
        try
        {
            if (!HttpContext.Items.TryGetValue("UserId", out var userIdObj) || userIdObj is not long userId)
            {
                return Unauthorized(new { error = "User ID not found in request" });
            }

            var success = await _battleLogService.DeleteAsync(battleToken, userId);
            if (!success)
            {
                return NotFound(new { error = "Battle log not found" });
            }

            return Ok(new { message = "Battle log deleted successfully" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "删除战斗日志失败: {BattleToken}", battleToken);
            return StatusCode(500, new { error = "Internal server error", details = ex.Message });
        }
    }
}
