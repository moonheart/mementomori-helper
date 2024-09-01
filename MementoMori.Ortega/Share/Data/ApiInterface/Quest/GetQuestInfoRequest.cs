using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Quest
{
    [MessagePackObject(true)]
    [OrtegaApi("quest/getQuestInfo ", true, false)]
    public class GetQuestInfoRequest : ApiRequestBase
    {
        public long TargetQuestId { get; set; }
    }
}