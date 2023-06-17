using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.DtoInfo
{
    [MessagePackObject(true)]
    public class UserCharacterBookDtoInfo
    {
        public long CharacterId { get; set; }

        public long MaxCharacterLevel { get; set; }

        public CharacterRarityFlags MaxCharacterRarityFlags { get; set; }

        public long MaxEpisodeId { get; set; }

        public UserCharacterBookDtoInfo()
        {
        }
    }
}