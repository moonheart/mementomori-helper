/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MasterBookBase } from "./masterBookBase";
import { IHasStartEndTime } from "./iHasStartEndTime";
import { StartEndTime } from "./startEndTime";

export class WorldGroupMB extends MasterBookBase implements IHasStartEndTime {
    endTime: string;
    endLegendLeagueDateTime: string;
    timeServerId: number;
    startTime: string;
    grandBattleDateTimeList: StartEndTime[];
    startLegendLeagueDateTime: string;
    worldIdList: number[];
    monthlyOpenCount: number;
}
