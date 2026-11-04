/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MasterBookBase } from "./masterBookBase";
import { IHasStartEndTime } from "./iHasStartEndTime";
import { MissionAchievementType } from "./missionAchievementType";
import { MissionType } from "./missionType";
import { MissionReward } from "./missionReward";
import { MissionTransitionDestinationType } from "./missionTransitionDestinationType";

export class MissionMB extends MasterBookBase implements IHasStartEndTime {
    achievementType: MissionAchievementType;
    descriptionKey: string;
    missionType: MissionType;
    nameKey: string;
    openingPeriod: number;
    requireValue: number;
    rewardList: MissionReward[];
    sortOrderA: number;
    sortOrderB: number;
    transitionDestination: MissionTransitionDestinationType;
    endTime: string;
    startTime: string;
}
