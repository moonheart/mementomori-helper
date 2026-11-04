/**
 * 游戏资源管理器 - 统一管理图标、图片等资源的URL生成
 *
 * 图标格式说明:
 * - _l: 1024*1024 (大图标)
 * - _m: 256*256 (中等图标)
 * - _s: 128*128 (小图标)
 * - _w: 256*152 (宽屏图标)
 */

import { CharacterRarityFlags } from '@/api/generated/characterRarityFlags';
import { BASE_URL, BASE_URL_LOCAL } from '@/lib/asset-constants';
import OrtegaAtlasManager from '@/lib/atlas-manager';

/** 特殊头像解码掩码 (63个1，去掉最高位符号位) */
const SPECIAL_ICON_MASK = 0x7FFFFFFFFFFFFFFFn;

export type IconSize = 'l' | 'm' | 's' | 'w';

export interface IconOptions {
    size?: IconSize;
    fallback?: string;
}

/**
 * 角色图标管理
 */
export class CharacterIconManager {
    static readonly NEED_FRAME_DECORATION_FLAGS =
        CharacterRarityFlags.RPlus |
        CharacterRarityFlags.SRPlus |
        CharacterRarityFlags.SSRPlus |
        CharacterRarityFlags.URPlus;

    /**
     * 解码特殊头像ID
     * 大负数格式: 使用掩码 0x7FFFFFFFFFFFFFFF 解码出特殊头像ID
     * @param mainCharacterIconId MainCharacterIconId (可能是普通角色ID或特殊头像编码，字符串类型保留完整精度)
     * @returns 解码后的图标信息
     */
    static decodeIconId(mainCharacterIconId: string | number): {
        isSpecialIcon: boolean;
        characterId: number;
        specialIconId?: number;
    } {
        // 转换为 BigInt 处理
        const iconIdBigInt = BigInt(mainCharacterIconId);

        // 正数直接作为角色ID使用
        if (iconIdBigInt > 0) {
            return {
                isSpecialIcon: false,
                characterId: Number(iconIdBigInt)
            };
        }

        // 处理大负数 (特殊头像编码)
        // 使用掩码 0x7FFFFFFFFFFFFFFF 解码出特殊头像ID
        const specialId = Number(iconIdBigInt & SPECIAL_ICON_MASK);

        return {
            isSpecialIcon: true,
            characterId: specialId, // 临时使用，需要查询 SpecialIconItemMB 获取实际角色ID
            specialIconId: specialId
        };
    }

    /**
     * 获取角色图标URL
     * @param characterId 角色ID (数字)
     * @param options 图标选项
     * @returns 完整的图标URL
     */
    static getUrl(characterId: number, options: IconOptions = {}): string {
        const { size = 'm' } = options;
        const paddedId = characterId.toString().padStart(6, '0');
        return `${BASE_URL}/CharacterIcon/CHR_${paddedId}/CHR_${paddedId}_00_${size}.png`;
    }

    /**
     * 获取特殊头像图标URL
     * 格式: CHR_{characterId}_00_em_001.png (后缀固定为001)
     * @param characterId 基础角色ID (来自 SpecialIconItemMB.characterId)
     * @returns 完整的图标URL
     */
    static getSpecialIconUrl(characterId: number): string {
        const paddedCharId = characterId.toString().padStart(6, '0');
        return `${BASE_URL}/CharacterIcon/CHR_${paddedCharId}/CHR_${paddedCharId}_00_em_001.png`;
    }

    /**
     * 获取角色头像URL (小尺寸，适合列表显示)
     */
    static getAvatarUrl(characterId: number): string {
        return this.getUrl(characterId, { size: 's' });
    }

    /**
     * 获取角色卡片URL (中等尺寸，适合卡片显示)
     */
    static getCardUrl(characterId: number): string {
        return this.getUrl(characterId, { size: 'm' });
    }

    /**
     * 获取角色详情URL (大尺寸，适合详情页)
     */
    static getDetailUrl(characterId: number): string {
        return this.getUrl(characterId, { size: 'l' });
    }

    /**
     * 获取角色宽屏图标URL (256*152，适合横幅)
     */
    static getWideUrl(characterId: number): string {
        return this.getUrl(characterId, { size: 'w' });
    }

    /**
     * 获取角色头像背景底板
     */
    static getFrameBackgroundUrl(): string {
        return OrtegaAtlasManager.getIconPlate();
    }

    /**
     * 获取角色头像边框
     */
    static getFrameUrl(rarity: CharacterRarityFlags): string {
        return OrtegaAtlasManager.getFrameCharacter(rarity);
    }

    /**
     * 获取边框装饰
     */
    static getDecorationUrl(rarity: CharacterRarityFlags): string | null {
        if ((CharacterIconManager.NEED_FRAME_DECORATION_FLAGS & rarity) !== rarity) {
            return null;
        }
        return OrtegaAtlasManager.getCharacterFrameDecoration(rarity);
    }

    /**
     * 获取角色元素图标
     */
    static getElementUrl(elementType?: number | null): string | null {
        if (elementType === undefined || elementType === null) return null;
        return OrtegaAtlasManager.getIconCharacterElement(elementType);
    }

    /**
     * 获取稀有度滤镜
     */
    static getRarityFilter(rarity: CharacterRarityFlags): string {
        switch (rarity) {
            case CharacterRarityFlags.N:
                return 'url(#svgTintN)';
            case CharacterRarityFlags.R:
            case CharacterRarityFlags.RPlus:
                return 'url(#svgTintR)';
            case CharacterRarityFlags.SR:
            case CharacterRarityFlags.SRPlus:
                return 'url(#svgTintSR)';
            case CharacterRarityFlags.SSR:
            case CharacterRarityFlags.SSRPlus:
                return 'url(#svgTintSSR)';
            case CharacterRarityFlags.UR:
            case CharacterRarityFlags.URPlus:
                return 'url(#svgTintUR)';
            default:
                return 'none';
        }
    }

    /**
     * 获取稀有星级数量
     */
    static getRarityStarCount(rarity: CharacterRarityFlags): number {
        switch (rarity) {
            case CharacterRarityFlags.LRPlus:
                return 1;
            case CharacterRarityFlags.LRPlus2:
                return 2;
            case CharacterRarityFlags.LRPlus3:
                return 3;
            case CharacterRarityFlags.LRPlus4:
                return 4;
            case CharacterRarityFlags.LRPlus5:
                return 5;
            case CharacterRarityFlags.LRPlus6:
                return 1;
            case CharacterRarityFlags.LRPlus7:
                return 2;
            case CharacterRarityFlags.LRPlus8:
                return 3;
            case CharacterRarityFlags.LRPlus9:
                return 4;
            case CharacterRarityFlags.LRPlus10:
                return 5;
            default:
                return 0;
        }
    }

    /**
     * 获取稀有度星星图标
     */
    static getRarityStarIconUrl(rarity?: CharacterRarityFlags | null): string {
        if (rarity && rarity >= CharacterRarityFlags.LRPlus6) {
            return OrtegaAtlasManager.getSprite('icon_rarity_plus_star_2');
        }
        return OrtegaAtlasManager.getSprite('icon_rarity_plus_star_1');
    }
}

/**
 * 装备图标管理
 */
export class EquipmentIconManager {
    /**
     * 获取装备图标URL
     * @param equipmentId 装备ID
     */
    static getUrl(equipmentId: number): string {
        const paddedId = equipmentId.toString().padStart(6, '0');
        return `${BASE_URL}/Icon/Equipment/EQP_${paddedId}.png`;
    }
}

/**
 * 道具图标管理
 */
export class ItemIconManager {
    /**
     * 获取道具图标URL
     * @param itemId 道具ID
     */
    static getUrl(itemId: number): string {
        const paddedId = itemId.toString().padStart(4, '0');
        return `${BASE_URL}/Icon/Item/Item_${paddedId}.png`;
    }
}

/**
 * 遗物(圣遗物)图标管理
 */
export class RelicIconManager {
    /**
     * 获取遗物图标URL
     * @param relicId 遗物ID
     * @param options 图标选项
     */
    static getUrl(relicId: number, options: IconOptions = {}): string {
        const { size = 'm' } = options;
        const paddedId = relicId.toString().padStart(6, '0');
        return `${BASE_URL}/DungeonBattleRelicIcon/ITM_DungeonBattleRelic_${paddedId}/ITM_DungeonBattleRelic_${paddedId}_${size}.png`;
    }
}

/**
 * 宝石图标管理
 */
export class SphereIconManager {
    /**
     * 获取宝石图标URL
     * @param categoryId 符石分类ID (2位)
     * @param sphereLv 符石等级 (1-20+)
     * @returns 图标URL，根据等级自动选择尺寸后缀
     *
     * URL格式: SPH_{categoryId}{suffix}.png
     * - categoryId: 2位分类ID
     * - suffix: 根据等级选择
     *   - 等级1-5: 01 (128*128)
     *   - 等级6-9: 02 (128*128)
     *   - 等级10+: 03 (128*128)
     *   - 00: 超小图标 (19*19)
     */
    static getUrl(categoryId: number, sphereLv?: number): string {
        let suffix: string;

        if (sphereLv === undefined) {
            // 默认使用中等尺寸 (等级1-5)
            suffix = '01';
        } else if (sphereLv >= 10) {
            suffix = '03';
        } else if (sphereLv >= 6) {
            suffix = '02';
        } else {
            suffix = '01';
        }

        const paddedCategoryId = categoryId.toString().padStart(2, '0');
        return `${BASE_URL_LOCAL}/Icon/Sphere/SPH_${paddedCategoryId}${suffix}.png`;
    }

    /**
     * 获取超小尺寸宝石图标 (19*19)
     */
    static getTinyUrl(categoryId: number): string {
        const paddedCategoryId = categoryId.toString().padStart(2, '0');
        return `${BASE_URL_LOCAL}/Icon/Sphere/SPH_${paddedCategoryId}00.png`;
    }
}

/**
 * 技能图标管理
 */
export class SkillIconManager {
    /**
     * 获取技能图标URL (SKL格式)
     * @param skillId 技能ID
     * @param options 图标选项
     */
    static getUrl(skillId: number, options: IconOptions = {}): string {
        const { size = 'm' } = options;
        const paddedId = skillId.toString().padStart(6, '0');
        return `${BASE_URL}/SkillIcon/SKL_${paddedId}/SKL_${paddedId}_${size}.png`;
    }

    /**
     * 获取技能图标URL (CSK格式，用于效果组图标)
     * @param skillId 技能ID (如 39003)
     * @returns CSK格式的图标URL (如 CSK_000039003.png)
     */
    static getCskUrl(skillId: number): string {
        const paddedId = skillId.toString().padStart(9, '0');
        return `${BASE_URL}/Icon/Skill/CSK_${paddedId}.png`;
    }
}

/**
 * 敌人图标管理
 */
export class EnemyIconManager {
    /**
     * 获取敌人图标URL
     * @param enemyId 敌人ID
     */
    static getUrl(enemyId: number): string {
        const paddedId = enemyId.toString().padStart(6, '0');
        return `${BASE_URL_LOCAL}/Icon/Enemy/ENE_${paddedId}.png`;
    }
}

/**
 * MyPage图标管理
 */
export class MyPageIconManager {
    /**
     * 获取MyPage图标URL
     * @param imageId 图片ID
     */
    static getUrl(imageId: number): string {
        const paddedId = imageId.toString().padStart(6, '0');
        return `${BASE_URL}/Icon/MyPage/MYPAGE_ICON_${paddedId}.png`;
    }
}

/**
 * 统一的资源管理器入口
 */
export const AssetManager = {
    character: CharacterIconManager,
    enemy: EnemyIconManager,
    equipment: EquipmentIconManager,
    item: ItemIconManager,
    relic: RelicIconManager,
    sphere: SphereIconManager,
    skill: SkillIconManager,
    myPage: MyPageIconManager,
} as const;

export default AssetManager;
