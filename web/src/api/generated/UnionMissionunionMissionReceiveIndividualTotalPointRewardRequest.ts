/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiRequestBase } from "./apiRequestBase";
import { UnionMissionTotalPointRewardType } from "./unionMissionTotalPointRewardType";

export class UnionMissionUnionMissionReceiveIndividualTotalPointRewardRequest extends ApiRequestBase {
    rewardMBId: number;
    rewardType: UnionMissionTotalPointRewardType;
    isRepeatPersonalReward: boolean;
}
