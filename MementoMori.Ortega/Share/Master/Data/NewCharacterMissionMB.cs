using System.ComponentModel;
using MementoMori.Ortega.Share.Master.Attributes;
using MementoMori.Ortega.Share.Utils;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("新キャラミッション")]
	public class NewCharacterMissionMB : MasterBookBase, IHasJstStartEndTime
	{
		[Description("開始日時")]
		[PropertyOrder(1)]
		public string StartTimeFixJST
		{
			get;
		}

		[Description("終了日時")]
		[PropertyOrder(2)]
		public string EndTimeFixJST
		{
			get;
		}

		[Description("強制開始時間")]
		[PropertyOrder(3)]
		public string ForceStartTime
		{
			get;
		}

		[PropertyOrder(4)]
		[Description("キャラ画像Id")]
		public int CharacterImageId
		{
			get;
		}

		[Description("キャラ画像座標X")]
		[PropertyOrder(5)]
		public float CharacterImageX
		{
			get;
		}

		[Description("キャラ画像座標Y")]
		[PropertyOrder(6)]
		public float CharacterImageY
		{
			get;
		}

		[PropertyOrder(7)]
		[Description("キャラ画像サイズ")]
		public float CharacterImageSize
		{
			get;
		}

		[PropertyOrder(8)]
		[Description("タイトル")]
		public string TitleTextKey
		{
			get;
		}

		[PropertyOrder(9)]
		[Description("対象ミッションID")]
		public IReadOnlyList<long> TargetMissionIdList
		{
			get;
		}

		[PropertyOrder(10)]
		[Description("YouTube")]
		public string YouTubeUrl
		{
			get;
		}

		[PropertyOrder(11)]
		[Description("Twitter")]
		public string TwitterUrl
		{
			get;
		}

		[SerializationConstructor]
		public NewCharacterMissionMB(long id, bool? isIgnore, string memo, string startTimeFixJST, string endTimeFixJST, string forceStartTime, int characterImageId, float characterImageX, float characterImageY, float characterImageSize, string titleTextKey, IReadOnlyList<long> targetMissionIdList, string youTubeUrl, string twitterUrl)
			:base(id, isIgnore, memo)
		{
			StartTimeFixJST = startTimeFixJST;
			EndTimeFixJST = endTimeFixJST;
			ForceStartTime = forceStartTime;
			CharacterImageId = characterImageId;
			CharacterImageX = characterImageX;
			CharacterImageY = characterImageY;
			CharacterImageSize = characterImageSize;
			TitleTextKey = titleTextKey;
			TargetMissionIdList = targetMissionIdList;
			YouTubeUrl = youTubeUrl;
			TwitterUrl = twitterUrl;
		}

		public NewCharacterMissionMB() :base(0L, false, ""){}
	}
}
