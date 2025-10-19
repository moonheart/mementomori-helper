using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Interfaces;
using MementoMori.Ortega.Share.Master.Table;

namespace MementoMori.Ortega.Share;

public static class Masters
{
    public static AchieveRankingRewardTable AchieveRankingRewardTable { get; } = new();

    public static CharacterProfileTable CharacterProfileTable { get; } = new();

    public static CharacterDetailVoiceTable CharacterDetailVoiceTable { get; } = new();

    public static CommunityTable CommunityTable { get; } = new();

    public static TextResourceTable TextResourceTable { get; } = new();

    public static TextLanguageTable TextLanguageTable { get; } = new();

    public static TipTable TipTable { get; } = new();

    public static EquipmentExclusiveSkillDescriptionTable EquipmentExclusiveSkillDescriptionTable { get; } = new();

    public static BattleSkillCharacterSettingTable BattleSkillCharacterSettingTable { get; } = new();

    public static BattleSkillNameSettingTable BattleSkillNameSettingTable { get; } = new();

    public static HelpTable HelpTable { get; } = new();

    public static TutorialDescriptionTable TutorialDescriptionTable { get; } = new();

    public static TutorialTextBoxTable TutorialTextBoxTable { get; } = new();

    public static LocalPushNotificationTable LocalPushNotificationTable { get; } = new();

    public static InquiryButtonTable InquiryButtonTable { get; } = new();

    public static FaqListTable FaqListTable { get; } = new();

    public static TermsTable TermsTable { get; } = new();

    public static LocalRaidBannerTable LocalRaidBannerTable { get; } = new();

    public static DeepLinkTable DeepLinkTable { get; } = new();

    public static GachaDestinyAddCharacterTable GachaDestinyAddCharacterTable { get; } = new();

    public static ChatEffectKeywordTable ChatEffectKeywordTable { get; } = new();

    public static SkillDescriptionLinkTextTable SkillDescriptionLinkTextTable { get; } = new();

    public static ActiveSkillTable ActiveSkillTable { get; } = new();

    public static AppVersionTable AppVersionTable { get; } = new();

    public static AutoBattleEnemyTable AutoBattleEnemyTable { get; } = new();

    public static BattleScheduleTable BattleScheduleTable { get; } = new();

    public static BoardRankTable BoardRankTable { get; } = new();

    public static BookSortBonusFloorRewardTable BookSortBonusFloorRewardTable { get; } = new();

    public static BookSortEventTable BookSortEventTable { get; } = new();

    public static BossBattleEnemyTable BossBattleEnemyTable { get; } = new();

    public static BountyQuestEventTable BountyQuestEventTable { get; } = new();

    public static ChangeItemTable ChangeItemTable { get; } = new();

    public static CharacterBoxSizeTable CharacterBoxSizeTable { get; } = new();

    public static CharacterLevelTable CharacterLevelTable { get; } = new();

    public static CharacterResetTable CharacterResetTable { get; } = new();

    public static CharacterRankUpMaterialTable CharacterRankUpMaterialTable { get; } = new();

    public static CharacterLiveModeTable CharacterLiveModeTable { get; } = new();

    public static CharacterPotentialTable CharacterPotentialTable { get; } = new();

    public static CharacterPotentialCoefficientTable CharacterPotentialCoefficientTable { get; } = new();

    public static CharacterStoryTable CharacterStoryTable { get; } = new();

    public static CharacterTable CharacterTable { get; } = new();

    public static CharacterCollectionTable CharacterCollectionTable { get; } = new();

    public static CharacterCollectionLevelTable CharacterCollectionLevelTable { get; } = new();

    public static CharacterCollectionRewardTable CharacterCollectionRewardTable { get; } = new();

    public static ChapterTable ChapterTable { get; } = new();

    public static CollabMissionTable CollabMissionTable { get; } = new();

    public static DownloadRawDataTable DownloadRawDataTable { get; } = new();

    public static DungeonBattleGridTable DungeonBattleGridTable { get; } = new();

    public static DungeonBattleGuestTable DungeonBattleGuestTable { get; } = new();

    public static DungeonBattleRelicTable DungeonBattleRelicTable { get; } = new();

    public static ElementBonusTable ElementBonusTable { get; } = new();

    public static EquipmentCompositeTable EquipmentCompositeTable { get; } = new();

    public static EquipmentEvolutionTable EquipmentEvolutionTable { get; } = new();

    public static EquipmentExclusiveEffectTable EquipmentExclusiveEffectTable { get; } = new();

    public static EquipmentForgeTable EquipmentForgeTable { get; } = new();

    public static EquipmentLegendSacredTreasureTable EquipmentLegendSacredTreasureTable { get; } = new();

    public static EquipmentMatchlessSacredTreasureTable EquipmentMatchlessSacredTreasureTable { get; } = new();

    public static EquipmentReinforcementMaterialTable EquipmentReinforcementMaterialTable { get; } = new();

    public static EquipmentReinforcementParameterTable EquipmentReinforcementParameterTable { get; } = new();

    public static EquipmentSetMaterialBoxTable EquipmentSetMaterialBoxTable { get; } = new();

    public static EquipmentSetMaterialTable EquipmentSetMaterialTable { get; } = new();

    public static EquipmentSetTable EquipmentSetTable { get; } = new();

    public static EquipmentSynchroGroupTable EquipmentSynchroGroupTable { get; } = new();

    public static EquipmentTable EquipmentTable { get; } = new();

    public static ErrorCodeTable ErrorCodeTable { get; } = new();

    public static FriendCampaignTable FriendCampaignTable { get; } = new();

    public static FriendMissionTable FriendMissionTable { get; } = new();

    public static GachaCaseTable GachaCaseTable { get; } = new();

    public static GachaCaseUiTable GachaCaseUiTable { get; } = new();

    public static GachaSelectListTable GachaSelectListTable { get; } = new();

    public static GlobalGvgCastleTable GlobalGvgCastleTable { get; } = new();

    public static GuildLevelTable GuildLevelTable { get; } = new();

    public static GuildMissionTable GuildMissionTable { get; } = new();

    public static GuildRaidBossTable GuildRaidBossTable { get; } = new();

    public static GuildRaidRewardTable GuildRaidRewardTable { get; } = new();

    public static GuildTowerChallengeRewardTable GuildTowerChallengeRewardTable { get; } = new();

    public static GuildTowerEnemyTable GuildTowerEnemyTable { get; } = new();

    public static GuildTowerEventTable GuildTowerEventTable { get; } = new();

    public static GuildTowerFloorTable GuildTowerFloorTable { get; } = new();

    public static GuildTowerReinforcementJobLevelTable GuildTowerReinforcementJobLevelTable { get; } = new();

    public static ItemMaxCountSwitchingQuestTable ItemMaxCountSwitchingQuestTable { get; } = new();

    public static ItemTable ItemTable { get; } = new();

    public static MonthlyLoginBonusRewardListTable MonthlyLoginBonusRewardListTable { get; } = new();

    public static MonthlyLoginBonusTable MonthlyLoginBonusTable { get; } = new();

    public static MonologueTable MonologueTable { get; } = new();

    public static SphereTable SphereTable { get; } = new();

    public static LeadReviewTable LeadReviewTable { get; } = new();

    public static LevelLinkTable LevelLinkTable { get; } = new();

    public static LimitedLoginBonusTable LimitedLoginBonusTable { get; } = new();

    public static LimitedLoginBonusRewardListTable LimitedLoginBonusRewardListTable { get; } = new();

    public static LimitedMissionTable LimitedMissionTable { get; } = new();

    public static LimitedEventTable LimitedEventTable { get; } = new();

    public static LuckyChanceTable LuckyChanceTable { get; } = new();

    public static NewCharacterMissionTable NewCharacterMissionTable { get; } = new();

    public static LocalGvgCastleTable LocalGvgCastleTable { get; } = new();

    public static LocalRaidBonusScheduleTable LocalRaidBonusScheduleTable { get; } = new();

    public static LocalRaidEventScheduleTable LocalRaidEventScheduleTable { get; } = new();

    public static LocalRaidQuestGroupTable LocalRaidQuestGroupTable { get; } = new();

    public static MissionTable MissionTable { get; } = new();

    public static MissionOpenContentTable MissionOpenContentTable { get; } = new();

    public static MissionTabNameTable MissionTabNameTable { get; } = new();

    public static MissionClearCountRewardTable MissionClearCountRewardTable { get; } = new();

    public static MissionGuideTable MissionGuideTable { get; } = new();

    public static PanelTable PanelTable { get; } = new();

    public static PassiveSkillTable PassiveSkillTable { get; } = new();

    public static PanelMissionTable PanelMissionTable { get; } = new();

    public static PatternSettingTable PatternSettingTable { get; } = new();

    public static PlayerRankTable PlayerRankTable { get; } = new();

    public static PopularityVoteTable PopularityVoteTable { get; } = new();

    public static PvpRankingRewardTable PvpRankingRewardTable { get; } = new();

    public static QuestMapBuildingTable QuestMapBuildingTable { get; } = new();

    public static QuestTable QuestTable { get; } = new();

    public static RequiredCurrencyTable RequiredCurrencyTable { get; } = new();

    public static SpecialIconItemTable SpecialIconItemTable { get; } = new();

    public static StateBonusTable StateBonusTable { get; } = new();

    public static StateTable StateTable { get; } = new();

    public static TimeServerTable TimeServerTable { get; } = new();

    public static TowerBattleEnemyTable TowerBattleEnemyTable { get; } = new();

    public static TowerBattleQuestTable TowerBattleQuestTable { get; } = new();

    public static TradeShopSphereTable TradeShopSphereTable { get; } = new();

    public static TradeShopTabTable TradeShopTabTable { get; } = new();

    public static TotalActivityMedalRewardTable TotalActivityMedalRewardTable { get; } = new();

    public static TreasureChestCeilingTable TreasureChestCeilingTable { get; } = new();

    public static TreasureChestItemTable TreasureChestItemTable { get; } = new();

    public static TreasureChestTable TreasureChestTable { get; } = new();

    public static MusicTable MusicTable { get; } = new();

    public static ChatEmoticonTable ChatEmoticonTable { get; } = new();

    public static VipTable VipTable { get; } = new();

    public static WorldGroupTable WorldGroupTable { get; } = new();

    public static OpenContentTable OpenContentTable { get; } = new();

    public static LegendLeagueClassTable LegendLeagueClassTable { get; } = new();

    public static EffectGroupTable EffectGroupTable { get; } = new();

    public static ITable[] GetAllTables()
    {
        return new ITable[]
        {
            AchieveRankingRewardTable,
            CharacterProfileTable,
            CharacterDetailVoiceTable,
            CommunityTable,
            TextResourceTable,
            TextLanguageTable,
            TipTable,
            EquipmentExclusiveSkillDescriptionTable,
            BattleSkillCharacterSettingTable,
            BattleSkillNameSettingTable,
            HelpTable,
            TutorialDescriptionTable,
            TutorialTextBoxTable,
            LocalPushNotificationTable,
            InquiryButtonTable,
            FaqListTable,
            TermsTable,
            LocalRaidBannerTable,
            DeepLinkTable,
            GachaDestinyAddCharacterTable,
            ActiveSkillTable,
            AppVersionTable,
            AutoBattleEnemyTable,
            BattleScheduleTable,
            BoardRankTable,
            BookSortBonusFloorRewardTable,
            BookSortEventTable,
            BossBattleEnemyTable,
            BountyQuestEventTable,
            ChangeItemTable,
            CharacterBoxSizeTable,
            CharacterLevelTable,
            CharacterResetTable,
            CharacterRankUpMaterialTable,
            CharacterLiveModeTable,
            CharacterPotentialTable,
            CharacterPotentialCoefficientTable,
            CharacterStoryTable,
            CharacterTable,
            CharacterCollectionTable,
            CharacterCollectionLevelTable,
            CharacterCollectionRewardTable,
            ChapterTable,
            CollabMissionTable,
            DungeonBattleGridTable,
            DungeonBattleGuestTable,
            DungeonBattleRelicTable,
            ElementBonusTable,
            EquipmentCompositeTable,
            EquipmentEvolutionTable,
            EquipmentExclusiveEffectTable,
            EquipmentForgeTable,
            EquipmentLegendSacredTreasureTable,
            EquipmentMatchlessSacredTreasureTable,
            EquipmentReinforcementMaterialTable,
            EquipmentReinforcementParameterTable,
            EquipmentSetMaterialBoxTable,
            EquipmentSetMaterialTable,
            EquipmentSetTable,
            EquipmentSynchroGroupTable,
            EquipmentTable,
            ErrorCodeTable,
            FriendCampaignTable,
            FriendMissionTable,
            GachaCaseTable,
            GachaCaseUiTable,
            GachaSelectListTable,
            GlobalGvgCastleTable,
            GuildLevelTable,
            GuildRaidBossTable,
            GuildRaidRewardTable,
            GuildTowerEventTable,
            GuildTowerEnemyTable,
            GuildTowerChallengeRewardTable,
            GuildTowerReinforcementJobLevelTable,
            ItemMaxCountSwitchingQuestTable,
            GuildTowerFloorTable,
            ItemTable,
            MonthlyLoginBonusRewardListTable,
            MonthlyLoginBonusTable,
            MonologueTable,
            SphereTable,
            LeadReviewTable,
            LevelLinkTable,
            LimitedLoginBonusTable,
            LimitedLoginBonusRewardListTable,
            LimitedMissionTable,
            LimitedEventTable,
            LuckyChanceTable,
            NewCharacterMissionTable,
            LocalGvgCastleTable,
            LocalRaidBonusScheduleTable,
            LocalRaidEventScheduleTable,
            LocalRaidQuestGroupTable,
            MissionTable,
            MissionOpenContentTable,
            MissionTabNameTable,
            MissionClearCountRewardTable,
            MissionGuideTable,
            PassiveSkillTable,
            PlayerRankTable,
            PvpRankingRewardTable,
            QuestMapBuildingTable,
            QuestTable,
            RequiredCurrencyTable,
            StateBonusTable,
            StateTable,
            TimeServerTable,
            TowerBattleEnemyTable,
            TowerBattleQuestTable,
            TradeShopSphereTable,
            TradeShopTabTable,
            GuildMissionTable,
            TotalActivityMedalRewardTable,
            TreasureChestCeilingTable,
            TreasureChestItemTable,
            TreasureChestTable,
            VipTable,
            WorldGroupTable,
            OpenContentTable,
            LegendLeagueClassTable,
            EffectGroupTable,
            ChatEffectKeywordTable,
            SkillDescriptionLinkTextTable,
            PanelTable,
            PanelMissionTable,
            PatternSettingTable,
            SpecialIconItemTable,
            MusicTable,
            ChatEmoticonTable,
            DownloadRawDataTable,
            PopularityVoteTable
        };
    }

    // public static List<ITable> LoadAllClientMasters(LanguageType languageType)
    // {
    //     var tables = new List<ITable>();
    //     
    //     TextResourceTable.SetLanguageType(languageType);
    //
    //     var allTables = GetAllTables();
    //     for (var i = 0; i < allTables.Length; i++)
    //     {
    //      allTables[i].Load();
    //         tables.Add(allTables[i]);
    //     }
    //
    //     var clientTables = GetClientTables();
    //     for (var i = 0; i < clientTables.Length; i++)
    //     {
    //      tables.Add(clientTables[i]);
    //     }
    // }
    public static bool LoadAllMasters()
    {
        foreach (var allTable in GetAllTables())
        {
            try
            {
                if (!allTable.Load()) return false;
            }
            catch (FileNotFoundException e)
            {
                // ignored
            }
        }

        return true;
    }

    private static ITable[] GetClientTables()
    {
        var array = new ITable[19];
        array[0] = TextResourceTable;
        array[1] = TextLanguageTable;
        array[2] = CharacterProfileTable;
        array[3] = CharacterDetailVoiceTable;
        array[4] = CommunityTable;
        array[5] = TipTable;
        array[6] = EquipmentExclusiveSkillDescriptionTable;
        array[7] = BattleSkillCharacterSettingTable;
        array[8] = BattleSkillNameSettingTable;
        array[9] = HelpTable;
        array[10] = TutorialDescriptionTable;
        array[11] = TutorialTextBoxTable;
        array[12] = LocalPushNotificationTable;
        array[13] = InquiryButtonTable;
        array[14] = FaqListTable;
        array[15] = TermsTable;
        array[16] = LocalRaidBannerTable;
        array[17] = DeepLinkTable;
        array[18] = GachaDestinyAddCharacterTable;
        return array;
    }

    public static bool LoadAllClientMast1ers(LanguageType languageType)
    {
        var textResourceTable = TextResourceTable;
        var flag = CharacterProfileTable.Load();
        var flag2 = CharacterDetailVoiceTable.Load();
        var flag3 = CommunityTable.Load();
        var flag4 = EffectGroupTable.Load();
        var flag5 = TipTable.Load();
        var flag6 = EquipmentExclusiveSkillDescriptionTable.Load();
        var flag7 = BattleSkillCharacterSettingTable.Load();
        var flag8 = BattleSkillNameSettingTable.Load();
        var flag9 = HelpTable.Load();
        var flag10 = TutorialDescriptionTable.Load();
        var flag11 = TutorialTextBoxTable.Load();
        var flag12 = LocalPushNotificationTable.Load();
        var flag13 = InquiryButtonTable.Load();
        var flag14 = FaqListTable.Load();
        var flag15 = TermsTable.Load();
        var flag16 = LocalRaidBannerTable.Load();
        var flag17 = DeepLinkTable.Load();
        return GachaDestinyAddCharacterTable.Load();
    }
}