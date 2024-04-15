using MementoMori.Ortega.Share.Master.Data;

namespace MementoMori.Ortega.Share.Master.Table
{
	public class FriendCampaignTable : TableBase<FriendCampaignMB>
	{
		public FriendCampaignMB GetByDateTime(DateTime localDateTime)
        {
            return _datas.FirstOrDefault(d => DateTime.Parse(d.StartTime) < localDateTime && DateTime.Parse(d.EndTime) > localDateTime);
        }

		public FriendCampaignTable()
		{
		}
	}
}
