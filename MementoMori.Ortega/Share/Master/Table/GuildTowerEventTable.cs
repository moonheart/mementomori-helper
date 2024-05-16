using MementoMori.Ortega.Share.Master.Data;
using MementoMori.Ortega.Share.Utils;

namespace MementoMori.Ortega.Share.Master.Table
{
	public class GuildTowerEventTable : TableBase<GuildTowerEventMB>
	{
		public GuildTowerEventMB? GetByInTime(Func<IHasStartEndTime, bool> func)
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

		public GuildTowerEventMB GetByInDisplayTime(OrtegaTimeManager timeManager)
		{
			// int num = 0;
			// bool flag;
			// bool flag2;
			// bool flag3;
			// if (!flag && (!flag2 || !flag3 || !timeManager.IsInTime(num, num)))
			// {
			// 	num++;
			// }
			throw new IndexOutOfRangeException();
		}
	}
}
