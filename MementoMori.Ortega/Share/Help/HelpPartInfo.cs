using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Help
{
    [Description("ヘルプパート")]
    [MessagePackObject(true)]
    public class HelpPartInfo
    {
        [Description("見出し")]
        [PropertyOrder(1)]
        public string HeadLine { get; set; }

        [Nest(true, 1)]
        [PropertyOrder(2)]
        [Description("本文リスト")]
        public IReadOnlyList<HelpMainText> MainTextList { get; set; }
        
        [Description("本文")]
        [PropertyOrder(4)]
        public string MainText { get; set; }

        [PropertyOrder(3)]
        [Description("付加情報")]
        public HelpParameterType HelpParameterType { get; set; }

        public HelpPartInfo()
        {
        }
    }
}