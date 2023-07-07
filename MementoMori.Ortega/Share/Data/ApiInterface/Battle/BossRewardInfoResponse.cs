using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Battle.Result;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Battle
{
	[MessagePackObject(true)]
	public class BossRewardInfoResponse : ApiResponseBase
	{
		public long QuestId { get; set; }

		public BattleRewardResult BattleRewardResult { get; set; }
	}
}
