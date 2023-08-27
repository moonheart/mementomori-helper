using MementoMori.Ortega.Share.Master.Data;

namespace MementoMori.Ortega.Share.Master.Table
{
	public class EquipmentReinforcementParameterTable : TableBase<EquipmentReinforcementParameterMB>
	{
		public EquipmentReinforcementParameterMB GetByLevel(long level)
		{
			return _datas.FirstOrDefault(d => d.Id == level);
		}

		public EquipmentReinforcementParameterTable()
		{
		}
	}
}
