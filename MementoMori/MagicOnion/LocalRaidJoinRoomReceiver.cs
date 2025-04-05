using System.Diagnostics;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Common.Enums;
using MementoMori.Ortega.Network.MagicOnion.Client;
using MementoMori.Ortega.Network.MagicOnion.Interface;
using MementoMori.Ortega.Share.Data.LocalRaid;
using MementoMori.Ortega.Share.MagicOnionShare.Response;

namespace MementoMori.MagicOnion;

public abstract class LocalRaidBaseReceiver : IMagicOnionLocalRaidReceiver, IMagicOnionErrorReceiver
{
    protected readonly OrtegaMagicOnionClient _ortegaMagicOnionClient;
    protected readonly CancellationToken CancellationToken;
    protected Action<string> _log;
    protected Stopwatch _startSw;

    public LocalRaidBaseReceiver(OrtegaMagicOnionClient ortegaMagicOnionClient, Action<string> log, CancellationToken cancellationToken)
    {
        _ortegaMagicOnionClient = ortegaMagicOnionClient;
        _log = log;
        CancellationToken = cancellationToken;
        _startSw = Stopwatch.StartNew();
    }

    public long QuestId { get; set; }
    public bool IsNoRemainingChallenges { get; protected set; }
    public bool IsMaxTimeExceeded { get; protected set; }

    public bool IsBattleStarted { get; set; }

    public virtual void OnError(ErrorCode errorCode)
    {
        _log($"OnError: {TextResourceTable.GetErrorCodeMessage(errorCode)}");
    }

    public virtual void OnGetRoomList(OnGetRoomListResponse response)
    {
        _log("OnGetRoomList");
    }

    public virtual void OnDisbandRoom()
    {
        _log("OnDisbandRoom");
    }

    public virtual void OnInvite(OnInviteResponse response)
    {
        _log("OnInvite");
    }

    public void OnInviteRefuse(OnInviteRefuseResponse response)
    {
        _log("OnInviteRefuse");
    }

    public virtual void OnLeaveRoom()
    {
        _log("OnLeaveRoom");
    }

    public virtual void OnLockRoom()
    {
        _log("OnLockRoom");
    }

    public virtual void OnJoinRoom(OnJoinRoomResponse response)
    {
        _log("OnJoinRoom");
    }

    public virtual void OnRefuse()
    {
        _log("OnRefuse");
    }

    public virtual void OnStartBattle()
    {
        _log(TextResourceTable.Get("[LocalRaidRoomManagementBattleStartMessage]"));
        IsBattleStarted = true;
    }

    public virtual void OnUpdateRoom(OnUpdateRoomResponse response)
    {
        _log("OnUpdateRoom");
    }

    public virtual void OnUpdatePartyCount(OnUpdatePartyCountResponse response)
    {
        _log("OnUpdatePartyCount");
    }
}

public class LocalRaidJoinRoomReceiver : LocalRaidBaseReceiver
{
    public LocalRaidJoinRoomReceiver(OrtegaMagicOnionClient ortegaMagicOnionClient, Action<string> log, CancellationToken cancellationToken) : base(ortegaMagicOnionClient, log, cancellationToken)
    {
    }


    public override async void OnDisbandRoom()
    {
        _log(TextResourceTable.GetErrorCodeMessage(ClientErrorCode.LocalRaidDismissedRoom));
        await Task.Delay(3000);
        _log(TextResourceTable.Get("[LocalRaidRoomSearchButtonJoinRandomRoom]"));
        try
        {
            _ortegaMagicOnionClient.SendLocalRaidJoinRandomRoom(QuestId);
        }
        catch (Exception e)
        {
            _log(e.Message);
        }
    }

    public override async void OnLeaveRoom()
    {
        await Task.Delay(3000);
        _log(TextResourceTable.Get("[LocalRaidRoomSearchButtonJoinRandomRoom]"));
        try
        {
            _ortegaMagicOnionClient.SendLocalRaidJoinRandomRoom(QuestId);
        }
        catch (Exception e)
        {
            _log(e.Message);
        }
    }

    public override async void OnRefuse()
    {
        _log(TextResourceTable.Get("[LocalRaidRoomRefuseReceiveDialogMessage]"));
        await Task.Delay(3000);
        try
        {
            _ortegaMagicOnionClient.SendLocalRaidJoinRandomRoom(QuestId);
        }
        catch (Exception e)
        {
            _log(e.Message);
        }
    }

    public override void OnJoinRoom(OnJoinRoomResponse response)
    {
        _log("OnJoinRoom");
        _ortegaMagicOnionClient.SendLocalRaidReady(true);
    }

    public override async void OnError(ErrorCode errorCode)
    {
        _log($"OnError: {TextResourceTable.GetErrorCodeMessage(errorCode)}");
        try
        {
            switch (errorCode)
            {
                case ErrorCode.MagicOnionLocalRaidJoinRandomRoomNotExistRoom:
                    if (_startSw.Elapsed > TimeSpan.FromMinutes(10))
                    {
                        IsMaxTimeExceeded = true;
                        _startSw.Stop();
                        break;
                    }

                    await Task.Delay(3000);
                    _log(TextResourceTable.Get("[LocalRaidRoomSearchButtonJoinRandomRoom]"));
                    _ortegaMagicOnionClient.SendLocalRaidJoinRandomRoom(QuestId);
                    break;
                case ErrorCode.MagicOnionLocalRaidInviteNoRemainingChallenges:
                case ErrorCode.MagicOnionLocalRaidJoinRoomNoRemainingChallenges:
                case ErrorCode.MagicOnionLocalRaidOpenRoomNoRemainingChallenges:
                case ErrorCode.MagicOnionLocalRaidJoinFriendRoomNoRemainingChallenges:
                case ErrorCode.MagicOnionLocalRaidJoinRandomRoomNoRemainingChallenges:
                    IsNoRemainingChallenges = true;
                    break;
            }
        }
        catch (Exception e)
        {
            _log(e.Message);
        }
    }
}

public class LocalRaidCreateRoomReceiver : LocalRaidBaseReceiver
{
    private readonly int _waitSeconds;
    private string _lastRoomId;

    public LocalRaidCreateRoomReceiver(OrtegaMagicOnionClient ortegaMagicOnionClient, Action<string> log, int waitSeconds, CancellationToken cancellationToken) : base(ortegaMagicOnionClient, log,
        cancellationToken)
    {
        _waitSeconds = waitSeconds;
    }

    public override void OnJoinRoom(OnJoinRoomResponse response)
    {
        _log("OnJoinRoom");
        StartBattle(response.LocalRaidPartyInfo);
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private void StartBattle(LocalRaidPartyInfo partyInfo)
    {
        _lastRoomId = partyInfo.RoomId;
        Task.Delay(TimeSpan.FromSeconds(_waitSeconds)).ContinueWith(async _ =>
        {
            while (!IsBattleStarted && !CancellationToken.IsCancellationRequested)
            {
                if (partyInfo.IsReady) _ortegaMagicOnionClient.SendLocalRaidStartBattle();

                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }, CancellationToken);
    }

    public override void OnUpdateRoom(OnUpdateRoomResponse response)
    {
        _log("OnUpdateRoom");
        StartBattle(response.LocalRaidPartyInfo);
    }

    public override void OnError(ErrorCode errorCode)
    {
        _log($"OnError: {TextResourceTable.GetErrorCodeMessage(errorCode)}");
        switch (errorCode)
        {
            case ErrorCode.MagicOnionLocalRaidJoinRandomRoomNotExistRoom:
            case ErrorCode.MagicOnionLocalRaidInviteNoRemainingChallenges:
            case ErrorCode.MagicOnionLocalRaidJoinRoomNoRemainingChallenges:
            case ErrorCode.MagicOnionLocalRaidOpenRoomNoRemainingChallenges:
            case ErrorCode.MagicOnionLocalRaidJoinFriendRoomNoRemainingChallenges:
            case ErrorCode.MagicOnionLocalRaidJoinRandomRoomNoRemainingChallenges:
                IsNoRemainingChallenges = true;
                break;
        }
    }
}