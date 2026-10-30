/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { IUserSyncApiResponse } from "./iUserSyncApiResponse";
import { UserItem } from "./userItem";
import { UserFriendMissionDtoInfo } from "./userFriendMissionDtoInfo";
import { UserSyncData } from "./userSyncData";

export class FriendRewardFriendMissionResponse extends ApiResponseBase implements IUserSyncApiResponse {
    rewardItemList: UserItem[];
    userFriendMissionDtoInfoList: UserFriendMissionDtoInfo[];
    userSyncData: UserSyncData;
}
