using System.ComponentModel;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
    [Description("時空の洞窟 敵情報")]
    [MessagePackObject(true)]
    public class DungeonBattleEnemyInfo
    {
        [Description("戦闘力")]
        public long BattlePower { get; set; }

        [Description("レアリティ")]
        public CharacterRarityFlags CharacterRarityFlags { get; set; }

        [Description("属性")]
        public ElementType ElementType { get; set; }

        [Description("レベル")]
        public long Level { get; set; }

        [Description("ユニットアイコンID")]
        public long UnitIconId { get; set; }

        [Description("ユニットアイコンタイプ")]
        public UnitIconType UnitIconType { get; set; }

        public DungeonBattleEnemyInfo()
        {
        }
    }
}