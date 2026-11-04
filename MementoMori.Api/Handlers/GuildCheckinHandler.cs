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

    public string ActionName => Text.T("ActionGuildCheckin");

    public async Task ExecuteAsync(AccountContext context)
    {
        var userId = context.AccountInfo.UserId;
        var nm = context.NetworkManager;

        await _jobLogger.LogAsync(userId, Text.T("GuildCheckingIn"));

        try
        {
            var guildIdResponse = await nm.SendRequest<GetGuildIdRequest, GetGuildIdResponse>(new GetGuildIdRequest());

            if (guildIdResponse.GuildId == 0)
            {
                await _jobLogger.LogAsync(userId, Text.T("GuildNotJoinedSkipCheckin"));
                return;
            }

            var guildInfoResponse = await nm.SendRequest<GetGuildBaseInfoRequest, GetGuildBaseInfoResponse>(
                new GetGuildBaseInfoRequest { BelongGuildId = guildIdResponse.GuildId });

            await _jobLogger.LogAsync(userId, Text.T("GuildCheckinDone", guildIdResponse.GuildId));
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "公会签到失败 for user {UserId}", userId);
        }
    }
}
