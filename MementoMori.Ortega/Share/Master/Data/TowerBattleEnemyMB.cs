using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Battle;
using MementoMori.Ortega.Share.Data.Character;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[Description("無窮の塔\u3000敵データ")]
	[MessagePackObject(true)]
	public class TowerBattleEnemyMB : MasterBookBase, IBattleEnemy
	{
		[PropertyOrder(15)]
		[Description("アクティブスキルIDのリスト")]
		public IReadOnlyList<long> ActiveSkillIds
		{
			get;
		}

		[Description("ベースパラメータ")]
		[Nest(true, 1)]
		[PropertyOrder(5)]
		public BaseParameter BaseParameter
		{
			get;
		}

		[Description("敵キャラクターID")]
		[PropertyOrder(1)]
		public long BattleEnemyCharacterId
		{
			get;
		}

		[PropertyOrder(6)]
		[Description("バトルパラメータ")]
		[Nest(true, 1)]
		public BattleParameter BattleParameter
		{
			get;
		}

		[PropertyOrder(7)]
		[Description("戦闘力")]
		public long BattlePower
		{
			get;
		}

		[Description("レアリティ")]
		[PropertyOrder(4)]
		public CharacterRarityFlags CharacterRarityFlags
		{
			get;
		}

		[Description("属性")]
		[PropertyOrder(11)]
		public ElementType ElementType
		{
			get;
		}

		[Description("敵調整値ID")]
		[PropertyOrder(8)]
		public long EnemyAdjustId
		{
			get;
		}

		[PropertyOrder(2)]
		[Description("敵武具ID")]
		public long EnemyEquipmentId
		{
			get;
		}

		[Description("敵のランク")]
		[PropertyOrder(3)]
		public long EnemyRank
		{
			get;
		}

		[Description("職業")]
		[PropertyOrder(10)]
		public JobFlags JobFlags
		{
			get;
		}

		[Description("名称キー")]
		[PropertyOrder(9)]
		public string NameKey
		{
			get;
		}

		[PropertyOrder(14)]
		[Description("通常攻撃として使ってくるスキルID")]
		public long NormalSkillId
		{
			get;
		}

		[Description("パッシブスキルIDのリスト")]
		[PropertyOrder(16)]
		public IReadOnlyList<long> PassiveSkillIds
		{
			get;
		}

		[Description("ユニットアイコンID")]
		[PropertyOrder(13)]
		public long UnitIconId
		{
			get;
		}

		[Description("ユニットアイコンタイプ")]
		[PropertyOrder(12)]
		public UnitIconType UnitIconType
		{
			get;
		}

		[SerializationConstructor]
		public TowerBattleEnemyMB(long id, bool? isIgnore, string memo, long battleEnemyCharacterId, long enemyEquipmentId, long enemyRank, CharacterRarityFlags characterRarityFlags, BaseParameter baseParameter, BattleParameter battleParameter, long battlePower, long enemyAdjustId, string nameKey, JobFlags jobFlags, ElementType elementType, UnitIconType unitIconType, long unitIconId, long normalSkillId, IReadOnlyList<long> activeSkillIds, IReadOnlyList<long> passiveSkillIds)
			:base(id, isIgnore, memo)
		{
			BattleEnemyCharacterId = battleEnemyCharacterId;
			EnemyEquipmentId = enemyEquipmentId;
			EnemyRank = enemyRank;
			CharacterRarityFlags = characterRarityFlags;
			BaseParameter = baseParameter;
			BattleParameter = battleParameter;
			BattlePower = battlePower;
			EnemyAdjustId = enemyAdjustId;
			NameKey = nameKey;
			JobFlags = jobFlags;
			ElementType = elementType;
			UnitIconType = unitIconType;
			UnitIconId = unitIconId;
			NormalSkillId = normalSkillId;
			ActiveSkillIds = activeSkillIds;
			PassiveSkillIds = passiveSkillIds;
		}

		public TowerBattleEnemyMB()  :base(0L, false, ""){}
	}
}
