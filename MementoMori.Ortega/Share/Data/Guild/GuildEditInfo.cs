using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Guild
{
	[MessagePackObject(true)]
	public class GuildEditInfo
	{
		public string GuildAnnouncement { get; set; }

		public GuildOverView GuildOverView{ get; set; }

		public List<GuildSystemChatOptionInfo> GuildSystemChatOptionInfos{ get; set; }
	}
}
