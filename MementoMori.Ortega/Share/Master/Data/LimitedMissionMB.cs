using System.ComponentModel;
using MementoMori.Ortega.Share.Master.Attributes;
using MementoMori.Ortega.Share.Utils;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[Description("期間限定ミッション")]
	[MessagePackObject(true)]
	public class LimitedMissionMB : MasterBookBase, IHasStartEndTime
	{
		[PropertyOrder(1)]
		[Description("開始日時")]
		public string StartTime
		{
			get;
		}

		[Description("終了日時")]
		[PropertyOrder(2)]
		public string EndTime
		{
			get;
		}

		[PropertyOrder(3)]
		[Description("キャラ画像Id")]
		public int CharacterImageId
		{
			get;
		}

		[PropertyOrder(4)]
		[Description("キャラ画像座標X")]
		public float CharacterImageX
		{
			get;
		}

		[PropertyOrder(5)]
		[Description("キャラ画像座標Y")]
		public float CharacterImageY
		{
			get;
		}

		[Description("キャラ画像サイズ")]
		[PropertyOrder(6)]
		public float CharacterImageSize
		{
			get;
		}

		[Description("タイトル")]
		[PropertyOrder(7)]
		public string TitleTextKey
		{
			get;
		}

		[Description("訴求文言")]
		[PropertyOrder(8)]
		public string AppealTextKey
		{
			get;
		}

		[Description("対象ミッションID")]
		[PropertyOrder(9)]
		public IReadOnlyList<long> TargetMissionIdList
		{
			get;
		}

		[SerializationConstructor]
		public LimitedMissionMB(long id, bool? isIgnore, string memo, string startTime, string endTime, int characterImageId, float characterImageX, float characterImageY, float characterImageSize, string titleTextKey, string appealTextKey, IReadOnlyList<long> targetMissionIdList)
			:base(id, isIgnore, memo)
		{
			StartTime = startTime;
			EndTime = endTime;
			CharacterImageId = characterImageId;
			CharacterImageX = characterImageX;
			CharacterImageY = characterImageY;
			CharacterImageSize = characterImageSize;
			TitleTextKey = titleTextKey;
			AppealTextKey = appealTextKey;
			TargetMissionIdList = targetMissionIdList;
		}

		public LimitedMissionMB() :base(0L, false, ""){}
	}
}
