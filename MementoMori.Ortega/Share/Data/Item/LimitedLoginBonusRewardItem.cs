using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Item
{
	[Description("期間限定ログボの報酬アイテム")]
	[MessagePackObject(true)]
	public class LimitedLoginBonusRewardItem : IUserCharacterItem
	{
		[Description("特別報酬アイテム")]
		public UserItem Item { get; set; }

		[Description("キャラレアリティ")]
		public CharacterRarityFlags RarityFlags
		{
			get;
			set;
		}

		public LimitedLoginBonusRewardItem()
		{
		}
	}
}
