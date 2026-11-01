using MementoMori.Api.Infrastructure;
using MementoMori.Api.Services;
using MementoMori.Ortega.Share.Data.ApiInterface.BountyQuest;

namespace MementoMori.Api.Handlers;

/// <summary>
/// 手动刷新悬赏任务 (manual tool, old RemakeBountyRequest): rerolls the list once when something is still undispatched.
/// </summary>
[RegisterTransient]
[AutoConstructor]
public partial class BountyQuestRemakeHandler : IGameActionHandler
{
    private readonly ILogger<BountyQuestRemakeHandler> _logger;
    private readonly JobLogger _jobLogger;

    public string ActionName => Text.T("ActionBountyQuestRemake");

    public async Task ExecuteAsync(AccountContext context)
    {
        var userId = context.AccountInfo.UserId;
        var nm = context.NetworkManager;
        try
        {
            // The list is null until the Fountain has been opened once, so ask for it first.
            var list = await nm.SendRequest<GetListRequest, GetListResponse>(new GetListRequest());
            if (list.UserBountyQuestDtoInfos?.Any(d => d.BountyQuestEndTime == 0) != true)
            {
                await _jobLogger.LogAsync(userId, Text.T("BountyRemakeNothing"));
                return;
            }

            await nm.SendRequest<RemakeRequest, RemakeResponse>(new RemakeRequest());
            await nm.SendRequest<GetListRequest, GetListResponse>(new GetListRequest());
            await _jobLogger.LogAsync(userId, Text.T("BountyRemakeDone"));
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Bounty remake failed for user {UserId}", userId);
            await _jobLogger.LogAsync(userId, $"{ActionName}: {ex.Message}", "Error");
        }
    }
}
