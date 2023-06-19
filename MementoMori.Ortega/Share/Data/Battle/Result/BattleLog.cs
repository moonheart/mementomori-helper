using MessagePack;

namespace MementoMori.Ortega.Share.Data.Battle.Result
{
	[MessagePackObject(true)]
	public class BattleLog
	{
		public List<SubSkillResult> BattleStartPassiveResults { get; set; }

		public List<SubSkillResult> BattleEndPassiveResults { get; set; }

		public List<BattleSubLog> BattleSubLogs { get; set; }

		public BattleLog()
		{
			this.BattleStartPassiveResults = new List<SubSkillResult>();
			this.BattleEndPassiveResults = new List<SubSkillResult>();
			this.BattleSubLogs = new List<BattleSubLog>();
		}
	}
}
