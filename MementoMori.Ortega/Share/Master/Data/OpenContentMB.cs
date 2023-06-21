using System.ComponentModel;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[Description("コンテンツ開放")]
	[MessagePackObject(true)]
	public class OpenContentMB : MasterBookBase
	{
		[PropertyOrder(4)]
		[Description("演出パス")]
		public string AssetPath
		{
			get;
		}

		[PropertyOrder(3)]
		[Description("開放内容キー")]
		public string DescriptionKey
		{
			get;
		}

		[PropertyOrder(8)]
		[Description("ガイド説明文")]
		public string GuideDescriptionKey
		{
			get;
		}

		[Description("ガイド表示優先順")]
		[PropertyOrder(7)]
		public int GuideOrderNumber
		{
			get;
		}

		[Description("ガイド表示タイプ")]
		[PropertyOrder(6)]
		public bool IsActiveGuide
		{
			get;
		}

		[Description("解放されるコマンドの種類")]
		[PropertyOrder(5)]
		public OpenCommandType OpenCommandType
		{
			get;
		}

		[Description("コンテンツ開放タイプ")]
		[PropertyOrder(1)]
		public OpenContentType OpenContentType
		{
			get;
		}

		[Description("コンテンツ開放値")]
		[PropertyOrder(2)]
		public long OpenContentValue
		{
			get;
		}

		[Description("トースト")]
		[PropertyOrder(9)]
		public string ToastKey
		{
			get;
		}

		[Description("トチュートリアルID")]
		[PropertyOrder(10)]
		public long TutorialId
		{
			get;
		}

		[SerializationConstructor]
		public OpenContentMB(long id, bool? isIgnore, string memo, string assetPath, string descriptionKey, OpenContentType openContentType, long openContentValue, OpenCommandType openCommandType, bool isActiveGuide, int guideOrderNumber, string guideDescriptionKey, string toastKey, long tutorialId)
			:base(id, isIgnore, memo)
		{
			AssetPath = assetPath;
			DescriptionKey = descriptionKey;
			OpenContentType = openContentType;
			OpenContentValue = openContentValue;
			OpenCommandType = openCommandType;
			IsActiveGuide = isActiveGuide;
			GuideOrderNumber = guideOrderNumber;
			GuideDescriptionKey = guideDescriptionKey;
			ToastKey = toastKey;
			TutorialId = tutorialId;
		}

		public OpenContentMB() :base(0L, false, ""){}
	}
}
