using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.MyPage
{
	[MessagePackObject(true)]
	public class DisplayMypageInfo
	{
		public List<MypageBannerInfo> MypageBannerInfos { get; set; }

		public List<MypageIconInfo> MypageIconInfos{ get; set; }

		public MypageIconInfo GetMypageIconInfoByTransferSpotType(TransferSpotType type)
        {
            return MypageIconInfos.FirstOrDefault(d => d.TransferDetailInfo.TransferSpotType == type);
		}
	}
}
