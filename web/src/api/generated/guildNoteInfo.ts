/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { IPlayerIconInfo } from "./iPlayerIconInfo";
import { GuildNoteCategoryType } from "./guildNoteCategoryType";
import { LegendLeagueClassType } from "./legendLeagueClassType";
import { ChatReactionType } from "./chatReactionType";

export class GuildNoteInfo implements IPlayerIconInfo {
    guildNoteId: number;
    title: string;
    content: string;
    guildNoteCategoryType: GuildNoteCategoryType;
    iconId: number;
    iconEffectId: number;
    legendLeagueClass: LegendLeagueClassType;
    playerId: number;
    playerName: string;
    isDeletedPlayer: boolean;
    myChatReactionType: ChatReactionType;
    chatReactionCountMap: { [key in ChatReactionType]?: number; };
    updateLocalTimeStamp: number;
    order: number;
}
