using MessagePack;
using MementoMori.Ortega.Share.Enums;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Debug
{
	[MessagePackObject(true)]
	public class SetMiningQuestMaxScoreResponse : ApiResponseBase
	{
		public MiningQuestGradeType GradeType { get; set; }
	}
}
