using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Global;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Extensions;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[Description("お問い合わせボタン")]
	[MessagePackObject(true)]
	public class InquiryButtonMB : MasterBookBase
	{
		[PropertyOrder(1)]
		[Description("ボタンテキスト")]
		public string ButtonTextKey
		{
			get;
			set;
		}

		[PropertyOrder(2)]
		[Description("ボタンタイプ")]
		public InquiryButtonType ButtonType
		{
			get;
			set;
		}

		[PropertyOrder(4)]
		[Description("メール本文キー")]
		public string MailBodyKey
		{
			get;
			set;
		}

		[PropertyOrder(3)]
		[Description("メール件名キー")]
		public string MailSubjectKey
		{
			get;
			set;
		}

		[PropertyOrder(6)]
		[Description("タイムサーバーID")]
		public IReadOnlyList<long> TimeServerIds
		{
			get;
		}

		[Nest(false, 0)]
		[PropertyOrder(5)]
		[Description("遷移先URL")]
		public TranslatedText TransferUrl
		{
			get;
			set;
		}

		[SerializationConstructor]
		public InquiryButtonMB(long id, bool? isIgnore, string memo, string buttonTextKey, InquiryButtonType buttonType, string mailSubjectKey, string mailBodyKey, TranslatedText transferUrl, IReadOnlyList<long> timeServerIds)
			:base(id, isIgnore, memo)
		{
			ButtonTextKey = buttonTextKey;
			ButtonType = buttonType;
			MailSubjectKey = mailSubjectKey;
			MailBodyKey = mailBodyKey;
			TransferUrl = transferUrl;
			TimeServerIds = timeServerIds;
		}

		public InquiryButtonMB() : base(0, false, "")
		{
		}

		public bool IsTarget(long timeServerId)
		{
			return this.TimeServerIds.IsNullOrEmpty<long>() || Enumerable.Contains<long>(this.TimeServerIds, timeServerId);
		}
	}
}
