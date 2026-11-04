/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MasterBookBase } from "./masterBookBase";
import { IHasJstStartEndTime } from "./iHasJstStartEndTime";
import { GachaSelectListType } from "./gachaSelectListType";

export class GachaSelectListMB extends MasterBookBase implements IHasJstStartEndTime {
    characterIdList: number[];
    gachaSelectListType: GachaSelectListType;
    isResetSelectedCharacter: boolean;
    newCharacterIdList: number[];
    orderNumber: number;
    startTimeFixJST: string;
    endTimeFixJST: string;
}
