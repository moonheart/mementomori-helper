using MementoMori.Ortega.Share.Data.Interface;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Battle
{
    [MessagePackObject(true)]
    public class BattlePosition : IDeepCopy<BattlePosition>
    {
        public int DeckIndex { get; set; }

        public BattleFieldCharacterGroupType GroupType { get; set; }

        public bool IsAttacker => this.GroupType == BattleFieldCharacterGroupType.Attacker;

        public BattlePosition()
        {
        }

        public BattlePosition(BattleFieldCharacterGroupType groupType, int deckIndex)
        {
            this.GroupType = groupType;
            this.DeckIndex = deckIndex;
        }

        public int CalcGuid()
        {
            return this.DeckIndex;
        }

        public static BattlePosition CreateNpc(int deckIndex)
        {
            return new BattlePosition
            {
                GroupType = (BattleFieldCharacterGroupType) ((ulong) 1L),
                DeckIndex = deckIndex
            };
        }

        public BattlePosition DeepCopy()
        {
            BattleFieldCharacterGroupType battleFieldCharacterGroupType = this.GroupType;
            int num = this.DeckIndex;
            return new BattlePosition
            {
                DeckIndex = num,
                GroupType = battleFieldCharacterGroupType
            };
        }

        public override string ToString()
        {
            int num = this.DeckIndex;
            return string.Format("IsAttacker:{0} Index:{1}", "IsAttacker:{0} Index:{1}", "IsAttacker:{0} Index:{1}");
        }

        public bool SamePosition(BattlePosition other)
        {
            bool flag = this.GroupType == BattleFieldCharacterGroupType.Attacker;
            bool flag2 = other.GroupType == BattleFieldCharacterGroupType.Attacker;
            if (flag != flag2)
            {
            }

            int num = other.DeckIndex;
            return this.DeckIndex == num;
        }
    }
}