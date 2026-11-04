/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { IUserSyncApiResponse } from "./iUserSyncApiResponse";
import { AcquisitionMissionRewardInfo } from "./acquisitionMissionRewardInfo";
import { UserSyncData } from "./userSyncData";

export class MissionReceivePanelMissionBingoRewardResponse extends ApiResponseBase implements IUserSyncApiResponse {
    rewardInfo: AcquisitionMissionRewardInfo;
    userSyncData: UserSyncData;
}
