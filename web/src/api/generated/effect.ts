/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { IDeepCopy } from "./iDeepCopy";
import { EffectType } from "./effectType";

export class Effect implements IDeepCopy<Effect> {
    effectType: EffectType;
    effectValue: number;
    effectMaxCount: number;
    effectCount: number;
}
