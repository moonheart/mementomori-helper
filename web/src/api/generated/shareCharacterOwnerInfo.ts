/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { IPlayerIconInfo } from "./iPlayerIconInfo";
import { IShareCharacterOwnerInfo } from "./iShareCharacterOwnerInfo";
import { LegendLeagueClassType } from "./legendLeagueClassType";

export class ShareCharacterOwnerInfo implements IPlayerIconInfo, IShareCharacterOwnerInfo {
    characterGuid: string;
    playerName: string;
    worldId: number;
    mainCharacterIconId: number;
    mainCharacterIconEffectId: number;
    prevLegendLeagueClass: LegendLeagueClassType;

    //<custom-body>
    get playerIconInfo(): IPlayerIconInfo {
        return this;
    }
    //</custom-body>
}
