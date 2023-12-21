using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Data;

namespace MementoMori.Ortega.Share.Master.Table
{
	public class EquipmentLegendSacredTreasureTable : TableBase<EquipmentLegendSacredTreasureMB>
	{
		public EquipmentLegendSacredTreasureMB GetByLevel(long level)
		{
			foreach (var data in _datas)
			{
				if (data.Lv == level)
				{
					return data;
				}
			}

			return null;
		}

        public string GetByLvAndSlotType(long level, EquipmentSlotType slotType)
        {
            var treasureMb = GetByLevel(level);
            if (treasureMb == null)
            {
                return "0%";
            }

            var value = slotType switch
            {
                EquipmentSlotType.Weapon => treasureMb.WeaponAttackPowerPercent,
                EquipmentSlotType.Sub => treasureMb.SubHitPercent,
                EquipmentSlotType.Gauntlet => treasureMb.GauntletCriticalDamagePercent,
                EquipmentSlotType.Helmet => treasureMb.HelmetPhysicalCriticalDamageRelaxPercent,
                EquipmentSlotType.Armor => treasureMb.ArmorMagicCriticalDamageRelaxPercent,
                EquipmentSlotType.Shoes => treasureMb.ShoesHpDrainPercent,
                _ => 0
            };

            return (value / 100).ToString("P");
        }

		public EquipmentLegendSacredTreasureTable()
		{
		}
	}
}
