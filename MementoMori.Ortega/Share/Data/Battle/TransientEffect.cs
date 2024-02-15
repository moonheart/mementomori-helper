using System.ComponentModel;
using MementoMori.Ortega.Share.Enums.Battle.Skill;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Battle
{
    [MessagePackObject(true)]
    public class TransientEffect
    {
        [Description("効果種別")]
        public EffectType EffectType { get; set; }

        [Description("効果値")]
        public long EffectValue { get; set; }

        [Description("ヒット種別")]
        public HitType HitType { get; set; }

        [Description("付加されたバフ・デバフの効果")]
        public List<EffectGroup> AddEffectGroups { get; set; } = new();

        [Description("削除されたバフ・デバフの効果")]
        public List<EffectGroup> RemoveEffectGroups { get; set; } = new();
    }
}