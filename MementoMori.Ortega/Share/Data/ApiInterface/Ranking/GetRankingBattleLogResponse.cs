using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Battle.Result;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Ranking
{
	[MessagePackObject(true)]
	public class GetRankingBattleLogResponse : ApiResponseBase
	{
		public BattleSimulationResult BattleSimulationResult { get; set; }
	}
}
