using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Battle
{
	[OrtegaApi("battle/bossRewardInfo", true, false)]
	[MessagePackObject(true)]
	public class BossRewardInfoRequest : ApiRequestBase
	{
	}
}
