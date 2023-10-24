using MementoMori.Common;
using MementoMori.Ortega.Common.Enums;
using MementoMori.Ortega.Share;
using MementoMori.Ortega.Share.Data.ApiInterface.Battle;
using MementoMori.Ortega.Share.Data.Battle;
using MementoMori.Ortega.Share.Data.Battle.Result;
using MementoMori.Ortega.Share.Data.DtoInfo;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Data;
using MementoMori.Ortega.Share.Master.Table;
using MementoMori.Ortega.Share.Utils;

namespace MementoMori.Ortega.Common.Utils
{
	public static class BattleUtil
	{
// 		public static void SetBossBattleChallengeResponse(BossResponse response)
// 		{
// 			if (response.BattleResult.SimulationResult.BattleEndInfo.IsWinAttacker())
// 			{
// 				AppsFlyerManager instance = SingletonMonoBehaviour.Instance;
// 				long QuestId = response.BattleResult.QuestId;
// 				instance.CheckAndSendQuestClearEvent(QuestId);
// 			}
// 			UserDataManager instance2 = SingletonMonoBehaviour.Instance;
// 			BattleRewardResult BattleRewardResult = response.BattleRewardResult;
// 			instance2.UpdateUppedRank(BattleRewardResult);
// 		}
//
// 		public static bool IsRemainingBossChallenge(long questId)
// 		{
// 			long clearedAutoBattleMaxQuestId = SingletonMonoBehaviour.Instance.GetClearedAutoBattleMaxQuestId();
// 			long remainingBossChallengeCount = BattleUtil.GetRemainingBossChallengeCount(SingletonMonoBehaviour.Instance.SyncData.UserBattleBossDtoInfo);
// 			bool flag;
// 			return flag;
// 		}
//
// 		public static long GetRemainingChallengeCount(BattleType battleType, TowerType towerType = TowerType.Infinite)
// 		{
// 			return BattleUtil.GetRemainingBossChallengeCount(SingletonMonoBehaviour.Instance.SyncData.UserBattleBossDtoInfo);
// 		}
//
		// public static long GetRemainingBossChallengeCount(UserBattleBossDtoInfo dtoInfo)
		// {
		// 	if (dtoInfo != null)
		// 	{
		// 		int dateIntYearMonthDay = DateUtil.GetDateIntYearMonthDay(dtoInfo.BossLastChallengeTime);
		// 		int dateIntYearMonthDay2 = Services.Get<OrtegaTimeManager>().GetDateIntYearMonthDay();
		//
		// 		if (dateIntYearMonthDay2 > dateIntYearMonthDay)
		// 		{
		// 			return OrtegaConst.BattleBoss.MaxBossBattleFreeCount;
		// 		}
		// 		else
		// 		{
		// 			return OrtegaConst.BattleBoss.MaxBossBattleFreeCount - dtoInfo.BossTodayWinCount;
		// 		}
		// 		
		// 		// long BossTodayUseCurrencyCount = dtoInfo.BossTodayUseCurrencyCount;
		// 		// long BossTodayUseTicketCount = dtoInfo.BossTodayUseTicketCount;
		// 		// long num = OrtegaConst.BattleBoss.MaxBossBattleFreeCount;
		// 		// long BossLastChallengeTime = dtoInfo.BossLastChallengeTime;
		// 		// num += BossTodayUseTicketCount;
		// 		// num += BossTodayUseCurrencyCount;
		// 		// int dateIntYearMonthDay = DateUtil.GetDateIntYearMonthDay(BossLastChallengeTime);
		// 		// int dateIntYearMonthDay2 = Services.Get<OrtegaTimeManager>().GetDateIntYearMonthDay();
		// 		// return num;
		// 	}
		// 	return OrtegaConst.BattleBoss.MaxBossBattleFreeCount;
		// }

// 		public static AutoBattleQuickBonusConditionType GetAutoQuickBonusConditionType(long questId)
// 		{
// 			QuestMB byId = Masters.QuestTable.GetById(questId);
// 			if (byId != 0)
// 			{
// 				ChapterTable ChapterTable = Masters.ChapterTable;
// 				long ChapterId = byId.ChapterId;
// 				ChapterMB byId2 = ChapterTable.GetById(ChapterId);
// 				if (byId2 != 0)
// 				{
// 					long StateId = byId2.StateId;
// 					bool flag2;
// 					bool flag = flag2 + true;
// 				}
// 			}
// 			throw new NullReferenceException();
// 		}
//
// 		public static bool IsAutoQuickBonusDay(long questId)
// 		{
// 			QuestMB byId = Masters.QuestTable.GetById(questId);
// 			if (byId != 0)
// 			{
// 				ChapterTable ChapterTable = Masters.ChapterTable;
// 				long ChapterId = byId.ChapterId;
// 				ChapterMB byId2 = ChapterTable.GetById(ChapterId);
// 				if (byId2 != 0)
// 				{
// 					long StateId = byId2.StateId;
// 					ListDayOfWeek> <AutoQuickBonusDayOfWeeks = OrtegaConst.StateBonus.AutoQuickBonusDayOfWeeks;
// 					DateTime localGameDate = TimeManager.Instance.GetLocalGameDate();
// 					bool flag;
// 					return flag;
// 				}
// 			}
// 			throw new NullReferenceException();
// 		}
//
// 		public static string GetPlayerName(BattleFieldCharacter battleFieldCharacter)
// 		{
// 			long OwnerPlayerId = battleFieldCharacter.OwnerPlayerId;
// 			return battleFieldCharacter.PlayerName;
// 		}
//
// 		public static string GetPlayerName(BattleReportData battleReportData)
// 		{
// 			long ownerPlayerId = battleReportData.OwnerPlayerId;
// 			return battleReportData.PlayerName;
// 		}
//
// 		public static string GetPlayerName(BattleCharacterReport battleCharacterReport)
// 		{
// 			long OwnerPlayerId = battleCharacterReport.OwnerPlayerId;
// 			return battleCharacterReport.PlayerName;
// 		}
//
// 		public static string GetPlayerName(BattleDeckDetailData battleDeckDetailData)
// 		{
// 			long ownerPlayerId = battleDeckDetailData.OwnerPlayerId;
// 			return battleDeckDetailData.PlayerName;
// 		}
//
// 		public static bool IsLastAutoBattleQuestInChapter(long questId)
// 		{
// 			QuestMB byId = Masters.QuestTable.GetById(questId);
// 			QuestTable QuestTable = Masters.QuestTable;
// 			long num = questId + 1L;
// 			QuestMB byId2 = QuestTable.GetById(num);
// 			if (byId2 != 0)
// 			{
// 				long ChapterId = byId2.ChapterId;
// 				return byId.ChapterId != ChapterId;
// 			}
// 			return true;
// 		}
//
// 		public static int GetTerritoryLevel(long questId)
// 		{
// 			int num = 0;
// 			QuestMB byId = Masters.QuestTable.GetById(questId);
// 			if (byId != 0)
// 			{
// 				QuestTable QuestTable = Masters.QuestTable;
// 				long ChapterId = byId.ChapterId;
// 				ListQuestMB> listByChapterId = <QuestTable.GetListByChapterId(ChapterId);
// 				int size = listByChapterId._size;
// 				QuestMB questMB = listByChapterId[size];
// 				if (listByChapterId[size].QuestDifficultyType == QuestDifficultyType.Hard)
// 				{
// 					num++;
// 				}
// 				return num;
// 			}
// 			throw new NullReferenceException();
// 		}
//
// 		public static int GetMaxTerritoryLevel(long questId)
// 		{
// 			int num;
// 			do
// 			{
// 				num = 0;
// 				QuestMB byId = Masters.QuestTable.GetById(questId);
// 				if (byId == 0)
// 				{
// 					goto IL_37;
// 				}
// 				QuestTable QuestTable = Masters.QuestTable;
// 				long ChapterId = byId.ChapterId;
// 				ListQuestMB> listByChapterId = <QuestTable.GetListByChapterId(ChapterId);
// 				bool flag;
// 				if (flag)
// 				{
// 					num++;
// 				}
// 			}
// 			while (num != 0);
// 			return num;
// 			IL_37:
// 			throw new NullReferenceException();
// 		}
//
// 		public static bool IsLastQuestInState(long questId)
// 		{
// 			QuestMB byId = Masters.QuestTable.GetById(questId);
// 			if (byId != 0)
// 			{
// 				ChapterTable ChapterTable = Masters.ChapterTable;
// 				long ChapterId = byId.ChapterId;
// 				ChapterMB byId2 = ChapterTable.GetById(ChapterId);
// 				if (byId2 != 0)
// 				{
// 					QuestTable QuestTable = Masters.QuestTable;
// 					long num = questId + 1L;
// 					QuestMB byId3 = QuestTable.GetById(num);
// 					if (byId3 != 0)
// 					{
// 						long ChapterId2 = byId3.ChapterId;
// 						ChapterMB chapterMB;
// 						if (chapterMB != 0)
// 						{
// 							long StateId = chapterMB.StateId;
// 							return byId2.StateId != StateId;
// 						}
// 					}
// 				}
// 			}
// 			throw new NullReferenceException();
// 		}
//
// 		public static BgmType GetBattleBgmType(BattleType battleType, bool isQlipha)
// 		{
// 			/*
// An exception occurred when decompiling this method
//
// ICSharpCode.Decompiler.DecompilerException: Error decompiling Ortega.Common.Enums.BgmType Ortega.Common.Utils.BattleUtil::GetBattleBgmType(Ortega.Share.Enums.BattleType,System.Boolean)
//
//  ---> System.Exception: Basic block has to end with unconditional control flow. 
// {
// 	IL_00:
// 	brtrue(IL_00, ceq:bool(ldloc:BattleType(battleType), ldc.i4:BattleType(1)))
// }
//
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.FlattenBasicBlocks(ILNode node) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 1813
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.FlattenBasicBlocks(ILNode node) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 1839
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.FlattenBasicBlocks(ILNode node) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 1839
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.FlattenBasicBlocks(ILNode node) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 1807
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.Optimize(DecompilerContext context, ILBlock method, AutoPropertyProvider autoPropertyProvider, StateMachineKind& stateMachineKind, MethodDef& inlinedMethod, AsyncMethodDebugInfo& asyncInfo, ILAstOptimizationStep abortBeforeStep) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 347
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 123
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
//    --- End of inner exception stack trace ---
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
//    at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1627
// */;
// 		}
//
// 		public static int GetBattleResourceId(BattleType battleType, bool isQlipha)
// 		{
// 			/*
// An exception occurred when decompiling this method
//
// ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Int32 Ortega.Common.Utils.BattleUtil::GetBattleResourceId(Ortega.Share.Enums.BattleType,System.Boolean)
//
//  ---> System.Exception: Basic block has to end with unconditional control flow. 
// {
// 	Block_0:
// 	brtrue(IL_00, cne:bool(ldloc:bool(isQlipha), ldc.i4:bool(0)))
// }
//
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.FlattenBasicBlocks(ILNode node) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 1813
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.FlattenBasicBlocks(ILNode node) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 1839
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.FlattenBasicBlocks(ILNode node) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 1839
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.FlattenBasicBlocks(ILNode node) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 1807
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.FlattenBasicBlocks(ILNode node) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 1839
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.FlattenBasicBlocks(ILNode node) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 1839
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.FlattenBasicBlocks(ILNode node) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 1807
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.Optimize(DecompilerContext context, ILBlock method, AutoPropertyProvider autoPropertyProvider, StateMachineKind& stateMachineKind, MethodDef& inlinedMethod, AsyncMethodDebugInfo& asyncInfo, ILAstOptimizationStep abortBeforeStep) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 347
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 123
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
//    --- End of inner exception stack trace ---
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
//    at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1627
// */;
// 		}
//
// 		public static int GetAutoBattleQuestRewardAmount(ItemType itemType, long itemId, long questId)
// 		{
// 			QuestMB byId = Masters.QuestTable.GetById(itemId);
// 			if (byId != 0)
// 			{
// 				if (itemType != ItemType.PlayerExp)
// 				{
// 					int PotentialJewelPerDay;
// 					if (itemType == ItemType.CharacterTrainingMaterial)
// 					{
// 						PotentialJewelPerDay = byId.PotentialJewelPerDay;
// 						return PotentialJewelPerDay;
// 					}
// 					return PotentialJewelPerDay;
// 				}
// 				else
// 				{
// 					ulong num;
// 					num += num;
// 				}
// 			}
// 			return 0;
// 		}
//
// 		public static string GetAutoBattleItemKeyFormat(ItemType itemType, long itemId)
// 		{
// 			if (itemType == ItemType.CharacterTrainingMaterial)
// 			{
// 				return "[CommonPlusWithPerDayFormat]";
// 			}
// 			if (itemType != ItemType.Gold)
// 			{
// 				return "[CommonPlusWithPerHourFormat]";
// 			}
// 			return "[CommonPlusWithPerMinuteFormat]";
// 		}
//
// 		public static long GetAutoBattleQuickRequiredCount(long selectedCount)
// 		{
// 			int num = 0;
// 			BattleManager instance = SingletonMonoBehaviour.Instance;
// 			int num2 = 0;
// 			long quickAutoBattleCount = instance.GetQuickAutoBattleCount((QuestQuickExecuteType)num2);
// 			RequiredCurrencyTable RequiredCurrencyTable = Masters.RequiredCurrencyTable;
// 			num++;
// 			num = (int)((long)num + quickAutoBattleCount);
// 			if (RequiredCurrencyTable.GetByCount((long)num) != 0)
// 			{
// 			}
// 			num++;
// 			throw new NullReferenceException();
// 		}
	}
}
