using Microsoft.AspNetCore.SignalR;
using MementoMori.Api.Models;
using MementoMori.Api.Services;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.MagicOnionShare.Request;

namespace MementoMori.Api.Infrastructure;

/// <summary>
/// LocalRaid 实时通信 Hub
/// </summary>
public class LocalRaidHub : Hub
{
    private readonly LocalRaidSessionManager _sessionManager;
    private readonly ILogger<LocalRaidHub> _logger;

    public LocalRaidHub(
        LocalRaidSessionManager sessionManager,
        ILogger<LocalRaidHub> logger)
    {
        _sessionManager = sessionManager;
        _logger = logger;
    }

    /// <summary>
    /// 获取当前用户ID
    /// </summary>
    private long? GetUserId()
    {
        var httpContext = Context.GetHttpContext();
        if (httpContext?.Items.TryGetValue("UserId", out var userIdObj) == true && userIdObj is long userId)
        {
            return userId;
        }
        return null;
    }

    public override async Task OnConnectedAsync()
    {
        var userId = GetUserId();
        if (userId.HasValue)
        {
            _logger.LogInformation("LocalRaidHub: User {UserId} connected, ConnectionId: {ConnectionId}", 
                userId, Context.ConnectionId);
            
            // 将用户加入其个人组，便于定向推送
            await Groups.AddToGroupAsync(Context.ConnectionId, userId.Value.ToString());
            
            // 自动创建 MagicOnion 会话
            try
            {
                var session = await _sessionManager.GetOrCreateSessionAsync(userId.Value);
                _logger.LogInformation("LocalRaidHub: Session created for user {UserId}, IsConnected: {IsConnected}", 
                    userId, session.IsConnected);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "LocalRaidHub: Failed to create session for user {UserId}", userId);
            }
        }
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = GetUserId();
        if (userId.HasValue)
        {
            _logger.LogInformation("LocalRaidHub: User {UserId} disconnected", userId);
            
            // 用户断开连接时，移除会话
            await _sessionManager.RemoveSessionAsync(userId.Value);
        }
        await base.OnDisconnectedAsync(exception);
    }

    #region 会话管理

    /// <summary>
    /// 加入 LocalRaid 会话
    /// </summary>
    public async Task<LocalRaidSessionInfo> JoinSession()
    {
        var userId = GetUserId();
        if (!userId.HasValue)
        {
            throw new HubException("User not authenticated");
        }

        try
        {
            var session = await _sessionManager.GetOrCreateSessionAsync(userId.Value);
            
            _logger.LogInformation("JoinSession: User {UserId}, IsConnected: {IsConnected}, ClientState: {ClientState}", 
                userId, session.IsConnected, session.Client?.GetState());
            
            return new LocalRaidSessionInfo
            {
                UserId = userId.Value,
                IsConnected = session.IsConnected
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to join LocalRaid session for user {UserId}", userId);
            throw new HubException($"Failed to join session: {ex.Message}");
        }
    }

    /// <summary>
    /// 离开 LocalRaid 会话
    /// </summary>
    public async Task LeaveSession()
    {
        var userId = GetUserId();
        if (userId.HasValue)
        {
            await _sessionManager.RemoveSessionAsync(userId.Value);
        }
    }

    /// <summary>
    /// 获取当前会话状态
    /// </summary>
    public async Task<LocalRaidSessionState> GetCurrentState()
    {
        var userId = GetUserId();
        if (!userId.HasValue)
        {
            return new LocalRaidSessionState { IsConnected = false };
        }

        var session = _sessionManager.GetSession(userId.Value);
        if (session == null)
        {
            return new LocalRaidSessionState { IsConnected = false };
        }

        return new LocalRaidSessionState
        {
            IsConnected = session.IsConnected,
            IsInRoom = session.CurrentRoom != null,
            CurrentRoom = session.CurrentRoom
        };
    }

    #endregion

    #region 房间操作

    /// <summary>
    /// 获取房间列表
    /// </summary>
    public async Task GetRoomList(long questId)
    {
        var userId = GetUserId();
        if (!userId.HasValue) throw new HubException("User not authenticated");

        var session = _sessionManager.GetSession(userId.Value);
        if (session == null || !session.IsConnected)
        {
            throw new HubException("Session not connected");
        }

        _logger.LogInformation("User {UserId} requesting room list for quest {QuestId}", userId, questId);
        session.SendGetRoomList(questId);
    }

    /// <summary>
    /// 创建房间
    /// </summary>
    public async Task CreateRoom(OpenRoomRequest request)
    {
        var userId = GetUserId();
        if (!userId.HasValue) throw new HubException("User not authenticated");

        var session = _sessionManager.GetSession(userId.Value);
        if (session == null || !session.IsConnected)
        {
            throw new HubException("Session not connected");
        }

        _logger.LogInformation("User {UserId} creating room for quest {QuestId}", userId, request.QuestId);
        session.SendOpenRoom(
            request.ConditionsType,
            request.QuestId,
            request.RequiredBattlePower,
            request.Password,
            request.IsAutoStart);
    }

    /// <summary>
    /// 加入房间
    /// </summary>
    public async Task JoinRoom(JoinRoomRequest request)
    {
        var userId = GetUserId();
        if (!userId.HasValue) throw new HubException("User not authenticated");

        var session = _sessionManager.GetSession(userId.Value);
        if (session == null || !session.IsConnected)
        {
            throw new HubException("Session not connected");
        }

        _logger.LogInformation("User {UserId} joining room {RoomId}", userId, request.RoomId);
        session.SendJoinRoom(request.RoomId, request.Password);
    }

    /// <summary>
    /// 随机加入房间
    /// </summary>
    public async Task JoinRandomRoom(long questId)
    {
        var userId = GetUserId();
        if (!userId.HasValue) throw new HubException("User not authenticated");

        var session = _sessionManager.GetSession(userId.Value);
        if (session == null || !session.IsConnected)
        {
            throw new HubException("Session not connected");
        }

        _logger.LogInformation("User {UserId} joining random room for quest {QuestId}", userId, questId);
        session.SendJoinRandomRoom(questId);
    }

    /// <summary>
    /// 设置准备状态
    /// </summary>
    public async Task SetReady(bool isReady)
    {
        var userId = GetUserId();
        if (!userId.HasValue) throw new HubException("User not authenticated");

        var session = _sessionManager.GetSession(userId.Value);
        if (session == null || !session.IsConnected)
        {
            throw new HubException("Session not connected");
        }

        session.SendReady(isReady);
    }

    /// <summary>
    /// 开始战斗
    /// </summary>
    public async Task StartBattle()
    {
        var userId = GetUserId();
        if (!userId.HasValue) throw new HubException("User not authenticated");

        var session = _sessionManager.GetSession(userId.Value);
        if (session == null || !session.IsConnected)
        {
            throw new HubException("Session not connected");
        }

        _logger.LogInformation("User {UserId} starting battle", userId);
        session.SendStartBattle();
    }

    /// <summary>
    /// 离开房间
    /// </summary>
    public async Task LeaveRoom()
    {
        var userId = GetUserId();
        if (!userId.HasValue) throw new HubException("User not authenticated");

        var session = _sessionManager.GetSession(userId.Value);
        if (session == null || !session.IsConnected)
        {
            throw new HubException("Session not connected");
        }

        _logger.LogInformation("User {UserId} leaving room", userId);
        session.SendLeaveRoom();
        session.CurrentRoom = null;
    }

    /// <summary>
    /// 更新房间条件
    /// </summary>
    public async Task UpdateRoomCondition(UpdateRoomConditionRequest request)
    {
        var userId = GetUserId();
        if (!userId.HasValue) throw new HubException("User not authenticated");

        var session = _sessionManager.GetSession(userId.Value);
        if (session == null || !session.IsConnected)
        {
            throw new HubException("Session not connected");
        }

        session.SendUpdateRoomCondition(
            request.ConditionsType,
            request.RequiredBattlePower,
            request.Password);
    }

    /// <summary>
    /// 邀请好友
    /// </summary>
    public async Task InviteFriend(long friendPlayerId)
    {
        var userId = GetUserId();
        if (!userId.HasValue) throw new HubException("User not authenticated");

        var session = _sessionManager.GetSession(userId.Value);
        if (session == null || !session.IsConnected)
        {
            throw new HubException("Session not connected");
        }

        _logger.LogInformation("User {UserId} inviting friend {FriendId}", userId, friendPlayerId);
        session.SendInvite(friendPlayerId);
    }

    /// <summary>
    /// 解散房间
    /// </summary>
    public async Task DisbandRoom()
    {
        var userId = GetUserId();
        if (!userId.HasValue) throw new HubException("User not authenticated");

        var session = _sessionManager.GetSession(userId.Value);
        if (session == null || !session.IsConnected)
        {
            throw new HubException("Session not connected");
        }

        _logger.LogInformation("User {UserId} disbanding room", userId);
        session.SendDisbandRoom();
        session.CurrentRoom = null;
    }

    #endregion
}
