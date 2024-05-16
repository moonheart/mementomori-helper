using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.GuildTower
{
	[MessagePackObject(true)]
	[OrtegaApi("guildTower/entryCharacter", true, false)]
	public class EntryCharacterRequest : ApiRequestBase
	{
		public GuildTowerEntryType GuildTowerEntryType { get; set; }

		public List<string> CharacterGuidList { get; set; }

		public bool IsContinueEntry { get; set; }
	}
}
