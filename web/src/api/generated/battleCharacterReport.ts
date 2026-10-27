/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { BattleFieldCharacterGroupType } from "./battleFieldCharacterGroupType";
import { UnitType } from "./unitType";
import { CharacterRarityFlags } from "./characterRarityFlags";
import { ElementType } from "./elementType";

export class BattleCharacterReport {
    playerName: string;
    ownerPlayerId: number;
    deckIndex: number;
    groupType: BattleFieldCharacterGroupType;
    characterGuid: string;
    battleCharacterGuid: number;
    unitType: UnitType;
    unitId: number;
    characterLevel: number;
    characterRarityFlags: CharacterRarityFlags;
    elementType: ElementType;
    totalGiveDamage: number;
    totalHpRecovery: number;
    totalReceiveDamage: number;
    maxHp: number;
    hp: number;
}
