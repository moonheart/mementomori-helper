using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Equipment;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[Description("聖装テーブル")]
	[MessagePackObject(true)]
	public class EquipmentLegendSacredTreasureMB : MasterBookBase
	{
		[Description("メイル：魔法クリティカル抵抗（加算式増加）")]
		[PropertyOrder(7)]
		public double ArmorMagicCriticalDamageRelaxPercent { get; }

		[Description("アーム：クリティカルダメージ（加算式増加）")]
		[PropertyOrder(5)]
		public double GauntletCriticalDamagePercent { get; }

		[Description("メット：物理クリティカル抵抗（加算式増加）")]
		[PropertyOrder(6)]
		public double HelmetPhysicalCriticalDamageRelaxPercent { get; }

		[Description("聖装レベル")]
		[PropertyOrder(1)]
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

		[Description("ブーツ：HPドレイン（加算式増加）")]
		[PropertyOrder(8)]
		public double ShoesHpDrainPercent { get; }

		[Description("アクセサリー：命中（割合増加）")]
		[PropertyOrder(4)]
		public double SubHitPercent { get; }

		[Description("武器：攻撃力（割合増加）")]
		[PropertyOrder(3)]
		public double WeaponAttackPowerPercent { get; }

		[SerializationConstructor]
		public EquipmentLegendSacredTreasureMB(long id, bool? isIgnore, string memo, long lv, long requiredTotalExp, double weaponAttackPowerPercent, double subHitPercent, double gauntletCriticalDamagePercent, double helmetPhysicalCriticalDamageRelaxPercent, double armorMagicCriticalDamageRelaxPercent, double shoesHpDrainPercent)
			:base(id, isIgnore, memo)
		{
			Lv = lv;
			RequiredTotalExp = requiredTotalExp;
			WeaponAttackPowerPercent = weaponAttackPowerPercent;
			SubHitPercent = subHitPercent;
			GauntletCriticalDamagePercent = gauntletCriticalDamagePercent;
			HelmetPhysicalCriticalDamageRelaxPercent = helmetPhysicalCriticalDamageRelaxPercent;
			ArmorMagicCriticalDamageRelaxPercent = armorMagicCriticalDamageRelaxPercent;
			ShoesHpDrainPercent = shoesHpDrainPercent;
		}

		public EquipmentLegendSacredTreasureMB() : base(0, false, "")
		{
		}

		public BattleParameterChangeInfo GetValue(EquipmentSlotType slotType)
		{
			if (slotType - EquipmentSlotType.Weapon <= 5)
			{
				BattleParameterChangeInfo battleParameterChangeInfo = new BattleParameterChangeInfo();
				battleParameterChangeInfo.BattleParameterType = (BattleParameterType)((ulong)2L);
				battleParameterChangeInfo.ChangeParameterType = (ChangeParameterType)((ulong)2L);
				double num = this.WeaponAttackPowerPercent;
				battleParameterChangeInfo.Value = num;
				return battleParameterChangeInfo;
			}
			throw new NullReferenceException();
		}
	}
}
