using Microsoft.AspNetCore.Mvc;
using MementoMori.Api.Extensions;
using MementoMori.Api.Services;
using MementoMori.Api.Utils;
using MementoMori.Ortega.Share.Data.Character;

namespace MementoMori.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[AutoConstructor]
public partial class CharacterController : ControllerBase
{
    private readonly AccountManager _accountManager;
    private readonly ILogger<CharacterController> _logger;

    /// <summary>
    /// 获取角色计算后的详细能力信息（与 Blazor Character.razor 使用逻辑一致）
    /// </summary>
    [HttpGet("{userCharacterGuid}/calculated-detail")]
    public async Task<ActionResult<CharacterDetailInfo>> GetCalculatedDetail(string userCharacterGuid)
    {
        if (!HttpContext.Items.TryGetValue("UserId", out var userIdObj) || userIdObj is not long userId)
        {
            return Unauthorized(new { error = "User ID not found in request" });
        }

        try
        {
            var accountContext = await _accountManager.GetOrCreateAsync(userId);
            var nm = accountContext.NetworkManager;
            var syncData = nm.UserSyncData;

            var userCharacterInfo = syncData.GetUserCharacterInfoByUserCharacterGuid(userCharacterGuid);
            if (userCharacterInfo == null)
            {
                return NotFound(new { error = $"Character {userCharacterGuid} not found" });
            }

            var (baseParameter, battleParameter) = BattlePowerCalculatorUtil.CalcCharacterBattleParameter(nm, userCharacterInfo);
            var battlePower = BattlePowerCalculatorUtil.GetUserCharacterBattlePower(nm, userCharacterInfo);
            var userEquipmentDtoInfos = syncData.GetUserEquipmentDtoInfosByCharacterGuid(userCharacterInfo.Guid);

            var detail = new CharacterDetailInfo
            {
                UserEquipmentDtoInfos = userEquipmentDtoInfos,
                BaseParameter = baseParameter,
                BattleParameter = battleParameter,
                BattlePower = battlePower,
                Level = userCharacterInfo.Level,
                RarityFlags = userCharacterInfo.RarityFlags
            };

            return Ok(detail);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to calculate character detail for {Guid}", userCharacterGuid);
            return StatusCode(500, new { error = "Failed to calculate character detail", detail = ex.Message });
        }
    }
}
