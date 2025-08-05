using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
    [MessagePackObject(true)]
    [Description("武器シンクログループ")]
    public class EquipmentSynchroGroupMB : MasterBookBase
    {
        [PropertyOrder(1)]
        [Description("ベース枠の枠数")]
        public int BaseCellCount { get; }

        [PropertyOrder(2)]
        [Description("無料枠フラグ")]
        public bool IsFree { get; }

        [Nest(false, 0)]
        [PropertyOrder(3)]
        [Description("解放に必要なアイテム")]
        public IReadOnlyList<UserItem> UnlockItemList { get; }

        [PropertyOrder(4)]
        [Description("表示順(昇順)")]
        public int DisplayOrder { get; }

        public EquipmentSynchroGroupMB(long id, bool? isIgnore, string memo, int baseCellCount, bool isFree, IReadOnlyList<UserItem> unlockItemList, int displayOrder)
            : base(id, isIgnore, memo)
        {
            this.BaseCellCount = baseCellCount;
            this.IsFree = isFree;
            this.UnlockItemList = unlockItemList ?? new List<UserItem>();
            this.DisplayOrder = displayOrder;
        }

        public EquipmentSynchroGroupMB(): base(0, null, string.Empty)
        {
        }
    }
}