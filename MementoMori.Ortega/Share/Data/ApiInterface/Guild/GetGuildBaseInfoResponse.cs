using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Guild;
using MementoMori.Ortega.Share.Data.GuildTower;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Guild
{
    [MessagePackObject(true)]
    public class GetGuildBaseInfoResponse : ApiResponseBase, IGuildSyncApiResponse, IUserSyncApiResponse
    {
        public int DefaultGlobalGvgMatchingNumber { get; set; }

        public GuildGvgInfo GlobalGuildGvgInfo { get; set; }

        public long GuildLoginBonusLevel { get; set; }

        public List<GuildSystemChatOptionInfo> GuildSystemChatOptionInfos { get; set; }

        public GuildTowerComboData GuildTowerComboData { get; set; }

        public int GuildTowerCurrentFloor { get; set; }

        public int GuildTowerTodayWinCount { get; set; }

        public bool IsRaidOpen { get; set; }
        public GuildGvgInfo LocalGuildGvgInfo { get; set; }

        public GuildSyncData GuildSyncData { get; set; }

        public UserSyncData UserSyncData { get; set; }
    }
}