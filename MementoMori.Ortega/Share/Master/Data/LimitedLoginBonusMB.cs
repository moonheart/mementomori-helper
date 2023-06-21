using System.ComponentModel;
using MementoMori.Ortega.Share.Master.Attributes;
using MementoMori.Ortega.Share.Utils;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("期間限定ログインボーナス")]
	public class LimitedLoginBonusMB : MasterBookBase, IHasStartEndTime
	{
		[Description("訴求文言")]
		[PropertyOrder(8)]
		public string AppealTextKey
		{
			get;
		}

		[Description("キャラ画像Id")]
		[PropertyOrder(4)]
		public int CharacterImageId
		{
			get;
		}

		[Description("報酬背景画像ID")]
		[PropertyOrder(5)]
		public int RewardBackgroundImageId
		{
			get;
		}

		[PropertyOrder(3)]
		[Description("報酬リストID")]
		public long RewardListId
		{
			get;
		}

		[Description("特別報酬訴求文言")]
		[PropertyOrder(9)]
		public string SpecialRewardAppealTextKey
		{
			get;
		}

		[Description("特別報酬背景画像ID")]
		[PropertyOrder(6)]
		public int SpecialRewardBackgroundImageId
		{
			get;
		}

		[Description("特別報酬カウントテキスト色")]
		[PropertyOrder(12)]
		public string SpecialRewardCountTextColor
		{
			get;
		}

		[Description("特別報酬ラベル色")]
		[PropertyOrder(10)]
		public string SpecialRewardLabelTextColor
		{
			get;
		}

		[Description("特別報酬ラベルアウトライン色")]
		[PropertyOrder(11)]
		public string SpecialRewardLabelTextOutlineColor
		{
			get;
		}

		[Description("タイトル")]
		[PropertyOrder(7)]
		public string TitleTextKey
		{
			get;
		}

		[SerializationConstructor]
		public LimitedLoginBonusMB(long id, bool? isIgnore, string memo, string startTime, string endTime, long rewardListId, int characterImageId, int rewardBackgroundImageId, int specialRewardBackgroundImageId, string titleTextKey, string appealTextKey, string specialRewardAppealTextKey, string specialRewardLabelTextColor, string specialRewardLabelTextOutlineColor, string specialRewardCountTextColor)
			:base(id, isIgnore, memo)
		{
			StartTime = startTime;
			EndTime = endTime;
			RewardListId = rewardListId;
			CharacterImageId = characterImageId;
			RewardBackgroundImageId = rewardBackgroundImageId;
			SpecialRewardBackgroundImageId = specialRewardBackgroundImageId;
			TitleTextKey = titleTextKey;
			AppealTextKey = appealTextKey;
			SpecialRewardAppealTextKey = specialRewardAppealTextKey;
			SpecialRewardLabelTextColor = specialRewardLabelTextColor;
			SpecialRewardLabelTextOutlineColor = specialRewardLabelTextOutlineColor;
			SpecialRewardCountTextColor = specialRewardCountTextColor;
		}

		public LimitedLoginBonusMB() :base(0L, false, ""){}

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
	}
}
