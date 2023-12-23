using MementoMori.Ortega.Share.Master.Data;

namespace MementoMori.Ortega.Share.Master.Table
{
    public class LimitedLoginBonusTable : TableBase<LimitedLoginBonusMB>
    {
        // public LimitedLoginBonusMB GetByInTime(OrtegaTimeManager timeManager)
        // {
        // 	int num = 0;
        // 	bool flag;
        // 	if (!flag)
        // 	{
        // 		num++;
        // 	}
        // 	throw new NullReferenceException();
        // }

        public LimitedLoginBonusMB GetByInDelayTime(OrtegaTimeManager timeManager)
        {
            // int num = 0;
            // DateTime dateTime;
            // DateTime dateTime2;
            // if (!timeManager.IsInTime(dateTime, dateTime2))
            // {
            //     num++;
            // }
            throw new NullReferenceException();
        }

        public QuestMB GetFirstQuestMBByChapterId(long chapterId)
        {
            int num = 0;
            num++;
            throw new NullReferenceException();
        }
    }
}