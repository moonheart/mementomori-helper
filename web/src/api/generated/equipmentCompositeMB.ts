/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MasterBookBase } from "./masterBookBase";
import { ItemRarityFlags } from "./itemRarityFlags";
import { UserItem } from "./userItem";

export class EquipmentCompositeMB extends MasterBookBase {
    equipmentId: number;
    itemRarityFlags: ItemRarityFlags;
    requiredFragmentCount: number;
    requiredItemList: UserItem[];
}
