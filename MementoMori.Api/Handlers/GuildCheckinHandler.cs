using MementoMori.Api.Infrastructure;
using MementoMori.Api.Services;
using MementoMori.Ortega.Share.Data.ApiInterface.Guild;

namespace MementoMori.Api.Handlers;

/// <summary>
/// 公会签到处理器
/// </summary>
[RegisterTransient]
[AutoConstructor]
public partial class GuildCheckinHandler : IGameActionHandler
{
    private readonly ILogger<GuildCheckinHandler> _logger;
    private readonly JobLogger _jobLogger;

    public string ActionName => "公会签到";

    public async Task ExecuteAsync(AccountContext context)
    {
        var userId = context.AccountInfo.UserId;
        var nm = context.NetworkManager;

        await _jobLogger.LogAsync(userId, "正在执行公会签到...");

        try
        {
            var guildIdResponse = await nm.SendRequest<GetGuildIdRequest, GetGuildIdResponse>(new GetGuildIdRequest(), false);

            if (guildIdResponse.GuildId == 0)
            {
                await _jobLogger.LogAsync(userId, "未加入公会，跳过签到。");
                return;
            }

            var guildInfoResponse = await nm.SendRequest<GetGuildBaseInfoRequest, GetGuildBaseInfoResponse>(
                new GetGuildBaseInfoRequest { BelongGuildId = guildIdResponse.GuildId }, false);

            await _jobLogger.LogAsync(userId, $"公会签到完成，公会ID: {guildIdResponse.GuildId}。");
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "公会签到失败 for user {UserId}", userId);
        }
    }
}
