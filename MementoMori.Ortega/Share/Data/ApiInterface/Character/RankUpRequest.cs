using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Character;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Character
{
	[MessagePackObject(true)]
	[OrtegaApi("character/rankUp", true, false)]
	public class RankUpRequest : ApiRequestBase
	{
		public List<CharacterRankUpMaterialInfo> RankUpList { get; set; }
	}
}
