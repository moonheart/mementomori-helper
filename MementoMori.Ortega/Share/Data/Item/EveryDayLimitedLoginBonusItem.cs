using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Item
{
	[MessagePackObject(true)]
	[Description("期間限定ログボの毎日報酬アイテム")]
	public class EveryDayLimitedLoginBonusItem : IUserCharacterItem
	{
		[Description("毎日報酬アイテム")]
		[PropertyOrder(1)]
		[Nest(true, 1)]
		public UserItem EveryDayRewardItem
		{
			get;
			set;
		}

		[PropertyOrder(2)]
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

		public EveryDayLimitedLoginBonusItem()
		{
		}
	}
}
