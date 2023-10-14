using MementoMori.Ortega.Share.MagicOnionShare.Response;

namespace MementoMori.Ortega.Share.MagicOnionShare.Interfaces.Receiver
{
	public interface IOrtegaReceiver
	{
		void OnAuthenticateSuccess();

		void OnError(ErrorCode errorCode);

		void OnDisbandRoom();

		void OnGetRoomList(OnGetRoomListResponse response);

		void OnJoinRoom(OnJoinRoomResponse response);

		void OnInvite(OnInviteResponse response);

		void OnLeaveRoom();

		void OnLockRoom();

		void OnRefuse();

		void OnStartBattle();

		void OnUpdatePartyCount(OnUpdatePartyCountResponse response);

		void OnUpdateRoom(OnUpdateRoomResponse response);

		void OnNoticePrivateMessage(OnNoticePrivateMessageResponse response);

		void OnRemovedFromGuild();

		void OnReceiveGuildChatLog(OnReceiveGuildChatLogResponse response);

		void OnReceiveSvSChatLog(OnReceiveSvSChatLogResponse response);

		void OnReceiveWorldChatLog(OnReceiveWorldChatLogResponse response);

		void OnReceiveMessage(OnReceiveMessageResponse response);

		void OnLocalGvgOpenBattleDialog(OnOpenBattleDialogResponse response);

		void OnLocalGvgUpdateCastleParty(OnUpdateCastlePartyResponse response);

		void OnLocalGvgEndCastleBattle(OnEndCastleBattleResponse response);

		void OnLocalGvgUpdateMap(OnUpdateMapResponse response);

		void OnLocalGvgUpdateDeployCharacter(OnUpdateDeployCharacterResponse response);

		void OnLocalGvgAddOnlyReceiverParty(OnAddOnlyReceiverPartyResponse response);

		void OnGlobalGvgOpenBattleDialog(OnOpenBattleDialogResponse response);

		void OnGlobalGvgUpdateCastleParty(OnUpdateCastlePartyResponse response);

		void OnGlobalGvgEndCastleBattle(OnEndCastleBattleResponse response);

		void OnGlobalGvgUpdateMap(OnUpdateMapResponse response);

		void OnGlobalGvgUpdateDeployCharacter(OnUpdateDeployCharacterResponse response);

		void OnGlobalGvgAddOnlyReceiverParty(OnAddOnlyReceiverPartyResponse response);
	}
}
