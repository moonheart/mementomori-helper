using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Master.Data;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.DungeonBattle
{
    [MessagePackObject(true)]
    public class GetBattleGridDataResponse : ApiResponseBase
    {
        public List<DungeonBattleEnemyInfo> EnemyInfos { get; set; }

        public List<UserItem> NormalRewardItemList { get; set; }

        public List<UserItem> SpecialRewardItemList { get; set; }

        public GetBattleGridDataResponse()
        {
        }
    }
}