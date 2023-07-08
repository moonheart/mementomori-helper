using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Battle.Result;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.TowerBattle
{
    [MessagePackObject(true)]
    public class TowerBattleQuickResponse : ApiResponseBase, IUserSyncApiResponse
    {
        public List<BattleReward> BattleRewardList { get; set; }

        public BattleRewardResult BattleRewardResult { get; set; }

        public UserSyncData UserSyncData { get; set; }
    }
}