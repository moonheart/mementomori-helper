using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Battle;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[Description("時空の洞窟 加護")]
	[MessagePackObject(true)]
	public class DungeonBattleRelicMB : MasterBookBase
	{
		[Description("戦闘力ボーナス")]
		[PropertyOrder(4)]
		public long BattlePowerBonus
		{
			get;
		}

		[Description("戦闘力ボーナス対象")]
		[PropertyOrder(5)]
		public DungeonBattleRelicBattlePowerBonusTargetType BattlePowerBonusTargetType
		{
			get;
		}

		[Description("複数所持可能有無")]
		[PropertyOrder(6)]
		public bool CanMultiplePossession
		{
			get;
		}

		[Description("説明文キー")]
		[PropertyOrder(8)]
		public string DescriptionKey
		{
			get;
		}

		[Description("レアリティ")]
		[PropertyOrder(2)]
		public DungeonBattleRelicRarityType DungeonRelicRarityType
		{
			get;
		}

		[PropertyOrder(1)]
		[Description("名前キー")]
		public string NameKey
		{
			get;
		}

		[Description("パッシブスキルタイプ情報")]
		[PropertyOrder(7)]
		[Nest(false, 0)]
		public IReadOnlyList<PassiveSkillTypeInfo> PassiveSkillTypeInfos
		{
			get;
		}

		[PropertyOrder(3)]
		[Description("下位ID（0の場合は直接入手可能）")]
		public long ReinforceFrom
		{
			get;
		}

		[Description("アイコンID")]
		[PropertyOrder(9)]
		public long IconId
		{
			get;
		}

		[SerializationConstructor]
		public DungeonBattleRelicMB(long id, bool? isIgnore, string memo, string descriptionKey, DungeonBattleRelicRarityType dungeonRelicRarityType, string nameKey, long reinforceFrom, long battlePowerBonus, DungeonBattleRelicBattlePowerBonusTargetType battlePowerBonusTargetType, bool canMultiplePossession, IReadOnlyList<PassiveSkillTypeInfo> passiveSkillTypeInfos, long iconId)
			:base(id, isIgnore, memo)
		{
			DescriptionKey = descriptionKey;
			DungeonRelicRarityType = dungeonRelicRarityType;
			NameKey = nameKey;
			ReinforceFrom = reinforceFrom;
			BattlePowerBonus = battlePowerBonus;
			BattlePowerBonusTargetType = battlePowerBonusTargetType;
			CanMultiplePossession = canMultiplePossession;
			PassiveSkillTypeInfos = passiveSkillTypeInfos;
			IconId = iconId;
		}

		public DungeonBattleRelicMB() : base(0, false, "")
		{
		}
	}
}
