using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Battle;
using MementoMori.Ortega.Share.Data.Character;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
    [MessagePackObject(true)]
    [NotUseOnAuth]
    [NotUseOnBatch]
    [NotUseOnSerialCodeInput]
    [Description("放置バトル敵データ")]
	public class AutoBattleEnemyMB : MasterBookBase, IBattleEnemy
	{
		[PropertyOrder(12)]
		[Description("アクティブスキルIDのリスト")]
		public IReadOnlyList<long> ActiveSkillIds { get; }

		[Nest(true, 1)]
		[PropertyOrder(9)]
		[Description("ベースパラメータ")]
		public BaseParameter BaseParameter { get; }

		[PropertyOrder(16)]
		[Description("敵キャラクターID")]
		public long BattleEnemyCharacterId { get; }

		[PropertyOrder(10)]
		[Nest(true, 1)]
		[Description("バトルパラメータ")]
		public BattleParameter BattleParameter { get; }

		[PropertyOrder(5)]
		[Description("戦闘力")]
		public long BattlePower { get; }

		[PropertyOrder(8)]
		[Description("レアリティ")]
		public CharacterRarityFlags CharacterRarityFlags { get; }

		[PropertyOrder(7)]
		[Description("属性")]
		public ElementType ElementType { get; }

		[PropertyOrder(19)]
		[Description("敵調整ID")]
		public long EnemyAdjustId { get; }

		[PropertyOrder(17)]
		[Description("敵武具ID")]
		public long EnemyEquipmentId { get; }

        [PropertyOrder(18)]
        [Description("専用武器レアリティ")]
        public EquipmentRarityFlags ExclusiveEquipmentRarityFlags { get; }

		[PropertyOrder(4)]
		[Description("敵のランク")]
		public long EnemyRank { get; }

		[Description("職業")]
		[PropertyOrder(6)]
		public JobFlags JobFlags { get; }

		[Description("名称キー")]
		[PropertyOrder(3)]
		public string NameKey { get; }

		[Description("通常攻撃として使ってくるスキルID")]
		[PropertyOrder(11)]
		public long NormalSkillId { get; }

		[PropertyOrder(13)]
		[Description("パッシブスキルIDのリスト")]
		public IReadOnlyList<long> PassiveSkillIds { get; }

		[PropertyOrder(14)]
		[Description("ドロップするキャラクター経験値")]
		public int RewardCharacterExp { get; }

		[PropertyOrder(15)]
		[Description("ドロップするプレイヤー経験値")]
		public int RewardPlayerExp { get; }

		[Description("ユニットアイコンID")]
		[PropertyOrder(2)]
		public long UnitIconId { get; }

		[PropertyOrder(1)]
		[Description("ユニットアイコンタイプ")]
		public UnitIconType UnitIconType { get; }

		[SerializationConstructor]
		public AutoBattleEnemyMB(long id, bool? isIgnore, string memo, IReadOnlyList<long> activeSkillIds, BaseParameter baseParameter, UnitIconType unitIconType, long unitIconId, BattleParameter battleParameter, long enemyRank, long battlePower, JobFlags jobFlags, ElementType elementType, CharacterRarityFlags characterRarityFlags, string nameKey, long normalSkillId, IReadOnlyList<long> passiveSkillIds, int rewardCharacterExp, int rewardPlayerExp, long battleEnemyCharacterId, long enemyEquipmentId, EquipmentRarityFlags exclusiveEquipmentRarityFlags, long enemyAdjustId)
			: base(id, isIgnore, memo)
		{
			this.ActiveSkillIds = activeSkillIds;
			this.BaseParameter = baseParameter;
			this.UnitIconType = unitIconType;
			this.UnitIconId = unitIconId;
			this.BattleParameter = battleParameter;
			this.EnemyRank = enemyRank;
			this.BattlePower = battlePower;
			this.JobFlags = jobFlags;
			this.ElementType = elementType;
			this.CharacterRarityFlags = characterRarityFlags;
			this.NameKey = nameKey;
			this.NormalSkillId = normalSkillId;
			this.PassiveSkillIds = passiveSkillIds;
			this.RewardCharacterExp = rewardCharacterExp;
			this.RewardPlayerExp = rewardPlayerExp;
			this.BattleEnemyCharacterId = battleEnemyCharacterId;
			this.EnemyEquipmentId = enemyEquipmentId;
            this.ExclusiveEquipmentRarityFlags = exclusiveEquipmentRarityFlags;
			this.EnemyAdjustId = enemyAdjustId;

		}

		public AutoBattleEnemyMB() : base(0, false, "")
		{
			{
				/*
	An exception occurred when decompiling this method (0600348F)
	
	ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void Ortega.Share.Master.Data.AutoBattleEnemyMB::.ctor()
	
	 ---> System.Exception: Basic block has to end with unconditional control flow. 
	{
		Block_0:
		call:void(object::.ctor, ldloc:AutoBattleEnemyMB[exp:object](this))
		stloc:int32(var_0_07, ldc.i4:int32(0))
		stfld:int64(MasterBookBase::Id, ldloc:AutoBattleEnemyMB[exp:MasterBookBase](this), ldloc:int32[exp:int64](var_0_07))
		stfld:valuetype [mscorlib]System.Nullable`1<bool>(MasterBookBase::IsIgnore, ldloc:AutoBattleEnemyMB[exp:MasterBookBase](this), ldloc:int32[exp:valuetype [mscorlib]System.Nullable`1<bool>](var_0_07))
		stfld:string(MasterBookBase::Memo, ldloc:AutoBattleEnemyMB[exp:MasterBookBase](this), ldloc:int32[exp:string](var_0_07))
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
}