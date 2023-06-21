using System.ComponentModel;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("APIエラーコード")]
	public class ErrorCodeMB : MasterBookBase
	{
		[PropertyOrder(1)]
		[Description("メッセージタイプ")]
		public ErrorMessageType ErrorMessageType
		{
			get;
		}

		[SerializationConstructor]
		public ErrorCodeMB(long id, bool? isIgnore, string memo, ErrorMessageType errorMessageType)
			:base(id, isIgnore, memo)
		{
			ErrorMessageType = errorMessageType;
		}

		public ErrorCodeMB() : base(0, false, "")
		{
		}
	}
}
