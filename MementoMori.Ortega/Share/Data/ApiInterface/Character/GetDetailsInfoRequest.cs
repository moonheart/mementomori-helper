using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Character
{
	[MessagePackObject(true)]
	[OrtegaApi("character/getDetailsInfo", true, false)]
	public class GetDetailsInfoRequest : ApiRequestBase
	{
		public DeckUseContentType DeckType { get; set; }

		public List<string> TargetUserCharacterGuids{ get; set; }

		public long TargetPlayerId { get; set; }
	}
}
