using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Gvg;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.MagicOnionShare.Response
{
	[MessagePackObject(false)]
	public class OnOpenBattleDialogResponse
	{
		[Key(4)]
		public long AttackerGuildId { get; set; }

		[Key(0)]
		public List<PartyInfoSlim> AttackerParties { get; set; }

		[Key(1)]
		public int AttackerPartyCount { get; set; }

		[Key(8)]
		public int MatchingNumber { get; set; }

		[Key(5)]
		public long ReceiverGuildId { get; set; }

		[Key(2)]
		public List<PartyInfoSlim> ReceiverParties { get; set; }

		[Key(3)]
		public int ReceiverPartyCount { get; set; }

		[Key(6)]
		public long WinContinueCount { get; set; }

		[Key(7)]
		public BattleFieldCharacterGroupType WinGroupType { get; set; }
	}
}
