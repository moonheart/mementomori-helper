using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Notice;

[MessagePackObject(true)]
[OrtegaAuth("notice/getNoticeInfoList", false, true)]
public class GetNoticeInfoListRequest : ApiRequestBase
{
    public NoticeAccessType AccessType { get; set; }

    public string CountryCode { get; set; }

    public LanguageType LanguageType { get; set; }

    public long UserId { get; set; }
}