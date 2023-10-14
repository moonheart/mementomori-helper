using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Gvg;
using MessagePack;

namespace MementoMori.Ortega.Share.MagicOnionShare.Response
{
	[MessagePackObject(false)]
	public class OnUpdateCastlePartyResponse
	{
		[Key(0)]
		public List<PartyInfoSlim> AttackerParties { get; set; }

		[Key(2)]
		public int MatchingNumber { get; set; }

		[Key(1)]
		public List<PartyInfoSlim> ReceiverParties { get; set; }
	}
}
