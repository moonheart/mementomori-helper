using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.DtoInfo;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.GuildRaid
{
    [MessagePackObject(true)]
    public class OpenGuildRaidResponse : ApiResponseBase, IGuildSyncApiResponse
    {
        public GuildRaidBossInfo GuildRaidBossInfo { get; set; }

        public GuildSyncData GuildSyncData { get; set; }
    }
}