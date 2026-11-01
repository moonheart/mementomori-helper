/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { IPlayerIconInfo } from "./iPlayerIconInfo";
import { LegendLeagueClassType } from "./legendLeagueClassType";

export class UnionMissionPersonalRankingEntry implements IPlayerIconInfo {
    point: number;
    acceptedCount: number;
    achievedCount: number;
    playerId: number;
    playerName: string;
    rank: number;
    legendLeagueClass: LegendLeagueClassType;
    mainCharacterIconId: number;
    mainCharacterIconEffectId: number;
}
