import { useState, useEffect } from 'react';
import { ItemCard } from '../ItemCard';
import { UserItemDtoInfo, ItemType, ItemMB, TreasureChestMB, CharacterMB } from '@/api/generated';
import { getItemName } from '@/lib/itemUtils';
import { useMasterStore } from '@/store/masterStore';
import { useLocalizationStore } from '@/store/localization-store';
import { ItemIconManager } from '@/lib/asset-manager';

interface ConsumptionTabProps {
    items: UserItemDtoInfo[];
    onUseItem?: (item: UserItemDtoInfo) => void;
}

const CONSUMPTION_TYPES: ItemType[] = [
    ItemType.QuestQuickTicket,
    ItemType.TreasureChestKey,
    ItemType.TreasureChest,
    ItemType.CharacterFragment,
];

interface ConsumptionItem {
    itemId: number;
    itemType: ItemType;
    itemCount: number;
    playerId: number;
    name: string;
    rarity: string;
    iconUrl: string;
}

export function ConsumptionTab({ items, onUseItem }: ConsumptionTabProps) {
    const { getTable } = useMasterStore();
    const t = useLocalizationStore((state) => state.t);
    const [consumptionItems, setConsumptionItems] = useState<ConsumptionItem[]>([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const loadConsumptionItems = async () => {
            try {
                setLoading(true);

                // 异步加载Master表
                const itemTable = await getTable<ItemMB>('ItemTable');
                const treasureChestTable = await getTable<TreasureChestMB>('TreasureChestTable');
                const characterTable = await getTable<CharacterMB>('CharacterTable');

                const masterTables = {
                    ItemTable: itemTable,
                    TreasureChestTable: treasureChestTable,
                    CharacterTable: characterTable,
                };

                // 过滤和格式化数据
                const formattedItems = items
                    .filter((item) => CONSUMPTION_TYPES.includes(item.itemType) && item.itemCount > 0)
                    .map((item) => ({
                        ...item,
                        name: getItemName(item.itemType, item.itemId, masterTables, t),
                        rarity: '',
                        iconUrl: ItemIconManager.getUrl(item.itemId),
                    }))
                    .sort((a, b) => {
                        if (a.itemType !== b.itemType) {
                            return CONSUMPTION_TYPES.indexOf(a.itemType) - CONSUMPTION_TYPES.indexOf(b.itemType);
                        }
                        return b.itemCount - a.itemCount;
                    });

                setConsumptionItems(formattedItems);
            } catch (error) {
                console.error('Failed to load consumption items:', error);
            } finally {
                setLoading(false);
            }
        };

        loadConsumptionItems();
    }, [items, getTable, t]);

    if (loading) {
        return (
            <div className="flex items-center justify-center py-16">
                <div className="animate-spin rounded-full h-8 w-8 border-b-2 border-primary"></div>
            </div>
        );
    }

    if (consumptionItems.length === 0) {
        return (
            <div className="flex flex-col items-center justify-center py-16 text-muted-foreground">
                <div className="text-6xl mb-4 opacity-20">🎁</div>
                <p>{t('ITEMS_EMPTY_CONSUMPTION')}</p>
            </div>
        );
    }

    return (
        <div className="grid grid-cols-2 md:grid-cols-3 lg:grid-cols-4 xl:grid-cols-5 gap-4">
            {consumptionItems.map((item) => (
                <ItemCard
                    key={`${item.itemType}-${item.itemId}`}
                    name={item.name}
                    rarity={item.rarity}
                    count={item.itemCount}
                    iconUrl={item.iconUrl}
                    canUse={item.itemType === ItemType.QuestQuickTicket}
                    onUse={() => {
                        const originalItem = items.find(
                            i => i.itemType === item.itemType && i.itemId === item.itemId
                        );
                        if (originalItem) onUseItem?.(originalItem);
                    }}
                />
            ))}
        </div>
    );
}
