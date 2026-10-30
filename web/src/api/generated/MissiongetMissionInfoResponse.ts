/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { MissionGroupType } from "./missionGroupType";
import { MissionInfo } from "./missionInfo";

export class MissionGetMissionInfoResponse extends ApiResponseBase {
    isOpenCurrencyMission: boolean;
    missionInfoDict: { [key in MissionGroupType]?: MissionInfo; };
}
