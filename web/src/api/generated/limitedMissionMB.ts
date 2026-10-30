/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MasterBookBase } from "./masterBookBase";
import { IHasStartEndTime } from "./iHasStartEndTime";
import { MypageIconDisplayLocationType } from "./mypageIconDisplayLocationType";

export class LimitedMissionMB extends MasterBookBase implements IHasStartEndTime {
    startTime: string;
    endTime: string;
    forceStartTime: string;
    delayDays: number;
    characterImageId: number;
    characterImageX: number;
    characterImageY: number;
    characterImageSize: number;
    titleTextKey: string;
    appealTextKey: string;
    targetMissionIdList: number[];
    mypageIconDisplayLocationType: MypageIconDisplayLocationType;
}
