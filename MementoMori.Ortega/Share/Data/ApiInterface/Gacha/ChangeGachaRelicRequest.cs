using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Gacha
{
	[MessagePackObject(true)]
	[OrtegaApi("gacha/changeGachaRelic", true, false)]
	public class ChangeGachaRelicRequest : ApiRequestBase
	{
		public GachaRelicType GachaRelicType { get; set; }
	}
}
