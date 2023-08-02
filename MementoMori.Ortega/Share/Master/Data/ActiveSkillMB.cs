using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Battle;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
    [Description("アクティブスキル")]
    [MessagePackObject(true)]
    public class ActiveSkillMB : MasterBookBase
    {
        [PropertyOrder(4)]
        [Description("アクティブスキル条件式")]
        public string ActiveSkillConditions { get; }

        [Nest(true, 1)]
        [PropertyOrder(5)]
        [Description("サブセットスキルリスト")]
        public IReadOnlyList<ActiveSkillInfo> ActiveSkillInfos { get; }

        [Description("スキル名キー")]
        [PropertyOrder(1)]
        public string NameKey { get; }

        [Description("初期スキルクールタイム")]
        [PropertyOrder(2)]
        public int SkillInitCoolTime { get; }

        [Description("スキルMAXクールタイム")]
        [PropertyOrder(3)]
        public int SkillMaxCoolTime { get; }

        [PropertyOrder(6)]
        [Description("ルートスキルID")]
        public long RootActiveSkillId { get; }

        [SerializationConstructor]
        public ActiveSkillMB(long id, bool? isIgnore, string memo, string nameKey, int skillInitCoolTime,string activeSkillConditions, 
            int skillMaxCoolTime, IReadOnlyList<ActiveSkillInfo> activeSkillInfos, long rootActiveSkillId)
            :base(id, isIgnore, memo)
        {
            this.NameKey = nameKey;
            this.SkillInitCoolTime = skillInitCoolTime;
            this.SkillMaxCoolTime = skillMaxCoolTime;
            this.ActiveSkillInfos = activeSkillInfos;
            this.ActiveSkillConditions = activeSkillConditions;
            this.RootActiveSkillId = rootActiveSkillId;
        }

        public ActiveSkillMB() : base(0L, false, "")
        {
        }
    }
}