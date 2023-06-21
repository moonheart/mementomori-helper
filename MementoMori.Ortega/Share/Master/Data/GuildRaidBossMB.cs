using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Battle;
using MementoMori.Ortega.Share.Data.Character;
using MementoMori.Ortega.Share.Data.GuildRaid;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MementoMori.Ortega.Share.Utils;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[Description(" ギルドレイドボスデータ")]
	[MessagePackObject(true)]
	public class GuildRaidBossMB : MasterBookBase, IHasStartEndTime
	{
		[Description("アクティブスキルIDのリスト")]
		[PropertyOrder(17)]
		public IReadOnlyList<long> ActiveSkillIds
		{
			get;
		}

		[Description("ベースパラメータ")]
		[Nest(true, 1)]
		[PropertyOrder(14)]
		public BaseParameter BaseParameter
		{
			get;
		}

		[Description("バトルパラメータ")]
		[PropertyOrder(15)]
		[Nest(true, 1)]
		public BattleParameter BattleParameter
		{
			get;
		}

		[PropertyOrder(12)]
		[Description("戦闘力")]
		public long BattlePower
		{
			get;
		}

		[Description("レアリティ")]
		[PropertyOrder(13)]
		public CharacterRarityFlags CharacterRarityFlags
		{
			get;
		}

		[Description("属性")]
		[PropertyOrder(10)]
		public ElementType ElementType
		{
			get;
		}

		[Description("敵のランク")]
		[PropertyOrder(8)]
		public long EnemyRank
		{
			get;
		}

		[Description("ギルドダメージバー")]
		[PropertyOrder(20)]
		[Nest(false, 0)]
		public IReadOnlyList<GuildRaidDamageBar> GuildDamageBar
		{
			get;
		}

		[Description("ボス種別")]
		[PropertyOrder(3)]
		public GuildRaidBossType GuildRaidBossType
		{
			get;
		}

		[PropertyOrder(11)]
		[Description("職業")]
		public JobFlags JobFlags
		{
			get;
		}

		[PropertyOrder(9)]
		[Description("名称キー")]
		public string NameKey
		{
			get;
		}

		[PropertyOrder(19)]
		[Nest(false, 0)]
		[Description("通常ダメージバー")]
		public IReadOnlyList<GuildRaidDamageBar> NormalDamageBar
		{
			get;
		}

		[Description("通常攻撃として使ってくるスキルID")]
		[PropertyOrder(16)]
		public long NormalSkillId
		{
			get;
		}

		[Description("パッシブスキルIDのリスト")]
		[PropertyOrder(18)]
		public IReadOnlyList<long> PassiveSkillIds
		{
			get;
		}

		[Description("必要ギルド貢献値")]
		[PropertyOrder(6)]
		public long ReleasableGuildFame
		{
			get;
		}

		[PropertyOrder(2)]
		[Description("ユニットアイコンID")]
		public long UnitIconId
		{
			get;
		}

		[Description("ユニットアイコンタイプ")]
		[PropertyOrder(1)]
		public UnitIconType UnitIconType
		{
			get;
		}

		[Description("開始日時（現地時間）")]
		[PropertyOrder(4)]
		public string StartTime
		{
			get;
		}

		[Description("終了日時（現地時間")]
		[PropertyOrder(5)]
		public string EndTime
		{
			get;
		}

		[Description("キャラクター座標X")]
		[PropertyOrder(21)]
		public float CharacterImageX
		{
			get;
		}

		[PropertyOrder(22)]
		[Description("キャラクター座標Y")]
		public float CharacterImageY
		{
			get;
		}

		[PropertyOrder(23)]
		[Description("バナーテキスト")]
		public string BannerText
		{
			get;
		}

		[Description("ギルドレイドボタン座標U")]
		[PropertyOrder(24)]
		public float GuildRaidButtonU
		{
			get;
		}

		[Description("ギルドレイドボタン座標V")]
		[PropertyOrder(25)]
		public float GuildRaidButtonV
		{
			get;
		}

		[SerializationConstructor]
		public GuildRaidBossMB(long id, bool? isIgnore, string memo, BaseParameter baseParameter, BattleParameter battleParameter, UnitIconType unitIconType, long unitIconId, long normalSkillId, GuildRaidBossType guildRaidBossType, long releasableGuildFame, IReadOnlyList<long> activeSkillIds, IReadOnlyList<long> passiveSkillIds, long enemyRank, JobFlags jobFlags, ElementType elementType, long battlePower, CharacterRarityFlags characterRarityFlags, string nameKey, IReadOnlyList<GuildRaidDamageBar> normalDamageBar, IReadOnlyList<GuildRaidDamageBar> guildDamageBar, string startTime, string endTime, float characterImageX, float characterImageY, string bannerText, float guildRaidButtonU, float guildRaidButtonV)
			:base(id, isIgnore, memo)
		{
			this.BaseParameter = baseParameter;
			this.BattleParameter = battleParameter;
			this.UnitIconType = unitIconType;
			this.UnitIconId = unitIconId;
			this.NormalSkillId = normalSkillId;
			this.GuildRaidBossType = guildRaidBossType;
			this.ReleasableGuildFame = releasableGuildFame;
			this.ActiveSkillIds = activeSkillIds;
			this.PassiveSkillIds = passiveSkillIds;
			this.EnemyRank = enemyRank;
			this.JobFlags = jobFlags;
			this.ElementType = elementType;
			this.BattlePower = battlePower;
			this.CharacterRarityFlags = characterRarityFlags;
			this.NameKey = nameKey;
			this.NormalDamageBar = normalDamageBar;
			this.GuildDamageBar = guildDamageBar;
			this.StartTime = startTime;
			this.EndTime = endTime;
			this.CharacterImageX = characterImageX;
			this.CharacterImageY = characterImageY;
			this.BannerText = bannerText;
			this.GuildRaidButtonU = guildRaidButtonU;
			this.GuildRaidButtonV = guildRaidButtonV;
		}

		public GuildRaidBossMB() : base(0L, false, "")
		{
			
		}
	}
}
