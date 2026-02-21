using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.LocalRaid
{
	[MessagePackObject(true)]
	[OrtegaApi("localRaid/getLocalRaidBattleResultOld", true, false)]
	public class GetLocalRaidBattleResultOldRequest : ApiRequestBase
	{
		public long LeaderPlayerId { get; set; }

		public string BattleToken { get; set; }
	}
}
