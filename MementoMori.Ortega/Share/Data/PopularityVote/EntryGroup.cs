using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.PopularityVote
{
	[MessagePackObject(true)]
	public class EntryGroup
	{
		public int GroupId { get; set; }

		public string NameKey { get; set; }
	}
}
