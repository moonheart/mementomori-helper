using MementoMori.Ortega.Share.Data.Interface;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Character
{
    [MessagePackObject(true)]
    public class UserCharacterInfo : IDeepCopy<UserCharacterInfo>
    {
        public string Guid { get; set; }

        public long PlayerId { get; set; }

        public long CharacterId { get; set; }

        public long Level { get; set; }

        public int SubLevel { get; set; }

        public long Exp { get; set; }

        public CharacterRarityFlags RarityFlags { get; set; }

        public bool IsLocked { get; set; }

        public UserCharacterInfo DeepCopy()
        {
            UserCharacterInfo userCharacterInfo = new UserCharacterInfo();
            userCharacterInfo.PlayerId = PlayerId;
            userCharacterInfo.CharacterId = CharacterId;
            userCharacterInfo.Exp = Exp;
            userCharacterInfo.Guid = Guid;
            userCharacterInfo.RarityFlags = RarityFlags;
            userCharacterInfo.IsLocked = IsLocked;
            userCharacterInfo.Level = Level;
            userCharacterInfo.SubLevel = SubLevel;
            return userCharacterInfo;
        }

        public UserCharacterInfo()
        {
        }
    }
}