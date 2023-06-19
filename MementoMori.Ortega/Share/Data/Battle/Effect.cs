using MementoMori.Ortega.Share.Data.Interface;
using MementoMori.Ortega.Share.Enums.Battle.Skill;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Battle
{
    [MessagePackObject(true)]
    public class Effect : IDeepCopy<Effect>
    {
        public EffectType EffectType { get; set; }

        public long EffectValue { get; set; }

        public int EffectMaxCount { get; set; }

        public int EffectCount { get; set; }

        public Effect DeepCopy()
        {
            Effect effect = new Effect();
            EffectType effectType = this.EffectType;
            effect.EffectType = effectType;
            long num = this.EffectValue;
            effect.EffectValue = num;
            int num2 = this.EffectMaxCount;
            effect.EffectMaxCount = num2;
            int num3 = this.EffectCount;
            effect.EffectCount = num3;
            return effect;
        }

        public Effect()
        {
        }
    }
}