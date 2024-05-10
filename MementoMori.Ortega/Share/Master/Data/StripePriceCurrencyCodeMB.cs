using System.ComponentModel;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("Stripe国料金コード")]
	public class StripePriceCurrencyCodeMB : MasterBookBase
	{
		[PropertyOrder(1)]
		[Description("国料金コード")]
		public string CurrencyCode { get; }

		[PropertyOrder(1)]
		[Description("説明")]
		public string Discription { get; }

		[SerializationConstructor]
		public StripePriceCurrencyCodeMB(long id, bool? isIgnore, string memo, string currencyCode, string discription)
            : base(id, isIgnore, memo)
		{
			CurrencyCode = currencyCode;
            Discription = discription;
		}

		public StripePriceCurrencyCodeMB(): base(0, false, "")
        {
        }
	}
}
