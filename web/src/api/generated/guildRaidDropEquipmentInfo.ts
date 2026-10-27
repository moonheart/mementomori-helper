/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ILotteryWeight } from "./iLotteryWeight";
import { JobFlags } from "./jobFlags";
import { EquipmentRarityFlags } from "./equipmentRarityFlags";
import { EquipmentSlotType } from "./equipmentSlotType";

export class GuildRaidDropEquipmentInfo implements ILotteryWeight {
    canEquipJobFlags: JobFlags;
    equipmentRarityFlags: EquipmentRarityFlags;
    equipmentSlotType: EquipmentSlotType;
    qualityLv: number;
    lotteryWeight: number;
}
