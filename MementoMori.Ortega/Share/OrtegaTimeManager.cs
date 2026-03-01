using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Data;
using MementoMori.Ortega.Share.Utils;

namespace MementoMori.Ortega.Share
{
	public class OrtegaTimeManager : ILocalTime
	{
		public OrtegaTimeManager()
		{
			DifferenceFromUtc = 0L;
		}

		private const long JstDifferenceTimeStampFromUtc = 9L * 60L * 60L * 1000L;

		public OrtegaTimeManager(TimeServerMB timeServerMB)
		{
			if (timeServerMB == null)
			{
				throw new ArgumentNullException(nameof(timeServerMB));
			}

			DifferenceFromUtc = (long)TimeSpan.Parse(timeServerMB.DifferenceDateTimeFromUtc).TotalMilliseconds;
		}

		public long DifferenceFromUtc { get; private set; }

		public void SetDifferenceFromUtc(TimeServerMB timeServerMB)
		{
			if (timeServerMB == null)
			{
				throw new ArgumentNullException(nameof(timeServerMB));
			}

			if (!string.IsNullOrEmpty(timeServerMB.DifferenceDateTimeFromUtc))
			{
				TimeSpan timeSpan = TimeSpan.Parse(timeServerMB.DifferenceDateTimeFromUtc);
				DifferenceFromUtc = (long)timeSpan.TotalMilliseconds;
			}
		}

		public long GetLocalTimestamp()
		{
			return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + DifferenceFromUtc;
		}

		public long GetLocalTimestamp(StartEndTimeZoneType type, DateTime dateTime, bool isStartTime)
		{
			if (isStartTime)
			{
				if (type != StartEndTimeZoneType.LocalStartLocalEnd && type != StartEndTimeZoneType.LocalStartJstEnd)
				{
					dateTime = ConvertJstDateTImeToLocalDateTime(dateTime);
				}
			}
			else
			{
				if (type != StartEndTimeZoneType.LocalStartLocalEnd && type != StartEndTimeZoneType.JstStartLocalEnd)
				{
					dateTime = ConvertJstDateTImeToLocalDateTime(dateTime);
				}
			}

			return ConvertDateTimeToTimeStamp(dateTime);
		}

		public DateTime GetLocalDateTime()
		{
			return TimeUtil.UtcEpoch.AddMilliseconds(GetLocalTimestamp());
		}

		public long GetNDaysLaterChangeDayTimeStamp(long nDay, long timestamp = -1L)
		{
			if (timestamp == -1L)
			{
				timestamp = GetLocalTimestamp();
			}

			DateTime dateTime = TimeUtil.UtcEpoch.AddMilliseconds(timestamp);
			DateTime baseDate = (dateTime - TimeUtil.ChangeDayTime).Date;
			DateTime changeDayDateTime = baseDate.AddDays(nDay) + TimeUtil.ChangeDayTime;
			return ConvertDateTimeToTimeStamp(changeDayDateTime);
		}

		public long GetLocalYesterdayChangeDayTimeStamp()
		{
			DateTime baseDate = (GetLocalDateTime() - TimeUtil.ChangeDayTime).Date;
			DateTime changeDayDateTime = baseDate.AddDays(-1.0) + TimeUtil.ChangeDayTime;
			return ConvertDateTimeToTimeStamp(changeDayDateTime);
		}

		public long GetLocalTomorrowChangeDayTimeStamp()
		{
			DateTime baseDate = (GetLocalDateTime() - TimeUtil.ChangeDayTime).Date;
			DateTime changeDayDateTime = baseDate.AddDays(1.0) + TimeUtil.ChangeDayTime;
			return ConvertDateTimeToTimeStamp(changeDayDateTime);
		}

		public long GetLocalTodayChangeDayTimeStamp()
		{
			DateTime baseDate = (GetLocalDateTime() - TimeUtil.ChangeDayTime).Date;
			DateTime changeDayDateTime = baseDate + TimeUtil.ChangeDayTime;
			return ConvertDateTimeToTimeStamp(changeDayDateTime);
		}

		public long GetLocalNextChangeDayTime(long localTime)
		{
			long changeDayMilliseconds = (long)TimeUtil.ChangeDayTime.TotalMilliseconds;
			DateTime dateTime = TimeUtil.UtcEpoch.AddMilliseconds(localTime - changeDayMilliseconds);
			DateTime nextChangeDayDateTime = dateTime.Date.AddDays(1.0) + TimeUtil.ChangeDayTime;
			return ConvertDateTimeToTimeStamp(nextChangeDayDateTime);
		}

		public long GetLocalLastMondayTimeStamp()
		{
			DateTime baseDate = (GetLocalDateTime() - TimeUtil.ChangeDayTime).Date;
			int diff = 1 - (int)baseDate.DayOfWeek;
			if (diff > 0)
			{
				diff -= 7;
			}

			DateTime mondayDateTime = baseDate.AddDays(diff) + TimeUtil.ChangeDayTime;
			return ConvertDateTimeToTimeStamp(mondayDateTime);
		}

		public long GetLocalLastMondayTimeStamp(long timestamp)
		{
			DateTime dateTime = TimeUtil.UtcEpoch.AddMilliseconds(timestamp);
			DateTime baseDate = (dateTime - TimeUtil.ChangeDayTime).Date;
			int diff = 1 - (int)baseDate.DayOfWeek;
			if (diff > 0)
			{
				diff -= 7;
			}

			DateTime mondayDateTime = baseDate.AddDays(diff) + TimeUtil.ChangeDayTime;
			return ConvertDateTimeToTimeStamp(mondayDateTime);
		}

		public long GetLocalNextMondayTimeStamp()
		{
			DateTime baseDate = (GetLocalDateTime() - TimeUtil.ChangeDayTime).Date;
			int diff = 1 - (int)baseDate.DayOfWeek;
			if (diff <= 0)
			{
				diff += 7;
			}

			DateTime mondayDateTime = baseDate.AddDays(diff) + TimeUtil.ChangeDayTime;
			return ConvertDateTimeToTimeStamp(mondayDateTime);
		}

		public long GetLocalNextMondayTimeStamp(long timestamp)
		{
			DateTime dateTime = TimeUtil.UtcEpoch.AddMilliseconds(timestamp);
			DateTime baseDate = (dateTime - TimeUtil.ChangeDayTime).Date;
			int diff = 1 - (int)baseDate.DayOfWeek;
			if (diff <= 0)
			{
				diff += 7;
			}

			DateTime mondayDateTime = baseDate.AddDays(diff) + TimeUtil.ChangeDayTime;
			return ConvertDateTimeToTimeStamp(mondayDateTime);
		}

		public long GetLocalNextDayOfWeekTimeStamp(DayOfWeek dayOfWeek)
		{
			DateTime baseDate = (GetLocalDateTime() - TimeUtil.ChangeDayTime).Date;
			int diff = (int)dayOfWeek - (int)baseDate.DayOfWeek;
			if (diff <= 0)
			{
				diff += 7;
			}

			DateTime dayOfWeekDateTime = baseDate.AddDays(diff) + TimeUtil.ChangeDayTime;
			return ConvertDateTimeToTimeStamp(dayOfWeekDateTime);
		}

		public long GetGrandBattleStartBattleLocalTimestamp()
		{
			DateTime baseDate = (GetLocalDateTime() - TimeUtil.ChangeDayTime).Date;
			DateTime startDateTime = baseDate
				.AddHours(OrtegaConst.GlobalGvg.StartHour)
				.AddMinutes(OrtegaConst.GlobalGvg.StartMinute);
			return ConvertDateTimeToTimeStamp(startDateTime);
		}

		public long GetGuildBattleStartBattleLocalTimestamp()
		{
			DateTime baseDate = (GetLocalDateTime() - TimeUtil.ChangeDayTime).Date;
			DateTime startDateTime = baseDate
				.AddHours(OrtegaConst.LocalGvg.StartHour)
				.AddMinutes(OrtegaConst.LocalGvg.StartMinute);
			return ConvertDateTimeToTimeStamp(startDateTime);
		}

		public long GetLocalNextMonthFirstDayTimeStamp()
		{
			DateTime dateTime = GetLocalDateTime() - TimeUtil.ChangeDayTime;
			DateTime firstDayOfNextMonth = new DateTime(dateTime.Year, dateTime.Month, 1).AddMonths(1);
			DateTime changeDayDateTime = firstDayOfNextMonth + TimeUtil.ChangeDayTime;
			return ConvertDateTimeToTimeStamp(changeDayDateTime);
		}

		public long GetLocalCurrentMonthFirstDayTimeStamp()
		{
			DateTime dateTime = GetLocalDateTime() - TimeUtil.ChangeDayTime;
			DateTime firstDayOfCurrentMonth = new DateTime(dateTime.Year, dateTime.Month, 1);
			DateTime changeDayDateTime = firstDayOfCurrentMonth + TimeUtil.ChangeDayTime;
			return ConvertDateTimeToTimeStamp(changeDayDateTime);
		}

		public long GetLocalTodayTimestamp(long hour, long minute)
		{
			DateTime dateTime = GetLocalDateTime().Date.AddHours(hour).AddMinutes(minute);
			return ConvertDateTimeToTimeStamp(dateTime);
		}

		public DateTime GetChangeDayLocalDateTime()
		{
			DateTime baseDate = (GetLocalDateTime() - TimeUtil.ChangeDayTime).Date;
			return baseDate.AddDays(1.0).AddHours(TimeUtil.ChangeDayTime.Hours);
		}

		public bool IsChangeDayByChangeDayTime(long timestamp)
		{
			long changeDayMilliseconds = (long)TimeUtil.ChangeDayTime.TotalMilliseconds;
			long current = GetLocalTimestamp() - changeDayMilliseconds;
			long target = timestamp - changeDayMilliseconds;

			DateTime targetDate = DateTimeOffset.FromUnixTimeMilliseconds(target).Date;
			DateTime currentDate = DateTimeOffset.FromUnixTimeMilliseconds(current).Date;
			return targetDate < currentDate;
		}

		public DayOfWeek GetDayOfWeek()
		{
			return (GetLocalDateTime() - TimeUtil.ChangeDayTime).DayOfWeek;
		}

		public DateTime GetLocalTodayDateTime()
		{
			return GetLocalDateTime() - TimeUtil.ChangeDayTime;
		}

		public DateTime GetLocalGameDate()
		{
			return (GetLocalDateTime() - TimeUtil.ChangeDayTime).Date;
		}

		public DateTime GetLocalGameDate(long localTimeStamp)
		{
			DateTime dateTime = TimeUtil.UtcEpoch.AddMilliseconds(localTimeStamp);
			return (dateTime - TimeUtil.ChangeDayTime).Date;
		}

		public DateTime GetStartTime(IHasStartEndTimeZone data)
		{
			if (data == null)
			{
				throw new ArgumentNullException(nameof(data));
			}

			DateTime startTime = DateTime.Parse(data.StartTime);
			if (data.StartEndTimeZoneType != StartEndTimeZoneType.LocalStartLocalEnd &&
				data.StartEndTimeZoneType != StartEndTimeZoneType.LocalStartJstEnd)
			{
				startTime = ConvertJstDateTImeToLocalDateTime(startTime);
			}

			return startTime;
		}

		public DateTime GetEndTime(IHasStartEndTimeZone data)
		{
			if (data == null)
			{
				throw new ArgumentNullException(nameof(data));
			}

			return GetEndTime(data.StartEndTimeZoneType, data.EndTime);
		}

		public DateTime GetEndTime(StartEndTimeZoneType startEndTimeZoneTyp, string endTime)
		{
			DateTime parsedEndTime = DateTime.Parse(endTime);
			if (startEndTimeZoneTyp != StartEndTimeZoneType.LocalStartLocalEnd &&
				startEndTimeZoneTyp != StartEndTimeZoneType.JstStartLocalEnd)
			{
				parsedEndTime = ConvertJstDateTImeToLocalDateTime(parsedEndTime);
			}

			return parsedEndTime;
		}

		public (DateTime startTime, DateTime endTime) GetStartEndLocalDateTime(IHasStartEndTimeZone data)
		{
			if (data == null)
			{
				throw new ArgumentNullException(nameof(data));
			}

			if (!DateTime.TryParse(data.StartTime, out DateTime parsedStart) ||
				!DateTime.TryParse(data.EndTime, out DateTime parsedEnd))
			{
				return (DateTime.MinValue, DateTime.MinValue);
			}

			return data.StartEndTimeZoneType switch
			{
				StartEndTimeZoneType.LocalStartLocalEnd => (parsedStart, parsedEnd),
				StartEndTimeZoneType.LocalStartJstEnd => (parsedStart, ConvertJstDateTImeToLocalDateTime(parsedEnd)),
				StartEndTimeZoneType.JstStartLocalEnd => (ConvertJstDateTImeToLocalDateTime(parsedStart), parsedEnd),
				StartEndTimeZoneType.JstStartJstEnd =>
					(ConvertJstDateTImeToLocalDateTime(parsedStart), ConvertJstDateTImeToLocalDateTime(parsedEnd)),
				_ => (parsedStart, parsedEnd)
			};
		}

		public bool IsStarted(IHasStartEndTimeZone data, DateTime now)
		{
			if (data == null)
			{
				throw new ArgumentNullException(nameof(data));
			}

			DateTime startTime = DateTime.Parse(data.StartTime);
			if (data.StartEndTimeZoneType != StartEndTimeZoneType.LocalStartLocalEnd &&
				data.StartEndTimeZoneType != StartEndTimeZoneType.LocalStartJstEnd)
			{
				startTime = ConvertJstDateTImeToLocalDateTime(startTime);
			}

			return startTime <= now;
		}

		public bool IsEnded(IHasStartEndTimeZone data, DateTime now)
		{
			if (data == null)
			{
				throw new ArgumentNullException(nameof(data));
			}

			DateTime endTime = DateTime.Parse(data.EndTime);
			if (data.StartEndTimeZoneType != StartEndTimeZoneType.LocalStartLocalEnd &&
				data.StartEndTimeZoneType != StartEndTimeZoneType.JstStartLocalEnd)
			{
				endTime = ConvertJstDateTImeToLocalDateTime(endTime);
			}

			return endTime < now;
		}

		public bool IsEndByLocalTime(DateTime endTime)
		{
			return GetLocalDateTime() > endTime;
		}

		public bool IsInEventTime(IHasEventStartEndTime data)
		{
			DateTime now = GetLocalDateTime();
			if (data == null)
			{
				throw new ArgumentNullException(nameof(data));
			}

			if (string.IsNullOrEmpty(data.EventStartTime) || string.IsNullOrEmpty(data.EventEndTime))
			{
				return false;
			}

			DateTime startTime = DateTime.Parse(data.EventStartTime);
			if (startTime > now)
			{
				return false;
			}

			DateTime endTime = DateTime.Parse(data.EventEndTime);
			return now <= endTime;
		}

		public bool IsInTime(IHasStartEndTime data)
		{
			DateTime now = GetLocalDateTime();
			DateTime startTime = DateTime.Parse(data.StartTime);
			if (startTime > now)
			{
				return false;
			}

			DateTime endTime = DateTime.Parse(data.EndTime);
			return now <= endTime;
		}

		public bool IsInTime(IHasJstStartEndTime data)
		{
			if (data == null)
			{
				throw new ArgumentNullException(nameof(data));
			}

			if (string.IsNullOrEmpty(data.StartTimeFixJST) || string.IsNullOrEmpty(data.EndTimeFixJST))
			{
				return false;
			}

			DateTime jstNow = ConvertLocalDateTimeToJstDateTIme(GetLocalDateTime());
			DateTime startTime = DateTime.Parse(data.StartTimeFixJST);
			if (startTime > jstNow)
			{
				return false;
			}

			DateTime endTime = DateTime.Parse(data.EndTimeFixJST);
			return jstNow <= endTime;
		}

		public bool IsInTime(IHasStartEndTimeZone data)
		{
			return IsInTime(data, GetLocalDateTime());
		}

		public bool IsInTime(DateTime startTime, DateTime endTime)
		{
			DateTime now = GetLocalDateTime();
			if (now < startTime)
			{
				return false;
			}

			return now <= endTime;
		}

		public bool IsInTime(IHasStartEndTimeZone data, DateTime localDateTime)
		{
			DateTime jstDateTime = ConvertLocalDateTimeToJstDateTIme(localDateTime);
			DateTime startTime;
			DateTime endTime;
			DateTime startNow;
			DateTime endNow;

			switch (data.StartEndTimeZoneType)
			{
				case StartEndTimeZoneType.LocalStartLocalEnd:
					startTime = DateTime.Parse(data.StartTime);
					endTime = DateTime.Parse(data.EndTime);
					startNow = localDateTime;
					endNow = localDateTime;
					break;
				case StartEndTimeZoneType.LocalStartJstEnd:
					startTime = DateTime.Parse(data.StartTime);
					endTime = DateTime.Parse(data.EndTime);
					startNow = localDateTime;
					endNow = jstDateTime;
					break;
				case StartEndTimeZoneType.JstStartLocalEnd:
					startTime = DateTime.Parse(data.StartTime);
					endTime = DateTime.Parse(data.EndTime);
					startNow = jstDateTime;
					endNow = localDateTime;
					break;
				case StartEndTimeZoneType.JstStartJstEnd:
					startTime = DateTime.Parse(data.StartTime);
					endTime = DateTime.Parse(data.EndTime);
					startNow = jstDateTime;
					endNow = jstDateTime;
					break;
				default:
					startTime = DateTime.Parse(data.StartTime);
					endTime = DateTime.Parse(data.EndTime);
					startNow = localDateTime;
					endNow = localDateTime;
					break;
			}

			if (startTime > startNow)
			{
				return false;
			}

			return endNow <= endTime;
		}

		public bool IsInTimeByHourAndMinuteAndSecond(long startTime, long endTime, long timestamp)
		{
			DateTime dateTime = TimeUtil.UtcEpoch.AddMilliseconds(timestamp);
			int now = (dateTime.Hour * 100 + dateTime.Minute) * 100 + dateTime.Second;
			return startTime <= now && now <= endTime;
		}

		public bool IsInTimeByHourAndMinuteAndSecond(long startTime, long endTime)
		{
			DateTime dateTime = GetLocalDateTime();
			int now = (dateTime.Hour * 100 + dateTime.Minute) * 100 + dateTime.Second;
			return startTime <= now && now <= endTime;
		}

		public IHasStartEndTime GetInTimeData(IReadOnlyList<IHasStartEndTime> datas)
		{
			DateTime now = GetLocalDateTime();
			foreach (IHasStartEndTime data in datas)
			{
				DateTime startTime = DateTime.Parse(data.StartTime);
				if (startTime > now)
				{
					continue;
				}

				DateTime endTime = DateTime.Parse(data.EndTime);
				if (now <= endTime)
				{
					return data;
				}
			}

			return null;
		}

		public IHasStartEndTimeZone GetInTimeData(IReadOnlyList<IHasStartEndTimeZone> datas)
		{
			foreach (IHasStartEndTimeZone data in datas)
			{
				if (IsInTime(data))
				{
					return data;
				}
			}

			return null;
		}

		public IHasJstStartEndTime GetInTimeData(IReadOnlyList<IHasJstStartEndTime> datas)
		{
			DateTime jstNow = ConvertLocalDateTimeToJstDateTIme(GetLocalDateTime());
			foreach (IHasJstStartEndTime data in datas)
			{
				DateTime startTime = DateTime.Parse(data.StartTimeFixJST);
				if (startTime > jstNow)
				{
					continue;
				}

				DateTime endTime = DateTime.Parse(data.EndTimeFixJST);
				if (jstNow <= endTime)
				{
					return data;
				}
			}

			return null;
		}

		public bool IsSameDay(long beforeTimestamp, long afterTimestamp)
		{
			DateTime beforeDate = TimeUtil.UtcEpoch.AddMilliseconds(beforeTimestamp) - TimeUtil.ChangeDayTime;
			DateTime afterDate = TimeUtil.UtcEpoch.AddMilliseconds(afterTimestamp) - TimeUtil.ChangeDayTime;
			return beforeDate.Year == afterDate.Year
				&& beforeDate.Month == afterDate.Month
				&& beforeDate.Day == afterDate.Day;
		}

		public long GetElapsedDays(long startTimestamp)
		{
			long nextChangeTime = GetLocalNextChangeDayTime(startTimestamp);
			double oneDayMilliseconds = TimeSpan.FromDays(1.0).TotalMilliseconds;
			long localTimestamp = GetLocalTimestamp();
			return (int)((localTimestamp + (long)oneDayMilliseconds - nextChangeTime) / oneDayMilliseconds);
		}

        public long GetLocalLastGrandBattleMatchingTimeStamp()
		{
			DateTime baseDate = (GetLocalDateTime() - TimeUtil.ChangeDayTime).Date;
			int diff = 2 - (int)baseDate.DayOfWeek;
			if (diff > 0)
			{
				diff -= 7;
			}

			DateTime matchingDateTime = baseDate.AddDays(diff) + TimeUtil.ChangeDayTime;
			return ConvertDateTimeToTimeStamp(matchingDateTime);
		}

		public long GetLocalGrandBattleEndMatchingTimeStamp(long lastMatchingTimeStamp = -1L)
		{
			if (lastMatchingTimeStamp == -1L)
			{
				lastMatchingTimeStamp = GetLocalLastGrandBattleMatchingTimeStamp();
			}

			long changeDayMilliseconds = (long)TimeUtil.ChangeDayTime.TotalMilliseconds;
			long endMatchingHourMilliseconds = (long)TimeSpan.FromHours(OrtegaConst.GlobalGvg.EndMatchingHour).TotalMilliseconds;
			long endMinuteMilliseconds = (long)TimeSpan.FromMinutes(OrtegaConst.GlobalGvg.EndMinute).TotalMilliseconds;
			return lastMatchingTimeStamp + endMatchingHourMilliseconds + endMinuteMilliseconds - changeDayMilliseconds;
		}

		public long GetPrevUpdateGrandBattleMvpRankingLocalTimestamp(long localTimestamp)
		{
			DateTime now = TimeUtil.UtcEpoch.AddMilliseconds(localTimestamp);
			DateTime baseDate = (TimeUtil.UtcEpoch.AddMilliseconds(localTimestamp) - TimeUtil.ChangeDayTime).Date;
			int waitingMinutes = OrtegaConst.Gvg.WaitingUpdateMvpRankingMinutes;
			DateTime candidate = baseDate
				.AddHours(OrtegaConst.GlobalGvg.EndHour)
				.AddMinutes(OrtegaConst.GlobalGvg.EndMinute + waitingMinutes);
			if (!(candidate < now))
			{
				candidate = candidate.AddDays(-1);
			}

			return ConvertDateTimeToTimeStamp(candidate);
		}

		public long GetPrevUpdateGuildBattleMvpRankingLocalTimestamp(long localTimestamp)
		{
			DateTime now = TimeUtil.UtcEpoch.AddMilliseconds(localTimestamp);
			DateTime baseDate = (TimeUtil.UtcEpoch.AddMilliseconds(localTimestamp) - TimeUtil.ChangeDayTime).Date;
			int waitingMinutes = OrtegaConst.Gvg.WaitingUpdateMvpRankingMinutes;
			DateTime candidate = baseDate
				.AddHours(OrtegaConst.LocalGvg.EndHour)
				.AddMinutes(OrtegaConst.LocalGvg.EndMinute + waitingMinutes);
			if (!(candidate < now))
			{
				candidate = candidate.AddDays(-1);
			}

			return ConvertDateTimeToTimeStamp(candidate);
		}

		public DateTime GetJstDateTime()
		{
			return TimeUtil.UtcEpoch.AddMilliseconds(GetJstTimestamp());
		}

		public long GetJstTimestamp()
		{
			return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + JstDifferenceTimeStampFromUtc;
		}

		public long GetAddTime(DateAddTimeType type, long value, long utcTimeStamp = 0L)
		{
			DateTime baseDateTime = utcTimeStamp > 0
				? TimeUtil.UtcEpoch.AddMilliseconds(utcTimeStamp)
				: TimeUtil.UtcEpoch.AddMilliseconds(GetLocalTimestamp());

			DateTime resultDateTime = type switch
			{
				DateAddTimeType.Milliseconds => baseDateTime.AddMilliseconds(value),
				DateAddTimeType.Seconds => baseDateTime.AddSeconds(value),
				DateAddTimeType.Minutes => baseDateTime.AddMinutes(value),
				DateAddTimeType.Hours => baseDateTime.AddHours(value),
				DateAddTimeType.Days => baseDateTime.AddDays(value),
				DateAddTimeType.Months => baseDateTime.AddMonths((int)value),
				DateAddTimeType.Years => baseDateTime.AddYears((int)value),
				_ => throw new ArgumentOutOfRangeException(nameof(type), type, null),
			};

			return ConvertDateTimeToTimeStamp(resultDateTime);
		}

		public long GetLocalTodayUpdateLegendLeagueTimeStamp()
		{
			DateTime dateTime = GetLocalDateTime().Date
				.AddHours((int)OrtegaConst.BattlePvp.LegendLeagueUpdateHour)
				.AddMinutes((int)OrtegaConst.BattlePvp.LegendLeagueUpdateMinute);
			return ConvertDateTimeToTimeStamp(dateTime);
		}

		public long GetLocalLastUpdateLegendLeagueTimeStamp()
		{
			long localTimestamp = GetLocalTimestamp();
			long today = GetLocalTodayUpdateLegendLeagueTimeStamp();
			if (localTimestamp <= today)
			{
				return today - (long)TimeSpan.FromDays(1).TotalMilliseconds;
			}

			return today;
		}

		public int GetLegendLeagueDayOfWeek()
		{
			DateTime localDateTime = GetLocalDateTime();
			if (GetLocalTimestamp() < GetLocalTodayUpdateLegendLeagueTimeStamp())
			{
				int result = (int)localDateTime.DayOfWeek - 1;
				return result < 0 ? 6 : result;
			}

			return (int)localDateTime.DayOfWeek;
		}

		public int GetYesterdayLegendLeagueDayOfWeek()
		{
			DateTime localDateTime = GetLocalDateTime();
			if (GetLocalTimestamp() < GetLocalTodayUpdateLegendLeagueTimeStamp())
			{
				int day = (int)localDateTime.DayOfWeek - 1;
				if (day < 0)
				{
					day = 6;
				}

				int result = day - 1;
				return result < 0 ? 6 : result;
			}

			int yesterday = (int)localDateTime.DayOfWeek - 1;
			return yesterday < 0 ? 6 : yesterday;
		}

		public long ConvertLocalTimeStampToUtcTimeStamp(long localTimeStamp)
		{
			return localTimeStamp - DifferenceFromUtc;
		}
//
		public long ConvertJstTimeStampToLocalTimeStamp(DateTime jstDateTime)
		{
			DateTime utcDateTime = jstDateTime - TimeSpan.FromMilliseconds(JstDifferenceTimeStampFromUtc);
			return ConvertDateTimeToTimeStamp(utcDateTime) + DifferenceFromUtc;
		}

		public DateTime ConvertJstDateTImeToLocalDateTime(DateTime jstDateTime)
		{
			DateTime jstNowDateTime = ConvertUtcTimeStampToJstDateTime(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());
			TimeSpan delta = jstDateTime - jstNowDateTime;
			return GetLocalDateTime() + delta;
		}

		public DateTime ConvertLocalDateTimeToJstDateTIme(DateTime localDateTime)
		{
			DateTime utcDateTime = localDateTime - TimeSpan.FromMilliseconds(DifferenceFromUtc);
			return utcDateTime + TimeSpan.FromMilliseconds(JstDifferenceTimeStampFromUtc);
		}

		public DateTime ConvertUtcTimeStampToJstDateTime(long utcTimeStamp)
		{
			return TimeUtil.UtcEpoch.AddMilliseconds(utcTimeStamp + JstDifferenceTimeStampFromUtc);
		}

		public DateTime ConvertUtcTimeStampToLocalDateTime(long utcTimeStamp)
		{
			return TimeUtil.UtcEpoch.AddMilliseconds(utcTimeStamp + DifferenceFromUtc);
		}

		public int GetDateIntYearMonthDay()
		{
			long localTimestamp = GetLocalTimestamp();
			DateTime dateData = TimeUtil.UtcEpoch.AddMilliseconds(localTimestamp) - TimeUtil.ChangeDayTime;
			return (dateData.Year * 100 + dateData.Month) * 100 + dateData.Day;
		}

		public int GetDateIntYearMonthDay(long timeStamp)
		{
			DateTime dateData = TimeUtil.UtcEpoch.AddMilliseconds(timeStamp) - TimeUtil.ChangeDayTime;
			return (dateData.Year * 100 + dateData.Month) * 100 + dateData.Day;
		}

		public long ConvertUtcTimeStampToLocalTimeStamp(long utcTimeStamp)
		{
			return utcTimeStamp + DifferenceFromUtc;
		}

		private static long ConvertDateTimeToTimeStamp(DateTime dateTime)
		{
			return (long)(dateTime - TimeUtil.UtcEpoch).TotalMilliseconds;
		}
	}
}
