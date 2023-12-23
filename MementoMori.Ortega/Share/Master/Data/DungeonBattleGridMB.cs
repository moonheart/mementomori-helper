using System.ComponentModel;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
    [MessagePackObject(true)]
    [Description("時空の洞窟 マス詳細")]
    public class DungeonBattleGridMB : MasterBookBase
    {
        [Description("マス名キー")]
        [PropertyOrder(1)]
        public string DungeonGridNameKey { get; }

        [Description("マス種別")]
        [PropertyOrder(2)]
        public DungeonBattleGridType DungeonGridType { get; }

        [SerializationConstructor]
        public DungeonBattleGridMB(long id, bool? isIgnore, string memo, string dungeonGridNameKey, DungeonBattleGridType dungeonGridType)
            : base(id, isIgnore, memo)
        {
            DungeonGridNameKey = dungeonGridNameKey;
            DungeonGridType = dungeonGridType;
        }

        public DungeonBattleGridMB() : base(0, false, "")
        {
        }

        public bool IsBattleType()
        {
            return DungeonGridType == DungeonBattleGridType.BattleAndRelicReinforce ||
                   DungeonGridType == DungeonBattleGridType.BattleBoss ||
                   DungeonGridType == DungeonBattleGridType.BattleBossNoRelic ||
                   DungeonGridType == DungeonBattleGridType.BattleElite ||
                   DungeonGridType == DungeonBattleGridType.BattleNormal ||
                   DungeonGridType == DungeonBattleGridType.EventBattleElite ||
                   DungeonGridType == DungeonBattleGridType.EventBattleNormal ||
                   DungeonGridType == DungeonBattleGridType.EventBattleSpecial
                ;
        }
    }
}