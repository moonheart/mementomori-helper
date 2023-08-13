using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Mission
{
	[MessagePackObject(true)]
	[OrtegaApi("mission/getMissionInfo", true, false)]
	public class GetMissionInfoRequest : ApiRequestBase
	{
		public List<MissionGroupType> TargetMissionGroupList { get; set; }
	}
}
