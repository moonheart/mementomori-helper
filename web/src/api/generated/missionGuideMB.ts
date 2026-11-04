/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MasterBookBase } from "./masterBookBase";
import { IHasStartEndTime } from "./iHasStartEndTime";

export class MissionGuideMB extends MasterBookBase implements IHasStartEndTime {
    guideId: number;
    missionId: number;
    sortOrderA: number;
    sortOrderB: number;
    endTime: string;
    startTime: string;
}
