using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Item
{
	[MessagePackObject(true)]
	[Description("期間限定ログボの日別報酬アイテム")]
	public class DailyLimitedLoginBonusItem : IUserCharacterItem
	{
		[PropertyOrder(2)]
		[Nest(true, 1)]
		[Description("報酬アイテム")]
		public UserItem DailyRewardItem
		{
			get;
			set;
		}

		[Description("日付")]
		[PropertyOrder(1)]
		public int Date
		{
			get;
			set;
		}

		[PropertyOrder(3)]
		[Description("キャラレアリティ")]
		public CharacterRarityFlags RarityFlags
		{
			get;
			set;
		}

		UserItem IUserCharacterItem.Item
		{
			get;
		}

		public DailyLimitedLoginBonusItem()
		{
		}
	}
}
