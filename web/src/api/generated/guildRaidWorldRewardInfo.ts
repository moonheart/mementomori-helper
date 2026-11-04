/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { UserItem } from "./userItem";
import { GuildRaidBossInfo } from "./guildRaidBossInfo";
import { GuildRaidDtoInfo } from "./guildRaidDtoInfo";
import { GuildRaidUserRankingInfo } from "./guildRaidUserRankingInfo";
import { UserGuildRaidDtoInfo } from "./userGuildRaidDtoInfo";
import { UserGuildRaidPreviousDtoInfo } from "./userGuildRaidPreviousDtoInfo";

export class GuildRaidWorldRewardInfo {
    dropDiamondLotteryItemList: UserItem[];
    guildRaidBossInfo: GuildRaidBossInfo;
    guildRaidDtoInfo: GuildRaidDtoInfo;
    guildRaidUserRankingInfos: GuildRaidUserRankingInfo[];
    isOpen: boolean;
    obtainableEquipmentList: UserItem[];
    userGuildRaidDtoInfo: UserGuildRaidDtoInfo;
    userGuildRaidPreviousDtoInfo: UserGuildRaidPreviousDtoInfo;
}
