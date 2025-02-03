using MementoMori.Ortega.Share.MagicOnionShare.Response;

namespace MementoMori.Ortega.Network.MagicOnion.Interface
{
	public interface IMagicOnionLocalRaidReceiver
	{
        void OnGetRoomList(OnGetRoomListResponse response);

        void OnDisbandRoom();

        void OnInvite(OnInviteResponse response);

        void OnInviteRefuse(OnInviteRefuseResponse response);

        void OnLeaveRoom();

        void OnLockRoom();

        void OnJoinRoom(OnJoinRoomResponse response);

        void OnRefuse();

        void OnStartBattle();

        void OnUpdateRoom(OnUpdateRoomResponse response);

        void OnUpdatePartyCount(OnUpdatePartyCountResponse response);
	}
}
