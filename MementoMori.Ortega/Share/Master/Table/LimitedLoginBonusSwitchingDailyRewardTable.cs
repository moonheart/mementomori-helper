using MementoMori.Ortega.Share.Master.Data;

namespace MementoMori.Ortega.Share.Master.Table
{
	public class LimitedLoginBonusSwitchingDailyRewardTable : TableBase<LimitedLoginBonusSwitchingDailyRewardMB>
	{
		public LimitedLoginBonusSwitchingDailyRewardMB GetSwitchingDailyReward(long limitedLoginBonusRewardListId, int date, long maxClearQuestId, long vip)
		{
			int num = 0;
			if (num < date)
			{
				num++;
			}
			throw new NullReferenceException();
		}
	}
}
