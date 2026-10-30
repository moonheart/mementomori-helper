/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MasterBookBase } from "./masterBookBase";
import { CharacterRarityFlags } from "./characterRarityFlags";
import { UserItem } from "./userItem";

export class CharacterStoryMB extends MasterBookBase {
    characterId: number;
    episodeId: number;
    level: number;
    rarityFlags: CharacterRarityFlags;
    rewardItemList: UserItem[];
    textKey: string;
    titleKey: string;
}
