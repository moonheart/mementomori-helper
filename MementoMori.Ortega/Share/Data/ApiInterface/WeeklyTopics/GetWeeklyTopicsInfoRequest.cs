using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.WeeklyTopics;

[MessagePackObject(true)]
[OrtegaApi("weeklyTopics/getWeeklyTopicsInfo")]
public class GetWeeklyTopicsInfoRequest : ApiRequestBase
{
}