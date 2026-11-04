/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { ItemType } from "./itemType";
import { StarsGuidanceConsumeItemInfo } from "./starsGuidanceConsumeItemInfo";

export class StarsGuidanceTradeShopGetStarsGuidanceTradeShopInfoResponse extends ApiResponseBase {
    starsGuidanceTradeShopId: number;
    endLocalTimestamp: number;
    giveItemType: ItemType;
    giveItemId: number;
    todayTradeCount: number;
    limitTradeCount: number;
    starsGuidanceConsumeItemInfoList: StarsGuidanceConsumeItemInfo[];
}
