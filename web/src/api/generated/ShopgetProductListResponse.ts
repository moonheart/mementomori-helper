/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { ShopProductSubInfo } from "./shopProductSubInfo";

export class ShopGetProductListResponse extends ApiResponseBase {
    productSubInfoList: ShopProductSubInfo[];
    contractPrivilegeProductSubInfoList: ShopProductSubInfo[];
}
