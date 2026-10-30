using MementoMori.Ortega.Share.MagicOnionShare.Response;

namespace MementoMori.Ortega.Network.MagicOnion.Interface
{
	/// <summary>
	/// 公会塔接收器接口
	/// </summary>
	public interface IMagicOnionGuildTowerReceiver
	{
		/// <summary>
		/// 接收公会塔信息通知
		/// </summary>
		void OnNoticeGuildTowerInfo(GuildTowerInfoResponse response);
	}
}
