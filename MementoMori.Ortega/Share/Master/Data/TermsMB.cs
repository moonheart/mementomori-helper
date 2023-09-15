using System.ComponentModel;
using MementoMori.Ortega.Share.Master.Attributes;
using MementoMori.Ortega.Share.Terms;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[Description("利用規約")]
	[MessagePackObject(true)]
	public class TermsMB : MasterBookBase
	{
		[PropertyOrder(1)]
		[Description("TimeServerMBのID")]
		public long TimeServerId
		{
			get;
		}

		[PropertyOrder(2)]
		[Description("DMM用か？")]
		public bool IsDmm
		{
			get;
		}

		[PropertyOrder(3)]
		[Nest(false, 0)]
		[Description("利用規約")]
		public TermsButtonInfo Terms
		{
			get;
		}

		[Nest(false, 0)]
		[Description("プライバシーポリシー")]
		[PropertyOrder(4)]
		public TermsButtonInfo PrivacyPolicy
		{
			get;
		}

		[Description("サブスク規約")]
		[Nest(false, 0)]
		[PropertyOrder(5)]
		public TermsButtonInfo Subscription
		{
			get;
		}

		[Nest(false, 0)]
		[PropertyOrder(6)]
		[Description("その他")]
		public IReadOnlyList<TermsButtonInfo> TermsButtonInfos
		{
			get;
		}

        [DateTimeString]
        [PropertyOrder(7)]
        [Description("開始日時")]
        public string StartTime { get; }

		[SerializationConstructor]
		public TermsMB(long id, bool? isIgnore, string memo, long timeServerId, bool isDmm, TermsButtonInfo terms, TermsButtonInfo privacyPolicy, TermsButtonInfo subscription, IReadOnlyList<TermsButtonInfo> termsButtonInfos, string startTime)
			:base(id, isIgnore, memo)
		{
			TimeServerId = timeServerId;
			IsDmm = isDmm;
			Terms = terms;
			PrivacyPolicy = privacyPolicy;
			Subscription = subscription;
			TermsButtonInfos = termsButtonInfos;
            StartTime = startTime;
        }

		public TermsMB() :base(0L, false, ""){}
	}
}
