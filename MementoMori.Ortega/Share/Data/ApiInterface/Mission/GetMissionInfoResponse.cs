using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Mission;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Mission
{
	[MessagePackObject(true)]
	public class GetMissionInfoResponse : ApiResponseBase
	{
		public bool IsOpenCurrencyMission { get; set; }

		public Dictionary<MissionGroupType, MissionInfo> MissionInfoDict { get; set; }
	}
}
