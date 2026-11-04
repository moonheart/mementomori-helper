/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MasterBookBase } from "./masterBookBase";
import { UnionMissionClassType } from "./unionMissionClassType";
import { RarityItem } from "./rarityItem";

export class UnionMissionTotalPointRewardMB extends MasterBookBase {
    classType: UnionMissionClassType;
    requireGuildPoint: number;
    requirePersonalPoint: number;
    guildPointRewardItemList: RarityItem[];
    personalPointRewardItemList: RarityItem[];
}
