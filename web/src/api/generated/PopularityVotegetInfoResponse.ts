/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { IUserSyncApiResponse } from "./iUserSyncApiResponse";
import { PopularityVotePopularityVoteRewardInfo } from "./PopularityVotepopularityVoteRewardInfo";
import { UserSyncData } from "./userSyncData";

export class PopularityVoteGetInfoResponse extends ApiResponseBase implements IUserSyncApiResponse {
    popularityVoteRewardInfoList: PopularityVotePopularityVoteRewardInfo[];
    totalVoteCount: number;
    userSyncData: UserSyncData;
}
