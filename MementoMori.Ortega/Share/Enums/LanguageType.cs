using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
    [Description("言語")]
    public enum LanguageType
    {
        None,
        [Description("日本語")]
        jaJP,
        [Description("英語")]
        enUS,
        [Description("韓国語")]
        koKR,
        [Description("中国語(繁体字)")]
        zhTW,
        [Description("フランス語")]
        frFR,
        [Description("中国語(簡体字)")]
        zhCN,
        [Description("スペイン語")]
        esMX,
        [Description("ポルトガル語")]
        ptBR,
        [Description("タイ語")]
        thTH,
        [Description("インドネシア語")]
        idID,
        [Description("ベトナム語")]
        viVN,
        [Description("ロシア語")]
        ruRU,
        [Description("ドイツ語")]
        deDE,
        [Description("アラビア語")]
        arEG
    }
}