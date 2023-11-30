using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Data;

namespace MementoMori.Ortega.Share.Master.Table
{
	public class AchieveRankingRewardTable : TableBase<AchieveRankingRewardMB>
	{
		public List<AchieveRankingRewardMB> GetByRankingDataType(RankingDataType rankingDataType)
		{
            return _datas.Where(x=>x.RankingDataType == rankingDataType).ToList(); 
		}
	}
}
