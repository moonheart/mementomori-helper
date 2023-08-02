using System.ComponentModel;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
    [Description("スキル効果グループ")]
    [MessagePackObject(true)]
    public class EffectGroupMB : MasterBookBase
    {
        [Description("付与者アイコンID")]
        [PropertyOrder(6)]
        public long CasterIconId { get; }

        [Description("付与者アイコンタイプ")]
        [PropertyOrder(5)]
        public EffectGroupIconType CasterIconType { get; }

        [PropertyOrder(2)]
        [Description("説明文キー")]
        public string DescriptionKey { get; }

        [Description("効果アイコンID")]
        [PropertyOrder(4)]
        public long IconId { get; }

        [PropertyOrder(3)]
        [Description("効果アイコンタイプ")]
        public EffectGroupIconType IconType { get; }

        [Description("効果名キー")]
        [PropertyOrder(1)]
        public string NameKey { get; }

        [Description("強制非表示フラグ")]
        [PropertyOrder(7)]
        public bool IsHide { get; }

        [Description("ターン数非表示フラグ")]
        [PropertyOrder(8)]
        public bool IsTurnHide { get; }

        // [Description("同じEffectGroupリスト")]
        // [Nest(false, 0)]
        // [PropertyOrder(9)]
        // public IReadOnlyList<EffectGroupInfo> EffectGroupInfoList { get; }

        [SerializationConstructor]
        public EffectGroupMB(long id, bool? isIgnore, string memo, string nameKey, string descriptionKey, EffectGroupIconType iconType, long iconId, EffectGroupIconType casterIconType, long casterIconId, bool isHide, bool isTurnHide/*, IReadOnlyList<EffectGroupInfo> effectGroupInfoList*/)
            : base(id, isIgnore, memo)
        {
            NameKey = nameKey;
            DescriptionKey = descriptionKey;
            IconType = iconType;
            IconId = iconId;
            CasterIconType = casterIconType;
            CasterIconId = casterIconId;
            IsHide = isHide;
            IsTurnHide = isTurnHide;
            // EffectGroupInfoList = effectGroupInfoList;
        }

        public EffectGroupMB() : base(0, false, "")
        {
        }
    }
}