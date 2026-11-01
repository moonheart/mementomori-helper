using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.GuildNote
{
	[MessagePackObject(true)]
	[OrtegaApi("guildNote/shareGuildNote", true, false)]
	public class ShareGuildNoteRequest : ApiRequestBase
	{
		public long GuildNoteId { get; set; }
	}
}
