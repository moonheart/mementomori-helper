/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MiningQuestGradeType } from "./miningQuestGradeType";
import { UserItem } from "./userItem";
import { MiningQuestDailyRewardStatusType } from "./miningQuestDailyRewardStatusType";

export class MiningQuestGradeInfo {
    gradeType: MiningQuestGradeType;
    nameTextKey: string;
    score: number;
    dailyRewardList: UserItem[];
    dailyRewardStatusType: MiningQuestDailyRewardStatusType;
    seasonRewardList: UserItem[];
}
