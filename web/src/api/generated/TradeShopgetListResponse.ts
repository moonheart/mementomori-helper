/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { TradeShopTabInfo } from "./tradeShopTabInfo";

export class TradeShopGetListResponse extends ApiResponseBase {
    tradeShopTabInfoList: TradeShopTabInfo[];
    minOpenQuestId: number;
    minOpenPartyLevel: number;
}
