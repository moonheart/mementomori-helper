using System.ComponentModel;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
    [Description("バトルスキル演出設定")]
    [MessagePackObject(true)]
    public class BattleSkillNameSettingMB : MasterBookBase
    {
        [PropertyOrder(1)]
        [Description("ルートスキルID")]
        public long RootActiveSkillId { get; }

        [PropertyOrder(2)]
        [Description("改行位置設定 JP")]
        public int NewLineIndexJP { get; }

        [PropertyOrder(3)]
        [Description("改行位置設定 US")]
        public int NewLineIndexUS { get; }

        [PropertyOrder(2)]
        [Description("改行位置設定 KR")]
        public int NewLineIndexKR { get; }

        [PropertyOrder(3)]
        [Description("改行位置設定 TW")]
        public int NewLineIndexTW { get; }

        [PropertyOrder(3)]
        [Description("改行位置設定 FR")]
        public int NewLineIndexFR { get; }

        [Description("改行位置設定 CN")]
        [PropertyOrder(3)]
        public int NewLineIndexCN { get; }

        [Description("改行位置設定 MX")]
        [PropertyOrder(3)]
        public int NewLineIndexMX { get; }

        [Description("改行位置設定 BR")]
        [PropertyOrder(3)]
        public int NewLineIndexBR { get; }

        [PropertyOrder(3)]
        [Description("改行位置設定 TH")]
        public int NewLineIndexTH { get; }

        [PropertyOrder(3)]
        [Description("改行位置設定 ID")]
        public int NewLineIndexID { get; }

        [PropertyOrder(3)]
        [Description("改行位置設定 VN")]
        public int NewLineIndexVN { get; }

        [PropertyOrder(3)]
        [Description("改行位置設定 RU")]
        public int NewLineIndexRU { get; }

        [PropertyOrder(3)]
        [Description("改行位置設定 DE")]
        public int NewLineIndexDE { get; }

        [PropertyOrder(3)]
        [Description("改行位置設定 EG")]
        public int NewLineIndexEG { get; }

        [SerializationConstructor]
        public BattleSkillNameSettingMB(long id, bool? isIgnore, string memo, long rootActiveSkillId, int newLineIndexJP, int newLineIndexUS, int newLineIndexKR, int newLineIndexTW, int newLineIndexFR, int newLineIndexCN, int newLineIndexMX, int newLineIndexBR, int newLineIndexTH, int newLineIndexID, int newLineIndexVN, int newLineIndexRU, int newLineIndexDE, int newLineIndexEG)
            : base(id, isIgnore, memo)
        {
            RootActiveSkillId = rootActiveSkillId;
            NewLineIndexJP = newLineIndexJP;
            NewLineIndexUS = newLineIndexUS;
            NewLineIndexKR = newLineIndexKR;
            NewLineIndexTW = newLineIndexTW;
            NewLineIndexFR = newLineIndexFR;
            NewLineIndexCN = newLineIndexCN;
            NewLineIndexMX = newLineIndexMX;
            NewLineIndexBR = newLineIndexBR;
            NewLineIndexTH = newLineIndexTH;
            NewLineIndexID = newLineIndexID;
            NewLineIndexVN = newLineIndexVN;
            NewLineIndexRU = newLineIndexRU;
            NewLineIndexDE = newLineIndexDE;
            NewLineIndexEG = newLineIndexEG;
        }

        public BattleSkillNameSettingMB() : base(0, false, "")
        {
            /*
An exception occurred when decompiling this method (0600349F)

ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void Ortega.Share.Master.Data.BattleSkillNameSettingMB::.ctor()

 ---> System.Exception: Basic block has to end with unconditional control flow. 
{
    Block_0:
    call:void(object::.ctor, ldloc:BattleSkillNameSettingMB[exp:object](this))
    stloc:int32(var_0_07, ldc.i4:int32(0))
    stfld:int64(MasterBookBase::Id, ldloc:BattleSkillNameSettingMB[exp:MasterBookBase](this), ldloc:int32[exp:int64](var_0_07))
    stfld:valuetype [mscorlib]System.Nullable`1<bool>(MasterBookBase::IsIgnore, ldloc:BattleSkillNameSettingMB[exp:MasterBookBase](this), ldloc:int32[exp:valuetype [mscorlib]System.Nullable`1<bool>](var_0_07))
    stfld:string(MasterBookBase::Memo, ldloc:BattleSkillNameSettingMB[exp:MasterBookBase](this), ldloc:int32[exp:string](var_0_07))
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

        public int GetNewLineIndex(LanguageType languageType)
        {
            if (languageType == LanguageType.jaJP)
            {
                return this.NewLineIndexTW;
            }

            return this.NewLineIndexUS;
        }
    }
}