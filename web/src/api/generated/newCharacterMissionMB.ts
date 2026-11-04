/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MasterBookBase } from "./masterBookBase";
import { IHasJstStartEndTime } from "./iHasJstStartEndTime";
import { MypageIconDisplayLocationType } from "./mypageIconDisplayLocationType";

export class NewCharacterMissionMB extends MasterBookBase implements IHasJstStartEndTime {
    startTimeFixJST: string;
    endTimeFixJST: string;
    forceStartTime: string;
    characterImageId: number;
    characterImageX: number;
    characterImageY: number;
    characterImageSize: number;
    titleTextKey: string;
    targetMissionIdList: number[];
    youTubeUrl: string;
    twitterUrl: string;
    mypageIconDisplayLocationType: MypageIconDisplayLocationType;
}
