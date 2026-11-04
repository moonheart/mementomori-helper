import type { UserEquipmentDtoInfo, EquipmentMB } from '@/api/generated';
import { EquipmentSlotType } from '@/api/generated/equipmentSlotType';
import { EquipmentRarityFlags } from '@/api/generated/equipmentRarityFlags';
import { BattleParameterType } from '@/api/generated/battleParameterType';

/**
 * 组合后的 UI 装备类型
 */
export interface UIEquipment extends UserEquipmentDtoInfo {
    master?: EquipmentMB;
    name: string;
    rarity: EquipmentRarityFlags;
    slotType: EquipmentSlotType;
    level: number;
    setName?: string;
    equippedByName?: string;
    power: number;
    sphereCategoryIds?: (number | undefined)[]; // 每个插槽的符石 categoryId
}

/**
 * 获取稀有度对应的颜色类名
 */
export function getRarityColor(rarity: EquipmentRarityFlags): string {
    if (rarity >= EquipmentRarityFlags.LR) return 'bg-gradient-to-r from-orange-500 to-red-600';
    if (rarity >= EquipmentRarityFlags.UR) return 'bg-yellow-500';
    if (rarity >= EquipmentRarityFlags.SSR) return 'bg-purple-600';
    if (rarity >= EquipmentRarityFlags.SR) return 'bg-blue-600';
    if (rarity >= EquipmentRarityFlags.R) return 'bg-blue-400';
    if (rarity >= EquipmentRarityFlags.S) return 'bg-green-500';
    if (rarity >= EquipmentRarityFlags.A) return 'bg-gray-500';
    return 'bg-gray-400';
}

/**
 * 获取稀有度名称
 */
export function getRarityName(rarity: EquipmentRarityFlags): string {
    if (rarity >= EquipmentRarityFlags.LR) return 'LR';
    if (rarity >= EquipmentRarityFlags.UR) return 'UR';
    if (rarity >= EquipmentRarityFlags.SSR) return 'SSR';
    if (rarity >= EquipmentRarityFlags.SR) return 'SR';
    if (rarity >= EquipmentRarityFlags.R) return 'R';
    if (rarity >= EquipmentRarityFlags.S) return 'S';
    if (rarity >= EquipmentRarityFlags.A) return 'A';
    if (rarity >= EquipmentRarityFlags.B) return 'B';
    if (rarity >= EquipmentRarityFlags.C) return 'C';
    if (rarity >= EquipmentRarityFlags.D) return 'D';
    return 'None';
}

/**
 * 获取装备部位对应的图标
 */
export function getSlotIcon(slot: EquipmentSlotType): string {
    const icons: Record<number, string> = {
        [EquipmentSlotType.Weapon]: '⚔️',
        [EquipmentSlotType.Sub]: '💍',
        [EquipmentSlotType.Gauntlet]: '🧤',
        [EquipmentSlotType.Helmet]: '🪖',
        [EquipmentSlotType.Armor]: '🦺',
        [EquipmentSlotType.Shoes]: '👖',
    };
    return icons[slot] || '📦';
}

/**
 * 获取装备部位的中文名称
 */
export function getSlotName(slot: EquipmentSlotType): string {
    const names: Record<number, string> = {
        [EquipmentSlotType.Weapon]: '[EquipmentSlotTypeWeapon]',
        [EquipmentSlotType.Sub]: '[EquipmentSlotTypeSub]',
        [EquipmentSlotType.Gauntlet]: '[EquipmentSlotTypeGauntlet]',
        [EquipmentSlotType.Helmet]: '[EquipmentSlotTypeHelmet]',
        [EquipmentSlotType.Armor]: '[EquipmentSlotTypeArmor]',
        [EquipmentSlotType.Shoes]: '[EquipmentSlotTypeShoes]',
    };
    return names[slot] || 'AUTO_TOWER_UNKNOWN';
}

/**
 * 计算装备的总战斗力
 */
export function calculateEquipmentPower(equipment: UserEquipmentDtoInfo, master?: EquipmentMB): number {
    let power = 0;

    // 1. 基础属性贡献 (来自 Master)
    if (master?.battleParameterChangeInfo) {
        const param = master.battleParameterChangeInfo;
        const weight = getStatWeight(param.battleParameterType);
        power += (param.value || 0) * weight;
    }

    // 2. 附加属性贡献 (来自四维)
    power += (equipment.additionalParameterMuscle || 0) * 1.2;
    power += (equipment.additionalParameterEnergy || 0) * 1.2;
    power += (equipment.additionalParameterIntelligence || 0) * 1.2;
    power += (equipment.additionalParameterHealth || 0) * 1.2;

    // 3. 强化等级加成
    power += (equipment.reinforcementLv || 0) * 100;

    // 4. 圣装/魔装加成
    power += (equipment.legendSacredTreasureLv || 0) * 50;
    power += (equipment.matchlessSacredTreasureLv || 0) * 50;

    // 5. 稀有度系数
    const rarity = master?.rarityFlags || EquipmentRarityFlags.None;
    let multiplier = 1.0;
    if (rarity >= EquipmentRarityFlags.LR) multiplier = 2.5;
    else if (rarity >= EquipmentRarityFlags.UR) multiplier = 2.0;
    else if (rarity >= EquipmentRarityFlags.SSR) multiplier = 1.6;
    else if (rarity >= EquipmentRarityFlags.SR) multiplier = 1.3;

    return Math.floor(power * multiplier);
}

function getStatWeight(type: BattleParameterType): number {
    const weights: Partial<Record<BattleParameterType, number>> = {
        [BattleParameterType.Hp]: 0.2,
        [BattleParameterType.AttackPower]: 2.0,
        [BattleParameterType.Defense]: 1.5,
        [BattleParameterType.Speed]: 5.0,
        [BattleParameterType.Hit]: 2.0,
        [BattleParameterType.Avoidance]: 2.0,
        [BattleParameterType.Critical]: 2.5,
        [BattleParameterType.CriticalResist]: 2.0,
        [BattleParameterType.DefensePenetration]: 3.0,
    };
    return weights[type] || 1.0;
}

/**
 * 格式化属性名称
 */
export function formatStatName(type: BattleParameterType): string {
    const names: Partial<Record<BattleParameterType, string>> = {
        [BattleParameterType.Hp]: '[BattleParameterTypeHp]',
        [BattleParameterType.AttackPower]: '[BattleParameterTypeAttackPower]',
        [BattleParameterType.Defense]: '[BattleParameterTypeDefense]',
        [BattleParameterType.Speed]: '[BattleParameterTypeSpeed]',
        [BattleParameterType.Hit]: '[BattleParameterTypeHit]',
        [BattleParameterType.Avoidance]: '[BattleParameterTypeAvoidance]',
        [BattleParameterType.Critical]: '[BattleParameterTypeCritical]',
        [BattleParameterType.CriticalResist]: '[BattleParameterTypeCriticalResist]',
        [BattleParameterType.DefensePenetration]: '[BattleParameterTypeDefensePenetration]',
        [BattleParameterType.HpDrain]: '[BattleParameterTypeHpDrain]',
        [BattleParameterType.DamageReflect]: '[BattleParameterTypeDamageReflect]',
    };
    return names[type] || `[BattleParameterType${BattleParameterType[type]}]`;
}

/**
 * 格式化属性值显示
 */
export function formatStatValue(type: BattleParameterType, value: number): string {
    const percentTypes = [
        BattleParameterType.Critical,
        BattleParameterType.CriticalResist,
        BattleParameterType.Hit,
        BattleParameterType.Avoidance,
        BattleParameterType.HpDrain,
        BattleParameterType.DamageReflect
    ];

    if (percentTypes.includes(type)) {
        return `+${(value / 100).toFixed(1)}%`;
    }

    return `+${value.toLocaleString()}`;
}

/**
 * 检查装备是否可以强化
 */
export function canEnhance(equipment: UIEquipment): boolean {
    return (equipment.reinforcementLv || 0) < 100;
}

/**
 * 检查装备是否可以打磨
 */
export function canRefine(equipment: UIEquipment): boolean {
    const rarity = equipment.rarity || EquipmentRarityFlags.None;
    return rarity >= EquipmentRarityFlags.R;
}

/**
 * 检查装备是否可以继承
 */
export function canInherit(equipment: UIEquipment): boolean {
    return (
        (equipment.reinforcementLv || 0) > 0 ||
        (equipment.legendSacredTreasureLv || 0) > 0 ||
        (equipment.matchlessSacredTreasureLv || 0) > 0
    );
}

/**
 * 计算符石插槽使用率
 */
export function getRuneSlotUsage(equipment: UserEquipmentDtoInfo): { used: number; total: number; percentage: number } {
    let used = 0;
    if (equipment.sphereId1) used++;
    if (equipment.sphereId2) used++;
    if (equipment.sphereId3) used++;
    if (equipment.sphereId4) used++;
    
    const total = equipment.sphereUnlockedCount || 0;
    const percentage = total > 0 ? Math.floor((used / total) * 100) : 0;
    return { used, total, percentage };
}

/**
 * 获取强化等级显示颜色
 */
export function getEnhanceLevelColor(enhanceLevel: number, maxLevel: number): string {
    const percentage = (enhanceLevel / maxLevel) * 100;

    if (percentage >= 90) return 'text-orange-500';
    if (percentage >= 70) return 'text-purple-500';
    if (percentage >= 50) return 'text-blue-500';
    if (percentage >= 30) return 'text-green-500';
    return 'text-gray-500';
}

/**
 * 排序装备列表
 */
export function sortEquipment(
    equipment: UIEquipment[],
    sortBy: 'level' | 'rarity' | 'enhance' | 'power'
): UIEquipment[] {
    return [...equipment].sort((a, b) => {
        switch (sortBy) {
            case 'level':
                return b.level - a.level;
            case 'rarity':
                return b.rarity - a.rarity;
            case 'enhance':
                return (b.reinforcementLv || 0) - (a.reinforcementLv || 0);
            case 'power':
                return b.power - a.power;
            default:
                return 0;
        }
    });
}
