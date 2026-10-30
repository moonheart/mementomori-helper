/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MasterBookBase } from "./masterBookBase";
import { ActiveSkillInfo } from "./activeSkillInfo";

export class ActiveSkillMB extends MasterBookBase {
    activeSkillConditions: string;
    activeSkillInfos: ActiveSkillInfo[];
    nameKey: string;
    skillInitCoolTime: number;
    skillMaxCoolTime: number;
    rootActiveSkillId: number;
}
