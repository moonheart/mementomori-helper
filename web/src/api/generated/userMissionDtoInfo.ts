/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MissionAchievementType } from "./missionAchievementType";
import { MissionStatusType } from "./missionStatusType";
import { MissionType } from "./missionType";

export class UserMissionDtoInfo {
    achievementType: MissionAchievementType;
    missionStatusHistory: { [key in MissionStatusType]?: number[]; };
    missionType: MissionType;
    playerId: number;
    progressCount: number;
}
