using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Character
{
	[MessagePackObject(true)]
	[OrtegaApi("character/getCharacterStoryReward", true, false)]
	public class GetCharacterStoryRewardRequest : ApiRequestBase
	{
		public List<long> CharacterStoryIdList { get; set; }

		public bool IsSkip { get; set; }
	}
}
