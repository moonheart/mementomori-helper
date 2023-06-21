using System.ComponentModel;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[Description("武具進化素材")]
	[MessagePackObject(true)]
	public class EquipmentSetMaterialMB : MasterBookBase
	{
		[PropertyOrder(4)]
		[Description("説明文キー")]
		public string DescriptionKey
		{
			get;
		}

		[Description("アイコンID")]
		[PropertyOrder(5)]
		public long IconId
		{
			get;
		}

		[PropertyOrder(7)]
		[Description("レアリティ")]
		public ItemRarityFlags ItemRarityFlags
		{
			get;
		}

		[PropertyOrder(3)]
		[Description("レベル")]
		public long Lv
		{
			get;
		}

		[PropertyOrder(1)]
		[Description("名称キー")]
		public string NameKey
		{
			get;
		}

		[Description("表示名キー")]
		[PropertyOrder(2)]
		public string DisplayNameKey
		{
			get;
		}

		[PropertyOrder(6)]
		[Description("入手ステージ Quest ID")]
		public IReadOnlyList<long> QuestIdList
		{
			get;
		}

		[SerializationConstructor]
		public EquipmentSetMaterialMB(long id, bool? isIgnore, string memo, string descriptionKey, long iconId, ItemRarityFlags itemRarityFlags, long lv, string nameKey, string displayNameKey, IReadOnlyList<long> questIdList)
			:base(id, isIgnore, memo)
		{
			DescriptionKey = descriptionKey;
			IconId = iconId;
			ItemRarityFlags = itemRarityFlags;
			Lv = lv;
			NameKey = nameKey;
			DisplayNameKey = displayNameKey;
			QuestIdList = questIdList;
		}

		public EquipmentSetMaterialMB() : base(0, false, "")
		{
		}
	}
}
