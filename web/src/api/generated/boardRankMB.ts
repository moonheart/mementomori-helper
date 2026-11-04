/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MasterBookBase } from "./masterBookBase";
import { BoardRankConditionInfo } from "./boardRankConditionInfo";
import { BountyQuestRarityFlags } from "./bountyQuestRarityFlags";

export class BoardRankMB extends MasterBookBase {
    boardRankConditionInfos: BoardRankConditionInfo[];
    openBountyQuestMaxRarity: BountyQuestRarityFlags;
    openBountyQuestMinRarity: BountyQuestRarityFlags;
    questLotteryInfoListId: number;
    rank: number;
}
