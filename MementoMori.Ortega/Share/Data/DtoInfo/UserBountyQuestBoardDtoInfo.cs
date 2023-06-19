using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.DtoInfo
{
    [MessagePackObject(true)]
    public class UserBountyQuestBoardDtoInfo
    {
        public BountyQuestType BountyQuestType { get; set; }

        public BountyQuestRarityFlags BountyQuestRarity { get; set; }

        public int ClearCount { get; set; }

        public UserBountyQuestBoardDtoInfo()
        {
        }
    }
}