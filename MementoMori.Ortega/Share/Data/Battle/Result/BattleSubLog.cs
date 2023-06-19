using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Battle.Result
{
	[MessagePackObject(true)]
	public struct BattleSubLog
	{
		public List<SubSkillResult> TurnStartPassiveResults { get; set; }

		public List<SubSkillResult> TurnEndPassiveResults { get; set; }

		public List<ActiveSkillData> ActiveSkillDatas { get; set; }

		public int Turn
		{
			readonly get;
			set;
		}
	}
}
