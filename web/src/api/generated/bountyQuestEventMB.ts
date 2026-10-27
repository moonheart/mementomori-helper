/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MasterBookBase } from "./masterBookBase";
import { IHasStartEndTime } from "./iHasStartEndTime";
import { BountyQuestEventTargetItemInfo } from "./bountyQuestEventTargetItemInfo";
import { BountyQuestEventTargetQuestTypeInfo } from "./bountyQuestEventTargetQuestTypeInfo";

export class BountyQuestEventMB extends MasterBookBase implements IHasStartEndTime {
    eventDescriptionKey: string;
    eventNameKey: string;
    multipleNumber: number;
    targetItemList: BountyQuestEventTargetItemInfo[];
    targetQuestTypeList: BountyQuestEventTargetQuestTypeInfo[];
    endTime: string;
    startTime: string;
}
