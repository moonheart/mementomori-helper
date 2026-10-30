/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MasterBookBase } from "./masterBookBase";
import { IBattleEnemy } from "./iBattleEnemy";
import { BaseParameter } from "./baseParameter";
import { UnitIconType } from "./unitIconType";
import { BattleParameter } from "./battleParameter";
import { JobFlags } from "./jobFlags";
import { ElementType } from "./elementType";
import { CharacterRarityFlags } from "./characterRarityFlags";
import { EquipmentRarityFlags } from "./equipmentRarityFlags";

export class LocalRaidEnemyMB extends MasterBookBase implements IBattleEnemy {
    activeSkillIds: number[];
    baseParameter: BaseParameter;
    battleEnemyCharacterId: number;
    unitIconType: UnitIconType;
    unitIconId: number;
    battleParameter: BattleParameter;
    battlePower: number;
    jobFlags: JobFlags;
    elementType: ElementType;
    characterRarityFlags: CharacterRarityFlags;
    enemyRank: number;
    nameKey: string;
    normalSkillId: number;
    passiveSkillIds: number[];
    enemyAdjustId: number;
    enemyEquipmentId: number;
    exclusiveEquipmentRarityFlags: EquipmentRarityFlags;
}
