using System.ComponentModel;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.BountyQuest
{
	[MessagePackObject(true)]
	public class BoardRankConditionInfo
	{
		[Description("懸賞カウンタークエストタイプ")]
		public BountyQuestType BountyQuestType
		{
			get;
			set;
		}

		[Description("クエストレベル")]
		public BountyQuestRarityFlags BountyQuestRarity
		{
			get;
			set;
		}

		[Description("必要な数")]
		public int RequireCount
		{
			get;
			set;
		}

		[Description("累計必要クリア数")]
		public int TotalRequireCount
		{
			get;
			set;
		}

		public BoardRankConditionInfo()
		{
		}
	}
}
