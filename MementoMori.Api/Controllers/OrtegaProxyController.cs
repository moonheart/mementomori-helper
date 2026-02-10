using Microsoft.AspNetCore.Mvc;
using MementoMori.Api.Infrastructure;
using MementoMori.Api.Services;
using System.Text.Json;

namespace MementoMori.Api.Controllers
{
    /// <summary>
    /// Ortega API 通用代理控制器
    /// 动态处理所有 Ortega API 请求
    /// </summary>
    [ApiController]
    [Route("api/ortega")]
    [AutoConstructor]
    public partial class OrtegaProxyController : ControllerBase
    {
        private readonly OrtegaApiDiscoveryService _discoveryService;
        private readonly OrtegaInvoker _invoker;
        private readonly BattleLogService _battleLogService;
        private readonly ILogger<OrtegaProxyController> _logger;

        /// <summary>
        /// 通用 Ortega API 代理端点
        /// </summary>
        /// <param name="category">API 类别（如 user, mission, shop）</param>
        /// <param name="ortegaAction">API 操作（如 getUserData, getMissionInfo）</param>
        /// <returns>API 响应</returns>
        [HttpPost("{category}/{ortegaAction}")]
        public async Task<IActionResult> ProxyRequest(
            [FromRoute]string category, 
            [FromRoute]string ortegaAction)
        {
            try
            {
                // 构建 Ortega API URI
                var apiUri = $"{category}/{ortegaAction}";
                
                _logger.LogInformation("ProxyRequest received: Category={Category}, Action={Action}, URI={ApiUri}", category, ortegaAction, apiUri);

                // 查找 API 信息
                var apiInfo = _discoveryService.GetApiInfo(apiUri);
                if (apiInfo == null)
                {
                    _logger.LogWarning("API lookup failed for URI: {ApiUri}. Available APIs count: {Count}", apiUri, _discoveryService.GetAllApis().Count);
                    return NotFound(new {error = $"API '{apiUri}' not found"});
                }

                // 从 Header 获取 UserId（由 UserIdAuthenticationMiddleware 设置）
                if (!HttpContext.Items.TryGetValue("UserId", out var userIdObj) || userIdObj is not long userId)
                {
                    return Unauthorized(new {error = "User ID not found in request"});
                }

                // 读取请求体
                string requestJson;
                using (var reader = new StreamReader(Request.Body))
                {
                    requestJson = await reader.ReadToEndAsync();
                    if (string.IsNullOrWhiteSpace(requestJson))
                    {
                        requestJson = "{}"; // 空请求体
                    }
                }

                _logger.LogInformation("Proxying request to {ApiUri} for user {UserId}", apiUri, userId);

                // 调用 Ortega API
                var response = await _invoker.InvokeAsync(userId, requestJson, apiInfo.RequestType, apiInfo.ResponseType);

                // 尝试保存战斗日志 (失败不影响主流程)
                _ = Task.Run(async () =>
                {
                    try
                    {
                        await _battleLogService.TrySaveBattleLogAsync(userId, apiUri, response);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "保存战斗日志时发生错误");
                    }
                });

                // 返回响应
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Invalid operation: {Message}", ex.Message);
                return BadRequest(new {error = ex.Message});
            }
            catch (NetworkManager.ApiErrorException ex)
            {
                _logger.LogError(ex, "Ortega API error: {ErrorCode}", ex.ErrorCode);
                return BadRequest(new
                {
                    error = "Ortega API error",
                    errorCode = ex.ErrorCode,
                    category,
                    ortegaAction
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while proxying request to {Category}/{Action}", category, ortegaAction);
                return StatusCode(500, new {error = "Internal server error", details = ex.Message});
            }
        }

        /// <summary>
        /// 获取所有可用的 Ortega API 列表
        /// </summary>
        /// <returns>API 列表</returns>
        [HttpGet("list")]
        public IActionResult ListApis()
        {
            var apis = _discoveryService.GetAllApis()
                .Select(kv => new
                {
                    uri = kv.Key,
                    requestType = kv.Value.RequestType.Name,
                    responseType = kv.Value.ResponseType.Name,
                    isRequiredLogin = kv.Value.IsRequiredLogin,
                    isIgnoreMaintenance = kv.Value.IsIgnoreMaintenance
                })
                .OrderBy(a => a.uri)
                .ToList();

            return Ok(new
            {
                total = apis.Count,
                apis
            });
        }


        /// <summary>
        /// 调试 API 发现逻辑
        /// </summary>
        [HttpGet("debug")]
        public IActionResult ListDebugInfo()
        {
            var logs = _discoveryService.DebugDiscovery();
            return Ok(logs);
        }
    }
}
