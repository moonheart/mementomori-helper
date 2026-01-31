using Microsoft.AspNetCore.Mvc;
using MementoMori.Api.Models;
using MementoMori.Api.Models.DTOs;
using MementoMori.Api.Services;
using MementoMori.Api.Utils;
using MementoMori.Ortega.Share.Data.ApiInterface.BountyQuest;
using MementoMori.Ortega.Share.Data.BountyQuest;

namespace MementoMori.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[AutoConstructor]
public partial class BountyQuestController : ControllerBase
{
    private readonly ILogger<BountyQuestController> _logger;
    private readonly AccountManager _accountManager;
    private readonly PlayerSettingService _settingService;

    /// <summary>
    /// 派遣悬赏任务（支持单体或按类型批量）
    /// </summary>
    [HttpPost("dispatch")]
    public async Task<IActionResult> Dispatch([FromBody] CalculateBountyQuestFormationRequest request)
    {
        try
        {
            // 1. 获取用户上下文
            if (!HttpContext.Items.TryGetValue("UserId", out var userIdObj) || userIdObj is not long userId)
            {
                return Unauthorized(new { error = "User ID not found in request" });
            }

            var context = await _accountManager.GetOrCreateAsync(userId);
            var nm = context.NetworkManager;

            // 2. 获取当前悬赏列表和玩家数据
            var getListResponse = await nm.SendRequest<GetListRequest, GetListResponse>(new GetListRequest(), false);
            var userSyncData = nm.UserSyncData;
            var bountyQuestAuto = await _settingService.GetSettingAsync<GameConfig.BountyQuestAutoConfig>(userId, "bountyquestauto") 
                                 ?? new GameConfig.BountyQuestAutoConfig();

            // 3. 筛选需要派遣的任务
            var questsToCalculate = getListResponse.BountyQuestInfos.AsEnumerable();
            if (request.BountyQuestId.HasValue)
            {
                questsToCalculate = questsToCalculate.Where(q => q.BountyQuestId == request.BountyQuestId.Value);
            }
            else if (request.BountyQuestType.HasValue)
            {
                questsToCalculate = questsToCalculate.Where(q => q.BountyQuestType == request.BountyQuestType.Value);
            }
            else
            {
                return BadRequest(new { error = "Either BountyQuestId or BountyQuestType must be provided" });
            }

            // 排除已经在进行中的任务
            var ongoingQuestIds = getListResponse.UserBountyQuestDtoInfos
                .Where(d => d.BountyQuestEndTime > 0)
                .Select(d => d.BountyQuestId)
                .ToHashSet();
            
            var targetQuests = questsToCalculate.Where(q => !ongoingQuestIds.Contains(q.BountyQuestId)).ToList();
            
            if (targetQuests.Count == 0)
            {
                return Ok(new { successCount = 0, message = "没有待派遣的任务" });
            }

            // 4. 调用计算逻辑
            // 临时替换 getListResponse 中的任务列表，以便复用 CalcAutoFormation
            var originalInfos = getListResponse.BountyQuestInfos;
            getListResponse.BountyQuestInfos = targetQuests;

            try
            {
                var formations = BountyQuestAutoFormationUtil.CalcAutoFormation(getListResponse, userSyncData, bountyQuestAuto, force: true);
                
                int successCount = 0;
                var results = new List<object>();

                foreach (var formation in formations)
                {
                    try
                    {
                        var startResponse = await nm.SendRequest<StartRequest, StartResponse>(new StartRequest
                        {
                            BountyQuestStartInfos = new List<BountyQuestStartInfo> { formation }
                        }, false);
                        
                        successCount++;
                        results.Add(new { questId = formation.BountyQuestId, success = true });
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to dispatch quest {QuestId}", formation.BountyQuestId);
                        results.Add(new { questId = formation.BountyQuestId, success = false, error = ex.Message });
                    }
                }

                return Ok(new 
                { 
                    successCount, 
                    totalAttempted = formations.Count,
                    totalRequested = targetQuests.Count,
                    results 
                });
            }
            finally
            {
                getListResponse.BountyQuestInfos = originalInfos;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in BountyQuest Dispatch");
            return StatusCode(500, new { error = "Internal server error", message = ex.Message });
        }
    }
}
