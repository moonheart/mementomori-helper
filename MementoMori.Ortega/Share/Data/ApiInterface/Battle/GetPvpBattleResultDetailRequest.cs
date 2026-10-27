using MementoMori.Ortega.Share.Data.Battle;
using MementoMori.Ortega.Share.Data.Battle.Result;
using MementoMori.Ortega.Share.Data.Pvp;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Battle
{
	[MessagePackObject(true)]
	[OrtegaApi("battle/getPvpBattleResultDetail", true, false)]
	public class GetPvpBattleResultDetailRequest : ApiRequestBase
	{
		public string BattleToken { get; set; }
	}

	[MessagePackObject(true)]
	public class GetPvpBattleResultDetailResponse : ApiResponseBase
	{
		public BattleResult BattleResult { get; set; }
	}
}
