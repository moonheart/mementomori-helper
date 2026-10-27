/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MasterBookBase } from "./masterBookBase";
import { IHasStartEndTime } from "./iHasStartEndTime";
import { LimitedEventType } from "./limitedEventType";

export class LimitedEventMB extends MasterBookBase implements IHasStartEndTime {
    limitedEventType: LimitedEventType;
    startTime: string;
    endTime: string;
}
