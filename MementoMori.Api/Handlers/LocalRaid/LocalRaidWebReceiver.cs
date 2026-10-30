using Microsoft.AspNetCore.SignalR;
using MementoMori.Api.Infrastructure;
using MementoMori.Api.Models;
using MementoMori.Api.Services;
using MementoMori.Ortega.Common.Enums;
using MementoMori.Ortega.Network.MagicOnion.Interface;
using MementoMori.Ortega.Share;
using MementoMori.Ortega.Share.MagicOnionShare.Response;
using MementoMori.Ortega.Share.Master;

namespace MementoMori.Api.Handlers.LocalRaid;

/// <summary>
/// Web 端的 LocalRaid 接收器
/// 将 MagicOnion 事件转发到 SignalR
/// 实现 IMagicOnionLocalRaidNotificaiton（构造函数需要）和 IMagicOnionLocalRaidReceiver（SetupLocalRaid 需要）
/// </summary>
public class LocalRaidWebReceiver : 
    IMagicOnionLocalRaidNotificaiton,
    IMagicOnionLocalRaidReceiver, 
    IMagicOnionErrorReceiver
{
    private readonly long _userId;
    private readonly IHubContext<LocalRaidHub> _hubContext;
    private readonly LocalRaidSessionManager _sessionManager;
    private readonly ILogger<LocalRaidWebReceiver> _logger;

    public LocalRaidWebReceiver(
        long userId,
        IHubContext<LocalRaidHub> hubContext,
        LocalRaidSessionManager sessionManager,
        ILogger<LocalRaidWebReceiver> logger)
    {
        _userId = userId;
        _hubContext = hubContext;
        _sessionManager = sessionManager;
        _logger = logger;
    }

    /// <summary>
    /// 发送事件到 SignalR 客户端
    /// </summary>
    private async Task SendToClient(string method, object? arg = null)
    {
        try
        {
            // 使用 Group 而不是 User，因为我们在 OnConnectedAsync 中已经将用户加入了组
            if (arg != null)
            {
                await _hubContext.Clients.Group(_userId.ToString()).SendAsync(method, arg);
            }
            else
            {
                await _hubContext.Clients.Group(_userId.ToString()).SendAsync(method);
            }
            _logger.LogDebug("Sent {Method} to user {UserId}", method, _userId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send {Method} to user {UserId}", method, _userId);
        }
    }

    #region IMagicOnionLocalRaidNotificaiton 实现（构造函数需要）

    public void OnError(ErrorCode errorCode)
    {
        _logger.LogWarning("OnError (Notification) for user {UserId}, code: {ErrorCode}", _userId, errorCode);
        var error = new LocalRaidError
        {
            ErrorCode = (int)errorCode,
            Message = GetErrorMessage(errorCode)
        };
        _ = SendToClient("OnError", error);
    }

    public void OnReadyBattle()
    {
        _logger.LogDebug("OnReadyBattle for user {UserId}", _userId);
        _ = SendToClient("OnReadyBattle");
    }

    public void OnStartBattle()
    {
        _logger.LogInformation("OnStartBattle (Notification) for user {UserId}", _userId);
        _ = SendToClient("OnBattleStarted");
    }

    public void OnDisbandRoom()
    {
        _logger.LogDebug("OnDisbandRoom (Notification) for user {UserId}", _userId);
        var session = _sessionManager.GetSession(_userId);
        if (session != null)
        {
            session.CurrentRoom = null;
        }
        _ = SendToClient("OnRoomDisbanded");
    }

    public void OnRefused()
    {
        _logger.LogDebug("OnRefused for user {UserId}", _userId);
        _ = SendToClient("OnRefused");
    }

    public void OnInvited(OnInviteResponse response)
    {
        _logger.LogDebug("OnInvited for user {UserId}, from {PlayerName}", _userId, response.PlayerName);
        _ = SendToClient("OnInvited", new
        {
            InvitePlayerId = response.PlayerId,
            InvitePlayerName = response.PlayerName,
            QuestId = response.QuestId,
            RoomId = response.RoomId,
            BattlePower = response.BattlePower,
            PlayerRank = response.PlayerRank,
            CharacterIconId = response.CharacterIconId
        });
    }

    public void OnJoinRoom()
    {
        _logger.LogDebug("OnJoinRoom (Notification) for user {UserId}", _userId);
        _ = SendToClient("OnJoinRoomNotification");
    }

    #endregion

    #region IMagicOnionLocalRaidReceiver 实现（SetupLocalRaid 需要）

    void IMagicOnionLocalRaidReceiver.OnGetRoomList(OnGetRoomListResponse response)
    {
        _logger.LogDebug("OnGetRoomList for user {UserId}, room count: {Count}", _userId, response.LocalRaidPartyInfoList?.Count ?? 0);
        _ = SendToClient("OnRoomListReceived", response.LocalRaidPartyInfoList);
    }

    void IMagicOnionLocalRaidReceiver.OnDisbandRoom()
    {
        _logger.LogDebug("OnDisbandRoom (Receiver) for user {UserId}", _userId);
        var session = _sessionManager.GetSession(_userId);
        if (session != null)
        {
            session.CurrentRoom = null;
        }
        _ = SendToClient("OnRoomDisbanded");
    }

    void IMagicOnionLocalRaidReceiver.OnInvite(OnInviteResponse response)
    {
        _logger.LogDebug("OnInvite for user {UserId}, from {PlayerName}", _userId, response.PlayerName);
        _ = SendToClient("OnInviteReceived", new
        {
            InvitePlayerId = response.PlayerId,
            InvitePlayerName = response.PlayerName,
            QuestId = response.QuestId,
            RoomId = response.RoomId,
            BattlePower = response.BattlePower,
            PlayerRank = response.PlayerRank,
            CharacterIconId = response.CharacterIconId
        });
    }

    void IMagicOnionLocalRaidReceiver.OnInviteRefuse(OnInviteRefuseResponse response)
    {
        _logger.LogDebug("OnInviteRefuse for user {UserId}", _userId);
        _ = SendToClient("OnInviteRefused", response);
    }

    void IMagicOnionLocalRaidReceiver.OnLeaveRoom()
    {
        _logger.LogDebug("OnLeaveRoom for user {UserId}", _userId);
        var session = _sessionManager.GetSession(_userId);
        if (session != null)
        {
            session.CurrentRoom = null;
        }
        _ = SendToClient("OnLeaveRoom");
    }

    void IMagicOnionLocalRaidReceiver.OnLockRoom()
    {
        _logger.LogDebug("OnLockRoom for user {UserId}", _userId);
        _ = SendToClient("OnRoomLocked");
    }

    void IMagicOnionLocalRaidReceiver.OnJoinRoom(OnJoinRoomResponse response)
    {
        _logger.LogDebug("OnJoinRoom (Receiver) for user {UserId}, room {RoomId}", _userId, response.LocalRaidPartyInfo?.RoomId);
        var session = _sessionManager.GetSession(_userId);
        if (session != null)
        {
            session.CurrentRoom = response.LocalRaidPartyInfo;
        }
        _ = SendToClient("OnRoomJoined", response.LocalRaidPartyInfo);
    }

    void IMagicOnionLocalRaidReceiver.OnRefuse()
    {
        _logger.LogDebug("OnRefuse (Receiver) for user {UserId}", _userId);
        _ = SendToClient("OnRefused");
    }

    void IMagicOnionLocalRaidReceiver.OnStartBattle()
    {
        _logger.LogInformation("OnStartBattle (Receiver) for user {UserId}", _userId);
        _ = SendToClient("OnBattleStarted");
    }

    void IMagicOnionLocalRaidReceiver.OnUpdateRoom(OnUpdateRoomResponse response)
    {
        _logger.LogDebug("OnUpdateRoom for user {UserId}, room {RoomId}", _userId, response.LocalRaidPartyInfo?.RoomId);
        var session = _sessionManager.GetSession(_userId);
        if (session != null)
        {
            session.CurrentRoom = response.LocalRaidPartyInfo;
        }
        _ = SendToClient("OnRoomUpdated", response.LocalRaidPartyInfo);
    }

    void IMagicOnionLocalRaidReceiver.OnUpdatePartyCount(OnUpdatePartyCountResponse response)
    {
        _logger.LogDebug("OnUpdatePartyCount for user {UserId}", _userId);
        _ = SendToClient("OnPartyCountUpdated", response.PartyCountDictionary);
    }

    #endregion

    #region IMagicOnionErrorReceiver 实现

    void IMagicOnionErrorReceiver.OnError(ErrorCode errorCode)
    {
        _logger.LogWarning("OnError (ErrorReceiver) for user {UserId}, code: {ErrorCode}", _userId, errorCode);
        var error = new LocalRaidError
        {
            ErrorCode = (int)errorCode,
            Message = GetErrorMessage(errorCode)
        };
        _ = SendToClient("OnError", error);
    }

    #endregion

    #region 辅助方法

    private string GetErrorMessage(ErrorCode errorCode)
    {
        try
        {
            return Masters.TextResourceTable.GetErrorCodeMessage(errorCode);
        }
        catch
        {
            return $"Error code: {errorCode}";
        }
    }

    #endregion
}
