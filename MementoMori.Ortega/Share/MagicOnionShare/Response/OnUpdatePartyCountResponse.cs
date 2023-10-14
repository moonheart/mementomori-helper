using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.MagicOnionShare.Response
{
	[MessagePackObject(false)]
	public class OnUpdatePartyCountResponse
	{
		[Key(0)]
		public Dictionary<long, long> PartyCountDictionary { get; set; }
	}
}
