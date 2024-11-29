using MementoMori.Ortega.Share.Data.Notice;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Notice;

[MessagePackObject(true)]
public class GetNoticeInfoListResponse : ApiResponseBase
{
    public List<NoticeInfo> NoticeInfoList { get; set; }

    public List<NoticeInfo> EventInfoList { get; set; }
}