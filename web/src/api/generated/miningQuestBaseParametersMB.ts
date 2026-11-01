/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MasterBookBase } from "./masterBookBase";
import { MiningQuestBlockRespawn } from "./miningQuestBlockRespawn";
import { MiningQuestSpecialAttack } from "./miningQuestSpecialAttack";
import { MiningQuestBlock } from "./miningQuestBlock";

export class MiningQuestBaseParametersMB extends MasterBookBase {
    baseLimitTimeSeconds: number;
    baseBlockGenerateLimit: number;
    baseAttackPower: number;
    baseAttackRange: number;
    baseAttackFrequency: number;
    baseAttackSpeed: number;
    contactBlockAttackSpeedRatePercent: number;
    respawnBlockIntervalMilliseconds: number;
    blockRespawnList: MiningQuestBlockRespawn[];
    specialAttackList: MiningQuestSpecialAttack[];
    blockList: MiningQuestBlock[];
    virtualStickSizeList: number[];
    layerBlockSizePercentList: number[];
    layerBlockCapacityWeightList: number[];
    baseFixedRewardCount: number;
}
