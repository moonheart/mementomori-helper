using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[Description("武具強化パラメータテーブル")]
	[MessagePackObject(true)]
	public class EquipmentReinforcementParameterMB : MasterBookBase
	{
		[PropertyOrder(1)]
		[Description("強化倍率係数")]
		public double ReinforcementCoefficient
		{
			 get;
		}

		[SerializationConstructor]
		public EquipmentReinforcementParameterMB(long id, bool? isIgnore, string memo, double reinforcementCoefficient)
			:base(id, isIgnore, memo)
		{
			ReinforcementCoefficient = reinforcementCoefficient;
		}

		public EquipmentReinforcementParameterMB() : base(0, false, "")
		{
		}
	}
}
