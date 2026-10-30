/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MasterBookBase } from "./masterBookBase";
import { DungeonBattleRelicBattlePowerBonusTargetType } from "./dungeonBattleRelicBattlePowerBonusTargetType";
import { DungeonBattleRelicRarityType } from "./dungeonBattleRelicRarityType";
import { PassiveSkillTypeInfo } from "./passiveSkillTypeInfo";

export class DungeonBattleRelicMB extends MasterBookBase {
    battlePowerBonus: number;
    battlePowerBonusTargetType: DungeonBattleRelicBattlePowerBonusTargetType;
    canMultiplePossession: boolean;
    descriptionKey: string;
    dungeonRelicRarityType: DungeonBattleRelicRarityType;
    nameKey: string;
    passiveSkillTypeInfos: PassiveSkillTypeInfo[];
    reinforceFrom: number;
    iconId: number;
}
