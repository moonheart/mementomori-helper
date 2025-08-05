using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Item;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Equipment
{
    [MessagePackObject(true)]
    public class InheritanceEquipmentResponse : ApiResponseBase, IUserSyncApiResponse
    {
        public List<UserItem> ReturnItemList { get; set; }

        public UserSyncData UserSyncData { get; set; }
    }
}