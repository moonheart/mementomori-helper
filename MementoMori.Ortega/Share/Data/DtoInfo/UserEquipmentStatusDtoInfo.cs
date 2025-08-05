using MessagePack;

namespace MementoMori.Ortega.Share.Data.DtoInfo
{
	[MessagePackObject(true)]
	public class UserEquipmentStatusDtoInfo
	{
		public int SynchroMaxPossessionCount { get; set; }

		public int SynchroMissionRewardedCount { get; set; }

		public int UseResetCount { get; set; }

		public long LastResetCountUpdateTime { get; set; }
	}
}
