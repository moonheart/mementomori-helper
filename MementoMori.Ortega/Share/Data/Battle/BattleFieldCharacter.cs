using MementoMori.Ortega.Share.Data.Character;
using MementoMori.Ortega.Share.Data.DtoInfo;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Battle
{
    [MessagePackObject(true)]
    public class BattleFieldCharacter
    {
        public string PlayerName { get; set; }

        public string CharacterGuid { get; set; }

        public long CharacterLevel { get; set; }

        public CharacterRarityFlags CharacterRarityFlags { get; set; }

        public long EquipmentMaxLevel { get; set; }

        public List<UserEquipmentDtoInfo> EquipmentDtoInfos { get; set; }

        public UnitType UnitType { get; set; }

        public long UnitId { get; set; }

        public JobFlags JobFlags { get; set; }

        public ElementType ElementType { get; set; }

        public BaseParameter DefaultBaseParameter { get; set; }

        public BattleParameter DefaultBattleParameter { get; set; }

        public BattleParameter BattleParameterWithoutBonus { get; set; }

        public long OnStartHP { get; set; }

        public BattlePosition DefaultPosition { get; set; }

        public int Guid
        {
            get
            {
                BattlePosition battlePosition = this.DefaultPosition;
                return battlePosition.DeckIndex;
            }
        }

        public BattleActiveSkill NormalSkill { get; set; }

        public List<BattleActiveSkill> ActiveSkills { get; set; }

        public List<BattlePassiveSkill> PassiveSkills { get; set; }

        public long OwnerPlayerId { get; set; }

        public long PlayerRankHitBonus { get; set; }

        public DungeonBattleInfo DungeonBattleInfo { get; set; }

        public int RentalRaidMaxHpRate { get; set; }

        public BattleFieldCharacter()
        {
        }
    }
}