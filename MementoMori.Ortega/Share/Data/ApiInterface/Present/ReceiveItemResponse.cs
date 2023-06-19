using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.DtoInfo;
using MementoMori.Ortega.Share.Data.Present;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Present
{
    [MessagePackObject(true)]
    public class ReceiveItemResponse : ApiResponseBase, IUserSyncApiResponse
    {
        public List<PresentItem> ResultItems { get; set; }

        public List<UserPresentDtoInfo> UpsertPresentDtoInfoList { get; set; }

        public UserSyncData UserSyncData { get; set; }

        public ReceiveItemResponse()
        {
        }
    }
}