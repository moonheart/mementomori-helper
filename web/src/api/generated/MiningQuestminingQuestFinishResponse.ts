/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { IUserSyncApiResponse } from "./iUserSyncApiResponse";
import { MiningQuestMinedBlockInfo } from "./miningQuestMinedBlockInfo";
import { MiningQuestGradeType } from "./miningQuestGradeType";
import { UserItem } from "./userItem";
import { UserSyncData } from "./userSyncData";

export class MiningQuestMiningQuestFinishResponse extends ApiResponseBase implements IUserSyncApiResponse {
    minedBlockInfoList: MiningQuestMinedBlockInfo[];
    score: number;
    gradeType: MiningQuestGradeType;
    gradeNameTextKey: string;
    rewardItem: UserItem;
    totalRewardCount: number;
    maxRewardCount: number;
    isDisplayMissionBadge: boolean;
    isDisplayGradeBadge: boolean;
    isDisplayMaxRewardCountBadge: boolean;
    userSyncData: UserSyncData;
}
