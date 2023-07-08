using System.ComponentModel;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[Description("テキスト言語")]
	[MessagePackObject(true)]
	public class TextLanguageMB : MasterBookBase
	{
		[PropertyOrder(1)]
		[Description("言語")]
		public LanguageType LanguageType { get; }

		[PropertyOrder(2)]
		[Description("行頭禁則文字")]
		public string HyphenationFront { get; }

		[PropertyOrder(3)]
		[Description("行末禁則文字")]
		public string HyphenationBack { get; }

		[PropertyOrder(4)]
		[Description("分離禁止文字")]
		public IReadOnlyList<string> HyphenationSeparable { get; }

		[SerializationConstructor]
		public TextLanguageMB(long id, bool? isIgnore, string memo, LanguageType languageType, string hyphenationFront, string hyphenationBack, IReadOnlyList<string> hyphenationSeparable)
			: base(id, isIgnore, memo)
		{
			this.LanguageType = languageType;
			this.HyphenationFront = hyphenationFront;
			this.HyphenationBack = hyphenationBack;
			this.HyphenationSeparable = hyphenationSeparable;
		}

		public TextLanguageMB() : base(0L, false, "")
		{
		}

		public bool CheckHyphenation(char front, char back)
		{
			// while (!this.<HyphenationFront>k__BackingField.Contains(front))
			// {
			// 	bool flag = this.<HyphenationBack>k__BackingField.Contains(front);
			// 	if (flag || this.<HyphenationSeparable>k__BackingField == (ulong)0L || flag <= false)
			// 	{
			// 		break;
			// 	}
			// 	IReadOnlyList<string> readOnlyList = this.<HyphenationSeparable>k__BackingField;
			// 	int num = 0;
			// 	if (flag)
			// 	{
			// 		uint num2;
			// 		if (num < (int)num2)
			// 		{
			// 			num += num;
			// 			if (num == (int)num2)
			// 			{
			// 				goto IL_59;
			// 			}
			// 			num++;
			// 		}
			// 		bool flag2;
			// 		while (!flag2)
			// 		{
			// 		}
			// 		bool flag3;
			// 		while (!flag3)
			// 		{
			// 		}
			// 		int num3 = 0;
			// 		IL_59:
			// 		num3 += num3;
			// 	}
			// 	if ("{il2cpp array field local14->}" != (ulong)0L)
			// 	{
			// 	}
			// 	if (num == 0)
			// 	{
			// 		break;
			// 	}
			// }
			// throw new NullReferenceException();
			throw new NotImplementedException();
		}
	}
}
