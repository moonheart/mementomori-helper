using System.ComponentModel;
using MementoMori.Ortega.Share.Data.LoginBonus;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[Description("月間ログインボーナス報酬リスト")]
	[MessagePackObject(true)]
	public class MonthlyLoginBonusRewardListMB : MasterBookBase
	{
		[Description("日別ログイン報酬リスト")]
		[PropertyOrder(1)]
		[Nest(true, 1)]
		public IReadOnlyList<LoginDailyRewardInfo> DailyRewardList
		{
			get;
		}

		[PropertyOrder(2)]
		[Nest(true, 1)]
		[Description("合計ログイン報酬リスト")]
		public IReadOnlyList<LoginCountRewardInfo> LoginCountRewardList
		{
			get;
		}

		[SerializationConstructor]
		public MonthlyLoginBonusRewardListMB(long id, bool? isIgnore, string memo, IReadOnlyList<LoginDailyRewardInfo> dailyRewardList, IReadOnlyList<LoginCountRewardInfo> loginCountRewardList)
			:base(id, isIgnore, memo)
		{
			DailyRewardList = dailyRewardList;
			LoginCountRewardList = loginCountRewardList;
		}

		public MonthlyLoginBonusRewardListMB() :base(0L, false, ""){}
	}
}
