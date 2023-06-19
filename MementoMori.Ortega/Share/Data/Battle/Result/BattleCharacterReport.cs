using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Battle.Result
{
    [MessagePackObject(true)]
    public class BattleCharacterReport
    {
        public string PlayerName { get; set; }

        public long OwnerPlayerId { get; set; }

        public int DeckIndex { get; set; }

        public BattleFieldCharacterGroupType GroupType { get; set; }

        public string CharacterGuid { get; set; }

        public int BattleCharacterGuid { get; set; }

        public UnitType UnitType { get; set; }

        public long UnitId { get; set; }

        public long CharacterLevel { get; set; }

        public CharacterRarityFlags CharacterRarityFlags { get; set; }

        public ElementType ElementType { get; set; }

        public long TotalGiveDamage { get; set; }

        public long TotalHpRecovery { get; set; }

        public long TotalReceiveDamage { get; set; }

        public long MaxHp { get; set; }

        public long Hp { get; set; }

        public BattleCharacterReport()
        {
        }
    }
}