using MementoMori.Ortega.Share.Master.Data;

namespace MementoMori.Ortega.Share.Master.Table
{
	public class GuildTowerFloorTable : TableBase<GuildTowerFloorMB>
	{
		public GuildTowerFloorMB GetByEventIdAndFloor(long eventId, int floor)
		{
			return _datas.FirstOrDefault(d => d.EventNo == eventId && d.FloorCount == floor);
		}

		public List<GuildTowerFloorMB> GetListByEventId(long eventId)
		{
			return _datas.Where(d => d.EventNo == eventId).ToList();
		}

		public int GetMaxFloor(long eventId)
		{
			return _datas.Where(d => d.EventNo == eventId).MaxBy(d => d.FloorCount).FloorCount;
		}

		public GuildTowerFloorMB GetMinFloor(long eventId)
        {
            return _datas.Where(d => d.EventNo == eventId).MinBy(d => d.FloorCount);
        }
	}
}
