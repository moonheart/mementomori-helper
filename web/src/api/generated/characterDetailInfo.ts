/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { UserEquipmentDtoInfo } from "./userEquipmentDtoInfo";
import { BaseParameter } from "./baseParameter";
import { BattleParameter } from "./battleParameter";
import { CharacterRarityFlags } from "./characterRarityFlags";

export class CharacterDetailInfo {
    userEquipmentDtoInfos: UserEquipmentDtoInfo[];
    baseParameter: BaseParameter;
    battleParameter: BattleParameter;
    battlePower: number;
    level: number;
    rarityFlags: CharacterRarityFlags;
}
