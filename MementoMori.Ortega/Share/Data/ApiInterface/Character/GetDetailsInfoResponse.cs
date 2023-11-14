using MementoMori.Ortega.Share.Data.Character;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Character
{
	[MessagePackObject(true)]
	public class GetDetailsInfoResponse : ApiErrorResponse
	{
		public List<CharacterDetailInfo> CharacterDetailInfos { get; set; }
	}
}
