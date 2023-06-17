using MessagePack;

namespace MementoMori.Ortega.Share.Data.DtoInfo
{
    [MessagePackObject(true)]
    public class UserLevelLinkDtoInfo
    {
        public int PartyMaxLevel { get; set; }

        public int PartyLevel { get; set; }

        public int PartySubLevel { get; set; }

        public int MemberMaxCount { get; set; }

        public int BuyFrameCount { get; set; }

        public bool IsPartyMode { get; set; }

        public UserLevelLinkDtoInfo()
        {
        }
    }
}