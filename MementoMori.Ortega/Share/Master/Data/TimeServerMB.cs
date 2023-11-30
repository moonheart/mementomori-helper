using System.ComponentModel;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
    [MessagePackObject(true)]
    [Description("時差グループ")]
    public class TimeServerMB : MasterBookBase
    {
        [PropertyOrder(2)]
        [Description("時差グループタイプ")]
        public TimeServerType TimeServerType { get; set; }

        [PropertyOrder(6)]
        [Description("国コードリスト")]
        public IReadOnlyList<string> CountryCodeList { get; }

        [PropertyOrder(3)]
        [Description("デフォルト設定言語")]
        public LanguageType DefaultLanguageType { get; }

        [Description("デフォルト設定ボイス言語")]
        [PropertyOrder(4)]
        public LanguageType DefaultVoiceLanguageType { get; }

        [PropertyOrder(5)]
        [Description("表示名のキー")]
        public string DisplayNameKey { get; }

        [PropertyOrder(1)]
        [Description("時差グループ名")]
        public string Name { get; }

        [SerializationConstructor]
        public TimeServerMB(long id, bool? isIgnore, string memo, string name, TimeServerType timeServerType, string differenceDateTimeFromUtc, LanguageType defaultLanguageType,
            LanguageType defaultVoiceLanguageType, string displayNameKey, IReadOnlyList<string> countryCodeList)
            : base(id, isIgnore, memo)
        {
            Name = name;
            TimeServerType = timeServerType;
            DifferenceDateTimeFromUtc = differenceDateTimeFromUtc;
            DefaultLanguageType = defaultLanguageType;
            DefaultVoiceLanguageType = defaultVoiceLanguageType;
            DisplayNameKey = displayNameKey;
            CountryCodeList = countryCodeList;
        }

        public TimeServerMB() : base(0L, false, "")
        {
        }

        [Description("UTCとの時差(例 01:00:00)")]
        [TimeSpanString]
        [PropertyOrder(2)]
        public string DifferenceDateTimeFromUtc { get; }

        public long GetLocalTimestamp()
        {
            throw new NotImplementedException();
            /*
An exception occurred when decompiling this method (060037CB)

ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Int64 Ortega.Share.Master.Data.TimeServerMB::GetLocalTimestamp()

 ---> System.Exception: Basic block has to end with unconditional control flow.
{
    Block_0:
    stloc:int64(var_1_07, callgetter:int64(TimeUtil::get_UtcNowTimeStamp))
    stloc:TimeSpan(var_2_13, call:TimeSpan(StringExtension::ToTimeSpan, ldfld:string(TimeServerMB::DifferenceDateTimeFromUtc, ldloc:TimeServerMB(this))))
    stloc:TimeSpan(var_2_17, add:TimeSpan(ldloc:TimeSpan(var_2_13), ldloc:int64[exp:TimeSpan](var_1_07)))
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
    }
}