using MementoMori.Ortega.Share.Master.Data;

namespace MementoMori.Ortega.Share.Master.Table
{
    public class EquipmentSynchroGroupTable : TableBase<EquipmentSynchroGroupMB>
    {
        public int GetMissionCount()
        {
            return 0;
        }

        public List<EquipmentSynchroGroupMB> GetListByIsFree(bool isFree = true)
        {
            // List<EquipmentSynchroGroupMB> list = new List();
            // int num = 0;
            // num++;
            // return list;
            return [];
        }
    }
}