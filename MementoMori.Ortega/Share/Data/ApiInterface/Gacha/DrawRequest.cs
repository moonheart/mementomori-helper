using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Gacha
{
	[OrtegaApi("gacha/draw", true, false)]
	[MessagePackObject(true)]
	public class DrawRequest : ApiRequestBase
	{
		public long GachaButtonId { get; set; }
	}
}
