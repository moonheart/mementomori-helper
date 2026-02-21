using MementoMori.Api.Extensions;
using MementoMori.Api.Infrastructure;
using MementoMori.Api.Models;
using MementoMori.Api.Services;
using MementoMori.Ortega.Share.Data.ApiInterface.Guild;
using MementoMori.Ortega.Share.Data.ApiInterface.GuildRaid;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Extensions;

namespace MementoMori.Api.Handlers;

/// <summary>
/// 自动开启公会副本处理器
/// </summary>
[RegisterTransient]
[AutoConstructor]
public partial class GuildRaidOpenHandler : IGameActionHandler
{
    private readonly ILogger<GuildRaidOpenHandler> _logger;
    private readonly JobLogger _jobLogger;
    private readonly IServiceProvider _serviceProvider;

    public string ActionName => "自动开启公会副本";

    public async Task ExecuteAsync(AccountContext context)
    {
        var userId = context.AccountInfo.UserId;
        var nm = context.NetworkManager;

        // 获取配置
        using var scope = _serviceProvider.CreateScope();
        var settingService = scope.ServiceProvider.GetRequiredService<PlayerSettingService>();
        var autoJobConfig = await settingService.GetSettingAsync<GameConfig.AutoJobModel>(userId, SettingKeys.AutoJob);

        if (autoJobConfig == null || !autoJobConfig.AutoOpenGuildRaid)
        {
            _logger.LogDebug("User {UserId} has not enabled auto open guild raid, skipping.", userId);
            return;
        }

        _logger.LogInformation("Checking guild raid open for user {UserId}", userId);

        // 1. 获取公会 ID
        var guildIdResp = await nm.SendRequest<GetGuildIdRequest, GetGuildIdResponse>(new GetGuildIdRequest());
        if (guildIdResp.GuildId == 0)
        {
            _logger.LogDebug("User {UserId} is not in a guild, skipping.", userId);
            return;
        }

        // 2. 检查权限
        var guildMemberResp = await nm.SendRequest<GetGuildMemberInfoRequest, GetGuildMemberInfoResponse>(
            new GetGuildMemberInfoRequest { GuildId = guildIdResp.GuildId });
        
        var playerInfo = guildMemberResp.PlayerInfoList?.FirstOrDefault(d => d.PlayerId == nm.PlayerId);
        if (playerInfo == null || 
            playerInfo.PlayerGuildPositionType == PlayerGuildPositionType.Member || 
            playerInfo.PlayerGuildPositionType == PlayerGuildPositionType.None)
        {
            _logger.LogInformation("User {UserId} does not have permission to open guild raid.", userId);
            return;
        }

        // 3. 检查副本状态
        var raidInfoResp = await nm.SendRequest<GetGuildRaidInfoRequest, GetGuildRaidInfoResponse>(
            new GetGuildRaidInfoRequest { BelongGuildId = guildIdResp.GuildId });
        
        var releasableRaid = raidInfoResp.GuildRaidInfos?.FirstOrDefault(d => d.GuildRaidDtoInfo.BossType == GuildRaidBossType.Releasable);
        if (releasableRaid == null)
        {
            _logger.LogWarning("Releasable guild raid info not found for user {UserId}.", userId);
            return;
        }

        if (releasableRaid.IsOpen)
        {
            _logger.LogDebug("Guild raid is already open for user {UserId}.", userId);
            return;
        }

        // 4. 执行开启
        try
        {
            await nm.SendRequest<OpenGuildRaidRequest, OpenGuildRaidResponse>(
                new OpenGuildRaidRequest 
                { 
                    BelongGuildId = guildIdResp.GuildId, 
                    GuildRaidBossType = GuildRaidBossType.Releasable 
                });

            await _jobLogger.LogAsync(userId, "成功开启公会副本 (消耗公会点数)。");
            _logger.LogInformation("Successfully opened guild raid for user {UserId}, GuildId: {GuildId}", userId, guildIdResp.GuildId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to open guild raid for user {UserId}", userId);
            await _jobLogger.LogAsync(userId, $"开启公会副本失败: {ex.Message}", "Error");
        }
    }
}
