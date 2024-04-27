using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Interface;
using MementoMori.Ortega.Share.Enums.Battle.Skill;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Battle
{
    [MessagePackObject(true)]
    public class EffectGroup : IDeepCopy<EffectGroup>
    {
        public long EffectGroupId { get; set; }

        public SkillCategory SkillCategory { get; set; }

        public EffectGroupType EffectGroupType { get; set; }

        public int EffectTurn { get; set; }

        public List<Effect> Effects { get; set; }

        public RemoveEffectType RemoveEffectType { get; set; }

        public int LinkTargetGuid { get; set; }

        public int GranterGuid { get; set; }

        public bool IsExtendEffectTurn { get; set; }

        public EffectGroup DeepCopy()
        {
            throw new NotImplementedException();
            // EffectGroup effectGroup = new EffectGroup();
            // int num = this.LinkTargetGuid;
            // effectGroup.LinkTargetGuid = num;
            // long num2 = this.EffectGroupId;
            // effectGroup.EffectGroupId = num2;
            // EffectGroupType effectGroupType = this.EffectGroupType;
            // effectGroup.EffectGroupType = effectGroupType;
            // List<Effect> list = this.Effects;
            // Func<Effect, Effect> <>9__28_ = EffectGroup.<>c.<>9__28_0;
            // if (<>9__28_ == 0)
            // {
            // 	Func<Effect, Effect> func;
            // 	EffectGroup.<>c.<>9__28_0 = func;
            // }
            // List<Effect> list2 = Enumerable.ToList<Effect>(Enumerable.Select<Effect, Effect>(list, <>9__28_));
            // effectGroup.Effects = list2;
            // int num3 = this.EffectTurn;
            // effectGroup.EffectTurn = num3;
            // RemoveEffectType removeEffectType = this.RemoveEffectType;
            // effectGroup.RemoveEffectType = removeEffectType;
            // SkillCategory skillCategory = this.SkillCategory;
            // effectGroup.SkillCategory = skillCategory;
            // return effectGroup;
        }

    }
}