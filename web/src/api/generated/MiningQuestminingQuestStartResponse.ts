/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { MiningQuestParameterInfo } from "./miningQuestParameterInfo";
import { MiningQuestBlockInfo } from "./miningQuestBlockInfo";
import { MiningQuestBlockRespawnInfo } from "./miningQuestBlockRespawnInfo";
import { MiningQuestSpecialAttackInfo } from "./miningQuestSpecialAttackInfo";

export class MiningQuestMiningQuestStartResponse extends ApiResponseBase {
    limitTimeSeconds: number;
    parameterInfo: MiningQuestParameterInfo;
    blockInfoList: MiningQuestBlockInfo[];
    blockRespawnInfoList: MiningQuestBlockRespawnInfo[];
    specialAttackInfoList: MiningQuestSpecialAttackInfo[];
    layerBlockSizePercentList: number[];
    layerBlockCapacityWeightList: number[];
    seed: number;
}
