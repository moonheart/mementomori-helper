/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiRequestBase } from "./apiRequestBase";
import { GuildTowerEntryType } from "./guildTowerEntryType";

export class GuildTowerEntryCharacterRequest extends ApiRequestBase {
    guildTowerEntryType: GuildTowerEntryType;
    characterGuidList: string[];
    isContinueEntry: boolean;
}
