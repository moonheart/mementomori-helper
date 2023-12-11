using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Character;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
    [NotUseOnBatch]
	[Description("キャラクター視聴可能ボイス")]
	public class CharacterDetailVoiceMB : MasterBookBase
	{
		[PropertyOrder(1)]
		[Description("キャラクターId")]
		public long CharacterId
		{
			get;
		}

		[PropertyOrder(4)]
		[Description("キャラクターボイス分類")]
		public CharacterVoiceCategory CharacterVoiceCategory
		{
			get;
		}

		[Description("Path")]
		[PropertyOrder(7)]
		[Nest(false, 0)]
		public CharacterVoicePath Path
		{
			get;
		}

		[Description("スクロールビューでの表示順（昇順）")]
		[PropertyOrder(8)]
		public int SortOrder
		{
			get;
		}

		[Description("ボイス再生時の字幕キー")]
		[PropertyOrder(3)]
		public string SubtitleKey
		{
			get;
		}

		[PropertyOrder(5)]
		[Description("解放条件")]
		public UnlockCharacterDetailVoiceType UnlockCondition
		{
			get;
		}

		[PropertyOrder(2)]
		[Description("ボタン表示用テキスト（解放済）キー")]
		public string UnlockedVoiceButtonTextKey
		{
			get;
		}

		[PropertyOrder(6)]
		[Description("クエストID")]
		public long UnlockQuestId
		{
			get;
		}

		[SerializationConstructor]
		public CharacterDetailVoiceMB(long id, bool? isIgnore, string memo, long characterId, string unlockedVoiceButtonTextKey, string subtitleKey, CharacterVoiceCategory characterVoiceCategory, UnlockCharacterDetailVoiceType unlockCondition, long unlockQuestId, CharacterVoicePath path, int sortOrder)
			:base(id, isIgnore, memo)
		{
			CharacterId = characterId;
			UnlockedVoiceButtonTextKey = unlockedVoiceButtonTextKey;
			SubtitleKey = subtitleKey;
			CharacterVoiceCategory = characterVoiceCategory;
			UnlockCondition = unlockCondition;
			UnlockQuestId = unlockQuestId;
			Path = path;
			SortOrder = sortOrder;
		}

		public CharacterDetailVoiceMB():base(0, false, "")
		{
			/*
An exception occurred when decompiling this method (060034F3)

ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void Ortega.Share.Master.Data.CharacterDetailVoiceMB::.ctor()

 ---> System.Exception: Basic block has to end with unconditional control flow. 
{
	Block_0:
	call:void(object::.ctor, ldloc:CharacterDetailVoiceMB[exp:object](this))
	stloc:int32(var_0_07, ldc.i4:int32(0))
	stfld:int64(MasterBookBase::Id, ldloc:CharacterDetailVoiceMB[exp:MasterBookBase](this), ldloc:int32[exp:int64](var_0_07))
	stfld:valuetype [mscorlib]System.Nullable`1<bool>(MasterBookBase::IsIgnore, ldloc:CharacterDetailVoiceMB[exp:MasterBookBase](this), ldloc:int32[exp:valuetype [mscorlib]System.Nullable`1<bool>](var_0_07))
	stfld:string(MasterBookBase::Memo, ldloc:CharacterDetailVoiceMB[exp:MasterBookBase](this), ldloc:int32[exp:string](var_0_07))
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
