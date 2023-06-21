using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Help
{
	[Description("ヘルプパート")]
	[MessagePackObject(true)]
	public class HelpPartInfo
	{
		[Description("見出し")]
		[PropertyOrder(1)]
		public string HeadLine
		{
			get;
			set;
		}

		[Description("本文")]
		[PropertyOrder(2)]
		public string MainText
		{
			get;
			set;
		}

		[PropertyOrder(3)]
		[Description("付加情報")]
		public HelpParameterType HelpParameterType
		{
			get;
			set;
		}

		public HelpPartInfo()
		{
		}
	}
}
