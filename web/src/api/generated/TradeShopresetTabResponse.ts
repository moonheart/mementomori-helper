/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { IUserSyncApiResponse } from "./iUserSyncApiResponse";
import { TradeShopItem } from "./tradeShopItem";
import { UserSyncData } from "./userSyncData";

export class TradeShopResetTabResponse extends ApiResponseBase implements IUserSyncApiResponse {
    lastFreeManualUpdateTime: number;
    tradeShopItems: TradeShopItem[];
    userSyncData: UserSyncData;
}
