using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Interface;
using MementoMori.Ortega.Share.Data.LocalRaid;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.LocalRaid
{
	[MessagePackObject(true)]
	public class GetLocalRaidBattleLogsResponse : ApiResponseBase, ILocalRaidInfoApiResponse
	{
        public List<LocalRaidBattleLogInfo> LocalRaidBattleLogInfoList { get; set; }


		public List<LocalRaidQuestInfo> LocalRaidQuestInfos { get; set; }

		public List<LocalRaidEnemyInfo> LocalRaidEnemyInfos { get; set; }
	}
}
