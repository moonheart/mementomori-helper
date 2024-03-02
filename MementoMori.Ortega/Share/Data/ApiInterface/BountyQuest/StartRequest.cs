using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.BountyQuest;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.BountyQuest
{
    [OrtegaApi("bountyQuest/start", true, false)]
    [MessagePackObject(true)]
    public class StartRequest : ApiRequestBase
    {
        public List<BountyQuestStartInfo> BountyQuestStartInfos { get; set; }

        public bool IsSplit { get; set; }
    }
}