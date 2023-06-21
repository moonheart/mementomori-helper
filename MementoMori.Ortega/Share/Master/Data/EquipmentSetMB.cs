using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Equipment;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("武具セット")]
	public class EquipmentSetMB : MasterBookBase
	{
		[PropertyOrder(2)]
		[Nest(false, 0)]
		[Description("セット効果リスト")]
		public IReadOnlyList<EquipmentSetEffect> EffectList
		{
			get;
		}

		[PropertyOrder(1)]
		[Description("名称キー")]
		public string NameKey
		{
			get;
		}

		[SerializationConstructor]
		public EquipmentSetMB(long id, bool? isIgnore, string memo, IReadOnlyList<EquipmentSetEffect> effectList, string nameKey)
			:base(id, isIgnore, memo)
		{
			EffectList = effectList;
			NameKey = nameKey;
		}

		public EquipmentSetMB() : base(0, false, "")
		{
		}
	}
}
