using MementoMori.Common;
using Ortega.Common.Manager;

namespace MementoMori.Ortega.Share.Utils
{
	public static class TimeUtil
	{
		public static TimeSpan ChangeDayTime { get; } = TimeSpan.FromHours(4);

		public static DateTime UtcEpoch { get; } = new DateTime(1970, 1, 1, 0, 0, 0, 1, DateTimeKind.Unspecified);

		// public static DateTime UtcNowDateTime
		// {
		// 	get
		// 	{
		// 		DateTime dateTime = TimeUtil.UtcEpoch;
		// 		long utcNowTimeStamp = TimeUtil.UtcNowTimeStamp;
		// 		return dateTime.AddMilliseconds(utcNowTimeStamp);
		// 	}
		// }

		// public static DateTime JstNowDateTime
		// {
		// 	get
		// 	{
		// 		DateTime utcNowDateTime = TimeUtil.UtcNowDateTime;
		// 		DateTime dateTime;
		// 		return dateTime;
		// 	}
		// }

		// public static long UtcNowTimeStamp
		// {
		// 	get
		// 	{
		// 		long now = TimeUtil.UtcTimeCalculation(DateTime.UtcNow);
		// 		long diffServerTimeMilliSeconds = Services.Get<NetworkManager>().DiffServerTimeMilliSeconds;
		// 		return diffServerTimeMilliSeconds + now;
		// 	}
		// }
//
// 		public static long UtcTodayMidnightTimeStamp
// 		{
// 			get
// 			{
// 				/*
// An exception occurred when decompiling this method
//
// ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Int64 Ortega.Share.Utils.TimeUtil::get_UtcTodayMidnightTimeStamp()
//
//  ---> System.Exception: Basic block has to end with unconditional control flow. 
// {
// 	Block_0:
// 	stloc:DateTime(var_1_07, callgetter:DateTime(TimeUtil::get_UtcNowDateTime))
// 	stloc:int32(var_5_09, ldc.i4:int32(0))
// 	stloc:DateTime(var_7_10, ldsfld:DateTime(TimeUtil::UtcEpoch))
// 	stloc:TimeSpan(var_8_1B, call:TimeSpan(DateTime::op_Subtraction, ldloc:int32[exp:DateTime](var_5_09), ldloc:DateTime(var_7_10)))
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
// 			}
// 		}
//
// 		public static long UtcTomorrowMidnightTimeStamp
// 		{
// 			get
// 			{
// 				/*
// An exception occurred when decompiling this method
//
// ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Int64 Ortega.Share.Utils.TimeUtil::get_UtcTomorrowMidnightTimeStamp()
//
//  ---> System.Exception: Basic block has to end with unconditional control flow. 
// {
// 	Block_0:
// 	stloc:DateTime(var_2_09, callgetter:DateTime(TimeUtil::get_UtcNowDateTime))
// 	stloc:DateTime(var_8_0F, ldsfld:DateTime(TimeUtil::UtcEpoch))
// 	stloc:TimeSpan(var_9_1A, call:TimeSpan(DateTime::op_Subtraction, ldloc:DateTime(var_7), ldloc:DateTime(var_8_0F)))
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
// 			}
// 		}
//
// 		public static long GetLocalTimestamp(ILocalTime local)
// 		{
// 			long utcNowTimeStamp = TimeUtil.UtcNowTimeStamp;
// 			return utcNowTimeStamp + utcNowTimeStamp;
// 		}
//
// 		public static DateTime GetLocalDateTime(ILocalTime local)
// 		{
// 			long utcNowTimeStamp = TimeUtil.UtcNowTimeStamp;
// 			return TimeUtil.ConvertUtcTimeStampToDateTime(0L);
// 		}
//
// 		public static DateTime ConvertUtcTimeStampToDateTime(long utcTimeStamp)
// 		{
// 			DateTime dateTime = TimeUtil.UtcEpoch;
// 			DateTime dateTime2;
// 			return dateTime2;
// 		}
//
// 		public static long ConvertDateTimeToTimeStamp(DateTime dateTime)
// 		{
// 			/*
// An exception occurred when decompiling this method
//
// ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Int64 Ortega.Share.Utils.TimeUtil::ConvertDateTimeToTimeStamp(System.DateTime)
//
//  ---> System.Exception: Basic block has to end with unconditional control flow. 
// {
// 	Block_0:
// 	stloc:DateTime(var_0_05, ldsfld:DateTime(TimeUtil::UtcEpoch))
// 	stloc:TimeSpan(var_1_0D, call:TimeSpan(DateTime::op_Subtraction, ldloc:DateTime(dateTime), ldloc:DateTime(var_0_05)))
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
// 		[Obsolete("時差考慮する")]
// 		public static long AddDaysTimeStamp(int day)
// 		{
// 			DateTime utcNowDateTime = TimeUtil.UtcNowDateTime;
// 			DateTime dateTime;
// 			return TimeUtil.UtcTimeCalculation(dateTime);
// 		}
//
// 		[Obsolete("時差考慮する")]
// 		public static long GetAddTime(long utcTimeStamp, DateAddTimeType type, long value)
// 		{
// 			DateTime dateTime = TimeUtil.UtcEpoch;
// 			if (type <= DateAddTimeType.Years)
// 			{
// 				DateTime dateTime2;
// 				return TimeUtil.UtcTimeCalculation(dateTime2);
// 			}
// 			throw new NullReferenceException();
// 		}
//
		public static long UtcTimeCalculation(DateTime dateTime)
		{
			return (long) (dateTime - UtcEpoch).TotalMilliseconds;
		}
//
// 		public static DateTime GetGameDate(long utcTimeStamp)
// 		{
// 			DateTime dateTime = TimeUtil.ConvertUtcTimeStampToDateTime(utcTimeStamp);
// 			TimeSpan timeSpan = TimeUtil.ChangeDayTime;
// 			DateTime dateTime2 = dateTime - timeSpan;
// 			DateTime dateTime3;
// 			return dateTime3;
// 		}
//
// 		public static DateTime GetGameDate(DateTime dateTime)
// 		{
// 			TimeSpan timeSpan = TimeUtil.ChangeDayTime;
// 			DateTime dateTime2 = dateTime - timeSpan;
// 			DateTime dateTime3;
// 			return dateTime3;
// 		}

	}
}
