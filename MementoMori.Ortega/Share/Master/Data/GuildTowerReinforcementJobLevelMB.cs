using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
    [MessagePackObject(true)]
    [Description("ギルドツリー職業強化レベル")]
    public class GuildTowerReinforcementJobLevelMB : MasterBookBase
    {
        [PropertyOrder(5)]
        [Description("攻撃力")]
        public long AttackPower { get; set; }

        [PropertyOrder(7)]
        [Description("クリティカル")]
        public int Critical { get; set; }

        [PropertyOrder(10)]
        [Description("クリダメ強化")]
        public long CriticalDamageEnhance { get; set; }

        [PropertyOrder(9)]
        [Description("弱体効果命中\u200b")]
        public int DebuffHit { get; set; }

        [PropertyOrder(1)]
        [Description("イベント番号")]
        public int EventNo { get; }

        [PropertyOrder(8)]
        [Description("命中")]
        public int Hit { get; set; }

        [PropertyOrder(6)]
        [Description("HP")]
        public long HP { get; set; }

        [PropertyOrder(2)]
        [Description("職業種別")]
        public JobFlags JobFlags { get; }

        [PropertyOrder(3)]
        [Description("レベル")]
        public int Level { get; }

        [PropertyOrder(4)]
        [Description("レベルキャップ")]
        public int LevelCap { get; }

        [Nest(false, 0)]
        [PropertyOrder(11)]
        [Description("必要強化素材リスト")]
        public IReadOnlyList<UserItem> RequiredMaterialList { get; set; }

        [SerializationConstructor]
        public GuildTowerReinforcementJobLevelMB(long id, bool? isIgnore, string memo, int eventNo, JobFlags jobFlags, int level, int levelCap, long attackPower, long hp, int critical, int hit,
            int debuffHit, long criticalDamageEnhance, IReadOnlyList<UserItem> requiredMaterialList)
            : base(id, isIgnore, memo)
        {
            EventNo = eventNo;
            JobFlags = jobFlags;
            Level = level;
            LevelCap = levelCap;
            AttackPower = attackPower;
            HP = hp;
            Critical = critical;
            Hit = hit;
            DebuffHit = debuffHit;
            CriticalDamageEnhance = criticalDamageEnhance;
            RequiredMaterialList = requiredMaterialList;
        }

        public GuildTowerReinforcementJobLevelMB() : base(default, default, default)
        {
        }
    }
}