using MementoMori.Ortega.Share.Data.Notice;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Notice
{
    [MessagePackObject(true)]
    public class GetMyPageNoticeInfoListResponse : ApiResponseBase
    {
        public List<NoticeInfo> NoticeInfoList { get; set; }
    }
}