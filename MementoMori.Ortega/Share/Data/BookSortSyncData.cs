using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data
{
    [MessagePackObject(true)]
    public class BookSortSyncData
    {
        public int CurrentFloor { get; set; }

        public List<UnlockedGridCellInfo> UnlockedGridCellInfoList { get; set; }

        public bool? IsExistReceivableMission { get; set; }

        public int SelectedBonusFloorRewardIndex { get; set; }

        public List<BookSortFloorHistory> FloorHistoryList { get; set; }

        public int? LastUnlockGridCellDateIntYearMonthDay { get; set; }

        public bool IsUnlockedWinGridCell()
        {
            if (this.UnlockedGridCellInfoList != null)
            {
                foreach (var info in this.UnlockedGridCellInfoList)
                {
                    if (info.IsWin)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool IsUnlockedKeyGridCell()
        {
            if (this.UnlockedGridCellInfoList != null)
            {
                foreach (var info in this.UnlockedGridCellInfoList)
                {
                    if (info.IsKey)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool IsUnlockedWinAndKeyGridCell()
        {
            return this.IsUnlockedWinGridCell() && this.IsUnlockedKeyGridCell();
        }

        public bool IsAcquiredReward(float floorCount)
        {
            return (float) this.CurrentFloor > floorCount || (floorCount <= (float) this.CurrentFloor && this.IsUnlockedWinGridCell() && this.IsUnlockedKeyGridCell());
        }

        public int GetLockedGridCellCount()
        {
            return OrtegaConst.BookSort.MaxGridCellCount - (this.UnlockedGridCellInfoList != null ? this.UnlockedGridCellInfoList.Count : 0);
        }

        public void Merge(BookSortSyncData newBookSortSyncData)
        {
            if (newBookSortSyncData == null)
            {
                return;
            }
            this.CurrentFloor = newBookSortSyncData.CurrentFloor;
            this.SelectedBonusFloorRewardIndex = newBookSortSyncData.SelectedBonusFloorRewardIndex;
            if (newBookSortSyncData.UnlockedGridCellInfoList != null)
            {
                this.UnlockedGridCellInfoList = newBookSortSyncData.UnlockedGridCellInfoList;
            }
            if (newBookSortSyncData.FloorHistoryList != null)
            {
                this.FloorHistoryList = newBookSortSyncData.FloorHistoryList;
            }
            if (newBookSortSyncData.IsExistReceivableMission.HasValue)
            {
                this.IsExistReceivableMission = newBookSortSyncData.IsExistReceivableMission;
            }
            if (newBookSortSyncData.LastUnlockGridCellDateIntYearMonthDay.HasValue)
            {
                this.LastUnlockGridCellDateIntYearMonthDay = newBookSortSyncData.LastUnlockGridCellDateIntYearMonthDay;
            }
        }
    }
}
