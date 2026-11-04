/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MasterBookBase } from "./masterBookBase";
import { IHasStartEndTime } from "./iHasStartEndTime";
import { ItemRarityFlags } from "./itemRarityFlags";
import { SecondaryFrameType } from "./secondaryFrameType";

export class EquipmentSetMaterialBoxMB extends MasterBookBase implements IHasStartEndTime {
    descriptionKey: string;
    displayNameKey: string;
    endTime: string;
    equipmentTypeList: number[];
    iconId: number;
    itemRarityFlags: ItemRarityFlags;
    levelList: number[];
    maxItemCount: number;
    nameKey: string;
    secondaryFrameNum: number;
    secondaryFrameType: SecondaryFrameType;
    startTime: string;
}
