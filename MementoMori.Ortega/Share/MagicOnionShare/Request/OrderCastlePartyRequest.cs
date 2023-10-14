using MessagePack;

namespace MementoMori.Ortega.Share.MagicOnionShare.Request
{
	[MessagePackObject(false)]
	public class OrderCastlePartyRequest
	{
		[Key(0)]
		public long FirstCharacterId { get; set; }

		[Key(3)]
		public int Index { get; set; }

		[Key(2)]
		public bool IsUp { get; set; }

		[Key(1)]
		public long OwnerPlayerId { get; set; }
	}
}
