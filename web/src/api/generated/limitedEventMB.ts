/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MasterBookBase } from "./masterBookBase";
import { IHasStartEndTime } from "./iHasStartEndTime";
import { LimitedEventType } from "./limitedEventType";
import { TargetInfo } from "./targetInfo";

export class LimitedEventMB extends MasterBookBase implements IHasStartEndTime {
    limitedEventType: LimitedEventType;
    targetInfoList: TargetInfo[];
    startTime: string;
    endTime: string;
}
