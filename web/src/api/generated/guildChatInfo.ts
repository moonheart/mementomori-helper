/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ChatInfo } from "./chatInfo";
import { ChatReactionType } from "./chatReactionType";

export class GuildChatInfo {
    chatInfo: ChatInfo;
    myChatReactionType: ChatReactionType;
    chatReactionCountMap: { [key in ChatReactionType]?: number; };
    canReact: boolean;
    isAnnounced: boolean;
}
