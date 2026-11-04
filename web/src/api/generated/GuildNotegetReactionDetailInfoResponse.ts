/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { ChatReactionType } from "./chatReactionType";
import { ReactionPlayerInfo } from "./reactionPlayerInfo";

export class GuildNoteGetReactionDetailInfoResponse extends ApiResponseBase {
    playerInfoListByReactionType: { [key in ChatReactionType]?: ReactionPlayerInfo[]; };
}
