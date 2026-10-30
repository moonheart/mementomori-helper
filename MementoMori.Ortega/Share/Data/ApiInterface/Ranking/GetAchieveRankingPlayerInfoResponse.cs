using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Battle;
using MementoMori.Ortega.Share.Data.Player;
using MementoMori.Ortega.Share.Data.Ranking;
using MementoMori.Ortega.Share.Extensions;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Ranking
{
	[MessagePackObject(true)]
	public class GetAchieveRankingPlayerInfoResponse : ApiResponseBase
	{
		public List<AchieveRankingPlayerInfo> AchieveRankingPlayerInfoList { get; set; }

		public List<long> NoRecordMBIdList { get; set; }

		public List<AchieveRewardReceivedPlayerInfo> ReceivedAchieveRewardPlayerInfoList { get; set; }

		public List<string> ReceivedAchieveRewardPlayerNameList { get; set; }

		public long FirstRankPlayerId { get; set; }

		public List<ClearPartyCharacterInfo> FirstRankClearPartyCharacterInfos { get; set; }

		public bool IsCheckedAwardPresentation { get; set; }

		public PlayerInfo GetTopPlayerInfo()
        {
            return this.AchieveRankingPlayerInfoList.FirstOrDefault()?.PlayerInfo;
        }
	}
}
