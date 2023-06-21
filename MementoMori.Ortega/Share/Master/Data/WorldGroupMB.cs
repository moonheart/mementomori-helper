using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Time;
using MementoMori.Ortega.Share.Master.Attributes;
using MementoMori.Ortega.Share.Utils;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[Description("ワールドグループ")]
	[MessagePackObject(true)]
	public class WorldGroupMB : MasterBookBase, IHasStartEndTime
	{
		[Description("終了日時")]
		[PropertyOrder(5)]
		public string EndTime
		{
			get;
		}

		[PropertyOrder(9)]
		[Description("レジェンドリーグ終了日時")]
		public string EndLegendLeagueDateTime
		{
			get;
		}

		[PropertyOrder(1)]
		[Description("タイムサーバー")]
		public long TimeServerId
		{
			get;
		}

		[PropertyOrder(4)]
		[Description("開始日時")]
		public string StartTime
		{
			get;
		}

		[Description("グランドバトル開始日時")]
		[PropertyOrder(6)]
		[Nest(false, 0)]
		public IReadOnlyList<StartEndTime> GrandBattleDateTimeList
		{
			get;
		}

		[Description("レジェンドリーグ開始日時")]
		[PropertyOrder(8)]
		public string StartLegendLeagueDateTime
		{
			get;
		}

		[PropertyOrder(3)]
		[Description("WorldIdのリスト")]
		public IReadOnlyList<long> WorldIdList
		{
			get;
		}

		[SerializationConstructor]
		public WorldGroupMB(long id, bool? isIgnore, string memo, string endTime, long timeServerId, string startTime, IReadOnlyList<long> worldIdList, IReadOnlyList<StartEndTime> grandBattleDateTimeList, string startLegendLeagueDateTime, string endLegendLeagueDateTime)
			:base(id, isIgnore, memo)
		{
			EndTime = endTime;
			TimeServerId = timeServerId;
			StartTime = startTime;
			WorldIdList = worldIdList;
			GrandBattleDateTimeList = grandBattleDateTimeList;
			StartLegendLeagueDateTime = startLegendLeagueDateTime;
			EndLegendLeagueDateTime = endLegendLeagueDateTime;
		}

		public WorldGroupMB() :base(0L, false, ""){}

		public bool IsActiveGroup(DateTime localDateTime)
		{
			bool flag = this.StartTime.ToDateTime() <= localDateTime;
			if (!flag)
			{
				return flag;
			}
			DateTime dateTime = this.EndTime.ToDateTime();
			return localDateTime < dateTime;
		}

		public bool IsOpenedGrandBattle(DateTime localDateTime)
		{
			// for (;;)
			// {
			// 	IReadOnlyList<StartEndTime> readOnlyList = this.GrandBattleDateTimeList;
			// 	if (typeof(StringExtension).TypeHandle != 0)
			// 	{
			// 		DateTime dateTime;
			// 		DateTime dateTime2;
			// 		if (dateTime <= localDateTime && localDateTime < dateTime2)
			// 		{
			// 			break;
			// 		}
			// 	}
			// 	else
			// 	{
			// 		if ("{il2cpp array field local6->}" != (ulong)0L)
			// 		{
			// 		}
			// 		ulong num;
			// 		if (num == (ulong)0L)
			// 		{
			// 			goto Block_3;
			// 		}
			// 	}
			// }
			// return true;
			// Block_3:
			// throw new NullReferenceException();
			throw new NotImplementedException();
		}

		public bool IsOpenedLegendLeague(DateTime localDateTime)
		{
			bool flag = this.StartLegendLeagueDateTime.ToDateTime() <= localDateTime;
			if (!flag)
			{
				return flag;
			}
			DateTime dateTime = this.EndLegendLeagueDateTime.ToDateTime();
			return localDateTime < dateTime;
		}

		public bool IsAfterStartGrandBattle(DateTime localDateTime)
		{
			// for (;;)
			// {
			// 	IReadOnlyList<StartEndTime> readOnlyList = this.GrandBattleDateTimeList;
			// 	if (typeof(StringExtension).TypeHandle != 0)
			// 	{
			// 		DateTime dateTime;
			// 		if (dateTime <= localDateTime)
			// 		{
			// 			break;
			// 		}
			// 	}
			// 	else
			// 	{
			// 		if ("{il2cpp array field local6->}" != (ulong)0L)
			// 		{
			// 		}
			// 		ulong num;
			// 		if (num == (ulong)0L)
			// 		{
			// 			goto Block_2;
			// 		}
			// 	}
			// }
			// return true;
			// Block_2:
			// throw new NullReferenceException();
			throw new NotImplementedException();
		}
	}
}
