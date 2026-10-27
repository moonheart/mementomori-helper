/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MasterBookBase } from "./masterBookBase";
import { IHasStartEndTime } from "./iHasStartEndTime";
import { ItemRarityFlags } from "./itemRarityFlags";
import { ItemType } from "./itemType";
import { SecondaryFrameType } from "./secondaryFrameType";

export class ItemMB extends MasterBookBase implements IHasStartEndTime {
    descriptionKey: string;
    displayName: string;
    endTime: string;
    itemId: number;
    itemRarityFlags: ItemRarityFlags;
    itemType: ItemType;
    maxItemCount: number;
    nameKey: string;
    iconId: number;
    secondaryFrameNum: number;
    secondaryFrameType: SecondaryFrameType;
    sortOrder: number;
    startTime: string;
    transferSpotId: number;
}
