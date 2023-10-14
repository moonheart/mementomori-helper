using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Gvg;
using MessagePack;

namespace MementoMori.Ortega.Share.MagicOnionShare.Response
{
	[MessagePackObject(false)]
	public class OnUpdateMapResponse
	{
		[Key(0)]
		public List<CastleInfo> CastleInfos { get; set; }

		[Key(1)]
		public int MatchingNumber { get; set; }
	}
}
