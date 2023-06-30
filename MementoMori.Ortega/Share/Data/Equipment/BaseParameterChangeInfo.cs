using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Equipment
{
    [MessagePackObject(true)]
    [Description("基礎パラメータ変動情報")]
    public class BaseParameterChangeInfo
    {
        [Description("変動する基礎パラメータ")]
        [PropertyOrder(1)]
        public BaseParameterType BaseParameterType { get; set; }

        [Description("パラメータ増減タイプ")]
        [PropertyOrder(2)]
        public ChangeParameterType ChangeParameterType { get; set; }

        [PropertyOrder(3)]
        [Description("値")]
        public double Value { get; set; }

        public BaseParameterChangeInfo()
        {
        }
    }
}