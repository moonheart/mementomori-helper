/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MasterBookBase } from "./masterBookBase";
import { IHasStartEndTime } from "./iHasStartEndTime";
import { BaseParameter } from "./baseParameter";
import { BattleParameter } from "./battleParameter";
import { CharacterRarityFlags } from "./characterRarityFlags";
import { ElementType } from "./elementType";
import { EquipmentRarityFlags } from "./equipmentRarityFlags";
import { GuildRaidDamageBar } from "./guildRaidDamageBar";
import { GuildRaidBossType } from "./guildRaidBossType";
import { JobFlags } from "./jobFlags";
import { UnitIconType } from "./unitIconType";

export class GuildRaidBossMB extends MasterBookBase implements IHasStartEndTime {
    activeSkillIds: number[];
    bannerText: string;
    baseParameter: BaseParameter;
    battleParameter: BattleParameter;
    battlePower: number;
    characterRarityFlags: CharacterRarityFlags;
    elementType: ElementType;
    enemyRank: number;
    exclusiveEquipmentRarityFlags: EquipmentRarityFlags;
    guildDamageBar: GuildRaidDamageBar[];
    guildRaidBossType: GuildRaidBossType;
    guildRaidButtonU: number;
    guildRaidButtonV: number;
    isActiveMypageIcon: boolean;
    jobFlags: JobFlags;
    nameKey: string;
    normalDamageBar: GuildRaidDamageBar[];
    normalSkillId: number;
    passiveSkillIds: number[];
    releasableGuildFame: number;
    unitIconId: number;
    unitIconType: UnitIconType;
    startTime: string;
    endTime: string;
    worldDamageBarRewardCharacterImageSize: number;
    worldDamageBarRewardCharacterImageX: number;
    worldDamageBarRewardCharacterImageY: number;
}
