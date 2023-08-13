using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Mission
{
	[MessagePackObject(true)]
	[OrtegaApi("mission/rewardMission", true, false)]
	public class RewardMissionRequest : ApiRequestBase
	{
		public List<long> TargetMissionIdList { get; set; }
	}
}
