using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.GuildTower
{
	[MessagePackObject(true)]
	[OrtegaApi("guildTower/receiveFloorReward", true, false)]
	public class ReceiveFloorRewardRequest : ApiRequestBase
	{
		public List<long> FloorRewardMBIdList { get; set; }
	}
}
