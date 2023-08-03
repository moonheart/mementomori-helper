using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Guild
{
	[MessagePackObject(true)]
	public class RecommendationGuildInfo
	{
		public GuildInfo GuildInfo{ get; set; }

		public bool IsApplying { get; set; }
	}
}
