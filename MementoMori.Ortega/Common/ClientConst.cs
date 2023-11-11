using System.Drawing;
using System.Numerics;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Common.Enums;
using MementoMori.Ortega.Custom;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Enums;
using OrtegaEnvironment = MementoMori.Ortega.Common.Enums.OrtegaEnvironment;

namespace MementoMori.Ortega.Common
{
	public class ClientConst
	{
		public static class App
		{
			public const string DevVersion = "9999.0.0";

			public const string Version = "1.4.2";

			public const string ChainKey = "mementomori_uuid";

			public const int iOSBundleVersionCode = 1;

			public const int AndroidBundleVersionCode = 36;

			public static readonly Dictionary<OrtegaEnvironment, string> GameApiUrlDictionary = new();

			public static readonly Dictionary<OrtegaEnvironment, string> AuthApiUrlDictionary = new();

			public static readonly OrtegaEnvironment ConnectionType;
		}

		public static class Common
		{
			// Note: this type is marked as 'beforefieldinit'.
			static Common()
			{
				
			}

			public const long ButtonIntervalMilliseconds = 200L;

			public const long ButtonLongIntervalMilliseconds = 3000L;

			public const int DirectionDown = 1;

			public const int DirectionUp = 0;

			public const int IntervalSortingOrder = 200;

			public const int LayerDefault = 0;

			public const int LayerDefaultAfterProcessing = 8;

			public const int LayerIgnoreShareScreenShot = 6;

			public const int LayerShareScreenShotOnly = 7;

			public const int LayerWorld = 20;

			public const int MaxMonth = 12;

			public const int MinMonth = 1;

			public const int MaxDay = 31;

			public const int MinDay = 1;

			public const int PlayerIdLength = 12;

			public const int MinSettableYear = 1900;

			public const float ScrollHandleSize = 50f;

			public const int ScrollSensivity = 100;

			public const int TutorialMaxQuestId = 60;

			public static readonly Color ButtonOffColor;

			public static readonly Color ButtonOnColor;

			public static readonly Vector2 CanvasResolution;

			public static readonly Color TextActiveColor;
			
			public static readonly Color TextInactiveColor;

			public static readonly List<long> TutorialCharacterIds;

			public const int MaxDayOfWeek = 7;

			public const float DepthOfFieldAdjustValue = 1f;

			// public static readonly Vector2Int[] ResoultionSizes;
		}

		public static class Camera
		{
			public const float BaseHeight = 9f;

			public const float BaseWidth = 16f;

			public const float OrthographicSize = 15f;
		}

		public static class Network
		{
			public const int MagicOnionMaxRetryCount = 3;

			public const float MagicOnionCheckReconnectionMilliSeconds = 3000f;

			public const float MagicOnionKeepAliveMilliSeconds = 30000f;

			public const int ApiMaxRetryCount = 3;

			public const long ApiTimeOutMilliSeconds = 30000L;

			public const int FileDownloadMaxClients = 128;
		}

		public static class Dmm
		{
			public const string PointChargeURL = "https://point.dmm.com/choice/pay";

			public const string SubscriptionURL = "https://www.dmm.com/netgame/monthly/-/regist/=/service_id=861692826/app_id=683144/";
		}

		public static class Title
		{
			public const int MaxWorldCountInTab = 10;
		}

		public static class Battle
		{
			// Note: this type is marked as 'beforefieldinit'.
			static Battle()
			{
				/*
An exception occurred when decompiling this method

ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void Ortega.Common.ClientConst/Battle::.cctor()

 ---> System.Exception: Basic block has to end with unconditional control flow. 
{
	Block_0:
	stsfld:Color(Battle::ColorReportHpRecovery, ldloc:uint64[exp:Color](var_1))
	stsfld:Color(Battle::ColorReportReceiveDamage, ldloc:uint64[exp:Color](var_1))
	stsfld:class [mscorlib]System.Collections.Generic.Dictionary`2<int64, valuetype [UnityEngine.CoreModule]UnityEngine.Vector2>(Battle::MapBuildingBadgePositions, newobj:Dictionary`2[exp:class [mscorlib]System.Collections.Generic.Dictionary`2<int64, valuetype [UnityEngine.CoreModule]UnityEngine.Vector2>](Dictionary`2::.ctor))
	stloc:valuetype [UnityEngine.CoreModule]UnityEngine.GradientAlphaKey[](var_2_1E, newarr:valuetype [UnityEngine.CoreModule]UnityEngine.GradientAlphaKey[]([UnityEngine.CoreModule]UnityEngine.GradientAlphaKey, ldc.i4:int32(0)))
	stloc:int32(var_3_20, ldc.i4:int32(0))
	stelem:GradientAlphaKey([UnityEngine.CoreModule]UnityEngine.GradientAlphaKey, ldloc:valuetype [UnityEngine.CoreModule]UnityEngine.GradientAlphaKey[](var_2_1E), ldc.i4:int32(0), ldloc:int32[exp:GradientAlphaKey](var_3_20))
	stloc:int32(var_4_2A, ldc.i4:int32(0))
	stelem:GradientAlphaKey([UnityEngine.CoreModule]UnityEngine.GradientAlphaKey, ldloc:valuetype [UnityEngine.CoreModule]UnityEngine.GradientAlphaKey[](var_2_1E), ldc.i4:int32(3), ldloc:int32[exp:GradientAlphaKey](var_4_2A))
	stsfld:valuetype [UnityEngine.CoreModule]UnityEngine.GradientAlphaKey[](Battle::MaxSplashParticleAlphaKeys, ldloc:valuetype [UnityEngine.CoreModule]UnityEngine.GradientAlphaKey[](var_2_1E))
	stloc:valuetype [UnityEngine.CoreModule]UnityEngine.GradientAlphaKey[](var_5_41, newarr:valuetype [UnityEngine.CoreModule]UnityEngine.GradientAlphaKey[]([UnityEngine.CoreModule]UnityEngine.GradientAlphaKey, ldc.i4:int32(0)))
	stloc:int32(var_6_44, ldc.i4:int32(0))
	stelem:GradientAlphaKey([UnityEngine.CoreModule]UnityEngine.GradientAlphaKey, ldloc:valuetype [UnityEngine.CoreModule]UnityEngine.GradientAlphaKey[](var_5_41), ldc.i4:int32(0), ldloc:int32[exp:GradientAlphaKey](var_6_44))
	stloc:int32(var_7_51, ldc.i4:int32(0))
	stelem:GradientAlphaKey([UnityEngine.CoreModule]UnityEngine.GradientAlphaKey, ldloc:valuetype [UnityEngine.CoreModule]UnityEngine.GradientAlphaKey[](var_5_41), ldc.i4:int32(3), ldloc:int32[exp:GradientAlphaKey](var_7_51))
	stsfld:valuetype [UnityEngine.CoreModule]UnityEngine.GradientAlphaKey[](Battle::MaxSpreadParticleAlphaKeys, ldloc:valuetype [UnityEngine.CoreModule]UnityEngine.GradientAlphaKey[](var_5_41))
	stloc:valuetype [UnityEngine.CoreModule]UnityEngine.GradientAlphaKey[](var_8_6A, newarr:valuetype [UnityEngine.CoreModule]UnityEngine.GradientAlphaKey[]([UnityEngine.CoreModule]UnityEngine.GradientAlphaKey, ldc.i4:int32(0)))
	stloc:int32(var_9_6D, ldc.i4:int32(0))
	stelem:GradientAlphaKey([UnityEngine.CoreModule]UnityEngine.GradientAlphaKey, ldloc:valuetype [UnityEngine.CoreModule]UnityEngine.GradientAlphaKey[](var_8_6A), ldc.i4:int32(0), ldloc:int32[exp:GradientAlphaKey](var_9_6D))
	stloc:int32(var_10_7A, ldc.i4:int32(0))
	stelem:GradientAlphaKey([UnityEngine.CoreModule]UnityEngine.GradientAlphaKey, ldloc:valuetype [UnityEngine.CoreModule]UnityEngine.GradientAlphaKey[](var_8_6A), ldc.i4:int32(3), ldloc:int32[exp:GradientAlphaKey](var_10_7A))
	stsfld:valuetype [UnityEngine.CoreModule]UnityEngine.GradientAlphaKey[](Battle::MinSplashParticleAlphaKeys, ldloc:valuetype [UnityEngine.CoreModule]UnityEngine.GradientAlphaKey[](var_8_6A))
	stloc:valuetype [UnityEngine.CoreModule]UnityEngine.GradientAlphaKey[](var_11_93, newarr:valuetype [UnityEngine.CoreModule]UnityEngine.GradientAlphaKey[]([UnityEngine.CoreModule]UnityEngine.GradientAlphaKey, ldc.i4:int32(0)))
	stloc:int32(var_12_96, ldc.i4:int32(0))
	stelem:GradientAlphaKey([UnityEngine.CoreModule]UnityEngine.GradientAlphaKey, ldloc:valuetype [UnityEngine.CoreModule]UnityEngine.GradientAlphaKey[](var_11_93), ldc.i4:int32(0), ldloc:int32[exp:GradientAlphaKey](var_12_96))
	stelem:GradientAlphaKey([UnityEngine.CoreModule]UnityEngine.GradientAlphaKey, ldloc:valuetype [UnityEngine.CoreModule]UnityEngine.GradientAlphaKey[](var_11_93), ldc.i4:int32(2), ldloc:uint64[exp:GradientAlphaKey](var_1))
	stloc:int32(var_13_AC, ldc.i4:int32(0))
	stelem:GradientAlphaKey([UnityEngine.CoreModule]UnityEngine.GradientAlphaKey, ldloc:valuetype [UnityEngine.CoreModule]UnityEngine.GradientAlphaKey[](var_11_93), ldc.i4:int32(3), ldloc:int32[exp:GradientAlphaKey](var_13_AC))
	stsfld:valuetype [UnityEngine.CoreModule]UnityEngine.GradientAlphaKey[](Battle::MinSpreadParticleAlphaKeys, ldloc:valuetype [UnityEngine.CoreModule]UnityEngine.GradientAlphaKey[](var_11_93))
	stloc:class [mscorlib]System.Collections.Generic.Dictionary`2<valuetype Ortega.Share.Enums.CharacterColorType, valuetype [UnityEngine.CoreModule]UnityEngine.Color[]>(var_14_C4, newobj:Dictionary`2[exp:class [mscorlib]System.Collections.Generic.Dictionary`2<valuetype Ortega.Share.Enums.CharacterColorType, valuetype [UnityEngine.CoreModule]UnityEngine.Color[]>](Dictionary`2::.ctor))
	stloc:valuetype [UnityEngine.CoreModule]UnityEngine.Color[](var_15_CC, newarr:valuetype [UnityEngine.CoreModule]UnityEngine.Color[]([UnityEngine.CoreModule]UnityEngine.Color, ldc.i4:int32(0)))
	call:void(Dictionary`2::Add, ldloc:class [mscorlib]System.Collections.Generic.Dictionary`2<valuetype Ortega.Share.Enums.CharacterColorType, valuetype [UnityEngine.CoreModule]UnityEngine.Color[]>[exp:Dictionary`2](var_14_C4), ldc.i4:int32[exp:!0](1), ldloc:valuetype [UnityEngine.CoreModule]UnityEngine.Color[][exp:!1](var_15_CC))
	stloc:valuetype [UnityEngine.CoreModule]UnityEngine.Color[](var_16_DE, newarr:valuetype [UnityEngine.CoreModule]UnityEngine.Color[]([UnityEngine.CoreModule]UnityEngine.Color, ldc.i4:int32(0)))
	stelem:Color([UnityEngine.CoreModule]UnityEngine.Color, ldloc:valuetype [UnityEngine.CoreModule]UnityEngine.Color[](var_16_DE), ldc.i4:int32(0), ldloc:uint64[exp:Color](var_17))
	stelem:Color([UnityEngine.CoreModule]UnityEngine.Color, ldloc:valuetype [UnityEngine.CoreModule]UnityEngine.Color[](var_16_DE), ldc.i4:int32(2), ldloc:uint64[exp:Color](var_18))
	stelem:Color([UnityEngine.CoreModule]UnityEngine.Color, ldloc:valuetype [UnityEngine.CoreModule]UnityEngine.Color[](var_16_DE), ldc.i4:int32(4), ldloc:uint64[exp:Color](var_19))
	call:void(Dictionary`2::Add, ldloc:class [mscorlib]System.Collections.Generic.Dictionary`2<valuetype Ortega.Share.Enums.CharacterColorType, valuetype [UnityEngine.CoreModule]UnityEngine.Color[]>[exp:Dictionary`2](var_14_C4), ldc.i4:int32[exp:!0](2), ldloc:valuetype [UnityEngine.CoreModule]UnityEngine.Color[][exp:!1](var_16_DE))
	stloc:valuetype [UnityEngine.CoreModule]UnityEngine.Color[](var_20_10E, newarr:valuetype [UnityEngine.CoreModule]UnityEngine.Color[]([UnityEngine.CoreModule]UnityEngine.Color, ldc.i4:int32(0)))
	call:void(Dictionary`2::Add, ldloc:class [mscorlib]System.Collections.Generic.Dictionary`2<valuetype Ortega.Share.Enums.CharacterColorType, valuetype [UnityEngine.CoreModule]UnityEngine.Color[]>[exp:Dictionary`2](var_14_C4), ldc.i4:int32[exp:!0](3), ldloc:valuetype [UnityEngine.CoreModule]UnityEngine.Color[][exp:!1](var_20_10E))
	stloc:valuetype [UnityEngine.CoreModule]UnityEngine.Color[](var_21_120, newarr:valuetype [UnityEngine.CoreModule]UnityEngine.Color[]([UnityEngine.CoreModule]UnityEngine.Color, ldc.i4:int32(0)))
	stelem:Color([UnityEngine.CoreModule]UnityEngine.Color, ldloc:valuetype [UnityEngine.CoreModule]UnityEngine.Color[](var_21_120), ldc.i4:int32(0), ldloc:uint64[exp:Color](var_22))
	call:void(Dictionary`2::Add, ldloc:class [mscorlib]System.Collections.Generic.Dictionary`2<valuetype Ortega.Share.Enums.CharacterColorType, valuetype [UnityEngine.CoreModule]UnityEngine.Color[]>[exp:Dictionary`2](var_14_C4), ldc.i4:int32[exp:!0](4), ldloc:valuetype [UnityEngine.CoreModule]UnityEngine.Color[][exp:!1](var_21_120))
	stloc:valuetype [UnityEngine.CoreModule]UnityEngine.Color[](var_23_13C, newarr:valuetype [UnityEngine.CoreModule]UnityEngine.Color[]([UnityEngine.CoreModule]UnityEngine.Color, ldc.i4:int32(0)))
	stelem:Color([UnityEngine.CoreModule]UnityEngine.Color, ldloc:valuetype [UnityEngine.CoreModule]UnityEngine.Color[](var_23_13C), ldc.i4:int32(0), ldloc:uint64[exp:Color](var_24))
	stelem:Color([UnityEngine.CoreModule]UnityEngine.Color, ldloc:valuetype [UnityEngine.CoreModule]UnityEngine.Color[](var_23_13C), ldc.i4:int32(6), ldloc:uint64[exp:Color](var_25))
	call:void(Dictionary`2::Add, ldloc:class [mscorlib]System.Collections.Generic.Dictionary`2<valuetype Ortega.Share.Enums.CharacterColorType, valuetype [UnityEngine.CoreModule]UnityEngine.Color[]>[exp:Dictionary`2](var_14_C4), ldc.i4:int32[exp:!0](5), ldloc:valuetype [UnityEngine.CoreModule]UnityEngine.Color[][exp:!1](var_23_13C))
	stsfld:class [mscorlib]System.Collections.Generic.Dictionary`2<valuetype Ortega.Share.Enums.CharacterColorType, valuetype [UnityEngine.CoreModule]UnityEngine.Color[]>(Battle::ParticleSplashColorDict, ldloc:class [mscorlib]System.Collections.Generic.Dictionary`2<valuetype Ortega.Share.Enums.CharacterColorType, valuetype [UnityEngine.CoreModule]UnityEngine.Color[]>(var_14_C4))
	stloc:class [mscorlib]System.Collections.Generic.Dictionary`2<valuetype Ortega.Share.Enums.CharacterColorType, valuetype [UnityEngine.CoreModule]UnityEngine.Color[]>(var_26_168, newobj:Dictionary`2[exp:class [mscorlib]System.Collections.Generic.Dictionary`2<valuetype Ortega.Share.Enums.CharacterColorType, valuetype [UnityEngine.CoreModule]UnityEngine.Color[]>](Dictionary`2::.ctor))
	stloc:valuetype [UnityEngine.CoreModule]UnityEngine.Color[](var_27_170, newarr:valuetype [UnityEngine.CoreModule]UnityEngine.Color[]([UnityEngine.CoreModule]UnityEngine.Color, ldc.i4:int32(0)))
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

			public const int ClearPartyMinChapter = 5;

			public const float MapCameraMaxOrthographicSize = 7f;

			public const float MapCameraMinOrthographicSize = 3.38f;

			public const float QuestIconParticleScaleEasy = 0.9f;

			public const float QuestIconParticleScaleHard = 1.5f;

			public const float QuestIconScaleEasy = 0.85f;

			public const float QuestIconScaleHard = 1.4f;

			public const long QuestTerritoryMaxChapterId = 12L;

			public const int QuestTerritoryMaxLevel = 6;

			public static readonly Color ColorReportGiveDamage;

			public static readonly Color ColorReportHpRecovery;

			public static readonly Color ColorReportReceiveDamage;

			public static readonly Dictionary<long, Vector2> MapBuildingBadgePositions;

			// public static readonly GradientAlphaKey[] MaxSplashParticleAlphaKeys;
			//
			// public static readonly GradientAlphaKey[] MaxSpreadParticleAlphaKeys;
			//
			// public static readonly GradientAlphaKey[] MinSplashParticleAlphaKeys;
			//
			// public static readonly GradientAlphaKey[] MinSpreadParticleAlphaKeys;

			public static readonly Dictionary<CharacterColorType, Color[]> ParticleSplashColorDict;

			public static readonly Dictionary<CharacterColorType, Color[]> ParticleSpreadColorDict;

			public static readonly int[] SpeedList;
		}

		public static class Guild
		{
			public const int GrandBattleGreenColorRanking = 48;

			public const int GuildBattleGreenColorRanking = 16;

			public const int InputFieldBattlePowerMaxLetter = 14;

			public const int MaxGuildDescriptionLength = 60;

			public const int MaxGuildNameLength = 10;
		}

		public static class LocalRaid
		{
			public const long BattleStartWaitTimeMilliseconds = 3000L;

			public const int MaxInvitation = 5;

			public const int RoomBattlePowerMaxLetter = 14;

			public const long RoomConditionNoneBattlePower = 0L;

			public const int RoomConditionNonePassword = -1;

			public const int RoomPasswordMaxLetter = 4;

			public const long SendInvitationWaitTimeMilliseconds = 60000L;
		}

		public static class Scenario
		{
			// Note: this type is marked as 'beforefieldinit'.
			static Scenario()
			{
				// Dictionary<ScenarioType, int> dictionary = new();
				// int num = 0;
				// dictionary.Add(num, 2);
				// dictionary.Add((uint)1, 2);
				// ClientConst.Scenario.CharacterCount = dictionary;
				// ClientConst.Scenario.TextColor = ClientConst.Text.ColorBlack;
			}

			public const int TextSize = 40;

			// public static readonly Dictionary<ScenarioType, int> CharacterCount;

			public static readonly Color TextColor;

			public static readonly Color TextOutlineColor;
		}

		public static class Dialog
		{
			public static readonly Vector3 ButtonPositionCenter;

			public static readonly Vector3 ThreeButtonPositionLeft;

			public static readonly Vector3 ThreeButtonPositionRight;

			public static readonly Vector3 TwoButtonPositionLeft;

			public static readonly Vector3 TwoButtonPositionRight;
		}

		public static class Character
		{
			public static Color NormalLevelColor
			{
				get
				{
					return ClientConst.Text.ColorWhite;
				}
			}

			// Note: this type is marked as 'beforefieldinit'.
			static Character()
			{
				// Dictionary<CharacterColorType, GradientColorData> dictionary = new();
				// uint num;
				// ulong num2;
				// int num3;
				// GradientColorData gradientColorData = new GradientColorData(num, num, num, (uint)num2, (float)num3, (float)num3);
				// num3 = 0;
				// int num4 = 0;
				// dictionary.Add(num4, gradientColorData);
				// GradientColorData gradientColorData2;
				// dictionary.Add(1, gradientColorData2);
				// GradientColorData gradientColorData3;
				// dictionary.Add(2, gradientColorData3);
				// GradientColorData gradientColorData4;
				// dictionary.Add(3, gradientColorData4);
				// uint num5;
				// uint num6;
				// uint num7;
				// ulong num8;
				// GradientColorData gradientColorData5 = new GradientColorData(num5, num6, num7, (uint)num8, (float)num3, (float)num3);
				// dictionary.Add(4, gradientColorData5);
				// uint num9;
				// uint num10;
				// uint num11;
				// ulong num12;
				// GradientColorData gradientColorData6 = new GradientColorData(num9, num10, num11, (uint)num12, (float)num3, (float)num3);
				// dictionary.Add(5, gradientColorData6);
				// ClientConst.Character.ColorDictionary = dictionary;
				// BaseParameterType[] array = new BaseParameterType[0];
				// RuntimeHelpers.InitializeArray(array, fieldof(<PrivateImplementationDetails>.CF97ADEEDB59E05BFD73A2B4C2A8885708C4F4F70C84C64B27120E72AB733B72).FieldHandle);
				// ClientConst.Character.DisplayBaseParameterTypes = array;
				// ClientConst.Character.EyeTrackingDefaultDirectionScale = 0;
				// ClientConst.Character.LevelLinkLevelColor = ClientConst.Text.ColorUnique1;
			}

			public const CharacterRarityFlags BaseCharacterRarityFlagsOfStars = CharacterRarityFlags.LR;

			public const int BattleResultLoseTimelineId = 21;

			public const int BattleResultWinTimelineId = 20;

			public const CharacterRarityFlags BulkRankUpMaxRarityFlags = CharacterRarityFlags.SR;

			public const CharacterRarityFlags BulkRankUpMinRarityFlags = CharacterRarityFlags.R;

			public const int CannotRankResetMaxLevelAndRarityCharacterCount = 1;

			public const float EmotionBlendTime = 0.3f;

			public const float EmotionLookBlendTime = 0.15f;

			public const CharacterRarityFlags EnableShareButtonRarityFlags = CharacterRarityFlags.LR;

			public const int LevelLinkMaxSubLevel = 9;

			public const int MaxCacheCount = 5;

			public const int MaxEvolutionActionAmount = 6;

			public const float PinchScaleMax = 5000f;

			public const float PinchScaleMin = 1000f;

			public const float PinchScaleRatio = 5f;

			public const int RankUpMaxMaterialCount = 2;

			public const CharacterRarityFlags RankUpWarningRarityFlags = CharacterRarityFlags.SR;

			public const int TimelineIdleActionId = 0;

			// public static readonly Dictionary<CharacterColorType, GradientColorData> ColorDictionary;

			public static readonly BaseParameterType[] DisplayBaseParameterTypes;

			// public static readonly Rect EyeTrackingDefaultDirectionScale;

			public static Color LevelLinkLevelColor;

			public static readonly Vector2 PinchPositionMax;

			public static readonly Vector2 PinchPositionMin;
		}

		public static class Equipment
		{
			// Note: this type is marked as 'beforefieldinit'.
			static Equipment()
			{
				// KeyValuePair<ParameterType, int>[] array = new KeyValuePair<ParameterType, int>[9];
				// int num = 0;
				// array[0] = array;
				// array[1] = array;
				// array[2] = num;
				// array[3] = num;
				// array[4] = num;
				// array[5] = num;
				// array[6] = num;
				// array[7] = num;
				// array[8] = num;
				// ClientConst.Equipment.AutoSetOrderArrayNotEquippedAttack = array;
				// KeyValuePair<ParameterType, int>[] array2 = new KeyValuePair<ParameterType, int>[0];
				// array2[0] = num;
				// array2[1] = num;
				// array2[2] = num;
				// array2[3] = num;
				// array2[4] = num;
				// array2[5] = num;
				// array2[6] = num;
				// ClientConst.Equipment.AutoSetOrderArrayNotEquippedDefense = array2;
				// KeyValuePair<ParameterType, int>[] array3 = new KeyValuePair<ParameterType, int>[0];
				// array3[1] = array3;
				// array3[2] = array3;
				// array3[3] = array3;
				// array3[4] = array3;
				// array3[5] = array3;
				// array3[6] = array3;
				// array3[7] = array3;
				// array3[8] = array3;
				// ClientConst.Equipment.AutoSetOrderArraySniperAttack = array3;
				// KeyValuePair<ParameterType, int>[] array4 = new KeyValuePair<ParameterType, int>[0];
				// array4[1] = array4;
				// array4[2] = array4;
				// array4[3] = array4;
				// array4[4] = array4;
				// array4[5] = array4;
				// array4[6] = array4;
				// ClientConst.Equipment.AutoSetOrderArraySniperDefense = array4;
				// KeyValuePair<ParameterType, int>[] array5 = new KeyValuePair<ParameterType, int>[0];
				// array5[1] = array5;
				// array5[2] = array5;
				// array5[3] = array5;
				// array5[4] = array5;
				// array5[5] = array5;
				// array5[6] = array5;
				// array5[7] = array5;
				// array5[8] = array5;
				// ClientConst.Equipment.AutoSetOrderArraySorcererAttack = array5;
				// KeyValuePair<ParameterType, int>[] array6 = new KeyValuePair<ParameterType, int>[0];
				// array6[1] = array6;
				// array6[2] = array6;
				// array6[3] = array6;
				// array6[4] = array6;
				// array6[5] = array6;
				// array6[6] = array6;
				// ClientConst.Equipment.AutoSetOrderArraySorcererDefense = array6;
				// KeyValuePair<ParameterType, int>[] array7 = new KeyValuePair<ParameterType, int>[0];
				// array7[1] = array7;
				// array7[2] = array7;
				// array7[3] = array7;
				// array7[4] = array7;
				// array7[5] = array7;
				// array7[6] = array7;
				// array7[7] = array7;
				// array7[8] = array7;
				// ClientConst.Equipment.AutoSetOrderArrayWarriorAttack = array7;
				// KeyValuePair<ParameterType, int>[] array8 = new KeyValuePair<ParameterType, int>[0];
				// array8[1] = array8;
				// array8[2] = array8;
				// array8[3] = array8;
				// array8[4] = array8;
				// array8[5] = array8;
				// array8[6] = array8;
				// ClientConst.Equipment.AutoSetOrderArrayWarriorDefense = array8;
				// ClientConst.Equipment.ColorTextDefault = ClientConst.Text.ColorBlack;
				// ClientConst.Equipment.ColorTextInheritance = ClientConst.Text.ColorDarkGreen;
				// ClientConst.Equipment.ColorTextUp = ClientConst.Text.ColorDarkGreen;
				// KeyValuePair<EquipmentRarityFlags, long>[] array9 = new KeyValuePair<EquipmentRarityFlags, long>[6];
				// int num2 = 0;
				// array9[0] = num2;
				// array9[2] = num2;
				// array9[4] = num2;
				// array9[6] = num2;
				// array9[8] = num2;
				// array9[10] = num2;
				// ClientConst.Equipment.ExclusiveRarityLevels = array9;
			}

			public const int MaxAscendMaterialItem = 1000;

			public const int MaxReinforcementCount = 10;

			public const int MinCopperMedalLevel = 180;

			public const int MinGoldMedalLevel = 220;

			public const int MinRainbowMedalLevel = 240;

			public const int MinSilverMedalLevel = 200;

			public const long RefineConfirmChangeLevel = 241L;

			public const long RefineConfirmHighLevelPercent = 55L;

			public const long RefineConfirmLowLevelPercent = 50L;

			public const long RefineMaxParameterPercent = 60L;

			public static readonly KeyValuePair<ParameterType, int>[] AutoSetOrderArrayNotEquippedAttack;

			public static readonly KeyValuePair<ParameterType, int>[] AutoSetOrderArrayNotEquippedDefense;

			public static readonly KeyValuePair<ParameterType, int>[] AutoSetOrderArraySniperAttack;

			public static readonly KeyValuePair<ParameterType, int>[] AutoSetOrderArraySniperDefense;

			public static readonly KeyValuePair<ParameterType, int>[] AutoSetOrderArraySorcererAttack;

			public static readonly KeyValuePair<ParameterType, int>[] AutoSetOrderArraySorcererDefense;

			public static readonly KeyValuePair<ParameterType, int>[] AutoSetOrderArrayWarriorAttack;

			public static readonly KeyValuePair<ParameterType, int>[] AutoSetOrderArrayWarriorDefense;

			public static readonly Color ColorImageDefault;

			public static readonly Color ColorImageInheritance;

			public static readonly Color ColorTextDefault;

			public static readonly Color ColorTextInheritance;

			public static readonly Color ColorTextUp;

			public static readonly KeyValuePair<EquipmentRarityFlags, long>[] ExclusiveRarityLevels;
		}

		public static class Player
		{
			public const int BirthdayVoiceAddDay = 7;

			public const int CommentMaxLetter = 60;

			public const int NameMaxLetter = 10;

			public const int NameOmittedLength = 5;
		}

		public static class Formation
		{
			public const int NameMaxLetter = 10;
		}

		public static class Login
		{
			public static readonly Color DefaultColor;

			public static readonly Color OnlineColor;
		}

		public static class Text
		{
			// Note: this type is marked as 'beforefieldinit'.
			static Text()
			{
				/*
An exception occurred when decompiling this method

ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void Ortega.Common.ClientConst/Text::.cctor()

 ---> System.Exception: Basic block has to end with unconditional control flow. 
{
	Block_0:
	stsfld:class [mscorlib]System.Collections.Generic.Dictionary`2<valuetype Ortega.Common.Enums.TextColorType, valuetype [UnityEngine.CoreModule]UnityEngine.Color>(Text::ColorDictionary, newobj:Dictionary`2[exp:class [mscorlib]System.Collections.Generic.Dictionary`2<valuetype Ortega.Common.Enums.TextColorType, valuetype [UnityEngine.CoreModule]UnityEngine.Color>](Dictionary`2::.ctor))
	stloc:int32(var_1_0D, ldc.i4:int32(0))
	stloc:class [mscorlib]System.Collections.Generic.Dictionary`2<valuetype Ortega.Common.Enums.TextOutlineType, class Ortega.Common.Data.TextOutlineData>(var_2_13, newobj:Dictionary`2[exp:class [mscorlib]System.Collections.Generic.Dictionary`2<valuetype Ortega.Common.Enums.TextOutlineType, class Ortega.Common.Data.TextOutlineData>](Dictionary`2::.ctor))
	call:void(Dictionary`2::Add, ldloc:class [mscorlib]System.Collections.Generic.Dictionary`2<valuetype Ortega.Common.Enums.TextOutlineType, class Ortega.Common.Data.TextOutlineData>[exp:Dictionary`2](var_2_13), ldc.i4:int32[exp:!0](1), ldloc:TextOutlineData[exp:!1](var_3))
	stloc:valuetype [UnityEngine.CoreModule]UnityEngine.Vector2[](var_6_22, newarr:valuetype [UnityEngine.CoreModule]UnityEngine.Vector2[]([UnityEngine.CoreModule]UnityEngine.Vector2, ldc.i4:int32(0)))
	stelem:Vector2([UnityEngine.CoreModule]UnityEngine.Vector2, ldloc:valuetype [UnityEngine.CoreModule]UnityEngine.Vector2[](var_6_22), ldc.i4:int32(0), conv.u8:uint64[exp:Vector2](ldc.i8:int64[exp:uint64](1065353216)))
	stelem:Vector2([UnityEngine.CoreModule]UnityEngine.Vector2, ldloc:valuetype [UnityEngine.CoreModule]UnityEngine.Vector2[](var_6_22), ldc.i4:int32(0), conv.u8:uint64[exp:Vector2](ldc.i8:int64[exp:uint64](3212836864)))
	stelem:Vector2([UnityEngine.CoreModule]UnityEngine.Vector2, ldloc:valuetype [UnityEngine.CoreModule]UnityEngine.Vector2[](var_6_22), ldc.i4:int32(1), conv.u8:uint64[exp:Vector2](ldc.i8:int64[exp:uint64](1073741824)))
	stelem:Vector2([UnityEngine.CoreModule]UnityEngine.Vector2, ldloc:valuetype [UnityEngine.CoreModule]UnityEngine.Vector2[](var_6_22), ldc.i4:int32(1), conv.u8:uint64[exp:Vector2](ldc.i8:int64[exp:uint64](3221225472)))
	stloc:valuetype [UnityEngine.CoreModule]UnityEngine.Vector2[](var_8_6C, newarr:valuetype [UnityEngine.CoreModule]UnityEngine.Vector2[]([UnityEngine.CoreModule]UnityEngine.Vector2, ldc.i4:int32(0)))
	stelem:Vector2([UnityEngine.CoreModule]UnityEngine.Vector2, ldloc:valuetype [UnityEngine.CoreModule]UnityEngine.Vector2[](var_8_6C), ldc.i4:int32(0), conv.u8:uint64[exp:Vector2](ldc.i8:int64[exp:uint64](1065353216)))
	stelem:Vector2([UnityEngine.CoreModule]UnityEngine.Vector2, ldloc:valuetype [UnityEngine.CoreModule]UnityEngine.Vector2[](var_8_6C), ldc.i4:int32(0), conv.u8:uint64[exp:Vector2](ldc.i8:int64[exp:uint64](3212836864)))
	stelem:Vector2([UnityEngine.CoreModule]UnityEngine.Vector2, ldloc:valuetype [UnityEngine.CoreModule]UnityEngine.Vector2[](var_8_6C), ldc.i4:int32(1), conv.u8:uint64[exp:Vector2](ldc.i8:int64[exp:uint64](1073741824)))
	stelem:Vector2([UnityEngine.CoreModule]UnityEngine.Vector2, ldloc:valuetype [UnityEngine.CoreModule]UnityEngine.Vector2[](var_8_6C), ldc.i4:int32(1), conv.u8:uint64[exp:Vector2](ldc.i8:int64[exp:uint64](3221225472)))
	stloc:valuetype [UnityEngine.CoreModule]UnityEngine.Vector2[](var_10_B6, newarr:valuetype [UnityEngine.CoreModule]UnityEngine.Vector2[]([UnityEngine.CoreModule]UnityEngine.Vector2, ldc.i4:int32(0)))
	stelem:Vector2([UnityEngine.CoreModule]UnityEngine.Vector2, ldloc:valuetype [UnityEngine.CoreModule]UnityEngine.Vector2[](var_10_B6), ldc.i4:int32(0), conv.u8:uint64[exp:Vector2](ldc.i8:int64[exp:uint64](1065353216)))
	stelem:Vector2([UnityEngine.CoreModule]UnityEngine.Vector2, ldloc:valuetype [UnityEngine.CoreModule]UnityEngine.Vector2[](var_10_B6), ldc.i4:int32(0), conv.u8:uint64[exp:Vector2](ldc.i8:int64[exp:uint64](3212836864)))
	stelem:Vector2([UnityEngine.CoreModule]UnityEngine.Vector2, ldloc:valuetype [UnityEngine.CoreModule]UnityEngine.Vector2[](var_10_B6), ldc.i4:int32(1), conv.u8:uint64[exp:Vector2](ldc.i8:int64[exp:uint64](1073741824)))
	stelem:Vector2([UnityEngine.CoreModule]UnityEngine.Vector2, ldloc:valuetype [UnityEngine.CoreModule]UnityEngine.Vector2[](var_10_B6), ldc.i4:int32(1), conv.u8:uint64[exp:Vector2](ldc.i8:int64[exp:uint64](3221225472)))
	stsfld:class [mscorlib]System.Collections.Generic.Dictionary`2<valuetype Ortega.Common.Enums.TextOutlineType, class Ortega.Common.Data.TextOutlineData>(Text::OutlineDictionary, ldloc:class [mscorlib]System.Collections.Generic.Dictionary`2<valuetype Ortega.Common.Enums.TextOutlineType, class Ortega.Common.Data.TextOutlineData>(var_2_13))
	stsfld:class [mscorlib]System.Collections.Generic.Dictionary`2<valuetype Ortega.Common.Enums.TextRarityType, valuetype [UnityEngine.CoreModule]UnityEngine.Color>(Text::RarityColorDictionary, newobj:Dictionary`2[exp:class [mscorlib]System.Collections.Generic.Dictionary`2<valuetype Ortega.Common.Enums.TextRarityType, valuetype [UnityEngine.CoreModule]UnityEngine.Color>](Dictionary`2::.ctor))
	stloc:class [mscorlib]System.Collections.Generic.Dictionary`2<valuetype Ortega.Common.Enums.TextRarityType, class Ortega.Common.Data.GradientColorData>(var_17_10F, newobj:Dictionary`2[exp:class [mscorlib]System.Collections.Generic.Dictionary`2<valuetype Ortega.Common.Enums.TextRarityType, class Ortega.Common.Data.GradientColorData>](Dictionary`2::.ctor))
	stloc:GradientColorData(var_18_120, newobj:GradientColorData(GradientColorData::.ctor, ldloc:uint32(var_19), ldloc:uint32(var_21), ldloc:uint32(var_20), ldloc:uint64[exp:uint32](var_22), ldloc:int32[exp:float32](var_1_0D), ldloc:int32[exp:float32](var_1_0D)))
	stloc:GradientColorData(var_23_131, newobj:GradientColorData(GradientColorData::.ctor, ldloc:uint32(var_24), ldloc:uint32(var_26), ldloc:uint32(var_25), ldloc:uint64[exp:uint32](var_27), ldloc:int32[exp:float32](var_1_0D), ldloc:int32[exp:float32](var_1_0D)))
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

			public const char CarriageReturn = '\r';

			public const string EllipsisText = "...";

			public const char NewLine = '\n';

			public const char PasswordHiddenWord = '*';

			public const char WhiteSpace = ' ';

			public static readonly Color ColorBlack;

			public static readonly Color ColorDarkBlue;

			public static readonly Color ColorDarkGreen;

			public static readonly Color ColorDarkYellow;

			public static readonly Color ColorGray;

			public static readonly Color ColorLightBlue;

			public static readonly Color ColorLightGreen;

			public static readonly Color ColorLightYellow;

			public static readonly Color ColorPink;

			public static readonly Color ColorPurple;

			public static readonly Color ColorRed;

			public static readonly Color ColorUnique1;

			public static readonly Color ColorUnique2;

			public static readonly Color ColorUnique3;

			public static readonly Color ColorUnique4;

			public static readonly Color ColorUnique5;

			public static readonly Color ColorWhite;

			public static readonly Dictionary<TextColorType, Color> ColorDictionary;

			public static readonly Color OutlineColorBlack;

			// public static readonly Dictionary<TextOutlineType, TextOutlineData> OutlineDictionary;

			public static readonly Dictionary<TextRarityType, Color> RarityColorDictionary;

			// public static readonly Dictionary<TextRarityType, GradientColorData> RarityGradientColorDataDictionary;
		}

		public static class Chat
		{
			public const int MaxLetter = 80;

			public const int MaxPrivateChatPlayerCount = 20;
		}

		public static class Gacha
		{
			// Note: this type is marked as 'beforefieldinit'.
			static Gacha()
			{
				
			}

			public const float ResultFadeTime = 0.5f;

			public const int ShowCharacterReleaseGuidanceCondition = 5;

			public const int ShowTimeDays = 365;

			public const long TabIdCastingStone = 16L;

			public const long TabIdsHolyAngel = 12L;

			public static readonly UserItem[] RelicItems;

			public static readonly Dictionary<GachaTitleColorType, Color[]> TitleColorDictionary;
		}

		public static class DungeonBattle
		{
			public const int BaseTypeBroken = 2;

			public const int BaseTypeNormal = 1;

			public const int MapSizeX = 3;

			public const int MapSizeY = 3;
		}

		public static class PlayerPrefsKey
		{
			public const string AgeVerifyBirthDay = "AgeVerifyBirthDay";

			public const string AndroidNotificationIds = "AndroidNotificationIds";

			public const string AppsFlyerFirstLoginLocalTimeStamp = "AppsFlyerFirstLoginLocalTimeStamp";

			public const string AppsFlyerGachaCount = "AppsFlyerGachaCount";

			public const string AppsFlyerLastLoginDate = "AppsFlyerLastLoginDate";

			public const string AppsFlyerLoginCount = "AppsFlyerLoginCount";

			public const string AppsFlyerSendEventList = "AppsFlyerSendEventList";

			public const string ArcanaSortInfo = "ArcanaSortInfo";

			public const string AutoBattleDeckDetailOnOff = "AutoBattleDeckDetailOnOff";

			public const string AutoBattlePartySameAsBoss = "AutoBattlePartySameAsBoss";

			public const string AutoBattleShowChangeWarningTime = "AutoBattleShowChangeWarningTime";

			public const string BattleClearedLastQuest = "BattleClearedLastQuest";

			public const string BattleSpeedIndex = "BattleSpeedIndex";

			public const string BattleReplaySpeedIndex = "BattleReplaySpeedIndex";

			public const string BgmEventActionType = "BgmEventActionType";

			public const string BgmMute = "BgmMute";

			public const string BgmVolume = "BgmVolumeSetting";

			public const string BountyQuestBadgeOffQuestIds = "BountyQuestBadgeOffQuestIds";

			public const string BountyQuestBoardRankBadge = "BountyQuestBoardRankBadge";

			public const string BountyQuestFormationSortInfo = "BountyQuestFormationSortInfo";

			public const string BountyQuestSupportFormationSortInfo = "BountyQuestSupportFormationSortInfo";

			public const string BountyQuestSupportSettingSortInfo = "BountyQuestSupportSettingSortInfo ";

			public const string BulkCastingEquipmentRarityFlags = "BulkCastingEquipmentRarityFlags";

			public const string CanSkipTutorial = "CanSkipTutorial";

			public const string CharacterListCharacterSortInfo = "CharacterListCharacterSortInfo";

			public const string CommonFormationSortInfo = "CommonFormationSortInfo";

			public const string ConnectionId = "ConnectionId";

			public const string DeckDetailOnOff = "DeckDetailOnOff";

			public const string DmmOneTimeToken = "DmmOneTimeToken";

			public const string DungeonBattleCharacterSortInfo = "DungeonBattleCharacterSortInfo";

			public const string EquipmentAscendMaterialSortInfo = "EquipmentAscendMaterialSortInfo";

			public const string ExchangeBadgeDate = "ExchangeBadgeDate";

			public const string FirstStart = "FirstStart";

			public const string FirstTimeExitApplication = "FirstTimeExitApplication";

			public const string FirstTimeExitApplicationViewControllerNames = "FirstTimeExitApplicationViewControllerNames";

			public const string FormationList = "FormationList";

			public const string GachaDestinyCharacterBadgeOffIds = "GachaDestinyCharacterBadgeOffIds";

			public const string GachaElementGroup = "GachaElementGroup";

			public const string GachaPurchaseConfirmationInvalidDate = "GachaPurchaseConfirmationInvalidDate";

			public const string GachaSelectListMaxRarityBadgeCharacterIds = "GachaSelectListMaxRarityBadgeCharacterIds";

			public const string GachaSelectListMaxRarityBadgeOffCharacterIds = "GachaSelectListMaxRarityBadgeOffCharacterIds";

			public const string GachaLastCheckStarsGuidanceStartTime = "GachaLastCheckStarsGuidanceStartTime";

			public const string GameSettingCurrency = "GameSettingCurrency";

			public const string GameSettingFps = "GameSettingFps";

			public const string GameSettingLiveMode = "GameSettingLiveMode";

			public const string GameSettingTapEffect = "GameSettingTapEffect";

			public const string GlobalGvgLastStrategyTimeAnimationTimeStamp = "GlobalGvgLastStrategyTimeAnimationTimeStamp";

			public const string GuildApplyPlayerSortInfo = "GuildApplyPlayerSortInfo";

			public const string GuildMemberSortInfo = "GuildMemberSortInfo";

			public const string GvgPartyAttributeFilterInfo = "GvgPartyAttributeFilterInfo";

			public const string GvgPartyNavigateToBattle = "GvgPartyNavigateToBattle";

			public const string GvgPartySetting = "GvgPartySetting";

			public const string GvgPartySortInfo = "GvgPartySortInfo";

			public const string IsReviewGuided1 = "IsReviewGuided1";

			public const string IsReviewGuided2 = "IsReviewGuided2";

			public const string IsSoundWarning = "IsSoundWarning";

			public const string IsTermsConsent = "IsTermsConsent";

			public const string ItemBoxSelectCharacterSortInfo = "ItemBoxSelectCharacterSortInfo";

			public const string JingleVolume = "JingleVolume";

			public const string LamentDownloadBadgeFlags = "LamentDownloadBadgeFlags";

			public const string LanguageType = "LanguageType";

			public const string LastDateTimeCheckLegendLeague = "LastDateTimeCheckLegendLeague";

			public const string LastReviewGuidance2DateTime = "LastReviewGuidance2DateTime";

			public const string LastSelectChatTabType = "LastSelectChatTabType";

			public const string LatestGuildChatTimeStamp = "LatestGuildChatTimeStamp";

			public const string LevelLinkCharacterSelectSortInfo = "LevelLinkCharacterSelectSortInfo";

			public const string LevelLinkEliminationCoolTimeWaitIndexes = "LevelLinkEliminationCoolTimeWaitIndexes";

			public const string LevelLinkMemberPlacement = "LevelLinkMemberPlacement";

			public const string LevelLinkSlotSortInfo = "LevelLinkSlotSortInfo";

			public const string LimitedMissionBadgeOffId = "LimitedMissionBadgeOffId";

			public const string LimitedMissionBadgeOffTypes = "LimitedMissionBadgeOffTypes";

			public const string LimitedMissionShowId = "LimitedMissionShowId";

			public const string LocalGvgLastStrategyTimeAnimationTimeStamp = "LocalGvgLastStrategyTimeAnimationTimeStamp";

			public const string LocalRaidChangedFormation = "LocalRaidChangedFormation";

			public const string LocalRaidFirstFormation = "LocalRaidFirstFormation";

			public const string LocalRaidRoomBattlePowerCondition = "LocalRaidRoomBattlePowerCondition";

			public const string LocalRaidRoomBattlePowerConditionOnOff = "LocalRaidRoomBattlePowerConditionOnOff";

			public const string LocalRaidRoomPasswordCondition = "LocalRaidRoomPasswordCondition";

			public const string LocalRaidRoomPasswordConditionOnOff = "LocalRaidRoomPasswordConditionOnOff";

			public const string LocalRaidSentInvitation = "LocalRaidSentInvitation";

			public const string MemoryBadge = "MemoryBadge";

			public const string MissionBadgeOffTypes = "MissionBadgeOffTypes";

			public const string MonthlyPurchaseData = "MonthlyPurchaseData";

			public const string MyPageFavoriteCharacterSortInfo = "MyPageFavoriteCharacterSortInfo";

			public const string NewApplyPlayerIdList = "NewApplyPlayerIdList";

			public const string NewCharacterMissionShowId = "NewCharacterMissionShowId";

			public const string LastOpenedFriendCampaignMBId = "LastOpenedFriendCampaignMBId";

			public const string NoticeBadge = "NoticeBadge";

			public const string NotificationDeviceToken = "NotificationDeviceToken";

			public const string NotificationTokenLastGenerateTimeStamp = "NotificationTokenLastGenerateTimeStamp";

			public const string OpenLimitedLoginBonusDate = "OpenLimitedLoginBonusDate";

			public const string OpenLoginBonusDate = "OpenLoginBonusDate";

			public const string PartyUseViewSkipDate = "PartyUseViewSkipDate";

			public const string PendingProductData = "PendingProductData";

			public const string PlayerId = "PlayerId";

			public const string PlayerLoginKey = "ClientUuid";

			public const string PlayerName = "PlayeName";

			public const string RankUpCharacterSortInfo = "RankUpCharacterSortInfo";

			public const string ResolutionType = "ResolutionType";

			public const string SeMute = "SeMute";

			public const string SeVolume = "SeVolumeSetting";

			public const string ShardReversionButtonBadgeState = "ShardReversionButtonBadgeState";

			public const string SrCountFromGacha = "SrCountFromGacha";

			public const string StoryAuto = "StoryAuto";

			public const string TutorialBattleLoseData = "TutorialBattleLoseData";

			public const string UserId = "Userid";

			public const string UserLoginKey = "ClientKey";

			public const string VoiceLanguageType = "VoiceLanguageType";

			public const string VoiceMute = "VoiceMute";

			public const string VoiceVolume = "VoiceVolumeSetting";

			public const string LockEquipmentConfirmationInvalidDate = "LockEquipmentConfirmationInvalidDate{0}";

			public const string LockEquipmentCharacterSortInfo = "LockEquipmentCharacterSortInfo";
		}

		public static class Sns
		{
			// Note: this type is marked as 'beforefieldinit'.
			static Sns()
			{
				/*
An exception occurred when decompiling this method

ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void Ortega.Common.ClientConst/Sns::.cctor()

 ---> System.Exception: Basic block has to end with unconditional control flow. 
{
	Block_0:
	stsfld:string(Sns::ShareScreenShotFilePath, call:string(string::Concat, callgetter:string(Application::get_persistentDataPath), ldstr:string("/image.png")))
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

			public const string GooglePlayScopeEmail = "https://www.googleapis.com/auth/userinfo.email";

			public const string GooglePlayScopeOpenId = "openid";

			public const int LinkPasswordMaxLength = 16;

			public const int LinkPasswordMinLength = 8;

			public const int UserIdLength = 12;

			public static readonly string ShareScreenShotFilePath;
		}

		public static class CharacterBox
		{
			public const long SliderMaxValue = 100L;

			public const long SliderMultipleValue = 20L;

			public const long SliderUnitValue = 5L;
		}

		public static class Shop
		{
			// Note: this type is marked as 'beforefieldinit'.
			static Shop()
			{
				
			}

			public const int BuyMissionPointUnitValue = 10;

			public const int InputAgeLength = 6;

			public const int MaxReceiveDayFirstBonus = 3;

			public const int NumberOfMonthlyBoostDays = 30;

			public static readonly Dictionary<PurchaseAgeGroupType, int> PurchaseLimitDictionary;
		}

		public static class ShopTabId
		{
			public const long Currency = 1L;

			public const long GachaTicket = 5L;

			public const long Monthly = 4L;
		}

		public static class Bgm
		{
			public const float DefaultVolumeRatio = 1f;

			public const long PlayCharacterSongQuestId = 8L;
		}

		public static class Ranking
		{
			public const long MaxGuildRanking = 50L;

			public const long MaxPlayerRanking = 500L;
		}

		public static class Help
		{
			public const long Arcana = 1900L;

			public const long AutoBattle = 3200L;

			public const long BountyQuest = 4100L;

			public const long BulkCasting = 3000L;

			public const long CharacterDetail = 2810L;

			public const long CharacterListView = 1800L;

			public const long CharacterRelease = 2100L;

			public const long CharacterReset = 2800L;

			public const long CharacterStatusDetail = 2700L;

			public const long CharacterTraining = 2200L;

			public const long Chat = 5300L;

			public const long Competition = 3600L;

			public const long DungeonBattle = 3900L;

			public const long DungeonBattleEvent = 3910L;

			public const long DungeonBattleHardMode = 4000L;

			public const long DungeonBattleHardModeEvent = 4010L;

			public const long DungeonBattleCompensation = 4020L;

			public const long EquipmentAscend = 2330L;

			public const long EquipmentChange = 2410L;

			public const long EquipmentDetail = 2300L;

			public const long EquipmentEvolution = 2340L;

			public const long EquipmentInherit = 2400L;

			public const long EquipmentRefine = 2320L;

			public const long EquipmentSphere = 2350L;

			public const long EquipmentStrengthening = 2310L;

			public const long Exchange = 1500L;

			public const long FavoriteCharacter = 900L;

			public const long Friend = 1600L;

			public const long FriendCampaign = 5400L;

			public const long GachaElementSwitch = 4700L;

			public const long GachaSelectList = 4600L;

			public const long GlobalGvg = 5200L;

			public const long GlobalPvp = 3800L;

			public const long GuildGrandBattleRanking = 4902L;

			public const long GuildJoin = 4800L;

			public const long GuildRaid = 5000L;

			public const long GuildRaidEvent = 5010L;

			public const long GuildRanking = 500L;

			public const long GuildStock = 4901L;

			public const long GuildSubscriber = 4900L;

			public const long Gvg = 5100L;

			public const long LevelLink = 1820L;

			public const long LevelLinkPartyMode = 1830L;

			public const long LockEquipment = 1840L;

			public const long LocalPvp = 3700L;

			public const long LocalRaid = 4200L;

			public const long LocalRaidEvent = 4210L;

			public const long LoginBonus = 800L;

			public const long Mission = 700L;

			public const long MyPagePlayerInfo = 100L;

			public const long None = 0L;

			public const long PlayerRanking = 400L;

			public const long Present = 200L;

			public const long QuestInfo = 3300L;

			public const long QuickBattle = 3400L;

			public const long Ranking = 300L;

			public const long RankUpView = 1810L;

			public const long ShardReversion = 2000L;

			public const long ShopContractPrivilege = 1400L;

			public const long ShopContractPrivilegeDMM = 1410L;

			public const long ShopGrowthPack = 1300L;

			public const long ShopMonthlyBoost = 1100L;

			public const long ShopMonthlyBoostReward = 1200L;

			public const long ShopPlatinumBonus = 1000L;

			public const long SingleCasting = 2900L;

			public const long SphereBatchSynthesis = 3100L;

			public const long SphereEquipment = 2500L;

			public const long SphereSynthesis = 2600L;

			public const long Statistics = 3500L;

			public const long TowerBattleElementStageSelection = 4500L;

			public const long TowerBattleStageSelection = 4400L;

			public const long TowerBattleTowerSelection = 4300L;

			public const long VipPrivilege = 600L;

			public const long BattleReport = 5500L;

			public const long CharacterSkill = 2210L;
		}

		public class Sound
		{
			// Note: this type is marked as 'beforefieldinit'.
			static Sound()
			{
				
			}

			public const long FirstPlayCharacterSongQuestId = 12L;

			public const int InitVolume = 70;

			public static readonly Dictionary<VolumeType, string> MutePlayerPrefsKey;

			public static readonly Dictionary<VolumeType, string> VolumePlayerPrefsKey;
		}

		public static class Mission
		{
			// Note: this type is marked as 'beforefieldinit'.
			static Mission()
			{
				
			}

			public const int BeginnerMissionMaxDay = 7;

			public static readonly UserItem BeginnerMedalItem = new UserItem
			{
				ItemType = (ItemType)((ulong)28L),
				ItemId = (long)((ulong)3L),
				ItemCount = (long)((ulong)1L)
			};

			public static readonly Dictionary<int, MissionType[]> BeginnerMissionTypes;

			public static readonly UserItem LimitedMedalItem;
		}

		public static class LimitedMission
		{
			// Note: this type is marked as 'beforefieldinit'.
			static LimitedMission()
			{
				Dictionary<int, MissionType[]> dictionary = new();
				MissionType[] array = new MissionType[]
				{
					MissionType.LimitedFirstDayTab1,
					MissionType.LimitedFirstDayTab2,
					MissionType.LimitedFirstDayTab3,
					MissionType.LimitedFirstDayTab4
				};
				dictionary.Add(1, array);
				MissionType[] array2 = new MissionType[]
				{
					MissionType.LimitedSecondDayTab1,
					MissionType.LimitedSecondDayTab2,
					MissionType.LimitedSecondDayTab3,
					MissionType.LimitedSecondDayTab4
				};
				dictionary.Add(2, array2);
				MissionType[] array3 = new MissionType[]
				{
					MissionType.LimitedThirdDayTab1,
					MissionType.LimitedThirdDayTab2,
					MissionType.LimitedThirdDayTab3,
					MissionType.LimitedThirdDayTab4
				};
				dictionary.Add(3, array3);
				MissionType[] array4 = new MissionType[]
				{
					MissionType.LimitedFourthDayTab1,
					MissionType.LimitedFourthDayTab2,
					MissionType.LimitedFourthDayTab3,
					MissionType.LimitedFourthDayTab4
				};
				dictionary.Add(4, array4);
				MissionType[] array5 = new MissionType[]
				{
					MissionType.LimitedFifthDayTab1,
					MissionType.LimitedFifthDayTab2,
					MissionType.LimitedFifthDayTab3,
					MissionType.LimitedFifthDayTab4
				};
				dictionary.Add(5, array5);
				MissionType[] array6 = new MissionType[]
				{
					MissionType.LimitedSixthDayTab1,
					MissionType.LimitedSixthDayTab2,
					MissionType.LimitedSixthDayTab3,
					MissionType.LimitedSixthDayTab4
				};
				dictionary.Add(6, array6);
				MissionType[] array7 = new MissionType[]
				{
					MissionType.LimitedSeventhDayTab1,
					MissionType.LimitedSeventhDayTab2,
					MissionType.LimitedSeventhDayTab3,
					MissionType.LimitedSeventhDayTab4
				};
				dictionary.Add(7, array7);
				ClientConst.LimitedMission.MissionTypes = dictionary;
				throw new NullReferenceException();
			}

			public const int MaxDay = 7;

			public static readonly Dictionary<int, MissionType[]> MissionTypes;
		}

		public static class Exchange
		{
			public const int CastingStoneMaxExchangeValue = 50;

			public const int ShopTabIdDefault = 1;

			public const int ShopTabIdDungeonBattle = 6;

			public const int ShopTabIdGacha = 11;

			public const int ShopTabIdGlobalPvp = 8;

			public const int ShopTabIdGuild = 4;

			public const int ShopTabIdItemBox = 2;

			public const int ShopTabIdLocalPvp = 7;

			public const int ShopTabIdSacredTreasureLegend = 3;
		}

		public static class PushNotification
		{
			// Note: this type is marked as 'beforefieldinit'.
			static PushNotification()
			{
			}

			public const string AndroidNotificationIdFormat = "AndroidNotificationId{0}{1}";

			public const string CommonChannelId = "CommonChannelId";

			public const string IosNotificationIdentifierFormat = "Notification_Identifier_{0}{1}";

			public const string NotificationCharacterIconPathFormat = "chr_{0:D6}";

			public static readonly TimeSpan RepeatDailyNotificationInterval;

			public static readonly TimeSpan RepeatDailyNotificationThreshold;
		}

		public static class TutorialId
		{
			public const long Arcana = 3100L;

			public const long Building = 11L;

			public const long DungeonBattleHard = 2900L;

			public const long EquipmentAscend = 3300L;

			public const long EquipmentCasting = 2000L;

			public const long EquipmentRefine = 3400L;

			public const long EquipmentStrengthening = 2100L;

			public const long EsperiaTower = 3000L;

			public const long FavoriteCharacter = 3200L;

			public const long GlobalGvg = 2700L;

			public const long GuildRaid = 2500L;

			public const long GvgTutorial1 = 1001L;

			public const long LevelLink = 2800L;

			public const long LocalGvG = 2600L;

			public const long Memory = 13L;

			public const long Mission = 17L;

			public const long RankUp = 2400L;

			public const long SphereSet = 2200L;

			public const long SphereSynthesis = 2300L;

			public const long LockEquipment = 3500L;
		}

		public static class Tutorial
		{
			// Note: this type is marked as 'beforefieldinit'.
			static Tutorial()
			{
				throw new NullReferenceException();
			}

			public const long GvgCastleId = 1L;

			public const long MemoryCharacterId = 2L;

			public const int RankUpCharacterCount = 3;

			public const long RankUpCharacterId = 2L;

			public const long ShowBossBattleArrowChapterId = 1L;

			// public static readonly IReadOnlyDictionary<long, long> BossBattleArrowQuestIds = new ();
		}

		public static class AutoBattle
		{
			// Note: this type is marked as 'beforefieldinit'.
			static AutoBattle()
			{
				throw new NullReferenceException();
			}

			public static readonly List<IUserItem> CharacterDeadRewards = new ()
			{
				new UserItem
				{
					ItemType = (ItemType)((ulong)22L),
					ItemId = (long)((ulong)1L)
				},
				new UserItem
				{
					ItemType = (ItemType)((ulong)11L),
					ItemId = (long)((ulong)1L)
				}
			};
		}

		public static class Gvg
		{
			public const long MaxRanking = 50L;

			public const int UnspecifiedUnitCount = 6;
		}

		public static class Item
		{
			// Note: this type is marked as 'beforefieldinit'.
			static Item()
			{
				
			}

			public static readonly List<long> PartsTreasureBoxIds;
		}

		public static class ReviewGuidance
		{
			public const int ElapsedDaysToShowReview = 30;

			public const int SrCountToShowReview = 3;
		}

		public static class Backtrace
		{
			public const string BacktraceConfigurationPath = "Assets/Plugins/Backtrace/Backtrace Configuration.asset";

			public const string SymbolUploadToken = "54d81c821a0aa199daeec5a292d330ca2398496b98090adcdec88de6d88b9553";
		}

		public static class Icon
		{
			// Note: this type is marked as 'beforefieldinit'.
			static Icon()
			{
				
			}

			public const EquipmentRarityFlags MaxEquipmentRarityUsingWatercolor = EquipmentRarityFlags.S;

			public const ItemRarityFlags MaxItemRarityUsingWatercolor = ItemRarityFlags.S;

			public const CharacterRarityFlags NeedFrameDecorationCharacterRarityFlags = CharacterRarityFlags.RPlus | CharacterRarityFlags.SRPlus | CharacterRarityFlags.SSRPlus | CharacterRarityFlags.URPlus;

			public static readonly Dictionary<CharacterRarityFlags, Color> CharacterFrameColorDictionary = new ()
            {
                [CharacterRarityFlags.None] = Color.FromArgb(0x7A858A),
                [CharacterRarityFlags.N] = Color.FromArgb(0x9E7A7F),
                [CharacterRarityFlags.R] = Color.FromArgb(0xCBD7FD),
                [CharacterRarityFlags.RPlus] = Color.FromArgb(0xCBD7FD),
                [CharacterRarityFlags.SR] = Color.FromArgb(0xFFD47A),
                [CharacterRarityFlags.SRPlus] = Color.FromArgb(0xFFD47A),
                [CharacterRarityFlags.SSR] = Color.FromArgb(0xAE68ED),
                [CharacterRarityFlags.SSRPlus] = Color.FromArgb(0xAE68ED),
                [CharacterRarityFlags.UR] = Color.FromArgb(0xE35566),
                [CharacterRarityFlags.URPlus] = Color.FromArgb(0xE35566),
                [CharacterRarityFlags.LR] = Color.FromArgb(0xFFFFFF),
                
            };

			public static readonly Dictionary<CharacterRarityFlags, Color> CharacterWideFrameColorDictionary = new()
            {
                [CharacterRarityFlags.None] = Color.FromArgb(0x7A858A),
                [CharacterRarityFlags.N] = Color.FromArgb(0x9E7A7F),
                [CharacterRarityFlags.R] = Color.FromArgb(0xD3DDFE),
                [CharacterRarityFlags.RPlus] = Color.FromArgb(0xD3DDFE),
                [CharacterRarityFlags.SR] = Color.FromArgb(0xFFD47A),
                [CharacterRarityFlags.SRPlus] = Color.FromArgb(0xFFD47A),
                [CharacterRarityFlags.SSR] = Color.FromArgb(0xAE68ED),
                [CharacterRarityFlags.SSRPlus] = Color.FromArgb(0xAE68ED),
                [CharacterRarityFlags.UR] = Color.FromArgb(0xE35566),
                [CharacterRarityFlags.URPlus] = Color.FromArgb(0xE35566),
                [CharacterRarityFlags.LR] = Color.FromArgb(0xFFFFFF),

            };

            public static readonly Dictionary<EquipmentRarityFlags, Color> EquipmentFrameColorDictionary = new()
            {
                [EquipmentRarityFlags.None] = Color.FromArgb(0x7A858A),
                [EquipmentRarityFlags.D] = Color.FromArgb(0xBE9279),
                [EquipmentRarityFlags.C] = Color.FromArgb(0xFFFFFF),
                [EquipmentRarityFlags.B] = Color.FromArgb(0xFDC057),
                [EquipmentRarityFlags.A] = Color.FromArgb(0xB667FF),
                [EquipmentRarityFlags.S] = Color.FromArgb(0xF55555),
                [EquipmentRarityFlags.R] = Color.FromArgb(0xCBD7FD),
                [EquipmentRarityFlags.SR] = Color.FromArgb(0xFFD47A),
                [EquipmentRarityFlags.SSR] = Color.FromArgb(0xAE68ED),
                [EquipmentRarityFlags.UR] = Color.FromArgb(0xE35566),
                [EquipmentRarityFlags.LR] = Color.FromArgb(0xFFFFFF),
            };

			public static readonly Dictionary<ItemRarityFlags, Color> ItemFrameColorDictionary = new()
			{
				[ItemRarityFlags.None] =Color.FromArgb(0x7A858A),
				[ItemRarityFlags.D] = Color.FromArgb(0xBE9279),
				[ItemRarityFlags.C] = Color.FromArgb(0xFFFFFF),
				[ItemRarityFlags.B] = Color.FromArgb(0xFDC057),
				[ItemRarityFlags.A] = Color.FromArgb(0xB667FF),
				[ItemRarityFlags.S] = Color.FromArgb(0xF55555),
				[ItemRarityFlags.R] = Color.FromArgb(0xCBD7FD),
				[ItemRarityFlags.SR] = Color.FromArgb(0xFFD47A),
				[ItemRarityFlags.SSR] = Color.FromArgb(0xAE68ED),
				[ItemRarityFlags.UR] = Color.FromArgb(0xE35566),
				[ItemRarityFlags.LR] = Color.FromArgb(0xFFFFFF),
			};

			public static readonly Dictionary<IconPlateType, Color> PlateColorDictionary = new()
            {
                [IconPlateType.Default] = Color.FromArgb(0xA3A3A3),
                [IconPlateType.Character] = Color.FromArgb(0xFFFFFF),
            };

			public static readonly Dictionary<LegendLeagueClassType, Color> PlayerFrameColorDictionary = new()
            {
                [LegendLeagueClassType.None] = Color.FromArgb(0x7A858A),
                [LegendLeagueClassType.Chevalier] = Color.FromArgb(0xD69E7C),
                [LegendLeagueClassType.Paladin] = Color.FromArgb(0xFFFFFF),
                [LegendLeagueClassType.Duke] = Color.FromArgb(0xFFFFFF),
                [LegendLeagueClassType.Royal] = Color.FromArgb(0xFFFFFF),
                [LegendLeagueClassType.Legend] = Color.FromArgb(0xFFFFFF),
                [LegendLeagueClassType.WorldRuler] = Color.FromArgb(0xFFFFFF),
            };

			public static readonly Dictionary<EquipmentRarityFlags, Color> SphereFrameColorDictionary = new()
            {
                [EquipmentRarityFlags.None] = Color.FromArgb(0x7A858A),
                [EquipmentRarityFlags.D] = Color.FromArgb(0xC09363),
                [EquipmentRarityFlags.C] = Color.FromArgb(0xA7AEB0),
                [EquipmentRarityFlags.B] = Color.FromArgb(0xF5CC6B),
                [EquipmentRarityFlags.A] = Color.FromArgb(0x936ADF),
                [EquipmentRarityFlags.S] = Color.FromArgb(0xDD6161),
                [EquipmentRarityFlags.R] = Color.FromArgb(0x979797),
                [EquipmentRarityFlags.SR] = Color.FromArgb(0xFFDF9A),
                [EquipmentRarityFlags.SSR] = Color.FromArgb(0xC989FF),
                [EquipmentRarityFlags.UR] = Color.FromArgb(0xFF6B6A),
                [EquipmentRarityFlags.LR] = Color.FromArgb(0x696969),
            };
		}
	}
}
