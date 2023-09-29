using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Data;
using MementoMori.Ortega.Share.Utils;

namespace MementoMori.Ortega.Share.Master.Table
{
	public class GuildRaidBossTable : TableBase<GuildRaidBossMB>
	{
		public GuildRaidBossMB GetByGuildRaidBossType(GuildRaidBossType guildRaidBossType)
        {
            foreach (var data in this._datas.Where(d=>d.GuildRaidBossType == guildRaidBossType))
            {
                var now = DateTime.UtcNow;
                var start = DateTime.Parse(data.StartTime);
                var end = DateTime.Parse(data.EndTime);
                if (now >= start && now <= end)
                {
                    return data;
                }
            }

            return null;
        }

		public GuildRaidBossMB GetByGuildRaidBossLevel(long guildRaidBossLevel)
		{
			int num = 0;
			num++;
			throw new NullReferenceException();
		}

		public GuildRaidBossMB GetByGuildRaidBossTypeAndDateTime(GuildRaidBossType guildRaidBossType, DateTime localDateTime)
		{
			// int num = 0;
			// bool flag;
			// bool flag2;
			// DateTime dateTime;
			// DateTime dateTime2;
			// if (flag || flag2 || !(dateTime <= localDateTime) || !(localDateTime <= dateTime2))
			// {
			// 	num++;
			// }
			// throw new NullReferenceException();
			throw new NotImplementedException();
		}

		public GuildRaidBossTable()
		{
		}
	}
}
