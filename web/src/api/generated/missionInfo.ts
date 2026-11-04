/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { UserMissionActivityDtoInfo } from "./userMissionActivityDtoInfo";
import { MissionType } from "./missionType";
import { UserMissionDtoInfo } from "./userMissionDtoInfo";
import { UserPanelMissionDtoInfo } from "./userPanelMissionDtoInfo";

export class MissionInfo {
    missionExpirationTimeStamp: number;
    userMissionActivityDtoInfo: UserMissionActivityDtoInfo;
    userMissionDtoInfoDict: { [key in MissionType]?: UserMissionDtoInfo[]; };
    userPanelMissionDtoInfoList: UserPanelMissionDtoInfo[];
}
