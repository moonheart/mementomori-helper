/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiRequestBase } from "./apiRequestBase";
import { ChatReactionType } from "./chatReactionType";

export class GuildNoteReactGuildNoteRequest extends ApiRequestBase {
    guildNoteId: number;
    chatReactionType: ChatReactionType;
}
