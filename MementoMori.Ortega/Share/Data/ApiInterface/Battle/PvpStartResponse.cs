using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Battle.Result;
using MementoMori.Ortega.Share.Data.Player;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Battle
{
    [MessagePackObject(true)]
    public class PvpStartResponse : ApiResponseBase, IUserSyncApiResponse
    {
        public long AfterRank { get; set; }

        public BattleResult BattleResult { get; set; }

        public BattleRewardResult BattleRewardResult { get; set; }

        public long BeforeRank { get; set; }

        public bool CanBattle { get; set; }

        public bool IsNewRecord { get; set; }

        public long RivalBattlePower { get; set; }

        public PlayerInfo RivalPlayerInfo { get; set; }

        public UserSyncData UserSyncData { get; set; }

        public PvpStartResponse()
        {
        }
    }
}