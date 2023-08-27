using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Equipment
{
    [Description("セット装備の効果")]
    [MessagePackObject(true)]
    public class EquipmentSetEffect
    {
        [Nest(true, 1)]
        [PropertyOrder(2)]
        [Description("発動効果(BaseParameter)")]
        public BaseParameterChangeInfo BaseParameterChangeInfo { get; set; }

        [Nest(true, 1)]
        [PropertyOrder(3)]
        [Description("発動効果(BattleParameter)")]
        public BattleParameterChangeInfo BattleParameterChangeInfo { get; set; }

        [PropertyOrder(1)]
        [Description("発動に必要な装備数")]
        public long RequiredEquipmentCount { get; set; }
    }
}