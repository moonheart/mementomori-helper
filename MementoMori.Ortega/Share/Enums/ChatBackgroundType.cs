using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums;

[Description("チャット背景タイプ")]
public enum ChatBackgroundType
{
    [Description("デフォルト")] Default,

    [Description("ベースの色変更(暖色)＋とけねこワンポイント")]
    Warm,
    [Description("ベースの色変更(寒色)＋タイトルロゴ")] Cool,
    [Description("とけねこスタンプちりばめ")] Studded,

    [Description("メメモリ調の筆跡やインクのにじみのようなデザイン")]
    Smear
}