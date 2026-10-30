using System.Collections.Concurrent;
using Grpc.Net.Client;
using MementoMori.Api.Handlers.LocalRaid;
using MementoMori.Api.Infrastructure;
using MementoMori.Api.Models;
using MementoMori.Ortega.Common.Enums;
using MementoMori.Ortega.Network.MagicOnion.Client;
using MementoMori.Ortega.Share.Data.LocalRaid;
using MementoMori.Ortega.Share.Enums;

namespace MementoMori.Api.Services;

/// <summary>
/// LocalRaid 会话
/// </summary>
public class LocalRaidSession : IDisposable
{
    public long UserId { get; }
    public OrtegaMagicOnionClient? Client { get; private set; }
    public LocalRaidWebReceiver? Receiver { get; private set; }
    public LocalRaidPartyInfo? CurrentRoom { get; set; }
    public bool IsConnected => Client?.GetState() == HubClientState.Ready;

    private readonly CancellationTokenSource _cts = new();
    private readonly ILogger<LocalRaidSession> _logger;

    public LocalRaidSession(long userId, ILogger<LocalRaidSession> logger)
    {
        UserId = userId;
        _logger = logger;
    }

    /// <summary>
    /// 初始化会话，连接到 MagicOnion
    /// </summary>
    public async Task InitializeAsync(
        GrpcChannel grpcChannel,
        long playerId,
        string authToken,
        LocalRaidWebReceiver receiver)
    {
        Receiver = receiver;
        
        // 构造函数需要 IMagicOnionLocalRaidNotificaiton
        Client = new OrtegaMagicOnionClient(grpcChannel, playerId, authToken, receiver);

        // SetupLocalRaid 需要 IMagicOnionLocalRaidReceiver 和 IMagicOnionErrorReceiver
        Client.SetupLocalRaid(receiver, receiver);

        _logger.LogInformation("Connecting to MagicOnion for user {UserId}...", UserId);
        await Client.Connect();

        // 等待连接就绪（状态变为 Ready 需要等待 OnAuthenticateSuccess 回调）
        int retry = 20; // 增加重试次数
        while (Client.GetState() != HubClientState.Ready && retry-- > 0)
        {
            _logger.LogDebug("Waiting for MagicOnion connection... State: {State}, Retries left: {Retries}", 
                Client.GetState(), retry);
            await Task.Delay(500, _cts.Token);
        }

        var finalState = Client.GetState();
        if (finalState != HubClientState.Ready)
        {
            _logger.LogWarning("Failed to connect to MagicOnion for user {UserId}, final state: {State}", 
                UserId, finalState);
            throw new InvalidOperationException($"Failed to connect to MagicOnion, state: {finalState}");
        }

        // 启动 KeepAlive
        _ = Task.Run(async () =>
        {
            while (!_cts.Token.IsCancellationRequested)
            {
                Client.SendKeepAliveAsync();
                await Task.Delay(5000, _cts.Token);
            }
        }, _cts.Token);

        _logger.LogInformation("LocalRaid session initialized for user {UserId}", UserId);
    }

    /// <summary>
    /// 发送创建房间请求
    /// </summary>
    public void SendOpenRoom(LocalRaidRoomConditionsType conditionsType, long questId, long requiredBattlePower, int password, bool isAutoStart)
    {
        Client?.SendLocalRaidOpenRoom(conditionsType, questId, requiredBattlePower, password, isAutoStart);
    }

    /// <summary>
    /// 发送加入房间请求
    /// </summary>
    public void SendJoinRoom(string roomId, int password)
    {
        Client?.SendLocalRaidJoinRoom(roomId, password);
    }

    /// <summary>
    /// 发送随机加入请求
    /// </summary>
    public void SendJoinRandomRoom(long questId)
    {
        Client?.SendLocalRaidJoinRandomRoom(questId);
    }

    /// <summary>
    /// 发送准备请求
    /// </summary>
    public void SendReady(bool isReady)
    {
        Client?.SendLocalRaidReady(isReady);
    }

    /// <summary>
    /// 发送开始战斗请求
    /// </summary>
    public void SendStartBattle()
    {
        Client?.SendLocalRaidStartBattle();
    }

    /// <summary>
    /// 发送离开房间请求
    /// </summary>
    public void SendLeaveRoom()
    {
        Client?.SendLocalRaidLeaveRoom();
    }

    /// <summary>
    /// 发送获取房间列表请求
    /// </summary>
    public void SendGetRoomList(long questId)
    {
        _logger.LogInformation("SendGetRoomList for user {UserId}, quest {QuestId}, state: {State}", 
            UserId, questId, Client?.GetState());
        Client?.SendLocalRaidGetRoomList(questId);
    }

    /// <summary>
    /// 发送更新房间条件请求
    /// </summary>
    public void SendUpdateRoomCondition(LocalRaidRoomConditionsType conditionsType, long requiredBattlePower, int password)
    {
        Client?.SendLocalRaidUpdateRoomCondition(conditionsType, requiredBattlePower, password);
    }

    /// <summary>
    /// 发送邀请请求
    /// </summary>
    public void SendInvite(long friendPlayerId)
    {
        Client?.SendLocalRaidInvite(friendPlayerId);
    }

    /// <summary>
    /// 发送解散房间请求
    /// </summary>
    public void SendDisbandRoom()
    {
        Client?.SendLocalRaidDisbandRoom();
    }

    public void Dispose()
    {
        _cts.Cancel();
        _cts.Dispose();
        Client?.ClearLocalRaidReceiver();
        Client?.DisposeAsync();
        _logger.LogInformation("LocalRaid session disposed for user {UserId}", UserId);
    }
}

/// <summary>
/// LocalRaid 会话管理器
/// 管理 Web 用户与 MagicOnion 的连接会话
/// </summary>
[RegisterSingleton]
[AutoConstructor]
public partial class LocalRaidSessionManager
{
    private readonly IServiceProvider _serviceProvider;
    private readonly AccountManager _accountManager;
    private readonly ILoggerFactory _loggerFactory;
    private readonly ILogger<LocalRaidSessionManager> _logger;

    // 用户ID -> 会话
    private readonly ConcurrentDictionary<long, LocalRaidSession> _sessions = new();

    /// <summary>
    /// 获取或创建会话
    /// </summary>
    public async Task<LocalRaidSession> GetOrCreateSessionAsync(long userId)
    {
        if (_sessions.TryGetValue(userId, out var existingSession))
        {
            if (existingSession.IsConnected)
            {
                return existingSession;
            }
            // 会话已断开，移除并重新创建
            _sessions.TryRemove(userId, out _);
            existingSession.Dispose();
        }

        _logger.LogInformation("Creating new LocalRaid session for user {UserId}", userId);

        // 获取账户上下文
        var context = await _accountManager.GetOrCreateAsync(userId);
        var nm = context.NetworkManager;

        // 获取 gRPC Channel 和认证令牌
        var grpcChannel = nm.GrpcChannel;
        var authToken = nm.AuthTokenOfMagicOnion;

        if (grpcChannel == null || string.IsNullOrEmpty(authToken))
        {
            throw new InvalidOperationException("User is not logged in or MagicOnion is not ready");
        }

        // 创建会话
        var session = new LocalRaidSession(userId, _loggerFactory.CreateLogger<LocalRaidSession>());

        // 创建接收器
        var receiver = new LocalRaidWebReceiver(
            userId,
            _serviceProvider.GetRequiredService<Microsoft.AspNetCore.SignalR.IHubContext<LocalRaidHub>>(),
            this,
            _loggerFactory.CreateLogger<LocalRaidWebReceiver>());

        // 初始化会话
        await session.InitializeAsync(grpcChannel, nm.PlayerId, authToken, receiver);

        _sessions[userId] = session;
        return session;
    }

    /// <summary>
    /// 获取会话
    /// </summary>
    public LocalRaidSession? GetSession(long userId)
    {
        _sessions.TryGetValue(userId, out var session);
        return session;
    }

    /// <summary>
    /// 移除会话
    /// </summary>
    public async Task RemoveSessionAsync(long userId)
    {
        if (_sessions.TryRemove(userId, out var session))
        {
            await Task.Run(() => session.Dispose());
            _logger.LogInformation("Removed LocalRaid session for user {UserId}", userId);
        }
    }

    /// <summary>
    /// 获取会话状态
    /// </summary>
    public LocalRaidSessionState GetSessionState(long userId)
    {
        var session = GetSession(userId);
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
}
