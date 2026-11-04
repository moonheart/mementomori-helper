/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MasterBookBase } from "./masterBookBase";
import { IHasStartEndTime } from "./iHasStartEndTime";
import { LocalRaidStartEndTime } from "./localRaidStartEndTime";

export class LocalRaidEventScheduleMB extends MasterBookBase implements IHasStartEndTime {
    localRaidStartEndTimes: LocalRaidStartEndTime[];
    rewardBonusRate: number;
    startTime: string;
    endTime: string;
    localRaidEventTutorialId: number;
    eventBattleCount: number;
}
