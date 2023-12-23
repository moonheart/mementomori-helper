using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
    [Description("言語")]
    public enum LanguageType
    {
        None = 0,
        [Description("日本語")] jaJP = 1,
        [Description("英語")] enUS = 2,
        [Description("韓国語")] koKR = 3,
        [Description("中国語(繁体字)")] zhTW = 4,
        [Description("フランス語")] frFR = 5,
        [Description("中国語(簡体字)")] zhCN = 6,
        [Description("スペイン語")] esMX = 7,
        [Description("ポルトガル語")] ptBR = 8,
        [Description("タイ語")] thTH = 9,
        [Description("インドネシア語")] idID = 10,
        [Description("ベトナム語")] viVN = 11,
        [Description("ロシア語")] ruRU = 12,
        [Description("ドイツ語")] deDE = 13
    }
}