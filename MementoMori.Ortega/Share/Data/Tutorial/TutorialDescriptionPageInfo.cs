using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Tutorial
{
	[Description("ページ情報")]
	[MessagePackObject(true)]
	public class TutorialDescriptionPageInfo
	{
		[Description("画像ID")]
		[PropertyOrder(2)]
		public int ImageId
		{
			get;
			set;
		}

		[PropertyOrder(1)]
		[Description("本文")]
		public string MainTextKey
		{
			get;
			set;
		}

		public TutorialDescriptionPageInfo()
		{
		}
	}
}
