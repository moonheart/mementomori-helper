using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.MyPage
{
	[MessagePackObject(true)]
	public class MypageBannerInfo
	{
		public int DisplayPriority { get; set; }

		public long ImageId { get; set; }

        public long MBId { get; set; }
        
		public int SortOrder { get; set; }

		public TransferDetailInfo TransferDetailInfo { get; set; }
	}
}
