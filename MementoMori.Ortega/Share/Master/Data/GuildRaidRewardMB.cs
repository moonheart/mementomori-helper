using System.ComponentModel;
using MementoMori.Ortega.Share.Data.GuildRaid;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[Description(" ギルドレイド報酬データ")]
	[MessagePackObject(true)]
	public class GuildRaidRewardMB : MasterBookBase
	{
		[Nest(false, 0)]
		[PropertyOrder(6)]
		[Description("ギルドダメージバー報酬リスト")]
		public IReadOnlyList<GuildDamageBarReward> GuildDamageBarRewards
		{
			get;
		}

		[Description("一回の挑戦で獲得できるギルド経験値")]
		[PropertyOrder(2)]
		public long GuildExpPerChallenge
		{
			get;
		}

		[Description("ギルドレイドボスID")]
		[PropertyOrder(1)]
		public long GuildRaidBossId
		{
			get;
		}

		[Description("抽選報酬ID")]
		[PropertyOrder(3)]
		public long LotteryRewardId
		{
			get;
		}

		[Description("通常ダメージバー報酬リスト")]
		[Nest(false, 0)]
		[PropertyOrder(5)]
		public IReadOnlyList<NormalDamageBarReward> NormalDamageBarRewards
		{
			get;
		}

		[Description("武具Lvリスト")]
		[PropertyOrder(4)]
		[Nest(false, 0)]
		public IReadOnlyList<GuildRaidQuestClearEquipmentLvList> QuestClearEquipmentLvList
		{
			get;
		}

		[PropertyOrder(7)]
		[Description("ワールド報酬目標リスト")]
		[Nest(false, 0)]
		public IReadOnlyList<WorldDamageBarReward> WorldDamageBarRewards
		{
			get;
		}

		[PropertyOrder(8)]
		[Description("非表示ワールドダメージ")]
		public long HideWorldDamage
		{
			get;
		}

		[SerializationConstructor]
		public GuildRaidRewardMB(long id, bool? isIgnore, string memo, long guildRaidBossId, long guildExpPerChallenge, long lotteryRewardId, IReadOnlyList<GuildRaidQuestClearEquipmentLvList> questClearEquipmentLvList, IReadOnlyList<NormalDamageBarReward> normalDamageBarRewards, IReadOnlyList<GuildDamageBarReward> guildDamageBarRewards, IReadOnlyList<WorldDamageBarReward> worldDamageBarRewards, long hideWorldDamage)
			:base(id, isIgnore, memo)
		{
			GuildRaidBossId = guildRaidBossId;
			GuildExpPerChallenge = guildExpPerChallenge;
			LotteryRewardId = lotteryRewardId;
			QuestClearEquipmentLvList = questClearEquipmentLvList;
			NormalDamageBarRewards = normalDamageBarRewards;
			GuildDamageBarRewards = guildDamageBarRewards;
			WorldDamageBarRewards = worldDamageBarRewards;
			HideWorldDamage = hideWorldDamage;
		}

		public GuildRaidRewardMB():base( 0L, false, "") { }
	}
}
