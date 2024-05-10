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
    [Description("幻影の神殿敵データ")]
    public class LocalRaidEnemyMB : MasterBookBase, IBattleEnemy
    {
        [Description("アクティブスキルIDのリスト")]
        [PropertyOrder(12)]
        public IReadOnlyList<long> ActiveSkillIds { get; }

        [Description("ベースパラメータ")]
        [Nest(true, 1)]
        [PropertyOrder(9)]
        public BaseParameter BaseParameter { get; }

        [Description("敵キャラクターID")]
        [PropertyOrder(14)]
        public long BattleEnemyCharacterId { get; }

        [Description("ユニットアイコンタイプ")]
        [PropertyOrder(1)]
        public UnitIconType UnitIconType { get; }

        [Description("ユニットアイコンID")]
        [PropertyOrder(2)]
        public long UnitIconId { get; }

        [Nest(true, 1)]
        [Description("バトルパラメータ")]
        [PropertyOrder(10)]
        public BattleParameter BattleParameter { get; }

        [Description("戦闘力")]
        [PropertyOrder(5)]
        public long BattlePower { get; }

        [Description("職業")]
        [PropertyOrder(6)]
        public JobFlags JobFlags { get; }

        [Description("属性")]
        [PropertyOrder(7)]
        public ElementType ElementType { get; }

        [Description("レアリティ")]
        [PropertyOrder(8)]
        public CharacterRarityFlags CharacterRarityFlags { get; }

        [PropertyOrder(4)]
        [Description("敵のランク")]
        public long EnemyRank { get; }

        [PropertyOrder(3)]
        [Description("名称キー")]
        public string NameKey { get; }

        [Description("通常攻撃として使ってくるスキルID")]
        [PropertyOrder(11)]
        public long NormalSkillId { get; }

        [Description("パッシブスキルIDのリスト")]
        [PropertyOrder(13)]
        public IReadOnlyList<long> PassiveSkillIds { get; }

        [PropertyOrder(17)]
        [Description("敵調整ID")]
        public long EnemyAdjustId { get; }

        [PropertyOrder(15)]
        [Description("敵武具ID")]
        public long EnemyEquipmentId { get; }

        [PropertyOrder(16)]
        [Description("専用武器レアリティ")]
        public EquipmentRarityFlags ExclusiveEquipmentRarityFlags { get; }

        [SerializationConstructor]
        public LocalRaidEnemyMB(long id, bool? isIgnore, string memo, IReadOnlyList<long> activeSkillIds, BaseParameter baseParameter, UnitIconType unitIconType, long unitIconId, BattleParameter battleParameter, long enemyRank, long battlePower, JobFlags jobFlags, ElementType elementType, CharacterRarityFlags characterRarityFlags, string nameKey, long normalSkillId, IReadOnlyList<long> passiveSkillIds, long battleEnemyCharacterId, long enemyEquipmentId, EquipmentRarityFlags exclusiveEquipmentRarityFlags, long enemyAdjustId)
            : base(id, isIgnore, memo)
        {
            ActiveSkillIds = activeSkillIds;
            BaseParameter = baseParameter;
            UnitIconType = unitIconType;
            UnitIconId = unitIconId;
            BattleParameter = battleParameter;
            EnemyRank = enemyRank;
            BattlePower = battlePower;
            JobFlags = jobFlags;
            ElementType = elementType;
            CharacterRarityFlags = characterRarityFlags;
            NameKey = nameKey;
            NormalSkillId = normalSkillId;
            PassiveSkillIds = passiveSkillIds;
            BattleEnemyCharacterId = battleEnemyCharacterId;
            EnemyEquipmentId = enemyEquipmentId;
            ExclusiveEquipmentRarityFlags = exclusiveEquipmentRarityFlags;
            EnemyAdjustId = enemyAdjustId;
        }

        public LocalRaidEnemyMB() : base(0L, false, "")
        {
        }
    }
}