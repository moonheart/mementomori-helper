using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.DtoInfo;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.DungeonBattle
{
    [MessagePackObject(true)]
    public class LeaveShopResponse : ApiResponseBase
    {
        public UserDungeonBattleDtoInfo UserDungeonBattleDtoInfo { get; set; }

        public LeaveShopResponse()
        {
        }
    }
}