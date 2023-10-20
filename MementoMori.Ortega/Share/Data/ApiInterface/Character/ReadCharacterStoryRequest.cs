using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Character
{
	[MessagePackObject(true)]
	[OrtegaApi("character/readCharacterStory", true, false)]
	public class ReadCharacterStoryRequest : ApiRequestBase
	{
		public long CharacterStoryId { get; set; }

		public bool IsSkip { get; set; }

		public MemoryLogType MemoryLogType { get; set; }
	}
}
