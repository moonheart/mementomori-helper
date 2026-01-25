using MementoMori.Api.Infrastructure;
using MementoMori.Api.Models;
using MementoMori.Api.Services;
using MementoMori.Ortega.Share.Data.ApiInterface.Gacha;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Extensions;

namespace MementoMori.Api.Handlers;

/// <summary>
/// 自动更换圣装目标处理器
/// </summary>
[RegisterTransient]
[AutoConstructor]
public partial class GachaRelicChangeHandler : IGameActionHandler
{
    private readonly ILogger<GachaRelicChangeHandler> _logger;
    private readonly JobLogger _jobLogger;
    private readonly IServiceProvider _serviceProvider;

    public string ActionName => "自动更换圣装";

    public async Task ExecuteAsync(AccountContext context)
    {
        var userId = context.AccountInfo.UserId;
        var nm = context.NetworkManager;

        // 获取配置
        using var scope = _serviceProvider.CreateScope();
        var settingService = scope.ServiceProvider.GetRequiredService<PlayerSettingService>();
        var gachaConfig = await settingService.GetSettingAsync<GameConfig.GachaConfigModel>(userId, "GachaConfig");

        var targetRelicType = gachaConfig?.TargetRelicType ?? GachaRelicType.None;
        if (targetRelicType == GachaRelicType.None)
        {
            _logger.LogDebug("User {UserId} has no target relic type configured, skipping.", userId);
            return;
        }

        _logger.LogInformation("Checking gacha relic target for user {UserId}, target: {Target}", userId, targetRelicType);

        // 1. 获取卡池列表 (使用 false 表示不使用 Auth API)
        var listResp = await nm.SendRequest<GetListRequest, GetListResponse>(new GetListRequest(), false);
        
        // 2. 检查是否可以免费更换
        if (!listResp.IsFreeChangeRelicGacha)
        {
            _logger.LogDebug("User {UserId} cannot change relic gacha for free right now.", userId);
            return;
        }

        // 3. 找到目标卡池 (HolyAngel)
        var gachaCaseInfo = listResp.GachaCaseInfoList?.FirstOrDefault(d => d.GachaGroupType == GachaGroupType.HolyAngel);
        if (gachaCaseInfo == null)
        {
            _logger.LogWarning("HolyAngel gacha case not found for user {UserId}.", userId);
            return;
        }

        // 4. 检查是否需要更换
        if (gachaCaseInfo.GachaRelicType == targetRelicType)
        {
            _logger.LogDebug("User {UserId} already has the target relic type {Target} set.", userId, targetRelicType);
            return;
        }

        // 5. 检查保底进度 (保护玩家保底)
        if (gachaCaseInfo.GachaBonusDrawCount > 0)
        {
            _logger.LogInformation("User {UserId} has gacha bonus progress ({Count}/10), skipping auto-change to preserve progress.", 
                userId, gachaCaseInfo.GachaBonusDrawCount);
            await _jobLogger.LogAsync(userId, $"圣天使卡池已有保底进度 ({gachaCaseInfo.GachaBonusDrawCount}/10)，跳过自动更换以保留进度。");
            return;
        }

        // 6. 执行更换
        try
        {
            await nm.SendRequest<ChangeGachaRelicRequest, ChangeGachaRelicResponse>(
                new ChangeGachaRelicRequest { GachaRelicType = targetRelicType }, false);

            await _jobLogger.LogAsync(userId, $"成功将圣天使卡池目标更换为: {targetRelicType}");
            _logger.LogInformation("Successfully changed gacha relic target to {Target} for user {UserId}", targetRelicType, userId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to change gacha relic target for user {UserId}", userId);
            await _jobLogger.LogAsync(userId, $"更换圣装目标失败: {ex.Message}", "Error");
        }
    }
}
