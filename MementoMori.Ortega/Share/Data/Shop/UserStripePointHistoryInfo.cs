using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Shop
{
	[MessagePackObject(true)]
	public class UserStripePointHistoryInfo
	{
		public long PlayerId { get; set; }

		public DateTime BuyDateTime { get; set; }

		public long Price { get; set; }

		public long DiscountPrice { get; set; }

		public long BeforePoint { get; set; }

		public long AfterPoint { get; set; }

		public long SavePoint { get; set; }

		public long UsePoint { get; set; }

		public string ProductNameKey { get; set; }

		public string TransactionId { get; set; }

		public StripePointType StripePointType { get; set; }

		public StripePaidType StripePaidType { get; set; }

		public string CardSubInfo { get; set; }

		public string RefundDateTime { get; set; }

		public string ChargeBackDateTime { get; set; }
	}
}
