using System.ComponentModel;
using MementoMori.Ortega.Share.Data.BountyQuest;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[Description("祈りの泉\u3000ボードランク")]
	[MessagePackObject(true)]
	public class BoardRankMB : MasterBookBase
	{
		[Description("必要条件リスト")]
		[Nest(false, 0)]
		[PropertyOrder(2)]
		public IReadOnlyList<BoardRankConditionInfo> BoardRankConditionInfos
		{
			get;
		}

		[Description("開放クエスト最大レベル")]
		[PropertyOrder(3)]
		public BountyQuestRarityFlags OpenBountyQuestMaxRarity
		{
			get;
		}

		[PropertyOrder(4)]
		[Description("開放クエスト最低レベル")]
		public BountyQuestRarityFlags OpenBountyQuestMinRarity
		{
			get;
		}

		[PropertyOrder(5)]
		[Description("クエスト抽選リスト詳細ID")]
		public long QuestLotteryInfoListId
		{
			get;
		}

		[Description("ランク")]
		[PropertyOrder(1)]
		public long Rank
		{
			get;
		}

		[SerializationConstructor]
		public BoardRankMB(long id, bool? isIgnore, string memo, long rank, IReadOnlyList<BoardRankConditionInfo> boardRankConditionInfos, BountyQuestRarityFlags openBountyQuestMaxRarity, BountyQuestRarityFlags openBountyQuestMinRarity, long questLotteryInfoListId)
			:base(id, isIgnore, memo)
		{
			Rank = rank;
			BoardRankConditionInfos = boardRankConditionInfos;
			OpenBountyQuestMaxRarity = openBountyQuestMaxRarity;
			OpenBountyQuestMinRarity = openBountyQuestMinRarity;
			QuestLotteryInfoListId = questLotteryInfoListId;
		}

		public BoardRankMB():base(0, false, "")
		{
			/*
An exception occurred when decompiling this method (060034A7)

ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void Ortega.Share.Master.Data.BoardRankMB::.ctor()

 ---> System.Exception: Basic block has to end with unconditional control flow. 
{
	Block_0:
	call:void(object::.ctor, ldloc:BoardRankMB[exp:object](this))
	stloc:int32(var_0_07, ldc.i4:int32(0))
	stfld:int64(MasterBookBase::Id, ldloc:BoardRankMB[exp:MasterBookBase](this), ldloc:int32[exp:int64](var_0_07))
	stfld:valuetype [mscorlib]System.Nullable`1<bool>(MasterBookBase::IsIgnore, ldloc:BoardRankMB[exp:MasterBookBase](this), ldloc:int32[exp:valuetype [mscorlib]System.Nullable`1<bool>](var_0_07))
	stfld:string(MasterBookBase::Memo, ldloc:BoardRankMB[exp:MasterBookBase](this), ldloc:int32[exp:string](var_0_07))
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
}
