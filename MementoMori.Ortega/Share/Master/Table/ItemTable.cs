using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Data;

namespace MementoMori.Ortega.Share.Master.Table
{
	public class ItemTable : TableBase<ItemMB>
	{
		public ItemMB GetByItemTypeAndItemId(ItemType itemType, long itemId)
		{
			return _datas.FirstOrDefault(d => d.ItemType == itemType && d.ItemId == itemId);
			// int num = 0;
			// num++;
			// throw new NullReferenceException();
		}

		public ItemTable()
		{
		}
	}
}
