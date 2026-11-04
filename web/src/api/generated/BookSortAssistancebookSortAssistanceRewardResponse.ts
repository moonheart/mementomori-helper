/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { IUserSyncApiResponse } from "./iUserSyncApiResponse";
import { UserBookSortAssistanceQuest } from "./userBookSortAssistanceQuest";
import { UserItem } from "./userItem";
import { UserSyncData } from "./userSyncData";

export class BookSortAssistanceBookSortAssistanceRewardResponse extends ApiResponseBase implements IUserSyncApiResponse {
    userBookSortAssistanceQuestList: UserBookSortAssistanceQuest[];
    rewardItems: UserItem[];
    userSyncData: UserSyncData;
}
