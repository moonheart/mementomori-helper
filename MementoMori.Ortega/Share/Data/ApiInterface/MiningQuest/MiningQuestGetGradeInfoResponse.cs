using MementoMori.Ortega.Share.Data.MiningQuest;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.MiningQuest;

[MessagePackObject(true)]
public class MiningQuestGetGradeInfoResponse : ApiResponseBase
{
    public long ChangeDayLocalTimeStamp { get; set; }

    public List<MiningQuestGradeInfo> GradeInfoList { get; set; }
}
