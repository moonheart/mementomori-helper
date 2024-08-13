using System.Collections;
using System.ComponentModel;
using MementoMori.Ortega.Share.Data.BountyQuest;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MementoMori.Ortega.Share.Utils;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
    [Description("祈りの泉\u3000イベント")]
    [MessagePackObject(true)]
    public class BountyQuestEventMB : MasterBookBase, IHasStartEndTime
    {
        [PropertyOrder(2)]
        [Description("イベント説明")]
        public string EventDescriptionKey { get; }

        [Description("イベント名")]
        [PropertyOrder(1)]
        public string EventNameKey { get; }

        [Description("報酬量の増加率(%)")]
        [PropertyOrder(5)]
        public int MultipleNumber { get; }

        [PropertyOrder(7)]
        [Nest(false, 0)]
        [Description("対象となるアイテムリスト")]
        public IReadOnlyList<BountyQuestEventTargetItemInfo> TargetItemList { get; }

        [Description("対象となる祈りの泉クエストタイプ")]
        [PropertyOrder(6)]
        [Nest(false, 0)]
        public IReadOnlyList<BountyQuestEventTargetQuestTypeInfo> TargetQuestTypeList { get; }

        [SerializationConstructor]
        public BountyQuestEventMB(long id, bool? isIgnore, string memo, string eventNameKey, string eventDescriptionKey, string startTime, string endTime, int multipleNumber,
            IReadOnlyList<BountyQuestEventTargetQuestTypeInfo> targetQuestTypeList, IReadOnlyList<BountyQuestEventTargetItemInfo> targetItemList)
            : base(id, isIgnore, memo)
        {
            EventNameKey = eventNameKey;
            EventDescriptionKey = eventDescriptionKey;
            StartTime = startTime;
            EndTime = endTime;
            MultipleNumber = multipleNumber;
            TargetQuestTypeList = targetQuestTypeList;
            TargetItemList = targetItemList;
        }

        public BountyQuestEventMB() : base(0, false, "")
        {
            /*
An exception occurred when decompiling this method (060034C0)

ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void Ortega.Share.Master.Data.BountyQuestEventMB::.ctor()

 ---> System.Exception: Basic block has to end with unconditional control flow.
{
    Block_0:
    call:void(object::.ctor, ldloc:BountyQuestEventMB[exp:object](this))
    stloc:int32(var_0_07, ldc.i4:int32(0))
    stfld:int64(MasterBookBase::Id, ldloc:BountyQuestEventMB[exp:MasterBookBase](this), ldloc:int32[exp:int64](var_0_07))
    stfld:valuetype [mscorlib]System.Nullable`1<bool>(MasterBookBase::IsIgnore, ldloc:BountyQuestEventMB[exp:MasterBookBase](this), ldloc:int32[exp:valuetype [mscorlib]System.Nullable`1<bool>](var_0_07))
    stfld:string(MasterBookBase::Memo, ldloc:BountyQuestEventMB[exp:MasterBookBase](this), ldloc:int32[exp:string](var_0_07))
}

   at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.FlattenBasicBlocks(ILNode node) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 1813
   at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.Optimize(DecompilerContext context, ILBlock method, AutoPropertyProvider autoPropertyProvider, StateMachineKind& stateMachineKind, MethodDef& inlinedMethod, AsyncMethodDebugInfo& asyncInfo, ILAstOptimizationStep abortBeforeStep) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 347
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 123
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
   --- End of inner exception stack trace ---
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
   at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1627
*/
            ;
        }

        [Description("終了時刻")]
        [PropertyOrder(4)]
        public string EndTime { get; }

        [PropertyOrder(3)]
        [Description("開始時刻")]
        public string StartTime { get; }

        public bool IsTargetQuestType(BountyQuestType bountyQuestType)
        {
            // int num;
            // do
            // {
            // 	IReadOnlyList<BountyQuestEventTargetQuestTypeInfo> readOnlyList = this.TargetQuestTypeList;
            // 	num = 0;
            // 	if (typeof(IEnumerator).TypeHandle != 0)
            // 	{
            // 		uint num2;
            // 		if (num >= (int)num2)
            // 		{
            // 			goto IL_23;
            // 		}
            // 		num += num;
            // 		if (num != (int)num2)
            // 		{
            // 			num++;
            // 			goto IL_23;
            // 		}
            // 		IL_26:
            // 		int num3;
            // 		num3 += num3;
            // 		goto IL_2D;
            // 		IL_23:
            // 		num3 = 0;
            // 		goto IL_26;
            // 	}
            // 	IL_2D:
            // 	if ("{il2cpp array field local7->}" != (ulong)0L)
            // 	{
            // 	}
            // }
            // while (num != 0);
            // throw new NullReferenceException();
            throw new NotImplementedException();
        }

        public bool IsTargetItem(IUserItem userItem)
        {
            // int num;
            // do
            // {
            // 	IReadOnlyList<BountyQuestEventTargetItemInfo> readOnlyList = this.TargetItemList;
            // 	num = 0;
            // 	if (typeof(IEnumerator).TypeHandle != 0)
            // 	{
            // 		uint num2;
            // 		if (num < (int)num2)
            // 		{
            // 			num += num;
            // 			if (num == (int)num2)
            // 			{
            // 				goto IL_28;
            // 			}
            // 			num++;
            // 		}
            // 		bool flag;
            // 		while (!flag)
            // 		{
            // 		}
            // 		int num3 = 0;
            // 		IL_28:
            // 		num3 += num3;
            // 	}
            // 	if ("{il2cpp array field local6->}" != (ulong)0L)
            // 	{
            // 	}
            // }
            // while (num != 0);
            // throw new NullReferenceException();
            throw new NotImplementedException();
        }
    }
}