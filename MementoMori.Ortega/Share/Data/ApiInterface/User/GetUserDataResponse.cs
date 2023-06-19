using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.User
{
    [MessagePackObject(true)]
    public class GetUserDataResponse : ApiResponseBase, IUserSyncApiResponse
    {
        public bool IsNotClearDungeonBattleMap { get; set; }

        public List<long> GachaSelectListCharacterIds { get; set; }

        public UserSyncData UserSyncData { get; set; }

        public GetUserDataResponse()
        {
        }
    }
}