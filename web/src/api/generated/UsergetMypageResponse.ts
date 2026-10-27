/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { IUserSyncApiResponse } from "./iUserSyncApiResponse";
import { MissionGuideInfo } from "./missionGuideInfo";
import { DisplayMypageInfo } from "./displayMypageInfo";
import { UserFriendDtoInfo } from "./userFriendDtoInfo";
import { UserSyncData } from "./userSyncData";
import { WorldGuidanceInfo } from "./worldGuidanceInfo";

export class UserGetMypageResponse extends ApiResponseBase implements IUserSyncApiResponse {
    displayNoticeIdList: number[];
    existNewFriendPointTransfer: boolean;
    existNewPrivateChat: boolean;
    existNotReceivedBountyQuestReward: boolean;
    existNotReceivedMissionReward: boolean;
    latestAnnounceChatRegistrationLocalTimestamp: number;
    missionGuideInfo: MissionGuideInfo;
    mypageInfo: DisplayMypageInfo;
    notOrderedBountyQuestIdList: number[];
    receivableLimitedLoginBonusId: number;
    unreadIndividualNotificationIdList: number[];
    userFriendDtoInfoList: UserFriendDtoInfo[];
    userSyncData: UserSyncData;
    worldGuidanceInfo: WorldGuidanceInfo;
}
