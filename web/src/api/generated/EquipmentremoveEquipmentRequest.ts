/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiRequestBase } from "./apiRequestBase";
import { EquipmentSlotType } from "./equipmentSlotType";

export class EquipmentRemoveEquipmentRequest extends ApiRequestBase {
    userCharacterGuid: string;
    equipmentSlotTypes: EquipmentSlotType[];
}
