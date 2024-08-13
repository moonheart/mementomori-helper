using MementoMori.Ortega.Share.Master.Data;
using MementoMori.Ortega.Share.Utils;

namespace MementoMori.Ortega.Share.Master.Table
{
    public class BountyQuestEventTable : TableBase<BountyQuestEventMB>
    {
        public BountyQuestEventMB? GetByInTime(Func<IHasStartEndTime, bool> func)
        {
            foreach (var mb in _datas)
            {
                if (func(mb))
                {
                    return mb;
                }
            }

            return null;
        }

        public BountyQuestEventMB GetByTimeStamp(long localNowTimeStamp)
        {
            // DateTime UtcEpoch = TimeUtil.UtcEpoch;
            // int num = 0;
            // DateTime dateTime;
            // DateTime dateTime2;
            // DateTime dateTime3;
            // if (!(dateTime <= dateTime2) || !(dateTime2 <= dateTime3))
            // {
            // 	num++;
            // }
            throw new NullReferenceException();
        }

        public BountyQuestEventTable()
        {
        }
    }
}