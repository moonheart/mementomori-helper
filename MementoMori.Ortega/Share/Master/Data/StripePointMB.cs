using System.ComponentModel;
using MementoMori.Ortega.Share.Master.Attributes;
using MementoMori.Ortega.Share.Utils;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
    [MessagePackObject(true)]
    [Description("Stripeクーポン")]
    public class StripePointMB : MasterBookBase, IHasJstStartEndTime
    {
        [PropertyOrder(1)]
        [Description("貯めるクーポン額")]
        public int SavePointPercent { get; }

        [SerializationConstructor]
        public StripePointMB(long id, bool? isIgnore, string memo, int savePointPercent, string startTimeFixJST, string endTimeFixJST)
            : base(id, isIgnore, memo)
        {
            SavePointPercent = savePointPercent;
            StartTimeFixJST = startTimeFixJST;
            EndTimeFixJST = endTimeFixJST;
        }

        public StripePointMB() : base(0, false, "")
        {
        }

        [PropertyOrder(2)]
        [Description("クーポンの開始時間(JST)")]
        public string StartTimeFixJST { get; }

        [PropertyOrder(3)]
        [Description("クーポンの終了時間(JST)")]
        public string EndTimeFixJST { get; }
    }
}