using MessagePack;

namespace MementoMori.Ortega.ServerLib.Models.MySql.Dto
{
	[MessagePackObject(true)]
	public class UserShopCurrencyMissionRewardDtoInfo
	{
		public bool IsReceiveCommon { get; set; }

		public bool IsReceivePremium { get; set; }

		public long RequiredPoint { get; set; }
	}
}
