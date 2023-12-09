using System.Diagnostics;
using MementoMori.Ortega.Common.Enums;
using MementoMori.Ortega.Network.MagicOnion.Client;
using MementoMori.Ortega.Network.MagicOnion.Interface;
using MementoMori.Ortega.Share;
using MementoMori.Ortega.Share.MagicOnionShare.Response;
using MementoMori.Ortega.Share.Master.Table;

namespace MementoMori.MagicOnion;

public abstract class LocalRaidBaseReceiver : IMagicOnionLocalRaidReceiver, IMagicOnionErrorReceiver
{
    protected readonly OrtegaMagicOnionClient _ortegaMagicOnionClient;
    protected Action<string> _log;
    protected Stopwatch _startSw;
    public long QuestId { get; set; }
    public bool IsNoRemainingChallenges { get; protected set; }
    public bool IsMaxTimeExceeded { get; protected set; }

    public bool IsBattleStarted { get; protected set; }

    public LocalRaidBaseReceiver(OrtegaMagicOnionClient ortegaMagicOnionClient, Action<string> log)
    {
        _ortegaMagicOnionClient = ortegaMagicOnionClient;
        _log = log;
        _startSw = Stopwatch.StartNew();
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
        _log(Masters.TextResourceTable.Get("[LocalRaidRoomManagementBattleStartMessage]"));
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

    public virtual void OnError(ErrorCode errorCode)
    {
        _log($"OnError: {Masters.TextResourceTable.GetErrorCodeMessage(errorCode)}");
    }
}

public class LocalRaidJoinRoomReceiver : LocalRaidBaseReceiver
{
    public LocalRaidJoinRoomReceiver(OrtegaMagicOnionClient ortegaMagicOnionClient, Action<string> log) : base(ortegaMagicOnionClient, log)
    {
    }


    public override async void OnDisbandRoom()
    {
        _log(Masters.TextResourceTable.GetErrorCodeMessage(ClientErrorCode.LocalRaidDismissedRoom));
        await Task.Delay(3000);
        _log(Masters.TextResourceTable.Get("[LocalRaidRoomSearchButtonJoinRandomRoom]"));
        _ortegaMagicOnionClient.SendLocalRaidJoinRandomRoom(QuestId);
    }

    public override async void OnLeaveRoom()
    {
        _log(Masters.TextResourceTable.Get("[LocalRaidRoomRefuseReceiveDialogMessage]"));
        await Task.Delay(3000);
        _log(Masters.TextResourceTable.Get("[LocalRaidRoomSearchButtonJoinRandomRoom]"));
        _ortegaMagicOnionClient.SendLocalRaidJoinRandomRoom(QuestId);
    }

    public override void OnJoinRoom(OnJoinRoomResponse response)
    {
        _log("OnJoinRoom");
        _ortegaMagicOnionClient.SendLocalRaidReady(true);
    }

    public override async void OnError(ErrorCode errorCode)
    {
        _log($"OnError: {Masters.TextResourceTable.GetErrorCodeMessage(errorCode)}");
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
                _log(Masters.TextResourceTable.Get("[LocalRaidRoomSearchButtonJoinRandomRoom]"));
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
}

public class LocalRaidCreateRoomReceiver : LocalRaidBaseReceiver
{
    public LocalRaidCreateRoomReceiver(OrtegaMagicOnionClient ortegaMagicOnionClient, Action<string> log) : base(ortegaMagicOnionClient, log)
    {
    }

    public override void OnJoinRoom(OnJoinRoomResponse response)
    {
        _log("OnJoinRoom");
        if (response.LocalRaidPartyInfo.IsReady)
        {
            _ortegaMagicOnionClient.SendLocalRaidStartBattle();
        }
    }

    public override void OnUpdateRoom(OnUpdateRoomResponse response)
    {
        _log("OnUpdateRoom");
        if (response.LocalRaidPartyInfo.IsReady)
        {
            _ortegaMagicOnionClient.SendLocalRaidStartBattle();
        }
    }

    public override async void OnError(ErrorCode errorCode)
    {
        _log($"OnError: {Masters.TextResourceTable.GetErrorCodeMessage(errorCode)}");
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