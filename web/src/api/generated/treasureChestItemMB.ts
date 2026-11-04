/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MasterBookBase } from "./masterBookBase";
import { TreasureChestFixItem } from "./treasureChestFixItem";
import { TreasureChestSelectItem } from "./treasureChestSelectItem";
import { TreasureChestItemListType } from "./treasureChestItemListType";

export class TreasureChestItemMB extends MasterBookBase {
    fixItemList: TreasureChestFixItem[];
    lotteryRewardId: number;
    selectItemList: TreasureChestSelectItem[];
    treasureChestItemListType: TreasureChestItemListType;
}
