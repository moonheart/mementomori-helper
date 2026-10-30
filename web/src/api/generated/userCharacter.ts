/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { IUserItem } from "./iUserItem";
import { CharacterRarityFlags } from "./characterRarityFlags";
import { ItemType } from "./itemType";

export class UserCharacter implements IUserItem {
    characterId: number;
    characterRarityFlags: CharacterRarityFlags;
    guid: string;
    itemCount: number;
    itemId: number;
    itemType: ItemType;
}
