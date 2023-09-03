namespace MementoMori.Ortega.Share.Utils
{
    public static class DateUtil
    {
// 		[Obsolete("時差対応")]
// 		public static bool IsInTimeByHourAndMinuteAndSecond(long startTime, long endTime, long timestamp)
// 		{
// 			DateTime UtcEpoch = TimeUtil.UtcEpoch;
// 			int num2;
// 			long num = (long)(num2 * (int)((uint)100));
// 			int num3;
// 			num += (long)num3;
// 			long num4 = num * (long)((uint)100);
// 			int num5 = (int)((long)num5 + num4);
// 			return 0L < endTime;
// 		}
//
        public static int GetDateIntYearMonthDay(long timestamp)
        {
            var dateData = TimeUtil.UtcEpoch.AddMilliseconds(timestamp) - TimeUtil.ChangeDayTime;
            return (dateData.Year * 100 + dateData.Month) * 100 + dateData.Day;
        }
//
// 		public static int GetDateIntYearMonthDayAtMidnight(long timestamp)
// 		{
// 			DateTime UtcEpoch = TimeUtil.UtcEpoch;
// 			int num2;
// 			long num = (long)(num2 * (int)((uint)100));
// 			int num3;
// 			num += (long)num3;
// 			long num4 = num * (long)((uint)100);
// 			int num5 = (int)((long)num5 + num4);
// 			return num5;
// 		}
//
// 		public static int GetDateIntYearMonthDay(DateTime dateTime)
// 		{
// 			int num2;
// 			long num = (long)(num2 * (int)((uint)100));
// 			int num3;
// 			num += (long)num3;
// 			long num4 = num * (long)((uint)100);
// 			int num5 = (int)((long)num5 + num4);
// 			return num5;
// 		}
//
// 		public static int GetDateIntYearMonth(long timestamp)
// 		{
// 			/*
// An exception occurred when decompiling this method
//
// ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Int32 Ortega.Share.Utils.DateUtil::GetDateIntYearMonth(System.Int64)
//
//  ---> System.Exception: Basic block has to end with unconditional control flow. 
// {
// 	Block_0:
// 	stloc:DateTime(var_1_07, ldsfld:DateTime(TimeUtil::UtcEpoch))
// 	stloc:TimeSpan(var_3_0D, ldsfld:TimeSpan(TimeUtil::ChangeDayTime))
// 	stloc:DateTime(var_4_15, call:DateTime(DateTime::op_Subtraction, ldloc:DateTime(var_2), ldloc:TimeSpan(var_3_0D)))
// 	stloc:int64(var_7_1D, mul:int32[exp:int64](ldloc:int32(var_5), conv.u4:uint32[exp:int32](ldc.i4:int32[exp:uint32](100))))
// 	stloc:int32(var_6, add:int64[exp:int32](ldloc:int32[exp:int64](var_6), ldloc:int64(var_7_1D)))
// 	stloc:int64(var_8_2C, mul:int32[exp:int64](ldloc:int32(var_6), conv.u4:uint32[exp:int32](ldc.i4:int32[exp:uint32](100))))
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
// 		public static int GetDateIntYearMonthAtMidnight(long timestamp)
// 		{
// 			/*
// An exception occurred when decompiling this method
//
// ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Int32 Ortega.Share.Utils.DateUtil::GetDateIntYearMonthAtMidnight(System.Int64)
//
//  ---> System.Exception: Basic block has to end with unconditional control flow. 
// {
// 	Block_0:
// 	stloc:DateTime(var_1_07, ldsfld:DateTime(TimeUtil::UtcEpoch))
// 	stloc:int64(var_5_0D, mul:int32[exp:int64](ldloc:int32(var_3), conv.u4:uint32[exp:int32](ldc.i4:int32[exp:uint32](100))))
// 	stloc:int32(var_4, add:int64[exp:int32](ldloc:int32[exp:int64](var_4), ldloc:int64(var_5_0D)))
// 	stloc:int64(var_6_1C, mul:int32[exp:int64](ldloc:int32(var_4), conv.u4:uint32[exp:int32](ldc.i4:int32[exp:uint32](100))))
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
// 		public static int GetFirstDayOfThisWeekDateIntYearMonthDay(long timestamp)
// 		{
// 			DateTime UtcEpoch = TimeUtil.UtcEpoch;
// 			TimeSpan ChangeDayTime = TimeUtil.ChangeDayTime;
// 			DateTime dateTime2;
// 			DateTime dateTime = dateTime2 - ChangeDayTime;
// 			uint num;
// 			DayOfWeek dayOfWeek;
// 			num -= (uint)dayOfWeek;
// 			int num3;
// 			long num2 = (long)(num3 * (int)((uint)100));
// 			int num4;
// 			num2 += (long)num4;
// 			long num5 = num2 * (long)((uint)100);
// 			int num6 = (int)((long)num6 + num5);
// 			return num6;
// 		}
//
// 		public static int GetFirstDayOfThisWeekDateIntYearMonthDayAtMidnight(long timestamp)
// 		{
// 			DateTime UtcEpoch = TimeUtil.UtcEpoch;
// 			uint num;
// 			DayOfWeek dayOfWeek;
// 			num -= (uint)dayOfWeek;
// 			int num3;
// 			long num2 = (long)(num3 * (int)((uint)100));
// 			int num4;
// 			num2 += (long)num4;
// 			long num5 = num2 * (long)((uint)100);
// 			int num6 = (int)((long)num6 + num5);
// 			return num6;
// 		}
//
// 		public static int GetHoursAndMinutes(long timestamp)
// 		{
// 			DateTime UtcEpoch = TimeUtil.UtcEpoch;
// 			int num2;
// 			long num = (long)(num2 * (int)((uint)100));
// 			int num3 = (int)((long)num3 + num);
// 			return num3;
// 		}
//
// 		public static int GetHoursMinutesAndSeconds(long timestamp)
// 		{
// 			DateTime UtcEpoch = TimeUtil.UtcEpoch;
// 			int num2;
// 			long num = (long)(num2 * (int)((uint)100));
// 			int num3;
// 			num += (long)num3;
// 			long num4 = num * (long)((uint)100);
// 			int num5 = (int)((long)num5 + num4);
// 			return num5;
// 		}
//
// 		public static int GetDateIntMonthDay(long timestamp)
// 		{
// 			DateTime UtcEpoch = TimeUtil.UtcEpoch;
// 			TimeSpan ChangeDayTime = TimeUtil.ChangeDayTime;
// 			DateTime dateTime2;
// 			DateTime dateTime = dateTime2 - ChangeDayTime;
// 			int num2;
// 			long num = (long)(num2 * (int)((uint)100));
// 			int num3 = (int)((long)num3 + num);
// 			return num3;
// 		}
//
// 		public static bool ValidDate(int date)
// 		{
// 			/*
// An exception occurred when decompiling this method
//
// ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Boolean Ortega.Share.Utils.DateUtil::ValidDate(System.Int32)
//
//  ---> System.Exception: Basic block has to end with unconditional control flow. 
// {
// 	IL_30:
// 	stloc:int32(var_3_31, ldc.i4:int32(0))
// 	stloc:int32(var_3_35, add:uint64[exp:int32](ldloc:int32[exp:uint64](var_3_31), ldloc:uint64(var_2)))
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
// 		public static bool IsValidDateTimeString(string dateTimeString)
// 		{
// 			bool flag;
// 			return flag;
// 		}
    }
}