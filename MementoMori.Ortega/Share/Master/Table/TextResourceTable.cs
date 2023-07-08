using System.Runtime.InteropServices;
using MementoMori.Ortega.Common.Enums;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Data;
using MementoMori.Ortega.Share.Master.Interfaces;

namespace MementoMori.Ortega.Share.Master.Table
{
	public class TextResourceTable : TableBase<TextResourceMB>
	{
		public bool Load(LanguageType languageType)
		{
			base.Load();
			foreach (var textResourceMb in _datas)
			{
				switch (languageType)
				{
					case LanguageType.None:
						break;
					case LanguageType.jaJP:
						_cached[textResourceMb.StringKey] = textResourceMb.jaJP;
						break;
					case LanguageType.enUS:
						_cached[textResourceMb.StringKey] = textResourceMb.enUS;
						break;
					case LanguageType.koKR:
						_cached[textResourceMb.StringKey] = textResourceMb.koKR;
						break;
					case LanguageType.zhTW:
						_cached[textResourceMb.StringKey] = textResourceMb.zhTW;
						break;
					default:
						throw new ArgumentOutOfRangeException(nameof(languageType), languageType, null);
				}
			}

			return true;
			// int num = 0;
			// if (this._cached == num)
			// {
			// 	Dictionary<string, string> dictionary = new Dictionary();
			// 	this._cached = dictionary;
			// }
			// this._cached.Clear();
			// TextResourceUtil.TextResourceData[] textResourceDatas = TextResourceUtil.TextResourceDatas;
			// int length = textResourceDatas.Length;
			// if (num < length)
			// {
			// 	TextResourceUtil.TextResourceData textResourceData = textResourceDatas[num];
			// 	if (textResourceData.<LanguageType>k__BackingField != languageType)
			// 	{
			// 		num++;
			// 	}
			// 	string <Name>k__BackingField = textResourceData.<Name>k__BackingField;
			// 	string text = Application.persistentDataPath + "/Master/" + <Name>k__BackingField;
			// }
			// string empty = string.Empty;
			// int num2 = 0;
			// int num3 = 0;
			// string text2;
			// text2.FieldGetter(num3, num2, length);
			// string empty2 = string.Empty;
			// byte b;
			// int num8;
			// if (File.Exists(empty2))
			// {
			// 	FileStream fileStream;
			// 	byte[] array = new byte[fileStream.Length];
			// 	int length2 = array.Length;
			// 	int num4 = 0;
			// 	int num5 = fileStream.Read(array, num4, length2);
			// 	ITextResource[] array2;
			// 	if (num < array2.Length)
			// 	{
			// 		ITextResource textResource = array2[num];
			// 		uint num6;
			// 		if (num < (int)num6)
			// 		{
			// 			num += num;
			// 			num++;
			// 		}
			// 		bool flag;
			// 		if (!flag)
			// 		{
			// 			Dictionary<string, string> cached = this._cached;
			// 			bool flag2;
			// 			if (!flag2)
			// 			{
			// 				Dictionary<string, string> dictionary2 = this._cached;
			// 				num++;
			// 				dictionary2 += dictionary2;
			// 			}
			// 			num++;
			// 		}
			// 		string text4;
			// 		string text3 = text4 + "が設定されていません。";
			// 		num++;
			// 	}
			// 	string text5 = "cache loaded. Path:" + empty2;
			// 	if ("{il2cpp array field local23->}" != (ulong)0L)
			// 	{
			// 	}
			// 	if (num != 0)
			// 	{
			// 		throw new NullReferenceException();
			// 	}
			// 	int num7 = 0;
			// 	string text6;
			// 	text6.FieldGetter(num7, " ", b);
			// 	num8 = 0;
			// }
			// int num9 = 0;
			// string text7 = "file not exists.Path:" + num8;
			// int num10 = 0;
			// text7.FieldGetter(num10, num9, b);
		}

		public string Get(string key)
		{
			return _cached.TryGetValue(key, out var value) ? value : "未知";
			// if (this._cached != (ulong)0L)
			// {
			// 	if (string.IsNullOrEmpty(key))
			// 	{
			// 		return string.Empty;
			// 	}
			// 	Dictionary<string, string> cached = this._cached;
			// 	bool flag;
			// 	bool flag2;
			// 	if (flag && !flag2)
			// 	{
			// 		string newLine = Environment.NewLine;
			// 		string text;
			// 		return text;
			// 	}
			// }
			// return key;
			throw new NotImplementedException();
		}

		public string Get<T>(T enumValue) where T : Enum
		{
			// string name = typeof(Type).Name;
			// string text2;
			// string text = "[" + name + text2 + "]";
			// return this.Get(text);
			throw new NotImplementedException();
		}

		public string Get<T>(T enumValue, params object[] param) where T : Enum
		{
			// Type type;
			// string name = type.Name;
			// string text2;
			// string text = "[" + name + text2 + "]";
			// return this.Get(text, param);
			throw new NotImplementedException();
		}

		public string Get(string key, params object[] param)
		{
			// string text2;
			// string[] array;
			// for (;;)
			// {
			// 	string text = string.Format(this.Get(key), param);
			// 	string newLine = Environment.NewLine;
			// 	text2 = text.Replace("<br>", newLine);
			// 	array = new string[5];
			// 	if ("invalid Parameters (" == 0 || "invalid Parameters (" != 0)
			// 	{
			// 		array[0] = "invalid Parameters (";
			// 		if ("invalid Parameters (" != 0 && (")(" == 0 || ")(" != 0))
			// 		{
			// 			array[2] = ")(";
			// 			if (")(" == 0 || ")(" != 0)
			// 			{
			// 				array[3] = ")(";
			// 				if (")" == 0 || ")" != 0)
			// 				{
			// 					break;
			// 				}
			// 			}
			// 		}
			// 	}
			// }
			// array[4] = ")";
			// string text3 = string.Concat(array);
			// return text2;
			throw new NotImplementedException();
		}

		public string GetItemDescription<T>(T enumValue) where T : Enum
		{
			// Type type;
			// string name = type.Name;
			// string text2;
			// string text = "[" + name + text2 + "Description]";
			// return this.Get(text);
			throw new NotImplementedException();
		}

		public string GetItemDescription<T>(T enumValue, params object[] param) where T : Enum
		{
			// string name = typeof(Type).Name;
			// string text2;
			// string text = "[" + name + text2 + "Description]";
			// return this.Get(text, param);
			throw new NotImplementedException();
		}

		public string GetErrorCodeMessage(ErrorCode errorCode)
		{
			string text = string.Format("[ErrorMessage{0}]", "[ErrorMessage{0}]");
			return this.Get(text);
		}

		public string GetErrorCodeMessage(ErrorCode errorCode, params object[] param)
		{
			string text = string.Format("[ErrorMessage{0}]", "[ErrorMessage{0}]");
			return this.Get(text, param);
		}

		public string GetErrorCodeMessage(long errorCode)
		{
			string text = string.Format("[ErrorMessage{0}]", "[ErrorMessage{0}]");
			return this.Get(text);
		}

		public string GetErrorCodeMessage(long errorCode, params object[] param)
		{
			string text = string.Format("[ErrorMessage{0}]", "[ErrorMessage{0}]");
			return this.Get(text, param);
		}

		public string GetClientErrorCodeMessage(long errorCode)
		{
			string text = string.Format("[ClientErrorMessage{0}]", "[ClientErrorMessage{0}]");
			return this.Get(text);
		}

		public string GetClientErrorCodeMessage(long errorCode, params object[] param)
		{
			string text = string.Format("[ClientErrorMessage{0}]", "[ClientErrorMessage{0}]");
			return this.Get(text, param);
		}

		public string GetErrorCodeTitle(ErrorCode errorCode)
		{
			string text = string.Format("[ErrorTitle{0}]", "[ErrorTitle{0}]");
			string text2 = this.Get(text);
			if (!text.Equals(text2))
			{
				return text2;
			}
			return string.Empty;
		}

		public bool IsNullOrEmpty(string key)
		{
			// if (this._cached != (ulong)0L && !string.IsNullOrEmpty(key))
			// {
			// 	Dictionary<string, string> cached = this._cached;
			// 	bool flag;
			// 	if (flag)
			// 	{
			// 		bool flag2;
			// 		return flag2;
			// 	}
			// }
			// return true;
			throw new NotImplementedException();
		}

		public string GetErrorCodeMessage(ClientErrorCode errorCode)
		{
			string text = string.Format("[ClientErrorMessage{0}]", "[ClientErrorMessage{0}]");
			return this.Get(text);
		}

		public string GetErrorCodeMessage(ClientErrorCode errorCode, params object[] param)
		{
			string text = string.Format("[ClientErrorMessage{0}]", "[ClientErrorMessage{0}]");
			return this.Get(text, param);
		}

		public string GetClientErrorCodeTitle(ClientErrorCode errorCode)
		{
			string text = string.Format("[ClientErrorTitle{0}]", "[ClientErrorTitle{0}]");
			string text2 = this.Get(text);
			if (!text.Equals(text2))
			{
				return text2;
			}
			return string.Empty;
		}

		public TextResourceTable()
		{
		}
		private static string GetMasterDataPath(LanguageType languageType)
		{
			// int num = 0;
			// TextResourceUtil.TextResourceData[] textResourceDatas = TextResourceUtil.TextResourceDatas;
			// int length = textResourceDatas.Length;
			// if (num < length)
			// {
			// 	TextResourceUtil.TextResourceData textResourceData = textResourceDatas[num];
			// 	if (textResourceData.<LanguageType>k__BackingField != languageType)
			// 	{
			// 		num++;
			// 	}
			// 	string <Name>k__BackingField = textResourceData.<Name>k__BackingField;
			// 	return Application.persistentDataPath + "/Master/" + <Name>k__BackingField;
			// }
			// string empty = string.Empty;
			// return string.Empty;
			throw new NotImplementedException();
		}

		private ITextResource[] Deserialize(LanguageType languageType, byte[] data)
		{
			throw new NotImplementedException();
			/*
An exception occurred when decompiling this method (060034B0)

ICSharpCode.Decompiler.DecompilerException: Error decompiling Ortega.Share.Master.Interfaces.ITextResource[] Ortega.Share.Master.Table.TextResourceTable::Deserialize(Ortega.Share.Enums.LanguageType,System.Byte[])

 ---> System.Exception: Basic block has to end with unconditional control flow. 
{
	IL_09:
	stloc:string(var_14_14, call:string(string::Format, ldstr:string("Error Deserialize LanguageType:{0}"), ldloc:int32[exp:object](var_0_03)))
	stloc:int64(var_17, add:RuntimeTypeHandle[exp:int64](ldloc:int64[exp:RuntimeTypeHandle](var_17), ldtoken:RuntimeTypeHandle([MessagePack]MessagePack.MessagePackSerializer)))
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

		// [FieldOffset(Offset = "0x18")]
		private Dictionary<string, string> _cached = new Dictionary<string, string>();
	}
}
