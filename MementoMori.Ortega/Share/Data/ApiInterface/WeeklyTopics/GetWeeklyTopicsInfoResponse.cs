using MementoMori.Ortega.Share.Data.WeeklyTopics;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.WeeklyTopics;

[MessagePackObject(true)]
public class GetWeeklyTopicsInfoResponse : ApiResponseBase
{
    public List<WeeklyTopicsTopCharacterData> TopCharacterDataList { get; set; }

    public WeeklyTopicsPvpData BattleLeagueData { get; set; }

    public WeeklyTopicsPvpData LegendLeagueData { get; set; }

    public WeeklyTopicsBossBattleData BossBattleData { get; set; }

    public WeeklyTopicsShopData ShopData { get; set; }
}