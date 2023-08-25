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

		public EquipmentLegendSacredTreasureTable()
		{
		}
	}
}
