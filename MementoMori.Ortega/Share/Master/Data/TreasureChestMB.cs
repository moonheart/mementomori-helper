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
        [PropertyOrder(14)]
        [Description("一括使用")]
        public bool BulkUseEnabled { get; }

        [PropertyOrder(8)]
        [Description("開けるのに必要な鍵のID ※0の場合無効")]
        public long ChestKeyItemId { get; }

        [PropertyOrder(3)]
        [Description("説明文キー")]
        public string DescriptionKey { get; }

        [PropertyOrder(2)]
        [Description("表示名キー")]
        public string DisplayNameKey { get; }

        [PropertyOrder(4)]
        [Description("アイコンID")]
        public long IconId { get; }

        [PropertyOrder(5)]
        [Description("レアリティ")]
        public ItemRarityFlags ItemRarityFlags { get; }

        [PropertyOrder(10)]
        [Description("所持数上限")]
        public long MaxItemCount { get; }

        [PropertyOrder(9)]
        [Description("必要個数")]
        public int MinOpenCount { get; }

        [PropertyOrder(1)]
        [Description("名称キー")]
        public string NameKey { get; }

        [PropertyOrder(12)]
        [Description("第2フレーム値")]
        public int SecondaryFrameNum { get; }

        [PropertyOrder(11)]
        [Description("第2フラーム種類")]
        public SecondaryFrameType SecondaryFrameType { get; }

        [PropertyOrder(6)]
        [Description("並び順（昇順）")]
        public int SortOrder { get; }

        [PropertyOrder(13)]
        [Description("TreasureChestItemMBのIdリスト")]
        public IReadOnlyList<long> TreasureChestItemIdList { get; }

        [PropertyOrder(7)]
        [Description("宝箱の報酬判定方法タイプ")]
        public TreasureChestLotteryType TreasureChestLotteryType { get; }

		[SerializationConstructor]
        public TreasureChestMB(long id, bool? isIgnore, string memo, bool bulkUseEnabled, long chestKeyItemId, string descriptionKey, string displayNameKey, long iconId,
            ItemRarityFlags itemRarityFlags, long maxItemCount, int minOpenCount, string nameKey, int secondaryFrameNum, SecondaryFrameType secondaryFrameType, int sortOrder,
            TreasureChestLotteryType treasureChestLotteryType, IReadOnlyList<long> treasureChestItemIdList)
			:base(id, isIgnore, memo)
		{
            BulkUseEnabled = bulkUseEnabled;
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
            SortOrder = sortOrder;
			TreasureChestLotteryType = treasureChestLotteryType;
			TreasureChestItemIdList = treasureChestItemIdList;
		}

        [PropertyOrder(15)]
        [Description("終了日時")]
        public string EndTime { get; set; }

        [PropertyOrder(16)]
        [Description("開始日時")]
        public string StartTime { get; set; }

		public TreasureChestMB() :base(0L, false, ""){}
	}
}
