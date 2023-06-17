using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.DtoInfo
{
    [MessagePackObject(true)]
    public class UserLevelLinkMemberDtoInfo
    {
        public int CellNo { get; set; }

        public string UserCharacterGuid { get; set; }

        public long CharacterId { get; set; }

        public long UnavailableTime { get; set; }

        public UserLevelLinkMemberDtoInfo()
        {
        }
    }
}