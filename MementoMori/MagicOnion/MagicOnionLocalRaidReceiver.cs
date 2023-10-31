using MementoMori.Ortega.Common.Enums;
using MementoMori.Ortega.Network.MagicOnion.Client;
using MementoMori.Ortega.Network.MagicOnion.Interface;
using MementoMori.Ortega.Share;
using MementoMori.Ortega.Share.MagicOnionShare.Response;

namespace MementoMori.MagicOnion;

public class MagicOnionLocalRaidReceiver : IMagicOnionLocalRaidReceiver, IMagicOnionErrorReceiver
{
    private readonly OrtegaMagicOnionClient _ortegaMagicOnionClient;

    private readonly Action<string> _log;

    public MagicOnionLocalRaidReceiver(OrtegaMagicOnionClient ortegaMagicOnionClient, Action<string> log)
    {
        _ortegaMagicOnionClient = ortegaMagicOnionClient;
        _log = log;
    }

    public long QuestId { get; set; }
    public bool IsBattleStarted { get; private set; }
    public bool IsNoRemainingChallenges { get; private set; }

    public void OnGetRoomList(OnGetRoomListResponse response)
    {
    }

    public void OnDisbandRoom()
    {
        _log(Masters.TextResourceTable.GetErrorCodeMessage(ClientErrorCode.LocalRaidDismissedRoom));
        _ortegaMagicOnionClient.SendLocalRaidJoinRandomRoom(QuestId);
    }

    public void OnInvite(OnInviteResponse response)
    {
    }

    public void OnLeaveRoom()
    {
        _log(Masters.TextResourceTable.Get("[LocalRaidRoomRefuseReceiveDialogMessage]"));
        _ortegaMagicOnionClient.SendLocalRaidJoinRandomRoom(QuestId);
    }

    public void OnLockRoom()
    {
        Console.WriteLine("OnLockRoom");
    }

    public void OnJoinRoom(OnJoinRoomResponse response)
    {
        _log("OnJoinRoom");
        _ortegaMagicOnionClient.SendLocalRaidReady(true);
    }

    public void OnRefuse()
    {
    }

    public void OnStartBattle()
    {
        _log(Masters.TextResourceTable.Get("[LocalRaidRoomManagementBattleStartMessage]"));
        IsBattleStarted = true;
    }

    public void OnUpdateRoom(OnUpdateRoomResponse response)
    {
        _log("OnUpdateRoom");
    }

    public void OnUpdatePartyCount(OnUpdatePartyCountResponse response)
    {
        _log("OnUpdatePartyCount");
    }

    public async void OnError(ErrorCode errorCode)
    {
        _log($"OnError: {Masters.TextResourceTable.GetErrorCodeMessage(errorCode)}");
        switch (errorCode)
        {
            case ErrorCode.MagicOnionLocalRaidJoinRandomRoomNotExistRoom:
                await Task.Delay(500);
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