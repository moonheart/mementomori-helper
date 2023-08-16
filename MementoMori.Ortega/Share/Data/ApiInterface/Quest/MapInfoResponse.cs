using MementoMori.Ortega.Share.Data.DtoInfo;
using MementoMori.Ortega.Share.Data.Player;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Quest
{
    [MessagePackObject(true)]
    public class MapInfoResponse : ApiResponseBase
    {
        public int AutoBattleDropEquipmentPercent { get; set; }

        public List<long> CurrentQuestAutoEnemyIds { get; set; }

        public List<MapPlayerInfo> MapOtherPlayerInfos { get; set; }

        public List<MapPlayerInfo> MapPlayerInfos { get; set; }

        public List<UserMapBuildingDtoInfo> UserMapBuildingDtoInfos { get; set; }
    }
}