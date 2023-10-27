using MementoMori.Ortega.Share.MagicOnionShare.Response;

namespace MementoMori.Ortega.Network.MagicOnion.Interface
{
	public interface IMagicOnionChatReceiver
	{
		void OnNoticePrivateMessage(OnNoticePrivateMessageResponse response);

		void OnRemovedFromGuild();

		void OnReceiveGuildChatLog(OnReceiveGuildChatLogResponse response);

		void OnReceiveSvSChatLog(OnReceiveSvSChatLogResponse response);

		void OnReceiveWorldChatLog(OnReceiveWorldChatLogResponse response);

		void OnReceiveMessage(OnReceiveMessageResponse response);
	}
}
