using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Gvg;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.MagicOnionShare.Response
{
	[MessagePackObject(false)]
	public class OnEndCastleBattleResponse
	{
		[Key(3)]
		public List<PartyInfoSlim> AttackerParties { get; set; }

		[Key(5)]
		public int AttackerPartyCount { get; set; }

		[Key(7)]
		public GvgCastleState GvgCastleState { get; set; }

		[Key(8)]
		public int MatchingNumber { get; set; }

		[Key(4)]
		public List<PartyInfoSlim> ReceiverParties { get; set; }

		[Key(6)]
		public int ReceiverPartyCount { get; set; }

		[Key(1)]
		public int WinContinueCount { get; set; }

		[Key(2)]
		public BattleFieldCharacterGroupType WinGroupType { get; set; }
	}
}
