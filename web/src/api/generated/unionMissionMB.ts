/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MasterBookBase } from "./masterBookBase";
import { IHasStartEndTime } from "./iHasStartEndTime";
import { MissionAchievementType } from "./missionAchievementType";

export class UnionMissionMB extends MasterBookBase implements IHasStartEndTime {
    descriptionKey: string;
    difficulty: number;
    achievementType: MissionAchievementType;
    requireValue: number;
    getPoint: number;
    limitDays: number;
    isSelectableByTicket: boolean;
    startTime: string;
    endTime: string;
}
