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
			// int num;
			// do
			// {
			// 	List<MypageIconInfo> list = this.<MypageIconInfos>k__BackingField;
			// 	num = 0;
			// 	bool flag;
			// 	if (flag)
			// 	{
			// 		bool flag2;
			// 		while (flag2)
			// 		{
			// 		}
			// 		bool flag3;
			// 		if (flag3)
			// 		{
			// 		}
			// 		if (num != 0)
			// 		{
			// 			goto IL_3B;
			// 		}
			// 	}
			// }
			// while (num != 0);
			// throw new NullReferenceException();
			// IL_3B:
			// throw new NullReferenceException();
			throw new NotImplementedException();
		}
	}
}
