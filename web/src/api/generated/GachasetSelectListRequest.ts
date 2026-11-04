/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiRequestBase } from "./apiRequestBase";
import { GachaSelectListType } from "./gachaSelectListType";

export class GachaSetSelectListRequest extends ApiRequestBase {
    characterIdList: number[];
    gachaCaseId: number;
    gachaSelectListType: GachaSelectListType;
}
