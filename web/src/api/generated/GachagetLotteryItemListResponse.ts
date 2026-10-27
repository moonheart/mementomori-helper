/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { GachaBonusRate } from "./gachaBonusRate";
import { GachaItemRate } from "./gachaItemRate";
import { GachaRarityRate } from "./gachaRarityRate";

export class GachaGetLotteryItemListResponse extends ApiResponseBase {
    gachaBonusRateList: GachaBonusRate[];
    gachaItemRateList: GachaItemRate[];
    gachaItemRateSpecialList: GachaItemRate[];
    gachaRarityRateList: GachaRarityRate[];
    gachaRarityRateSpecialList: GachaRarityRate[];
}
