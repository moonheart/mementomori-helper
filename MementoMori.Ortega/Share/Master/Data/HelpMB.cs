using System.ComponentModel;
using MementoMori.Ortega.Share.Help;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[Description("ヘルプ")]
	[MessagePackObject(true)]
	public class HelpMB : MasterBookBase
	{
		[Description("ヘルプパートリスト")]
		[PropertyOrder(2)]
		[Nest(false, 0)]
		public IReadOnlyList<HelpPartInfo> HelpPartInfoList
		{
			get;
		}

		[Description("ヘルプタイトル")]
		[PropertyOrder(1)]
		public string HelpTitle
		{
			get;
		}

		[PropertyOrder(4)]
		[Description("表示フラグ")]
		public bool IsDisplayed
		{
			get;
		}

		[Description("チュートリアル説明ID")]
		[PropertyOrder(3)]
		public long TutorialDescriptionId
		{
			get;
		}

		[SerializationConstructor]
		public HelpMB(long id, bool? isIgnore, string memo, string helpTitle, IReadOnlyList<HelpPartInfo> helpPartInfoList, long tutorialDescriptionId, bool isDisplayed)
			:base(id, isIgnore, memo)
		{
			HelpTitle = helpTitle;
			HelpPartInfoList = helpPartInfoList;
			TutorialDescriptionId = tutorialDescriptionId;
			IsDisplayed = isDisplayed;
		}

		public HelpMB() : base(0, false, "")
		{
		}
	}
}
