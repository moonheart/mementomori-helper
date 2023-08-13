using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Table;

namespace MementoMori.Ortega.Share
{
    public static class Masters
    {
        public static bool LoadAllMasters()
        {
            return ActiveSkillTable.Load() &&
                   AppVersionTable.Load() &&
                   AutoBattleEnemyTable.Load() &&
                   BattleScheduleTable.Load() &&
                   BoardRankTable.Load() &&
                   BossBattleEnemyTable.Load() &&
                   BountyQuestEventTable.Load() &&
                   ChapterTable.Load() &&
                   CharacterBoxSizeTable.Load() &&
                   CharacterStoryTable.Load() &&
                   CharacterTable.Load() &&
                   CharacterCollectionTable.Load() &&
                   CharacterCollectionLevelTable.Load() &&
                   CharacterCollectionRewardTable.Load() &&
                   CharacterLevelTable.Load() &&
                   CharacterResetTable.Load() &&
                   CharacterPotentialTable.Load() &&
                   CharacterPotentialCoefficientTable.Load() &&
                   CharacterRankUpMaterialTable.Load() &&
                   CharacterLiveModeTable.Load() &&
                   ChangeItemTable.Load() &&
                   DungeonBattleGridTable.Load() &&
                   DungeonBattleGuestTable.Load() &&
                   DungeonBattleRelicTable.Load() &&
                   ElementBonusTable.Load() &&
                   EquipmentCompositeTable.Load() &&
                   EquipmentEvolutionTable.Load() &&
                   EquipmentExclusiveEffectTable.Load() &&
                   EquipmentForgeTable.Load() &&
                   EquipmentLegendSacredTreasureTable.Load() &&
                   EquipmentMatchlessSacredTreasureTable.Load() &&
                   EquipmentReinforcementMaterialTable.Load() &&
                   EquipmentReinforcementParameterTable.Load() &&
                   EquipmentSetMaterialTable.Load() &&
                   EquipmentSetTable.Load() &&
                   EquipmentTable.Load() &&
                   ErrorCodeTable.Load() &&
                   FriendCampaignTable.Load() &&
                   FriendMissionTable.Load() &&
                   GachaCaseTable.Load() &&
                   MissionTable.Load();
        }

        public static bool LoadAllClientMasters(LanguageType languageType)
        {
            TextResourceTable textResourceTable = Masters.TextResourceTable;
            bool flag = Masters.CharacterProfileTable.Load();
            bool flag2 = Masters.CharacterDetailVoiceTable.Load();
            bool flag3 = Masters.CommunityTable.Load();
            bool flag4 = Masters.EffectGroupTable.Load();
            bool flag5 = Masters.TipTable.Load();
            bool flag6 = Masters.EquipmentExclusiveSkillDescriptionTable.Load();
            bool flag7 = Masters.BattleSkillCharacterSettingTable.Load();
            bool flag8 = Masters.BattleSkillNameSettingTable.Load();
            bool flag9 = Masters.HelpTable.Load();
            bool flag10 = Masters.TutorialDescriptionTable.Load();
            bool flag11 = Masters.TutorialTextBoxTable.Load();
            bool flag12 = Masters.LocalPushNotificationTable.Load();
            bool flag13 = Masters.InquiryButtonTable.Load();
            bool flag14 = Masters.FaqListTable.Load();
            bool flag15 = Masters.TermsTable.Load();
            bool flag16 = Masters.LocalRaidBannerTable.Load();
            bool flag17 = Masters.DeepLinkTable.Load();
            return Masters.GachaDestinyAddCharacterTable.Load();
        }

        public static CharacterProfileTable CharacterProfileTable { get; }

        public static CharacterDetailVoiceTable CharacterDetailVoiceTable { get; }

        public static CommunityTable CommunityTable { get; }

        public static TextResourceTable TextResourceTable { get; }

        public static TextLanguageTable TextLanguageTable { get; }

        public static TipTable TipTable { get; }

        public static EquipmentExclusiveSkillDescriptionTable EquipmentExclusiveSkillDescriptionTable { get; }

        public static BattleSkillCharacterSettingTable BattleSkillCharacterSettingTable { get; }

        public static BattleSkillNameSettingTable BattleSkillNameSettingTable { get; }

        public static HelpTable HelpTable { get; }

        public static TutorialDescriptionTable TutorialDescriptionTable { get; }

        public static TutorialTextBoxTable TutorialTextBoxTable { get; }

        public static LocalPushNotificationTable LocalPushNotificationTable { get; }

        public static InquiryButtonTable InquiryButtonTable { get; }

        public static FaqListTable FaqListTable { get; }

        public static TermsTable TermsTable { get; }

        public static LocalRaidBannerTable LocalRaidBannerTable { get; }

        public static DeepLinkTable DeepLinkTable { get; }

        public static GachaDestinyAddCharacterTable GachaDestinyAddCharacterTable { get; }

        public static ActiveSkillTable ActiveSkillTable { get; }

        public static AppVersionTable AppVersionTable { get; }

        public static AutoBattleEnemyTable AutoBattleEnemyTable { get; }

        public static BattleScheduleTable BattleScheduleTable { get; }

        public static BoardRankTable BoardRankTable { get; }

        public static BossBattleEnemyTable BossBattleEnemyTable { get; }

        public static BountyQuestEventTable BountyQuestEventTable { get; }

        public static ChangeItemTable ChangeItemTable { get; }

        public static CharacterBoxSizeTable CharacterBoxSizeTable { get; }

        public static CharacterLevelTable CharacterLevelTable { get; }

        public static CharacterResetTable CharacterResetTable { get; }

        public static CharacterRankUpMaterialTable CharacterRankUpMaterialTable { get; }

        public static CharacterLiveModeTable CharacterLiveModeTable { get; }

        public static CharacterPotentialTable CharacterPotentialTable { get; }

        public static CharacterPotentialCoefficientTable CharacterPotentialCoefficientTable { get; }

        public static CharacterStoryTable CharacterStoryTable { get; }

        public static CharacterTable CharacterTable { get; }

        public static CharacterCollectionTable CharacterCollectionTable { get; }

        public static CharacterCollectionLevelTable CharacterCollectionLevelTable { get; }

        public static CharacterCollectionRewardTable CharacterCollectionRewardTable { get; }

        public static ChapterTable ChapterTable { get; }

        public static DungeonBattleGridTable DungeonBattleGridTable { get; }

        public static DungeonBattleGuestTable DungeonBattleGuestTable { get; }

        public static DungeonBattleRelicTable DungeonBattleRelicTable { get; }

        public static ElementBonusTable ElementBonusTable { get; }

        public static EquipmentCompositeTable EquipmentCompositeTable { get; }

        public static EquipmentEvolutionTable EquipmentEvolutionTable { get; }

        public static EquipmentExclusiveEffectTable EquipmentExclusiveEffectTable { get; }

        public static EquipmentForgeTable EquipmentForgeTable { get; }

        public static EquipmentLegendSacredTreasureTable EquipmentLegendSacredTreasureTable { get; }

        public static EquipmentMatchlessSacredTreasureTable EquipmentMatchlessSacredTreasureTable { get; }

        public static EquipmentReinforcementMaterialTable EquipmentReinforcementMaterialTable { get; }

        public static EquipmentReinforcementParameterTable EquipmentReinforcementParameterTable { get; }

        public static EquipmentSetMaterialTable EquipmentSetMaterialTable { get; }

        public static EquipmentSetTable EquipmentSetTable { get; }

        public static EquipmentTable EquipmentTable { get; }

        public static ErrorCodeTable ErrorCodeTable { get; }

        public static FriendCampaignTable FriendCampaignTable { get; }

        public static FriendMissionTable FriendMissionTable { get; }

        public static GachaCaseTable GachaCaseTable { get; }

        public static GachaCaseUiTable GachaCaseUiTable { get; }

        public static GachaSelectListTable GachaSelectListTable { get; }

        public static GlobalGvgCastleTable GlobalGvgCastleTable { get; }

        public static GuildLevelTable GuildLevelTable { get; }

        public static GuildRaidBossTable GuildRaidBossTable { get; }

        public static GuildRaidRewardTable GuildRaidRewardTable { get; }

        public static GvGServerLvTable GvGServerLvTable { get; }

        public static ItemTable ItemTable { get; }

        public static MonthlyLoginBonusRewardListTable MonthlyLoginBonusRewardListTable { get; }

        public static MonthlyLoginBonusTable MonthlyLoginBonusTable { get; }

        public static SphereTable SphereTable { get; }

        public static LeadReviewTable LeadReviewTable { get; }

        public static LevelLinkTable LevelLinkTable { get; }

        public static LimitedLoginBonusTable LimitedLoginBonusTable { get; }

        public static LimitedLoginBonusRewardListTable LimitedLoginBonusRewardListTable { get; }

        public static LimitedMissionTable LimitedMissionTable { get; }

        public static LimitedEventTable LimitedEventTable { get; }

        public static NewCharacterMissionTable NewCharacterMissionTable { get; }

        public static LocalGvgCastleTable LocalGvgCastleTable { get; }

        public static LocalRaidEnemyTable LocalRaidEnemyTable { get; }

        public static LocalRaidEventQuestGroupTable LocalRaidEventQuestGroupTable { get; }

        public static LocalRaidEventScheduleTable LocalRaidEventScheduleTable { get; }

        public static LocalRaidQuestTable LocalRaidQuestTable { get; }

        public static LocalRaidQuestGroupTable LocalRaidQuestGroupTable { get; }

        public static MissionTable MissionTable { get; }

        public static MissionOpenContentTable MissionOpenContentTable { get; }

        public static MissionTabNameTable MissionTabNameTable { get; }

        public static MissionClearCountRewardTable MissionClearCountRewardTable { get; }

        public static MissionGuideTable MissionGuideTable { get; }

        public static PassiveSkillTable PassiveSkillTable { get; }

        public static PlayerRankTable PlayerRankTable { get; }

        public static PvpRankingRewardTable PvpRankingRewardTable { get; }

        public static QuestMapBuildingTable QuestMapBuildingTable { get; }

        public static QuestTable QuestTable { get; }

        public static RequiredCurrencyTable RequiredCurrencyTable { get; }

        public static StateBonusTable StateBonusTable { get; }

        public static StateTable StateTable { get; }

        public static TimeServerTable TimeServerTable { get; }

        public static TowerBattleEnemyTable TowerBattleEnemyTable { get; }

        public static TowerBattleQuestTable TowerBattleQuestTable { get; }

        public static TradeShopSphereTable TradeShopSphereTable { get; }

        public static TradeShopTabTable TradeShopTabTable { get; }

        public static TotalActivityMedalRewardTable TotalActivityMedalRewardTable { get; }

        public static TreasureChestCeilingTable TreasureChestCeilingTable { get; }

        public static TreasureChestItemTable TreasureChestItemTable { get; }

        public static TreasureChestTable TreasureChestTable { get; }

        public static VipTable VipTable { get; }

        public static WorldGroupTable WorldGroupTable { get; }

        public static OpenContentTable OpenContentTable { get; }

        public static LegendLeagueClassTable LegendLeagueClassTable { get; }

        // Note: this type is marked as 'beforefieldinit'.
        static Masters()
        {
            SphereTable = new SphereTable();
            EquipmentTable = new EquipmentTable();
            ItemTable = new ItemTable();
            CharacterProfileTable = new CharacterProfileTable();
            CharacterDetailVoiceTable = new CharacterDetailVoiceTable();
            CommunityTable = new CommunityTable();
            TextResourceTable = new TextResourceTable();
            CharacterDetailVoiceTable = new CharacterDetailVoiceTable();
            TipTable = new TipTable();
            EquipmentExclusiveSkillDescriptionTable = new EquipmentExclusiveSkillDescriptionTable();
            BattleSkillCharacterSettingTable = new BattleSkillCharacterSettingTable();
            BattleSkillNameSettingTable = new BattleSkillNameSettingTable();
            EffectGroupTable = new EffectGroupTable();
            HelpTable = new HelpTable();
            TutorialDescriptionTable = new TutorialDescriptionTable();
            TutorialTextBoxTable = new TutorialTextBoxTable();
            LocalPushNotificationTable = new LocalPushNotificationTable();
            InquiryButtonTable = new InquiryButtonTable();
            FaqListTable = new FaqListTable();
            TermsTable = new TermsTable();
            LocalRaidBannerTable = new LocalRaidBannerTable();
            DeepLinkTable = new DeepLinkTable();
            GachaDestinyAddCharacterTable = new GachaDestinyAddCharacterTable();
            ActiveSkillTable = new ActiveSkillTable();
            AppVersionTable = new AppVersionTable();
            AutoBattleEnemyTable = new AutoBattleEnemyTable();
            BattleScheduleTable = new BattleScheduleTable();
            BoardRankTable = new BoardRankTable();
            BossBattleEnemyTable = new BossBattleEnemyTable();
            BountyQuestEventTable = new BountyQuestEventTable();
            ChangeItemTable = new ChangeItemTable();
            CharacterBoxSizeTable = new CharacterBoxSizeTable();
            CharacterLevelTable = new CharacterLevelTable();
            CharacterResetTable = new CharacterResetTable();
            CharacterRankUpMaterialTable = new CharacterRankUpMaterialTable();
            CharacterLiveModeTable = new CharacterLiveModeTable();
            CharacterPotentialTable = new CharacterPotentialTable();
            CharacterPotentialCoefficientTable = new CharacterPotentialCoefficientTable();
            CharacterStoryTable = new CharacterStoryTable();
            CharacterTable = new CharacterTable();
            CharacterCollectionTable = new CharacterCollectionTable();
            CharacterCollectionLevelTable = new CharacterCollectionLevelTable();
            CharacterCollectionRewardTable = new CharacterCollectionRewardTable();
            ChapterTable = new ChapterTable();
            DungeonBattleGridTable = new DungeonBattleGridTable();
            DungeonBattleGuestTable = new DungeonBattleGuestTable();
            DungeonBattleRelicTable = new DungeonBattleRelicTable();
            ElementBonusTable = new ElementBonusTable();
            EquipmentCompositeTable = new EquipmentCompositeTable();
            EquipmentSetMaterialTable = new EquipmentSetMaterialTable();
            TreasureChestTable = new TreasureChestTable();
            GachaCaseTable = new GachaCaseTable();
            LevelLinkTable = new LevelLinkTable();
            MissionTable = new MissionTable();
        }

        public static EffectGroupTable EffectGroupTable;
    }
}