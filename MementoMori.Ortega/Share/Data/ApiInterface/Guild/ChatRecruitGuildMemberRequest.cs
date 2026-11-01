using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Guild;

[MessagePackObject(true)]
[OrtegaApi("guild/chatRecruitGuildMember", true, false)]
public class ChatRecruitGuildMemberRequest : ApiRequestBase
{
    public string RecruitMessage { get; set; }
}
