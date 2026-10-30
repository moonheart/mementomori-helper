/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { BookSortAssistanceQuestStatusType } from "./bookSortAssistanceQuestStatusType";
import { UserItem } from "./userItem";
import { CharacterRarityFlags } from "./characterRarityFlags";

export class UserBookSortAssistanceQuest {
    guid: string;
    grade: number;
    nameKey: string;
    bookSortAssistanceQuestStatusType: BookSortAssistanceQuestStatusType;
    rewardItem: UserItem;
    dispatchEndLocalTimeStamp: number;
    characterId: number;
    characterRarityFlags: CharacterRarityFlags;
    isUnlockEffect: boolean;
}
