using MementoMori.Ortega.Share.Master.Data;
using MementoMori.Ortega.Share.Utils;

namespace MementoMori.Ortega.Share.Master.Table
{
	public class GuildMissionTable : TableBase<GuildMissionMB>
	{
		public GuildMissionMB GetCurrentGuildMissionMB(DateTime serverNow)
        {
            foreach (var guildMissionMb in this._datas)
            {
                var startTime = guildMissionMb.StartTime.ToDateTime();
                var endTime = guildMissionMb.EndTime.ToDateTime();
                if (startTime <= serverNow && serverNow <= endTime)
                {
                    return guildMissionMb;
                }
            }
            return null;
        }
	}
}
