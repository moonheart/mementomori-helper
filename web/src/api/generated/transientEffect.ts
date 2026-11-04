/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { EffectType } from "./effectType";
import { HitType } from "./hitType";
import { EffectGroup } from "./effectGroup";

export class TransientEffect {
    effectType: EffectType;
    effectValue: number;
    hitType: HitType;
    addEffectGroups: EffectGroup[] = [];
    removeEffectGroups: EffectGroup[] = [];
}
