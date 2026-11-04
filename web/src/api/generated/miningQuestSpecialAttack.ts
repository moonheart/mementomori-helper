/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MiningQuestSpecialAttackType } from "./miningQuestSpecialAttackType";
import { MiningQuestSpecialAttackRange } from "./miningQuestSpecialAttackRange";

export class MiningQuestSpecialAttack {
    specialAttackId: number;
    type: MiningQuestSpecialAttackType;
    initialCoolTimeMilliseconds: number;
    baseCoolTimeMilliseconds: number;
    attackPowerRate: number;
    attackRangeList: MiningQuestSpecialAttackRange[];
    rangeExpansionIntervalMilliseconds: number;
    baseSimultaneousCount: number;
}
