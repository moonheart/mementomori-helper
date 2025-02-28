using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Data;

namespace MementoMori.Ortega.Share.Master.Table
{
    public class GachaSelectListTable : TableBase<GachaSelectListMB>
    {
        public GachaSelectListMB GetByGachaSelectListTypeAndJstDateTime(GachaSelectListType gachaSelectListType, DateTime jstNowDateTime)
        {
            foreach (var item in _datas.Where(d => d.GachaSelectListType == gachaSelectListType).OrderByDescending(d => d.OrderNumber))
            {
                var startTime = DateTime.Parse(item.StartTimeFixJST);
                var endTime = DateTime.Parse(item.EndTimeFixJST);
                if (startTime <= jstNowDateTime && jstNowDateTime <= endTime)
                {
                    return item;
                }
            }

            // List<GachaSelectListMB> list = new List();
            // int num = 0;
            // DateTime dateTime;
            // DateTime dateTime2;
            // if (!(dateTime <= jstNowDateTime) || jstNowDateTime <= dateTime2)
            // {
            // }
            // num++;
            // Func<GachaSelectListMB, int> func;
            // if (GachaSelectListTable.<>c.<>9__0_0 == 0)
            // {
            // 	GachaSelectListTable.<>c.<>9__0_0 = func;
            // }
            // return Enumerable.FirstOrDefault<GachaSelectListMB>(Enumerable.ToList<GachaSelectListMB>(Enumerable.OrderByDescending<GachaSelectListMB, int>(list, func)));
            throw new NotImplementedException();
        }

        public GachaSelectListTable()
        {
        }
    }
}