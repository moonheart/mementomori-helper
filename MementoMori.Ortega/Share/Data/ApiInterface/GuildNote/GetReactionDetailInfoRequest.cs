using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.GuildNote
{
	[MessagePackObject(true)]
	[OrtegaApi("guildNote/getReactionDetailInfo", true, false)]
	public class GetReactionDetailInfoRequest : ApiRequestBase
	{
		public long GuildNoteId { get; set; }
	}
}
