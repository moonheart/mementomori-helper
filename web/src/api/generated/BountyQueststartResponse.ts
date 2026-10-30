/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { IUserSyncApiResponse } from "./iUserSyncApiResponse";
import { BountyQuestInfo } from "./bountyQuestInfo";
import { UserBountyQuestDtoInfo } from "./userBountyQuestDtoInfo";
import { UserSyncData } from "./userSyncData";

export class BountyQuestStartResponse extends ApiResponseBase implements IUserSyncApiResponse {
    guerrillaBountyQuestInfos: BountyQuestInfo[];
    userBountyQuestDtoInfos: UserBountyQuestDtoInfo[];
    userSyncData: UserSyncData;
}
