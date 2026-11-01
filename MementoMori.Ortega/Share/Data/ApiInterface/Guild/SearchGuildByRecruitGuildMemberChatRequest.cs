using MementoMori.Ortega.Share.Data.Chat;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Guild;

[MessagePackObject(true)]
[OrtegaApi("guild/searchGuildByRecruitGuildMemberChat", true, false)]
public class SearchGuildByRecruitGuildMemberChatRequest : ApiRequestBase
{
    public ChatIdentityInfo ChatIdentityInfo { get; set; }
}
