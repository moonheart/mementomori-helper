using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.MyPage
{
	[MessagePackObject(true)]
	public class MissionGuideInfo
	{
		public long GuideId { get; set; }

		public MissionGroupType MissionGroupType { get; set; }

		public MissionStatusType MissionStatus { get; set; }
	}
}
