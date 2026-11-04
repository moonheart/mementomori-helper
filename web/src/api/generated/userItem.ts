/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { IUserItem } from "./iUserItem";
import { IDeepCopy } from "./iDeepCopy";
import { ItemType } from "./itemType";

export class UserItem implements IUserItem, IDeepCopy<UserItem> {
    itemCount: number;
    itemId: number;
    itemType: ItemType;
}
