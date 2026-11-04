/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { IDeepCopy } from "./iDeepCopy";
import { SkillCategory } from "./skillCategory";
import { EffectGroupType } from "./effectGroupType";
import { Effect } from "./effect";
import { RemoveEffectType } from "./removeEffectType";

export class EffectGroup implements IDeepCopy<EffectGroup> {
    effectGroupId: number;
    skillCategory: SkillCategory;
    effectGroupType: EffectGroupType;
    effectTurn: number;
    effects: Effect[];
    removeEffectType: RemoveEffectType;
    linkTargetGuid: number;
    granterGuid: number;
    isExtendEffectTurn: boolean;
}
