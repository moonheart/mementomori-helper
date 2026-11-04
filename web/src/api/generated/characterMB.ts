/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MasterBookBase } from "./masterBookBase";
import { IHasJstStartEndTime } from "./iHasJstStartEndTime";
import { BaseParameter } from "./baseParameter";
import { CharacterType } from "./characterType";
import { ElementType } from "./elementType";
import { BattleParameter } from "./battleParameter";
import { ItemRarityFlags } from "./itemRarityFlags";
import { JobFlags } from "./jobFlags";
import { CharacterRarityFlags } from "./characterRarityFlags";

export class CharacterMB extends MasterBookBase implements IHasJstStartEndTime {
    activeSkillIds: number[];
    baseParameterCoefficient: BaseParameter;
    baseParameterGrossCoefficient: number;
    characterType: CharacterType;
    elementType: ElementType;
    initialBattleParameter: BattleParameter;
    itemRarityFlags: ItemRarityFlags;
    jobFlags: JobFlags;
    name2Key: string;
    nameKey: string;
    normalSkillId: number;
    passiveSkillIds: number[];
    rarityFlags: CharacterRarityFlags;
    requireFragmentCount: number;
    endTimeFixJST: string;
    startTimeFixJST: string;
}
