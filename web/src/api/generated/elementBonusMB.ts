/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MasterBookBase } from "./masterBookBase";
import { BattleParameterChangeInfo } from "./battleParameterChangeInfo";
import { ElementBonusConditionType } from "./elementBonusConditionType";
import { ElementBonusPhaseType } from "./elementBonusPhaseType";

export class ElementBonusMB extends MasterBookBase {
    battleParameterChangeInfos: BattleParameterChangeInfo[];
    conditionType: ElementBonusConditionType;
    phase: ElementBonusPhaseType;
}
