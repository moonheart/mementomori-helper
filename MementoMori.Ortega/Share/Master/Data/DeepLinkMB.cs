using System.ComponentModel;
using MementoMori.Ortega.Share.Data.MyPage;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("ディープリンク")]
	public class DeepLinkMB : MasterBookBase
	{
		[PropertyOrder(1)]
		[Description("遷移タイプ")]
		[Nest(false, 0)]
		public TransferDetailInfo TransferDetailInfo
		{
			get;
		}

		[SerializationConstructor]
		public DeepLinkMB(long id, bool? isIgnore, string memo, TransferDetailInfo transferDetailInfo)
			:base(id, isIgnore, memo)
		{
			TransferDetailInfo = transferDetailInfo;
		}

		public DeepLinkMB() : base(0, false, "")
		{
		}
	}
}
