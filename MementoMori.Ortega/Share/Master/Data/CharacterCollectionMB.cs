using System.ComponentModel;
using MementoMori.Ortega.Share.Master.Attributes;
using MementoMori.Ortega.Share.Utils;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
    [MessagePackObject(true)]
    [Description("アルカナ")]
    public class CharacterCollectionMB : MasterBookBase, IHasJstStartEndTime
    {
        [PropertyOrder(1)]
        [Description("名称キー")]
        public string NameKey { get; }

        [PropertyOrder(2)]
        [Description("開放に必要なキャラーIDリスト")]
        public IReadOnlyList<long> RequiredCharacterIds { get; }

        [PropertyOrder(5)]
        [Description("表示開始達成パーティLv")]
        public long RequiredPartyLv { get; }

        [SerializationConstructor]
        public CharacterCollectionMB(long id, bool? isIgnore, string memo, string nameKey, IReadOnlyList<long> requiredCharacterIds, long requiredPartyLv, string startTimeFixJST, string endTimeFixJST)
            : base(id, isIgnore, memo)
        {
            this.NameKey = nameKey;
            this.RequiredCharacterIds = requiredCharacterIds;
            this.RequiredPartyLv = requiredPartyLv;
            this.StartTimeFixJST = startTimeFixJST;
            this.EndTimeFixJST = endTimeFixJST;
        }

        public CharacterCollectionMB()
            : base(0L, null, null)
        {
        }

        [PropertyOrder(4)]
        [Description("新アルカナの終了時間(JST)")]
        public string EndTimeFixJST { get; }

        [PropertyOrder(3)]
        [Description("新アルカナの開始時間(JST)")]
        public string StartTimeFixJST { get; }
    }
}
