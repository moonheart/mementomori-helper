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
		[Description("説明文キー")]
		[PropertyOrder(5)]
		public string DescriptionKey
		{
			get;
		}

		[Description("表示名キー")]
		[PropertyOrder(4)]
		public string DisplayName
		{
			get;
		}

		[Description("終了時刻")]
		[PropertyOrder(9)]
		public string EndTime
		{
			get;
		}

		[Description("アイテムID")]
		[PropertyOrder(2)]
		public long ItemId
		{
			get;
		}

		[Description("レアリティ")]
		[PropertyOrder(6)]
		public ItemRarityFlags ItemRarityFlags
		{
			get;
		}

		[PropertyOrder(1)]
		[Description("アイテム種別")]
		public ItemType ItemType
		{
			get;
		}

		[Description("所持数上限")]
		[PropertyOrder(10)]
		public long MaxItemCount
		{
			get;
		}

		[Description("名称キー")]
		[PropertyOrder(3)]
		public string NameKey
		{
			get;
		}

		[PropertyOrder(11)]
		[Description("アイコンId")]
		public int IconId
		{
			get;
		}

		[PropertyOrder(14)]
		[Description("第2フレーム値")]
		public int SecondaryFrameNum
		{
			get;
		}

		[Description("第2フラーム種類")]
		[PropertyOrder(13)]
		public SecondaryFrameType SecondaryFrameType
		{
			get;
		}

		[Description("並び順（降順）")]
		[PropertyOrder(7)]
		public int SortOrder
		{
			get;
		}

		[PropertyOrder(8)]
		[Description("開始時刻")]
		public string StartTime
		{
			get;
		}

		[PropertyOrder(12)]
		[Description("遷移先ID")]
		public long TransferSpotId
		{
			get;
		}

		[SerializationConstructor]
		public ItemMB(long id, bool? isIgnore, string memo, string descriptionKey, string displayName, string endTime, long itemId, ItemRarityFlags itemRarityFlags, ItemType itemType, long maxItemCount, string nameKey, int iconId, int sortOrder, string startTime, long transferSpotId, SecondaryFrameType secondaryFrameType, int secondaryFrameNum)
			:base(id, isIgnore, memo)
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

		public ItemMB() :base(0L, false, ""){}
	}
}
