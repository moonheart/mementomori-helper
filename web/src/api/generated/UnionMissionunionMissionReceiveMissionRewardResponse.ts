/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { IUserSyncApiResponse } from "./iUserSyncApiResponse";
import { UnionMissionPanelInfo } from "./unionMissionPanelInfo";
import { UserSyncData } from "./userSyncData";

export class UnionMissionUnionMissionReceiveMissionRewardResponse extends ApiResponseBase implements IUserSyncApiResponse {
    gainedPoint: number;
    guildPoint: number;
    guildRank: number;
    sharedMissionPanelList: UnionMissionPanelInfo[];
    personalMissionPanelList: UnionMissionPanelInfo[];
    personalAchievedCount: number;
    personalPoint: number;
    personalRank: number;
    userSyncData: UserSyncData;
}
