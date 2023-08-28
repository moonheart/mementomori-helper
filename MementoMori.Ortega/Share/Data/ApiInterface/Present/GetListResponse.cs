using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.DtoInfo;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Present
{
	[MessagePackObject(true)]
	public class GetListResponse : ApiResponseBase
	{
		public List<UserPresentDtoInfo> userPresentDtoInfos { get; set; }
	}
}
