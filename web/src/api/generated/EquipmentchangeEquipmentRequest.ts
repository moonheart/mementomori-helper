/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiRequestBase } from "./apiRequestBase";
import { EquipmentChangeInfo } from "./equipmentChangeInfo";

export class EquipmentChangeEquipmentRequest extends ApiRequestBase {
    userCharacterGuid: string;
    equipmentChangeInfos: EquipmentChangeInfo[];
    isBulk: boolean;
    code: number;
}
