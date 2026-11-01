/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiRequestBase } from "./apiRequestBase";
import { GuildNoteCategoryType } from "./guildNoteCategoryType";

export class GuildNoteCreateGuildNoteRequest extends ApiRequestBase {
    title: string;
    content: string;
    guildNoteCategoryType: GuildNoteCategoryType;
}
