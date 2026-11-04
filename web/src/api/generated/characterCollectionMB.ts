/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MasterBookBase } from "./masterBookBase";
import { IHasJstStartEndTime } from "./iHasJstStartEndTime";

export class CharacterCollectionMB extends MasterBookBase implements IHasJstStartEndTime {
    nameKey: string;
    requiredCharacterIds: number[];
    requiredPartyLv: number;
    endTimeFixJST: string;
    startTimeFixJST: string;
}
