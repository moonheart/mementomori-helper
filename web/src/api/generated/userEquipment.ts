/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { IUserItem } from "./iUserItem";
import { IReadOnlyEquipment } from "./iReadOnlyEquipment";
import { IAdditionalParameter } from "./iAdditionalParameter";
import { ItemType } from "./itemType";

export class UserEquipment implements IUserItem, IReadOnlyEquipment, IAdditionalParameter {
    characterGuid: string;
    hasParameter: boolean;
    equipmentId: number;
    guid: string;
    itemCount: number;
    itemId: number;
    itemType: ItemType;
    additionalParameterHealth: number;
    additionalParameterIntelligence: number;
    additionalParameterMuscle: number;
    additionalParameterEnergy: number;
    sphereId1: number;
    sphereId2: number;
    sphereId3: number;
    sphereId4: number;
    sphereUnlockedCount: number;
    legendSacredTreasureExp: number;
    legendSacredTreasureLv: number;
    matchlessSacredTreasureExp: number;
    matchlessSacredTreasureLv: number;
    reinforcementLv: number;
    beforeSynchroEquipmentId: number;
    setBaseSynchroGroupId: number;
    playerId: number;
}
