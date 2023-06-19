using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Battle
{
	[OrtegaApi("battle/rewardAutoBattle", true, false)]
	[MessagePackObject(true)]
	public class RewardAutoBattleRequest : ApiRequestBase
	{
		public RewardAutoBattleRequest()
		{
		}
	}
}
