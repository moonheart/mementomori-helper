using MessagePack;

namespace MementoMori.Ortega.Share.Data.DtoInfo
{
    [MessagePackObject(true)]
    public class UserMapBuildingDtoInfo
    {
        public int SelectedIndex { get; set; }

        public long QuestMapBuildingId { get; set; }

        public UserMapBuildingDtoInfo()
        {
        }
    }
}