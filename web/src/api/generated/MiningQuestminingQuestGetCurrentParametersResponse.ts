/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { MiningQuestBlockLotteryInfo } from "./miningQuestBlockLotteryInfo";

export class MiningQuestMiningQuestGetCurrentParametersResponse extends ApiResponseBase {
    attackPower: number;
    attackRange: number;
    attackFrequency: number;
    attackSpeedAdditionalRate: number;
    specialAttackRange: number;
    specialAttackCoolTimeMilliseconds: number;
    specialAttackSimultaneousCount: number;
    blockGenerateLimit: number;
    respawnBlockLevel: number;
    limitTimeSeconds: number;
    rewardCountAdditionalRate: number;
    spawnBlockLevel: number;
    blockLotteryInfoList: MiningQuestBlockLotteryInfo[];
}
