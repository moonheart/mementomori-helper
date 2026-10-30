/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { BountyQuestType } from "./bountyQuestType";
import { BountyQuestMemberInfo } from "./bountyQuestMemberInfo";

export class UserBountyQuestDtoInfo {
    date: number;
    bountyQuestId: number;
    bountyQuestType: BountyQuestType;
    bountyQuestLimitStartTime: number;
    bountyQuestEndTime: number;
    rewardEndTime: number;
    isReward: boolean;
    startMembers: BountyQuestMemberInfo[];
}
