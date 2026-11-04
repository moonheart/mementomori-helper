/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MasterBookBase } from "./masterBookBase";
import { MissionGroupType } from "./missionGroupType";
import { MissionReward } from "./missionReward";
import { MissionClearCountRewardInfo } from "./missionClearCountRewardInfo";

export class MissionClearCountRewardMB extends MasterBookBase {
    missionGroupType: MissionGroupType;
    requiredClearCount: number;
    rewardList: MissionReward[];
    rewardInfoList: MissionClearCountRewardInfo[];
}
