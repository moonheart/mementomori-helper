/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiRequestBase } from "./apiRequestBase";
import { ItemType } from "./itemType";

export class ItemOpenTreasureChestRequest extends ApiRequestBase {
    openCount: number;
    treasureChestId: number;
    selectedTreasureItemListIndex: number;
    selectedItemType: ItemType;
    selectedItemId: number;
    selectedCharacterId: number;
}
