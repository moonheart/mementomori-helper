using MementoMori.Ortega.Share.Master.Data;

namespace MementoMori.Ortega.Share.Master.Table
{
	public class EquipmentMatchlessSacredTreasureTable : TableBase<EquipmentMatchlessSacredTreasureMB>
	{
		public EquipmentMatchlessSacredTreasureMB GetByLevel(long level)
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

		public EquipmentMatchlessSacredTreasureTable()
		{
		}
	}
}
