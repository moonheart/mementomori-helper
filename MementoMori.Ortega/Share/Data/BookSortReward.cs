using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data
{
    [MessagePackObject(true)]
    public class BookSortReward
    {
        public BookSortRewardType BookSortRewardType { get; set; }

        public double DropRate { get; set; }

        public List<BookSortRewardItemsRate> BookSortRewardItemsRateList { get; set; }

        public double GetTotalLotteryWeight()
        {
            double weight = 0.0;
            if (this.BookSortRewardItemsRateList != null)
            {
                foreach (var item in this.BookSortRewardItemsRateList)
                {
                    weight += item.LotteryRate;
                }
            }
            return weight;
        }
    }
}
