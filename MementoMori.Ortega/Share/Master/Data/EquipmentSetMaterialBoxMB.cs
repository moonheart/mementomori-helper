using System.ComponentModel;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MementoMori.Ortega.Share.Utils;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("アダマントボックス")]
	public class EquipmentSetMaterialBoxMB : MasterBookBase, IHasStartEndTime
	{
		[PropertyOrder(3)]
		[Description("説明文")]
		public string DescriptionKey { get; }

		[PropertyOrder(2)]
		[Description("表示名")]
		public string DisplayNameKey { get; }

		[PropertyOrder(12)]
		[Description("終了時刻")]
		public string EndTime { get; }

		[PropertyOrder(7)]
		[Description("部位種類リスト")]
		public IReadOnlyList<long> EquipmentTypeList { get; }

		[PropertyOrder(4)]
		[Description("アイコンId")]
		public int IconId { get; }

		[PropertyOrder(5)]
		[Description("レアリティ")]
		public ItemRarityFlags ItemRarityFlags { get; }

		[PropertyOrder(6)]
		[Description("レベルリスト")]
		public IReadOnlyList<long> LevelList { get; }

		[PropertyOrder(8)]
		[Description("所持数上限")]
		public long MaxItemCount { get; }

		[PropertyOrder(1)]
		[Description("アイテム名")]
		public string NameKey { get; }

		[PropertyOrder(10)]
		[Description("第2フレーム値")]
		public int SecondaryFrameNum { get; }

		[PropertyOrder(9)]
		[Description("第2フラーム種類")]
		public SecondaryFrameType SecondaryFrameType { get; }

		[PropertyOrder(11)]
		[Description("開始時刻")]
		public string StartTime { get; }

		[SerializationConstructor]
		public EquipmentSetMaterialBoxMB(long id, bool? isIgnore, string memo, string descriptionKey, string displayNameKey, string endTime, IReadOnlyList<long> equipmentTypeList, ItemRarityFlags itemRarityFlags, IReadOnlyList<long> levelList, long maxItemCount, string nameKey, int iconId, string startTime, SecondaryFrameType secondaryFrameType, int secondaryFrameNum)
			: base(id, isIgnore, memo)
		{
            this.DescriptionKey = descriptionKey;
            this.DisplayNameKey = displayNameKey;
            this.EndTime = endTime;
            this.EquipmentTypeList = equipmentTypeList;
            this.IconId = iconId;
            this.ItemRarityFlags = itemRarityFlags;
            this.LevelList = levelList;
            this.MaxItemCount = maxItemCount;
            this.NameKey = nameKey;
            this.SecondaryFrameNum = secondaryFrameNum;
            this.SecondaryFrameType = secondaryFrameType;
            this.StartTime = startTime;
		}

		public EquipmentSetMaterialBoxMB()
			: base(0L, null, null)
		{
		}
	}
}
