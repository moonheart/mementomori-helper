using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("期間限定ログインボーナス日別報酬出し分け情報")]
	public class LimitedLoginBonusSwitchingDailyRewardMB : MasterBookBase
	{
		[PropertyOrder(1)]
		[Description("期間限定ログインボーナス報酬リストID")]
		public long LimitedLoginBonusRewardListId { get; set; }

		[PropertyOrder(2)]
		[Description("日付")]
		public int Date { get; set; }

		[PropertyOrder(3)]
		[Description("開放ステージ")]
		public long MinQuestId { get; set; }

		[PropertyOrder(4)]
		[Description("終了ステージ")]
		public long MaxQuestId { get; set; }

		[PropertyOrder(5)]
		[Description("開放VIP")]
		public long MinVip { get; set; }

		[PropertyOrder(6)]
		[Description("終了VIP")]
		public long MaxVip { get; set; }

		[Nest(true, 1)]
		[PropertyOrder(7)]
		[Description("日別報酬アイテム")]
		public UserItem DailyRewardItem { get; set; }

		[PropertyOrder(8)]
		[Description("キャラレアリティ")]
		public CharacterRarityFlags RarityFlags { get; set; }

		[SerializationConstructor]
		public LimitedLoginBonusSwitchingDailyRewardMB(long id, bool? isIgnore, string memo, long limitedLoginBonusRewardListId, int date, long minQuestId, long maxQuestId, long minVip, long maxVip, UserItem dailyRewardItem, CharacterRarityFlags rarityFlags)
			: base(id, isIgnore, memo)
		{
            LimitedLoginBonusRewardListId = limitedLoginBonusRewardListId;
            Date = date;
            MinQuestId = minQuestId;
            MaxQuestId = maxQuestId;
            MinVip = minVip;
            MaxVip = maxVip;
            DailyRewardItem = dailyRewardItem;
            RarityFlags = rarityFlags;
		}

		public LimitedLoginBonusSwitchingDailyRewardMB(): base(default, default, default)
		{
			
		}
	}
}
