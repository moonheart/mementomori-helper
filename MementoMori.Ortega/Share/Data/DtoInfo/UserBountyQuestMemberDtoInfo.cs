using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.DtoInfo
{
    [MessagePackObject(true)]
    public class UserBountyQuestMemberDtoInfo
    {
        public string UserCharacterGuid { get; set; }

        public long CharacterId { get; set; }

        public CharacterRarityFlags RarityFlags { get; set; }

        public long DispatchPlayerId { get; set; }

        public string DispatchPlayerName { get; set; }

        public long DispatchEndTime { get; set; }

        public long PlayerId { get; set; }

        public UserBountyQuestMemberDtoInfo()
        {
        }
    }
}