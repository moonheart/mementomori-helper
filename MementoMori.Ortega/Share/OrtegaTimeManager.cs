using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Data;
using MementoMori.Ortega.Share.Utils;

namespace MementoMori.Ortega.Share
{
	public class OrtegaTimeManager : ILocalTime
	{
		// public OrtegaTimeManager()
		// {
		// 	this.DifferenceFromUtc = (long)0L;
		// }

		public OrtegaTimeManager(TimeServerMB timeServerMB)
		{
			TimeSpan timeSpan = TimeSpan.Parse(timeServerMB.DifferenceDateTimeFromUtc);
			this.DifferenceFromUtc = (long) timeSpan.TotalMilliseconds;
		}

		public long DifferenceFromUtc { get; private set; }

// 		public void SetDifferenceFromUtc(TimeServerMB timeServerMB)
// 		{
// 			if (!string.IsNullOrEmpty(timeServerMB.DifferenceDateTimeFromUtc))
// 			{
// 				TimeSpan timeSpan = TimeSpan.Parse(timeServerMB.DifferenceDateTimeFromUtc);
// 				this.DifferenceFromUtc = timeSpan;
// 			}
// 		}
//
		public long GetLocalTimestamp()
		{
			return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + this.DifferenceFromUtc;
		}
//
// 		public DateTime GetLocalDateTime()
// 		{
// 			DateTime UtcEpoch = TimeUtil.UtcEpoch;
// 			long localTimestamp = this.GetLocalTimestamp();
// 			DateTime dateTime;
// 			return dateTime;
// 		}
//
// 		public long GetNDaysLaterChangeDayTimeStamp(long nDay, long timestamp = -1L)
// 		{
// 			/*
// An exception occurred when decompiling this method
//
// ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Int64 Ortega.Share.OrtegaTimeManager::GetNDaysLaterChangeDayTimeStamp(System.Int64,System.Int64)
//
//  ---> System.Exception: Basic block has to end with unconditional control flow. 
// {
// 	Block_0:
// 	stloc:DateTime(var_1_05, ldsfld:DateTime(TimeUtil::UtcEpoch))
// 	stloc:int64(var_2_0C, call:int64(OrtegaTimeManager::GetLocalTimestamp, ldloc:OrtegaTimeManager(this)))
// 	stloc:DateTime(var_3_12, ldsfld:DateTime(TimeUtil::UtcEpoch))
// 	stloc:TimeSpan(var_6_1B, ldsfld:TimeSpan(TimeUtil::ChangeDayTime))
// 	stloc:DateTime(var_7_26, call:DateTime(DateTime::op_Subtraction, ldloc:DateTime(var_5), ldloc:TimeSpan(var_6_1B)))
// 	stloc:TimeSpan(var_14_30, ldsfld:TimeSpan(TimeUtil::ChangeDayTime))
// 	stloc:DateTime(var_15_3B, call:DateTime(DateTime::op_Addition, ldloc:DateTime(var_13), ldloc:TimeSpan(var_14_30)))
// 	stloc:DateTime(var_16_42, ldsfld:DateTime(TimeUtil::UtcEpoch))
// 	stloc:TimeSpan(var_17_4D, call:TimeSpan(DateTime::op_Subtraction, ldloc:DateTime(var_15_3B), ldloc:DateTime(var_16_42)))
// }
//
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.FlattenBasicBlocks(ILNode node) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 1813
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.Optimize(DecompilerContext context, ILBlock method, AutoPropertyProvider autoPropertyProvider, StateMachineKind& stateMachineKind, MethodDef& inlinedMethod, AsyncMethodDebugInfo& asyncInfo, ILAstOptimizationStep abortBeforeStep) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 347
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 123
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
//    --- End of inner exception stack trace ---
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
//    at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1627
// */;
// 		}
//
// 		public long GetLocalYesterdayChangeDayTimeStamp()
// 		{
// 			/*
// An exception occurred when decompiling this method
//
// ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Int64 Ortega.Share.OrtegaTimeManager::GetLocalYesterdayChangeDayTimeStamp()
//
//  ---> System.Exception: Basic block has to end with unconditional control flow. 
// {
// 	Block_0:
// 	stloc:DateTime(var_1_06, call:DateTime(OrtegaTimeManager::GetLocalDateTime, ldloc:OrtegaTimeManager(this)))
// 	stloc:TimeSpan(var_2_0C, ldsfld:TimeSpan(TimeUtil::ChangeDayTime))
// 	stloc:DateTime(var_3_14, call:DateTime(DateTime::op_Subtraction, ldloc:DateTime(var_1_06), ldloc:TimeSpan(var_2_0C)))
// 	stloc:TimeSpan(var_9_1A, ldsfld:TimeSpan(TimeUtil::ChangeDayTime))
// 	stloc:DateTime(var_10_25, call:DateTime(DateTime::op_Addition, ldloc:DateTime(var_8), ldloc:TimeSpan(var_9_1A)))
// 	stloc:DateTime(var_11_2C, ldsfld:DateTime(TimeUtil::UtcEpoch))
// 	stloc:TimeSpan(var_12_37, call:TimeSpan(DateTime::op_Subtraction, ldloc:DateTime(var_10_25), ldloc:DateTime(var_11_2C)))
// }
//
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.FlattenBasicBlocks(ILNode node) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 1813
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.Optimize(DecompilerContext context, ILBlock method, AutoPropertyProvider autoPropertyProvider, StateMachineKind& stateMachineKind, MethodDef& inlinedMethod, AsyncMethodDebugInfo& asyncInfo, ILAstOptimizationStep abortBeforeStep) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 347
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 123
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
//    --- End of inner exception stack trace ---
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
//    at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1627
// */;
// 		}
//
// 		public long GetLocalTomorrowChangeDayTimeStamp()
// 		{
// 			/*
// An exception occurred when decompiling this method
//
// ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Int64 Ortega.Share.OrtegaTimeManager::GetLocalTomorrowChangeDayTimeStamp()
//
//  ---> System.Exception: Basic block has to end with unconditional control flow. 
// {
// 	Block_0:
// 	stloc:DateTime(var_1_06, call:DateTime(OrtegaTimeManager::GetLocalDateTime, ldloc:OrtegaTimeManager(this)))
// 	stloc:TimeSpan(var_2_0C, ldsfld:TimeSpan(TimeUtil::ChangeDayTime))
// 	stloc:DateTime(var_3_14, call:DateTime(DateTime::op_Subtraction, ldloc:DateTime(var_1_06), ldloc:TimeSpan(var_2_0C)))
// 	stloc:TimeSpan(var_9_1A, ldsfld:TimeSpan(TimeUtil::ChangeDayTime))
// 	stloc:DateTime(var_10_25, call:DateTime(DateTime::op_Addition, ldloc:DateTime(var_8), ldloc:TimeSpan(var_9_1A)))
// 	stloc:DateTime(var_11_2C, ldsfld:DateTime(TimeUtil::UtcEpoch))
// 	stloc:TimeSpan(var_12_37, call:TimeSpan(DateTime::op_Subtraction, ldloc:DateTime(var_10_25), ldloc:DateTime(var_11_2C)))
// }
//
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.FlattenBasicBlocks(ILNode node) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 1813
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.Optimize(DecompilerContext context, ILBlock method, AutoPropertyProvider autoPropertyProvider, StateMachineKind& stateMachineKind, MethodDef& inlinedMethod, AsyncMethodDebugInfo& asyncInfo, ILAstOptimizationStep abortBeforeStep) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 347
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 123
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
//    --- End of inner exception stack trace ---
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
//    at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1627
// */;
// 		}
//
// 		public long GetLocalTodayChangeDayTimeStamp()
// 		{
// 			/*
// An exception occurred when decompiling this method
//
// ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Int64 Ortega.Share.OrtegaTimeManager::GetLocalTodayChangeDayTimeStamp()
//
//  ---> System.Exception: Basic block has to end with unconditional control flow. 
// {
// 	Block_0:
// 	stloc:DateTime(var_0_06, call:DateTime(OrtegaTimeManager::GetLocalDateTime, ldloc:OrtegaTimeManager(this)))
// 	stloc:TimeSpan(var_1_0C, ldsfld:TimeSpan(TimeUtil::ChangeDayTime))
// 	stloc:DateTime(var_2_14, call:DateTime(DateTime::op_Subtraction, ldloc:DateTime(var_0_06), ldloc:TimeSpan(var_1_0C)))
// 	stloc:int32(var_6_16, ldc.i4:int32(0))
// 	stloc:TimeSpan(var_7_1D, ldsfld:TimeSpan(TimeUtil::ChangeDayTime))
// 	stloc:DateTime(var_8_28, call:DateTime(DateTime::op_Addition, ldloc:int32[exp:DateTime](var_6_16), ldloc:TimeSpan(var_7_1D)))
// 	stloc:DateTime(var_9_2F, ldsfld:DateTime(TimeUtil::UtcEpoch))
// 	stloc:TimeSpan(var_10_3A, call:TimeSpan(DateTime::op_Subtraction, ldloc:DateTime(var_8_28), ldloc:DateTime(var_9_2F)))
// }
//
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.FlattenBasicBlocks(ILNode node) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 1813
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.Optimize(DecompilerContext context, ILBlock method, AutoPropertyProvider autoPropertyProvider, StateMachineKind& stateMachineKind, MethodDef& inlinedMethod, AsyncMethodDebugInfo& asyncInfo, ILAstOptimizationStep abortBeforeStep) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 347
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 123
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
//    --- End of inner exception stack trace ---
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
//    at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1627
// */;
// 		}
//
// 		public long GetLocalNextChangeDayTime(long localTime)
// 		{
// 			/*
// An exception occurred when decompiling this method
//
// ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Int64 Ortega.Share.OrtegaTimeManager::GetLocalNextChangeDayTime(System.Int64)
//
//  ---> System.Exception: Basic block has to end with unconditional control flow. 
// {
// 	Block_0:
// 	stloc:TimeSpan(var_0_05, ldsfld:TimeSpan(TimeUtil::ChangeDayTime))
// 	stloc:DateTime(var_3_0D, ldsfld:DateTime(TimeUtil::UtcEpoch))
// 	stloc:TimeSpan(var_10_16, ldsfld:TimeSpan(TimeUtil::ChangeDayTime))
// 	stloc:DateTime(var_11_21, call:DateTime(DateTime::op_Addition, ldloc:DateTime(var_9), ldloc:TimeSpan(var_10_16)))
// 	stloc:DateTime(var_12_28, ldsfld:DateTime(TimeUtil::UtcEpoch))
// 	stloc:TimeSpan(var_13_33, call:TimeSpan(DateTime::op_Subtraction, ldloc:DateTime(var_11_21), ldloc:DateTime(var_12_28)))
// }
//
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.FlattenBasicBlocks(ILNode node) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 1813
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.Optimize(DecompilerContext context, ILBlock method, AutoPropertyProvider autoPropertyProvider, StateMachineKind& stateMachineKind, MethodDef& inlinedMethod, AsyncMethodDebugInfo& asyncInfo, ILAstOptimizationStep abortBeforeStep) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 347
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 123
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
//    --- End of inner exception stack trace ---
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
//    at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1627
// */;
// 		}
//
// 		public long GetLocalLastMondayTimeStamp()
// 		{
// 			/*
// An exception occurred when decompiling this method
//
// ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Int64 Ortega.Share.OrtegaTimeManager::GetLocalLastMondayTimeStamp()
//
//  ---> System.Exception: Basic block has to end with unconditional control flow. 
// {
// 	Block_0:
// 	stloc:DateTime(var_1_06, call:DateTime(OrtegaTimeManager::GetLocalDateTime, ldloc:OrtegaTimeManager(this)))
// 	stloc:TimeSpan(var_2_0C, ldsfld:TimeSpan(TimeUtil::ChangeDayTime))
// 	stloc:DateTime(var_3_14, call:DateTime(DateTime::op_Subtraction, ldloc:DateTime(var_1_06), ldloc:TimeSpan(var_2_0C)))
// 	stloc:uint32(var_10, sub:uint32(ldloc:uint32(var_10), ldloc:DayOfWeek[exp:uint32](var_9)))
// 	stloc:TimeSpan(var_12_24, ldsfld:TimeSpan(TimeUtil::ChangeDayTime))
// 	stloc:DateTime(var_13_2F, call:DateTime(DateTime::op_Addition, ldloc:DateTime(var_11), ldloc:TimeSpan(var_12_24)))
// 	stloc:DateTime(var_14_36, ldsfld:DateTime(TimeUtil::UtcEpoch))
// 	stloc:TimeSpan(var_15_41, call:TimeSpan(DateTime::op_Subtraction, ldloc:DateTime(var_13_2F), ldloc:DateTime(var_14_36)))
// }
//
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.FlattenBasicBlocks(ILNode node) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 1813
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.Optimize(DecompilerContext context, ILBlock method, AutoPropertyProvider autoPropertyProvider, StateMachineKind& stateMachineKind, MethodDef& inlinedMethod, AsyncMethodDebugInfo& asyncInfo, ILAstOptimizationStep abortBeforeStep) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 347
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 123
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
//    --- End of inner exception stack trace ---
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
//    at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1627
// */;
// 		}
//
// 		public long GetLocalLastGrandBattleMatchingTimeStamp()
// 		{
// 			/*
// An exception occurred when decompiling this method
//
// ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Int64 Ortega.Share.OrtegaTimeManager::GetLocalLastGrandBattleMatchingTimeStamp()
//
//  ---> System.Exception: Basic block has to end with unconditional control flow. 
// {
// 	Block_0:
// 	stloc:DateTime(var_1_06, call:DateTime(OrtegaTimeManager::GetLocalDateTime, ldloc:OrtegaTimeManager(this)))
// 	stloc:TimeSpan(var_2_0C, ldsfld:TimeSpan(TimeUtil::ChangeDayTime))
// 	stloc:DateTime(var_3_14, call:DateTime(DateTime::op_Subtraction, ldloc:DateTime(var_1_06), ldloc:TimeSpan(var_2_0C)))
// 	stloc:uint32(var_10, sub:uint32(ldloc:uint32(var_10), ldloc:DayOfWeek[exp:uint32](var_9)))
// 	stloc:TimeSpan(var_12_24, ldsfld:TimeSpan(TimeUtil::ChangeDayTime))
// 	stloc:DateTime(var_13_2F, call:DateTime(DateTime::op_Addition, ldloc:DateTime(var_11), ldloc:TimeSpan(var_12_24)))
// 	stloc:DateTime(var_14_36, ldsfld:DateTime(TimeUtil::UtcEpoch))
// 	stloc:TimeSpan(var_15_41, call:TimeSpan(DateTime::op_Subtraction, ldloc:DateTime(var_13_2F), ldloc:DateTime(var_14_36)))
// }
//
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.FlattenBasicBlocks(ILNode node) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 1813
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.Optimize(DecompilerContext context, ILBlock method, AutoPropertyProvider autoPropertyProvider, StateMachineKind& stateMachineKind, MethodDef& inlinedMethod, AsyncMethodDebugInfo& asyncInfo, ILAstOptimizationStep abortBeforeStep) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 347
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 123
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
//    --- End of inner exception stack trace ---
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
//    at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1627
// */;
// 		}
//
// 		public long GetLocalLastMondayTimeStamp(long timestamp)
// 		{
// 			/*
// An exception occurred when decompiling this method
//
// ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Int64 Ortega.Share.OrtegaTimeManager::GetLocalLastMondayTimeStamp(System.Int64)
//
//  ---> System.Exception: Basic block has to end with unconditional control flow. 
// {
// 	Block_0:
// 	stloc:DateTime(var_2_07, ldsfld:DateTime(TimeUtil::UtcEpoch))
// 	stloc:TimeSpan(var_4_0D, ldsfld:TimeSpan(TimeUtil::ChangeDayTime))
// 	stloc:DateTime(var_5_17, call:DateTime(DateTime::op_Subtraction, ldloc:DateTime(var_3), ldloc:TimeSpan(var_4_0D)))
// 	stloc:uint32(var_11, sub:uint32(ldloc:uint32(var_11), ldloc:DayOfWeek[exp:uint32](var_10)))
// 	stloc:TimeSpan(var_13_25, ldsfld:TimeSpan(TimeUtil::ChangeDayTime))
// 	stloc:DateTime(var_14_30, call:DateTime(DateTime::op_Addition, ldloc:DateTime(var_12), ldloc:TimeSpan(var_13_25)))
// 	stloc:DateTime(var_15_37, ldsfld:DateTime(TimeUtil::UtcEpoch))
// 	stloc:TimeSpan(var_16_42, call:TimeSpan(DateTime::op_Subtraction, ldloc:DateTime(var_14_30), ldloc:DateTime(var_15_37)))
// }
//
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.FlattenBasicBlocks(ILNode node) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 1813
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.Optimize(DecompilerContext context, ILBlock method, AutoPropertyProvider autoPropertyProvider, StateMachineKind& stateMachineKind, MethodDef& inlinedMethod, AsyncMethodDebugInfo& asyncInfo, ILAstOptimizationStep abortBeforeStep) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 347
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 123
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
//    --- End of inner exception stack trace ---
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
//    at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1627
// */;
// 		}
//
// 		public long GetLocalNextMondayTimeStamp()
// 		{
// 			/*
// An exception occurred when decompiling this method
//
// ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Int64 Ortega.Share.OrtegaTimeManager::GetLocalNextMondayTimeStamp()
//
//  ---> System.Exception: Basic block has to end with unconditional control flow. 
// {
// 	Block_0:
// 	stloc:DateTime(var_1_06, call:DateTime(OrtegaTimeManager::GetLocalDateTime, ldloc:OrtegaTimeManager(this)))
// 	stloc:TimeSpan(var_2_0C, ldsfld:TimeSpan(TimeUtil::ChangeDayTime))
// 	stloc:DateTime(var_3_14, call:DateTime(DateTime::op_Subtraction, ldloc:DateTime(var_1_06), ldloc:TimeSpan(var_2_0C)))
// 	stloc:uint32(var_10, sub:uint32(ldloc:uint32(var_10), ldloc:DayOfWeek[exp:uint32](var_9)))
// 	stloc:TimeSpan(var_12_24, ldsfld:TimeSpan(TimeUtil::ChangeDayTime))
// 	stloc:DateTime(var_13_2F, call:DateTime(DateTime::op_Addition, ldloc:DateTime(var_11), ldloc:TimeSpan(var_12_24)))
// 	stloc:DateTime(var_14_36, ldsfld:DateTime(TimeUtil::UtcEpoch))
// 	stloc:TimeSpan(var_15_41, call:TimeSpan(DateTime::op_Subtraction, ldloc:DateTime(var_13_2F), ldloc:DateTime(var_14_36)))
// }
//
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.FlattenBasicBlocks(ILNode node) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 1813
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.Optimize(DecompilerContext context, ILBlock method, AutoPropertyProvider autoPropertyProvider, StateMachineKind& stateMachineKind, MethodDef& inlinedMethod, AsyncMethodDebugInfo& asyncInfo, ILAstOptimizationStep abortBeforeStep) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 347
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 123
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
//    --- End of inner exception stack trace ---
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
//    at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1627
// */;
// 		}
//
// 		public long GetLocalNextDayOfWeekTimeStamp(DayOfWeek dayOfWeek)
// 		{
// 			/*
// An exception occurred when decompiling this method
//
// ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Int64 Ortega.Share.OrtegaTimeManager::GetLocalNextDayOfWeekTimeStamp(System.DayOfWeek)
//
//  ---> System.Exception: Basic block has to end with unconditional control flow. 
// {
// 	Block_0:
// 	stloc:DateTime(var_2_08, call:DateTime(OrtegaTimeManager::GetLocalDateTime, ldloc:OrtegaTimeManager(this)))
// 	stloc:TimeSpan(var_3_0E, ldsfld:TimeSpan(TimeUtil::ChangeDayTime))
// 	stloc:DateTime(var_4_16, call:DateTime(DateTime::op_Subtraction, ldloc:DateTime(var_2_08), ldloc:TimeSpan(var_3_0E)))
// 	stloc:int32(var_0_1F, sub:int32(ldloc:DayOfWeek(dayOfWeek), ldloc:DayOfWeek(var_10)))
// 	stloc:TimeSpan(var_12_25, ldsfld:TimeSpan(TimeUtil::ChangeDayTime))
// 	stloc:DateTime(var_13_30, call:DateTime(DateTime::op_Addition, ldloc:DateTime(var_11), ldloc:TimeSpan(var_12_25)))
// 	stloc:DateTime(var_14_37, ldsfld:DateTime(TimeUtil::UtcEpoch))
// 	stloc:TimeSpan(var_15_42, call:TimeSpan(DateTime::op_Subtraction, ldloc:DateTime(var_13_30), ldloc:DateTime(var_14_37)))
// }
//
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.FlattenBasicBlocks(ILNode node) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 1813
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.Optimize(DecompilerContext context, ILBlock method, AutoPropertyProvider autoPropertyProvider, StateMachineKind& stateMachineKind, MethodDef& inlinedMethod, AsyncMethodDebugInfo& asyncInfo, ILAstOptimizationStep abortBeforeStep) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 347
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 123
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
//    --- End of inner exception stack trace ---
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
//    at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1627
// */;
// 		}
//
// 		public long GetLocalNextMondayTimeStamp(long timestamp)
// 		{
// 			/*
// An exception occurred when decompiling this method
//
// ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Int64 Ortega.Share.OrtegaTimeManager::GetLocalNextMondayTimeStamp(System.Int64)
//
//  ---> System.Exception: Basic block has to end with unconditional control flow. 
// {
// 	Block_0:
// 	stloc:DateTime(var_2_07, ldsfld:DateTime(TimeUtil::UtcEpoch))
// 	stloc:TimeSpan(var_4_0D, ldsfld:TimeSpan(TimeUtil::ChangeDayTime))
// 	stloc:DateTime(var_5_17, call:DateTime(DateTime::op_Subtraction, ldloc:DateTime(var_3), ldloc:TimeSpan(var_4_0D)))
// 	stloc:uint32(var_11, sub:uint32(ldloc:uint32(var_11), ldloc:DayOfWeek[exp:uint32](var_10)))
// 	stloc:TimeSpan(var_13_25, ldsfld:TimeSpan(TimeUtil::ChangeDayTime))
// 	stloc:DateTime(var_14_30, call:DateTime(DateTime::op_Addition, ldloc:DateTime(var_12), ldloc:TimeSpan(var_13_25)))
// 	stloc:DateTime(var_15_37, ldsfld:DateTime(TimeUtil::UtcEpoch))
// 	stloc:TimeSpan(var_16_42, call:TimeSpan(DateTime::op_Subtraction, ldloc:DateTime(var_14_30), ldloc:DateTime(var_15_37)))
// }
//
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.FlattenBasicBlocks(ILNode node) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 1813
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.Optimize(DecompilerContext context, ILBlock method, AutoPropertyProvider autoPropertyProvider, StateMachineKind& stateMachineKind, MethodDef& inlinedMethod, AsyncMethodDebugInfo& asyncInfo, ILAstOptimizationStep abortBeforeStep) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 347
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 123
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
//    --- End of inner exception stack trace ---
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
//    at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1627
// */;
// 		}
//
// 		public long GetLocalNextMonthFirstDayTimeStamp()
// 		{
// 			/*
// An exception occurred when decompiling this method
//
// ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Int64 Ortega.Share.OrtegaTimeManager::GetLocalNextMonthFirstDayTimeStamp()
//
//  ---> System.Exception: Basic block has to end with unconditional control flow. 
// {
// 	Block_0:
// 	stloc:DateTime(var_1_06, call:DateTime(OrtegaTimeManager::GetLocalDateTime, ldloc:OrtegaTimeManager(this)))
// 	stloc:TimeSpan(var_2_0C, ldsfld:TimeSpan(TimeUtil::ChangeDayTime))
// 	stloc:DateTime(var_3_14, call:DateTime(DateTime::op_Subtraction, ldloc:DateTime(var_1_06), ldloc:TimeSpan(var_2_0C)))
// 	stloc:TimeSpan(var_8_1A, ldsfld:TimeSpan(TimeUtil::ChangeDayTime))
// 	stloc:DateTime(var_9_25, call:DateTime(DateTime::op_Addition, ldloc:DateTime(var_7), ldloc:TimeSpan(var_8_1A)))
// 	stloc:DateTime(var_10_2C, ldsfld:DateTime(TimeUtil::UtcEpoch))
// 	stloc:TimeSpan(var_11_37, call:TimeSpan(DateTime::op_Subtraction, ldloc:DateTime(var_9_25), ldloc:DateTime(var_10_2C)))
// }
//
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.FlattenBasicBlocks(ILNode node) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 1813
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.Optimize(DecompilerContext context, ILBlock method, AutoPropertyProvider autoPropertyProvider, StateMachineKind& stateMachineKind, MethodDef& inlinedMethod, AsyncMethodDebugInfo& asyncInfo, ILAstOptimizationStep abortBeforeStep) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 347
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 123
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
//    --- End of inner exception stack trace ---
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
//    at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1627
// */;
// 		}
//
// 		public DayOfWeek GetDayOfWeek()
// 		{
// 			DateTime localDateTime = this.GetLocalDateTime();
// 			TimeSpan ChangeDayTime = TimeUtil.ChangeDayTime;
// 			DateTime dateTime = localDateTime - ChangeDayTime;
// 			DayOfWeek dayOfWeek;
// 			return dayOfWeek;
// 		}
//
// 		public DateTime GetLocalTodayDateTime()
// 		{
// 			DateTime localDateTime = this.GetLocalDateTime();
// 			TimeSpan ChangeDayTime = TimeUtil.ChangeDayTime;
// 			return localDateTime - ChangeDayTime;
// 		}
//
// 		public DateTime GetLocalGameDate()
// 		{
// 			DateTime localDateTime = this.GetLocalDateTime();
// 			TimeSpan ChangeDayTime = TimeUtil.ChangeDayTime;
// 			DateTime dateTime = localDateTime - ChangeDayTime;
// 			DateTime dateTime2;
// 			return dateTime2;
// 		}
//
// 		public DateTime GetLocalGameDate(long localTimeStamp)
// 		{
// 			DateTime UtcEpoch = TimeUtil.UtcEpoch;
// 			TimeSpan ChangeDayTime = TimeUtil.ChangeDayTime;
// 			DateTime dateTime2;
// 			DateTime dateTime = dateTime2 - ChangeDayTime;
// 			DateTime dateTime3;
// 			return dateTime3;
// 		}
//
// 		public DateTime ConvertUtcTimeStampToLocalDateTime(long utcTimeStamp)
// 		{
// 			DateTime dateTime;
// 			return dateTime;
// 		}
//
// 		public bool IsInTime(IHasStartEndTime data)
// 		{
// 			DateTime localDateTime = this.GetLocalDateTime();
// 			DateTime dateTime;
// 			bool flag = dateTime <= localDateTime;
// 			if (!flag)
// 			{
// 				return flag;
// 			}
// 			DateTime dateTime2;
// 			return localDateTime <= dateTime2;
// 		}
//
// 		public bool IsInTime(IHasStartEndTimeZone data)
// 		{
// 			DateTime localDateTime = this.GetLocalDateTime();
// 			DateTime dateTime;
// 			if (dateTime != 0 && dateTime != (ulong)1L)
// 			{
// 				DateTime jstNowDateTime = TimeUtil.JstNowDateTime;
// 			}
// 			if (!(dateTime > localDateTime))
// 			{
// 				DateTime dateTime2;
// 				if (dateTime2 != 0 && dateTime2 != (ulong)10L)
// 				{
// 					DateTime jstNowDateTime2 = TimeUtil.JstNowDateTime;
// 				}
// 				if (!(dateTime2 < localDateTime))
// 				{
// 					return true;
// 				}
// 			}
// 			throw new NullReferenceException();
// 		}
//
// 		public bool IsInEventTime(IHasEventStartEndTime data)
// 		{
// 			DateTime localDateTime = this.GetLocalDateTime();
// 			bool flag;
// 			bool flag2;
// 			DateTime dateTime;
// 			if (!flag && !flag2 && dateTime <= localDateTime)
// 			{
// 				DateTime dateTime2;
// 				return localDateTime <= dateTime2;
// 			}
// 			throw new NullReferenceException();
// 		}
//
// 		public IHasStartEndTime GetInTimeData(IReadOnlyList<IHasStartEndTime> datas)
// 		{
// 			for (;;)
// 			{
// 				DateTime localDateTime = this.GetLocalDateTime();
// 				DateTime dateTime;
// 				DateTime dateTime2;
// 				if (localDateTime == 0 || (dateTime <= localDateTime && localDateTime <= dateTime2))
// 				{
// 					if ("{il2cpp array field local6->}" != (ulong)0L)
// 					{
// 					}
// 					ulong num;
// 					if (num == (ulong)0L)
// 					{
// 						break;
// 					}
// 				}
// 			}
// 			throw new NullReferenceException();
// 		}
//
// 		public IHasJstStartEndTime GetInTimeData(IReadOnlyList<IHasJstStartEndTime> datas)
// 		{
// 			for (;;)
// 			{
// 				DateTime utcNowDateTime = TimeUtil.UtcNowDateTime;
// 				DateTime dateTime;
// 				DateTime dateTime2;
// 				DateTime dateTime3;
// 				if (dateTime == 0 || (dateTime2 <= dateTime && dateTime <= dateTime3))
// 				{
// 					if ("{il2cpp array field local8->}" != (ulong)0L)
// 					{
// 					}
// 					ulong num;
// 					if (num == (ulong)0L)
// 					{
// 						break;
// 					}
// 				}
// 			}
// 			throw new NullReferenceException();
// 		}
//
// 		public bool IsInTime(DateTime startTime, DateTime endTime)
// 		{
// 			DateTime localDateTime = this.GetLocalDateTime();
// 			if (localDateTime >= startTime)
// 			{
// 				return localDateTime <= endTime;
// 			}
// 		}
//
// 		public bool IsInTime(IHasJstStartEndTime data)
// 		{
// 			DateTime utcNowDateTime = TimeUtil.UtcNowDateTime;
// 			DateTime dateTime;
// 			DateTime dateTime2;
// 			bool flag = dateTime <= dateTime2;
// 			if (!flag)
// 			{
// 				return flag;
// 			}
// 			DateTime dateTime3;
// 			return dateTime2 <= dateTime3;
// 		}
//
// 		public bool IsInTimeByHourAndMinuteAndSecond(long startTime, long endTime, long timestamp)
// 		{
// 			DateTime UtcEpoch = TimeUtil.UtcEpoch;
// 			int num2;
// 			long num = (long)(num2 * (int)((uint)100));
// 			int num3;
// 			num += (long)num3;
// 			long num4 = num * (long)((uint)100);
// 			int num5 = (int)((long)num5 + num4);
// 			if (startTime > (long)num5)
// 			{
// 			}
// 			return (long)num5 <= endTime;
// 		}
//
// 		public bool IsInTimeByHourAndMinuteAndSecond(long startTime, long endTime)
// 		{
// 			DateTime localDateTime = this.GetLocalDateTime();
// 			int num2;
// 			long num = (long)(num2 * (int)((uint)100));
// 			int num3;
// 			num += (long)num3;
// 			long num4 = num * (long)((uint)100);
// 			int num5 = (int)((long)num5 + num4);
// 			if (startTime > (long)num5)
// 			{
// 			}
// 			return (long)num5 <= endTime;
// 		}
//
// 		public long GetAddTime(DateAddTimeType type, long value, long utcTimeStamp = 0L)
// 		{
// 			DateTime UtcEpoch = TimeUtil.UtcEpoch;
// 			DateTime UtcEpoch2 = TimeUtil.UtcEpoch;
// 			long localTimestamp = this.GetLocalTimestamp();
// 			if (type <= DateAddTimeType.Years)
// 			{
// 				DateTime UtcEpoch3 = TimeUtil.UtcEpoch;
// 				DateTime dateTime;
// 				TimeSpan timeSpan = dateTime - UtcEpoch3;
// 			}
// 			throw new NullReferenceException();
// 		}
//
// 		public long ConvertUtcTimeStampToLocalTimeStamp(long utcTimeStamp)
// 		{
// 			long num = this.DifferenceFromUtc;
// 			return num + utcTimeStamp;
// 		}
//
// 		public long ConvertLocalTimeStampToUtcTimeStamp(long localTimeStamp)
// 		{
// 			return localTimeStamp;
// 		}
//
// 		public long ConvertJstTimeStampToLocalTimeStamp(DateTime jstDateTime)
// 		{
// 			/*
// An exception occurred when decompiling this method
//
// ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Int64 Ortega.Share.OrtegaTimeManager::ConvertJstTimeStampToLocalTimeStamp(System.DateTime)
//
//  ---> System.Exception: Basic block has to end with unconditional control flow. 
// {
// 	Block_0:
// 	stloc:DateTime(var_1_05, ldsfld:DateTime(TimeUtil::UtcEpoch))
// 	stloc:TimeSpan(var_2_0D, call:TimeSpan(DateTime::op_Subtraction, ldloc:DateTime(var_0), ldloc:DateTime(var_1_05)))
// }
//
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.FlattenBasicBlocks(ILNode node) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 1813
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.Optimize(DecompilerContext context, ILBlock method, AutoPropertyProvider autoPropertyProvider, StateMachineKind& stateMachineKind, MethodDef& inlinedMethod, AsyncMethodDebugInfo& asyncInfo, ILAstOptimizationStep abortBeforeStep) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 347
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 123
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
//    --- End of inner exception stack trace ---
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
//    at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1627
// */;
// 		}
//
// 		public DateTime ConvertJstDateTImeToLocalDateTime(DateTime jstDateTime)
// 		{
// 			DateTime utcNowDateTime = TimeUtil.UtcNowDateTime;
// 			DateTime dateTime;
// 			TimeSpan timeSpan = jstDateTime - dateTime;
// 			return this.GetLocalDateTime() + timeSpan;
// 		}
//
// 		public long GetLocalTodayUpdateLegendLeagueTimeStamp()
// 		{
// 			/*
// An exception occurred when decompiling this method
//
// ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Int64 Ortega.Share.OrtegaTimeManager::GetLocalTodayUpdateLegendLeagueTimeStamp()
//
//  ---> System.Exception: Basic block has to end with unconditional control flow. 
// {
// 	Block_0:
// 	stloc:DateTime(var_0_06, call:DateTime(OrtegaTimeManager::GetLocalDateTime, ldloc:OrtegaTimeManager(this)))
// 	stloc:DateTime(var_6_11, ldsfld:DateTime(TimeUtil::UtcEpoch))
// 	stloc:TimeSpan(var_7_1C, call:TimeSpan(DateTime::op_Subtraction, ldloc:DateTime(var_5), ldloc:DateTime(var_6_11)))
// }
//
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.FlattenBasicBlocks(ILNode node) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 1813
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.Optimize(DecompilerContext context, ILBlock method, AutoPropertyProvider autoPropertyProvider, StateMachineKind& stateMachineKind, MethodDef& inlinedMethod, AsyncMethodDebugInfo& asyncInfo, ILAstOptimizationStep abortBeforeStep) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 347
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 123
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
//    --- End of inner exception stack trace ---
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
//    at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1627
// */;
// 		}
//
// 		public long GetLocalLastUpdateLegendLeagueTimeStamp()
// 		{
// 			long localTimestamp = this.GetLocalTimestamp();
// 			long localTodayUpdateLegendLeagueTimeStamp = this.GetLocalTodayUpdateLegendLeagueTimeStamp();
// 			TimeSpan timeSpan;
// 			return localTodayUpdateLegendLeagueTimeStamp - timeSpan;
// 		}
//
// 		public int GetLegendLeagueDayOfWeek()
// 		{
// 			/*
// An exception occurred when decompiling this method
//
// ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Int32 Ortega.Share.OrtegaTimeManager::GetLegendLeagueDayOfWeek()
//
//  ---> System.Exception: Basic block has to end with unconditional control flow. 
// {
// 	Block_0:
// 	stloc:DateTime(var_0_06, call:DateTime(OrtegaTimeManager::GetLocalDateTime, ldloc:OrtegaTimeManager(this)))
// 	stloc:int64(var_1_0D, call:int64(OrtegaTimeManager::GetLocalTimestamp, ldloc:OrtegaTimeManager(this)))
// 	stloc:int64(var_2_14, call:int64(OrtegaTimeManager::GetLocalTodayUpdateLegendLeagueTimeStamp, ldloc:OrtegaTimeManager(this)))
// }
//
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.FlattenBasicBlocks(ILNode node) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 1813
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.Optimize(DecompilerContext context, ILBlock method, AutoPropertyProvider autoPropertyProvider, StateMachineKind& stateMachineKind, MethodDef& inlinedMethod, AsyncMethodDebugInfo& asyncInfo, ILAstOptimizationStep abortBeforeStep) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 347
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 123
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
//    --- End of inner exception stack trace ---
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
//    at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1627
// */;
// 		}
//
// 		public int GetYesterdayLegendLeagueDayOfWeek()
// 		{
// 			/*
// An exception occurred when decompiling this method
//
// ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Int32 Ortega.Share.OrtegaTimeManager::GetYesterdayLegendLeagueDayOfWeek()
//
//  ---> System.Exception: Basic block has to end with unconditional control flow. 
// {
// 	Block_0:
// 	stloc:DateTime(var_0_06, call:DateTime(OrtegaTimeManager::GetLocalDateTime, ldloc:OrtegaTimeManager(this)))
// 	stloc:int64(var_1_0D, call:int64(OrtegaTimeManager::GetLocalTimestamp, ldloc:OrtegaTimeManager(this)))
// 	stloc:int64(var_2_14, call:int64(OrtegaTimeManager::GetLocalTodayUpdateLegendLeagueTimeStamp, ldloc:OrtegaTimeManager(this)))
// }
//
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.FlattenBasicBlocks(ILNode node) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 1813
//    at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.Optimize(DecompilerContext context, ILBlock method, AutoPropertyProvider autoPropertyProvider, StateMachineKind& stateMachineKind, MethodDef& inlinedMethod, AsyncMethodDebugInfo& asyncInfo, ILAstOptimizationStep abortBeforeStep) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 347
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 123
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
//    --- End of inner exception stack trace ---
//    at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
//    at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1627
// */;
// 		}
//
// 		public bool IsChangeDayByChangeDayTime(long timestamp)
// 		{
// 			long num = this.GetLocalTimestamp();
// 			TimeSpan ChangeDayTime = TimeUtil.ChangeDayTime;
// 			num -= ChangeDayTime;
// 			TimeSpan ChangeDayTime2 = TimeUtil.ChangeDayTime;
// 			DateTime dateTime;
// 			DateTime dateTime2;
// 			return dateTime < dateTime2;
// 		}
//
// 		public DateTime GetChangeDayLocalDateTime()
// 		{
// 			DateTime localDateTime = this.GetLocalDateTime();
// 			TimeSpan ChangeDayTime = TimeUtil.ChangeDayTime;
// 			DateTime dateTime = localDateTime - ChangeDayTime;
// 			TimeSpan ChangeDayTime2 = TimeUtil.ChangeDayTime;
// 			DateTime dateTime2;
// 			return dateTime2;
// 		}
//
		public int GetDateIntYearMonthDay()
		{
			long localTimestamp = this.GetLocalTimestamp();
			DateTime dateData = TimeUtil.UtcEpoch.AddMilliseconds(localTimestamp)- TimeUtil.ChangeDayTime;
			return (dateData.Year * 100 + dateData.Month) * 100 + dateData.Day;
		}
//
// 		public int GetDateIntYearMonthDay(long timeStamp)
// 		{
// 			DateTime UtcEpoch = TimeUtil.UtcEpoch;
// 			TimeSpan ChangeDayTime = TimeUtil.ChangeDayTime;
// 			DateTime dateTime2;
// 			DateTime dateTime = dateTime2 - ChangeDayTime;
// 			int num2;
// 			long num = (long)(num2 * (int)((uint)100));
// 			int num3;
// 			num += (long)num3;
// 			long num4 = num * (long)((uint)100);
// 			int num5 = (int)((long)num5 + num4);
// 			return num5;
// 		}
	}
}
