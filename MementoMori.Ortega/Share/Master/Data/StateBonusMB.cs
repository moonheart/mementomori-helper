using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Data.StateBonus;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[Description("国ボーナス")]
	[MessagePackObject(true)]
	public class StateBonusMB : MasterBookBase
	{
		[Description("時空の洞窟時空コインボーナス")]
		[PropertyOrder(5)]
		public int DungeonCoinBonusPercent
		{
			get;
		}

		[Description("ギルドバトルギルドコインボーナス")]
		[PropertyOrder(6)]
		public int GuildCoinBonusPercent
		{
			get;
		}

		[Description("曜日ボーナスが有効か")]
		[PropertyOrder(4)]
		public bool IsActiveWeeklyBonus
		{
			get;
		}

		[Description("国ボーナス表示アイテムアイコン情報")]
		[PropertyOrder(2)]
		[Nest(false, 0)]
		public ItemIconInfo ItemIconInfo
		{
			get;
		}

		[PropertyOrder(1)]
		[Description("国ボーナス表示テキスト")]
		public string StateBonusNameTextKey
		{
			get;
		}

		[Description("国ボーナス表示数値")]
		[PropertyOrder(3)]
		public string StateBonusNumberTextKey
		{
			get;
		}

		[PropertyOrder(7)]
		[Nest(true, 0)]
		[Description("累計貢献メダル報酬ボーナス情報")]
		public IReadOnlyList<TotalActivityMedalRewardBonus> TotalActivityMedalRewardBonusList
		{
			get;
		}

		[SerializationConstructor]
		public StateBonusMB(long id, bool? isIgnore, string memo, string stateBonusNameTextKey, ItemIconInfo itemIconInfo, string stateBonusNumberTextKey, bool isActiveWeeklyBonus, int dungeonCoinBonusPercent, int guildCoinBonusPercent, IReadOnlyList<TotalActivityMedalRewardBonus> totalActivityMedalRewardBonusList)
			:base(id, isIgnore, memo)
		{
			StateBonusNameTextKey = stateBonusNameTextKey;
			ItemIconInfo = itemIconInfo;
			StateBonusNumberTextKey = stateBonusNumberTextKey;
			IsActiveWeeklyBonus = isActiveWeeklyBonus;
			DungeonCoinBonusPercent = dungeonCoinBonusPercent;
			GuildCoinBonusPercent = guildCoinBonusPercent;
			TotalActivityMedalRewardBonusList = totalActivityMedalRewardBonusList;
		}

		public StateBonusMB() :base(0L, false, ""){}
	}
}
