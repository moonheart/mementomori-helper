/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MasterBookBase } from "./masterBookBase";
import { BattleParameterChangeInfo } from "./battleParameterChangeInfo";
import { EquipmentCategory } from "./equipmentCategory";
import { JobFlags } from "./jobFlags";
import { EquipmentRarityFlags } from "./equipmentRarityFlags";
import { EquipmentSlotType } from "./equipmentSlotType";

export class EquipmentMB extends MasterBookBase {
    additionalParameterTotal: number;
    afterLevelEvolutionEquipmentId: number;
    afterRarityEvolutionEquipmentId: number;
    battleParameterChangeInfo: BattleParameterChangeInfo;
    category: EquipmentCategory;
    compositeId: number;
    equipmentEvolutionId: number;
    equipmentExclusiveSkillDescriptionId: number;
    equipmentForgeId: number;
    equipmentLv: number;
    equipmentSetId: number;
    equippedJobFlags: JobFlags;
    exclusiveEffectId: number;
    goldRequiredToOpeningFirstSphereSlot: number;
    goldRequiredToTraining: number;
    iconId: number;
    nameKey: string;
    performancePoint: number;
    qualityLv: number;
    rarityFlags: EquipmentRarityFlags;
    slotType: EquipmentSlotType;
}
