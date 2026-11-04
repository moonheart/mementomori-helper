/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { TargetSelectStrategy } from "./targetSelectStrategy";
import { CharacterFilter } from "./characterFilter";

export class PvpOption {
    selectStrategy: TargetSelectStrategy;
    characterFilters: CharacterFilter[] = [];
    excludePlayerIds: number[] = [];
}
