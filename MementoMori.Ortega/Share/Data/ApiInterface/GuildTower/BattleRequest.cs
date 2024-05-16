using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.GuildTower
{
	[MessagePackObject(true)]
	[OrtegaApi("guildTower/battle", true, false)]
	public class BattleRequest : ApiRequestBase
	{
		public List<string> CharacterGuidList { get; set; }

		public int Difficulty { get; set; }

		public int GuildTowerFloor { get; set; }
	}
}
