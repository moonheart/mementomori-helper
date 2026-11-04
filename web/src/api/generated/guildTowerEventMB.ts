/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MasterBookBase } from "./masterBookBase";
import { IHasStartEndTime } from "./iHasStartEndTime";
import { GuildTowerComboBonus } from "./guildTowerComboBonus";
import { GuildTowerDebuffParameter } from "./guildTowerDebuffParameter";

export class GuildTowerEventMB extends MasterBookBase implements IHasStartEndTime {
    comboAddTime: number;
    comboMaxCount: number;
    comboMaxTime: number;
    displayBannerEndTime: string;
    eventNo: number;
    guildTowerComboBonusList: GuildTowerComboBonus[];
    guildTowerDebuffParameterList: GuildTowerDebuffParameter[];
    maxChallengeCount: number;
    missionIdList: number[];
    registerCharacterCount: number;
    startTime: string;
    endTime: string;
}
