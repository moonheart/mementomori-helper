using MagicOnion;
using MementoMori.Ortega.Share.MagicOnionShare.Interfaces.Receiver;
using MementoMori.Ortega.Share.MagicOnionShare.Request;

namespace MementoMori.Ortega.Share.MagicOnionShare.Interfaces.Sender
{
	public interface IOrtegaSender : IStreamingHub<IOrtegaSender, IOrtegaReceiver>, IStreamingHubMarker, IServiceMarker
	{
		Task AuthenticateAsync(AuthenticateRequest request);

		Task KeepAliveAsync();

		Task DisbandRoomAsync();

		Task GetRoomListAsync(GetRoomListRequest request);

		Task InviteAsync(InviteRequest request);

		Task JoinFriendRoomAsync(JoinFriendRoomRequest request);

		Task JoinRandomRoomAsync(JoinRandomRoomRequest request);

		Task JoinRoomAsync(JoinRoomRequest request);

		Task LeaveRoomAsync();

		Task OpenRoomAsync(OpenRoomRequest request);

		Task RefuseAsync(RefuseRequest request);

		Task ReadyAsync(ReadyRequest request);

		Task UpdateBattlePowerAsync(UpdateBattlePowerRequest request);

		Task UpdateRoomConditionAsync(UpdateRoomConditionRequest request);

		Task StartBattleAsync();

		Task UpdatePartyCountAsync();

		Task RoomClearAsync();

		Task SendMessageAsync(SendMessageRequest request);

		Task LocalGvgAddCastleParty(AddCastlePartyRequest request);

		Task LocalGvgOrderCastleParty(OrderCastlePartyRequest request);

		Task LocalGvgOpenBattleDialog(OpenBattleDialogRequest request);

		Task LocalGvgOpenPartyDeployDialog(OpenPartyDeployDialogRequest request);

		Task LocalGvgCloseCastleDialog(CloseDialogRequest request);

		Task LocalGvgCastleDeclaration(CastleDeclarationRequest request);

		Task LocalGvgCastleDeclarationCounter(CastleDeclarationCounterRequest request);

		Task LocalGvgOpenMap();

		Task LocalGvgCloseMap();

		Task GlobalGvgAddCastleParty(AddCastlePartyRequest request);

		Task GlobalGvgOrderCastleParty(OrderCastlePartyRequest request);

		Task GlobalGvgOpenBattleDialog(OpenBattleDialogRequest request);

		Task GlobalGvgOpenPartyDeployDialog(OpenPartyDeployDialogRequest request);

		Task GlobalGvgCloseCastleDialog(CloseDialogRequest request);

		Task GlobalGvgCastleDeclaration(CastleDeclarationRequest request);

		Task GlobalGvgCastleDeclarationCounter(CastleDeclarationCounterRequest request);

		Task GlobalGvgOpenMap(OpenMapRequest request);

		Task GlobalGvgCloseMap();
	}
}
