using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.DtoInfo;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Battle
{
    [MessagePackObject(true)]
    public class AutoResponse : ApiResponseBase
    {
        public UserBattleAutoDtoInfo UserBattleAuto { get; set; }

        public long RemainNextRankUpTime { get; set; }
    }
}