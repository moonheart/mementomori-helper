using MementoMori.Api.Infrastructure;
using MementoMori.Api.Services;
using MementoMori.Ortega.Share;
using MementoMori.Ortega.Share.Data.ApiInterface.Equipment;

namespace MementoMori.Api.Handlers;

/// <summary>
/// 装备强化处理器
/// </summary>
[RegisterTransient]
[AutoConstructor]
public partial class EquipmentReinforcementHandler : IGameActionHandler
{
    private readonly ILogger<EquipmentReinforcementHandler> _logger;
    private readonly JobLogger _jobLogger;
    private readonly PlayerSettingService _playerSettingService;

    public string ActionName => "装备强化";

    public async Task ExecuteAsync(AccountContext context)
    {
        var userId = context.AccountInfo.UserId;
        var nm = context.NetworkManager;

        await _jobLogger.LogAsync(userId, "正在检查装备强化...");

        try
        {
            var autoJobSettings = await _playerSettingService.GetAutoJobSettingsAsync(userId);
            if (!autoJobSettings.AutoReinforcementEquipmentOneTime)
            {
                await _jobLogger.LogAsync(userId, "装备强化未开启，跳过。");
                return;
            }

            // 找到强化等级最低且已装备的装备
            var equipmentDtoInfo = nm.UserSyncData.UserEquipmentDtoInfos
                .Where(d => !string.IsNullOrEmpty(d.CharacterGuid))
                .OrderBy(d => d.ReinforcementLv)
                .FirstOrDefault(d => Masters.EquipmentTable.GetById(d.EquipmentId).EquipmentLv > d.ReinforcementLv);

            if (equipmentDtoInfo != null)
            {
                var response = await nm.SendRequest<ReinforcementRequest, ReinforcementResponse>(
                    new ReinforcementRequest { EquipmentGuid = equipmentDtoInfo.Guid, NumberOfTimes = 1 }, false);
                await _jobLogger.LogAsync(userId, "装备强化完成。");
            }
            else
            {
                await _jobLogger.LogAsync(userId, "暂无可强化的装备。");
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "装备强化失败 for user {UserId}", userId);
        }
    }
}
