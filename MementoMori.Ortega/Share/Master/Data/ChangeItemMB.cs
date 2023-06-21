using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("アイテム変換情報")]
	public class ChangeItemMB : MasterBookBase
	{
		[PropertyOrder(5)]
		[Nest(false, 0)]
		[Description("変換されるアイテムリスト")]
		public IReadOnlyList<UserItem> ChangeItems
		{
			get;
		}

		[Description("アイテム変換タイプ")]
		[PropertyOrder(1)]
		public ChangeItemType ChangeItemType
		{
			get;
		}

		[Description("アイテムID")]
		[PropertyOrder(3)]
		public long ItemId
		{
			get;
		}

		[Description("アイテム種別")]
		[PropertyOrder(2)]
		public ItemType ItemType
		{
			get;
		}

		[PropertyOrder(4)]
		[Description("変換に必要な数")]
		public int NeedCount
		{
			get;
		}

		[SerializationConstructor]
		public ChangeItemMB(long id, bool? isIgnore, string memo, IReadOnlyList<UserItem> changeItems, ChangeItemType changeItemType, long itemId, ItemType itemType, int needCount)
			:base(id, isIgnore, memo)
		{
			ChangeItems = changeItems;
			ChangeItemType = changeItemType;
			ItemId = itemId;
			ItemType = itemType;
			NeedCount = needCount;
		}

		public ChangeItemMB():base(0, false, "")
		{
			/*
An exception occurred when decompiling this method (060034CB)

ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void Ortega.Share.Master.Data.ChangeItemMB::.ctor()

 ---> System.Exception: Basic block has to end with unconditional control flow. 
{
	Block_0:
	call:void(object::.ctor, ldloc:ChangeItemMB[exp:object](this))
	stloc:int32(var_0_07, ldc.i4:int32(0))
	stfld:int64(MasterBookBase::Id, ldloc:ChangeItemMB[exp:MasterBookBase](this), ldloc:int32[exp:int64](var_0_07))
	stfld:valuetype [mscorlib]System.Nullable`1<bool>(MasterBookBase::IsIgnore, ldloc:ChangeItemMB[exp:MasterBookBase](this), ldloc:int32[exp:valuetype [mscorlib]System.Nullable`1<bool>](var_0_07))
	stfld:string(MasterBookBase::Memo, ldloc:ChangeItemMB[exp:MasterBookBase](this), ldloc:int32[exp:string](var_0_07))
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
