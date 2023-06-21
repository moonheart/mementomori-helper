using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Data;

namespace MementoMori.Ortega.Share.Master.Table
{
	public class EquipmentTable : TableBase<EquipmentMB>
	{
		public List<EquipmentMB> GetListByRarityAndEquipmentEvolutionId(EquipmentRarityFlags rarityFlags, long equipmentEvolutionId)
		{
			// List<EquipmentMB> list = new List();
			// int num = 0;
			// num++;
			// return list;
			throw new NotImplementedException();
		}

		public EquipmentMB GetByAfterRarityEvolutionEquipmentId(long equipmentId)
		{
			int num = 0;
			num++;
			throw new NullReferenceException();
		}

		public EquipmentMB GetAfterEvolutionEquipmentMB(EquipmentMB currentEquipmentMB, EvolutionType evolutionType)
		{
			if (evolutionType == EvolutionType.ReinforcementLevelMaximumUp)
			{
			}
			long AfterRarityEvolutionEquipmentId = currentEquipmentMB.AfterRarityEvolutionEquipmentId;
			return base.GetById(AfterRarityEvolutionEquipmentId);
		}

		public EquipmentMB GetCastingResultEquipmentMB(EquipmentMB baseEquipmentMB, EquipmentForgeMB forgeMB)
		{
			// EquipmentTable EquipmentTable = Masters.EquipmentTable;
			// int num = 0;
			// bool? IsIgnore = forgeMB.IsIgnore;
			// if (num < IsIgnore)
			// {
			// 	EquipmentRarityFlags SuccessRarityFlag = forgeMB.SuccessRarityFlag;
			// 	EquipmentSlotType SlotType = baseEquipmentMB.SlotType;
			// 	JobFlags EquippedJobFlags = baseEquipmentMB.EquippedJobFlags;
			// 	long EquipmentLv = baseEquipmentMB.EquipmentLv;
			// 	int QualityLv = baseEquipmentMB.QualityLv;
			// 	num++;
			// }
			// throw new NullReferenceException();
			throw new NotImplementedException();
		}

		public EquipmentTable()
		{
		}
	}
}
