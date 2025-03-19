using System.ComponentModel;
using MementoMori.Ortega.Share.Master.Attributes;
using MementoMori.Ortega.Share.Terms;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("利用規約")]
	public class TermsMB : MasterBookBase
	{
		[PropertyOrder(1)]
		[Description("TimeServerMBのID")]
		public long TimeServerId { get; }

		[Nest(false, 0)]
		[PropertyOrder(2)]
		[Description("利用規約")]
		public TermsButtonInfo Terms { get; }

		[Nest(false, 0)]
		[PropertyOrder(3)]
		[Description("プライバシーポリシー")]
		public TermsButtonInfo PrivacyPolicy { get; }

		[Nest(false, 0)]
		[PropertyOrder(4)]
		[Description("サブスク規約")]
		public TermsButtonInfo Subscription { get; }

		[Nest(false, 0)]
		[PropertyOrder(5)]
		[Description("その他")]
		public IReadOnlyList<TermsButtonInfo> TermsButtonInfos { get; }

		[DateTimeString]
		[PropertyOrder(6)]
		[Description("開始日時")]
		public string StartTime { get; }

		[SerializationConstructor]
		public TermsMB(long id, bool? isIgnore, string memo, long timeServerId, TermsButtonInfo terms, TermsButtonInfo privacyPolicy, TermsButtonInfo subscription, IReadOnlyList<TermsButtonInfo> termsButtonInfos, string startTime)
			: base(id, isIgnore, memo)
		{
            this.TimeServerId = timeServerId;
            this.Terms = terms;
            this.PrivacyPolicy = privacyPolicy;
            this.Subscription = subscription;
            this.TermsButtonInfos = termsButtonInfos;
            this.StartTime = startTime;
		}

		public TermsMB()
			: base(0L, null, null)
		{
		}
	}
}
