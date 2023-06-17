using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
    [Description("パラメーター増減タイプ")]
    public enum ChangeParameterType
    {
        [Description("加算(+X)")] Addition = 1,
        [Description("乗算(+X%)")] AdditionPercent,
        [Description("キャラLv×係数")] CharacterLevelConstantMultiplicationAddition
    }
}