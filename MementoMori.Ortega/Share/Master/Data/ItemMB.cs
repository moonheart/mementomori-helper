using System.ComponentModel;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MementoMori.Ortega.Share.Utils;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("アイテムの表示情報")]
	public class ItemMB : MasterBookBase, IHasStartEndTime
	{
        [PropertyOrder(5)]
        [Description("説明文キー")]
        public string DescriptionKey { get; }

        [PropertyOrder(4)]
        [Description("表示名キー")]
        public string DisplayName { get; }

        [PropertyOrder(9)]
        [Description("終了時刻")]
        public string EndTime { get; }

        [PropertyOrder(2)]
        [Description("アイテムID")]
        public long ItemId { get; }

        [PropertyOrder(6)]
        [Description("レアリティ")]
        public ItemRarityFlags ItemRarityFlags { get; }

        [PropertyOrder(1)]
        [Description("アイテム種別")]
        public ItemType ItemType { get; }

        [PropertyOrder(10)]
        [Description("所持数上限")]
        public long MaxItemCount { get; }

        [PropertyOrder(3)]
        [Description("名称キー")]
        public string NameKey { get; }

        [PropertyOrder(11)]
        [Description("アイコンId")]
        public int IconId { get; }

        [PropertyOrder(14)]
        [Description("第2フレーム値")]
        public int SecondaryFrameNum { get; }

        [PropertyOrder(13)]
        [Description("第2フラーム種類")]
        public SecondaryFrameType SecondaryFrameType { get; }

        [PropertyOrder(7)]
        [Description("並び順（昇順）")]
        public int SortOrder { get; }

        [PropertyOrder(8)]
        [Description("開始時刻")]
        public string StartTime { get; }

        [PropertyOrder(12)]
        [Description("遷移先ID")]
        public long TransferSpotId { get; }

        [SerializationConstructor]
        public ItemMB(long id, bool? isIgnore, string memo, string descriptionKey, string displayName, string endTime, long itemId, ItemRarityFlags itemRarityFlags, ItemType itemType, long maxItemCount, string nameKey, int iconId, int sortOrder, string startTime, long transferSpotId, SecondaryFrameType secondaryFrameType, int secondaryFrameNum)
            : base(id, isIgnore, memo)
        {
            DescriptionKey = descriptionKey;
            DisplayName = displayName;
            EndTime = endTime;
            ItemId = itemId;
            ItemRarityFlags = itemRarityFlags;
            ItemType = itemType;
            MaxItemCount = maxItemCount;
            NameKey = nameKey;
            IconId = iconId;
            SortOrder = sortOrder;
            StartTime = startTime;
            TransferSpotId = transferSpotId;
            SecondaryFrameType = secondaryFrameType;
            SecondaryFrameNum = secondaryFrameNum;
        }

        public ItemMB()
            : base(0L, null, null)
        {
        }
	}
}
