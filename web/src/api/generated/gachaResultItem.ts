/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { IUserItem } from "./iUserItem";
import { CharacterRarityFlags } from "./characterRarityFlags";
import { ItemType } from "./itemType";

export class GachaResultItem implements IUserItem {
    characterRarityFlags: CharacterRarityFlags;
    gachaLotteryId: number;
    gachaLotteryItemListId: number;
    guid: string;
    isCeilingItem: boolean;
    itemCount: number;
    itemId: number;
    itemType: ItemType;
    isSentPresentBox: boolean;
}
