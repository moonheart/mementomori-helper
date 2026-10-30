/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { CharacterRarityFlags } from "./characterRarityFlags";
import { UserEquipmentDtoInfo } from "./userEquipmentDtoInfo";
import { UnitType } from "./unitType";
import { JobFlags } from "./jobFlags";
import { ElementType } from "./elementType";
import { BaseParameter } from "./baseParameter";
import { BattleParameter } from "./battleParameter";
import { BattlePosition } from "./battlePosition";
import { BattleActiveSkill } from "./battleActiveSkill";
import { BattlePassiveSkill } from "./battlePassiveSkill";
import { DungeonBattleInfo } from "./dungeonBattleInfo";

export class BattleFieldCharacter {
    playerName: string;
    characterGuid: string;
    characterLevel: number;
    characterRarityFlags: CharacterRarityFlags;
    equipmentMaxLevel: number;
    equipmentDtoInfos: UserEquipmentDtoInfo[];
    unitType: UnitType;
    unitId: number;
    jobFlags: JobFlags;
    elementType: ElementType;
    defaultBaseParameter: BaseParameter;
    defaultBattleParameter: BattleParameter;
    battleParameterWithoutBonus: BattleParameter;
    onStartHP: number;
    defaultPosition: BattlePosition;
    guid: number;
    normalSkill: BattleActiveSkill;
    activeSkills: BattleActiveSkill[];
    passiveSkills: BattlePassiveSkill[];
    ownerPlayerId: number;
    playerRankHitBonus: number;
    dungeonBattleInfo: DungeonBattleInfo;
    rentalRaidMaxHpRate: number;
}
