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

    public string ActionName => "批量转移友情点";

    public async Task ExecuteAsync(AccountContext context)
    {
        var userId = context.AccountInfo.UserId;
        var nm = context.NetworkManager;

        await _jobLogger.LogAsync(userId, "正在批量转移友情点...");

        try
        {
            await nm.SendRequest<BulkTransferFriendPointRequest, BulkTransferFriendPointResponse>(
                new BulkTransferFriendPointRequest(), false);
            await _jobLogger.LogAsync(userId, "友情点批量转移完成。");
        }
        catch (Exception ex) when (ex.Message.Contains("AlreadyMaxReceived"))
        {
            await _jobLogger.LogAsync(userId, "今日友情点已达上限。");
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "友情点转移失败 for user {UserId}", userId);
        }
    }
}
