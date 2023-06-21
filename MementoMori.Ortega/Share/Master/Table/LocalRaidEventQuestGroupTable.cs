using MementoMori.Ortega.Share.Master.Data;

namespace MementoMori.Ortega.Share.Master.Table
{
	public class LocalRaidEventQuestGroupTable : TableBase<LocalRaidEventQuestGroupMB>
	{
		public LocalRaidEventQuestGroupMB GetByLocalNowTimeAndWorldCreatedTime(IReadOnlyList<long> eventQuestGroupIds, long localNowTimeStamp, long worldCreatedTime)
		{
			// int num;
			// int num2;
			// List<LocalRaidEventQuestGroupMB> list2;
			// do
			// {
			// 	num = 0;
			// 	List<LocalRaidEventQuestGroupMB> list = new List();
			// 	num2 = 0;
			// 	bool flag;
			// 	if (flag)
			// 	{
			// 	}
			// 	num2++;
			// 	Func<LocalRaidEventQuestGroupMB, long> func;
			// 	if (LocalRaidEventQuestGroupTable.<>c.<>9__0_0 == 0)
			// 	{
			// 		LocalRaidEventQuestGroupTable.<>c.<>9__0_0 = func;
			// 	}
			// 	list2 = Enumerable.ToList<LocalRaidEventQuestGroupMB>(Enumerable.OrderBy<LocalRaidEventQuestGroupMB, long>(list, func));
			// 	bool flag2;
			// 	if (flag2)
			// 	{
			// 		long num3;
			// 		num3 += worldCreatedTime;
			// 		if (num2 != 0)
			// 		{
			// 		}
			// 	}
			// }
			// while (num2 != 0 || num != 0);
			// return Enumerable.Last<LocalRaidEventQuestGroupMB>(list2);
			throw new NotImplementedException();
		}

		public LocalRaidEventQuestGroupMB GetByLocalNowTimeAndWorldCreatedTimeAndOverDayFromStartEventTime(IReadOnlyList<long> eventQuestGroupIds, long localNowTimeStamp, long worldCreatedTime, long overDayFromStartEventTime)
		{
			// int num;
			// int num2;
			// List<LocalRaidEventQuestGroupMB> list2;
			// do
			// {
			// 	num = 0;
			// 	List<LocalRaidEventQuestGroupMB> list = new List();
			// 	num2 = 0;
			// 	bool flag;
			// 	if (flag)
			// 	{
			// 	}
			// 	num2++;
			// 	Func<LocalRaidEventQuestGroupMB, long> func;
			// 	if (LocalRaidEventQuestGroupTable.<>c.<>9__1_0 == 0)
			// 	{
			// 		LocalRaidEventQuestGroupTable.<>c.<>9__1_0 = func;
			// 	}
			// 	list2 = Enumerable.ToList<LocalRaidEventQuestGroupMB>(Enumerable.OrderBy<LocalRaidEventQuestGroupMB, long>(list, func));
			// 	bool flag2;
			// 	if (!flag2 || num2 != 0)
			// 	{
			// 	}
			// }
			// while (num2 != 0 || num != 0);
			// return Enumerable.Last<LocalRaidEventQuestGroupMB>(list2);
			throw new NotImplementedException();
		}

		public LocalRaidEventQuestGroupTable()
		{
		}
	}
}
