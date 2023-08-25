using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Equipment;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("魔装テーブル")]
	public class EquipmentMatchlessSacredTreasureMB : MasterBookBase
	{
		[Description("メイル：防御貫通（加算式増加）")]
		[PropertyOrder(7)]
		public long ArmorDefensePenetration
		{
			get;
		}

		[Description("アーム：魔法防御（加算式増加）")]
		[PropertyOrder(5)]
		public long GauntletMagicDamageRelax
		{
			get;
		}

		[Description("メット：クリティカル（加算式増加）")]
		[PropertyOrder(6)]
		public long HelmetCritical
		{
			get;
		}

		[PropertyOrder(1)]
		[Description("魔装レベル")]
		public long Lv
		{
			get;
		}

		[Description("必要累積経験値")]
		[PropertyOrder(2)]
		public long RequiredTotalExp
		{
			get;
		}

		[PropertyOrder(8)]
		[Description("ブーツ：HP（加算式増加）")]
		public long ShoesHp
		{
			get;
		}

		[PropertyOrder(4)]
		[Description("アクセサリ：物理防御（加算式増加）")]
		public long SubPhysicalDamageRelax
		{
			get;
		}

		[PropertyOrder(3)]
		[Description("武器：攻撃力（加算式増加）")]
		public long WeaponAttackPower
		{
			get;
		}

		[SerializationConstructor]
		public EquipmentMatchlessSacredTreasureMB(long id, bool? isIgnore, string memo, long lv, long requiredTotalExp, long weaponAttackPower, long subPhysicalDamageRelax, long gauntletMagicDamageRelax, long helmetCritical, long armorDefensePenetration, long shoesHp)
			:base(id, isIgnore, memo)
		{
			Lv = lv;
			RequiredTotalExp = requiredTotalExp;
			WeaponAttackPower = weaponAttackPower;
			SubPhysicalDamageRelax = subPhysicalDamageRelax;
			GauntletMagicDamageRelax = gauntletMagicDamageRelax;
			HelmetCritical = helmetCritical;
			ArmorDefensePenetration = armorDefensePenetration;
			ShoesHp = shoesHp;
		}

		public EquipmentMatchlessSacredTreasureMB() : base(0, false, "")
		{
		}

		public BattleParameterChangeInfo GetValue(EquipmentSlotType slotType)
		{
			return slotType switch
			{
				EquipmentSlotType.Weapon => new BattleParameterChangeInfo
				{
					BattleParameterType = BattleParameterType.AttackPower, ChangeParameterType = ChangeParameterType.Addition, Value = WeaponAttackPower
				},
				EquipmentSlotType.Sub => new BattleParameterChangeInfo
				{
					BattleParameterType = BattleParameterType.PhysicalDamageRelax, ChangeParameterType = ChangeParameterType.Addition, Value = SubPhysicalDamageRelax
				},
				EquipmentSlotType.Gauntlet => new BattleParameterChangeInfo
				{
					BattleParameterType = BattleParameterType.MagicDamageRelax, ChangeParameterType = ChangeParameterType.Addition, Value = GauntletMagicDamageRelax
				},
				EquipmentSlotType.Helmet => new BattleParameterChangeInfo
				{
					BattleParameterType = BattleParameterType.Critical, ChangeParameterType = ChangeParameterType.Addition, Value = HelmetCritical
				},
				EquipmentSlotType.Armor => new BattleParameterChangeInfo
				{
					BattleParameterType = BattleParameterType.DefensePenetration, ChangeParameterType = ChangeParameterType.Addition, Value = ArmorDefensePenetration
				},
				EquipmentSlotType.Shoes => new BattleParameterChangeInfo
				{
					BattleParameterType = BattleParameterType.Hp, ChangeParameterType = ChangeParameterType.Addition, Value = ShoesHp
				},
				_ => throw new ArgumentOutOfRangeException(nameof(slotType), slotType, null)
			};
		}
	}
}
