using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Equipment;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("武具強化アイテムテーブル")]
	public class EquipmentReinforcementMaterialMB : MasterBookBase
	{
		[Nest(false, 0)]
		[PropertyOrder(1)]
		[Description("レベル毎の強化アイテムリスト")]
		public IReadOnlyList<EquipmentReinforcementMaterialInfo> ReinforcementMap
		{
			get;
		}

		[SerializationConstructor]
		public EquipmentReinforcementMaterialMB(long id, bool? isIgnore, string memo, IReadOnlyList<EquipmentReinforcementMaterialInfo> reinforcementMap)
			:base(id, isIgnore, memo)
		{
			ReinforcementMap = reinforcementMap;
		}

		public EquipmentReinforcementMaterialMB() : base(0, false, "")
		{
		}
	}
}
