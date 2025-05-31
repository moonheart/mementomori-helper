using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Chat
{
	[MessagePackObject(false)]
	public class ChatBattlePartyInfo
	{
		[Key(0)]
		public long BattlePower { get; set; }

		[Key(1)]
		public string GuildName { get; set; }

		[Key(2)]
		public string PlayerName { get; set; }

		[Key(3)]
		public BattleFieldCharacterGroupType BattleFieldCharacterGroupType { get; set; }

		[Key(4)]
		public bool IsWin { get; set; }

		[Key(5)]
		public List<long> CharacterIdList { get; set; }
	}
}
