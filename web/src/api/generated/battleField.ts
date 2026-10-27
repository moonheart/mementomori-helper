/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { BattleType } from "./battleType";
import { BattleFieldCharacter } from "./battleFieldCharacter";
import { DamageBar } from "./damageBar";

export class BattleField {
    battleType: BattleType;
    characters: BattleFieldCharacter[] = [];
    attackTeamPassiveSkillIds: number[] = [];
    receiveTeamPassiveSkillIds: number[] = [];
    attackTeamTotalKillCount: number;
    receiveTeamTotalKillCount: number;
    damageBarList: DamageBar[];
}
