using MementoMori.Ortega.Share;

namespace MementoMori.Ortega.Network.MagicOnion.Interface
{
	public interface IMagicOnionErrorReceiver
	{
		void OnError(ErrorCode errorCode);
	}
}
