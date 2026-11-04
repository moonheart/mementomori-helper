/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MasterBookBase } from "./masterBookBase";
import { UnionMissionClassType } from "./unionMissionClassType";
import { UnionMissionRankingType } from "./unionMissionRankingType";
import { RarityItem } from "./rarityItem";

export class UnionMissionRankingRewardMB extends MasterBookBase {
    classType: UnionMissionClassType;
    rankingType: UnionMissionRankingType;
    rank: number;
    rewardItemList: RarityItem[];
}
