/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MasterBookBase } from "./masterBookBase";
import { BaseParameterChangeInfo } from "./baseParameterChangeInfo";
import { BattleParameterChangeInfo } from "./battleParameterChangeInfo";
import { CharacterRarityFlags } from "./characterRarityFlags";

export class CharacterCollectionLevelMB extends MasterBookBase {
    baseParameterChangeInfos: BaseParameterChangeInfo[];
    battleParameterChangeInfos: BattleParameterChangeInfo[];
    characterRarityBonus: number;
    characterRarityFlags: CharacterRarityFlags;
    collectionId: number;
    collectionLevel: number;
    maxLevelIncreaseValue: number;
}
