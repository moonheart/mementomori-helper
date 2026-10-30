/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MissionGroupType } from "./missionGroupType";
import { MissionActivityRewardStatusType } from "./missionActivityRewardStatusType";

export class UserMissionActivityDtoInfo {
    missionGroupType: MissionGroupType;
    playerId: number;
    progressCount: number;
    rewardStatusDict: { [key: number]: MissionActivityRewardStatusType; };
}
