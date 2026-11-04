/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MasterBookBase } from "./masterBookBase";
import { IBattleEnemy } from "./iBattleEnemy";
import { BaseParameter } from "./baseParameter";
import { BattleParameter } from "./battleParameter";
import { CharacterRarityFlags } from "./characterRarityFlags";
import { ElementType } from "./elementType";
import { EquipmentRarityFlags } from "./equipmentRarityFlags";
import { JobFlags } from "./jobFlags";
import { UnitIconType } from "./unitIconType";

export class BossBattleEnemyMB extends MasterBookBase implements IBattleEnemy {
    activeSkillIds: number[];
    baseParameter: BaseParameter;
    battleParameter: BattleParameter;
    battlePower: number;
    characterRarityFlags: CharacterRarityFlags;
    elementType: ElementType;
    enemyAdjustId: number;
    battleEnemyCharacterId: number;
    enemyEquipmentId: number;
    exclusiveEquipmentRarityFlags: EquipmentRarityFlags;
    enemyRank: number;
    jobFlags: JobFlags;
    nameKey: string;
    normalSkillId: number;
    passiveSkillIds: number[];
    unitIconId: number;
    unitIconType: UnitIconType;
}
