using System.ComponentModel;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
    [MessagePackObject(true)]
    [Description("武具進化素材")]
    public class EquipmentSetMaterialMB : MasterBookBase
    {
        [PropertyOrder(4)]
        [Description("説明文キー")]
        public string DescriptionKey { get; }

        [PropertyOrder(5)]
        [Description("アイコンID")]
        public long IconId { get; }

        [PropertyOrder(7)]
        [Description("レアリティ")]
        public ItemRarityFlags ItemRarityFlags { get; }

        [PropertyOrder(3)]
        [Description("レベル")]
        public long Lv { get; }

        [PropertyOrder(1)]
        [Description("名称キー")]
        public string NameKey { get; }

        [PropertyOrder(2)]
        [Description("表示名キー")]
        public string DisplayNameKey { get; }

        [PropertyOrder(6)]
        [Description("入手ステージ Quest ID")]
        public IReadOnlyList<long> QuestIdList { get; }

        [PropertyOrder(8)]
        [Description("入手宝箱Id")]
        public long TreasureChestId { get; }

        [PropertyOrder(9)]
        [Description("入手アダマントボックスId")]
        public long EquipmentSetMaterialBoxId { get; }

        [SerializationConstructor]
        public EquipmentSetMaterialMB(long id, bool? isIgnore, string memo, string descriptionKey, long iconId, ItemRarityFlags itemRarityFlags, long lv, string nameKey, string displayNameKey, IReadOnlyList<long> questIdList, long treasureChestId, long equipmentSetMaterialBoxId)
            : base(id, isIgnore, memo)
        {
            this.DescriptionKey = descriptionKey;
            this.IconId = iconId;
            this.ItemRarityFlags = itemRarityFlags;
            this.Lv = lv;
            this.NameKey = nameKey;
            this.DisplayNameKey = displayNameKey;
            this.QuestIdList = questIdList;
            this.TreasureChestId = treasureChestId;
            this.EquipmentSetMaterialBoxId = equipmentSetMaterialBoxId;
        }

        public EquipmentSetMaterialMB()
            : base(0L, null, null)
        {
        }
    }
}
