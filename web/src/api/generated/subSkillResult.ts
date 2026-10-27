/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { SubSkillResultType } from "./subSkillResultType";
import { SkillDisplayType } from "./skillDisplayType";
import { EffectGroup } from "./effectGroup";
import { HitType } from "./hitType";

export class SubSkillResult {
    subSkillResultType: SubSkillResultType;
    subSkillIndex: number;
    skillDisplayType: SkillDisplayType;
    attackUnitGuid: number;
    targetUnitGuid: number;
    addEffectGroups: EffectGroup[] = [];
    removeEffectGroups: EffectGroup[] = [];
    hitType: HitType;
    changeHp: number;
    targetRemainHp: number;
}
