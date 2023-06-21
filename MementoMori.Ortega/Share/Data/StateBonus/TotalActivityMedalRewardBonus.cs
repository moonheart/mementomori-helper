using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.StateBonus
{
	[MessagePackObject(true)]
	[Description("累計貢献メダル報酬ボーナス情報")]
	public class TotalActivityMedalRewardBonus
	{
		[Nest(true, 1)]
		[PropertyOrder(2)]
		[Description("ボーナス報酬リスト")]
		public List<MissionReward> BonusRewardList
		{
			get;
			set;
		}

		[Description("累計貢献メダル報酬ID")]
		[PropertyOrder(1)]
		public long TotalActivityMedalRewardId
		{
			get;
			set;
		}

		public TotalActivityMedalRewardBonus()
		{
		}
	}
}
