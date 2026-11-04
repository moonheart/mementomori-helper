/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MasterBookBase } from "./masterBookBase";
import { IHasStartEndTime } from "./iHasStartEndTime";
import { ItemRarityFlags } from "./itemRarityFlags";
import { SecondaryFrameType } from "./secondaryFrameType";
import { TreasureChestLotteryType } from "./treasureChestLotteryType";

export class TreasureChestMB extends MasterBookBase implements IHasStartEndTime {
    bulkUseEnabled: boolean;
    chestKeyItemId: number;
    descriptionKey: string;
    displayNameKey: string;
    iconId: number;
    itemRarityFlags: ItemRarityFlags;
    maxItemCount: number;
    minOpenCount: number;
    nameKey: string;
    secondaryFrameNum: number;
    secondaryFrameType: SecondaryFrameType;
    sortOrder: number;
    treasureChestItemIdList: number[];
    treasureChestLotteryType: TreasureChestLotteryType;
    endTime: string;
    startTime: string;
}
