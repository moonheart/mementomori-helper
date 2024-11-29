using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Notice;

[MessagePackObject(true)]
[OrtegaApi("notice/getMypageNoticeInfoList")]
public class GetMyPageNoticeInfoListRequest : ApiRequestBase
{
    public LanguageType LanguageType { get; set; }
}