using MementoMori.Ortega.Share.Master.Data;

namespace MementoMori.Ortega.Share.Master.Table
{
	public class EquipmentExclusiveEffectTable : TableBase<EquipmentExclusiveEffectMB>
	{
		public List<EquipmentExclusiveEffectMB> GetByCharacterId(long characterId)
		{
			// List<EquipmentExclusiveEffectMB> list = new List();
			// int num = 0;
			// num++;
			// return list;
			throw new NotImplementedException();
		}

		public EquipmentExclusiveEffectMB GetByIdAndCharacterId(long id, long characterId)
		{
			return _datas.FirstOrDefault(d => d.Id == id && d.CharacterId == characterId);
		}

		public EquipmentExclusiveEffectTable()
		{
		}
	}
}
