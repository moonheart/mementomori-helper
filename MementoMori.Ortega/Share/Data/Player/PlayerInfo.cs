using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Character;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Player
{
    [MessagePackObject(true)]
    public class PlayerInfo
    {
        public long BackgroundCharacterId { get; set; }

        public List<UserCharacterInfo> DeckUserCharacterInfoList { get; set; }

        public long BattlePower { get; set; }

        public string Comment { get; set; }

        public long CumulativeGuildFame { get; set; }

        public FriendStatusType FriendStatus { get; set; }

        public long GuildId { get; set; }

        public long GuildJoinRequestUtcTimeStamp { get; set; }

        public long GuildJoinTimeStamp { get; set; }

        public string GuildName { get; set; }

        public long GuildPeriodTotalFame { get; set; }

        public bool IsBlock { get; set; }

        public bool IsRecruit { get; set; }

        public TimeSpan LastLoginTime { get; set; }

        public long LatestQuestId { get; set; }

        public long LatestTowerBattleQuestId { get; set; }

        public long LocalRaidBattlePower { get; set; }

        public long MainCharacterIconId { get; set; }

        public string NpcNameKey { get; set; }

        public PlayerGuildPositionType PlayerGuildPositionType { get; set; }

        public long PlayerId { get; set; }

        public int PlayerLevel { get; set; }

        public string PlayerName { get; set; }

        public LegendLeagueClassType PrevLegendLeagueClass { get; set; }

        public long RecruitGuildMemberTimeStamp { get; set; }

        public PlayerRecruitType PlayerRecruitType { get; set; }

        public PlayerCommunicationPolicyType CommunicationPolicyType { get; set; }

        public PlayerEventPolicyType EventPolicyType { get; set; }

        public PlayerGuildBattlePolicyType GuildBattlePolicyType { get; set; }

        public long BattleLeagueRankingToday { get; set; }

        public long LegendLeagueRankingToday { get; set; }

        public long LegendLeaguePointToday { get; set; }

        public long ChatBalloonItemId { get; set; }

        public bool IsAllowedFriendBattle { get; set; }

        public List<long> GetAutoBattleCharacterIds()
        {
            throw new NotImplementedException();
        }

        public PlayerInfo()
        {
        }

        public string GetPlayerName()
        {
            return null;
        }
    }
}