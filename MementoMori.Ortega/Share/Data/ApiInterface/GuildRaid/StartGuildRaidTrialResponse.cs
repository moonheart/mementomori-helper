using MementoMori.Ortega.Share.Data.Battle.Result;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.GuildRaid;

[MessagePackObject(true)]
public class StartGuildRaidTrialResponse : ApiResponseBase
{
    public BattleResult BattleResult { get; set; }
}
