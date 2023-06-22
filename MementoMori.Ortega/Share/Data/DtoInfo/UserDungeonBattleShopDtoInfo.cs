using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.TradeShop;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.DtoInfo
{
	[MessagePackObject(true)]
	public class UserDungeonBattleShopDtoInfo
	{
		public string GridGuid
		{
			get;
			set;
		}

		public long PlayerId
		{
			get;
			set;
		}

		public long TermId
		{
			get;
			set;
		}

		public List<TradeShopItem> TradeShopItemList
		{
			get;
			set;
		}

		public UserDungeonBattleShopDtoInfo()
		{
		}
	}
}
