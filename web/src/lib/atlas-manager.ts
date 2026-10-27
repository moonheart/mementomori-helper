/**
 * Ortega 版 Atlas 资源管理（基于 `Ortega_Common_Manager_AtlasManager`）
 * - 保留函数与分支逻辑，最终返回拼接后的 URL
 * - URL 规则使用 [`src/lib/asset-constants.ts`](src/lib/asset-constants.ts) 的 `BASE_URL_LOCAL`
 */

import { BattleFieldCharacterGroupType } from '@/api/generated/battleFieldCharacterGroupType';
import { BASE_URL_LOCAL } from '@/lib/asset-constants';
import { BookSortGridCellUnlockItemType } from '@/api/generated/bookSortGridCellUnlockItemType';
import { CharacterRarityFlags } from '@/api/generated/characterRarityFlags';
import { DungeonBattleRelicBattlePowerBonusTargetType } from '@/api/generated/dungeonBattleRelicBattlePowerBonusTargetType';
import { ElementType } from '@/api/generated/elementType';
import { EquipmentMB } from '@/api/generated/equipmentMB';
import { EquipmentRarityFlags } from '@/api/generated/equipmentRarityFlags';
import { EquipmentSlotType } from '@/api/generated/equipmentSlotType';
import { GuildTowerCharacterConditionType } from '@/api/generated/guildTowerCharacterConditionType';
import { ItemRarityFlags } from '@/api/generated/itemRarityFlags';
import { ItemType } from '@/api/generated/itemType';
import { JobFlags } from '@/api/generated/jobFlags';
import { LegendLeagueClassType } from '@/api/generated/legendLeagueClassType';
import { NoticeButtonImageType } from '@/api/generated/noticeButtonImageType';
import { PlayerGuildPositionType } from '@/api/generated/playerGuildPositionType';
import { CommonButtonSpriteType } from '@/api/generated/CommonbuttonSpriteType';
import { CommonElementBonusType } from '@/api/generated/CommonelementBonusType';
import { CommonEquipmentMedalType } from '@/api/generated/CommonequipmentMedalType';
import { CommonPlayerRankingIconType } from '@/api/generated/CommonplayerRankingIconType';
import { SkillCategory } from '@/api/generated/skillCategory';
import { TransferSpotType } from '@/api/generated/transferSpotType';

const getAtlasUrl = (name: string): string => `${BASE_URL_LOCAL}/Atlas/${name}.png`;

type EnumLike = Record<string, string | number>;

type JobFlagsResolver = (characterId: number) => JobFlags | undefined;

const enumToLower = (enumObj: EnumLike, value: number): string => {
    const name = enumObj[value as unknown as keyof typeof enumObj];
    if (typeof name === 'string') return name.toLowerCase();
    return `${value}`;
};

const BOOK_SORT_UNLOCK_ICON_MAP: Record<BookSortGridCellUnlockItemType, string> = {
    [BookSortGridCellUnlockItemType.None]: 'icon_booksort_unlock_grid_1',
    [BookSortGridCellUnlockItemType.Square1X1]: 'icon_booksort_unlock_grid_1',
    [BookSortGridCellUnlockItemType.Square2X1]: 'icon_booksort_unlock_grid_2_horizontal',
    [BookSortGridCellUnlockItemType.Square4X1]: 'icon_booksort_unlock_grid_4_horizontal',
    [BookSortGridCellUnlockItemType.Square1X4]: 'icon_booksort_unlock_grid_4_vertical',
    [BookSortGridCellUnlockItemType.Square2X2]: 'icon_booksort_unlock_grid_4_square',
    [BookSortGridCellUnlockItemType.Cross]: 'icon_booksort_unlock_grid_9_cross',
    [BookSortGridCellUnlockItemType.DiagonalCross]: 'icon_booksort_unlock_grid_9_x',
    [BookSortGridCellUnlockItemType.Square3X3]: 'icon_booksort_unlock_grid_9_square',
};

const NOTICE_ICON_MAP: Record<NoticeButtonImageType, string> = {
    [NoticeButtonImageType.None]: '',
    [NoticeButtonImageType.Information]: 'icon_notice_information',
    [NoticeButtonImageType.Update]: 'icon_notice_update',
    [NoticeButtonImageType.Maintenance]: 'icon_notice_maintenance',
    [NoticeButtonImageType.Bug]: 'icon_notice_bug',
    [NoticeButtonImageType.Event]: 'icon_notice_event',
    [NoticeButtonImageType.Gacha]: 'icon_notice_gacha',
    [NoticeButtonImageType.Campaign]: 'icon_notice_campaign',
    [NoticeButtonImageType.Other]: 'icon_notice_other',
    [NoticeButtonImageType.Survey]: 'icon_notice_survey',
};

const COMMON_BUTTON_ICON_LIST = [
    'button_l_01',
    'button_l_01_orange',
    'button_l_01_white',
    'button_l_02',
    'button_l_02_orange',
    'button_l_02_white',
];

export class OrtegaAtlasManager {
    static characterJobFlagsResolver: JobFlagsResolver | null = null;

    static setCharacterJobFlagsResolver(resolver: JobFlagsResolver | null) {
        this.characterJobFlagsResolver = resolver;
    }

    static load() {
        // URL 版不需要资源加载，保留接口。
    }

    static isAllLoaded(): boolean {
        return true;
    }

    static onDestroy() {
        // URL 版不需要资源释放，保留接口。
    }

    static getSprite(name: string): string {
        return getAtlasUrl(name);
    }

    static getWhite(): string {
        return getAtlasUrl('base_white');
    }

    static getCommonButton(buttonSpriteType: CommonButtonSpriteType): string {
        const icon = COMMON_BUTTON_ICON_LIST[buttonSpriteType];
        return getAtlasUrl(icon ?? 'base_white');
    }

    static getIconCharacterElement(elementType: ElementType | number): string {
        return getAtlasUrl(`icon_element_${elementType}`);
    }

    static getIconJob(jobFlags: JobFlags | number): string {
        return getAtlasUrl(`icon_job_${enumToLower(JobFlags, jobFlags)}`);
    }

    static getIconEquipmentMedal(equipmentMedalType: CommonEquipmentMedalType): string {
        return getAtlasUrl(`icon_equipment_medal_${equipmentMedalType}`);
    }

    static getIconEquipmentSlot(slotType: EquipmentSlotType, characterId: number): string {
        if (slotType !== EquipmentSlotType.Weapon) {
            return getAtlasUrl(`icon_equipment_${enumToLower(EquipmentSlotType, slotType)}_02`);
        }

        const jobFlags = this.characterJobFlagsResolver?.(characterId);
        if (jobFlags === undefined) {
            return getAtlasUrl('base_white');
        }

        return getAtlasUrl(
            `icon_equipment_${enumToLower(EquipmentSlotType, slotType)}_${enumToLower(JobFlags, jobFlags)}_02`
        );
    }

    static getIconEquipmentSlotWithBase(equipmentMB: EquipmentMB | null | undefined): string {
        if (!equipmentMB) {
            return getAtlasUrl('base_white');
        }

        if (equipmentMB.slotType !== EquipmentSlotType.Weapon) {
            return getAtlasUrl(`icon_equipment_${enumToLower(EquipmentSlotType, equipmentMB.slotType)}_01`);
        }

        return getAtlasUrl(
            `icon_equipment_${enumToLower(EquipmentSlotType, equipmentMB.slotType)}_${enumToLower(
                JobFlags,
                equipmentMB.equippedJobFlags
            )}_01`
        );
    }

    static getIconElementBonus(elementBonusType: CommonElementBonusType): string {
        return getAtlasUrl(`icon_element_bonus_${elementBonusType + 1}`);
    }

    static getIconCharacterLRPlusStar(starNumber: number): string {
        return getAtlasUrl(`icon_rarity_plus_star_${starNumber}`);
    }

    static getIconEvolutionMaterial(): string {
        return getAtlasUrl('icon_evolution_material_ssrplus');
    }

    static getIconTip(iconId: number): string {
        const paddedId = iconId.toString().padStart(2, '0');
        return getAtlasUrl(`icon_tip_${paddedId}`);
    }

    static getIconNotice(noticeButtonImageType: NoticeButtonImageType): string | null {
        const iconName = NOTICE_ICON_MAP[noticeButtonImageType];
        if (!iconName) return null;
        return getAtlasUrl(iconName);
    }

    static getIconEventPortalOutsideBanner(transferSpotType: TransferSpotType): string | null {
        switch (transferSpotType) {
            case TransferSpotType.DungeonBattle:
                return getAtlasUrl('icon_dungeon_battle');
            case TransferSpotType.LocalRaid:
                return getAtlasUrl('icon_local_raid');
            case TransferSpotType.TradeShop:
                return getAtlasUrl('icon_menu_shop');
            case TransferSpotType.GuildRaid:
                return getAtlasUrl('icon_guild_raid');
            case TransferSpotType.GuildRaidWorldReward:
                return getAtlasUrl('icon_guild_raid_world_reward');
            default:
                return null;
        }
    }

    static getIconPlayerGuildPosition(playerGuildPositionType: PlayerGuildPositionType): string | null {
        switch (playerGuildPositionType) {
            case PlayerGuildPositionType.Leader:
                return getAtlasUrl('icon_guild_position_leader');
            case PlayerGuildPositionType.SubLeader:
                return getAtlasUrl('icon_guild_position_subleader');
            case PlayerGuildPositionType.Commander:
                return getAtlasUrl('icon_guild_position_commander');
            default:
                return null;
        }
    }

    static getIconBookSortGridCellUnlockItem(itemId: BookSortGridCellUnlockItemType | number): string {
        const iconName = BOOK_SORT_UNLOCK_ICON_MAP[itemId as BookSortGridCellUnlockItemType];
        return getAtlasUrl(iconName ?? `icon_book_sort_unlock_${itemId}`);
    }

    static getFrameSphere(equipmentRarityFlags: EquipmentRarityFlags): string {
        if (equipmentRarityFlags > EquipmentRarityFlags.S) {
            return getAtlasUrl('frame_sphere_metal');
        }
        return getAtlasUrl('frame_sphere_watercolor');
    }

    static getFrameCommon(rarityFlags: ItemRarityFlags | EquipmentRarityFlags): string {
        if (rarityFlags <= ItemRarityFlags.S) {
            return getAtlasUrl('frame_common_watercolor');
        }
        if (rarityFlags !== ItemRarityFlags.LR) {
            return getAtlasUrl('frame_common_slice');
        }
        return getAtlasUrl('frame_common_lr_slice');
    }

    static getFrameCharacter(rarityFlags: CharacterRarityFlags): string {
        if (rarityFlags === CharacterRarityFlags.None) {
            return getAtlasUrl('frame_common_watercolor');
        }
        if (rarityFlags < CharacterRarityFlags.LR) {
            return getAtlasUrl('frame_common_slice');
        }
        return getAtlasUrl('frame_common_lr_slice');
    }

    static getWideCharacterFrame(rarityFlags: CharacterRarityFlags): string {
        if (rarityFlags < CharacterRarityFlags.LR) {
            return getAtlasUrl('frame_wide_slice');
        }
        return getAtlasUrl('frame_wide_lr_slice');
    }

    static getFramePlayer(type: LegendLeagueClassType): string {
        if (type === LegendLeagueClassType.None) {
            return getAtlasUrl('frame_common_watercolor');
        }
        return getAtlasUrl(`frame_player_${enumToLower(LegendLeagueClassType, type)}`);
    }

    static getIconPlate(): string {
        return getAtlasUrl('plate_character');
    }

    static getIconPlateNone(): string {
        return getAtlasUrl('plate_none');
    }

    static getPlateRanking(rankingIconType: CommonPlayerRankingIconType): string {
        return getAtlasUrl(`plate_ranking_${rankingIconType}`);
    }

    static getIconPlateStamp(): string {
        return getAtlasUrl('plate_stamp');
    }

    static getTerritoryIcon(chapterId: number): string {
        const paddedId = chapterId.toString().padStart(3, '0');
        return getAtlasUrl(`icon_territory_${paddedId}`);
    }

    static getCharacterFrameDecoration(rarityFlags: CharacterRarityFlags): string {
        return getAtlasUrl(`frame_decoration_${enumToLower(CharacterRarityFlags, rarityFlags)}`);
    }

    static getDungeonBattleRelicTargetTypeIcon(type: DungeonBattleRelicBattlePowerBonusTargetType): string {
        if (type <= DungeonBattleRelicBattlePowerBonusTargetType.ElementYellow) {
            return getAtlasUrl(`icon_element_${type}`);
        }

        if (type >= DungeonBattleRelicBattlePowerBonusTargetType.Warrior && type <= DungeonBattleRelicBattlePowerBonusTargetType.Sorcerer) {
            return getAtlasUrl(`icon_job_${enumToLower(DungeonBattleRelicBattlePowerBonusTargetType, type)}`);
        }

        return getAtlasUrl('base_white');
    }

    static getTemporaryIcon(): string {
        return getAtlasUrl('icon_temporary');
    }

    static getTreasureBox(isOpen: boolean): string {
        return getAtlasUrl(isOpen ? 'icon_treasure_box_open' : 'icon_treasure_box_close');
    }

    static getGuildTowerCharacterConditionIcon(conditionType: GuildTowerCharacterConditionType): string {
        return getAtlasUrl(`icon_character_condition_${enumToLower(GuildTowerCharacterConditionType, conditionType)}`);
    }

    static getBattleReportIcon(type: number): string | null {
        if (type === BattleFieldCharacterGroupType.Attacker || type === 4) {
            return getAtlasUrl('icon_battle_report_offense');
        }

        if (type === BattleFieldCharacterGroupType.Receiver || type === 5) {
            return getAtlasUrl('icon_battle_report_defense');
        }

        return null;
    }

    static getIconQuestion(): string {
        return getAtlasUrl('icon_question');
    }

    static getBattleEffectStateIcon(skillCategory: SkillCategory): string {
        if (skillCategory === SkillCategory.Buff) {
            return getAtlasUrl('icon_battle_effect_buff');
        }
        if (skillCategory === SkillCategory.DeBuff) {
            return getAtlasUrl('icon_battle_effect_debuff');
        }
        return getAtlasUrl('icon_battle_effect_lock');
    }

    static getIconItemCategory(itemType: ItemType): string | null {
        if (itemType === ItemType.EquipmentFragment || itemType === ItemType.CharacterFragment) {
            return getAtlasUrl('icon_fragment');
        }

        if (itemType === ItemType.ChatEmoticon) {
            return getAtlasUrl('icon_thumbnail_stamp');
        }

        return null;
    }
}

export default OrtegaAtlasManager;
