/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MasterBookBase } from "./masterBookBase";
import { IHasStartEndTimeZone } from "./iHasStartEndTimeZone";
import { StartEndTimeZoneType } from "./startEndTimeZoneType";
import { MypageIconDisplayLocationType } from "./mypageIconDisplayLocationType";
import { SweepstakesItem } from "./sweepstakesItem";

export class CollabMissionMB extends MasterBookBase implements IHasStartEndTimeZone {
    startEndTimeZoneType: StartEndTimeZoneType;
    startTime: string;
    endTime: string;
    imageId: number;
    imageX: number;
    imageY: number;
    imageSize: number;
    descriptionTextKey: string;
    sweepstakesDescriptionTextKey: string;
    titleTextKey: string;
    sweepstakesTitleTextKey: string;
    termsTextKey: string;
    activityMedalType: number;
    targetMissionIdList: number[];
    url1: string;
    url2: string;
    characterId: number;
    mypageIconDisplayLocationType: MypageIconDisplayLocationType;
    sweepstakesTargetTimeServerIdList: number[];
    sweepstakesTicketItemId: number;
    sweepstakesEntryUpperLimit: number;
    sweepstakesItemList: SweepstakesItem[];
    noticeGroupId: number;
}
