using MementoMori.Ortega.Share.Data.Character;
using MementoMori.Ortega.Share.Enums;
using MessagePack;
using Ortega.Share.Data.Interface;

namespace MementoMori.Ortega.Share.Data.DtoInfo
{
    [MessagePackObject(true)]
    public class UserCharacterDtoInfo: ICharacterInfo
    {
        public string Guid { get; set; }

        public long PlayerId { get; set; }

        public long CharacterId { get; set; }

        public long Level { get; set; }

        public long Exp { get; set; }

        public CharacterRarityFlags RarityFlags { get; set; }

        public bool IsLocked { get; set; }

        public UserCharacterDtoInfo()
        {
        }

        public UserCharacterDtoInfo(long playerId,
            UserDungeonBattleGuestCharacterDtoInfo userDungeonBattleGuestCharacterDtoInfo)
        {
            Guid = userDungeonBattleGuestCharacterDtoInfo.Guid;
            CharacterId = userDungeonBattleGuestCharacterDtoInfo.CharacterId;
            Level = userDungeonBattleGuestCharacterDtoInfo.Level;
            PlayerId = playerId;
            RarityFlags = userDungeonBattleGuestCharacterDtoInfo.RarityFlags;
            Exp = -1L;
            IsLocked = false;
        }

        public UserCharacterDtoInfo(UserCharacterInfo userCharacterInfo)
        {
            Guid = userCharacterInfo.Guid;
            CharacterId = userCharacterInfo.CharacterId;
            Level = userCharacterInfo.Level;
            RarityFlags = userCharacterInfo.RarityFlags;
            Exp = userCharacterInfo.Exp;
            PlayerId = userCharacterInfo.PlayerId;
            IsLocked = false;
        }

        public UserCharacterDtoInfo DeepCopy()
        {
            UserCharacterDtoInfo userCharacterDtoInfo = new UserCharacterDtoInfo();
            userCharacterDtoInfo.Guid = Guid;
            userCharacterDtoInfo.PlayerId = PlayerId;
            userCharacterDtoInfo.CharacterId = CharacterId;
            userCharacterDtoInfo.Level = Level;
            userCharacterDtoInfo.Exp = Exp;
            userCharacterDtoInfo.RarityFlags = RarityFlags;
            userCharacterDtoInfo.IsLocked = IsLocked;
            return userCharacterDtoInfo;
        }

        public bool IsGuest()
        {
            bool flag = false;
            return flag;
        }

        public bool IsShare()
        {
            return false;
        }
    }
}