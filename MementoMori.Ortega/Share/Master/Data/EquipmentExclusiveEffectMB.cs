using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Equipment;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[Description("武具専属効果")]
	[MessagePackObject(true)]
	public class EquipmentExclusiveEffectMB : MasterBookBase
	{
		[Nest(false, 0)]
		[PropertyOrder(2)]
		[Description("専属効果(BaseParameter)")]
		public IReadOnlyList<BaseParameterChangeInfo> BaseParameterChangeInfoList
		{
			get;
		}

		[PropertyOrder(3)]
		[Nest(false, 0)]
		[Description("専属効果(BattleParameter)")]
		public IReadOnlyList<BattleParameterChangeInfo> BattleParameterChangeInfoList
		{
			get;
		}

		[Description("専属効果適用キャラクターID")]
		[PropertyOrder(1)]
		public long CharacterId
		{
			get;
		}

		[SerializationConstructor]
		public EquipmentExclusiveEffectMB(long id, bool? isIgnore, string memo, IReadOnlyList<BaseParameterChangeInfo> baseParameterChangeInfoList, IReadOnlyList<BattleParameterChangeInfo> battleParameterChangeInfoList, long characterId)
			:base(id, isIgnore, memo)
		{
			BaseParameterChangeInfoList = baseParameterChangeInfoList;
			BattleParameterChangeInfoList = battleParameterChangeInfoList;
			CharacterId = characterId;
		}

		public EquipmentExclusiveEffectMB() : base(0, false, "")
		{
		}
	}
}
