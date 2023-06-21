using System.Collections;
using System.ComponentModel;
using MementoMori.Ortega.Share.Data.MyPage;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("コミュニティ")]
	public class CommunityMB : MasterBookBase
	{
		[Description("地域タイプ")]
		[PropertyOrder(1)]
		public CountryCodeType CountryCodeType
		{
			get;
		}

		[PropertyOrder(2)]
		[Nest(false, 0)]
		[Description("SNS情報リスト")]
		public IReadOnlyList<SnsInfo> SnsInfoList
		{
			get;
		}

		[SerializationConstructor]
		public CommunityMB(long id, bool? isIgnore, string memo, CountryCodeType countryCodeType, IReadOnlyList<SnsInfo> snsInfoList)
			:base(id, isIgnore, memo)
		{
			CountryCodeType = countryCodeType;
			SnsInfoList = snsInfoList;
		}

		public CommunityMB() : base(0, false, "")
		{
		}

		public SnsInfo GetSnsInfo(MissionAchievementType missionAchievementType)
		{
			// int num;
			// int num3;
			// for (;;)
			// {
			// 	IReadOnlyList<SnsInfo> readOnlyList = this.SnsInfoList;
			// 	num = 0;
			// 	if (typeof(IEnumerator).TypeHandle != 0)
			// 	{
			// 		uint num2;
			// 		if (num >= (int)num2)
			// 		{
			// 			goto IL_23;
			// 		}
			// 		num += num;
			// 		if (num != (int)num2)
			// 		{
			// 			break;
			// 		}
			// 		num3 += num3;
			// 	}
			// 	if ("{il2cpp array field local7->}" != (ulong)0L)
			// 	{
			// 	}
			// 	if (num == 0)
			// 	{
			// 		goto Block_3;
			// 	}
			// }
			// num++;
			// IL_23:
			// num3 = 0;
			// return typeof(IEnumerator).TypeHandle;
			// Block_3:
			// throw new NullReferenceException();
			throw new NotImplementedException();
		}
	}
}
