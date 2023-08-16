using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Quest
{
    [MessagePackObject(true)]
    [OrtegaApi("quest/mapInfo", true, false)]
    public class MapInfoRequest : ApiRequestBase
    {
        public bool IsUpdateOtherPlayerInfo { get; set; }
    }
}