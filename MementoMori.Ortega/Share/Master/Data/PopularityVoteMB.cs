using System.ComponentModel;
using MementoMori.Ortega.Share.Data.PopularityVote;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MementoMori.Ortega.Share.Utils;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("人気投票")]
	public class PopularityVoteMB : MasterBookBase, IHasJstStartEndTime
	{
		[PropertyOrder(1)]
		[Description("投票形式")]
		public PopularityVoteVotingType VotingType { get; }

		[PropertyOrder(2)]
		[Description("キャラソート形式")]
		public PopularityVoteSortType SortType { get; }

		[PropertyOrder(3)]
		[Description("開始日時")]
		public string StartTimeFixJST { get; }

		[PropertyOrder(4)]
		[Description("終了日時")]
		public string EndTimeFixJST { get; }

		[DateTimeString]
		[PropertyOrder(5)]
		[Description("予選開始日時")]
		public string PreliminaryStartTimeFixJst { get; }

		[DateTimeString]
		[PropertyOrder(6)]
		[Description("予選終了日時")]
		public string PreliminaryEndTimeFixJst { get; }

		[DateTimeString]
		[PropertyOrder(7)]
		[Description("本選開始日時")]
		public string FinalStartTimeFixJst { get; }

		[DateTimeString]
		[PropertyOrder(8)]
		[Description("本選終了日時")]
		public string FinalEndTimeFixJst { get; }

		[DateTimeString]
		[PropertyOrder(9)]
		[Description("結果発表開始日時")]
		public string FinalResultStartTimeFixJst { get; }

		[DateTimeString]
		[PropertyOrder(10)]
		[Description("予選中間発表日時")]
		public string PreliminaryInterimStartTimeFixJst { get; }

		[DateTimeString]
		[PropertyOrder(11)]
		[Description("本選中間発表日時")]
		public string FinalInterimStartTimeFixJst { get; }

		[DateTimeString]
		[PropertyOrder(12)]
		[Description("ミッション開始日時（現地時間）")]
		public string MissionStartTime { get; }

		[DateTimeString]
		[PropertyOrder(13)]
		[Description("ミッション終了日時（現地時間）")]
		public string MissionEndTime { get; }

		[DateTimeString]
		[PropertyOrder(14)]
		[Description("ミッションリセット日時（現地時間）")]
		public string MissionResetTime { get; }

		[PropertyOrder(15)]
		[Description("対象ミッションID")]
		public IReadOnlyList<long> TargetMissionIdList { get; }

		[PropertyOrder(16)]
		[Description("本選出場定員数")]
		public int FinalCharacterCount { get; }

		[Nest(false, 0)]
		[PropertyOrder(17)]
		[Description("結果発表設定リスト")]
		public IReadOnlyList<ResultPresentationSetting> ResultPresentationSettingList { get; }

		[Nest(false, 0)]
		[PropertyOrder(18)]
		[Description("エントリーキャラクターリスト")]
		public IReadOnlyList<EntryCharacter> EntryCharacterList { get; }

		[Nest(false, 0)]
		[PropertyOrder(19)]
		[Description("殿堂入りキャラクターリスト")]
		public IReadOnlyList<PastChampionCharacter> PastChampionCharacterList { get; }

		[Nest(false, 0)]
		[PropertyOrder(20)]
		[Description("グループリスト")]
		public IReadOnlyList<EntryGroup> GroupList { get; }

		[Nest(false, 0)]
		[PropertyOrder(21)]
		[Description("投票報酬リスト")]
		public IReadOnlyList<PopularityVoteReward> PopularityVoteRewardList { get; }

		[SerializationConstructor]
        public PopularityVoteMB(long id, bool? isIgnore, string memo, PopularityVoteVotingType votingType, PopularityVoteSortType sortType, string startTimeFixJst, string endTimeFixJst, string preliminaryStartTimeFixJst, string preliminaryEndTimeFixJst, string finalStartTimeFixJst, string finalEndTimeFixJst, string finalResultStartTimeFixJst, string preliminaryInterimStartTimeFixJst, string finalInterimStartTimeFixJst, string missionStartTime, string missionEndTime, string missionResetTime, IReadOnlyList<long> targetMissionIdList, int finalCharacterCount, IReadOnlyList<ResultPresentationSetting> resultPresentationSettingList, IReadOnlyList<EntryCharacter> entryCharacterList, IReadOnlyList<PastChampionCharacter> pastChampionCharacterList, IReadOnlyList<EntryGroup> groupList, IReadOnlyList<PopularityVoteReward> popularityVoteRewardList)
			: base(id, isIgnore, memo)
		{
            this.VotingType = votingType;
            this.SortType = sortType;
            this.StartTimeFixJST = startTimeFixJst;
            this.EndTimeFixJST = endTimeFixJst;
            this.PreliminaryStartTimeFixJst = preliminaryStartTimeFixJst;
            this.PreliminaryEndTimeFixJst = preliminaryEndTimeFixJst;
            this.FinalStartTimeFixJst = finalStartTimeFixJst;
            this.FinalEndTimeFixJst = finalEndTimeFixJst;
            this.FinalResultStartTimeFixJst = finalResultStartTimeFixJst;
            this.PreliminaryInterimStartTimeFixJst = preliminaryInterimStartTimeFixJst;
            this.FinalInterimStartTimeFixJst = finalInterimStartTimeFixJst;
            this.MissionStartTime = missionStartTime;
            this.MissionEndTime = missionEndTime;
            this.MissionResetTime = missionResetTime;
            this.TargetMissionIdList = targetMissionIdList;
            this.FinalCharacterCount = finalCharacterCount;
            this.ResultPresentationSettingList = resultPresentationSettingList;
            this.EntryCharacterList = entryCharacterList;
            this.PastChampionCharacterList = pastChampionCharacterList;
            this.GroupList = groupList;
            this.PopularityVoteRewardList = popularityVoteRewardList;
		}

		public PopularityVoteMB()
			: base(0L, null, null)
		{
		}

		public DateTime GetTermEndJstDateTime(PopularityVoteType popularityVoteType)
		{
			return default(DateTime);
		}

		public bool IsMissionInTime(DateTime localDateTime)
		{
			return false;
		}
	}
}
