/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { IUserCharacterItem } from "./iUserCharacterItem";
import { SacredTreasureType } from "./sacredTreasureType";
import { CharacterRarityFlags } from "./characterRarityFlags";
import { UserItem } from "./userItem";

export class TreasureChestReward implements IUserCharacterItem {
    sacredTreasureType: SacredTreasureType;
    rarityFlags: CharacterRarityFlags;
    item: UserItem;
}
