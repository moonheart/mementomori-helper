using System.ComponentModel;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
    [Description("Tips")]
    [MessagePackObject(true)]
    public class TipMB : MasterBookBase
    {
        [PropertyOrder(1)]
        [Description("Tipメッセージキー")]
        public string MessageKey { get; }

        [PropertyOrder(2)]
        [Description("IconID")]
        public long IconId { get; }

        [PropertyOrder(3)]
        [Description("遷移先")]
        public ViewTransitionType ViewTransitionType { get; }

        [PropertyOrder(4)]
        [Description("解放判定")]
        public OpenCommandType OpenCommandType { get; }

        [PropertyOrder(5)]
        [Description("表示対象バトル")]
        public BattleType BattleType { get; }

        [SerializationConstructor]
        public TipMB(long id, bool? isIgnore, string memo, string messageKey, long iconId, ViewTransitionType viewTransitionType, OpenCommandType openCommandType, BattleType battleType)
            : base(id, isIgnore, memo)
        {
            MessageKey = messageKey;
            IconId = iconId;
            ViewTransitionType = viewTransitionType;
            OpenCommandType = openCommandType;
            BattleType = battleType;
        }

        public TipMB() : base(0L, false, "")
        {
        }
    }
}