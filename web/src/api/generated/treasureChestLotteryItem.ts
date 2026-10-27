/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ILotteryWeight } from "./iLotteryWeight";
import { UserItem } from "./userItem";
import { SacredTreasureType } from "./sacredTreasureType";

export class TreasureChestLotteryItem implements ILotteryWeight {
    isCeilingTarget: boolean;
    item: UserItem;
    sacredTreasureType: SacredTreasureType;
    lotteryWeight: number;
}
