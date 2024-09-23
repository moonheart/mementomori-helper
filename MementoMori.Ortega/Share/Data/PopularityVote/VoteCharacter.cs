using MessagePack;

namespace MementoMori.Ortega.Share.Data.PopularityVote
{
	[MessagePackObject(true)]
	public class VoteCharacter
	{
		public long CharacterId { get; set; }

		public long VoteCount { get; set; }
	}
}
