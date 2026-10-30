/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { BountyQuestType } from "./bountyQuestType";
import { BountyQuestRarityFlags } from "./bountyQuestRarityFlags";
import { BountyQuestConditionInfo } from "./bountyQuestConditionInfo";
import { UserItem } from "./userItem";

export class BountyQuestInfo {
    bountyQuestId: number;
    bountyQuestNameKey: string;
    bountyQuestType: BountyQuestType;
    bountyQuestRarity: BountyQuestRarityFlags;
    characterMaxCount: number;
    bountyQuestLimitTime: number;
    bountyQuestClearTime: number;
    bountyQuestConditionInfos: BountyQuestConditionInfo[];
    rewardItems: UserItem[];
    initialRequireCurrencyForQuick: number;
}
