using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Interface;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Battle
{
    [Description("バトルパラメータ")]
    [MessagePackObject(true)]
    public class BattleParameter : IDeepCopy<BattleParameter>
    {
        [Description("攻撃力")]
        public long AttackPower { get; set; }

        [Description("回避")]
        public long Avoidance { get; set; }

        [Description("クリティカル")]
        public long Critical { get; set; }

        [Description("クリダメ強化")]
        public long CriticalDamageEnhance { get; set; }

        [Description("クリティカル耐性")]
        public long CriticalResist { get; set; }

        [Description("ダメージ強化")]
        public long DamageEnhance { get; set; }

        [Description("カウンタ\u200b")]
        public int DamageReflect { get; set; }

        [Description("弱体効果命中\u200b")]
        public long DebuffHit { get; set; }

        [Description("弱体効果耐性")]
        public long DebuffResist { get; set; }

        [Description("防御力")]
        public long Defense { get; set; }

        [Description("防御貫通力")]
        public long DefensePenetration { get; set; }

        [Description("命中")]
        public long Hit { get; set; }

        [Description("HP")]
        public long HP { get; set; }

        [Description("HP吸収")]
        public int HpDrain { get; set; }

        [Description("魔法クリダメ緩和")]
        public int MagicCriticalDamageRelax { get; set; }

        [Description("魔法ダメージ緩和")]
        public long MagicDamageRelax { get; set; }

        [Description("物理クリダメ緩和")]
        public int PhysicalCriticalDamageRelax { get; set; }

        [Description("物理ダメージ緩和")]
        public long PhysicalDamageRelax { get; set; }

        [Description("スピード\u200b")]
        public int Speed { get; set; }

        public BattleParameter DeepCopy()
        {
            BattleParameter battleParameter = new BattleParameter();
            battleParameter.HP = DefaultIdLessThanZero(HP);
            battleParameter.AttackPower = DefaultIdLessThanZero(AttackPower);
            battleParameter.PhysicalDamageRelax = DefaultIdLessThanZero(PhysicalDamageRelax);
            battleParameter.MagicDamageRelax = DefaultIdLessThanZero(MagicDamageRelax);
            battleParameter.Hit = DefaultIdLessThanZero(Hit);
            battleParameter.Avoidance = DefaultIdLessThanZero(Avoidance);
            battleParameter.Critical = DefaultIdLessThanZero(Critical);
            battleParameter.CriticalResist = DefaultIdLessThanZero(CriticalResist);
            battleParameter.CriticalDamageEnhance = DefaultIdLessThanZero(CriticalDamageEnhance);
            battleParameter.PhysicalCriticalDamageRelax = DefaultIdLessThanZero(PhysicalCriticalDamageRelax);
            battleParameter.MagicCriticalDamageRelax = DefaultIdLessThanZero(MagicCriticalDamageRelax);
            battleParameter.DefensePenetration = DefaultIdLessThanZero(DefensePenetration);
            battleParameter.Defense = DefaultIdLessThanZero(Defense);
            battleParameter.DamageEnhance = DefaultIdLessThanZero(DamageEnhance);
            battleParameter.DebuffHit = DefaultIdLessThanZero(DebuffHit);
            battleParameter.DebuffResist = DefaultIdLessThanZero(DebuffResist);
            battleParameter.DamageReflect = DefaultIdLessThanZero(DamageReflect);
            battleParameter.HpDrain = DefaultIdLessThanZero(HpDrain);
            battleParameter.Speed = DefaultIdLessThanZero(Speed);
            return battleParameter;
        }

        private int DefaultIdLessThanZero(int n)
        {
            return n;
            return n <= 0 ? 0 : n;
        }   
        private long DefaultIdLessThanZero(long n)
        {
            return n;
            return n <= 0 ? 0 : n;
        }

        public long GetParameter(BattleParameterType parameterType)
        {
            return parameterType switch
            {
                BattleParameterType.Hp => HP,
                BattleParameterType.AttackPower => AttackPower,
                BattleParameterType.PhysicalDamageRelax => PhysicalDamageRelax,
                BattleParameterType.MagicDamageRelax => MagicDamageRelax,
                BattleParameterType.Hit => Hit,
                BattleParameterType.Avoidance => Avoidance,
                BattleParameterType.Critical => Critical,
                BattleParameterType.CriticalResist => CriticalResist,
                BattleParameterType.CriticalDamageEnhance => CriticalDamageEnhance,
                BattleParameterType.PhysicalCriticalDamageRelax => PhysicalCriticalDamageRelax,
                BattleParameterType.MagicCriticalDamageRelax => MagicCriticalDamageRelax,
                BattleParameterType.DefensePenetration => DefensePenetration,
                BattleParameterType.Defense => Defense,
                BattleParameterType.DamageEnhance => DamageEnhance,
                BattleParameterType.DebuffHit => DebuffHit,
                BattleParameterType.DebuffResist => DebuffResist,
                BattleParameterType.DamageReflect => DamageReflect,
                BattleParameterType.HpDrain => HpDrain,
                BattleParameterType.Speed => Speed,
                _ => throw new ArgumentOutOfRangeException(nameof(parameterType), parameterType, null)
            };
        }

        public BattleParameter BattleParameterCorrected(double rate)
        {
            BattleParameter battleParameter = new BattleParameter();
            battleParameter.HP = (long) (battleParameter.HP * rate);
            battleParameter.AttackPower = (long) (battleParameter.AttackPower * rate);
            battleParameter.PhysicalDamageRelax = (long) (battleParameter.PhysicalDamageRelax * rate);
            battleParameter.MagicDamageRelax = (long) (battleParameter.MagicDamageRelax * rate);
            battleParameter.Hit = (long) (battleParameter.Hit * rate);
            battleParameter.Avoidance = (long) (battleParameter.Avoidance * rate);
            battleParameter.Critical = (long) (battleParameter.Critical * rate);
            battleParameter.CriticalResist = (long) (battleParameter.CriticalResist * rate);
            battleParameter.CriticalDamageEnhance = (long) (battleParameter.CriticalDamageEnhance * rate);
            battleParameter.PhysicalCriticalDamageRelax = (int) (battleParameter.PhysicalCriticalDamageRelax * rate);
            battleParameter.MagicCriticalDamageRelax = (int) (battleParameter.MagicCriticalDamageRelax * rate);
            battleParameter.DefensePenetration = (long) (battleParameter.DefensePenetration * rate);
            battleParameter.Defense = (long) (battleParameter.Defense * rate);
            battleParameter.DamageEnhance = (long) (battleParameter.DamageEnhance * rate);
            battleParameter.DebuffHit = (long) (battleParameter.DebuffHit * rate);
            battleParameter.DebuffResist = (long) (battleParameter.DebuffResist * rate);
            battleParameter.DamageReflect = (int) (battleParameter.DamageReflect * rate);
            battleParameter.HpDrain = (int) (battleParameter.HpDrain * rate);
            battleParameter.Speed = (int) (battleParameter.Speed * rate);
            return battleParameter;
        }

        public BattleParameter Add(BattleParameter battleParameter)
        {
            BattleParameter battleParameter2 = new BattleParameter();
            battleParameter2.HP = HP + battleParameter.HP;
            battleParameter2.AttackPower = AttackPower + battleParameter.AttackPower;
            battleParameter2.PhysicalDamageRelax = PhysicalDamageRelax + battleParameter.PhysicalDamageRelax;
            battleParameter2.MagicDamageRelax = MagicDamageRelax + battleParameter.MagicDamageRelax;
            battleParameter2.Hit = Hit + battleParameter.Hit;
            battleParameter2.Avoidance = Avoidance + battleParameter.Avoidance;
            battleParameter2.Critical = Critical + battleParameter.Critical;
            battleParameter2.CriticalResist = CriticalResist + battleParameter.CriticalResist;
            battleParameter2.CriticalDamageEnhance = CriticalDamageEnhance + battleParameter.CriticalDamageEnhance;
            battleParameter2.PhysicalCriticalDamageRelax = PhysicalCriticalDamageRelax + battleParameter.PhysicalCriticalDamageRelax;
            battleParameter2.MagicCriticalDamageRelax = MagicCriticalDamageRelax + battleParameter.MagicCriticalDamageRelax;
            battleParameter2.DefensePenetration = DefensePenetration + battleParameter.DefensePenetration;
            battleParameter2.Defense = Defense + battleParameter.Defense;
            battleParameter2.DamageEnhance = DamageEnhance + battleParameter.DamageEnhance;
            battleParameter2.DebuffHit = DebuffHit + battleParameter.DebuffHit;
            battleParameter2.DebuffResist = DebuffResist + battleParameter.DebuffResist;
            battleParameter2.DamageReflect = DamageReflect + battleParameter.DamageReflect;
            battleParameter2.HpDrain = HpDrain + battleParameter.HpDrain;
            battleParameter2.Speed = Speed + battleParameter.Speed;
            return battleParameter2;
        }

        public BattleParameter()
        {
        }
    }
}