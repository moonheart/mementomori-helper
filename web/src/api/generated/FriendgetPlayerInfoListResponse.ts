/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { IUserSyncApiResponse } from "./iUserSyncApiResponse";
import { NewFriendInfo } from "./newFriendInfo";
import { PlayerInfo } from "./playerInfo";
import { UserSyncData } from "./userSyncData";

export class FriendGetPlayerInfoListResponse extends ApiResponseBase implements IUserSyncApiResponse {
    alreadyReceiveFriendPointPlayerIdList: number[];
    canReceiveFriendPointPlayerIdList: number[];
    canSendFriendPointPlayerIdList: number[];
    currentTypePlayerNum: number;
    friendNum: number;
    newFriendInfoList: NewFriendInfo[];
    playerInfoList: PlayerInfo[];
    receivedFriendPointCount: number;
    friendBattleDefenseDeckCommentMap: { [key: number]: string; };
    userSyncData: UserSyncData;
}
