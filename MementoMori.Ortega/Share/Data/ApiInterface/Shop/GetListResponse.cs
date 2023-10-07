using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Shop;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Shop;

[MessagePackObject(true)]
public class GetListResponse : ApiResponseBase
{
    public List<ShopTabInfo> ShopTabInfoList{ get; set; }

    // public ShopContractPrivilegeData GetContractPrivilegeData()
    // {
    // 	for (;;)
    // 	{
    // 		List<ShopTabInfo> list = this.<ShopTabInfoList>k__BackingField;
    // 		int num = 0;
    // 		bool flag;
    // 		if (flag)
    // 		{
    // 			bool flag2;
    // 			if (flag2)
    // 			{
    // 				break;
    // 			}
    // 			if (num != 0)
    // 			{
    // 				goto IL_4D;
    // 			}
    // 		}
    // 		if (num == 0)
    // 		{
    // 			goto Block_2;
    // 		}
    // 	}
    // 	ShopContractPrivilegeData shopContractPrivilegeData = new ShopContractPrivilegeData();
    // 	shopContractPrivilegeData.MbId = shopContractPrivilegeData;
    // 	return shopContractPrivilegeData;
    // 	Block_2:
    // 	throw new NullReferenceException();
    // 	IL_4D:
    // 	throw new NullReferenceException();
    // }
    //
    // public ShopMonthlyBoostData GetMonthlyBoostData()
    // {
    // 	for (;;)
    // 	{
    // 		List<ShopTabInfo> list = this.<ShopTabInfoList>k__BackingField;
    // 		int num = 0;
    // 		bool flag;
    // 		if (flag)
    // 		{
    // 			bool flag2;
    // 			if (flag2)
    // 			{
    // 				break;
    // 			}
    // 			if (num != 0)
    // 			{
    // 				goto IL_4D;
    // 			}
    // 		}
    // 		if (num == 0)
    // 		{
    // 			goto Block_2;
    // 		}
    // 	}
    // 	ShopMonthlyBoostData shopMonthlyBoostData = new ShopMonthlyBoostData();
    // 	shopMonthlyBoostData.MbId = shopMonthlyBoostData;
    // 	return shopMonthlyBoostData;
    // 	Block_2:
    // 	throw new NullReferenceException();
    // 	IL_4D:
    // 	throw new NullReferenceException();
    // }
}