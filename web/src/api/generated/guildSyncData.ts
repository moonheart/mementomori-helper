/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { PlayerInfo } from "./playerInfo";
import { GlobalGvgGroupType } from "./globalGvgGroupType";
import { GuildInfo } from "./guildInfo";
import { GuildTowerBadgeInfo } from "./guildTowerBadgeInfo";
import { PlayerGuildPositionType } from "./playerGuildPositionType";

export class GuildSyncData {
    applyPlayerInfoList: PlayerInfo[] = [];
    createGuildLocalTime: number;
    globalGvgGroupType: GlobalGvgGroupType;
    guildAnnouncement: string;
    guildAnnouncementUpdateTime: number;
    guildBattlePower: number;
    guildInfo: GuildInfo;
    guildPlayerInfoList: PlayerInfo[] = [];
    guildTowerBadgeInfo: GuildTowerBadgeInfo;
    joinGuildTime: number;
    matchingNumber: number;
    playerGuildPositionType: PlayerGuildPositionType;
}
