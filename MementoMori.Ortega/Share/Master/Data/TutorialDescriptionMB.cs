using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Data.Tutorial;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("チュートリアル詳細")]
	public class TutorialDescriptionMB : MasterBookBase
	{
		[PropertyOrder(2)]
		[Description("HelpMB ID(チュートリアル用)")]
		public long HelpId
		{
			get;
		}

		[Description("可変画像アイテム1")]
		[PropertyOrder(4)]
		[Nest(false, 0)]
		public UserItem ImageItem1
		{
			get;
		}

		[Description("可変画像アイテム2")]
		[PropertyOrder(5)]
		[Nest(false, 0)]
		public UserItem ImageItem2
		{
			get;
		}

		[Description("画像位置フォーマットタイプ")]
		[PropertyOrder(3)]
		public ImagePositionFormatType ImagePositionFormatType
		{
			get;
		}

		[Nest(false, 0)]
		[PropertyOrder(1)]
		[Description("ページ情報")]
		public IReadOnlyList<TutorialDescriptionPageInfo> PageInfo
		{
			get;
		}

		[PropertyOrder(6)]
		[Description("可変画像キャラID")]
		public long ImageCharacterId
		{
			get;
		}

		[SerializationConstructor]
		public TutorialDescriptionMB(long id, bool? isIgnore, string memo, IReadOnlyList<TutorialDescriptionPageInfo> pageInfo, long helpId, ImagePositionFormatType imagePositionFormatType, UserItem imageItem1, UserItem imageItem2, long imageCharacterId)
			:base(id, isIgnore, memo)
		{
			PageInfo = pageInfo;
			HelpId = helpId;
			ImagePositionFormatType = imagePositionFormatType;
			ImageItem1 = imageItem1;
			ImageItem2 = imageItem2;
			ImageCharacterId = imageCharacterId;
		}

		public TutorialDescriptionMB() :base(0L, false, ""){}
	}
}
