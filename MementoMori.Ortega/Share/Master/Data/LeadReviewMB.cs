using System.ComponentModel;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("レビュー誘導用データ")]
	public class LeadReviewMB : MasterBookBase
	{
		[Description("リセット日時")]
		[PropertyOrder(1)]
		public string ResetDate
		{
			get;
		}

		[SerializationConstructor]
		public LeadReviewMB(long id, bool? isIgnore, string memo, string resetDate)
			:base(id, isIgnore, memo)
		{
			ResetDate = resetDate;
		}

		public LeadReviewMB() :base(0L, false, ""){}
	}
}
