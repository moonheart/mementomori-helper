using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.PopularityVote
{
	[MessagePackObject(true)]
	public class PastChampionCharacter
	{
		public long CharacterId { get; set; }

		public int EventNo { get; set; }

		public List<long> SubDisplayCharacterIdList { get; set; }
	}
}
