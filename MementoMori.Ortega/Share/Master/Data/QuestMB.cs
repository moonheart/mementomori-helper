using System.ComponentModel;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("クエスト")]
	public class QuestMB : MasterBookBase
	{
		[PropertyOrder(7)]
		[Description("ベース戦闘力")]
		public long BaseBattlePower
		{
			get;
		}

		[MasterBookId(typeof(ChapterMB))]
		[PropertyOrder(1)]
		[Description("章ID")]
		public long ChapterId
		{
			get;
		}

		[PropertyOrder(3)]
		[Description("ゴールド(/m)")]
		public int GoldPerMinute
		{
			get;
		}

		[PropertyOrder(8)]
		[Description("最低獲得経験珠")]
		public int MinCharacterExp
		{
			get;
		}

		[Description("最低獲得プレイヤー経験値")]
		[PropertyOrder(9)]
		public int MinPlayerExp
		{
			get;
		}

		[Description("潜在宝珠(/d)")]
		[PropertyOrder(4)]
		public int PotentialJewelPerDay
		{
			get;
		}

		[Description("人口")]
		[PropertyOrder(2)]
		public int Population
		{
			get;
		}

		[Description("クエスト難易度")]
		[PropertyOrder(6)]
		public QuestDifficultyType QuestDifficultyType
		{
			get;
		}

		[PropertyOrder(5)]
		[Description("マップ建物ID")]
		public long QuestMapBuildingId
		{
			get;
		}

		[SerializationConstructor]
		public QuestMB(long id, bool? isIgnore, string memo, long chapterId, int goldPerMinute, int potentialJewelPerDay, long questMapBuildingId, QuestDifficultyType questDifficultyType, int population, long baseBattlePower, int minCharacterExp, int minPlayerExp)
			:base(id, isIgnore, memo)
		{
			ChapterId = chapterId;
			GoldPerMinute = goldPerMinute;
			PotentialJewelPerDay = potentialJewelPerDay;
			QuestMapBuildingId = questMapBuildingId;
			QuestDifficultyType = questDifficultyType;
			Population = population;
			BaseBattlePower = baseBattlePower;
			MinCharacterExp = minCharacterExp;
			MinPlayerExp = minPlayerExp;
		}

		public QuestMB() :base(0L, false, ""){}
	}
}
