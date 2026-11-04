/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { IUserSyncApiResponse } from "./iUserSyncApiResponse";
import { UnlockGridCellResult } from "./unlockGridCellResult";
import { BookSortSyncData } from "./bookSortSyncData";
import { UserSyncData } from "./userSyncData";

export class BookSortBookSortUnlockGridCellResponse extends ApiResponseBase implements IUserSyncApiResponse {
    unlockGridCellResultList: UnlockGridCellResult[];
    bookSortSyncData: BookSortSyncData;
    userSyncData: UserSyncData;
}
