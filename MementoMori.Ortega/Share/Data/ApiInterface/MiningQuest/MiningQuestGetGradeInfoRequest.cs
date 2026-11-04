using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.MiningQuest
{
	[MessagePackObject(true)]
	[OrtegaApi("miningQuest/getGradeInfo", true, false)]
	public class MiningQuestGetGradeInfoRequest : ApiRequestBase
	{
	}
}
