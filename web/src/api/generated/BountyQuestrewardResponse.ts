/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { IUserSyncApiResponse } from "./iUserSyncApiResponse";
import { BountyQuestInfo } from "./bountyQuestInfo";
import { UserItem } from "./userItem";
import { UserBountyQuestDtoInfo } from "./userBountyQuestDtoInfo";
import { UserSyncData } from "./userSyncData";

export class BountyQuestRewardResponse extends ApiResponseBase implements IUserSyncApiResponse {
    bountyQuestInfos: BountyQuestInfo[];
    rewardItems: UserItem[];
    userBoardRank: number;
    userBountyQuestDtoInfos: UserBountyQuestDtoInfo[];
    userSyncData: UserSyncData;
}
