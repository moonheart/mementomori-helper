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
        [Description("サブセットスキルリスト")]
        [PropertyOrder(4)]
        [Nest(true, 1)]
        public IReadOnlyList<ActiveSkillInfo> ActiveSkillInfos { get; }

        [Description("スキル名キー")]
        [PropertyOrder(1)]
        public string NameKey { get; }

        [Description("初期スキルクールタイム")]
        [PropertyOrder(2)]
        public int SkillInitCoolTime { get; }

        [PropertyOrder(3)]
        [Description("スキルMAXクールタイム")]
        public int SkillMaxCoolTime { get; }

        [PropertyOrder(5)]
        [Description("ルートスキルID")]
        public long RootActiveSkillId { get; }

        [SerializationConstructor]
        public ActiveSkillMB(long id, bool? isIgnore, string memo, string nameKey, int skillInitCoolTime,
            int skillMaxCoolTime, IReadOnlyList<ActiveSkillInfo> activeSkillInfos, long rootActiveSkillId)
            :base(id, isIgnore, memo)
        {
            this.NameKey = nameKey;
            this.SkillInitCoolTime = skillInitCoolTime;
            this.SkillMaxCoolTime = skillMaxCoolTime;
            this.ActiveSkillInfos = activeSkillInfos;
            this.RootActiveSkillId = rootActiveSkillId;
        }

        public ActiveSkillMB() : base(0L, false, "")
        {
        }
    }
}