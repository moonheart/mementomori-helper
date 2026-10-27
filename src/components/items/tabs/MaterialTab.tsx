import { useState, useEffect } from 'react';
import { ItemCard } from '../ItemCard';
import { UserItemDtoInfo, ItemType, ItemMB, EquipmentSetMaterialMB } from '@/api/generated';
import { getItemName } from '@/lib/itemUtils';
import { useMasterStore } from '@/store/masterStore';
import { useLocalizationStore } from '@/store/localization-store';
import { ItemIconManager } from '@/lib/asset-manager';

interface MaterialTabProps {
    items: UserItemDtoInfo[];
}

const MATERIAL_TYPES: ItemType[] = [
    ItemType.CharacterTrainingMaterial,
    ItemType.EquipmentReinforcementItem,
    ItemType.MatchlessSacredTreasureExpItem,
    ItemType.GachaTicket,
    ItemType.BossChallengeTicket,
    ItemType.TowerBattleTicket,
    ItemType.DungeonRecoveryItem,
    ItemType.FriendPoint,
    ItemType.EquipmentRarityCrystal,
    ItemType.ExchangePlaceItem,
    ItemType.EventExchangePlaceItem,
    ItemType.EquipmentSetMaterial,
];

interface MaterialItem {
    itemId: number;
    itemType: ItemType;
    itemCount: number;
    playerId: number;
    name: string;
    rarity: string;
    iconUrl: string;
}

export function MaterialTab({ items }: MaterialTabProps) {
    const { getTable } = useMasterStore();
    const t = useLocalizationStore((state) => state.t);
    const [materialItems, setMaterialItems] = useState<MaterialItem[]>([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const loadMaterialItems = async () => {
            try {
                setLoading(true);

                // 异步加载Master表
                const itemTable = await getTable<ItemMB>('ItemTable');
                const equipmentSetMaterialTable = await getTable<EquipmentSetMaterialMB>('EquipmentSetMaterialTable');

                const masterTables = {
                    ItemTable: itemTable,
                    EquipmentSetMaterialTable: equipmentSetMaterialTable,
                };

                // 过滤和格式化数据
                const formattedItems = items
                    .filter((item) => MATERIAL_TYPES.includes(item.itemType) && item.itemCount > 0)
                    .map((item) => ({
                        ...item,
                        name: getItemName(item.itemType, item.itemId, masterTables, t),
                        rarity: '',
                        iconUrl: ItemIconManager.getUrl(item.itemId),
                    }))
                    .sort((a, b) => b.itemCount - a.itemCount);

                setMaterialItems(formattedItems);
            } catch (error) {
                console.error('Failed to load material items:', error);
            } finally {
                setLoading(false);
            }
        };

        loadMaterialItems();
    }, [items, getTable, t]);

    if (loading) {
        return (
            <div className="flex items-center justify-center py-16">
                <div className="animate-spin rounded-full h-8 w-8 border-b-2 border-primary"></div>
            </div>
        );
    }

    if (materialItems.length === 0) {
        return (
            <div className="flex flex-col items-center justify-center py-16 text-muted-foreground">
                <div className="text-6xl mb-4 opacity-20">📦</div>
                <p>{t('ITEMS_EMPTY_MATERIAL')}</p>
            </div>
        );
    }

    return (
        <div className="grid grid-cols-2 md:grid-cols-3 lg:grid-cols-4 xl:grid-cols-5 gap-4">
            {materialItems.map((item) => (
                <ItemCard
                    key={`${item.itemType}-${item.itemId}`}
                    name={item.name}
                    rarity={item.rarity}
                    count={item.itemCount}
                    iconUrl={item.iconUrl}
                />
            ))}
        </div>
    );
}
