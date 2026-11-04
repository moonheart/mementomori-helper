/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MasterBookBase } from "./masterBookBase";
import { ElementClassificationType } from "./elementClassificationType";
import { CharacterRarityFlags } from "./characterRarityFlags";
import { RankUpType } from "./rankUpType";

export class CharacterRankUpMaterialMB extends MasterBookBase {
    elementClassification: ElementClassificationType;
    materialRankFlags: CharacterRarityFlags;
    materialRankSecondFlags: CharacterRarityFlags;
    rankFlags: CharacterRarityFlags;
    rankUpResultRarityFlags: CharacterRarityFlags;
    upType: RankUpType;
}
