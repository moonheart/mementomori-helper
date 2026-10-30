/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { UserBookSortAssistanceQuest } from "./userBookSortAssistanceQuest";
import { UserBookSortAssistanceLv } from "./userBookSortAssistanceLv";

export class BookSortAssistanceBookSortAssistanceGetInfoResponse extends ApiResponseBase {
    userBookSortAssistanceQuestList: UserBookSortAssistanceQuest[];
    changeDayLocalTimeStamp: number;
    userBookSortAssistanceLv: UserBookSortAssistanceLv;
    endLocalTimeStamp: number;
    addedAssistanceConditionCharacterIdList: number[];
    shopProductEndLocalTimeStamp: number;
}
