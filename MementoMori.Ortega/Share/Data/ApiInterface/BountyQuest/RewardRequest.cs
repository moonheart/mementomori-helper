using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.BountyQuest
{
    [OrtegaApi("bountyQuest/reward", true, false)]
    [MessagePackObject(true)]
    public class RewardRequest : ApiRequestBase
    {
        public List<long> BountyQuestIds { get; set; }

        public long ConsumeCurrency { get; set; }

        public bool IsQuick { get; set; }

        public RewardRequest()
        {
        }
    }
}