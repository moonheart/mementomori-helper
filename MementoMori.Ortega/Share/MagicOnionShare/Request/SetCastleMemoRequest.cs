using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.MagicOnionShare.Request
{
	[MessagePackObject(false)]
	public class SetCastleMemoRequest
	{
		[Key(0)]
		public long CastleId { get; set; }

		[Key(1)]
		public GvgCastleMemoMarkType GvgCastleMemoMarkType { get; set; }

		[Key(2)]
		public string Message { get; set; }
	}
}
