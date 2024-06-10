using System.ComponentModel;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
    [MessagePackObject(true)]
    [Description("特別アイコンアイテム")]
    public class SpecialIconItemMB : MasterBookBase
    {
        [PropertyOrder(6)]
        [Description("キャラクターID")]
        public long CharacterId { get; }

        [PropertyOrder(3)]
        [Description("説明文キー")]
        public string DescriptionKey { get; }

        [PropertyOrder(2)]
        [Description("表示名キー")]
        public string DisplayNameKey { get; }

        [PropertyOrder(4)]
        [Description("アイコンID")]
        public long IconId { get; }

        [PropertyOrder(5)]
        [Description("レアリティ")]
        public ItemRarityFlags ItemRarityFlags { get; }

        [PropertyOrder(1)]
        [Description("名称キー")]
        public string NameKey { get; }

        [PropertyOrder(7)]
        [Description("バッジ表示フラグ")]
        public bool DisplayBadge { get; }

        [SerializationConstructor]
        public SpecialIconItemMB(long id, bool? isIgnore, string memo, long characterId, string descriptionKey, string displayNameKey, long iconId, ItemRarityFlags itemRarityFlags, string nameKey, bool displayBadge)
            : base(id, isIgnore, memo)
        {
            CharacterId = characterId;
            DescriptionKey = descriptionKey;
            DisplayNameKey = displayNameKey;
            IconId = iconId;
            ItemRarityFlags = itemRarityFlags;
            NameKey = nameKey;
            DisplayBadge = displayBadge;
        }

        public SpecialIconItemMB() : base(0, null, null)
        {
        }
    }
}