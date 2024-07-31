using MementoMori.Ortega.Custom;
using MementoMori.Ortega.Share.Data.ApiInterface.Item;
using MementoMori.Ortega.Share.Master.Data;

namespace MementoMori.Funcs;

public partial class MementoMoriFuncs
{
    public Dictionary<GameConfig.AutoUseItemType, long[]> AutoUseItemDict { get; set; } = new()
    {
        [GameConfig.AutoUseItemType.DiamondBag] = new long[] {1, 2, 3},
        [GameConfig.AutoUseItemType.MysteryRune] = new long[] {4, 5, 6, 7, 8, 9, 10},
        [GameConfig.AutoUseItemType.WitchLetterR] = new long[] {17, 18, 19, 20},
        [GameConfig.AutoUseItemType.WitchLetterSr] = new long[] {21, 22, 23, 24},
        [GameConfig.AutoUseItemType.WitchShard] = new long[] {27, 28},
        [GameConfig.AutoUseItemType.Pot] = new long[] {29, 30, 31},
        [GameConfig.AutoUseItemType.Amphora] = new long[] {32, 33, 34},
        [GameConfig.AutoUseItemType.SealedChest] = new long[] {35, 36, 37},
        [GameConfig.AutoUseItemType.WitchLetterGift] = new long[] {85, 86, 87, 88},
        [GameConfig.AutoUseItemType.ChestOfChance] = new long[] {89}
    };

    public async Task AutoUseItems()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            bool successOpen;
            do
            {
                successOpen = false;
                await UserGetUserData();
                foreach (var userItemDtoInfo in UserSyncData.UserItemDtoInfo.Where(d => d.ItemCount > 0 && d.ItemType == ItemType.TreasureChest).ToList())
                {
                    var treasureChestMb = TreasureChestTable.GetById(userItemDtoInfo.ItemId);
                    if (treasureChestMb.TreasureChestLotteryType == TreasureChestLotteryType.Fix ||
                        treasureChestMb.TreasureChestLotteryType == TreasureChestLotteryType.Random)
                    {
                        var type = AutoUseItemDict.FirstOrDefault(d => d.Value.Contains(userItemDtoInfo.ItemId)).Key;
                        if (!GameConfig.Items.AutoUseItemTypes.Contains(type)) continue;

                        if (userItemDtoInfo.ItemCount >= treasureChestMb.MinOpenCount)
                        {
                            if (treasureChestMb.ChestKeyItemId > 0)
                            {
                                var keyCount = UserSyncData.GetUserItemCount(ItemType.TreasureChestKey, treasureChestMb.ChestKeyItemId);
                                var available = Math.Min(userItemDtoInfo.ItemCount, keyCount);
                                if (available > 0)
                                {
                                    var openCount = available / treasureChestMb.MinOpenCount * treasureChestMb.MinOpenCount;
                                    await OpenTreasure(openCount, treasureChestMb, log);
                                    successOpen = true;
                                }
                            }
                            else
                            {
                                var openCount = userItemDtoInfo.ItemCount / treasureChestMb.MinOpenCount * treasureChestMb.MinOpenCount;
                                await OpenTreasure(openCount, treasureChestMb, log);
                                successOpen = true;
                            }
                        }
                    }
                }
            } while (successOpen);

            log(TextResourceTable.Get("[CharacterResetErrorMessageNotEnoughDiamond]"));
        });

        async Task OpenTreasure(long openCount, TreasureChestMB treasureChestMb, Action<string> log)
        {
            var response = await GetResponse<OpenTreasureChestRequest, OpenTreasureChestResponse>(new OpenTreasureChestRequest
            {
                OpenCount = (int) openCount,
                TreasureChestId = treasureChestMb.Id
            });
            log($"{TextResourceTable.Get("[CommonOpenLabel]")} {TextResourceTable.Get(treasureChestMb.NameKey)} x {openCount}");
            response.RewardItems.PrintUserItems(log);
        }
    }
}