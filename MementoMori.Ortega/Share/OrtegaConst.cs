using MementoMori.Ortega.Share.Data.Equipment;
using MementoMori.Ortega.Share.Data.Item;
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
				// throw new AnalysisFailedException("CPP2IL failed to recover any usable IL for this method.");
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

			public static long MaxDisplayGuildRanking { get; }

			public static long MaxFame { get; }

			public static int MaxGuildMember { get; }

			public static long MaxUserApplyingNum { get; }

			public static long RequiredExpToRemoveMember { get; }

			public static long RequiredRankToJoinGuild { get; }

			// Note: this type is marked as 'beforefieldinit'.
			static Guild()
			{
				// uint num;
				// OrtegaConst.Guild.ItemRequiredToCreateGuild = new UserCurrencyFree((long)num);
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

			public static string HeaderFlagDmmUpdateOneTimeToken { get; }

			public static string HeaderUUID { get; }

			public static string HeaderIpAddress { get; }

			// Note: this type is marked as 'beforefieldinit'.
			static HttpHeaderRequest()
			{
				/*
An exception occurred when decompiling this method

ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void Ortega.Share.OrtegaConst/HttpHeaderRequest::.cctor()

 ---> System.Exception: Basic block has to end with unconditional control flow. 
{
	Block_0:
	callreadonlysetter:string(HttpHeaderRequest::get_HeaderAccessTokenKey, ldstr:string("OrtegaAccessToken"))
	callreadonlysetter:string(HttpHeaderRequest::get_HeaderAppVersionKey, ldstr:string("OrtegaAppVersion"))
	callreadonlysetter:string(HttpHeaderRequest::get_HeaderDeviceType, ldstr:string("OrtegaDeviceType"))
	callreadonlysetter:string(HttpHeaderRequest::get_HeaderDmmOneTimeToken, ldstr:string("OrtegaDmmOneTimeToken"))
	callreadonlysetter:string(HttpHeaderRequest::get_HeaderFlagDmmUpdateOneTimeToken, ldstr:string("OrtegaFlagDmmUpdateOneTimeToken"))
	callreadonlysetter:string(HttpHeaderRequest::get_HeaderUUID, ldstr:string("OrtegaUUID"))
	callreadonlysetter:string(HttpHeaderRequest::get_HeaderIpAddress, ldstr:string("X-Forwarded-For"))
}

   at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.FlattenBasicBlocks(ILNode node) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 1813
   at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.Optimize(DecompilerContext context, ILBlock method, AutoPropertyProvider autoPropertyProvider, StateMachineKind& stateMachineKind, MethodDef& inlinedMethod, AsyncMethodDebugInfo& asyncInfo, ILAstOptimizationStep abortBeforeStep) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 347
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 123
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
   --- End of inner exception stack trace ---
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
   at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1627
*/;
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
				/*
An exception occurred when decompiling this method

ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void Ortega.Share.OrtegaConst/HttpHeaderResponse::.cctor()

 ---> System.Exception: Basic block has to end with unconditional control flow. 
{
	Block_0:
	callreadonlysetter:string(HttpHeaderResponse::get_HeaderAssetVersion, ldstr:string("OrtegaAssetVersion"))
	callreadonlysetter:string(HttpHeaderResponse::get_HeaderMasterVersion, ldstr:string("OrtegaMasterVersion"))
	callreadonlysetter:string(HttpHeaderResponse::get_HeaderNextAccessTokenKey, ldstr:string("OrtegaNextAccessToken"))
	callreadonlysetter:string(HttpHeaderResponse::get_HeaderStatusCodeKey, ldstr:string("OrtegaStatusCode"))
	callreadonlysetter:string(HttpHeaderResponse::get_HeaderUtcNowTimeStamp, ldstr:string("OrtegaUtcNowTimeStamp"))
}

   at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.FlattenBasicBlocks(ILNode node) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 1813
   at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.Optimize(DecompilerContext context, ILBlock method, AutoPropertyProvider autoPropertyProvider, StateMachineKind& stateMachineKind, MethodDef& inlinedMethod, AsyncMethodDebugInfo& asyncInfo, ILAstOptimizationStep abortBeforeStep) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 347
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 123
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
   --- End of inner exception stack trace ---
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
   at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1627
*/;
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

			public static long RequiredCurrencyChangeUserName { get; }

			// Note: this type is marked as 'beforefieldinit'.
			static User()
			{
				// throw new AnalysisFailedException("CPP2IL failed to recover any usable IL for this method.");
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

			public static Dictionary<BattleType, int> MaxTurn { get; } = new ();

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
				// ulong num;
				// OrtegaConst.Battle.NpcPlayerId = num;
				// ulong num2;
				// OrtegaConst.Battle.OneDarkElementBonus = new BattleParameterChangeInfo
				// {
				// 	BattleParameterType = (BattleParameterType)((ulong)13L),
				// 	Value = (double)num2,
				// 	ChangeParameterType = (ChangeParameterType)((ulong)2L)
				// };
				// OrtegaConst.Battle.ResultAnimationKeyLoseAnnihilationStart = "Lose-Annihilation-Start";
				// OrtegaConst.Battle.ResultAnimationKeyLoseOutOfTurnsStart = "Lose-OutOfTurns-Start";
				// OrtegaConst.Battle.ResultAnimationKeyWin = "Win-Start";
				// ulong num3;
				// OrtegaConst.Battle.ThreeDarkElementBonus = new BattleParameterChangeInfo
				// {
				// 	BattleParameterType = (BattleParameterType)((ulong)2L),
				// 	Value = (double)num3,
				// 	ChangeParameterType = (ChangeParameterType)((ulong)2L)
				// };
				// OrtegaConst.Battle.ThreeDefaultElementAndAnotherTwoElementBonus1 = new BattleParameterChangeInfo
				// {
				// 	BattleParameterType = (BattleParameterType)((ulong)2L),
				// 	ChangeParameterType = (ChangeParameterType)((ulong)2L),
				// 	Value = (double)num3
				// };
				// OrtegaConst.Battle.ThreeDefaultElementAndAnotherTwoElementBonus2 = new BattleParameterChangeInfo
				// {
				// 	BattleParameterType = (BattleParameterType)((ulong)1L),
				// 	ChangeParameterType = (ChangeParameterType)((ulong)2L),
				// 	Value = (double)num3
				// };
				// OrtegaConst.Battle.ThreeDefaultElementBonus1 = new BattleParameterChangeInfo
				// {
				// 	BattleParameterType = (BattleParameterType)((ulong)2L),
				// 	ChangeParameterType = (ChangeParameterType)((ulong)2L),
				// 	Value = (double)num3
				// };
				// OrtegaConst.Battle.ThreeDefaultElementBonus2 = new BattleParameterChangeInfo
				// {
				// 	BattleParameterType = (BattleParameterType)((ulong)1L),
				// 	ChangeParameterType = (ChangeParameterType)((ulong)2L),
				// 	Value = (double)num3
				// };
				// ulong num4;
				// OrtegaConst.Battle.TwoDarkElementBonus = new BattleParameterChangeInfo
				// {
				// 	BattleParameterType = (BattleParameterType)((ulong)7L),
				// 	Value = (double)num4,
				// 	ChangeParameterType = (ChangeParameterType)((ulong)2L)
				// };
				// throw new NullReferenceException();
			}

			public static int MaxSkillTurn;
		}

		public static class Skill
		{
			public static int TeamPassiveGuid { get; }

			// Note: this type is marked as 'beforefieldinit'.
			static Skill()
			{
				// throw new AnalysisFailedException("CPP2IL failed to recover any usable IL for this method.");
			}

			public static List<EffectType> AdvantageGroupBuff;

			public static List<EffectType> ConfuesActionGroupDebuff;

			public static List<EffectType> DamageReflectEnhanceGroup;

			public static List<EffectType> LockOnGroup;

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
				// throw new AnalysisFailedException("CPP2IL failed to recover any usable IL for this method.");
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

			public static int MinBattleEfficiency;
		}

		public static class BattleBoss
		{
			public static long ClearPartyLogChapterId { get; }

			public static long DefaultQuestId { get; }

			public static long MaxBossBattleFreeCount { get; } = 3;
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
				// throw new AnalysisFailedException("CPP2IL failed to recover any usable IL for this method.");
			}
		}

		public static class Deck
		{
			public static int MaxCount { get; }
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
				// throw new AnalysisFailedException("CPP2IL failed to recover any usable IL for this method.");
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

			public static long ResetLevelRequiredCurrency { get; }

			public static long ResetRankRequiredCurrency { get; }

			public static Dictionary<ElementType, long> ReturnWitchLetterIdDict { get; }

			// Note: this type is marked as 'beforefieldinit'.
			static Character()
			{
				// throw new AnalysisFailedException("CPP2IL failed to recover any usable IL for this method.");
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
				throw new NullReferenceException();
			}

			public static Dictionary<long, CharacterRarityFlags> CharacterCollectionBonusMaxRarityDict = new();
		}

		public static class CharacterShardReversion
		{
			public static Dictionary<CharacterRarityFlags, int> CharacterFragmentCountDict { get; } = new();

			public static CharacterRarityFlags UnlockRarity { get; }
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

			public static int MaxLevelDifferenceThatCanBeEquipped { get; }

			public static double MaxRateBaseParameterDefault { get; }

			public static double MaxRateBaseParameterTraining { get; }

			public static int MaxUnlockSphereSlot { get; }

			public static List<IUserItem> RequiredItemListToLockWithTraining { get; }

			public static List<IUserItem> RequiredItemListToUnlockSphereSlot { get; }

			// Note: this type is marked as 'beforefieldinit'.
			static Equipment()
			{
				// throw new AnalysisFailedException("CPP2IL failed to recover any usable IL for this method.");
			}

			public static readonly List<EquipmentRarityFlags> ActiveExclusiveSkillRaritys = new() {EquipmentRarityFlags.SSR, EquipmentRarityFlags.UR, EquipmentRarityFlags.LR};

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
			public static int SkillCoolDownForRelay;
		}

		public static class Gvg
		{
			public static int DefaultActionPoint { get; }

			public static int DisplayBattleDialogPartyCount { get; }

			public static double ValidJoinedGuildSeconds = (double)((ulong)4680673776000565248L);
		}

		public static class LocalGvg
		{
			public static int AddCounterMilliseconds { get; }

			public static int EndDeclarationHour { get; }

			public static int EndDeclarationMinute { get; }

			public static int EndHour { get; }

			public static int EndMinute { get; }

			public static int MaxCharacterNum { get; }

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
				// string[] array2;
				// for (;;)
				// {
				// 	string[] array = new string[4];
				// 	if ("[GvgGroup1NameLabel]" == 0 || "[GvgGroup1NameLabel]" != 0)
				// 	{
				// 		array[0] = "[GvgGroup1NameLabel]";
				// 		if ("[GvgGroup2NameLabel]" == 0 || "[GvgGroup2NameLabel]" != 0)
				// 		{
				// 			array[1] = "[GvgGroup2NameLabel]";
				// 			if ("[GvgGroup3NameLabel]" == 0 || "[GvgGroup3NameLabel]" != 0)
				// 			{
				// 				array[2] = "[GvgGroup3NameLabel]";
				// 				if ("[GvgGroup4NameLabel]" == 0 || "[GvgGroup4NameLabel]" != 0)
				// 				{
				// 					array[3] = "[GvgGroup4NameLabel]";
				// 					OrtegaConst.GlobalGvg.GroupNameKey = array;
				// 					array2 = new string[3];
				// 					if ("[GvgGroupLevelNameBronzeLabel]" == 0 || "[GvgGroupLevelNameBronzeLabel]" != 0)
				// 					{
				// 						array2[0] = "[GvgGroupLevelNameBronzeLabel]";
				// 						if ("[GvgGroupLevelNameSilverLabel]" == 0 || "[GvgGroupLevelNameSilverLabel]" != 0)
				// 						{
				// 							array2[1] = "[GvgGroupLevelNameSilverLabel]";
				// 							if ("[GvgGroupLevelNameGoldenLabel]" == 0 || "[GvgGroupLevelNameGoldenLabel]" != 0)
				// 							{
				// 								break;
				// 							}
				// 						}
				// 					}
				// 				}
				// 			}
				// 		}
				// 	}
				// }
				// array2[2] = "[GvgGroupLevelNameGoldenLabel]";
				// OrtegaConst.GlobalGvg.GlobalGvgGroupTypeNameKey = array2;
			}

			public static readonly string[] GroupNameKey;

			public static readonly string[] GlobalGvgGroupTypeNameKey;
		}

		public static class Friend
		{
			public static int AcquisitionFriendPointPerFriend { get; } = 5;

			public static int MaxApplyingNum { get; } = 1000;

			public static int MaxApprovalPendingNum { get; } = 90;

			public static int MaxBlockNum { get; } = 100;

			public static int MaxDailyReceiveFriendPoint { get; } = 20;

			public static int MaxFriendNum { get; } = 40;

			public static int MaxFriendPoint { get; } = 9999;

			public static long UsableFriendCodeTime { get; } = 604800000;

			public static long RecommendFriendDisplayNum = 20;
		}

		public static class MyPageBanner
		{
			public static int MaxDisplayBannerNum { get; }
		}

		public static class PlayerInfo
		{
			public static int MaxPlayerInfoInPage { get; }
		}

		public static class Ranking
		{
			public static int DisplayRankingCount { get; }
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

			// Note: this type is marked as 'beforefieldinit'.
			static Mission()
			{
				// throw new AnalysisFailedException("CPP2IL failed to recover any usable IL for this method.");
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

			// public static UserExchangePlaceItem RequiredItemForExchangeInProduct { get; }

			// public static List<UserExchangePlaceItem> RequiredItemsForExchangeInFame { get; }

			public static int MaxGuerrillaPackCount { get; }

			public static long CanBuyPrePurchasedMonthlyBoostTime { get; }

			// Note: this type is marked as 'beforefieldinit'.
			static Shop()
			{
				// ulong num;
				// OrtegaConst.Shop.PaidDMMCurrencyLimitDateTime = num;
				// uint num2;
				// OrtegaConst.Shop.RequiredItemForExchangeInProduct = new UserExchangePlaceItem(ExchangePlaceItemType.CastingValue, (long)num2);
				// List<UserExchangePlaceItem> list = new();
				// uint num3;
				// UserExchangePlaceItem userExchangePlaceItem = new UserExchangePlaceItem(ExchangePlaceItemType.Fame, (long)num3);
				// list.Add(userExchangePlaceItem);
				// uint num4;
				// UserExchangePlaceItem userExchangePlaceItem2 = new UserExchangePlaceItem(ExchangePlaceItemType.CastingValue, (long)num4);
				// list.Add(userExchangePlaceItem2);
				// OrtegaConst.Shop.RequiredItemsForExchangeInFame = list;
				// OrtegaConst.Shop.CanBuyPrePurchasedMonthlyBoostTime = 0;
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
				// uint num;
				// TimeSpan timeSpan;
				// uint num2;
				// TimeSpan timeSpan2;
				// uint num3;
				// TimeSpan timeSpan3;
				// uint num4;
				// TimeSpan timeSpan4;
				// uint num5;
				// TimeSpan timeSpan5;
				// uint num6;
				// TimeSpan timeSpan6;
				// uint num7;
				// TimeSpan timeSpan7;
				// uint num8;
				// TimeSpan timeSpan8;
				// uint num9;
				// TimeSpan timeSpan9;
				// uint num10;
				// TimeSpan timeSpan10;
				// uint num11;
				// TimeSpan timeSpan11;
				// uint num12;
				// TimeSpan timeSpan12;
				// uint num13;
				// TimeSpan timeSpan13;
				// OrtegaConst.BountyQuest.GuerrillaQuestTime = new Dictionary
				// {
				// 	{ num, timeSpan },
				// 	{ num2, timeSpan2 },
				// 	{ num3, timeSpan3 },
				// 	{ num4, timeSpan4 },
				// 	{ num5, timeSpan5 },
				// 	{ num6, timeSpan6 },
				// 	{ num7, timeSpan7 },
				// 	{ num8, timeSpan8 },
				// 	{ num9, timeSpan9 },
				// 	{ num10, timeSpan10 },
				// 	{ num11, timeSpan11 },
				// 	{ num12, timeSpan12 },
				// 	{ num13, timeSpan13 }
				// };
			}
		}

		public static class GuildRaid
		{
			public static int AttackRepeatTime { get; }

			public static long AutoJoinCost { get; }

			public static long BattleTime { get; }

			public static long GuildRaidCanJoinDelayTime { get; } = 0;

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
				// double num;
				// TimeSpan timeSpan = TimeSpan.FromHours(num);
				// OrtegaConst.GuildRaid.GuildRaidOpeningTime = 0;
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

			public static int ResetCoolTimeCurrency { get; } = 100;

		}

		public static class Map
		{
			public static int DisplayMaxQuestCount { get; }

			public static List<UserItem> FirstMapBuildingRewardItems { get; }

			public static int OtherPlayerCount { get; }

			// Note: this type is marked as 'beforefieldinit'.
			static Map()
			{
				// List<UserItem> list = new();
				// uint num;
				// UserItem userItem = new UserGold((long)num).ToUserItem();
				// list.Add(userItem);
				// UserCharacterTrainingMaterial userCharacterTrainingMaterial;
				// UserItem userItem2 = userCharacterTrainingMaterial.ToUserItem();
				// list.Add(userItem2);
				// OrtegaConst.Map.FirstMapBuildingRewardItems = list;
			}
		}

		public static class ClearParty
		{
			public static int ListLimitCount { get; }

			public static int MaxSubDay { get; }

			public static int StartSubDay { get; }
		}

		public static class LocalRaid
		{
			public static int MaxLevel { get; }

			public static int MinLevel { get; }

			public static int RoomExpire { get; }

			public static long BatchStartTime;

			public static long BatchEndTime;
		}

		public static class StateBonus
		{
			public static List<DayOfWeek> AutoQuickBonusDayOfWeeks { get; }

			public static long DailyBonusStateId { get; }

			public static int DailyWeeklyBonus { get; }

			// Note: this type is marked as 'beforefieldinit'.
			static StateBonus()
			{
				// throw new AnalysisFailedException("CPP2IL failed to recover any usable IL for this method.");
			}
		}

		public static class Tutorial
		{
			public static long AbsorbSacredTreasureTutorialId { get; }

			public static long CharacterRankUpTutorialId { get; }

			public static long ChangeEquipmentTutorialId { get; }

			public static long EquipmentReinforcementTutorialId { get; }

			public static long EquipmentTrainingTutorialId { get; }

			public static int GiveGachaTicketTutorialId { get; }

			public static long LevelLinkTutorialId { get; }

			public static long LevelUpTutorialId { get; }

			public static long NextChapterTutorialId { get; }

			public static long NextChapterTutorialQuestId { get; }

			public static long ReceiveBountyQuestRewardTutorialId { get; }

			public static long StartBountyQuestTutorialId { get; }

			public static long TutorialGachaCaseId { get; }
		}

		public static class Sns
		{
			// Note: this type is marked as 'beforefieldinit'.
			static Sns()
			{
				// UserCurrencyFree userCurrencyFree;
				// OrtegaConst.Sns.FirstSnsShareReward = userCurrencyFree;
				// throw new NullReferenceException();
			}

			public static IUserItem FirstSnsShareReward;
		}

		public static class Present
		{
			public static int MaxReceivableCount { get; }

			public static int PresentLimitAddDay { get; }
		}

		public static class Common
		{
			// Note: this type is marked as 'beforefieldinit'.
			static Common()
			{
				/*
An exception occurred when decompiling this method

ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void Ortega.Share.OrtegaConst/Common::.cctor()

 ---> System.Exception: Basic block has to end with unconditional control flow. 
{
	IL_735:
	stelem:string([mscorlib]System.String, ldloc:string[](var_13_640), ldc.i4:int32(7), ldstr:string("Z"))
	stsfld:string[](Common::NumberUnitEsMX, ldloc:string[](var_13_640))
}

   at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.FlattenBasicBlocks(ILNode node) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 1813
   at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.Optimize(DecompilerContext context, ILBlock method, AutoPropertyProvider autoPropertyProvider, StateMachineKind& stateMachineKind, MethodDef& inlinedMethod, AsyncMethodDebugInfo& asyncInfo, ILAstOptimizationStep abortBeforeStep) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 347
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 123
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
   --- End of inner exception stack trace ---
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
   at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1627
*/;
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
		}

		public static class Notice
		{
			public static string NoticeBannerImageFileNameFormat { get; }

			// Note: this type is marked as 'beforefieldinit'.
			static Notice()
			{
				/*
An exception occurred when decompiling this method

ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void Ortega.Share.OrtegaConst/Notice::.cctor()

 ---> System.Exception: Basic block has to end with unconditional control flow. 
{
	Block_0:
	callreadonlysetter:string(Notice::get_NoticeBannerImageFileNameFormat, ldstr:string("NOTICE_BANNER_{0}_{1:D6}.png"))
}

   at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.FlattenBasicBlocks(ILNode node) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 1813
   at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.Optimize(DecompilerContext context, ILBlock method, AutoPropertyProvider autoPropertyProvider, StateMachineKind& stateMachineKind, MethodDef& inlinedMethod, AsyncMethodDebugInfo& asyncInfo, ILAstOptimizationStep abortBeforeStep) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 347
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 123
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
   --- End of inner exception stack trace ---
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
   at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1627
*/;
			}
		}

		public static class DebugTool
		{
			public static long DebugLegendLeagueMBId { get; }
		}

		public static class Addressable
		{
			// Note: this type is marked as 'beforefieldinit'.
			static Addressable()
			{
				
			}

			public static readonly Dictionary<LanguageType, string> LanguageNameDictionary = new ()
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
	}
}
