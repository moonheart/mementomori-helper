using System.Text;

namespace MementoMori.Ortega.Share.Utils
{
	public static class StringExtension
	{
		private static Encoding ByteConvertEncoding
		{
			get;
		}

		public static byte[] ToBytes(this string target)
		{
			// if (target._stringLength != 0)
			// {
			// 	Encoding encoding = StringExtension.<ByteConvertEncoding>k__BackingField;
			// }
			// throw new NullReferenceException();
			throw new NotImplementedException();
		}

		public static byte[] ToHexBytes(this string target)
		{
			// bool flag;
			// if (flag)
			// {
			// }
			// if (target != 0)
			// {
			// }
			// InvalidCastException ex = new InvalidCastException("invalid hex string: " + target);
			// throw new NullReferenceException();
			throw new NotImplementedException();
		}

		public static string ToHexString(this byte[] target)
		{
			// Func<byte, string> <>9__5_ = StringExtension.<>c.<>9__5_0;
			// if (<>9__5_ == 0)
			// {
			// 	Func<byte, string> func;
			// 	StringExtension.<>c.<>9__5_0 = func;
			// }
			// return string.Concat(Enumerable.Select<byte, string>(target, <>9__5_));
			throw new NotImplementedException();
		}

		public static string ToLowerFirst(this string target)
		{
			// if (!string.IsNullOrEmpty(target))
			// {
			// 	int num = 0;
			// 	char c = target[num];
			// 	if (char.IsUpper(c))
			// 	{
			// 		if (target._stringLength == 1)
			// 		{
			// 			return target.ToLower();
			// 		}
			// 		int num2 = 0;
			// 		c = target[num2];
			// 		c = char.ToLower(c);
			// 		string text = target.Substring(1);
			// 		return string.Format("{0}{1}", c, text);
			// 	}
			// }
			// return target;
			throw new NotImplementedException();
		}

		public static string ToUpperFirst(this string target)
		{
			// if (!string.IsNullOrEmpty(target))
			// {
			// 	int num = 0;
			// 	char c = target[num];
			// 	if (char.IsLower(c))
			// 	{
			// 		if (target._stringLength != 1)
			// 		{
			// 			int num2 = 0;
			// 			c = target[num2];
			// 			c = char.ToUpper(c);
			// 			string text = target.Substring(1);
			// 			return string.Format("{0}{1}", c, text);
			// 		}
			// 		return target.ToUpper();
			// 	}
			// }
			// return target;
			throw new NotImplementedException();
		}

		public static string ToUTF8String(this byte[] target)
		{
			// if (target.Length != 0)
			// {
			// 	Encoding encoding = StringExtension.<ByteConvertEncoding>k__BackingField;
			// }
			// return string.Empty;
			throw new NotImplementedException();
		}

		public static int ToInt(this string target)
		{
			// int num = 0;
			// bool flag = int.TryParse(target, num);
			// return num;
			throw new NotImplementedException();
		}

		public static long ToLong(this string target)
		{
			/*
An exception occurred when decompiling this method (060031F1)

ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Int64 Ortega.Share.Utils.StringExtension::ToLong(System.String)

 ---> System.Exception: Basic block has to end with unconditional control flow. 
{
	Block_0:
	stloc:int32(var_0_01, ldc.i4:int32(0))
	stloc:bool(var_1_09, call:bool(int64::TryParse, ldloc:string(target), ldloc:int32[exp:int64](var_0_01)))
}

   at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.FlattenBasicBlocks(ILNode node) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 1813
   at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.Optimize(DecompilerContext context, ILBlock method, AutoPropertyProvider autoPropertyProvider, StateMachineKind& stateMachineKind, MethodDef& inlinedMethod, AsyncMethodDebugInfo& asyncInfo, ILAstOptimizationStep abortBeforeStep) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 347
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 123
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 88
   --- End of inner exception stack trace ---
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 92
   at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1627
*/;
			throw new NotImplementedException();
		}

		public static float ToFloat(this string target)
		{
			// ulong num;
			// while (float.TryParse(target, (float)num))
			// {
			// }
			throw new NotImplementedException();
		}

		public static DateTime ToDateTime(this string str)
		{
			return DateTime.Parse(str);
		}

		public static TimeSpan ToTimeSpan(this string str)
		{
			return TimeSpan.Parse(str);
		}

		// Note: this type is marked as 'beforefieldinit'.
		static StringExtension()
		{
			ByteConvertEncoding = Encoding.UTF8;
		}
	}
}
