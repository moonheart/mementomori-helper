/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { IUserSyncApiResponse } from "./iUserSyncApiResponse";
import { UnionMissionClassType } from "./unionMissionClassType";
import { UnionMissionPanelInfo } from "./unionMissionPanelInfo";
import { UserSyncData } from "./userSyncData";

export class UnionMissionUnionMissionGetInfoResponse extends ApiResponseBase implements IUserSyncApiResponse {
    classType: UnionMissionClassType;
    prevClassType: UnionMissionClassType;
    existReceivableTotalPointReward: boolean;
    guildPoint: number;
    guildRank: number;
    blockGuildCount: number;
    sharedMissionPanelList: UnionMissionPanelInfo[];
    personalMissionPanelList: UnionMissionPanelInfo[];
    personalAchievedCount: number;
    personalPoint: number;
    personalRank: number;
    cumulativePersonalPoint: number;
    receivedGuildPointRewardThreshold: number;
    receivedPersonalPointRewardThreshold: number;
    remainingAcceptCount: number;
    buyAcceptCountToday: number;
    isOpen: boolean;
    userSyncData: UserSyncData;
}
