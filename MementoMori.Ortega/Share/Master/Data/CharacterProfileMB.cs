using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Global;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
    [MessagePackObject(true)]
    [Description("キャラクタープロフィール")]
    public class CharacterProfileMB : MasterBookBase
    {
        [Description("誕生日")]
        [PropertyOrder(4)]
        public int Birthday { get; }

        [Description("血液型")]
        [PropertyOrder(5)]
        public CharacterBloodType BloodType { get; }

        [Description("CV(JP)キー")]
        [PropertyOrder(10)]
        public string CharacterVoiceJPKey { get; }

        [PropertyOrder(11)]
        [Description("CV(US)キー")]
        public string CharacterVoiceUSKey { get; }

        [PropertyOrder(7)]
        [Description("説明キー")]
        public string DescriptionKey { get; }

        [PropertyOrder(6)]
        [Description("専属武具の欠片ID")]
        public long EquipmentCompositeId { get; }

        [PropertyOrder(16)]
        [Description("新キャラ演出メッセージ2キー")]
        public string GachaResultMessage2Key { get; }

        [Description("身長")]
        [PropertyOrder(2)]
        public int Height { get; }

        [PropertyOrder(12)]
        [Description("ラメント(JP)キー")]
        public string LamentJPKey { get; }

        [PropertyOrder(13)]
        [Description("ラメント(US)キー")]
        public string LamentUSKey { get; }

        [PropertyOrder(17)]
        [Description("日本語キャラソン歌詞")]
        public string LyricsJPKey { get; }

        [Description("英語キャラソン歌詞")]
        [PropertyOrder(18)]
        public string LyricsUSKey { get; }

        [Description("動画配信サイトURL(JP)")]
        [PropertyOrder(8)]
        [Nest(false, 0)]
        public TranslatedText MovieJpUrl { get; }

        [PropertyOrder(9)]
        [Description("動画配信サイトURL(US)")]
        [Nest(false, 0)]
        public TranslatedText MovieUsUrl { get; }

        [Description("歌手(JP)キー")]
        [PropertyOrder(14)]
        public string VocalJPKey { get; }

        [Description("歌手(US)キー")]
        [PropertyOrder(15)]
        public string VocalUSKey { get; }

        [Description("体重")]
        [PropertyOrder(3)]
        public int Weight { get; }

        [SerializationConstructor]
        public CharacterProfileMB(long id, bool? isIgnore, string memo, int birthday, CharacterBloodType bloodType, string characterVoiceJPKey, string characterVoiceUSKey, long equipmentCompositeId,
            int height, string descriptionKey, string vocalJPKey, string vocalUSKey, string lamentJPKey, string lamentUSKey, int weight, TranslatedText movieJpUrl, TranslatedText movieUsUrl,
            string gachaResultMessage2Key, string lyricsJPKey, string lyricsUSKey)
            : base(id, isIgnore, memo)
        {
            Birthday = birthday;
            BloodType = bloodType;
            CharacterVoiceJPKey = characterVoiceJPKey;
            CharacterVoiceUSKey = characterVoiceUSKey;
            EquipmentCompositeId = equipmentCompositeId;
            Height = height;
            DescriptionKey = descriptionKey;
            VocalJPKey = vocalJPKey;
            VocalUSKey = vocalUSKey;
            LamentJPKey = lamentJPKey;
            LamentUSKey = lamentUSKey;
            Weight = weight;
            MovieJpUrl = movieJpUrl;
            MovieUsUrl = movieUsUrl;
            GachaResultMessage2Key = gachaResultMessage2Key;
            LyricsJPKey = lyricsJPKey;
            LyricsUSKey = lyricsUSKey;
        }

        public CharacterProfileMB() : base(0L, null, null)
        {
        }
    }
}