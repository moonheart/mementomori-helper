using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[Description("武具合成")]
	[MessagePackObject(true)]
	public class EquipmentCompositeMB : MasterBookBase
	{
		[PropertyOrder(1)]
		[Description("合成した際に取得する武具ID")]
		public long EquipmentId
		{
			get;
		}

		[Description("レアリティ")]
		[PropertyOrder(4)]
		public ItemRarityFlags ItemRarityFlags
		{
			get;
		}

		[Description("必要欠片数")]
		[PropertyOrder(2)]
		public long RequiredFragmentCount
		{
			get;
		}

		[Description("必要アイテム情報")]
		[PropertyOrder(3)]
		[Nest(false, 0)]
		public IReadOnlyList<UserItem> RequiredItemList
		{
			get;
		}

		[SerializationConstructor]
		public EquipmentCompositeMB(long id, bool? isIgnore, string memo, long equipmentId, ItemRarityFlags itemRarityFlags, long requiredFragmentCount, IReadOnlyList<UserItem> requiredItemList)
			:base(id, isIgnore, memo)
		{
			EquipmentId = equipmentId;
			ItemRarityFlags = itemRarityFlags;
			RequiredFragmentCount = requiredFragmentCount;
			RequiredItemList = requiredItemList;
		}

		public EquipmentCompositeMB() : base(0, false, "")
		{
		}
	}
}
