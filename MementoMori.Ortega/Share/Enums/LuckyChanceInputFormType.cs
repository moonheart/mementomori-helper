using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums;

public enum LuckyChanceInputFormType
{
    [Description("不明")] None,
    [Description("景品選択")] PrizeSelection,
    [Description("個人情報入力")] InputPersonalInfo,
    [Description("個人情報入力(メアドのみ)")] InputMailAddress,
    [Description("個人情報登録済み")] RegisteredPersonalInfo
}