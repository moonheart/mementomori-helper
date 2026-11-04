using MementoMori.Ortega.Share.Data.MiningQuest;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.MiningQuest;

[MessagePackObject(true)]
public class MiningQuestUnlockBenefitResponse : ApiResponseBase
{
    public List<MiningQuestBenefitInfo> BenefitInfoList { get; set; }
}
