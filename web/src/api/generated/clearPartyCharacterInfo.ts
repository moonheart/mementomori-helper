/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { CharacterRarityFlags } from "./characterRarityFlags";
import { UserEquipmentDtoInfo } from "./userEquipmentDtoInfo";
import { BaseParameter } from "./baseParameter";
import { BattleParameter } from "./battleParameter";

export class ClearPartyCharacterInfo {
    characterGuid: string;
    characterId: number;
    rarityFlags: CharacterRarityFlags;
    level: number;
    isLevelLink: boolean;
    userEquipmentDtoInfos: UserEquipmentDtoInfo[];
    baseParameter: BaseParameter;
    battleParameter: BattleParameter;
    battlePower: number;
}
