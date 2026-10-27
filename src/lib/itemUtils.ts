import { ItemType } from '@/api/generated/itemType';
import { UserItemDtoInfo } from '@/api/generated/userItemDtoInfo';
import AssetManager from '@/lib/asset-manager';
import {
    ItemMB,
    EquipmentMB,
    CharacterMB,
    EquipmentCompositeMB,
    SphereMB,
    TreasureChestMB,
    EquipmentSetMaterialMB,
    LevelLinkMB,
    DungeonBattleRelicMB,
    EquipmentSetMaterialBoxMB
} from '@/api/generated';

export interface ItemMasterTables {
    ItemTable?: ItemMB[];
    EquipmentTable?: EquipmentMB[];
    CharacterTable?: CharacterMB[];
    EquipmentCompositeTable?: EquipmentCompositeMB[];
    SphereTable?: SphereMB[];
    TreasureChestTable?: TreasureChestMB[];
    EquipmentSetMaterialTable?: EquipmentSetMaterialMB[];
    LevelLinkTable?: LevelLinkMB[];
    DungeonBattleRelicTable?: DungeonBattleRelicMB[];
    EquipmentSetMaterialBoxTable?: EquipmentSetMaterialBoxMB[];
}

/**
 * 获取道具名称的通用工具函数
 * 参考 api\MementoMori.Ortega\Common\Utils\ItemUtil.cs
 */
export function getItemName(
    itemType: ItemType,
    itemId: number,
    masterTables: ItemMasterTables,
    t: (key: string | undefined | null, params?: unknown[]) => string
): string {
    return formatItemName(itemType, itemId, masterTables, t);
}

/**
 * 获取玩家持有的道具数量
 * 参考 api\MementoMori.Api\Extensions\UserSyncDataExtensions.cs
 */
export function getUserItemCount(
    items: UserItemDtoInfo[] | undefined | null,
    itemType: ItemType,
    itemId: number = 0,
    isAnyCurrency: boolean = false
): number {
    if (!items) return 0;

    if (isAnyCurrency && (itemType === ItemType.CurrencyFree || itemType === ItemType.CurrencyPaid)) {
        return items
            .filter(x => x.itemType === ItemType.CurrencyFree || x.itemType === ItemType.CurrencyPaid)
            .reduce((sum, d) => sum + d.itemCount, 0);
    }

    return items
        .filter(x => x.itemType === itemType && (itemId === 0 || x.itemId === itemId))
        .reduce((sum, d) => sum + d.itemCount, 0);
}

/**
 * 内部实现，用于格式化道具名称
 */
function formatItemName(
    itemType: ItemType,
    itemId: number,
    masterTables: ItemMasterTables,
    t: (key: string | undefined | null, params?: unknown[]) => string
): string {
    switch (itemType) {
        case ItemType.Equipment: {
            const equipmentMb = masterTables.EquipmentTable?.find(m => m.id === itemId);
            return t(equipmentMb?.nameKey) || equipmentMb?.memo || t('CHARACTER_DETAIL_EQUIPMENT_FALLBACK', [itemId]);
        }
        case ItemType.EquipmentFragment: {
            const compositeMb = masterTables.EquipmentCompositeTable?.find(m => m.id === itemId);
            const equipmentMb = masterTables.EquipmentTable?.find(m => m.id === compositeMb?.equipmentId);
            const name = t(equipmentMb?.nameKey) || equipmentMb?.memo || t('CHARACTER_DETAIL_EQUIPMENT_FALLBACK', [compositeMb?.equipmentId]);
            return t('[CommonItemEquipmentFragmentFormat]', [name]) || `${name}碎片`;
        }
        case ItemType.Character: {
            const characterMb = masterTables.CharacterTable?.find(m => m.id === itemId);
            if (!characterMb) return t('ITEM_FALLBACK_CHARACTER', [itemId]);
            const n1 = t(characterMb.nameKey);
            const n2 = t(characterMb.name2Key);
            return n2 ? `${n1} ${n2}` : n1;
        }
        case ItemType.CharacterFragment: {
            const characterMb = masterTables.CharacterTable?.find(m => m.id === itemId);
            if (!characterMb) return t('ITEM_FALLBACK_CHARACTER_FRAGMENT', [itemId]);
            const n1 = t(characterMb.nameKey);
            const n2 = t(characterMb.name2Key);
            const name = n2 ? `${n1} ${n2}` : n1;
            return t('[CommonItemCharacterFragment]', [name]) || `${name}碎片`;
        }
        case ItemType.DungeonBattleRelic: {
            const mb = masterTables.DungeonBattleRelicTable?.find(m => m.id === itemId);
            return t(mb?.nameKey) || mb?.memo || t('ITEM_FALLBACK_TREASURE', [itemId]);
        }
        case ItemType.EquipmentSetMaterial: {
            const mb = masterTables.EquipmentSetMaterialTable?.find(m => m.id === itemId);
            return t(mb?.nameKey) || mb?.memo || t('ITEM_FALLBACK_SET_MATERIAL', [itemId]);
        }
        case ItemType.Sphere: {
            const mb = masterTables.SphereTable?.find(m => m.id === itemId);
            return t(mb?.nameKey) || mb?.memo || t('ITEM_FALLBACK_SPHERE', [itemId]);
        }
        case ItemType.TreasureChest: {
            const mb = masterTables.TreasureChestTable?.find(m => m.id === itemId);
            return t(mb?.nameKey) || mb?.memo || t('ITEM_FALLBACK_CHEST', [itemId]);
        }
        case ItemType.LevelLinkExp: {
            const mb = masterTables.LevelLinkTable?.find(m => m.id === itemId);
            return mb?.memo || t('ITEM_FALLBACK_LEVEL_LINK_EXP', [itemId]);
        }
        case ItemType.EquipmentSetMaterialBox: {
            const mb = masterTables.EquipmentSetMaterialBoxTable?.find(m => m.id === itemId);
            return t(mb?.nameKey) || mb?.memo || t('ITEM_FALLBACK_SET_MATERIAL_BOX', [itemId]);
        }
        default: {
            const itemMb = masterTables.ItemTable?.find(m => m.itemType === itemType && m.itemId === itemId);
            return t(itemMb?.nameKey) || itemMb?.memo || t('ITEM_FALLBACK_ITEM', [itemId]);
        }
    }
}

/**
 * 根据 ItemType 和 ItemId 获取道具图标 URL
 * 适用于需要显示道具图标的任意场景
 *
 * 注意：对于普通道具 (default 分支)，图标文件名使用的是 ItemMB.id（主键），
 * 而非 UserItem.itemId。传入 masterTables 时会自动查表取 ItemMB.id；
 * 不传时降级直接使用 itemId（图标可能不准确）。
 */
export function getItemIconUrl(
    itemType: ItemType,
    itemId: number,
    masterTables?: Pick<ItemMasterTables, 'ItemTable'>
): string {
    switch (itemType) {
        case ItemType.Equipment:
        case ItemType.EquipmentFragment:
            return AssetManager.equipment.getUrl(itemId);
        case ItemType.Character:
        case ItemType.CharacterFragment:
            return AssetManager.character.getUrl(itemId);
        case ItemType.DungeonBattleRelic:
            return AssetManager.relic.getUrl(itemId);
        case ItemType.Sphere:
            return AssetManager.sphere.getUrl(itemId);
        default: {
            // 图标文件名使用的是 ItemMB.id（主键），需要从 master table 查找
            const mbId = masterTables?.ItemTable?.find(
                m => m.itemType === itemType && m.itemId === itemId
            )?.id;
            return AssetManager.item.getUrl(mbId ?? itemId);
        }
    }
}
