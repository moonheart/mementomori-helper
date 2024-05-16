using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.GuildTower
{
	[MessagePackObject(true)]
	public class GuildTowerEntryCharacter
	{
		public string CharacterGuid { get; set; }

		public int TodayUseCount { get; set; }
	}
}
