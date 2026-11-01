using MementoMori.Ortega.Share.Data.Guild;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Guild;

[MessagePackObject(true)]
public class SearchGuildByRecruitGuildMemberChatResponse : ApiResponseBase
{
    public long GuildApplyingCount { get; set; }

    public SearchGuildResultInfo SearchGuildResultInfo { get; set; }
}
