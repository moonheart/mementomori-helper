/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MasterBookBase } from "./masterBookBase";
import { IHasStartEndTime } from "./iHasStartEndTime";
import { PatternSettingType } from "./patternSettingType";

export class PatternSettingMB extends MasterBookBase implements IHasStartEndTime {
    elapsedFromCreatePlayerDayConditionList: number[];
    numberInfo: number;
    patternSettingType: PatternSettingType;
    playerIdRemainConditionList: number[];
    progressQuestIdConditionList: number[];
    timeServerIdConditionList: number[];
    endTime: string;
    startTime: string;
}
