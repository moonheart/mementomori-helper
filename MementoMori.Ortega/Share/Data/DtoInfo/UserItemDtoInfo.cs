using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.DtoInfo
{
	[MessagePackObject(true)]
	public class UserItemDtoInfo : IUserItem
	{
		public long ItemCount
		{
			get;
			set;
		}

		public long ItemId
		{
			get;
			set;
		}

		public ItemType ItemType
		{
			get;
			set;
		}

		public long PlayerId
		{
			get;
			set;
		}

		public UserItemDtoInfo()
		{
		}
	}
}
