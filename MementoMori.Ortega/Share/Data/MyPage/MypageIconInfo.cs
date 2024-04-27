using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.MyPage
{
	[MessagePackObject(true)]
	public class MypageIconInfo
	{
		public BadgeType BadgeType { get; set; }

		public int DisplayPriority { get; set; }

		public int HidePriority { get; set; }

		public string IconNameKey { get; set; }

		public long Id { get; set; }

		public long ImageId { get; set; }

        public bool IsBlackout { get; set; }

        public bool IsDisplayBadge { get; set; }

        public long NotOpenEventStoreIconId { get; set; }

        public long OpenContentLocalTimestamp { get; set; }

		public int SortOrder { get; set; }

		public List<MypageIconInfo> StoredIconInfoList { get; set; }

		public long StoreIconId { get; set; }

		public TransferDetailInfo TransferDetailInfo { get; set; }
	}
}
