/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { TransientEffectResult } from "./transientEffectResult";
import { SubSetSkillResult } from "./subSetSkillResult";
import { SubSkillResult } from "./subSkillResult";

export class ActiveSkillData {
    transientEffectResult: TransientEffectResult;
    activeSkillId: number;
    subSetSkillResults: SubSetSkillResult[];
    actionStartSubSkillResults: SubSkillResult[];
    actionEndSubSkillResults: SubSkillResult[];
    turnEndSubSkillResults: SubSkillResult[];
    isNonActionStance: boolean;
    fromGuid: number;
}
