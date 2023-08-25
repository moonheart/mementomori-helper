using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
    [Description("基礎パラメータの種類")]
    public enum BaseParameterType
    {
        [Description("筋力")] Muscle = 1,
        [Description("技力")] Energy = 2,
        [Description("魔力")] Intelligence = 3,
        [Description("耐久力")] Health = 4
    }
}