using MementoMori.Ortega.Share.Data.Gvg;
using MessagePack;

namespace MementoMori.Ortega.Share.MagicOnionShare.Response
{
	[MessagePackObject(false)]
	public class OnUpdateCastleMemoResponse
	{
		[Key(0)]
		public long GuildId { get; set; }

		[Key(1)]
		public List<CastleMemoInfo> CastleMemoInfos { get; set; }

		[Key(2)]
		public int MatchingNumber { get; set; }
	}
}
