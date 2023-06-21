using System.ComponentModel;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[Description("ギルドレベル")]
	[MessagePackObject(true)]
	public class GuildLevelMB : MasterBookBase
	{
		[PropertyOrder(4)]
		[Description("1日の経験値獲得上限")]
		public long DayLimitExp
		{
			get;
		}

		[Description("レベル")]
		[PropertyOrder(1)]
		public long Level
		{
			get;
		}

		[Description("加入可能人数")]
		[PropertyOrder(2)]
		public long NumberOfPossibleJoinPeople
		{
			get;
		}

		[Description("必要累計経験値")]
		[PropertyOrder(3)]
		public long RequiredTotalExp
		{
			get;
		}

		[SerializationConstructor]
		public GuildLevelMB(long id, bool? isIgnore, string memo, long level, long numberOfPossibleJoinPeople, long requiredTotalExp, long dayLimitExp)
			:base(id, isIgnore, memo)
		{
			Level = level;
			NumberOfPossibleJoinPeople = numberOfPossibleJoinPeople;
			RequiredTotalExp = requiredTotalExp;
			DayLimitExp = dayLimitExp;
		}

		public GuildLevelMB() : base(0, false, "")
		{
		}
	}
}
