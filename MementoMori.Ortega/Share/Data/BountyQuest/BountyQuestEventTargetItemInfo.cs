using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.BountyQuest
{
	[MessagePackObject(true)]
	public class BountyQuestEventTargetItemInfo
	{
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

		public bool Equals(IUserItem userItem)
		{
			throw new System.NotImplementedException();
		}

		public BountyQuestEventTargetItemInfo()
		{
		}
	}
}
