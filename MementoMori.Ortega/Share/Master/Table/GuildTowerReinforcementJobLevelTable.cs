using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Data;

namespace MementoMori.Ortega.Share.Master.Table
{
	public class GuildTowerReinforcementJobLevelTable : TableBase<GuildTowerReinforcementJobLevelMB>
	{
		public GuildTowerReinforcementJobLevelMB GetByEventNoJobFlagsLevel(int eventNo, JobFlags jobFlags, int level)
		{
			return _datas.FirstOrDefault(d => d.EventNo == eventNo && d.JobFlags == jobFlags && d.Level == level);
		}
	}
}
