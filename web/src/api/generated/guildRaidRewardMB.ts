/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MasterBookBase } from "./masterBookBase";
import { GuildDamageBarReward } from "./guildDamageBarReward";
import { NormalDamageBarReward } from "./normalDamageBarReward";
import { GuildRaidQuestClearEquipmentLvList } from "./guildRaidQuestClearEquipmentLvList";
import { WorldDamageBarReward } from "./worldDamageBarReward";

export class GuildRaidRewardMB extends MasterBookBase {
    guildDamageBarRewards: GuildDamageBarReward[];
    guildExpPerChallenge: number;
    guildRaidBossId: number;
    lotteryRewardId: number;
    normalDamageBarRewards: NormalDamageBarReward[];
    questClearEquipmentLvList: GuildRaidQuestClearEquipmentLvList[];
    worldDamageBarRewards: WorldDamageBarReward[];
    hideWorldDamage: number;
}
