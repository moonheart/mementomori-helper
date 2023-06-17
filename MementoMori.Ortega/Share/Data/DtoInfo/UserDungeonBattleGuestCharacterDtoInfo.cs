using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Battle;
using MementoMori.Ortega.Share.Data.Character;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.DtoInfo
{
    [MessagePackObject(true)]
    public class UserDungeonBattleGuestCharacterDtoInfo
    {
        public BaseParameter BaseParameter { get; set; }

        public BattleParameter BattleParameter { get; set; }
        public long BattlePower { get; set; }

        public long CharacterId { get; set; }

        public List<UserEquipmentDtoInfo> GuestEquipmentDtoInfos { get; set; }

        public string Guid { get; set; }

        public long Level { get; set; }

        public long PlayerId { get; set; }

        public CharacterRarityFlags RarityFlags { get; set; }

        public UserDungeonBattleGuestCharacterDtoInfo()
        {
        }
    }
}