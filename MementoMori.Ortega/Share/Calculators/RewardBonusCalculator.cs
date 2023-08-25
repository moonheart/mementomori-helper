namespace MementoMori.Ortega.Share.Calculators
{
	public static class RewardBonusCalculator
	{
		public static long CalcRewardBonus(long rewardCount, int balance)
		{
			throw new NotImplementedException();
			/*
An exception occurred when decompiling this method

ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Int64 Ortega.Share.Calculators.RewardBonusCalculator::CalcRewardBonus(System.Int64,System.Int32)

 ---> System.Exception: Basic block has to end with unconditional control flow. 
{
	Block_0:
	stloc:uint64(var_0, add:uint64(ldloc:uint64(var_0), ldloc:uint64(var_0)))
	stloc:uint64(var_0, add:uint64(ldloc:uint64(var_0), ldloc:int64[exp:uint64](rewardCount)))
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

		public static int CalcWeeklyBonus(long currentMaxQuestId, DayOfWeek dayOfWeek)
		{
			throw new NotImplementedException();
			// StateBonusMB stateBonusMB = StateBonusUtil.GetStateBonusMB(currentMaxQuestId);
			// if (stateBonusMB != 0 && stateBonusMB.<IsActiveWeeklyBonus>k__BackingField)
			// {
			// 	List<DayOfWeek> <AutoQuickBonusDayOfWeeks>k__BackingField = OrtegaConst.StateBonus.<AutoQuickBonusDayOfWeeks>k__BackingField;
			// 	bool flag;
			// 	if (flag)
			// 	{
			// 	}
			// }
			return 1;
		}

		public static long CalculateGuildCoinBonus(long count, long currentMaxQuestId, bool isValidContractPrivilege)
		{
			throw new NotImplementedException();
			// if (isValidContractPrivilege)
			// {
			// }
			// if (StateBonusUtil.GetStateBonusMB(currentMaxQuestId) != 0)
			// {
			// }
			// long num;
			// return num;
		}
	}
}
