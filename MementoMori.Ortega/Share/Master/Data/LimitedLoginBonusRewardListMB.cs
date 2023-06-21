using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("期間限定ログインボーナス報酬リスト")]
	public class LimitedLoginBonusRewardListMB : MasterBookBase
	{
		[Description("日別報酬リスト")]
		[PropertyOrder(1)]
		[Nest(false, 0)]
		public IReadOnlyList<DailyLimitedLoginBonusItem> DailyRewardList
		{
			get;
		}

		[PropertyOrder(3)]
		[Nest(false, 0)]
		[Description("毎日報酬")]
		public EveryDayLimitedLoginBonusItem EveryDayRewardItem
		{
			get;
		}

		[PropertyOrder(2)]
		[Description("毎日報酬有無")]
		public bool ExistEveryDayReward
		{
			get;
		}

		[Description("特別報酬有無")]
		[PropertyOrder(4)]
		public bool ExistSpecialReward
		{
			get;
		}

		[Nest(false, 0)]
		[Description("特別報酬")]
		[PropertyOrder(5)]
		public SpecialLimitedLoginBonusItem SpecialRewardItem
		{
			get;
		}

		[SerializationConstructor]
		public LimitedLoginBonusRewardListMB(long id, bool? isIgnore, string memo, IReadOnlyList<DailyLimitedLoginBonusItem> dailyRewardList, bool existEveryDayReward, EveryDayLimitedLoginBonusItem everyDayRewardItem, bool existSpecialReward, SpecialLimitedLoginBonusItem specialRewardItem)
			:base(id, isIgnore, memo)
		{
			DailyRewardList = dailyRewardList;
			ExistEveryDayReward = existEveryDayReward;
			EveryDayRewardItem = everyDayRewardItem;
			ExistSpecialReward = existSpecialReward;
			SpecialRewardItem = specialRewardItem;
		}

		public LimitedLoginBonusRewardListMB() :base(0L, false, ""){}
	}
}
