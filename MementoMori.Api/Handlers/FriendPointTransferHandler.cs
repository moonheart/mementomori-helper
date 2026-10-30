using MementoMori.Api.Infrastructure;
using MementoMori.Api.Services;
using MementoMori.Ortega.Share.Data.ApiInterface.Friend;

namespace MementoMori.Api.Handlers;

/// <summary>
/// 友情点批量转移处理器
/// </summary>
[RegisterTransient]
[AutoConstructor]
public partial class FriendPointTransferHandler : IGameActionHandler
{
    private readonly ILogger<FriendPointTransferHandler> _logger;
    private readonly JobLogger _jobLogger;

    public string ActionName => Text.T("ActionFriendPointTransfer");

    public async Task ExecuteAsync(AccountContext context)
    {
        var userId = context.AccountInfo.UserId;
        var nm = context.NetworkManager;

        await _jobLogger.LogAsync(userId, Text.T("FriendPointTransferring"));

        try
        {
            await nm.SendRequest<BulkTransferFriendPointRequest, BulkTransferFriendPointResponse>(
                new BulkTransferFriendPointRequest());
            await _jobLogger.LogAsync(userId, Text.T("FriendPointTransferDone"));
        }
        catch (Exception ex) when (ex.Message.Contains("AlreadyMaxReceived"))
        {
            await _jobLogger.LogAsync(userId, Text.T("FriendPointLimitReached"));
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "友情点转移失败 for user {UserId}", userId);
        }
    }
}
