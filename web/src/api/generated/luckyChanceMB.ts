/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MasterBookBase } from "./masterBookBase";
import { IHasStartEndTimeZone } from "./iHasStartEndTimeZone";
import { StartEndTimeZoneType } from "./startEndTimeZoneType";
import { MypageIconDisplayLocationType } from "./mypageIconDisplayLocationType";
import { UserItem } from "./userItem";

export class LuckyChanceMB extends MasterBookBase implements IHasStartEndTimeZone {
    startEndTimeZoneType: StartEndTimeZoneType;
    startTime: string;
    endTime: string;
    inputFormEndTime: string;
    mypageIconDisplayLocationType: MypageIconDisplayLocationType;
    titleTextKey: string;
    limitUserDrawCount: number;
    consumeItem: UserItem;
    canDeletePersonalInfoTime: string;
}
