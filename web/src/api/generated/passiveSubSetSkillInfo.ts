/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { IDeepCopy } from "./iDeepCopy";
import { PassiveTrigger } from "./passiveTrigger";

export class PassiveSubSetSkillInfo implements IDeepCopy<PassiveSubSetSkillInfo> {
    passiveTrigger: PassiveTrigger;
    skillCoolTime: number;
    skillMaxCoolTime: number;
    passiveGroupId: number;
    subSetSkillId: number;
}
