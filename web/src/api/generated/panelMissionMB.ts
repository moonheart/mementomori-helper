/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MasterBookBase } from "./masterBookBase";
import { IHasStartEndTimeZone } from "./iHasStartEndTimeZone";
import { PanelMissionSheetInfo } from "./panelMissionSheetInfo";
import { MypageIconDisplayLocationType } from "./mypageIconDisplayLocationType";
import { StartEndTimeZoneType } from "./startEndTimeZoneType";

export class PanelMissionMB extends MasterBookBase implements IHasStartEndTimeZone {
    campaignTitleKey: string;
    delayDays: number;
    forceStartTime: string;
    panelMissionSheetInfoList: PanelMissionSheetInfo[];
    mypageIconDisplayLocationType: MypageIconDisplayLocationType;
    endTime: string;
    startTime: string;
    startEndTimeZoneType: StartEndTimeZoneType;
}
