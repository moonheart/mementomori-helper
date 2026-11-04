/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { IDeepCopy } from "./iDeepCopy";
import { CharacterRarityFlags } from "./characterRarityFlags";

export class UserCharacterInfo implements IDeepCopy<UserCharacterInfo> {
    guid: string;
    playerId: number;
    characterId: number;
    level: number;
    subLevel: number;
    exp: number;
    rarityFlags: CharacterRarityFlags;
    isLocked: boolean;
}
