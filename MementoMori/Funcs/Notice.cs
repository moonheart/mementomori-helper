using MementoMori.Ortega.Share.Data.ApiInterface.Notice;

namespace MementoMori;

public partial class MementoMoriFuncs
{
    public async Task GetNoticeInfoList()
    {
        var countryCode = OrtegaConst.Addressable.LanguageNameDictionary[NetworkManager.LanguageType];

        var response = await GetResponse<GetNoticeInfoListRequest, GetNoticeInfoListResponse>(new GetNoticeInfoListRequest
        {
            AccessType = NoticeAccessType.Title,
            CategoryType = NoticeCategoryType.NoticeTab,
            CountryCode = countryCode,
            LanguageType = NetworkManager.LanguageType,
            UserId = AuthOption.UserId
        });
        NoticeInfoList = response.NoticeInfoList.Where(d => d.Id % 10 != 6).ToList();
        var response2 = await GetResponse<GetNoticeInfoListRequest, GetNoticeInfoListResponse>(new GetNoticeInfoListRequest
        {
            AccessType = NoticeAccessType.MyPage,
            CategoryType = NoticeCategoryType.EventTab,
            CountryCode = countryCode,
            LanguageType = NetworkManager.LanguageType,
            UserId = AuthOption.UserId
        });
        EventInfoList = response2.NoticeInfoList
            .GroupBy(n => n.Title)
            .Select(g => g.First())
            .ToList();
    }
}