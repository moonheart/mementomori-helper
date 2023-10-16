using System.ComponentModel;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
    [MessagePackObject(true)]
    [Description("国")]
    public class StateMB : MasterBookBase
    {
        [PropertyOrder(6)]
        [Description("クリファテキストキー")]
        public long AppearQliphaId { get; }

        [PropertyOrder(5)]
        [Description("クリファテキストキー")]
        public string AppearQliphaKey { get; }

        [PropertyOrder(8)]
        [Description("開始時間(JP)")]
        public float BgmStartTimeJP { get; }

        [PropertyOrder(9)]
        [Description("開始時間(US)")]
        public float BgmStartTimeUS { get; }

        [PropertyOrder(1)]
        [Description("大陸ID")]
        public long ContinentId { get; }

        [PropertyOrder(2)]
        [Description("名称キー")]
        public string NameKey { get; }

        [PropertyOrder(7)]
        [Description("国ボーナスID")]
        public long StateBonusId { get; }

        [PropertyOrder(3)]
        [Description("サブ名称キー")]
        public string SubNameKey { get; }

        [PropertyOrder(4)]
        [Description("テキストキー")]
        public string TextKey { get; }

        [SerializationConstructor]
        public StateMB(long id, bool? isIgnore, string memo, long continentId, string nameKey, string subNameKey, string textKey, string appearQliphaKey, long appearQliphaId, long stateBonusId, float bgmStartTimeJP, float bgmStartTimeUS)
            : base(id, isIgnore, memo)
        {
            ContinentId = continentId;
            NameKey = nameKey;
            SubNameKey = subNameKey;
            TextKey = textKey;
            AppearQliphaKey = appearQliphaKey;
            AppearQliphaId = appearQliphaId;
            StateBonusId = stateBonusId;
            BgmStartTimeJP = bgmStartTimeJP;
            BgmStartTimeUS = bgmStartTimeUS;
        }

        public StateMB() : base(0L, false, "")
        {
        }

        public float GetBgmStartTime(LanguageType languageType)
        {
            if (languageType == LanguageType.jaJP)
            {
                return this.BgmStartTimeJP;
            }

            return this.BgmStartTimeUS;
        }
    }
}