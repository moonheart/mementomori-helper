/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { BaseParameter } from "./baseParameter";
import { BattleParameter } from "./battleParameter";
import { UserEquipmentDtoInfo } from "./userEquipmentDtoInfo";
import { CharacterRarityFlags } from "./characterRarityFlags";

export class UserDungeonBattleGuestCharacterDtoInfo {
    baseParameter: BaseParameter;
    battleParameter: BattleParameter;
    battlePower: number;
    characterId: number;
    guestEquipmentDtoInfos: UserEquipmentDtoInfo[];
    guid: string;
    level: number;
    playerId: number;
    rarityFlags: CharacterRarityFlags;
}
