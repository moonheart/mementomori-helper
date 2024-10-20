using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Equipment;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
    [MessagePackObject(true)]
    [Description("武具強化アイテムテーブル")]
    public class EquipmentReinforcementMaterialMB : MasterBookBase
    {
        [PropertyOrder(1)]
        [Description("強化Lv")]
        public long ReinforcementLevel { get; }

        [Nest(false, 0)]
        [PropertyOrder(2)]
        [Description("武器強化アイテムリスト")]
        public IReadOnlyList<UserItem> WeaponRequiredItemList { get; }

        [Nest(false, 0)]
        [PropertyOrder(3)]
        [Description("防具強化アイテムリスト")]
        public IReadOnlyList<UserItem> OthersRequiredItemList { get; }

        [SerializationConstructor]
        public EquipmentReinforcementMaterialMB(long id, bool? isIgnore, string memo, long reinforcementLevel, IReadOnlyList<UserItem> weaponRequiredItemList, IReadOnlyList<UserItem> othersRequiredItemList)
            : base(id, isIgnore, memo)
        {
            this.ReinforcementLevel = reinforcementLevel;
            this.WeaponRequiredItemList = weaponRequiredItemList;
            this.OthersRequiredItemList = othersRequiredItemList;
        }

        public EquipmentReinforcementMaterialMB()
            : base(0L, null, null)
        {
        }

        public IReadOnlyList<UserItem> GetRequiredItemList(EquipmentSlotType slotType)
        {
            return null;
        }
    }
}
