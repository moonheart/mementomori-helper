using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Gvg
{
	[MessagePackObject(false)]
	public class CastleMemoInfo
	{
		[Key(0)]
		public long CastleId { get; set; }

		[Key(1)]
		public GvgCastleMemoMarkType GvgCastleMemoMarkType { get; set; }

		[Key(2)]
		public string Message { get; set; }

		public void Copy(CastleMemoInfo castleMemoInfo)
		{
            this.CastleId = castleMemoInfo.CastleId;
            this.GvgCastleMemoMarkType = castleMemoInfo.GvgCastleMemoMarkType;
            this.Message = castleMemoInfo.Message;
		}
	}
}
