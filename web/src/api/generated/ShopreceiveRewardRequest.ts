/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiRequestBase } from "./apiRequestBase";
import { ShopProductType } from "./shopProductType";

export class ShopReceiveRewardRequest extends ApiRequestBase {
    isBulk: boolean;
    mBId: number;
    productId: string;
    requiredValue: number;
    shopProductType: ShopProductType;
}
