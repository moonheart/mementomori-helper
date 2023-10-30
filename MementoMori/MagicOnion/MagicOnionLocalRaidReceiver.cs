using MementoMori.Ortega.Network.MagicOnion.Client;
using MementoMori.Ortega.Network.MagicOnion.Interface;
using MementoMori.Ortega.Share;
using MementoMori.Ortega.Share.MagicOnionShare.Response;

namespace MementoMori.MagicOnion;

public class MagicOnionLocalRaidReceiver : IMagicOnionLocalRaidReceiver, IMagicOnionErrorReceiver
{
    private readonly OrtegaMagicOnionClient _ortegaMagicOnionClient;

    public MagicOnionLocalRaidReceiver(OrtegaMagicOnionClient ortegaMagicOnionClient)
    {
        _ortegaMagicOnionClient = ortegaMagicOnionClient;
    }

    public long QuestId { get; set; }
    public bool IsBattleStarted { get; private set; }
    public bool IsNoRemainingChallenges { get; private set; }

    public void OnGetRoomList(OnGetRoomListResponse response)
    {
        Console.WriteLine("OnGetRoomList");
    }

    public void OnDisbandRoom()
    {
        Console.WriteLine("OnDisbandRoom");
        _ortegaMagicOnionClient.SendLocalRaidJoinRandomRoom(QuestId);
    }

    public void OnInvite(OnInviteResponse response)
    {
        Console.WriteLine("OnInvite");
    }

    public void OnLeaveRoom()
    {
        Console.WriteLine("OnLeaveRoom");
        _ortegaMagicOnionClient.SendLocalRaidJoinRandomRoom(QuestId);
    }

    public void OnLockRoom()
    {
        Console.WriteLine("OnLockRoom");
    }

    public void OnJoinRoom(OnJoinRoomResponse response)
    {
        Console.WriteLine("OnJoinRoom");
        _ortegaMagicOnionClient.SendLocalRaidReady(true);
    }

    public void OnRefuse()
    {
        Console.WriteLine("OnRefuse");
    }

    public void OnStartBattle()
    {
        Console.WriteLine("OnStartBattle");
        IsBattleStarted = true;
    }

    public void OnUpdateRoom(OnUpdateRoomResponse response)
    {
        Console.WriteLine("OnUpdateRoom");
    }

    public void OnUpdatePartyCount(OnUpdatePartyCountResponse response)
    {
        Console.WriteLine("OnUpdatePartyCount");
    }

    public async void OnError(ErrorCode errorCode)
    {
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