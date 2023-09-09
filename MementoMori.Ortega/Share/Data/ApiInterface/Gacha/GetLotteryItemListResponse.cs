using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Gacha;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Gacha
{
	[MessagePackObject(true)]
	public class GetLotteryItemListResponse : ApiResponseBase
	{
		public List<GachaBonusRate> GachaBonusRateList { get; set; }

		public List<GachaItemRate> GachaItemRateList { get; set; }

        public List<GachaItemRate> GachaItemRateSpecialList { get; set; }

        public List<GachaRarityRate> GachaRarityRateList { get; set; }

        public List<GachaRarityRate> GachaRarityRateSpecialList { get; set; }
    }
}
