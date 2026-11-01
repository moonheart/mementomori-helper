/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { MiningQuestGradeType } from "./miningQuestGradeType";
import { UserMiningQuestReinforcementInfo } from "./userMiningQuestReinforcementInfo";

export class MiningQuestMiningQuestGetInfoResponse extends ApiResponseBase {
    miningQuestEventId: number;
    maxScore: number;
    gradeType: MiningQuestGradeType;
    gradeNameTextKey: string;
    endLocalTimeStamp: number;
    isDisplayMissionBadge: boolean;
    isDisplayGradeBadge: boolean;
    isDisplayBenefitBadge: boolean;
    isDisplayMaxRewardCountBadge: boolean;
    currentAttackPower: number;
    currentAttackFrequency: number;
    currentCollectionLevel: number;
    userReinforcementInfoList: UserMiningQuestReinforcementInfo[];
}
