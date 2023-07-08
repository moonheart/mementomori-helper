using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Equipment;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
    [Description("アルカナレベル")]
    [MessagePackObject(true)]
    public class CharacterCollectionLevelMB : MasterBookBase
    {
        [Description("開放後適用されるベースパラメータ")]
        [PropertyOrder(4)]
        [Nest(false, 0)]
        public IReadOnlyList<BaseParameterChangeInfo> BaseParameterChangeInfos { get; }

        [Description("開放後適用されるバトルパラメータ")]
        [PropertyOrder(5)]
        [Nest(false, 0)]
        public IReadOnlyList<BattleParameterChangeInfo> BattleParameterChangeInfos { get; }

        [Description("キャラクターレアリティ増加")]
        [PropertyOrder(6)]
        public int CharacterRarityBonus { get; }

        [PropertyOrder(3)]
        [Description("アルカナレベル解放条件")]
        public CharacterRarityFlags CharacterRarityFlags { get; }

        [PropertyOrder(1)]
        [Description("アルカナMBのID")]
        public int CollectionId { get; }

        [PropertyOrder(2)]
        [Description("アルカナレベル")]
        public int CollectionLevel { get; }

        [SerializationConstructor]
        public CharacterCollectionLevelMB(long id, bool? isIgnore, string memo, IReadOnlyList<BaseParameterChangeInfo> baseParameterChangeInfos,
            IReadOnlyList<BattleParameterChangeInfo> battleParameterChangeInfos, int characterRarityBonus, CharacterRarityFlags characterRarityFlags, int collectionId, int collectionLevel)
            : base(id, isIgnore, memo)
        {
            BaseParameterChangeInfos = baseParameterChangeInfos;
            BattleParameterChangeInfos = battleParameterChangeInfos;
            CharacterRarityBonus = characterRarityBonus;
            CharacterRarityFlags = characterRarityFlags;
            CollectionId = collectionId;
            CollectionLevel = collectionLevel;
        }

        public CharacterCollectionLevelMB() : base(0, false, "")
        {
            /*
An exception occurred when decompiling this method (060034DE)

ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void Ortega.Share.Master.Data.CharacterCollectionLevelMB::.ctor()

 ---> System.Exception: Basic block has to end with unconditional control flow. 
{
    Block_0:
    call:void(object::.ctor, ldloc:CharacterCollectionLevelMB[exp:object](this))
    stloc:int32(var_0_07, ldc.i4:int32(0))
    stfld:int64(MasterBookBase::Id, ldloc:CharacterCollectionLevelMB[exp:MasterBookBase](this), ldloc:int32[exp:int64](var_0_07))
    stfld:valuetype [mscorlib]System.Nullable`1<bool>(MasterBookBase::IsIgnore, ldloc:CharacterCollectionLevelMB[exp:MasterBookBase](this), ldloc:int32[exp:valuetype [mscorlib]System.Nullable`1<bool>](var_0_07))
    stfld:string(MasterBookBase::Memo, ldloc:CharacterCollectionLevelMB[exp:MasterBookBase](this), ldloc:int32[exp:string](var_0_07))
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