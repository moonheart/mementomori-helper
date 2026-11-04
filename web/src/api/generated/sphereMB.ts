/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MasterBookBase } from "./masterBookBase";
import { BaseParameterChangeInfo } from "./baseParameterChangeInfo";
import { BattleParameterChangeInfo } from "./battleParameterChangeInfo";
import { UserItem } from "./userItem";
import { ItemRarityFlags } from "./itemRarityFlags";
import { SphereType } from "./sphereType";

export class SphereMB extends MasterBookBase {
    baseParameterChangeInfo: BaseParameterChangeInfo;
    battleParameterChangeInfo: BattleParameterChangeInfo;
    categoryId: number;
    descriptionKey: string;
    isAttackType: boolean;
    itemListRequiredToCombine: UserItem[];
    lv: number;
    nameKey: string;
    rarityFlags: ItemRarityFlags;
    sphereType: SphereType;
}
