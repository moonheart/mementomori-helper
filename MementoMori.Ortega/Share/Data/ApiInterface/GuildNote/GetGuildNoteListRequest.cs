using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.GuildNote
{
	[MessagePackObject(true)]
	[OrtegaApi("guildNote/getGuildNoteList", true, false)]
	public class GetGuildNoteListRequest : ApiRequestBase
	{
	}
}
