using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Character;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
    [MessagePackObject(true)]
    [Description("朗読")]
    public class MonologueMB : MasterBookBase
    {
        [PropertyOrder(1)]
        [Description("CharacterDteailVoiceMBのID")]
        public long CharacterDetailVoiceId { get; }

        [PropertyOrder(2)]
        [Description("キャラクターID")]
        public long CharacterId { get; }

        [Nest(false, 0)]
        [PropertyOrder(3)]
        [Description("朗読設定_JP")]
        public IReadOnlyList<MonologueSettingData> MonologueSettingDatasJP { get; }

        [Nest(false, 0)]
        [PropertyOrder(4)]
        [Description("朗読設定_US")]
        public IReadOnlyList<MonologueSettingData> MonologueSettingDatasUS { get; }

        [DateTimeString]
        [PropertyOrder(5)]
        [Description("表示開始日時")]
        public string StartTimeFixJST { get; }

        [PropertyOrder(6)]
        [Description("朗読再生BGM設定")]
        public MonologueBgmType MonologueBgmType { get; }

        [SerializationConstructor]
        public MonologueMB(long id, bool? isIgnore, string memo, long characterId, IReadOnlyList<MonologueSettingData> monologueSettingDatasJP,
            IReadOnlyList<MonologueSettingData> monologueSettingDatasUS, string StartTimeFixJst, long characterDetailVoiceId, MonologueBgmType monologueBgmType)
            : base(id, isIgnore, memo)
        {
            this.CharacterId = characterId;
            this.MonologueSettingDatasJP = monologueSettingDatasJP;
            this.MonologueSettingDatasUS = monologueSettingDatasUS;
            this.StartTimeFixJST = StartTimeFixJst;
            this.CharacterDetailVoiceId = characterDetailVoiceId;
            this.MonologueBgmType = monologueBgmType;
        }

        public MonologueMB()
            : base(0, null, null)
        {
        }

        public IReadOnlyList<MonologueSettingData> GetMonologueSettingDatas(LanguageType voiceLanguageType)
        {
            if (voiceLanguageType != LanguageType.jaJP && voiceLanguageType == LanguageType.enUS)
            {
                return this.MonologueSettingDatasUS;
            }

            return this.MonologueSettingDatasJP;
        }
    }
}