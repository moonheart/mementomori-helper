using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Data;

namespace MementoMori.Ortega.Share.Master.Table
{
	public class FriendMissionTable : TableBase<FriendMissionMB>
	{
		public List<FriendMissionMB> GetMbsByDateTimeAndMissionAchievementType(DateTime localDateTime, MissionAchievementType missionAchievementType)
		{
			// List<FriendMissionMB> list = new List();
			// int num = 0;
			// DateTime dateTime;
			// DateTime dateTime2;
			// if (string.IsNullOrEmpty(num) || string.IsNullOrEmpty(num) || dateTime > localDateTime || !(dateTime2 < localDateTime))
			// {
			// }
			// num++;
			// return list;
			throw new NotImplementedException();
		}

		public FriendMissionTable()
		{
		}
	}
}
