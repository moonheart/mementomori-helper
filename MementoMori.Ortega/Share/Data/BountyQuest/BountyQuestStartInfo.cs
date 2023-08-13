using System.ComponentModel;
using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.BountyQuest
{
    [MessagePackObject(true)]
    public class BountyQuestStartInfo
    {
        [Description("懸賞カウンタークエストID")]
        public long BountyQuestId { get; set; }

        [Description("懸賞カウンタークエスト派遣メンバー情報")]
        public List<BountyQuestMemberInfo> BountyQuestMemberInfos { get; set; }
    }
}