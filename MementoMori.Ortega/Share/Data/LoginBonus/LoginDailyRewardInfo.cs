using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.LoginBonus
{
	[Description("日別ログイン報酬情報")]
	[MessagePackObject(true)]
	public class LoginDailyRewardInfo
	{
		[Description("日数")]
		[PropertyOrder(1)]
		public int Day
		{
			get;
			set;
		}

		[Description("日別ログイン報酬")]
		[PropertyOrder(2)]
		[Nest(true, 1)]
		public UserItem RewardItem
		{
			get;
			set;
		}

		public LoginDailyRewardInfo()
		{
		}
	}
}
