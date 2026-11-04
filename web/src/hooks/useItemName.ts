import { useCallback, useState, useEffect } from 'react';
import { useTranslation } from '@/hooks/useTranslation';
import { useMasterStore } from '@/store/masterStore';
import { getItemName as getItemNameRaw, ItemMasterTables } from '@/lib/itemUtils';
import { ItemType } from '@/api/generated/itemType';
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

/**
 * 道具名称解析 Hook
 * 自动加载所需的 Master 数据表并提供解析函数
 */
export function useItemName() {
    const { t } = useTranslation();
    const getTable = useMasterStore(state => state.getTable);
    const [masterTables, setMasterTables] = useState<ItemMasterTables>({});
    const [isLoading, setIsLoading] = useState(true);

    useEffect(() => {
        const loadTables = async () => {
            try {
                const [
                    itemTable,
                    equipmentTable,
                    characterTable,
                    equipmentCompositeTable,
                    sphereTable,
                    treasureChestTable,
                    equipmentSetMaterialTable,
                    levelLinkTable,
                    dungeonBattleRelicTable,
                    equipmentSetMaterialBoxTable
                ] = await Promise.all([
                    getTable<ItemMB>('ItemTable'),
                    getTable<EquipmentMB>('EquipmentTable'),
                    getTable<CharacterMB>('CharacterTable'),
                    getTable<EquipmentCompositeMB>('EquipmentCompositeTable'),
                    getTable<SphereMB>('SphereTable'),
                    getTable<TreasureChestMB>('TreasureChestTable'),
                    getTable<EquipmentSetMaterialMB>('EquipmentSetMaterialTable'),
                    getTable<LevelLinkMB>('LevelLinkTable'),
                    getTable<DungeonBattleRelicMB>('DungeonBattleRelicTable'),
                    getTable<EquipmentSetMaterialBoxMB>('EquipmentSetMaterialBoxTable'),
                ]);

                setMasterTables({
                    ItemTable: itemTable,
                    EquipmentTable: equipmentTable,
                    CharacterTable: characterTable,
                    EquipmentCompositeTable: equipmentCompositeTable,
                    SphereTable: sphereTable,
                    TreasureChestTable: treasureChestTable,
                    EquipmentSetMaterialTable: equipmentSetMaterialTable,
                    LevelLinkTable: levelLinkTable,
                    DungeonBattleRelicTable: dungeonBattleRelicTable,
                    EquipmentSetMaterialBoxTable: equipmentSetMaterialBoxTable
                });
            } catch (error) {
                console.error('Failed to load item master tables:', error);
            } finally {
                setIsLoading(false);
            }
        };

        loadTables();
    }, [getTable]);

    /**
     * 获取道具名称
     */
    const getItemName = useCallback((itemType: ItemType, itemId: number) => {
        if (isLoading) return t('COMMON_ITEM_LOADING', [itemId]);
        return getItemNameRaw(itemType, itemId, masterTables, t);
    }, [isLoading, masterTables, t]);

    return {
        getItemName,
        isLoading,
        masterTables, // 暴露出去以便其他用途
        t // 暴露 t 方便组件使用
    };
}
