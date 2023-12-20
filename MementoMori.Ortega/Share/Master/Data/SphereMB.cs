using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Equipment;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
    [MessagePackObject(true)]
    [Description("スフィア")]
    public class SphereMB : MasterBookBase
    {
        [Nest(false, 0)]
        [Description("潜在能力変動情報")]
        [PropertyOrder(7)]
        public BaseParameterChangeInfo BaseParameterChangeInfo { get; }

        [PropertyOrder(8)]
        [Nest(false, 0)]
        [Description("バトルパラメータ変動情報")]
        public BattleParameterChangeInfo BattleParameterChangeInfo { get; }

        [Description("スフィアの種類ID")]
        [PropertyOrder(2)]
        public long CategoryId { get; }

        [Description("説明文キー")]
        [PropertyOrder(10)]
        public string DescriptionKey { get; }

        [Description("攻撃系のスフィアか")]
        [PropertyOrder(6)]
        public bool IsAttackType { get; }

        [Description("スフィア強化に必要なアイテム")]
        [PropertyOrder(9)]
        [Nest(false, 0)]
        public IReadOnlyList<UserItem> ItemListRequiredToCombine { get; }

        [Description("スフィアレベル")]
        [PropertyOrder(4)]
        public long Lv { get; }

        [Description("名称キー")]
        [PropertyOrder(1)]
        public string NameKey { get; }

        [PropertyOrder(5)]
        [Description("レアリティ")]
        public ItemRarityFlags RarityFlags { get; }

        [Description("タイプId")]
        [PropertyOrder(3)]
        public SphereType SphereType { get; }

        [SerializationConstructor]
        public SphereMB(long id, bool? isIgnore, string memo, BaseParameterChangeInfo baseParameterChangeInfo, BattleParameterChangeInfo battleParameterChangeInfo, long categoryId,
            SphereType sphereType, string descriptionKey, bool isAttackType, IReadOnlyList<UserItem> itemListRequiredToCombine, long lv, string nameKey, ItemRarityFlags rarityFlags)
            : base(id, isIgnore, memo)
        {
            BaseParameterChangeInfo = baseParameterChangeInfo;
            BattleParameterChangeInfo = battleParameterChangeInfo;
            CategoryId = categoryId;
            DescriptionKey = descriptionKey;
            IsAttackType = isAttackType;
            ItemListRequiredToCombine = itemListRequiredToCombine;
            Lv = lv;
            NameKey = nameKey;
            RarityFlags = rarityFlags;
            SphereType = sphereType;
        }

        public SphereMB() : base(0L, false, "")
        {
        }
    }
}