using MessagePack;
using MementoMori.Ortega.Share.Enums;

namespace MementoMori.Ortega.Share.Data.ApiInterface.GuildNote
{
	[MessagePackObject(true)]
	[OrtegaApi("guildNote/reactGuildNote", true, false)]
	public class ReactGuildNoteRequest : ApiRequestBase
	{
		public long GuildNoteId { get; set; }

		public ChatReactionType ChatReactionType { get; set; }
	}
}
