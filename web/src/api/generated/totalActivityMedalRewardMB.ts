/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MasterBookBase } from "./masterBookBase";
import { IHasStartEndTime } from "./iHasStartEndTime";
import { MissionGroupType } from "./missionGroupType";
import { MissionReward } from "./missionReward";

export class TotalActivityMedalRewardMB extends MasterBookBase implements IHasStartEndTime {
    missionGroupType: MissionGroupType;
    value: number;
    requiredActivityMedalCount: number;
    rewardList: MissionReward[];
    startTime: string;
    endTime: string;
    sortOrder: number;
    timeServerIdList: number[];
    imageId: number;
}
