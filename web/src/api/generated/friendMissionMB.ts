/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MasterBookBase } from "./masterBookBase";
import { IHasStartEndTime } from "./iHasStartEndTime";
import { MissionTransitionDestinationType } from "./missionTransitionDestinationType";
import { MissionAchievementType } from "./missionAchievementType";
import { UserItem } from "./userItem";

export class FriendMissionMB extends MasterBookBase implements IHasStartEndTime {
    nameKey: string;
    transitionDestination: MissionTransitionDestinationType;
    achievementType: MissionAchievementType;
    requireValue: number;
    sortOrder: number;
    rewardList: UserItem[];
    endTime: string;
    startTime: string;
}
