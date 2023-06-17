namespace MementoMori.Ortega.Share.Data.DtoInfo
{
    public interface IUserStatusDtoInfo
    {
        int Birthday { get; set; }

        long BoardRank { get; set; }

        string Comment { get; set; }

        long Exp { get; set; }

        long FavoriteCharacterId1 { get; set; }

        long FavoriteCharacterId2 { get; set; }

        long FavoriteCharacterId3 { get; set; }

        long FavoriteCharacterId4 { get; set; }

        long FavoriteCharacterId5 { get; set; }

        bool IsFirstVisitGuildAtDay { get; set; }

        bool IsReachBattleLeagueTop50 { get; set; }

        bool IsAlreadyChangedName { get; set; }

        long LastLoginTime { get; set; }

        long LastLvUpTime { get; set; }

        long MainCharacterIconId { get; set; }

        string Name { get; set; }

        long PlayerId { get; set; }

        long PreviousLoginTime { get; set; }

        long Rank { get; set; }

        long Vip { get; set; }

        long VipExp { get; set; }
    }
}