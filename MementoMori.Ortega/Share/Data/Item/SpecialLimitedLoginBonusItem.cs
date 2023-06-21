using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Item
{
	[Description("期間限定ログボの特別報酬アイテム")]
	[MessagePackObject(true)]
	public class SpecialLimitedLoginBonusItem : IUserCharacterItem
	{
		[PropertyOrder(1)]
		[Description("日付")]
		public int Date
		{
			get;
			set;
		}

		[Description("特別報酬アイテム")]
		[Nest(true, 1)]
		[PropertyOrder(2)]
		public UserItem SpecialRewardItem
		{
			get;
			set;
		}

		[Description("キャラレアリティ")]
		[PropertyOrder(3)]
		public CharacterRarityFlags RarityFlags
		{
			get;
			set;
		}

		UserItem IUserCharacterItem.Item
		{
			get;
		}

		public SpecialLimitedLoginBonusItem()
		{
		}
	}
}
