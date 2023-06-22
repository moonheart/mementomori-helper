using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Battle.Result;
using MementoMori.Ortega.Share.Master.Data;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.DungeonBattle
{
	[MessagePackObject(true)]
	public class ExecBattleResponse : ApiResponseBase
	{
		public BattleRewardResult BattleRewardResult
		{
			get;
			set;
		}

		public BattleSimulationResult BattleSimulationResult
		{
			get;
			set;
		}

		public List<DungeonBattleAllyInfo> DungeonBattleAllyInfos
		{
			get;
			set;
		}

		public long DungeonCoinCountToReset
		{
			get;
			set;
		}

		public ExecBattleResponse()
		{
		}
	}
}
