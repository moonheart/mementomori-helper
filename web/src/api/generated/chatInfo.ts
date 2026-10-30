/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { IPlayerIconInfo } from "./iPlayerIconInfo";
import { ChatBattleInfo } from "./chatBattleInfo";
import { ChatMusicPlaylistInfo } from "./chatMusicPlaylistInfo";
import { ChatType } from "./chatType";
import { LegendLeagueClassType } from "./legendLeagueClassType";
import { SystemChatMessageIdType } from "./systemChatMessageIdType";
import { SystemChatType } from "./systemChatType";

export class ChatInfo implements IPlayerIconInfo {
    characterId: number;
    iconEffectId: number;
    chatBattleInfo: ChatBattleInfo;
    chatMusicPlaylistInfo: ChatMusicPlaylistInfo;
    chatType: ChatType;
    guildName: string;
    legendLeagueClass: LegendLeagueClassType;
    localTimeStamp: number;
    message: string;
    playerId: number;
    playerName: string;
    systemChatMessageArgs: string[];
    systemChatMessageIdType: SystemChatMessageIdType;
    systemChatMessageKey: string;
    systemChatType: SystemChatType;
    balloonItemId: number;
}
