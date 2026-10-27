/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { DeviceType } from "./deviceType";
import { ShopGuerrillaPackRankType } from "./shopGuerrillaPackRankType";
import { ShopGuerrillaPackOpenType } from "./shopGuerrillaPackOpenType";
import { UserItem } from "./userItem";

export class ShopProductGuerrillaPack {
    dialogImageId: number;
    discountRate: number;
    endTime: number;
    nameKey: string;
    productIdDict: { [key in DeviceType]?: string; };
    shopGuerrillaPackRankType: ShopGuerrillaPackRankType;
    shopProductPrice: number;
    shopGuerrillaPackId: number;
    shopGuerrillaPackOpenType: ShopGuerrillaPackOpenType;
    shopGuerrillaPackOpenValue: number;
    textKey: string;
    userItemList: UserItem[];
}
