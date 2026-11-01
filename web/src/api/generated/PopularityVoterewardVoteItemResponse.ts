/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { IUserSyncApiResponse } from "./iUserSyncApiResponse";
import { PopularityVotePopularityVoteRewardInfo } from "./PopularityVotepopularityVoteRewardInfo";
import { UserItem } from "./userItem";
import { UserSyncData } from "./userSyncData";

export class PopularityVoteRewardVoteItemResponse extends ApiResponseBase implements IUserSyncApiResponse {
    popularityVoteRewardInfoList: PopularityVotePopularityVoteRewardInfo[];
    rewardItemList: UserItem[];
    userSyncData: UserSyncData;
}
