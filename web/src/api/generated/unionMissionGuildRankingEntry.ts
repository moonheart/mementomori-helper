/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { IPlayerIconInfo } from "./iPlayerIconInfo";
import { LegendLeagueClassType } from "./legendLeagueClassType";

export class UnionMissionGuildRankingEntry implements IPlayerIconInfo {
    guildId: number;
    guildName: string;
    point: number;
    rank: number;
    guildMemberCount: number;
    numberOfPossibleJoinPeople: number;
    legendLeagueClass: LegendLeagueClassType;
    mainCharacterIconId: number;
    mainCharacterIconEffectId: number;
}
