/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { GuildRank } from "./guildRank";

export class RankingGetGuildRankingResponse extends ApiResponseBase {
    guildApplyingCount: number;
    guildBattlePowerMax: number;
    guildBattlePowerRanking: number;
    guildBattlePowerRankingGuildList: GuildRank[];
    guildLvRanking: number;
    guildLvRankingGuildList: GuildRank[];
    guildLvRankingToday: number;
    guildStockRanking: number;
    guildStockRankingGuildList: GuildRank[];
    guildStockTodayCount: number;
}
