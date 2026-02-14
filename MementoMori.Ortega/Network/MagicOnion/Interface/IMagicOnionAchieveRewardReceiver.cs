using MementoMori.Ortega.Share.MagicOnionShare.Response;

namespace MementoMori.Ortega.Network.MagicOnion.Interface
{
	/// <summary>
	/// 成就奖励接收器接口
	/// </summary>
	public interface IMagicOnionAchieveRewardReceiver
	{
		/// <summary>
		/// 接收成就奖励通知
		/// </summary>
		void OnReceiveAchieveReward(OnReceiveAchieveRewardResponse response);
	}
}
