using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Gvg;
using MessagePack;

namespace MementoMori.Ortega.Share.MagicOnionShare.Response
{
	[MessagePackObject(false)]
	public class OnAddOnlyReceiverPartyResponse
	{
		[Key(1)]
		public int MatchingNumber { get; set; }

		[Key(0)]
		public List<PartyInfoSlim> ReceiverParties { get; set; }
	}
}
