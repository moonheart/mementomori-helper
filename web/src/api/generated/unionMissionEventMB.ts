/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MasterBookBase } from "./masterBookBase";
import { IHasStartEndTime } from "./iHasStartEndTime";

export class UnionMissionEventMB extends MasterBookBase implements IHasStartEndTime {
    missionLotteryIdList: number[];
    readyStartTime: string;
    startTime: string;
    endTime: string;
    displayBannerEndTime: string;
}
