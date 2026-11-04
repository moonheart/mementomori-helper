using MementoMori.Ortega.Share.Data.Chat;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.GuildNote;

[MessagePackObject(true)]
public class GetReactionDetailInfoResponse : ApiResponseBase
{
    public Dictionary<ChatReactionType, List<ReactionPlayerInfo>> PlayerInfoListByReactionType { get; set; }
}
