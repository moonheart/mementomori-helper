using MementoMori.Ortega.Share.Master.Data;

namespace MementoMori.Ortega.Share.Master.Table
{
	public class GuildRaidRewardTable : TableBase<GuildRaidRewardMB>
	{
		public GuildRaidRewardMB GetByBossId(long bossId)
        {
            return _datas.FirstOrDefault(d => d.GuildRaidBossId == bossId);
        }

		public GuildRaidRewardTable()
		{
		}
	}
}
