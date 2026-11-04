/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MissionGroupType } from "./missionGroupType";
import { MissionInfo } from "./missionInfo";

export class ApiGetMissionInfoResponse {
    success: boolean;
    message: string = "";
    missionInfoDict: { [key in MissionGroupType]?: MissionInfo; };
}
