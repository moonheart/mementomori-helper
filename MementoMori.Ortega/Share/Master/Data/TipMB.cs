using System.ComponentModel;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[Description("Tips")]
	[MessagePackObject(true)]
	public class TipMB : MasterBookBase
	{
		[PropertyOrder(1)]
		[Description("Tipメッセージキー")]
		public string MessageKey
		{
			get;
		}

		[PropertyOrder(2)]
		[Description("遷移先")]
		public ViewTransitionType ViewTransitionType
		{
			get;
		}

		[PropertyOrder(3)]
		[Description("解放判定")]
		public OpenCommandType OpenCommandType
		{
			get;
		}

		[SerializationConstructor]
		public TipMB(long id, bool? isIgnore, string memo, string messageKey, ViewTransitionType viewTransitionType, OpenCommandType openCommandType)
			:base(id, isIgnore, memo)
		{
			MessageKey = messageKey;
			ViewTransitionType = viewTransitionType;
			OpenCommandType = openCommandType;
		}

		public TipMB()  :base(0L, false, ""){}
	}
}
