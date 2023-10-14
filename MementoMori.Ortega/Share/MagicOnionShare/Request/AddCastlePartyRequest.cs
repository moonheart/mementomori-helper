using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.MagicOnionShare.Request
{
	[MessagePackObject(false)]
	public class AddCastlePartyRequest
	{
		[Key(2)]
		public long CastleId { get; set; }

		[Key(0)]
		public List<long> CharacterIds { get; set; }

		[Key(1)]
		public int MemberCount { get; set; }
	}
}
