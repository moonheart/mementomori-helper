import { useState, useEffect, useMemo } from 'react';
import { Card, CardContent } from '@/components/ui/card';
import { Badge } from '@/components/ui/badge';
import { Sparkles, CircleDot, Package } from 'lucide-react';
import { UserItemDtoInfo, UserEquipmentDtoInfo, ItemType, EquipmentMB, EquipmentRarityFlags, EquipmentSlotType } from '@/api/generated';
import { getItemName } from '@/lib/itemUtils';
import { useMasterStore } from '@/store/masterStore';
import { useLocalizationStore } from '@/store/localization-store';
import { EquipmentIconManager } from '@/lib/asset-manager';
import { cn } from '@/lib/utils';

interface EquipmentTabProps {
    items: UserItemDtoInfo[];
    userEquipments: UserEquipmentDtoInfo[];
}

interface UnsealedEquipmentItem {
    type: 'unsealed';
    equipment: UserEquipmentDtoInfo;
    masterData?: EquipmentMB;
    name: string;
    rarity: EquipmentRarityFlags;
}

interface SealedEquipmentItem {
    type: 'sealed';
    item: UserItemDtoInfo;
    masterData?: EquipmentMB;
    name: string;
    rarity: EquipmentRarityFlags;
}

// 获取装备槽位名称
function getSlotTypeName(slotType: EquipmentSlotType): string {
    switch (slotType) {
        case EquipmentSlotType.Weapon:
            return '[EquipmentSlotTypeWeapon]';
        case EquipmentSlotType.Armor:
            return '[EquipmentSlotTypeArmor]';
        case EquipmentSlotType.Helmet:
            return '[EquipmentSlotTypeHelmet]';
        case EquipmentSlotType.Shoes:
            return '[EquipmentSlotTypeShoes]';
        case EquipmentSlotType.Gauntlet:
            return '[EquipmentSlotTypeGauntlet]';
        case EquipmentSlotType.Sub:
            return '[EquipmentSlotTypeSub]';
        default:
            return '[ItemBoxTabOther]';
    }
}

// 获取稀有度名称
function getRarityName(rarity: EquipmentRarityFlags): string {
    switch (rarity) {
        case EquipmentRarityFlags.LR:
            return 'LR';
        case EquipmentRarityFlags.UR:
            return 'UR';
        case EquipmentRarityFlags.SSR:
            return 'SSR';
        case EquipmentRarityFlags.SR:
            return 'SR';
        case EquipmentRarityFlags.R:
            return 'R';
        case EquipmentRarityFlags.S:
            return 'S';
        case EquipmentRarityFlags.A:
            return 'A';
        case EquipmentRarityFlags.B:
            return 'B';
        case EquipmentRarityFlags.C:
            return 'C';
        case EquipmentRarityFlags.D:
            return 'D';
        default:
            return '';
    }
}

// 获取稀有度颜色样式
function getRarityColorClass(rarity: EquipmentRarityFlags): string {
    switch (rarity) {
        case EquipmentRarityFlags.LR:
            return 'bg-yellow-500 text-white border-yellow-600';
        case EquipmentRarityFlags.UR:
            return 'bg-purple-500 text-white border-purple-600';
        case EquipmentRarityFlags.SSR:
            return 'bg-red-500 text-white border-red-600';
        case EquipmentRarityFlags.SR:
            return 'bg-orange-500 text-white border-orange-600';
        case EquipmentRarityFlags.R:
            return 'bg-blue-500 text-white border-blue-600';
        case EquipmentRarityFlags.S:
            return 'bg-green-500 text-white border-green-600';
        case EquipmentRarityFlags.A:
            return 'bg-cyan-500 text-white border-cyan-600';
        default:
            return 'bg-gray-400 text-white border-gray-500';
    }
}

// 获取稀有度渐变背景
function getRarityGradient(rarity: EquipmentRarityFlags): string {
    switch (rarity) {
        case EquipmentRarityFlags.LR:
            return 'from-yellow-50 to-amber-100';
        case EquipmentRarityFlags.UR:
            return 'from-purple-50 to-purple-100';
        case EquipmentRarityFlags.SSR:
            return 'from-red-50 to-red-100';
        case EquipmentRarityFlags.SR:
            return 'from-orange-50 to-orange-100';
        case EquipmentRarityFlags.R:
            return 'from-blue-50 to-blue-100';
        case EquipmentRarityFlags.S:
            return 'from-green-50 to-green-100';
        case EquipmentRarityFlags.A:
            return 'from-cyan-50 to-cyan-100';
        default:
            return 'from-gray-50 to-gray-100';
    }
}

// 已解除封印装备卡片
function UnsealedEquipmentCard({ item }: { item: UnsealedEquipmentItem }) {
    const t = useLocalizationStore((state) => state.t);
    const equip = item.equipment;
    const master = item.masterData;
    const rarityName = getRarityName(item.rarity);

    // 已解锁的宝石槽数量
    const unlockedSpheres = equip.sphereUnlockedCount || 0;
    const sphereIds = [equip.sphereId1, equip.sphereId2, equip.sphereId3, equip.sphereId4].filter(id => id > 0);

    return (
        <Card className={cn(
            "overflow-hidden hover:shadow-lg transition-all duration-200 border-2",
            item.rarity >= EquipmentRarityFlags.SSR ? "border-opacity-50" : "border-gray-200"
        )}
        style={{ borderColor: item.rarity >= EquipmentRarityFlags.SSR ? undefined : undefined }}>
            <div className={cn("flex flex-row items-stretch min-w-[280px] flex-1 bg-gradient-to-r", getRarityGradient(item.rarity))}>
                {/* 左侧：图标和基本信息 */}
                <div className="flex flex-col items-center justify-center p-4 min-w-[100px] border-r border-black/5">
                    <div className="w-16 h-16 rounded-lg bg-white/80 shadow-sm flex items-center justify-center mb-2 overflow-hidden">
                        {master ? (
                            <img
                                src={EquipmentIconManager.getUrl(equip.equipmentId)}
                                alt={item.name}
                                className="w-full h-full object-contain"
                                loading="lazy"
                                decoding="async"
                                onError={(e) => {
                                    (e.target as HTMLImageElement).style.display = 'none';
                                    (e.target as HTMLImageElement).parentElement!.innerHTML = '<svg xmlns="http://www.w3.org/2000/svg" width="32" height="32" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="w-8 h-8 text-gray-500"><path d="m7.5 4.27 9 5.15"/><path d="M21 8a2 2 0 0 0-1-1.73l-7-4a2 2 0 0 0-2 0l-7 4A2 2 0 0 0 3 8v8a2 2 0 0 0 1 1.73l7 4a2 2 0 0 0 2 0l7-4A2 2 0 0 0 21 16Z"/><path d="m3.3 7 8.7 5 8.7-5"/><path d="M12 22V12"/></svg>';
                                }}
                            />
                        ) : (
                            <Package className="w-8 h-8 text-gray-500" />
                        )}
                    </div>
                    {rarityName && (
                        <Badge className={cn("text-xs font-bold", getRarityColorClass(item.rarity))}>
                            {rarityName}
                        </Badge>
                    )}
                </div>

                {/* 右侧：详细信息 */}
                <CardContent className="flex-1 p-4 flex flex-col justify-between">
                    <div>
                        {/* 名称和装备类型 */}
                        <div className="flex items-center gap-2 mb-1">
                            <h3 className="font-bold text-gray-900 line-clamp-1">{item.name}</h3>
                        </div>
                        {master && (
                            <p className="text-xs text-muted-foreground mb-2">
                                {t(getSlotTypeName(master.slotType))} · Lv.{master.equipmentLv}
                            </p>
                        )}

                        {/* 强化等级 */}
                        {equip.reinforcementLv > 0 && (
                            <div className="flex items-center gap-1 text-sm text-orange-600 font-medium mb-1">
                                <Sparkles className="w-3.5 h-3.5" />
                                <span>+{equip.reinforcementLv}</span>
                            </div>
                        )}

                        {/* 宝石槽信息 */}
                        {unlockedSpheres > 0 && (
                            <div className="flex items-center gap-1.5 mt-2">
                                <span className="text-xs text-muted-foreground">{t('ITEMS_SPHERES_LABEL')}</span>
                                <div className="flex gap-0.5">
                                    {[0, 1, 2, 3].map((index) => {
                                        const hasSphere = sphereIds[index] > 0;
                                        const isUnlocked = index < unlockedSpheres;
                                        return (
                                            <div
                                                key={index}
                                                className={cn(
                                                    "w-4 h-4 rounded-full border flex items-center justify-center",
                                                    hasSphere
                                                        ? "bg-purple-500 border-purple-600"
                                                        : isUnlocked
                                                            ? "bg-gray-200 border-gray-300"
                                                            : "bg-gray-100 border-gray-200 opacity-50"
                                                )}
                                            >
                                                {hasSphere && <CircleDot className="w-2.5 h-2.5 text-white" />}
                                            </div>
                                        );
                                    })}
                                </div>
                                <span className="text-xs text-muted-foreground">
                                    {sphereIds.length}/{unlockedSpheres}
                                </span>
                            </div>
                        )}

                        {/* 圣遗物等级 */}
                        {(equip.legendSacredTreasureLv > 0 || equip.matchlessSacredTreasureLv > 0) && (
                            <div className="flex gap-3 mt-2">
                                {equip.legendSacredTreasureLv > 0 && (
                                    <span className="text-xs text-amber-600">
                                        {t('ITEMS_LEGEND_SACRED_LV', [equip.legendSacredTreasureLv])}
                                    </span>
                                )}
                                {equip.matchlessSacredTreasureLv > 0 && (
                                    <span className="text-xs text-purple-600">
                                        {t('ITEMS_MATCHLESS_SACRED_LV', [equip.matchlessSacredTreasureLv])}
                                    </span>
                                )}
                            </div>
                        )}
                    </div>
                </CardContent>
            </div>
        </Card>
    );
}

// 未解除封印装备卡片
function SealedEquipmentCard({ item }: { item: SealedEquipmentItem }) {
    const t = useLocalizationStore((state) => state.t);
    const master = item.masterData;
    const rarityName = getRarityName(item.rarity);

    return (
        <Card className={cn(
            "overflow-hidden hover:shadow-md transition-all duration-200 border-2 border-gray-200"
        )}>
            <div className="flex flex-row items-stretch min-w-[220px] flex-1 bg-gradient-to-r from-gray-50 to-gray-100">
                {/* 左侧：图标 */}
                <div className="flex flex-col items-center justify-center p-4 min-w-[80px] border-r border-black/5">
                    <div className="w-12 h-12 rounded-lg bg-white/80 shadow-sm flex items-center justify-center overflow-hidden">
                        {master ? (
                            <img
                                src={EquipmentIconManager.getUrl(item.item.itemId)}
                                alt={item.name}
                                className="w-full h-full object-contain"
                                loading="lazy"
                                decoding="async"
                                onError={(e) => {
                                    (e.target as HTMLImageElement).style.display = 'none';
                                    (e.target as HTMLImageElement).parentElement!.innerHTML = '<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="w-6 h-6 text-gray-400"><path d="m7.5 4.27 9 5.15"/><path d="M21 8a2 2 0 0 0-1-1.73l-7-4a2 2 0 0 0-2 0l-7 4A2 2 0 0 0 3 8v8a2 2 0 0 0 1 1.73l7 4a2 2 0 0 0 2 0l7-4A2 2 0 0 0 21 16Z"/><path d="m3.3 7 8.7 5 8.7-5"/><path d="M12 22V12"/></svg>';
                                }}
                            />
                        ) : (
                            <Package className="w-6 h-6 text-gray-400" />
                        )}
                    </div>
                </div>

                {/* 右侧：信息 */}
                <CardContent className="flex-1 p-4 flex flex-col justify-center">
                    <div className="flex items-center gap-2 mb-1">
                        {rarityName && (
                            <Badge variant="outline" className="text-xs">
                                {rarityName}
                            </Badge>
                        )}
                    </div>
                    <h3 className="font-medium text-gray-700 text-sm line-clamp-1 mb-1">{item.name}</h3>
                    {master && (
                        <p className="text-xs text-muted-foreground">
                            {t(getSlotTypeName(master.slotType))}
                        </p>
                    )}
                    <div className="text-base font-bold text-gray-600 mt-1">
                        ×{item.item.itemCount.toLocaleString()}
                    </div>
                </CardContent>
            </div>
        </Card>
    );
}

export function EquipmentTab({ items, userEquipments }: EquipmentTabProps) {
    const { getTable } = useMasterStore();
    const t = useLocalizationStore((state) => state.t);
    const [equipmentTable, setEquipmentTable] = useState<Map<number, EquipmentMB> | null>(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const loadEquipmentData = async () => {
            try {
                setLoading(true);
                const table = await getTable<EquipmentMB>('EquipmentTable');
                const entries: [number, EquipmentMB][] = table.map((e) => [e.id, e]);
                const map = new Map<number, EquipmentMB>(entries);
                setEquipmentTable(map);
            } catch (error) {
                console.error('Failed to load equipment table:', error);
            } finally {
                setLoading(false);
            }
        };

        loadEquipmentData();
    }, [getTable]);

    // 分离已解除和未解除的装备
    const { unsealedItems, sealedItems } = useMemo(() => {
        if (!equipmentTable) return { unsealedItems: [], sealedItems: [] };

        const masterTables = { EquipmentTable: Array.from(equipmentTable.values()) };

        // 已解除封印的装备：来自 UserEquipmentDtoInfo，未装备在角色上
        const unsealed: UnsealedEquipmentItem[] = userEquipments
            .filter((equip) => !equip.characterGuid || equip.characterGuid === '')
            .map((equip) => {
                const masterData = equipmentTable.get(equip.equipmentId);
                return {
                    type: 'unsealed' as const,
                    equipment: equip,
                    masterData,
                    name: getItemName(ItemType.Equipment, equip.equipmentId, masterTables, t),
                    rarity: masterData?.rarityFlags ?? EquipmentRarityFlags.None,
                };
            })
            .sort((a, b) => b.rarity - a.rarity || (b.equipment.reinforcementLv - a.equipment.reinforcementLv));

        // 未解除封印的装备：来自 UserItemDtoInfo，ItemType.Equipment
        const sealed: SealedEquipmentItem[] = items
            .filter((item) => item.itemType === ItemType.Equipment && item.itemCount > 0)
            .map((item) => {
                const masterData = equipmentTable.get(item.itemId);
                return {
                    type: 'sealed' as const,
                    item,
                    masterData,
                    name: getItemName(ItemType.Equipment, item.itemId, masterTables, t),
                    rarity: masterData?.rarityFlags ?? EquipmentRarityFlags.None,
                };
            })
            .sort((a, b) => b.rarity - a.rarity);

        return { unsealedItems: unsealed, sealedItems: sealed };
    }, [items, userEquipments, equipmentTable, t]);

    if (loading) {
        return (
            <div className="flex items-center justify-center py-16">
                <div className="animate-spin rounded-full h-8 w-8 border-b-2 border-primary"></div>
            </div>
        );
    }

    const totalCount = unsealedItems.length + sealedItems.length;

    if (totalCount === 0) {
        return (
            <div className="flex flex-col items-center justify-center py-16 text-muted-foreground">
                <div className="text-6xl mb-4 opacity-20">⚔️</div>
                <p>{t('ITEMS_EMPTY_EQUIPMENT')}</p>
            </div>
        );
    }

    return (
        <div className="space-y-8">
            {/* 已解除封印的装备 */}
            {unsealedItems.length > 0 && (
                <div>
                    <div className="flex items-center gap-2 mb-4">
                        <Sparkles className="w-5 h-5 text-yellow-500" />
                        <h3 className="text-lg font-semibold">{t('ITEMS_UNSEALED')}</h3>
                        <Badge variant="secondary">{unsealedItems.length}</Badge>
                    </div>
                    <div className="flex flex-wrap gap-4">
                        {unsealedItems.map((item) => (
                            <UnsealedEquipmentCard key={item.equipment.guid} item={item} />
                        ))}
                    </div>
                </div>
            )}

            {/* 未解除封印的装备 */}
            {sealedItems.length > 0 && (
                <div>
                    <div className="flex items-center gap-2 mb-4">
                        <Package className="w-5 h-5 text-gray-500" />
                        <h3 className="text-lg font-semibold">{t('ITEMS_SEALED')}</h3>
                        <Badge variant="secondary">{t('ITEMS_COUNT_PCS', [sealedItems.reduce((sum, item) => sum + item.item.itemCount, 0)])}</Badge>
                    </div>
                    <div className="flex flex-wrap gap-4">
                        {sealedItems.map((item) => (
                            <SealedEquipmentCard key={`${item.item.itemId}-${item.item.itemCount}`} item={item} />
                        ))}
                    </div>
                </div>
            )}
        </div>
    );
}
