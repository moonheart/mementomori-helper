using System.ComponentModel;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.BountyQuest
{
    [MessagePackObject(true)]
    public class BountyQuestConditionInfo
    {
        [Description("懸賞カウンター条件タイプ")]
        public BountyQuestConditionType BountyQuestConditionType { get; set; }

        [Description("属性")]
        public ElementType ElementType { get; set; }

        [Description("レアリティ")]
        public CharacterRarityFlags Rarity { get; set; }

        [Description("必要な数")]
        public int RequireCount { get; set; }

        public BountyQuestConditionInfo()
        {
        }
    }
}