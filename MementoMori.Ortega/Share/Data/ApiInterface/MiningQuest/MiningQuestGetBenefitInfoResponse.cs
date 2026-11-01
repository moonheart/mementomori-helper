using MementoMori.Ortega.Share.Data.MiningQuest;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.MiningQuest;

[MessagePackObject(true)]
public class MiningQuestGetBenefitInfoResponse : ApiResponseBase
{
    public int CurrentPurchaseCount { get; set; }

    public List<MiningQuestBenefitInfo> BenefitInfoList { get; set; }
}
