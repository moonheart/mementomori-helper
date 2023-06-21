using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Battle
{
    [MessagePackObject(true)]
    public class ActiveSkillInfo
    {
        [Description("優先順位")]
        public int OrderNumber { get; set; }

        [Description("スキル説明文キー")]
        public string DescriptionKey { get; set; }

        [Description("キャラクターレベル制限")]
        public long CharacterLevel { get; set; }

        [Description("専属武具レアリティ")]
        public EquipmentRarityFlags EquipmentRarityFlags { get; set; }

        [Description("加護ID")]
        public long BlessingItemId { get; set; }

        [Description("サブセットIDリスト")]
        public List<long> SubSetSkillIds { get; set; }

        public ActiveSkillInfo()
        {
        }
    }
}