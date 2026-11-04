/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ICharacterInfo } from "./iCharacterInfo";
import { CharacterRarityFlags } from "./characterRarityFlags";

export class UserCharacterDtoInfo implements ICharacterInfo {
    guid: string;
    playerId: number;
    characterId: number;
    level: number;
    exp: number;
    rarityFlags: CharacterRarityFlags;
    isLocked: boolean;
}
