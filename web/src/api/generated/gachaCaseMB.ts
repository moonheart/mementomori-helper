/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MasterBookBase } from "./masterBookBase";
import { IHasJstStartEndTime } from "./iHasJstStartEndTime";
import { ElementType } from "./elementType";
import { GachaCategoryType } from "./gachaCategoryType";
import { GachaDestinyType } from "./gachaDestinyType";
import { GachaGroupType } from "./gachaGroupType";
import { GachaRelicType } from "./gachaRelicType";
import { GachaResetType } from "./gachaResetType";
import { GachaSelectListType } from "./gachaSelectListType";
import { GachaCaseFlags } from "./gachaCaseFlags";

export class GachaCaseMB extends MasterBookBase implements IHasJstStartEndTime {
    displayOrder: number;
    elementType: ElementType;
    gachaCaseUiId: number;
    gachaCategoryType: GachaCategoryType;
    gachaDestinyType: GachaDestinyType;
    gachaGroupType: GachaGroupType;
    gachaRelicType: GachaRelicType;
    gachaResetType: GachaResetType;
    gachaSelectListType: GachaSelectListType;
    gachaCaseFlags: GachaCaseFlags;
    gachaLimitDayFromCreatePlayer: number;
    gachaLimitDay: number;
    gachaOpenDayFromCreatePlayer: number;
    startTimeFixJST: string;
    endTimeFixJST: string;
}
