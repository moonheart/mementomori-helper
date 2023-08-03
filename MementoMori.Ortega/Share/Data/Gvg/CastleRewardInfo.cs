using MementoMori.Ortega.Share.Data.Item;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Gvg
{
	[MessagePackObject(true)]
	public class CastleRewardInfo
	{
		public long CastleId { get; set; }

		public List<UserItem> LotteryRewardList { get; set; }
	}
}
