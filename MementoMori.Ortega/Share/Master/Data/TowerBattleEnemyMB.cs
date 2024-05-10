using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Battle;
using MementoMori.Ortega.Share.Data.Character;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
    [MessagePackObject(true)]
    [NotUseOnAuth]
    [NotUseOnBatch]
    [NotUseOnSerialCodeInput]
    [Description("無窮の塔\u3000敵データ")]
    public class TowerBattleEnemyMB : MasterBookBase, IBattleEnemy
    {
        [PropertyOrder(12)]
        [Description("アクティブスキルIDのリスト")]
        public IReadOnlyList<long> ActiveSkillIds { get; }

        [Nest(true, 1)]
        [PropertyOrder(9)]
        [Description("ベースパラメータ")]
        public BaseParameter BaseParameter { get; }

        [PropertyOrder(14)]
        [Description("敵キャラクターID")]
        public long BattleEnemyCharacterId { get; }

        [Nest(true, 1)]
        [PropertyOrder(10)]
        [Description("バトルパラメータ")]
        public BattleParameter BattleParameter { get; }

        [PropertyOrder(5)]
        [Description("戦闘力")]
        public long BattlePower { get; }

        [PropertyOrder(8)]
        [Description("レアリティ")]
        public CharacterRarityFlags CharacterRarityFlags { get; }

        [PropertyOrder(7)]
        [Description("属性")]
        public ElementType ElementType { get; }

        [PropertyOrder(17)]
        [Description("敵調整値ID")]
        public long EnemyAdjustId { get; }

        [PropertyOrder(15)]
        [Description("敵武具ID")]
        public long EnemyEquipmentId { get; }

        [PropertyOrder(16)]
        [Description("専用武器レアリティ")]
        public EquipmentRarityFlags ExclusiveEquipmentRarityFlags { get; }

        [PropertyOrder(4)]
        [Description("敵のランク")]
        public long EnemyRank { get; }

        [PropertyOrder(6)]
        [Description("職業")]
        public JobFlags JobFlags { get; }

        [PropertyOrder(3)]
        [Description("名称キー")]
        public string NameKey { get; }

        [PropertyOrder(11)]
        [Description("通常攻撃として使ってくるスキルID")]
        public long NormalSkillId { get; }

        [PropertyOrder(13)]
        [Description("パッシブスキルIDのリスト")]
        public IReadOnlyList<long> PassiveSkillIds { get; }

        [PropertyOrder(2)]
        [Description("ユニットアイコンID")]
        public long UnitIconId { get; }

        [PropertyOrder(1)]
        [Description("ユニットアイコンタイプ")]
        public UnitIconType UnitIconType { get; }

        [SerializationConstructor]
        public TowerBattleEnemyMB(long id, bool? isIgnore, string memo, UnitIconType unitIconType, long unitIconId, string nameKey, long enemyRank, long battleEnemyCharacterId, long battlePower, JobFlags jobFlags, ElementType elementType, CharacterRarityFlags characterRarityFlags, BaseParameter baseParameter, BattleParameter battleParameter, long normalSkillId, IReadOnlyList<long> activeSkillIds, IReadOnlyList<long> passiveSkillIds, long enemyAdjustId, EquipmentRarityFlags exclusiveEquipmentRarityFlags, long enemyEquipmentId)
            : base(id, isIgnore, memo)
        {
            UnitIconType = unitIconType;
            UnitIconId = unitIconId;
            NameKey = nameKey;
            EnemyRank = enemyRank;
            BattleEnemyCharacterId = battleEnemyCharacterId;
            BattlePower = battlePower;
            JobFlags = jobFlags;
            ElementType = elementType;
            CharacterRarityFlags = characterRarityFlags;
            BaseParameter = baseParameter;
            BattleParameter = battleParameter;
            NormalSkillId = normalSkillId;
            ActiveSkillIds = activeSkillIds;
            PassiveSkillIds = passiveSkillIds;
            EnemyAdjustId = enemyAdjustId;
            ExclusiveEquipmentRarityFlags = exclusiveEquipmentRarityFlags;
            EnemyEquipmentId = enemyEquipmentId;
        }

        public TowerBattleEnemyMB() : base(0L, false, "")
        {
        }
    }
}