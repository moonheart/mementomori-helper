/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiRequestBase } from "./apiRequestBase";
import { EquipmentInheritanceType } from "./equipmentInheritanceType";

export class EquipmentInheritanceEquipmentRequest extends ApiRequestBase {
    inheritanceEquipmentGuid: string;
    sourceEquipmentGuid: string;
    equipmentInheritanceTypeList: EquipmentInheritanceType[];
}
