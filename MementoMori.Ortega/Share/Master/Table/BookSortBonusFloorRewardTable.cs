using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Master.Data;

namespace MementoMori.Ortega.Share.Master.Table
{
    public class BookSortBonusFloorRewardTable : TableBase<BookSortBonusFloorRewardMB>
    {
        public BookSortBonusFloorRewardMB GetByFloor(int floor)
        {
            var array = GetArray();
            if (array == null) return null;

            foreach (var item in array)
            {
                if (item == null) continue;
                if (floor >= item.StartFloor && (item.EndFloor == 0 || floor <= item.EndFloor))
                {
                    return item;
                }
            }
            return null;
        }

        public IReadOnlyList<BookSortBonusFloorSelectItems> GetSelectItemsListByFloor(int floor)
        {
            var item = GetByFloor(floor);
            return item?.SelectItemsList;
        }
    }
}
