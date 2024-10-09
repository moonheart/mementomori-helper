using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Notice
{
    [MessagePackObject(true)]
    [OrtegaApi("notice/getMypageNoticeInfoList", true, false)]
    public class GetMyPageNoticeInfoListRequest : ApiRequestBase
    {
        public NoticeCategoryType CategoryType { get; set; }

        public LanguageType LanguageType { get; set; }
    }
}