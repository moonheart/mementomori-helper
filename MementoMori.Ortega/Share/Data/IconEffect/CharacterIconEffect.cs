using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.IconEffect
{
	[MessagePackObject(true)]
	public class CharacterIconEffect
	{
		public long IconId { get; set; }

		public long ActiveIconEffectId { get; set; }

		public List<long> UnlockedIconEffectIds { get; set; }
	}
}
