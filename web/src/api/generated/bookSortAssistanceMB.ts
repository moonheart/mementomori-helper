/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MasterBookBase } from "./masterBookBase";
import { BookSortAssistanceRewardGrade } from "./bookSortAssistanceRewardGrade";
import { BookSortAssistanceLv } from "./bookSortAssistanceLv";
import { BookSortAddAssistanceCondition } from "./bookSortAddAssistanceCondition";

export class BookSortAssistanceMB extends MasterBookBase {
    bookSortEventId: number;
    endTime: string;
    shopTabId: number;
    shopProductDefaultId: number;
    bookSortAssistanceRewardGradeList: BookSortAssistanceRewardGrade[];
    bookSortAssistanceLvList: BookSortAssistanceLv[];
    bookSortAddAssistanceConditionList: BookSortAddAssistanceCondition[];
}
