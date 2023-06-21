using System.ComponentModel;
using MementoMori.Ortega.Share.Master.Attributes;
using MementoMori.Ortega.Share.Utils;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[Description("ミッションガイド")]
	[MessagePackObject(true)]
	public class MissionGuideMB : MasterBookBase, IHasStartEndTime
	{
		[Description("ガイドID")]
		[PropertyOrder(1)]
		public long GuideId
		{
			get;
		}

		[Description("ミッションId")]
		[PropertyOrder(2)]
		public long MissionId
		{
			get;
		}

		[Description("ガイド表示優先度A")]
		[PropertyOrder(5)]
		public int SortOrderA
		{
			get;
		}

		[PropertyOrder(6)]
		[Description("ガイド表示優先度B")]
		public int SortOrderB
		{
			get;
		}

		[SerializationConstructor]
		public MissionGuideMB(long id, bool? isIgnore, string memo, long guideId, long missionId, string startTime, string endTime, int sortOrderA, int sortOrderB)
			:base(id, isIgnore, memo)
		{
			GuideId = guideId;
			MissionId = missionId;
			SortOrderA = sortOrderA;
			SortOrderB = sortOrderB;
		}

		public MissionGuideMB() :base(0L, false, ""){}

		[PropertyOrder(4)]
		[Description("ユーザ登録終了日時")]
		public string EndTime
		{
			get;
		}

		[Description("ユーザ登録開始日時")]
		[PropertyOrder(3)]
		public string StartTime
		{
			get;
		}
	}
}
