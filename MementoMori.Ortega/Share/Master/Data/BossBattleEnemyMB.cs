using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Battle;
using MementoMori.Ortega.Share.Data.Character;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
    [MessagePackObject(true)]
    [NotUseOnBatch]
    [Description("ボスデータ")]
    public class BossBattleEnemyMB : MasterBookBase, IBattleEnemy
    {
        [Description("アクティブスキルIDのリスト")]
        [PropertyOrder(12)]
        public IReadOnlyList<long> ActiveSkillIds { get; }

        [PropertyOrder(9)]
        [Nest(true, 1)]
        [Description("ベースパラメータ")]
        public BaseParameter BaseParameter { get; }

        [Nest(true, 1)]
        [Description("バトルパラメータ")]
        [PropertyOrder(10)]
        public BattleParameter BattleParameter { get; }

        [Description("戦闘力")]
        [PropertyOrder(5)]
        public long BattlePower { get; }

        [Description("レアリティ")]
        [PropertyOrder(8)]
        public CharacterRarityFlags CharacterRarityFlags { get; }

        [PropertyOrder(7)]
        [Description("属性")]
        public ElementType ElementType { get; }

        [Description("敵調整ID")]
        [PropertyOrder(17)]
        public long EnemyAdjustId { get; }

        [PropertyOrder(14)]
        [Description("敵キャラID")]
        public long BattleEnemyCharacterId { get; }

        [PropertyOrder(15)]
        [Description("敵武具ID")]
        public long EnemyEquipmentId { get; }

        [PropertyOrder(16)]
        [Description("専用武器レアリティ")]
        public EquipmentRarityFlags ExclusiveEquipmentRarityFlags { get; }

        [Description("敵のランク")]
        [PropertyOrder(4)]
        public long EnemyRank { get; }

        [Description("職業")]
        [PropertyOrder(6)]
        public JobFlags JobFlags { get; }

        [Description("名称キー")]
        [PropertyOrder(3)]
        public string NameKey { get; }

        [PropertyOrder(11)]
        [Description("通常攻撃として使ってくるスキルID")]
        public long NormalSkillId { get; }

        [Description("パッシブスキルIDのリスト")]
        [PropertyOrder(13)]
        public IReadOnlyList<long> PassiveSkillIds { get; }

        [Description("ユニットアイコンID")]
        [PropertyOrder(2)]
        public long UnitIconId { get; }

        [Description("ユニットアイコンタイプ")]
        [PropertyOrder(1)]
        public UnitIconType UnitIconType { get; }

        [SerializationConstructor]
        public BossBattleEnemyMB(long id, bool? isIgnore, string memo, IReadOnlyList<long> activeSkillIds, BaseParameter baseParameter, UnitIconType unitIconType, long unitIconId, BattleParameter battleParameter, long enemyRank, long battlePower, JobFlags jobFlags, ElementType elementType, CharacterRarityFlags characterRarityFlags, string nameKey, long normalSkillId, IReadOnlyList<long> passiveSkillIds, long battleEnemyCharacterId, long enemyEquipmentId, EquipmentRarityFlags exclusiveEquipmentRarityFlags, long enemyAdjustId)
            : base(id, isIgnore, memo)
        {
            ActiveSkillIds = activeSkillIds;
            BaseParameter = baseParameter;
            UnitIconType = unitIconType;
            UnitIconId = unitIconId;
            BattleParameter = battleParameter;
            EnemyRank = enemyRank;
            BattlePower = battlePower;
            JobFlags = jobFlags;
            ElementType = elementType;
            CharacterRarityFlags = characterRarityFlags;
            NameKey = nameKey;
            NormalSkillId = normalSkillId;
            PassiveSkillIds = passiveSkillIds;
            BattleEnemyCharacterId = battleEnemyCharacterId;
            EnemyEquipmentId = enemyEquipmentId;
            ExclusiveEquipmentRarityFlags = exclusiveEquipmentRarityFlags;
            EnemyAdjustId = enemyAdjustId;
        }

        public BossBattleEnemyMB() : base(0, false, "")
        {
            /*
An exception occurred when decompiling this method (060034B9)

ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void Ortega.Share.Master.Data.BossBattleEnemyMB::.ctor()

 ---> System.Exception: Basic block has to end with unconditional control flow.
{
    Block_0:
    call:void(object::.ctor, ldloc:BossBattleEnemyMB[exp:object](this))
    stloc:int32(var_0_07, ldc.i4:int32(0))
    stfld:int64(MasterBookBase::Id, ldloc:BossBattleEnemyMB[exp:MasterBookBase](this), ldloc:int32[exp:int64](var_0_07))
    stfld:valuetype [mscorlib]System.Nullable`1<bool>(MasterBookBase::IsIgnore, ldloc:BossBattleEnemyMB[exp:MasterBookBase](this), ldloc:int32[exp:valuetype [mscorlib]System.Nullable`1<bool>](var_0_07))
    stfld:string(MasterBookBase::Memo, ldloc:BossBattleEnemyMB[exp:MasterBookBase](this), ldloc:int32[exp:string](var_0_07))
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