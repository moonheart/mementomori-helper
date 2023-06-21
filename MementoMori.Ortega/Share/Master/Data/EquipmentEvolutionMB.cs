using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Equipment;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("武具進化テーブル")]
	public class EquipmentEvolutionMB : MasterBookBase
	{
		[Description("必要アイテム情報")]
		[PropertyOrder(1)]
		[Nest(false, 0)]
		public IReadOnlyList<EquipmentEvolutionInfo> EquipmentEvolutionInfoList
		{
			get;
		}

		[PropertyOrder(2)]
		[Description("武具進化の種類")]
		public EvolutionType EvolutionType
		{
			get;
		}

		[SerializationConstructor]
		public EquipmentEvolutionMB(long id, bool? isIgnore, string memo, IReadOnlyList<EquipmentEvolutionInfo> equipmentEvolutionInfoList, EvolutionType evolutionType)
			:base(id, isIgnore, memo)
		{
			EquipmentEvolutionInfoList = equipmentEvolutionInfoList;
			EvolutionType = evolutionType;
		}

		public EquipmentEvolutionMB() : base(0, false, "")
		{
		}
	}
}
