using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Equipment;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("属性ボーナス")]
	public class ElementBonusMB : MasterBookBase
	{
		[PropertyOrder(3)]
		[Description("増加パラメータリスト")]
		[Nest(false, 0)]
		public IReadOnlyList<BattleParameterChangeInfo> BattleParameterChangeInfos
		{
			get;
		}

		[Description("属性条件タイプ")]
		[PropertyOrder(1)]
		public ElementBonusConditionType ConditionType
		{
			get;
		}

		[Description("発動段階")]
		[PropertyOrder(2)]
		public ElementBonusPhaseType Phase
		{
			get;
		}

		[SerializationConstructor]
		public ElementBonusMB(long id, bool? isIgnore, string memo, ElementBonusConditionType conditionType, ElementBonusPhaseType phase, IReadOnlyList<BattleParameterChangeInfo> battleParameterChangeInfos)
			:base(id, isIgnore, memo)
		{
			ConditionType = conditionType;
			Phase = phase;
			BattleParameterChangeInfos = battleParameterChangeInfos;
		}

		public ElementBonusMB() : base(0, false, "")
		{
		}
	}
}
