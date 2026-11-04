/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { IDeepCopy } from "./iDeepCopy";
import { BattleFieldCharacterGroupType } from "./battleFieldCharacterGroupType";

export class BattlePosition implements IDeepCopy<BattlePosition> {
    deckIndex: number;
    groupType: BattleFieldCharacterGroupType;
    isAttacker: boolean = true;
}
