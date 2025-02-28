using MessagePack;

namespace MementoMori.Ortega.Share.Data.DtoInfo
{
    [MessagePackObject(true)]
    public class UserStatusDtoInfo
    {
        public long CreateAt { get; set; }

        public bool IsFirstVisitGuildAtDay { get; set; }

        public bool IsReachBattleLeagueTop50 { get; set; }

        public bool IsAlreadyChangedName { get; set; }

        public long BackgroundCharacterId { get; set; }

        public int Birthday { get; set; }

        public string Comment { get; set; }

        public long PlayerId { get; set; }

        public long MainCharacterIconId { get; set; }

        public long FavoriteCharacterId1 { get; set; }

        public long FavoriteCharacterId2 { get; set; }

        public long FavoriteCharacterId3 { get; set; }

        public long FavoriteCharacterId4 { get; set; }

        public long FavoriteCharacterId5 { get; set; }

        public string Name { get; set; }

        public long Rank { get; set; }

        public long BoardRank { get; set; }

        public long Exp { get; set; }

        public long Vip { get; set; }

        public long LastLoginTime { get; set; }

        public long PreviousLoginTime { get; set; }

        public long LastLvUpTime { get; set; }

        public long VipExp { get; set; }

        public long LastLeaveGuildTime { get; set; }

        public List<long> GetFavoriteUserCharacterIds()
        {
            return new List<long>()
            {
                FavoriteCharacterId1, FavoriteCharacterId2, FavoriteCharacterId3, FavoriteCharacterId4,
                FavoriteCharacterId5
            };
        }

        public UserStatusDtoInfo()
        {
        }
    }
}