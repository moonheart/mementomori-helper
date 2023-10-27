using MementoMori.Ortega.Share;
using MementoMori.Ortega.Share.MagicOnionShare.Response;

namespace MementoMori.Ortega.Network.MagicOnion.Interface
{
	public interface IMagicOnionLocalRaidNotificaiton
	{
		void OnError(ErrorCode errorCode);

		void OnReadyBattle();

		void OnStartBattle();

		void OnDisbandRoom();

		void OnRefused();

		void OnInvited(OnInviteResponse response);

		void OnJoinRoom();
	}
}
