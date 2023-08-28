using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Present
{
	[MessagePackObject(true)]
	[OrtegaApi("present/getList", true, false)]
	public class GetListRequest : ApiRequestBase
	{
		public LanguageType LanguageType { get; set; }
	}
}
