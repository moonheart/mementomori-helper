using MementoMori.Ortega.Share.Data.Battle;
using MementoMori.Ortega.Share.Data.Battle.Result;
using MementoMori.Ortega.Share.Data.Pvp;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Battle
{
	[MessagePackObject(true)]
	[OrtegaApi("battle/getPvpBattleLogs", true, false)]
	public class GetPvpBattleLogsRequest : ApiRequestBase
	{

	}

	[MessagePackObject(true)]
	public class GetPvpBattleLogsResponse : ApiResponseBase
	{
		public List<PvpBattleLogInfo> AttackPvpBattleLogInfoList { get; set; }
		public List<PvpBattleLogInfo> DefensePvpBattleLogInfoList { get; set; }
	}
}
