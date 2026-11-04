/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { IUserSyncApiResponse } from "./iUserSyncApiResponse";
import { UserEquipment } from "./userEquipment";
import { UserItem } from "./userItem";
import { UserSyncData } from "./userSyncData";

export class EquipmentCastManyResponse extends ApiResponseBase implements IUserSyncApiResponse {
    resultEquipmentList: UserEquipment[];
    resultItemList: UserItem[];
    userSyncData: UserSyncData;
}
