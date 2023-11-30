using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Data;

namespace MementoMori.Ortega.Share.Master.Table
{
	public class PvpRankingRewardTable : TableBase<PvpRankingRewardMB>
	{
		public PvpRankingRewardMB GetByRankAndRewardType(long rank, PvpRankingRewardType type)
		{
			int num = 0;
			num++;
			throw new NullReferenceException();
		}

		public List<PvpRankingRewardMB> GetListByRewardType(PvpRankingRewardType type)
		{
			// List<PvpRankingRewardMB> list = new List();
			// int num = 0;
			// num++;
			// return list;
			throw new NullReferenceException();
		}


        public List<UserItem> GetDailyRewardItemList(long rank, bool isInIconReward)
        {
            // List<UserItem> list = new List();
            // PvpRankingRewardMB byRankAndRewardType = this.GetByRankAndRewardType(rank, PvpRankingRewardType.LegendLeagueDailyRankingReward);
            // if (byRankAndRewardType != 0 && !byRankAndRewardType.<RewardList>k__BackingField.IsNullOrEmpty<UserItem>())
            // {
            //     IReadOnlyList<UserItem> <RewardList>k__BackingField = byRankAndRewardType.<RewardList>k__BackingField;
            //     list.AddRange(<RewardList>k__BackingField);
            // }
            // if (isInIconReward)
            // {
            //     PvpRankingRewardMB byRankAndRewardType2 = this.GetByRankAndRewardType(rank, PvpRankingRewardType.LegendLeagueIconRankingReward);
            //     if (byRankAndRewardType2 != 0 && !byRankAndRewardType2.<RewardList>k__BackingField.IsNullOrEmpty<UserItem>())
            //     {
            //         IReadOnlyList<UserItem> <RewardList>k__BackingField2 = byRankAndRewardType2.<RewardList>k__BackingField;
            //         list.AddRange(<RewardList>k__BackingField2);
            //     }
            // }
            // return list;
            throw new NullReferenceException();
        }
	}
}
