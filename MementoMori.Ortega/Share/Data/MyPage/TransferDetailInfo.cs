using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Interface;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.MyPage
{
	[MessagePackObject(true)]
	public class TransferDetailInfo : IDeepCopy<TransferDetailInfo>
	{
		[Description("遷移先詳細１")]
		[PropertyOrder(2)]
		public long NumberInfo1
		{
			get;
			set;
		}

		[Description("遷移先詳細２")]
		[PropertyOrder(3)]
		public long NumberInfo2
		{
			get;
			set;
		}

		[Description("遷移先詳細３")]
		[PropertyOrder(4)]
		public string StringInfo
		{
			get;
			set;
		}

		[Description("遷移タイプ")]
		[PropertyOrder(1)]
		public TransferSpotType TransferSpotType
		{
			get;
			set;
		}

		public TransferDetailInfo DeepCopy()
		{
			TransferDetailInfo transferDetailInfo = new TransferDetailInfo();
			long num = this.NumberInfo1;
			transferDetailInfo.NumberInfo1 = num;
			long num2 = this.NumberInfo2;
			transferDetailInfo.NumberInfo2 = num2;
			string text = this.StringInfo;
			transferDetailInfo.StringInfo = text;
			TransferSpotType transferSpotType = this.TransferSpotType;
			transferDetailInfo.TransferSpotType = transferSpotType;
			return transferDetailInfo;
		}

		public TransferDetailInfo()
		{
		}
	}
}
