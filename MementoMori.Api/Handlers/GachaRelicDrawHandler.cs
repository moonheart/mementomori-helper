using MementoMori.Api.Extensions;
using MementoMori.Api.Infrastructure;
using MementoMori.Api.Models;
using MementoMori.Api.Services;
using MementoMori.Ortega.Share.Data.ApiInterface.Gacha;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Extensions;

namespace MementoMori.Api.Handlers;

/// <summary>
/// 自动抽取圣装处理器
/// </summary>
[RegisterTransient]
[AutoConstructor]
public partial class GachaRelicDrawHandler : IGameActionHandler
{
    private readonly ILogger<GachaRelicDrawHandler> _logger;
    private readonly JobLogger _jobLogger;
    private readonly IServiceProvider _serviceProvider;

    public string ActionName => "自动抽取圣装";

    public async Task ExecuteAsync(AccountContext context)
    {
        var userId = context.AccountInfo.UserId;
        var nm = context.NetworkManager;

        // 获取配置
        using var scope = _serviceProvider.CreateScope();
        var settingService = scope.ServiceProvider.GetRequiredService<PlayerSettingService>();
        var gachaConfig = await settingService.GetSettingAsync<GameConfig.GachaConfigModel>(userId, "GachaConfig");

        if (gachaConfig == null || !gachaConfig.AutoGachaRelic)
        {
            _logger.LogDebug("User {UserId} has not enabled auto gacha relic, skipping.", userId);
            return;
        }

        _logger.LogInformation("Checking gacha relic draw for user {UserId}", userId);

        // 1. 获取卡池列表
        var listResp = await nm.SendRequest<GetListRequest, GetListResponse>(new GetListRequest());
        
        // 2. 找到目标卡池 (HolyAngel)
        var gachaCaseInfo = listResp.GachaCaseInfoList?.FirstOrDefault(d => d.GachaGroupType == GachaGroupType.HolyAngel);
        if (gachaCaseInfo == null)
        {
            _logger.LogWarning("HolyAngel gacha case not found for user {UserId}.", userId);
            return;
        }

        // 3. 检查保底进度 (如果已经满 10 次则不再抽)
        if (gachaCaseInfo.GachaBonusDrawCount >= 10)
        {
            _logger.LogInformation("User {UserId} already completed gacha bonus (10/10), skipping.", userId);
            return;
        }

        // 4. 循环抽取 (原逻辑尝试最多 3 次)
        int drawCount = 0;
        while (drawCount < 3 && gachaCaseInfo.GachaBonusDrawCount < 10)
        {
            // 查找单抽按钮 (300 钻)
            var buttonInfo = gachaCaseInfo.GachaButtonInfoList?.FirstOrDefault(d => 
                d.ConsumeUserItem != null && 
                d.ConsumeUserItem.ItemType == ItemType.CurrencyFree && 
                d.ConsumeUserItem.ItemCount == 300);

            if (buttonInfo == null)
            {
                _logger.LogWarning("300 diamond gacha button not found for user {UserId}.", userId);
                break;
            }

            // 检查余额
            var currentCurrency = nm.UserSyncData.GetUserItemCount(ItemType.CurrencyFree, 1, true);
            if (currentCurrency < 300)
            {
                _logger.LogInformation("User {UserId} has insufficient diamonds ({Current}/300).", userId, currentCurrency);
                await _jobLogger.LogAsync(userId, "钻石不足，停止自动抽取圣装。");
                break;
            }

            // 执行抽取
            try
            {
                var drawResp = await nm.SendRequest<DrawRequest, DrawResponse>(new DrawRequest { GachaButtonId = buttonInfo.GachaButtonId });
                drawCount++;
                
                // 更新进度信息（从响应中获取最新的卡池信息）
                gachaCaseInfo = drawResp.GachaCaseInfoList?.FirstOrDefault(d => d.GachaGroupType == GachaGroupType.HolyAngel) ?? gachaCaseInfo;

                await _jobLogger.LogAsync(userId, $"成功抽取圣天使卡池一次。当前保底进度: {gachaCaseInfo.GachaBonusDrawCount}/10");
                _logger.LogInformation("Successfully drew gacha for user {UserId}, count: {Count}, new progress: {Progress}", 
                    userId, drawCount, gachaCaseInfo.GachaBonusDrawCount);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to draw gacha for user {UserId}", userId);
                await _jobLogger.LogAsync(userId, $"抽取圣装失败: {ex.Message}", "Error");
                break;
            }
        }
    }
}
