using System.ComponentModel;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[Description("レジェンドリーグ階級データ")]
	[MessagePackObject(true)]
	public class LegendLeagueClassMB : MasterBookBase
	{
		[Description("階級名キー")]
		[PropertyOrder(2)]
		public string ClassNameKey
		{
			get;
		}

		[PropertyOrder(1)]
		[Description("レジェンドリーグ階級")]
		public LegendLeagueClassType ClassType
		{
			get;
		}

		[Description("最低必要ランク")]
		[PropertyOrder(4)]
		public long RequiredMinimumRank
		{
			get;
		}

		[PropertyOrder(3)]
		[Description("要求ポイント")]
		public long RequiredPoint
		{
			get;
		}

		[SerializationConstructor]
		public LegendLeagueClassMB(long id, bool? isIgnore, string memo, LegendLeagueClassType classType, string classNameKey, long requiredPoint, long requiredMinimumRank)
			:base(id, isIgnore, memo)
		{
			ClassType = classType;
			ClassNameKey = classNameKey;
			RequiredPoint = requiredPoint;
			RequiredMinimumRank = requiredMinimumRank;
		}

		public LegendLeagueClassMB()  :base(0L, false, ""){}
	}
}
