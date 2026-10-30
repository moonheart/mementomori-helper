using MementoMori.Ortega.Share.Data.Equipment;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Data.Item.Model;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Enums.Battle.Skill;

namespace MementoMori.Ortega.Share
{
    public static class OrtegaConst
    {
        public static class Item
        {
            public static long EnergySphereId { get; }

            public static long HolySteelId { get; }

            public static long IntelligenceSphereId { get; }

            public static long MaxSphereLevel { get; }

            public static long MuscleSphereId { get; }

            public static long SilverCoinId { get; }

            public static List<ItemType> BulkUseEnabledItemTypes { get; }

            public static List<TreasureChestLotteryType> BulkUseEnabledTreasureChestLotteryTypes { get; }

            public static long SphereSynthesisWaningLevel { get; }

            static Item()
            {
                MatchlessSacredTreasureExpItem1Count = 1;
                MatchlessSacredTreasureExpItem2Count = 3;

                EnergySphereId = 2;
                HolySteelId = 3;
                IntelligenceSphereId = 3;
                MaxSphereLevel = 15;
                MuscleSphereId = 1;
                SilverCoinId = 16;
                SphereSynthesisWaningLevel = 10;

                BulkUseEnabledItemTypes = new List<ItemType>
                {
                    (ItemType)10,
                };

                BulkUseEnabledTreasureChestLotteryTypes = new List<TreasureChestLotteryType>
                {
                    (TreasureChestLotteryType)0,
                    (TreasureChestLotteryType)5,
                    (TreasureChestLotteryType)6,
                };
            }

            public static int MatchlessSacredTreasureExpItem1Count;

            public static int MatchlessSacredTreasureExpItem2Count;
        }

        public static class Gacha
        {
            public static IUserItem ItemRequiredToChangeGachaRelic { get; }

            public static IUserItem ItemRequiredToChangeGachaRelicFree { get; }

            public static IUserItem ItemRequiredToOpenElement { get; }

            public static int MaxCountSelectListDefault { get; }

            public static int MaxCountSelectListElementDefault { get; }

            public static int ResetFreeRelicChangeByDrawCount { get; }

            public static List<long> SelectListDefault { get; }

            public static long FirstPlatinumGachaCaseId { get; }

            public static long FirstPlatinumGacha10ButtonId { get; }

            public static long PlatinumGachaCaseId { get; }

            // Note: this type is marked as 'beforefieldinit'.
            static Gacha()
            {
                ItemRequiredToChangeGachaRelic = new UserCurrencyFree(300);
                ItemRequiredToChangeGachaRelicFree = new UserCurrencyFree(0);
                ItemRequiredToOpenElement = new UserCurrencyFree(300);
                MaxCountSelectListDefault = 20;
                MaxCountSelectListElementDefault = 5;
                ResetFreeRelicChangeByDrawCount = 50;
                SelectListDefault = new List<long>
                {
                    5,
                    6,
                    7,
                    8,
                    10,
                    15,
                    16,
                    17,
                    18,
                    20,
                    25,
                    26,
                    27,
                    28,
                    29,
                    35,
                    36,
                    37,
                    38,
                    39,
                };
                FirstPlatinumGachaCaseId = 20;
                FirstPlatinumGacha10ButtonId = 48;
                PlatinumGachaCaseId = 1;
            }
        }

        public static class Guild
        {
            public static int GuildFameDismissalSubDay { get; }

            public static int GuildJoinLimit { get; }

            public static int GuildLeaderChangeExceptionDay { get; }

            public static int GuildLeaderChangeFameSortSubDay { get; }

            public static int GuildLoginBonusExp { get; }

            public static int GuildMemberInfoTotalFameSubDay { get; }

            public static int GuildRaidStartOnceDayExp { get; }

            public static IUserItem ItemRequiredToCreateGuild { get; }

            public static long MaxApplyingNum { get; }

            public static int MaxCommanderCount { get; }

            public static long MaxDisplayGuildRanking { get; }

            public static long MaxFame { get; }

            public static int MaxGuildMember { get; }

            public static int MaxSubLeaderCount { get; }

            public static long MaxUserApplyingNum { get; }

            public static int RecruitGuildMemberMaxCount { get; }

            public static int RecruitGuildMemberOnPlayerSideMaxCount { get; }

            public static int RecruitMessageMaxLength { get; }

            public static long RequiredExpToRemoveMember { get; }

            public static long RequiredRankToJoinGuild { get; }

            // Note: this type is marked as 'beforefieldinit'.
            static Guild()
            {
                GuildFameDismissalSubDay = -3;
                GuildJoinLimit = 2;
                GuildLeaderChangeExceptionDay = 3;
                GuildLeaderChangeFameSortSubDay = -8;
                GuildLoginBonusExp = 10;
                GuildMemberInfoTotalFameSubDay = -7;
                GuildRaidStartOnceDayExp = 50;
                ItemRequiredToCreateGuild = new UserCurrencyFree(500);

                MaxApplyingNum = 100;
                MaxSubLeaderCount = 1;
                MaxCommanderCount = 3;
                MaxDisplayGuildRanking = 20;
                MaxFame = 70000;
                MaxGuildMember = 50;
                MaxUserApplyingNum = 10000;
                RequiredExpToRemoveMember = 30;
                RequiredRankToJoinGuild = 12;
                RecruitGuildMemberMaxCount = 20;
                RecruitGuildMemberOnPlayerSideMaxCount = 20;
                RecruitMessageMaxLength = 50;
            }
        }

        public static class HttpHeaderRequest
        {
            public static string HeaderAccessTokenKey { get; }

            public static string HeaderAppVersionKey { get; }

            public static string HeaderDeviceType { get; }

            public static string HeaderDmmOneTimeToken { get; }

            public static string HeaderDmmViewerId
            {
                get
                {
                    return "OrtegaDmmViewerId";
                }
            }

            public static string HeaderFlagDmmLoginOneTimeToken { get; }

            public static string HeaderFlagDmmUpdateOneTimeToken { get; }

            public static string HeaderKeyName { get; }

            public static string HeaderKeyValue { get; }

            public static string HeaderUUID { get; }

            public static string HeaderValidDeveloperKey { get; }

            public static string HeaderIpAddress { get; }

            // Note: this type is marked as 'beforefieldinit'.
            static HttpHeaderRequest()
            {
                HeaderAccessTokenKey = "OrtegaAccessToken";
                HeaderAppVersionKey = "OrtegaAppVersion";
                HeaderDeviceType = "OrtegaDeviceType";
                HeaderDmmOneTimeToken = "OrtegaDmmOneTimeToken";
                HeaderFlagDmmUpdateOneTimeToken = "OrtegaFlagDmmUpdateOneTimeToken";
                HeaderFlagDmmLoginOneTimeToken = "OrtegaFlagDmmLoginOneTimeToken";
                HeaderUUID = "OrtegaUUID";
                HeaderIpAddress = "X-Forwarded-For";
                HeaderKeyName = "Key";
                HeaderKeyValue = "xcOx7Uv1EMfFzigh";
                HeaderValidDeveloperKey = "ValidDeveloperKey";
            }
        }

        public static class HttpHeaderResponse
        {
            public static string HeaderAssetVersion { get; }

            public static string HeaderDmmOneTimeToken
            {
                get
                {
                    return "OrtegaDmmOneTimeToken";
                }
            }

            public static string HeaderMasterVersion { get; }

            public static string HeaderNextAccessTokenKey { get; }

            public static string HeaderStatusCodeKey { get; }

            public static string HeaderUtcNowTimeStamp { get; }

            // Note: this type is marked as 'beforefieldinit'.
            static HttpHeaderResponse()
            {
                HeaderAssetVersion = "OrtegaAssetVersion";
                HeaderMasterVersion = "OrtegaMasterVersion";
                HeaderNextAccessTokenKey = "OrtegaNextAccessToken";
                HeaderStatusCodeKey = "OrtegaStatusCode";
                HeaderUtcNowTimeStamp = "OrtegaUtcNowTimeStamp";
            }
        }

        public static class User
        {
            public static long DefaultFavoriteCharacterId { get; }

            public static int ChatIntervalChangedPlayerRankBorder { get; }

            public static Dictionary<TransferSpotType, OpenCommandType> CheckOpenContentDict { get; }

            public static double GiveGoldExtraExpAtMaxRankNum { get; }

            public static int MaxDisplayBannerNum { get; }

            public static int MaxDisplayIconNum { get; }

            public static int MaxRegisterFavoriteCharacterNum { get; }

            public static long PlayerIdExceptWorldId { get; }

            public static long DeletedPlayerMainIconId { get; }

            public static string DeletedPlayerNameKey { get; }

            public static long InactiveIconEffectId { get; }

            public static long RequiredCurrencyChangeUserName { get; }

            public static int RequiredSpecialIconCountForBuyIconEffect { get; }

            public static long SpecialIconItemIdMask { get; }

            // Note: this type is marked as 'beforefieldinit'.
            static User()
            {
                DefaultCharacterIds = new List<long>
                {
                    1,
                    2,
                };

                DefaultFavoriteCharacterId = 2;
                ChatIntervalChangedPlayerRankBorder = 10;
                CheckOpenContentDict = new Dictionary<TransferSpotType, OpenCommandType>
                {
                    [(TransferSpotType)20] = (OpenCommandType)10,
                    [(TransferSpotType)30] = (OpenCommandType)4,
                    [(TransferSpotType)40] = (OpenCommandType)44,
                    [(TransferSpotType)50] = (OpenCommandType)43,
                    [(TransferSpotType)60] = (OpenCommandType)12,
                    [(TransferSpotType)80] = (OpenCommandType)18,
                    [(TransferSpotType)81] = (OpenCommandType)18,
                    [(TransferSpotType)90] = (OpenCommandType)9,
                    [(TransferSpotType)91] = (OpenCommandType)9,
                    [(TransferSpotType)130] = (OpenCommandType)3,
                    [(TransferSpotType)131] = (OpenCommandType)3,
                    [(TransferSpotType)120] = (OpenCommandType)120,
                    [(TransferSpotType)121] = (OpenCommandType)121,
                    [(TransferSpotType)132] = (OpenCommandType)3,
                    [(TransferSpotType)133] = (OpenCommandType)3,
                    [(TransferSpotType)160] = (OpenCommandType)41,
                    [(TransferSpotType)170] = (OpenCommandType)45,
                    [(TransferSpotType)180] = (OpenCommandType)46,
                    [(TransferSpotType)181] = (OpenCommandType)46,
                    [(TransferSpotType)182] = (OpenCommandType)46,
                    [(TransferSpotType)135] = (OpenCommandType)3,
                    [(TransferSpotType)134] = (OpenCommandType)200,
                    [(TransferSpotType)270] = (OpenCommandType)360,
                    [(TransferSpotType)136] = (OpenCommandType)3,
                    [(TransferSpotType)260] = (OpenCommandType)320,
                    [(TransferSpotType)280] = (OpenCommandType)460,
                    [(TransferSpotType)281] = (OpenCommandType)460,
                    [(TransferSpotType)290] = (OpenCommandType)500,
                    [(TransferSpotType)330] = (OpenCommandType)3,
                    [(TransferSpotType)340] = (OpenCommandType)560,
                    [(TransferSpotType)100] = (OpenCommandType)2,
                };

                GiveGoldExtraExpAtMaxRankNum = 0.01;
                MaxDisplayBannerNum = 16;
                MaxDisplayIconNum = 8;
                MaxRegisterFavoriteCharacterNum = 5;
                PlayerIdExceptWorldId = 1000;
                RequiredCurrencyChangeUserName = 500;
                SpecialIconItemIdMask = unchecked((long)0x8000000000000000UL);
                DeletedPlayerNameKey = "[CharacterNameDeletedCharacter]";
                DeletedPlayerMainIconId = 2;
                InactiveIconEffectId = 0;
                RequiredSpecialIconCountForBuyIconEffect = 20;
            }

            public static readonly List<long> DefaultCharacterIds;
        }

        public static class Battle
        {
            public static long AttackActionTime { get; }

            public static long BattleEndTime { get; }

            public static long BattleStartTime { get; }

            public static long BossSubSetCharacterStartTime { get; }

            public static long BossSubSetStartTime { get; }

            public static int CriticalMaxBonus { get; }

            public static int DebuffHitMaxBase { get; }

            public static int DebuffHitMinBase { get; }

            public static long EffectRateMaxValue { get; }

            public static long EffectRateMinValue { get; }

            public static int MaxLeaderCharacterSkillNum { get; }

            public static int MaxRearguardSubUnit { get; }

            public static Dictionary<BattleType, int> MaxTurn { get; } = new();

            public static int MaxVanguardCharacterCount
            {
                get
                {
                    int num = OrtegaConst.Battle.MaxVanguardSubUnit;
                    return num + 1;
                }
            }

            public static int MaxVanguardSubUnit { get; }

            public static long NpcPlayerId { get; }

            public static BattleParameterChangeInfo OneDarkElementBonus { get; }

            public static string ResultAnimationKeyLoseAnnihilationStart { get; }

            public static string ResultAnimationKeyLoseOutOfTurnsStart { get; }

            public static string ResultAnimationKeyWin { get; }

            public static int Skill1Index { get; }

            public static int Skill2Index { get; }

            public static long MultiSkillActionTime1PerUnit { get; }

            public static long MultiSkillActionTime2PerUnit { get; }

            public static long MultiSkillActionTime3PerUnit { get; }

            public static long ResonanceDelayTime { get; }

            public static long SkillActionTime1PerUnit { get; }

            public static long SkillActionTime2PerUnit { get; }

            public static long SkillActionTime3PerUnit { get; }

            public static long SubSetWaitTime { get; }

            public static BattleParameterChangeInfo ThreeDarkElementBonus { get; }

            public static BattleParameterChangeInfo ThreeDefaultElementAndAnotherTwoElementBonus1 { get; }

            public static BattleParameterChangeInfo ThreeDefaultElementAndAnotherTwoElementBonus2 { get; }

            public static BattleParameterChangeInfo ThreeDefaultElementBonus1 { get; }

            public static BattleParameterChangeInfo ThreeDefaultElementBonus2 { get; }

            public static long TransientEffectTime { get; }

            public static long TurnEndTime { get; }

            public static long TurnStartTime { get; }

            public static BattleParameterChangeInfo TwoDarkElementBonus { get; }

            // Note: this type is marked as 'beforefieldinit'.
            static Battle()
            {
                MaxSkillTurn = 200;
                AttackActionTime = 200;
                BattleEndTime = 1500;
                BattleStartTime = 500;
                BossSubSetCharacterStartTime = 1200;
                BossSubSetStartTime = 200;
                CriticalMaxBonus = 30;
                DebuffHitMaxBase = 130;
                DebuffHitMinBase = 70;
                EffectRateMaxValue = 200;
                EffectRateMinValue = -100;
                MaxLeaderCharacterSkillNum = 4;
                MaxRearguardSubUnit = 5;
                MaxTurn = new Dictionary<BattleType, int>
                {
                    [(BattleType)1] = 10,
                    [(BattleType)2] = 40,
                    [(BattleType)3] = 40,
                    [(BattleType)4] = 40,
                    [(BattleType)5] = 40,
                    [(BattleType)6] = 40,
                    [(BattleType)7] = 40,
                    [(BattleType)8] = 40,
                    [(BattleType)9] = 40,
                    [(BattleType)11] = 10,
                    [(BattleType)12] = 40,
                    [(BattleType)13] = 40,
                    [(BattleType)14] = 40,
                    [(BattleType)15] = 10,
                };
                MaxVanguardSubUnit = 4;
                NpcPlayerId = 128000000000;
                OneDarkElementBonus = new BattleParameterChangeInfo
                {
                    BattleParameterType = (BattleParameterType)13,
                    ChangeParameterType = (ChangeParameterType)2,
                    Value = 30,
                };
                ResultAnimationKeyLoseAnnihilationStart = "Lose-Annihilation-Start";
                ResultAnimationKeyLoseOutOfTurnsStart = "Lose-OutOfTurns-Start";
                ResultAnimationKeyWin = "Win-Start";
                Skill1Index = 1;
                Skill2Index = 2;
                SkillActionTime1PerUnit = 400;
                MultiSkillActionTime1PerUnit = 200;
                SkillActionTime2PerUnit = 180;
                MultiSkillActionTime2PerUnit = 90;
                SkillActionTime3PerUnit = 80;
                MultiSkillActionTime3PerUnit = 40;
                SubSetWaitTime = 400;
                ResonanceDelayTime = 200;
                ThreeDarkElementBonus = new BattleParameterChangeInfo
                {
                    BattleParameterType = (BattleParameterType)2,
                    ChangeParameterType = (ChangeParameterType)2,
                    Value = 10,
                };
                ThreeDefaultElementAndAnotherTwoElementBonus1 = new BattleParameterChangeInfo
                {
                    BattleParameterType = (BattleParameterType)2,
                    ChangeParameterType = (ChangeParameterType)2,
                    Value = 10,
                };
                ThreeDefaultElementAndAnotherTwoElementBonus2 = new BattleParameterChangeInfo
                {
                    BattleParameterType = (BattleParameterType)1,
                    ChangeParameterType = (ChangeParameterType)2,
                    Value = 10,
                };
                ThreeDefaultElementBonus1 = new BattleParameterChangeInfo
                {
                    BattleParameterType = (BattleParameterType)2,
                    ChangeParameterType = (ChangeParameterType)2,
                    Value = 10,
                };
                ThreeDefaultElementBonus2 = new BattleParameterChangeInfo
                {
                    BattleParameterType = (BattleParameterType)1,
                    ChangeParameterType = (ChangeParameterType)2,
                    Value = 10,
                };
                TransientEffectTime = 400;
                TurnEndTime = 1000;
                TurnStartTime = 400;
                TwoDarkElementBonus = new BattleParameterChangeInfo
                {
                    BattleParameterType = (BattleParameterType)7,
                    ChangeParameterType = (ChangeParameterType)2,
                    Value = 15,
                };
            }

            public static int MaxSkillTurn;
        }

        public static class Skill
        {
            public static List<SkillCategory> ConfuseAffectedSkillCategoryGroup { get; }

            public static List<EffectType> SelfInjuryGroup { get; }

            public static int TeamPassiveGuid { get; }

            // Note: this type is marked as 'beforefieldinit'.
            static Skill()
            {
                AdvantageGroupBuff = new List<EffectType>
                {
                    (EffectType)2001,
                    (EffectType)2002,
                    (EffectType)2003,
                    (EffectType)2004,
                    (EffectType)2005,
                    (EffectType)2006,
                    (EffectType)2007,
                    (EffectType)2009,
                    (EffectType)2100,
                    (EffectType)2101,
                    (EffectType)2011,
                    (EffectType)2012,
                    (EffectType)2008,
                };

                ConfuesActionGroupDebuff = new List<EffectType>
                {
                    (EffectType)6001,
                    (EffectType)6002,
                    (EffectType)6003,
                    (EffectType)6004,
                };

                DamageReflectEnhanceGroup = new List<EffectType>
                {
                    (EffectType)2111,
                    (EffectType)2112,
                    (EffectType)2113,
                    (EffectType)2114,
                    (EffectType)2115,
                    (EffectType)2116,
                    (EffectType)2117,
                    (EffectType)2118,
                    (EffectType)2121,
                    (EffectType)2122,
                    (EffectType)2123,
                    (EffectType)2124,
                    (EffectType)2125,
                    (EffectType)2126,
                    (EffectType)2127,
                    (EffectType)2128,
                    (EffectType)2131,
                    (EffectType)2132,
                    (EffectType)2133,
                    (EffectType)2134,
                    (EffectType)2135,
                    (EffectType)2136,
                    (EffectType)2137,
                    (EffectType)2138,
                };

                LockOnGroup = new List<EffectType>
                {
                    (EffectType)7111,
                    (EffectType)7121,
                    (EffectType)7131,
                    (EffectType)7132,
                    (EffectType)7133,
                    (EffectType)7134,
                    (EffectType)7135,
                    (EffectType)7136,
                    (EffectType)7141,
                    (EffectType)7142,
                    (EffectType)7143,
                    (EffectType)7151,
                    (EffectType)7152,
                    (EffectType)7153,
                };

                DamageResonanceGroup = new List<EffectType>
                {
                    (EffectType)8111,
                    (EffectType)8121,
                    (EffectType)8122,
                    (EffectType)8123,
                    (EffectType)8124,
                    (EffectType)8125,
                    (EffectType)8126,
                    (EffectType)8127,
                    (EffectType)8128,
                    (EffectType)8129,
                    (EffectType)8131,
                    (EffectType)8141,
                };

                TurnOverDamageGroup = new List<EffectType>
                {
                    (EffectType)8001,
                    (EffectType)8002,
                    (EffectType)8003,
                    (EffectType)8004,
                    (EffectType)8101,
                    (EffectType)8102,
                    (EffectType)8103,
                };

                SelfInjuryGroup = new List<EffectType>
                {
                    (EffectType)8101,
                    (EffectType)8102,
                    (EffectType)8103,
                };

                ConfuseAffectedSkillCategoryGroup = new List<SkillCategory>
                {
                    (SkillCategory)10,
                    (SkillCategory)11,
                    (SkillCategory)12,
                    (SkillCategory)13,
                    (SkillCategory)14,
                    (SkillCategory)15,
                    (SkillCategory)16,
                    (SkillCategory)17,
                };

                TeamPassiveGuid = 10000;
            }

            public static List<EffectType> AdvantageGroupBuff;

            public static List<EffectType> ConfuesActionGroupDebuff;

            public static List<EffectType> DamageReflectEnhanceGroup;

            public static List<EffectType> LockOnGroup;

            public static List<EffectType> DamageResonanceGroup;

            public static List<EffectType> TurnOverDamageGroup;
        }

        public static class TowerBattle
        {
            public static int FreeCountPerDay { get; }

            public static int MaxClearNewFloorAtElementTowerPerDay { get; }

            public static int TowerTypeNum { get; }

            // Note: this type is marked as 'beforefieldinit'.
            static TowerBattle()
            {
                OpenedDayOfWeekDictionary = new Dictionary<TowerType, List<DayOfWeek>>
                {
                    [(TowerType)1] = new List<DayOfWeek>
                    {
                        (DayOfWeek)0,
                        (DayOfWeek)1,
                        (DayOfWeek)2,
                        (DayOfWeek)3,
                        (DayOfWeek)4,
                        (DayOfWeek)5,
                        (DayOfWeek)6,
                    },
                    [(TowerType)2] = new List<DayOfWeek>
                    {
                        (DayOfWeek)0,
                        (DayOfWeek)1,
                        (DayOfWeek)5,
                    },
                    [(TowerType)3] = new List<DayOfWeek>
                    {
                        (DayOfWeek)0,
                        (DayOfWeek)2,
                        (DayOfWeek)5,
                    },
                    [(TowerType)4] = new List<DayOfWeek>
                    {
                        (DayOfWeek)0,
                        (DayOfWeek)3,
                        (DayOfWeek)6,
                    },
                    [(TowerType)5] = new List<DayOfWeek>
                    {
                        (DayOfWeek)0,
                        (DayOfWeek)4,
                        (DayOfWeek)6,
                    },
                };

                FreeCountPerDay = 3;
                MaxClearNewFloorAtElementTowerPerDay = 10;
                TowerTypeNum = 1;
            }

            public static Dictionary<TowerType, List<DayOfWeek>> OpenedDayOfWeekDictionary;
        }

        public static class BattleAuto
        {
            public static long DefaultAverageTime { get; }

            public static long DefaultChapterId { get; }

            public static int DefaultDropBalance { get; }

            public static int DefaultEfficiency { get; }

            public static long DefaultQuestId { get; }

            public static long ExpectedPlayerExpZeroState { get; }

            public static long MaxBattleRewardTime { get; }

            public static long OneDay { get; }

            public static long OneHour { get; }

            public static long OneMinute { get; }

            public static long PlayerMaxRankState { get; }

            public static long PossiblePlayerRankUpState { get; }

            public static long WaitTimeAfterBattle { get; }

            public static long WaitTimeAutoRecoveryMP { get; }

            public static long WaitTimeBeforeBattle { get; }

            public static long WaitTimeResultBattle { get; }

            static BattleAuto()
            {
                MinBattleEfficiency = 65;
                DefaultAverageTime = 40000;
                DefaultChapterId = 1;
                DefaultDropBalance = 100;
                DefaultEfficiency = 80;
                DefaultQuestId = 1;
                ExpectedPlayerExpZeroState = -1;
                MaxBattleRewardTime = 86400000;
                OneDay = 86400000;
                OneHour = 3600000;
                OneMinute = 60000;
                PlayerMaxRankState = -2;
                PossiblePlayerRankUpState = 0;
                WaitTimeAfterBattle = 2000;
                WaitTimeAutoRecoveryMP = 500;
                WaitTimeBeforeBattle = 2000;
                WaitTimeResultBattle = 3000;
            }

            public static int MinBattleEfficiency;
        }

        public static class BattleBoss
        {
            public static long ClearPartyLogChapterId { get; }

            public static long DefaultQuestId { get; }

            public static long MaxBossBattleFreeCount { get; } = 3;

            static BattleBoss()
            {
                ClearPartyLogChapterId = 5;
                DefaultQuestId = 0;
                MaxBossBattleFreeCount = 3;
            }
        }

        public static class BattlePvp
        {
            public static List<DayOfWeek> AlwaysOpeningLegendLeagueDayOfWeeks { get; }

            public static List<IUserItem> AttackFailedRewardList { get; }

            public static List<IUserItem> AttackSucceededRewardList { get; }

            public static DayOfWeek CloseLegendLeagueDayOfWeek { get; }

            public static long LegendLeagueBorderRank { get; }

            public static List<long> LegendLeagueConsecutiveVictoryBonus { get; }

            public static List<long> LegendLeagueConsecutiveVictoryBonusRange { get; }

            public static long LegendLeagueDefeatPoint { get; }

            public static long LegendLeagueEndTime { get; }

            public static long LegendLeagueInitialPoint { get; }

            public static List<long> LegendLeagueMatchingRange { get; }

            public static List<long> LegendLeagueMatchingRankingBorder { get; }

            public static int LegendLeagueMatchingRivalCount { get; }

            public static long LegendLeaguePlayerRankPart { get; }

            public static long LegendLeagueTimestampPart { get; }

            public static long LegendLeaguePointBonusCalculator { get; }

            public static long LegendLeaguePointBonusDiff { get; }

            public static long LegendLeagueRegisterRequiredRank { get; }

            public static long LegendLeagueSearchMaxCorrectionRank { get; }

            public static long LegendLeagueSearchMinCorrectionRank { get; }

            public static long LegendLeagueStartTime { get; }

            public static TimeSpan LegendLeagueStartTimeSpan { get; }

            public static long LegendLeagueTopRankerMax { get; }

            public static long LegendLeagueTopRankerMin { get; }

            public static long LegendLeagueUtcUpdateTime { get; }

            public static long LegendLeagueUpdateHour { get; }

            public static long LegendLeagueUpdateMinute { get; }

            public static long LegendLeagueVictoryPoint { get; }

            public static List<long> LocalPvpRankingLowerLimitList { get; }

            public static long MaxDefenseSucceededRewardNumPerDay { get; }

            public static long MaxLegendLeagueBattleFreeCount { get; } = 10;

            public static long MaxLegendLeagueBuyChallengeCount { get; }

            public static long MaxPvpBattleFreeCount { get; } = 5;

            public static List<DayOfWeek> NotOpeningLegendLeagueDayOfWeeks { get; }

            public static DayOfWeek OpenLegendLeagueDayOfWeek { get; }

            public static int PvpDailyRewardHour { get; }

            public static int PvpDailyRewardMinute { get; }

            // Note: this type is marked as 'beforefieldinit'.
            static BattlePvp()
            {
                AlwaysOpeningLegendLeagueDayOfWeeks = new List<DayOfWeek>
                {
                    (DayOfWeek)3,
                    (DayOfWeek)4,
                    (DayOfWeek)5,
                    (DayOfWeek)6,
                };
                AttackFailedRewardList = new List<IUserItem>
                {
                    new UserExchangePlaceItem((ExchangePlaceItemType)10, 5),
                    new UserExchangePlaceItem((ExchangePlaceItemType)3, 5),
                };
                AttackSucceededRewardList = new List<IUserItem>
                {
                    new UserExchangePlaceItem((ExchangePlaceItemType)10, 10),
                    new UserExchangePlaceItem((ExchangePlaceItemType)3, 10),
                };
                CloseLegendLeagueDayOfWeek = (DayOfWeek)0;
                LegendLeagueBorderRank = 50;
                LegendLeagueConsecutiveVictoryBonus = new List<long> { 6, 5, 4, 3, 2, 1, 0 };
                LegendLeagueConsecutiveVictoryBonusRange = new List<long> { 50, 30, 20, 10, 5, 3 };
                LegendLeagueDefeatPoint = 10;
                LegendLeagueEndTime = 203000;
                LegendLeagueInitialPoint = 1000;
                LegendLeagueMatchingRange = new List<long> { 7, 6, 5 };
                LegendLeagueMatchingRankingBorder = new List<long> { 500, 100 };
                LegendLeagueMatchingRivalCount = 5;
                LegendLeagueTimestampPart = 10000000000;
                LegendLeaguePointBonusCalculator = 20;
                LegendLeaguePointBonusDiff = 60;
                LegendLeagueRegisterRequiredRank = 50;
                LegendLeagueSearchMaxCorrectionRank = 4;
                LegendLeagueSearchMinCorrectionRank = 6;
                LegendLeagueStartTime = 210000;
                LegendLeagueStartTimeSpan = TimeSpan.FromHours(21);
                LegendLeagueTopRankerMax = 50;
                LegendLeagueTopRankerMin = 1;
                LegendLeagueUtcUpdateTime = (long)TimeSpan.FromHours(11).Add(TimeSpan.FromMinutes(30)).TotalMilliseconds;
                LegendLeagueUpdateHour = 20;
                LegendLeagueUpdateMinute = 30;
                LegendLeagueVictoryPoint = 20;
                LocalPvpRankingLowerLimitList = new List<long> { 1, 50, 100, 500, 1000, 5000 };
                MaxDefenseSucceededRewardNumPerDay = 5;
                MaxLegendLeagueBuyChallengeCount = 5;
                NotOpeningLegendLeagueDayOfWeeks = new List<DayOfWeek>
                {
                    (DayOfWeek)1,
                };
                OpenLegendLeagueDayOfWeek = (DayOfWeek)2;
                PvpDailyRewardHour = 20;
                PvpDailyRewardMinute = 30;
            }
        }

        public static class Deck
        {
            public static int MaxCount { get; }

            static Deck()
            {
                MaxCount = 5;
            }
        }

        public static class DungeonBattle
        {
            public static int MissedCountMax { get; }

            public static long CanConsumeRecoveryItemLimitPerTerm { get; }

            public static int CanMultiplePossessionCount { get; }

            public static int ClearLayerRewardsCount { get; }

            public static int ConsumeDungeonBattleCoinAtResetBattle { get; }

            public static int ConsumeDungeonRecoveryItemCount { get; }

            public static int ContractPrivilegeCompensationRate { get; }

            public static int ContractPrivilegeDungeonCoinBonus { get; }

            public static int DefaultBattleCharacterCurrentHpPerMill { get; }

            public static long DungeonBattleMaxTermId { get; }

            public static List<DungeonBattleGridType> EnemyDropRelicGridType { get; }

            public static int EnemyDropRelicLotteryCount { get; }

            public static Dictionary<DungeonBattleGridType, List<int>> EnemyDropRelicLotteryTable { get; }

            public static int GetRelicRarityLotteryCountAtReinforceRelic { get; }

            public static List<int> GetRelicRarityLotteryTableAtReinforceRelic { get; }

            public static int GuestCount { get; }

            public static long MaxHpPerMill { get; }

            public static int MaxLayerCount { get; }

            public static int MissedCompensationRate { get; }

            public static int MysteryShopEquipmentCount { get; }

            public static List<ItemType> MysteryShopEquipmentSaleTargetType { get; }

            public static int MysteryShopEquipmentSalePercent { get; }

            public static int MysteryShopItemCount { get; }

            public static int MysteryShopItemLimitTradeCount { get; }

            public static int MysteryShopItemSalePercent { get; }

            public static List<ItemType> MysteryShopTargetEquipmentItemType { get; }

            public static long RecoveryBonusRelicId { get; }

            public static List<int> ReinforceRelicCountLotteryTable { get; }

            public static List<int> ReinforceRelicRarityLotteryBorder { get; }

            public static int RequiredPlayerRankHard { get; }

            public static long SetEnemyForBeginnerLimitQuestProgress { get; }

            public static List<DungeonBattleGridType> NormalEnemyBattleTypeList { get; }

            public static List<DungeonBattleGridType> StrongEnemyBattleTypeList { get; }

            public static long TermCalculationBaseTimestamp { get; }

            public static long TermSpan { get; }

            public static long MissedCountMaxTermDiff { get; }

            // Note: this type is marked as 'beforefieldinit'.
            static DungeonBattle()
            {
                MissedCountMax = 3;
                CanConsumeRecoveryItemLimitPerTerm = 20;
                CanMultiplePossessionCount = 4;
                ClearLayerRewardsCount = 3;
                ConsumeDungeonBattleCoinAtResetBattle = 10;
                ConsumeDungeonRecoveryItemCount = 1;
                ContractPrivilegeCompensationRate = 20;
                DefaultBattleCharacterCurrentHpPerMill = 1000;
                DungeonBattleMaxTermId = 40;

                EnemyDropRelicGridType = new List<DungeonBattleGridType>
                {
                    (DungeonBattleGridType)1,
                    (DungeonBattleGridType)2,
                    (DungeonBattleGridType)3,
                    (DungeonBattleGridType)9,
                    (DungeonBattleGridType)12,
                    (DungeonBattleGridType)13,
                    (DungeonBattleGridType)14,
                };

                EnemyDropRelicLotteryCount = 3;
                EnemyDropRelicLotteryTable = new Dictionary<DungeonBattleGridType, List<int>>
                {
                    [(DungeonBattleGridType)1] = new List<int> { 70, 100, 100 },
                    [(DungeonBattleGridType)2] = new List<int> { 0, 70, 100 },
                    [(DungeonBattleGridType)3] = new List<int> { 0, 0, 100 },
                };

                GetRelicRarityLotteryCountAtReinforceRelic = 1;
                GetRelicRarityLotteryTableAtReinforceRelic = new List<int> { 0, 60, 100 };
                GuestCount = 4;
                MaxHpPerMill = 1000;
                MaxLayerCount = 3;
                MissedCompensationRate = 80;

                MysteryShopEquipmentCount = 3;
                MysteryShopEquipmentSaleTargetType = new List<ItemType>
                {
                    (ItemType)4,
                    (ItemType)5,
                    (ItemType)9,
                };
                MysteryShopEquipmentSalePercent = 20;
                MysteryShopItemCount = 4;
                MysteryShopItemLimitTradeCount = 1;
                MysteryShopItemSalePercent = 50;
                MysteryShopTargetEquipmentItemType = new List<ItemType>
                {
                    (ItemType)4,
                    (ItemType)9,
                    (ItemType)5,
                };

                RecoveryBonusRelicId = 48;
                ReinforceRelicCountLotteryTable = new List<int> { 10, 70, 100 };
                ReinforceRelicRarityLotteryBorder = new List<int> { 0, 40, 100 };
                RequiredPlayerRankHard = 20;
                SetEnemyForBeginnerLimitQuestProgress = 60;

                NormalEnemyBattleTypeList = new List<DungeonBattleGridType>
                {
                    (DungeonBattleGridType)1,
                    (DungeonBattleGridType)12,
                };
                StrongEnemyBattleTypeList = new List<DungeonBattleGridType>
                {
                    (DungeonBattleGridType)2,
                    (DungeonBattleGridType)3,
                    (DungeonBattleGridType)4,
                    (DungeonBattleGridType)9,
                    (DungeonBattleGridType)10,
                    (DungeonBattleGridType)13,
                    (DungeonBattleGridType)14,
                };

                TermCalculationBaseTimestamp = 14400000;
                TermSpan = 172800000;
                MissedCountMaxTermDiff = 4;
            }
        }

        public static class Character
        {
            public static UserItem CharacterCoinSell { get; }

            public static List<long> CharacterLevelCapBySkillLevel { get; }

            public static UserItem CharacterTrainingMaterialSell { get; }

            public static Dictionary<ElementType, ElementClassificationType> ElementClassificationDict { get; }

            public static Dictionary<CharacterRarityFlags, long> MaxCharacterLevel { get; }

            public static CharacterRarityFlags RankResetReceiveCharacterRarityFlags { get; }

            public static Dictionary<ElementType, List<long>> RankUpPrioritySettingDefault { get; }

            public static long RankUpPrioritySettingMemberCount { get; }

            public static long ResetLevelRequiredCurrency { get; }

            public static long ResetRankRequiredCurrency { get; }

            public static Dictionary<ElementType, long> ReturnWitchLetterIdDict { get; }

            // Note: this type is marked as 'beforefieldinit'.
            static Character()
            {
                MaxSkillCount = 4;

                RankUpMaxRarityFlags = new Dictionary<CharacterRarityFlags, CharacterRarityFlags>
                {
                    [(CharacterRarityFlags)1] = (CharacterRarityFlags)1,
                    [(CharacterRarityFlags)2] = (CharacterRarityFlags)64,
                    [(CharacterRarityFlags)8] = (CharacterRarityFlags)524288,
                };
                RankUpMaxRarityFlagsR = (CharacterRarityFlags)64;
                RankUpMaxRarityFlagsSR = (CharacterRarityFlags)524288;
                RankUpMinRarityFlags = (CharacterRarityFlags)2;
                ReleaseableCharacterRarity = (CharacterRarityFlags)1;
                MaxRarityFlagsWithoutArcanaBonus = (CharacterRarityFlags)16384;

                CharacterCoinSell = new UserItem
                {
                    ItemType = (ItemType)13,
                    ItemId = 14,
                    ItemCount = 400,
                };

                CharacterLevelCapBySkillLevel = new List<long>
                {
                    10,
                    20,
                    40,
                    60,
                    80,
                    100,
                    120,
                    140,
                    160,
                    180,
                    200,
                    220,
                };

                CharacterTrainingMaterialSell = new UserItem
                {
                    ItemType = (ItemType)11,
                    ItemId = 2,
                    ItemCount = 6,
                };

                ElementClassificationDict = new Dictionary<ElementType, ElementClassificationType>
                {
                    [ElementType.Blue] = ElementClassificationType.DefaultElement,
                    [ElementType.Red] = ElementClassificationType.DefaultElement,
                    [ElementType.Green] = ElementClassificationType.DefaultElement,
                    [ElementType.Yellow] = ElementClassificationType.DefaultElement,
                    [ElementType.Light] = ElementClassificationType.SpecialElement,
                    [ElementType.Dark] = ElementClassificationType.SpecialElement,
                };

                MaxCharacterLevel = new Dictionary<CharacterRarityFlags, long>
                {
                    [(CharacterRarityFlags)1] = 100,
                    [(CharacterRarityFlags)2] = 100,
                    [(CharacterRarityFlags)4] = 100,
                    [(CharacterRarityFlags)8] = 100,
                    [(CharacterRarityFlags)16] = 120,
                    [(CharacterRarityFlags)32] = 140,
                    [(CharacterRarityFlags)64] = 160,
                    [(CharacterRarityFlags)128] = 180,
                    [(CharacterRarityFlags)256] = 200,
                    [(CharacterRarityFlags)512] = 240,
                    [(CharacterRarityFlags)1024] = 240,
                    [(CharacterRarityFlags)2048] = 240,
                    [(CharacterRarityFlags)4096] = 240,
                    [(CharacterRarityFlags)8192] = 240,
                    [(CharacterRarityFlags)16384] = 240,
                    [(CharacterRarityFlags)32768] = 240,
                    [(CharacterRarityFlags)65536] = 240,
                    [(CharacterRarityFlags)131072] = 240,
                    [(CharacterRarityFlags)262144] = 240,
                    [(CharacterRarityFlags)524288] = 240,
                };

                RankResetReceiveCharacterRarityFlags = (CharacterRarityFlags)16;
                ResetLevelRequiredCurrency = 50;
                ResetRankRequiredCurrency = 500;

                ReturnWitchLetterIdDict = new Dictionary<ElementType, long>
                {
                    [ElementType.Blue] = 17,
                    [ElementType.Red] = 18,
                    [ElementType.Green] = 19,
                    [ElementType.Yellow] = 20,
                };

                RankUpPrioritySettingDefault = new Dictionary<ElementType, List<long>>
                {
                    [ElementType.Blue] = new List<long> { 2, 3, 4 },
                    [ElementType.Red] = new List<long> { 12, 13, 14 },
                    [ElementType.Green] = new List<long> { 22, 23, 24 },
                    [ElementType.Yellow] = new List<long> { 32, 33, 34 },
                };

                RankUpPrioritySettingMemberCount = 3;
            }

            public static int MaxSkillCount;

            public static Dictionary<CharacterRarityFlags, CharacterRarityFlags> RankUpMaxRarityFlags;

            public static CharacterRarityFlags RankUpMaxRarityFlagsR;

            public static CharacterRarityFlags RankUpMaxRarityFlagsSR;

            public static CharacterRarityFlags RankUpMinRarityFlags;

            public static CharacterRarityFlags ReleaseableCharacterRarity;

            public static CharacterRarityFlags MaxRarityFlagsWithoutArcanaBonus;
        }

        public static class CharacterCollection
        {
            public static int OpenAllCharacterCollectionLevel { get; }

            // Note: this type is marked as 'beforefieldinit'.
            static CharacterCollection()
            {
                OpenAllCharacterCollectionLevel = 3;
                CharacterCollectionBonusMaxRarityDict = new Dictionary<long, CharacterRarityFlags>
                {
                    [0] = (CharacterRarityFlags)16384,
                    [1] = (CharacterRarityFlags)32768,
                    [2] = (CharacterRarityFlags)65536,
                    [3] = (CharacterRarityFlags)131072,
                    [4] = (CharacterRarityFlags)262144,
                    [5] = (CharacterRarityFlags)524288,
                };
            }

            public static Dictionary<long, CharacterRarityFlags> CharacterCollectionBonusMaxRarityDict = new();
        }

        public static class CharacterShardReversion
        {
            public static Dictionary<CharacterRarityFlags, (int CharacterCount, int CharacterFragmentCount)> RCharacterReturnItemCountDict { get; } = new();

            public static Dictionary<CharacterRarityFlags, int> SRCharacterReturnItemCountDict { get; } = new();

            public static CharacterRarityFlags UnlockRarity { get; }

            static CharacterShardReversion()
            {
                SRCharacterReturnItemCountDict[(CharacterRarityFlags)8] = 60;
                SRCharacterReturnItemCountDict[(CharacterRarityFlags)16] = 120;

                RCharacterReturnItemCountDict[(CharacterRarityFlags)2] = (60, 0);
                RCharacterReturnItemCountDict[(CharacterRarityFlags)4] = (180, 0);
                RCharacterReturnItemCountDict[(CharacterRarityFlags)8] = (180, 6);
                RCharacterReturnItemCountDict[(CharacterRarityFlags)16] = (360, 12);
                RCharacterReturnItemCountDict[(CharacterRarityFlags)32] = (360, 48);
                RCharacterReturnItemCountDict[(CharacterRarityFlags)64] = (720, 60);
                RCharacterReturnItemCountDict[(CharacterRarityFlags)128] = (720, 132);
                RCharacterReturnItemCountDict[(CharacterRarityFlags)256] = (720, 204);
                RCharacterReturnItemCountDict[(CharacterRarityFlags)512] = (1440, 228);
                RCharacterReturnItemCountDict[(CharacterRarityFlags)1024] = (1800, 240);
                RCharacterReturnItemCountDict[(CharacterRarityFlags)2048] = (2160, 252);
                RCharacterReturnItemCountDict[(CharacterRarityFlags)4096] = (2520, 264);
                RCharacterReturnItemCountDict[(CharacterRarityFlags)8192] = (2880, 276);
                RCharacterReturnItemCountDict[(CharacterRarityFlags)16384] = (3600, 300);
                RCharacterReturnItemCountDict[(CharacterRarityFlags)32768] = (4320, 324);
                RCharacterReturnItemCountDict[(CharacterRarityFlags)65536] = (5040, 348);
                RCharacterReturnItemCountDict[(CharacterRarityFlags)131072] = (5760, 372);
                RCharacterReturnItemCountDict[(CharacterRarityFlags)262144] = (6480, 396);
                RCharacterReturnItemCountDict[(CharacterRarityFlags)524288] = (7200, 420);

                UnlockRarity = (CharacterRarityFlags)16384;
            }
        }

        public static class Equipment
        {
            public static Dictionary<EquipmentRarityFlags, int> AdditionalParameterCountDict { get; }

            public static long EquipmentAbsorbRequireGold { get; }

            public static long EquipmentMergeRequireFame { get; set; }

            public static long EquipmentMergeRequireGold { get; }

            public static long EquipmentMergeSacredTreasureGold { get; }

            public static long EquipmentTypeCount { get; }

            public static long ExchangeFragmentRequireItemCount { get; }

            public static ExchangePlaceItemType ExchangeFragmentRequireItemType { get; }

            public static long ExchangeFragmentRewardItemCount { get; }

            public static long AvailableResetEquipmentLevel { get; }

            public static long BulkTrainingCount { get; }

            public static int ComposeManySphereMaxLv { get; }

            public static int MaxLevelDifferenceThatCanBeEquipped { get; }

            public static double MaxRateBaseParameterDefault { get; }

            public static double MaxRateBaseParameterTraining { get; }

            public static int MaxUnlockSphereSlot { get; }

            public static List<IUserItem> RequiredItemListToLockWithTraining { get; }

            public static List<IUserItem> RequiredItemListToUnlockSphereSlot { get; }

            // Note: this type is marked as 'beforefieldinit'.
            static Equipment()
            {
                ActiveExclusiveSkillRaritys = new List<EquipmentRarityFlags>
                {
                    EquipmentRarityFlags.SSR,
                    EquipmentRarityFlags.UR,
                    EquipmentRarityFlags.LR,
                };

                EquipmentComposeCountMissionEquipmentLv = 180;
                EvolutionExclusivePossibleLevel = 240;
                EvolutionMaxReinforcementIncreaseOnLimitReached = 10;
                EvolutionMaxReinforcementLevel = 240;
                EvolutionSetPossibleLevel = 180;
                MaxAdditionalParameterCount = 4;
                MaxSacredTreasureMaterialSelect = 7;
                RequiredCharacterLevelForEquipLr = 240;
                RequiredCharacterRarityForEquipLr = (CharacterRarityFlags)16384;

                AdditionalParameterCountDict = new Dictionary<EquipmentRarityFlags, int>
                {
                    [(EquipmentRarityFlags)1] = 0,
                    [(EquipmentRarityFlags)2] = 1,
                    [(EquipmentRarityFlags)4] = 2,
                    [(EquipmentRarityFlags)8] = 3,
                    [(EquipmentRarityFlags)16] = 4,
                    [(EquipmentRarityFlags)32] = 4,
                    [(EquipmentRarityFlags)64] = 4,
                    [(EquipmentRarityFlags)128] = 4,
                    [(EquipmentRarityFlags)256] = 4,
                    [(EquipmentRarityFlags)512] = 4,
                };

                EquipmentAbsorbRequireGold = 150000;
                EquipmentMergeRequireFame = 2000;
                EquipmentMergeRequireGold = 200000;
                EquipmentMergeSacredTreasureGold = 1000;
                EquipmentTypeCount = 6;
                ExchangeFragmentRequireItemCount = 3;
                ExchangeFragmentRequireItemType = (ExchangePlaceItemType)1;
                ExchangeFragmentRewardItemCount = 10;
                MaxLevelDifferenceThatCanBeEquipped = 10;
                MaxRateBaseParameterDefault = 0.3;
                MaxRateBaseParameterTraining = 0.6;
                MaxUnlockSphereSlot = 4;

                RequiredItemListToLockWithTraining = new List<IUserItem>
                {
                    new UserCurrencyFree(20),
                    new UserCurrencyFree(25),
                };

                BulkTrainingCount = 20;

                RequiredItemListToUnlockSphereSlot = new List<IUserItem>
                {
                    new UserCurrencyFree(20),
                    new UserCurrencyFree(50),
                    new UserCurrencyFree(100),
                };

                ComposeManySphereMaxLv = 10;
                AvailableResetEquipmentLevel = 300;
            }

            public static readonly List<EquipmentRarityFlags> ActiveExclusiveSkillRaritys = new() { EquipmentRarityFlags.SSR, EquipmentRarityFlags.UR, EquipmentRarityFlags.LR };

            public static long EquipmentComposeCountMissionEquipmentLv;

            public static int EvolutionExclusivePossibleLevel;

            public static int EvolutionMaxEquipmentLevel;

            public static int EvolutionMaxReinforcementIncreaseOnLimitReached;

            public static int EvolutionMaxReinforcementLevel;

            public static int EvolutionSetPossibleLevel;

            public static int MaxAdditionalParameterCount;

            public static int MaxSacredTreasureMaterialSelect;

            public static int RequiredCharacterLevelForEquipLr;

            public static CharacterRarityFlags RequiredCharacterRarityForEquipLr;
        }

        public static class GuildBattle
        {
            static GuildBattle()
            {
                SkillCoolDownForRelay = 10;
            }

            public static int SkillCoolDownForRelay;
        }

        public static class Gvg
        {
            public static int CastleMemoMessageMaxLength { get; }

            public static int DefaultActionPoint { get; }

            public static int DisplayActivePlayerRankingCount { get; }

            public static int DisplayBattleDialogPartyCount { get; }

            public static int DisplayMvpPlayerRankingCount { get; }

            public static int LargeCastleCountForMediumDeclaration { get; }

            public static int MediumCastleCountForLargeDeclaration { get; }

            public static int SmallCastleCountForMediumDeclaration { get; }

            public static int WaitingUpdateMvpRankingMinutes { get; }

            static Gvg()
            {
                ValidJoinedGuildSeconds = 86400.0;
                DefaultActionPoint = 2;
                DisplayBattleDialogPartyCount = 3;
                CastleMemoMessageMaxLength = 12;
                DisplayActivePlayerRankingCount = 5;
                DisplayMvpPlayerRankingCount = 3;
                WaitingUpdateMvpRankingMinutes = 1;
                SmallCastleCountForMediumDeclaration = 2;
                LargeCastleCountForMediumDeclaration = 1;
                MediumCastleCountForLargeDeclaration = 1;
            }

            public static double ValidJoinedGuildSeconds = (double) ((ulong) 4680673776000565248L);
        }

        public static class LocalGvg
        {
            public static int AddCounterMilliseconds { get; }

            public static int EndDeclarationHour { get; }

            public static int EndDeclarationMinute { get; }

            public static int EndHour { get; }

            public static int EndMinute { get; }

            public static int MaxCharacterNum { get; }

            public static int MaxDeclarableGuildStockRank { get; }

            public static int MaxDeclarationCount { get; }

            public static int StartDeclarationHour { get; }

            public static int StartDeclarationMinute { get; }

            public static int StartHour { get; }

            public static int StartMinute { get; }

            public static int CanDeclareCount { get; }

            public static int StartCloseGuildBattleHour { get; }

            public static int StartCloseGuildBattleMinute { get; }

            public static int EndCloseGuildBattleHour { get; }

            public static int EndCloseGuildBattleMinute { get; }

            static LocalGvg()
            {
                AddCounterMilliseconds = 900000;
                EndDeclarationHour = 20;
                EndDeclarationMinute = 30;
                EndHour = 21;
                EndMinute = 30;
                MaxCharacterNum = 5;
                MaxDeclarationCount = 2;
                StartDeclarationHour = 7;
                StartDeclarationMinute = 45;
                StartHour = 20;
                StartMinute = 45;
                CanDeclareCount = 2;
                StartCloseGuildBattleHour = 4;
                StartCloseGuildBattleMinute = 0;
                EndCloseGuildBattleHour = 4;
                EndCloseGuildBattleMinute = 30;
                MaxDeclarableGuildStockRank = 16;
            }
        }

        public static class GlobalGvg
        {
            public static int AddCounterMilliseconds { get; }

            public static int EndDeclarationHour { get; }

            public static int EndDeclarationMinute { get; }

            public static int EndHour { get; }

            public static int EndMinute { get; }

            public static int EndMatchingHour { get; }

            public static int EndMatchingMinute { get; }

            public static int MaxCharacterNum { get; }

            public static int MaxDeclarationCount { get; }

            public static int StartDeclarationHour { get; }

            public static int StartDeclarationMinute { get; }

            public static int StartHour { get; }

            public static int StartMinute { get; }

            public static int StartSeasonHour { get; }

            public static int StartSeasonMinute { get; }

            public static int DefaultHasGroup1Count { get; }

            public static int DefaultHasGroup2Count { get; }

            public static int DefaultHasGroup3Count { get; }

            // Note: this type is marked as 'beforefieldinit'.
            static GlobalGvg()
            {
                GroupNameKey = new[]
                {
                    "[GvgGroup1NameLabel]",
                    "[GvgGroup2NameLabel]",
                    "[GvgGroup3NameLabel]",
                    "[GvgGroup4NameLabel]",
                };
                GlobalGvgGroupTypeNameKey = new[]
                {
                    "[GvgGroupLevelNameBronzeLabel]",
                    "[GvgGroupLevelNameSilverLabel]",
                    "[GvgGroupLevelNameGoldenLabel]",
                };
                AddCounterMilliseconds = 900000;
                EndDeclarationHour = 20;
                EndDeclarationMinute = 30;
                EndHour = 21;
                EndMinute = 30;
                EndMatchingHour = 7;
                EndMatchingMinute = 30;
                MaxCharacterNum = 5;
                MaxDeclarationCount = 3;
                StartDeclarationHour = 7;
                StartDeclarationMinute = 45;
                StartHour = 20;
                StartMinute = 45;
                StartSeasonHour = 4;
                StartSeasonMinute = 0;
                DefaultHasGroup1Count = 1;
                DefaultHasGroup2Count = 1;
                DefaultHasGroup3Count = 2;
            }

            public static readonly string[] GroupNameKey;

            public static readonly string[] GlobalGvgGroupTypeNameKey;
        }

        public static class Friend
        {
            public static int AcquisitionFriendPointPerFriend { get; } = 5;

            public static int DefaultMaxFriendNum { get; }

            public static long FirstMaxFriendUpQuestId { get; }

            public static int MaxApplyingNum { get; } = 1000;

            public static int MaxApprovalPendingNum { get; } = 90;

            public static int MaxBlockNum { get; } = 100;

            public static int MaxDailyFriendBattleCount { get; }

            public static int MaxDailyReceiveFriendPoint { get; } = 20;

            public static int MaxFriendNum { get; } = 40;

            public static Dictionary<long, int> MaxFriendNumByQuestIdMap { get; }

            public static int MaxFriendPoint { get; } = 9999;

            public static long UsableFriendCodeTime { get; } = 604800000;

            static Friend()
            {
                DefaultMaxFriendNum = 40;
                FirstMaxFriendUpQuestId = 460;
                MaxFriendNumByQuestIdMap = new Dictionary<long, int>
                {
                    [FirstMaxFriendUpQuestId] = 50,
                };
                MaxDailyFriendBattleCount = 500;
            }

            public static long RecommendFriendDisplayNum = 20;
        }

        public static class MyPageBanner
        {
            public static int MaxDisplayBannerNum { get; }
        }

        public static class PlayerInfo
        {
            public static int MaxPlayerInfoInPage { get; }

            static PlayerInfo()
            {
                MaxPlayerInfoInPage = 20;
            }
        }

        public static class Ranking
        {
            public static int DisplayRankingCount { get; }

            static Ranking()
            {
                DisplayRankingCount = 20;
            }
        }

        public static class Mission
        {
            public static int ComeBackMissionIntervalLoginDays { get; }

            public static int ComeBackMissionIntervalOccurDays { get; }

            public static List<MissionAchievementType> MissionAchievementTypeIsRanking { get; }

            public static Dictionary<MissionGroupType, MissionActivityRewardType> MissionActivityRewardTypeDict { get; }

            public static Dictionary<MissionGroupType, long> MissionExpirationDays { get; }

            public static Dictionary<long, MissionAchievementType> MissionTradeShopTypes { get; }

            public static List<MissionAchievementType> SnsMissionAchievementTypes { get; }

            public static int MissionAchievementTypeTensInterval { get; }

            public static int DivideUnlockPanelGridItemIdToSheetNoNum { get; }

            // Note: this type is marked as 'beforefieldinit'.
            static Mission()
            {
                PlatinumPointItemId = 4;
                ComeBackMissionIntervalLoginDays = 14;
                ComeBackMissionIntervalOccurDays = 90;
                DivideUnlockPanelGridItemIdToSheetNoNum = 1000;

                MissionAchievementTypeIsRanking = new List<MissionAchievementType>
                {
                    (MissionAchievementType)12010200,
                };

                MissionActivityRewardTypeDict = new Dictionary<MissionGroupType, MissionActivityRewardType>
                {
                    [(MissionGroupType)0] = (MissionActivityRewardType)0,
                    [(MissionGroupType)1] = (MissionActivityRewardType)1,
                    [(MissionGroupType)2] = (MissionActivityRewardType)1,
                    [(MissionGroupType)3] = (MissionActivityRewardType)2,
                    [(MissionGroupType)4] = (MissionActivityRewardType)0,
                    [(MissionGroupType)5] = (MissionActivityRewardType)0,
                    [(MissionGroupType)6] = (MissionActivityRewardType)2,
                    [(MissionGroupType)9] = (MissionActivityRewardType)0,
                    [(MissionGroupType)10] = (MissionActivityRewardType)1,
                    [(MissionGroupType)11] = (MissionActivityRewardType)0,
                    [(MissionGroupType)12] = (MissionActivityRewardType)1,
                    [(MissionGroupType)13] = (MissionActivityRewardType)0,
                    [(MissionGroupType)1000] = (MissionActivityRewardType)0,
                    [(MissionGroupType)14] = (MissionActivityRewardType)0,
                    [(MissionGroupType)15] = (MissionActivityRewardType)0,
                };

                MissionExpirationDays = new Dictionary<MissionGroupType, long>
                {
                    [(MissionGroupType)0] = -1,
                    [(MissionGroupType)1] = 1,
                    [(MissionGroupType)2] = 7,
                    [(MissionGroupType)3] = 10,
                    [(MissionGroupType)4] = 10,
                    [(MissionGroupType)5] = -1,
                    [(MissionGroupType)6] = -1,
                    [(MissionGroupType)9] = -1,
                    [(MissionGroupType)10] = 7,
                    [(MissionGroupType)11] = -1,
                    [(MissionGroupType)12] = -1,
                    [(MissionGroupType)13] = -1,
                    [(MissionGroupType)14] = -1,
                    [(MissionGroupType)15] = -1,
                };

                MissionTradeShopTypes = new Dictionary<long, MissionAchievementType>
                {
                    [1] = (MissionAchievementType)5030200,
                    [2] = (MissionAchievementType)5020200,
                    [3] = (MissionAchievementType)5010100,
                    [4] = (MissionAchievementType)5040100,
                    [6] = (MissionAchievementType)5050100,
                };

                SnsMissionAchievementTypes = new List<MissionAchievementType>
                {
                    (MissionAchievementType)4020100,
                    (MissionAchievementType)4020200,
                    (MissionAchievementType)4020300,
                    (MissionAchievementType)4020400,
                };

                MissionAchievementTypeTensInterval = 100;
            }

            public static long PlatinumPointItemId;
        }

        public static class Shop
        {
            public static int MonthlyBoostBattleQuickBonus { get; } = 1;

            public static int MonthlyBoostCharacterExpBonus { get; } = 15;

            public static int MonthlyBoostPlayerExpBonus { get; } = 15;

            public static int MonthlyBoostPopulationGoldGoldBonus { get; } = 15;

            public static int MonthlyBoostValidDays { get; } = 30;

            public static long PaidDMMCurrencyLimitDateTime { get; } = 15552000000;

            public static UserExchangePlaceItem RequiredItemForExchangeInProduct { get; }

            public static List<UserExchangePlaceItem> RequiredItemsForExchangeInFame { get; }

            public static int MaxGuerrillaPackCount { get; }

            public static long CanBuyPrePurchasedMonthlyBoostTime { get; }

            // Note: this type is marked as 'beforefieldinit'.
            static Shop()
            {
                CurrencyDivisorForMissionPoint = 2;
                IOSReceiptRetryCount = 1;
                AndroidReceiptRetryCount = 1;

                MonthlyBoostBattleQuickBonus = 1;
                MonthlyBoostCharacterExpBonus = 15;
                MonthlyBoostPlayerExpBonus = 15;
                MonthlyBoostPopulationGoldGoldBonus = 15;
                MonthlyBoostValidDays = 30;

                PaidDMMCurrencyLimitDateTime = 15552000000;
                RequiredItemForExchangeInProduct = new UserExchangePlaceItem(ExchangePlaceItemType.CastingValue, 1500);
                RequiredItemsForExchangeInFame = new List<UserExchangePlaceItem>
                {
                    new UserExchangePlaceItem(ExchangePlaceItemType.Fame, 1000),
                    new UserExchangePlaceItem(ExchangePlaceItemType.CastingValue, 8000),
                };
                MaxGuerrillaPackCount = 10;
                CanBuyPrePurchasedMonthlyBoostTime = (long)TimeSpan.FromDays(3).TotalMilliseconds;
            }

            public const string AndroidAuthTokenUrl = "https://accounts.google.com/o/oauth2/token";

            public const string AndroidRefreshGrantType = "refresh_token";

            public const string AndroidVerifyUrl = "https://www.googleapis.com/androidpublisher/v3/applications/";

            public const int IOSDefaultStatus = -1;

            public const string IOSProductionVerifyUrl = "https://buy.itunes.apple.com/verifyReceipt";

            public const int IOSSandboxStatus = 21007;

            public const string IOSSandBoxVerifyUrl = "https://sandbox.itunes.apple.com/verifyReceipt";

            public const string IOSRefundCheckUrl = "https://api.storekit.itunes.apple.com/inApps/v2/refund/lookup/";

            public const int IOSRefundBatchScheduleId = 1;

            public const int RefundBatchSearchCount = 300;

            public const int PurchaseSuccessStatus = 0;

            public const int AndroidPurchaseRefund = 1;

            public static int CurrencyDivisorForMissionPoint;

            public static int IOSReceiptRetryCount;

            public static int AndroidReceiptRetryCount;
        }

        public static class BountyQuest
        {
            public static Dictionary<CharacterRarityFlags, int> GuerrillaQuestRarityPoint { get; } = new();

            public static Dictionary<int, TimeSpan> GuerrillaQuestTime { get; }

            public static int MaxDispatchMember { get; }

            public static int MaxGuerrillaQuestCount { get; }

            public static long RemakeCurrency { get; }

            public static long RewardPeriod { get; }

            // Note: this type is marked as 'beforefieldinit'.
            static BountyQuest()
            {
                GuerrillaQuestRarityPoint = new Dictionary<CharacterRarityFlags, int>
                {
                    [(CharacterRarityFlags)1] = 1,
                    [(CharacterRarityFlags)2] = 1,
                    [(CharacterRarityFlags)4] = 1,
                    [(CharacterRarityFlags)8] = 2,
                    [(CharacterRarityFlags)16] = 2,
                    [(CharacterRarityFlags)32] = 3,
                    [(CharacterRarityFlags)64] = 3,
                    [(CharacterRarityFlags)128] = 4,
                    [(CharacterRarityFlags)256] = 4,
                    [(CharacterRarityFlags)512] = 5,
                    [(CharacterRarityFlags)1024] = 5,
                    [(CharacterRarityFlags)2048] = 5,
                    [(CharacterRarityFlags)4096] = 5,
                    [(CharacterRarityFlags)8192] = 5,
                    [(CharacterRarityFlags)16384] = 5,
                    [(CharacterRarityFlags)32768] = 5,
                    [(CharacterRarityFlags)65536] = 5,
                    [(CharacterRarityFlags)131072] = 5,
                    [(CharacterRarityFlags)262144] = 5,
                    [(CharacterRarityFlags)524288] = 5,
                };

                GuerrillaQuestTime = new Dictionary<int, TimeSpan>
                {
                    [3] = TimeSpan.FromHours(1),
                    [4] = TimeSpan.FromHours(1),
                    [5] = TimeSpan.FromHours(1),
                    [6] = TimeSpan.FromHours(2),
                    [7] = TimeSpan.FromHours(4),
                    [8] = TimeSpan.FromHours(4),
                    [9] = TimeSpan.FromHours(6),
                    [10] = TimeSpan.FromHours(8),
                    [11] = TimeSpan.FromHours(8),
                    [12] = TimeSpan.FromHours(10),
                    [13] = TimeSpan.FromHours(12),
                    [14] = TimeSpan.FromHours(12),
                    [15] = TimeSpan.FromHours(14),
                };

                MaxDispatchMember = 6;
                MaxGuerrillaQuestCount = 1;
                RemakeCurrency = 20;
                RewardPeriod = 604800000;
            }
        }

        public static class GuildRaid
        {
            public static int AttackRepeatTime { get; }

            public static long AutoJoinCost { get; }

            public static long BattleTime { get; }

            public static long GuildRaidCanJoinDelayTime { get; } = 0;

            public static long GuildRaidCanOpenDelayTime { get; }

            public static long GuildRaidNormalBossStartTime { get; }

            public static long GuildRaidOpeningTime { get; }

            public static int GuildRewardsMaxDamage { get; }

            public static int LotteryDropCurrencyFreeValueRange { get; }

            public static int MaxSupportCount { get; }

            public static int PlayerRewardsGaugeIncreaseAmount { get; }

            public static int PlayerRewardsGaugeInitialAmount { get; }

            public static int RequiredGuildFame { get; }

            public static int RequiredSupportCurrency { get; }

            public static int SupportEffect { get; }

            // Note: this type is marked as 'beforefieldinit'.
            static GuildRaid()
            {
                AttackRepeatTime = 300000;
                AutoJoinCost = 20;
                BattleTime = 14400000;
                GuildRaidCanJoinDelayTime = (long)TimeSpan.FromHours(24).TotalMilliseconds;
                GuildRaidNormalBossStartTime = 14400000;
                GuildRaidCanOpenDelayTime = (long)TimeSpan.FromHours(24).TotalMilliseconds;
                GuildRaidOpeningTime = (long)TimeSpan.FromHours(24).TotalMilliseconds;
                GuildRewardsMaxDamage = 100000;
                LotteryDropCurrencyFreeValueRange = 10000;
                MaxSupportCount = 10;
                PlayerRewardsGaugeIncreaseAmount = 1000;
                PlayerRewardsGaugeInitialAmount = 1000;
                RequiredGuildFame = 100;
                RequiredSupportCurrency = 20;
                SupportEffect = 20;
            }
        }

        public static class LevelLink
        {
            public static int BaseMemberMaxCount { get; } = 5;

            public static int InitPartyLevel { get; } = 240;

            public static Dictionary<CharacterRarityFlags, long> MaxCharacterLevel { get; } = new()
            {
                [CharacterRarityFlags.N] = 100,
                [CharacterRarityFlags.R] = 160,
            };

            public static int MemberInitCount { get; } = 2;

            public static long MemberUnsetCoolTime { get; } = 86400000;

            public static int OpenSlotCountWithCurrency { get; } = 100;

            public static int PartyLevelLimitIncrease { get; } = 5;

            public static Dictionary<CharacterRarityFlags, long> RankReleaseMaxLevel { get; }

            public static int ResetCoolTimeCurrency { get; } = 100;

            static LevelLink()
            {
                BaseMemberMaxCount = 5;
                InitPartyLevel = 240;
                MaxCharacterLevel = new Dictionary<CharacterRarityFlags, long>
                {
                    [CharacterRarityFlags.N] = 100,
                    [CharacterRarityFlags.R] = 160,
                };

                RankReleaseMaxLevel = new Dictionary<CharacterRarityFlags, long>
                {
                    [CharacterRarityFlags.R] = 160,
                    [CharacterRarityFlags.SR] = 160,
                    [CharacterRarityFlags.SSR] = 160,
                    [CharacterRarityFlags.UR] = 160,
                    [CharacterRarityFlags.LR] = 160,
                    [CharacterRarityFlags.LRPlus] = 160,
                    [CharacterRarityFlags.SRPlus] = 180,
                    [CharacterRarityFlags.SSRPlus] = 200,
                    [CharacterRarityFlags.URPlus] = 240,
                    [CharacterRarityFlags.LRPlus2] = 240,
                    [CharacterRarityFlags.LRPlus3] = 240,
                    [CharacterRarityFlags.LRPlus4] = 240,
                    [CharacterRarityFlags.LRPlus5] = 240,
                    [CharacterRarityFlags.LRPlus6] = 240,
                    [CharacterRarityFlags.LRPlus7] = 240,
                    [CharacterRarityFlags.LRPlus8] = 240,
                    [CharacterRarityFlags.LRPlus9] = 240,
                    [CharacterRarityFlags.LRPlus10] = 240,
                };

                MemberInitCount = 2;
                MemberUnsetCoolTime = 86400000;
                OpenSlotCountWithCurrency = 100;
                PartyLevelLimitIncrease = 5;
                ResetCoolTimeCurrency = 100;
            }

        }

        public static class Map
        {
            public static int DisplayMaxQuestCount { get; }

            public static List<UserItem> FirstMapBuildingRewardItems { get; }

            public static int OtherPlayerCount { get; }

            // Note: this type is marked as 'beforefieldinit'.
            static Map()
            {
                DisplayMaxQuestCount = 5;
                FirstMapBuildingRewardItems = new List<UserItem>
                {
                    new UserGold(10000).ToUserItem(),
                    new UserCharacterTrainingMaterial((CharacterTrainingMaterialType)2, 10).ToUserItem(),
                };
                OtherPlayerCount = 5;
            }
        }

        public static class ClearParty
        {
            public static int ListLimitCount { get; }

            public static int MaxSubDay { get; }

            public static int StartSubDay { get; }

            static ClearParty()
            {
                ListLimitCount = 10;
                MaxSubDay = 49;
                StartSubDay = 7;
            }
        }

        public static class LocalRaid
        {
            public static int MaxLevel { get; }

            public static int MinLevel { get; }

            public static int RoomExpire { get; }

            public static long BatchStartTime;

            public static long BatchEndTime;

            static LocalRaid()
            {
                MaxLevel = 999;
                MinLevel = 1;
                RoomExpire = 15;
                BatchStartTime = 40000;
                BatchEndTime = 43000;
            }
        }

        public static class StateBonus
        {
            public static List<DayOfWeek> AutoQuickBonusDayOfWeeks { get; }

            public static long DailyBonusStateId { get; }

            public static int DailyWeeklyBonus { get; }

            // Note: this type is marked as 'beforefieldinit'.
            static StateBonus()
            {
                AutoQuickBonusDayOfWeeks = new List<DayOfWeek>
                {
                    (DayOfWeek)2,
                    (DayOfWeek)4,
                    (DayOfWeek)6,
                    (DayOfWeek)0,
                };
                DailyBonusStateId = 3;
                DailyWeeklyBonus = 2;
            }
        }

        public static class Tutorial
        {
            public static long AbsorbSacredTreasureTutorialId { get; }

            public static long CharacterRankUpTutorialId { get; }

            public static long ChangeEquipmentTutorialId { get; }

            public static long DungeonBattleTutorialId { get; }

            public static long EquipmentReinforcementTutorialId { get; }

            public static long EquipmentSynchroRewardMissionTutorialId { get; }

            public static long EquipmentSynchroUnlockTutorialId { get; }

            public static long EquipmentTrainingTutorialId { get; }

            public static int GiveGachaTicketTutorialId { get; }

            public static long LevelLinkTutorialId { get; }

            public static long LevelUpTutorialId { get; }

            public static long NextChapterTutorialId { get; }

            public static long NextChapterTutorialQuestId { get; }

            public static long ReceiveBountyQuestRewardTutorialId { get; }

            public static long StartBountyQuestTutorialId { get; }

            public static long TutorialGachaCaseId { get; }

            static Tutorial()
            {
                AbsorbSacredTreasureTutorialId = 3300;
                CharacterRankUpTutorialId = 2400;
                ChangeEquipmentTutorialId = 8;
                DungeonBattleTutorialId = 1001;
                EquipmentReinforcementTutorialId = 2100;
                EquipmentTrainingTutorialId = 3400;
                GiveGachaTicketTutorialId = 10;
                LevelLinkTutorialId = 1600;
                LevelUpTutorialId = 7;
                NextChapterTutorialId = 15;
                NextChapterTutorialQuestId = 13;
                ReceiveBountyQuestRewardTutorialId = 1301;
                StartBountyQuestTutorialId = 1300;
                TutorialGachaCaseId = 19;
                EquipmentSynchroRewardMissionTutorialId = 6100;
                EquipmentSynchroUnlockTutorialId = 6101;
            }
        }

        public static class Sns
        {
            // Note: this type is marked as 'beforefieldinit'.
            static Sns()
            {
                FirstSnsShareReward = new UserCurrencyFree(200);
            }

            public static IUserItem FirstSnsShareReward;
        }

        public static class Present
        {
            public static int MaxReceivableCount { get; }

            public static int PresentLimitAddDay { get; }

            static Present()
            {
                MaxReceivableCount = 400;
                PresentLimitAddDay = 30;
            }
        }

        public static class Common
        {
            // Note: this type is marked as 'beforefieldinit'.
            static Common()
            {
                NumberUnitEnUS = new[] { "", "K", "M", "B", "T", "q", "Q", "s" };
                NumberUnitJaJP = new[] { "", "万", "億", "兆", "京", "垓", "秭", "穣" };
                NumberUnitKoKR = new[] { "", "만", "억", "조", "경", "해", "자", "양" };
                NumberUnitZhTW = new[] { "", "萬", "億", "兆", "京", "垓", "秭", "穣" };
                NumberUnitFrFR = new[] { "", "M", "Mn", "Md", "Bn", "Bd", "Tr", "Td" };
                NumberUnitZhCN = new[] { "", "万", "亿", "兆", "京", "垓", "秭", "穰" };
                NumberUnitEsMX = new[] { "", "k", "M", "G", "T", "P", "E", "Z" };
                NumberUnitPtBR = new[] { "", "m", "M", "B", "T", "q", "Q", "s" };
                NumberUnitThTH = new[] { "", "K", "M", "B", "T", "q", "Q", "s" };
                NumberUnitIdID = new[] { "", "rb", "jt", "M", "T", "K", "ku", "S" };
                NumberUnitViVN = new[] { "", "K", "M", "B", "T", "q", "Q", "s" };
                NumberUnitRuRU = new[] { "", "K", "M", "B", "T", "q", "Q", "s" };
                NumberUnitDeDE = new[] { "", "k", "M", "m", "B", "b", "T", "t" };
            }

            public const long GB = 1073741824L;

            public const long MB = 1048576L;

            public static readonly string[] NumberUnitEnUS;

            public static readonly string[] NumberUnitJaJP;

            public static readonly string[] NumberUnitKoKR;

            public static readonly string[] NumberUnitZhTW;

            public static readonly string[] NumberUnitFrFR;

            public static readonly string[] NumberUnitZhCN;

            public static readonly string[] NumberUnitEsMX;

            public static readonly string[] NumberUnitPtBR;

            public static readonly string[] NumberUnitThTH;

            public static readonly string[] NumberUnitIdID;

            public static readonly string[] NumberUnitViVN;

            public static readonly string[] NumberUnitRuRU;

            public static readonly string[] NumberUnitDeDE;

            public static readonly string[] NumberUnitArEG;
        }

        public static class RecommendWorld
        {
            public static long ClearQuestId { get; }

            public static int PlayerCount { get; }

            static RecommendWorld()
            {
                ClearQuestId = 26;
                PlayerCount = 8000;
            }
        }

        public static class Notice
        {
            public static string NoticeBannerImageFileNameFormat { get; }

            // Note: this type is marked as 'beforefieldinit'.
            static Notice()
            {
                NoticeBannerImageFileNameFormat = "NOTICE_BANNER_{0}_{1:D6}.png";
            }
        }

        public static class DebugTool
        {
            public static long DebugLegendLeagueMBId { get; }

            static DebugTool()
            {
                DebugLegendLeagueMBId = 1;
            }
        }

        public static class Addressable
        {
            // Note: this type is marked as 'beforefieldinit'.
            static Addressable()
            {

            }

            public static readonly Dictionary<LanguageType, string> LanguageNameDictionary = new()
            {
                [LanguageType.jaJP] = "JP",
                [LanguageType.enUS] = "US",
                [LanguageType.koKR] = "KR",
                [LanguageType.zhTW] = "TW",
                [LanguageType.frFR] = "FR",
                [LanguageType.zhCN] = "CN",
                [LanguageType.esMX] = "MX",
                [LanguageType.ptBR] = "BR",
                [LanguageType.thTH] = "TH",
                [LanguageType.idID] = "ID",
                [LanguageType.viVN] = "VN",
                [LanguageType.ruRU] = "RU",
                [LanguageType.deDE] = "DE",
            };
        }

        public static class BookSort
        {
            public static int MaxGridCellCount { get; } = 35;

            public static int FloorRewardMaxCount { get; } = 4;
        }
    }
}
