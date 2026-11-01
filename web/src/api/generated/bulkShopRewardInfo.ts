/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { UserItem } from "./userItem";
import { UserCharacter } from "./userCharacter";
import { ShopProductType } from "./shopProductType";

export class BulkShopRewardInfo {
    bonusItemList: UserItem[];
    characterList: UserCharacter[];
    itemList: UserItem[];
    shopProductType: ShopProductType;
    count: number;
    price: number;
    shopProductNameKey: string;
}
