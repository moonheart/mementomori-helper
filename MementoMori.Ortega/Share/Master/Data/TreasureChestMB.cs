using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MementoMori.Ortega.Share.Utils;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("宝箱、宝石袋など")]
	public class TreasureChestMB : MasterBookBase, IHasStartEndTime
	{
		[PropertyOrder(7)]
		[Description("開けるのに必要な鍵のID ※0の場合無効")]
		public long ChestKeyItemId
		{
			get;
		}

		[PropertyOrder(3)]
		[Description("説明文キー")]
		public string DescriptionKey
		{
			get;
		}

		[Description("表示名キー")]
		[PropertyOrder(2)]
		public string DisplayNameKey
		{
			get;
		}

		[Description("アイコンID")]
		[PropertyOrder(4)]
		public long IconId
		{
			get;
		}

		[Description("レアリティ")]
		[PropertyOrder(5)]
		public ItemRarityFlags ItemRarityFlags
		{
			get;
		}

		[PropertyOrder(9)]
		[Description("所持数上限")]
		public long MaxItemCount
		{
			get;
		}

		[PropertyOrder(8)]
		[Description("必要個数")]
		public int MinOpenCount
		{
			get;
		}

		[PropertyOrder(1)]
		[Description("名称キー")]
		public string NameKey
		{
			get;
		}

		[Description("第2フレーム値")]
		[PropertyOrder(11)]
		public int SecondaryFrameNum
		{
			get;
		}

		[Description("第2フラーム種類")]
		[PropertyOrder(10)]
		public SecondaryFrameType SecondaryFrameType
		{
			get;
		}

		[Description("TreasureChestItemMBのIdリスト")]
		[PropertyOrder(12)]
		public IReadOnlyList<long> TreasureChestItemIdList
		{
			get;
		}

		[PropertyOrder(6)]
		[Description("宝箱の報酬判定方法タイプ")]
		public TreasureChestLotteryType TreasureChestLotteryType
		{
			get;
		}

		[SerializationConstructor]
		public TreasureChestMB(long id, bool? isIgnore, string memo, long chestKeyItemId, string descriptionKey, string displayNameKey, long iconId, ItemRarityFlags itemRarityFlags, long maxItemCount, int minOpenCount, string nameKey, int secondaryFrameNum, SecondaryFrameType secondaryFrameType, TreasureChestLotteryType treasureChestLotteryType, IReadOnlyList<long> treasureChestItemIdList)
			:base(id, isIgnore, memo)
		{
			ChestKeyItemId = chestKeyItemId;
			DescriptionKey = descriptionKey;
			DisplayNameKey = displayNameKey;
			IconId = iconId;
			ItemRarityFlags = itemRarityFlags;
			MaxItemCount = maxItemCount;
			MinOpenCount = minOpenCount;
			NameKey = nameKey;
			SecondaryFrameNum = secondaryFrameNum;
			SecondaryFrameType = secondaryFrameType;
			TreasureChestLotteryType = treasureChestLotteryType;
			TreasureChestItemIdList = treasureChestItemIdList;
		}

		[PropertyOrder(13)]
		[Description("終了日時")]
		public string EndTime
		{
			get;
			set;
		}

		[PropertyOrder(14)]
		[Description("開始日時")]
		public string StartTime
		{
			get;
			set;
		}

		public TreasureChestMB() :base(0L, false, ""){}
	}
}
