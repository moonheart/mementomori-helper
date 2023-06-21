using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.LoginBonus
{
	[Description("合計ログイン報酬情報")]
	[MessagePackObject(true)]
	public class LoginCountRewardInfo
	{
		[PropertyOrder(1)]
		[Description("合計ログイン日数")]
		public int DayCount
		{
			get;
			set;
		}

		[PropertyOrder(3)]
		[Description("画像パス")]
		public string ImagePath
		{
			get;
			set;
		}

		[PropertyOrder(4)]
		[Description("画像X座標")]
		public int PositionX
		{
			get;
			set;
		}

		[PropertyOrder(2)]
		[Nest(true, 1)]
		[Description("合計ログイン報酬リスト")]
		public List<UserItem> RewardItemList
		{
			get;
			set;
		}

		public LoginCountRewardInfo()
		{
		}
	}
}
