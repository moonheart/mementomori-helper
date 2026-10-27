/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { IUserCharacterItem } from "./iUserCharacterItem";
import { IDeepCopy } from "./iDeepCopy";
import { UserItem } from "./userItem";
import { CharacterRarityFlags } from "./characterRarityFlags";

export class PresentItem implements IUserCharacterItem, IDeepCopy<PresentItem> {
    item: UserItem;
    rarityFlags: CharacterRarityFlags;
}
