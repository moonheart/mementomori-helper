using MementoMori.Ortega.Share.Data.Battle.Result;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.LocalRaid
{
	[MessagePackObject(true)]
	public class LocalRaidBattleLogInfo
	{
        public BattleEndInfo BattleEndInfo { get; set; }

        public long BattleTime { get; set; }

		public string BattleToken { get; set; }

		public LocalRaidPartyInfo LocalRaidPartyInfo { get; set; }

		public long QuestId { get; set; }

		public int ClearLevel { get; set; }

		public bool IsAutoStart { get; set; }
	}
}
