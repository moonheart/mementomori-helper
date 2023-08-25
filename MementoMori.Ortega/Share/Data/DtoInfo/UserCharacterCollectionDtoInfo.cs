using MessagePack;

namespace MementoMori.Ortega.Share.Data.DtoInfo
{
    [MessagePackObject(true)]
    public class UserCharacterCollectionDtoInfo
    {
        public long CharacterCollectionId { get; set; }

        public int CollectionLevel { get; set; }
    }
}