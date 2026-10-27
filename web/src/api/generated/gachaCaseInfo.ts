/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ElementType } from "./elementType";
import { GachaBonusInfo } from "./gachaBonusInfo";
import { GachaButtonInfo } from "./gachaButtonInfo";
import { GachaCategoryType } from "./gachaCategoryType";
import { GachaGroupType } from "./gachaGroupType";
import { GachaRelicType } from "./gachaRelicType";
import { GachaSelectListType } from "./gachaSelectListType";
import { GachaCaseFlags } from "./gachaCaseFlags";

export class GachaCaseInfo {
    displayOrder: number;
    elementType: ElementType;
    drawStartTime: number;
    endTime: number;
    gachaBonusDrawCount: number;
    gachaBonusInfoList: GachaBonusInfo[];
    gachaButtonInfoList: GachaButtonInfo[];
    gachaCaseId: number;
    gachaCaseUiId: number;
    gachaCategoryType: GachaCategoryType;
    gachaGroupType: GachaGroupType;
    gachaRelicType: GachaRelicType;
    gachaSelectCharacterIdList: number[];
    gachaSelectListType: GachaSelectListType;
    maxDrawGold: number;
    remainingDrawGold: number;
    gachaDrawCount: number;
    gachaCeilingCount: number;
    gachaCaseFlags: GachaCaseFlags;
}
