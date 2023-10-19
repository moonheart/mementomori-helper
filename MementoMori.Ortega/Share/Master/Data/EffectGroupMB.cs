using System.ComponentModel;

using MementoMori.Ortega.Share.Data.Battle;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
    [Description("スキル効果グループ")]
    [MessagePackObject(true)]
    public class EffectGroupMB : MasterBookBase
    {
        [PropertyOrder(6)]
        [Description("付与者アイコンID")]
        public long CasterIconId { get; }

        [PropertyOrder(5)]
        [Description("付与者アイコンタイプ")]
        public EffectGroupIconType CasterIconType { get; }

        [PropertyOrder(2)]
        [Description("説明文キー")]
        public string DescriptionKey { get; }

        [PropertyOrder(4)]
        [Description("効果アイコンID")]
        public long IconId { get; }

        [PropertyOrder(3)]
        [Description("効果アイコンタイプ")]
        public EffectGroupIconType IconType { get; }

        [PropertyOrder(1)]
        [Description("効果名キー")]
        public string NameKey { get; }

        [PropertyOrder(7)]
        [Description("強制非表示フラグ")]
        public bool IsHide { get; }

        [PropertyOrder(8)]
        [Description("ターン数非表示フラグ")]
        public bool IsTurnHide { get; }

        [Nest(false, 0)]
        [PropertyOrder(9)]
        [Description("同じEffectGroupリスト")]
        public IReadOnlyList<EffectGroupInfo> EffectGroupInfoList { get; }

        [SerializationConstructor]
        public EffectGroupMB(long id, bool? isIgnore, string memo, string nameKey, string descriptionKey, EffectGroupIconType iconType, long iconId, EffectGroupIconType casterIconType, long casterIconId, bool isHide, bool isTurnHide, IReadOnlyList<EffectGroupInfo> effectGroupInfoList)
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
            EffectGroupInfoList = effectGroupInfoList;
        }

        public EffectGroupMB() : base(0, false, "")
        {
        }
    }
}