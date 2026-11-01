using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Guild
{
	[MessagePackObject(true)]
	public class ChatRecruitGuildMemberResponse : ApiResponseBase
	{
		public long ChatRecruitGuildMemberEndIntervalTimestamp { get; set; }
	}
}
