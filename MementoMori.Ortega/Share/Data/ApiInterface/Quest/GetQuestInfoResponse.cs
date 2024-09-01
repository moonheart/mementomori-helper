using MementoMori.Ortega.Share.Data.Battle;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Quest
{
    [MessagePackObject(true)]
    public class GetQuestInfoResponse : ApiResponseBase
    {
        public BossBattleInfo TargetBossBattleInfo { get; set; }
    }
}