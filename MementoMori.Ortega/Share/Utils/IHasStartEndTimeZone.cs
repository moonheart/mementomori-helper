using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;

namespace MementoMori.Ortega.Share.Utils
{
	public interface IHasStartEndTimeZone
	{
		[DateTimeString]
		string EndTime { get; }

		StartEndTimeZoneType StartEndTimeZoneType { get; }

		[DateTimeString]
		string StartTime { get; }
	}
}
