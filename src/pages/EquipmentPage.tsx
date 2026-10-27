import { useState, useMemo, useEffect } from 'react';
import { EquipmentCard } from '@/components/equipment/EquipmentCard';
import { EquipmentFilters } from '@/components/equipment/EquipmentFilters';
import { 
    UIEquipment, 
    sortEquipment, 
    calculateEquipmentPower 
} from '@/lib/equipmentUtils';
import { Book, Loader2 } from 'lucide-react';
import { Alert, AlertDescription } from '@/components/ui/alert';
import { useAccountStore } from '@/store/accountStore';
import { ortegaApi } from '@/api/ortega-client';
import { useMasterStore } from '@/store/masterStore';
import { useLocalizationStore } from '@/store/localization-store';
import { 
    EquipmentMB, 
    EquipmentSetMB, 
    UserEquipmentDtoInfo,
    EquipmentSlotType,
    EquipmentRarityFlags,
    CharacterMB,
    SphereMB
} from '@/api/generated';

export function EquipmentPage() {
    const { currentAccountId } = useAccountStore();
    const t = useLocalizationStore(state => state.t);
    const getTable = useMasterStore(state => state.getTable);

    // 状态
    const [loading, setLoading] = useState(false);
    const [userEquipments, setUserEquipments] = useState<UserEquipmentDtoInfo[]>([]);
    const [equipmentMasterMap, setEquipmentMasterMap] = useState<Record<number, EquipmentMB>>({});
    const [setMasterMap, setSetMasterMap] = useState<Record<number, EquipmentSetMB>>({});
    const [characterNameMap, setCharacterNameMap] = useState<Record<string, string>>({});
    const [sphereMasterMap, setSphereMasterMap] = useState<Record<number, SphereMB>>({});

    // 筛选状态
    const [searchQuery, setSearchQuery] = useState('');
    const [selectedSlot, setSelectedSlot] = useState<EquipmentSlotType | 'all'>('all');
    const [selectedRarities, setSelectedRarities] = useState<EquipmentRarityFlags[]>([]);
    const [equippedFilter, setEquippedFilter] = useState<'all' | 'equipped' | 'unequipped'>('all');
    const [setFilter, setSetFilter] = useState<string | 'all'>('all');
    const [sortBy, setSortBy] = useState<'level' | 'rarity' | 'enhance' | 'power'>('power');

    // 获取数据
    useEffect(() => {
        if (!currentAccountId) return;

        const fetchData = async () => {
            setLoading(true);
            try {
                // 1. 获取用户装备和角色信息
                const userData = await ortegaApi.user.getUserData({});
                const equipments = userData.userSyncData?.userEquipmentDtoInfos || [];
                setUserEquipments(equipments);

                // 构建角色 Guid 到名称的映射
                const chars = userData.userSyncData?.userCharacterDtoInfos || [];
                const charNameMap: Record<string, string> = {};
                
                // 获取 CharacterMaster 来获取名称
                const charMasterTable = await getTable<CharacterMB>('CharacterTable');
                const charMasterMap: Record<number, CharacterMB> = {};
                charMasterTable.forEach(m => { charMasterMap[m.id] = m; });

                chars.forEach(c => {
                    const master = charMasterMap[c.characterId];
                    if (master) {
                        charNameMap[c.guid] = t(master.nameKey);
                    }
                });
                setCharacterNameMap(charNameMap);

                // 2. 获取 Master 数据
                const eqMasterTable = await getTable<EquipmentMB>('EquipmentTable');
                const eqMasterMap: Record<number, EquipmentMB> = {};
                eqMasterTable.forEach((m) => {
                    eqMasterMap[m.id] = m;
                });
                setEquipmentMasterMap(eqMasterMap);

                const setTable = await getTable<EquipmentSetMB>('EquipmentSetTable');
                const setMap: Record<number, EquipmentSetMB> = {};
                setTable.forEach((m) => {
                    setMap[m.id] = m;
                });
                setSetMasterMap(setMap);

                // 3. 获取符石 Master 数据
                const sphereTable = await getTable<SphereMB>('SphereTable');
                const sphereMap: Record<number, SphereMB> = {};
                sphereTable.forEach((m) => {
                    sphereMap[m.id] = m;
                });
                setSphereMasterMap(sphereMap);

            } catch (error) {
                console.error('Failed to fetch equipment data:', error);
            } finally {
                setLoading(false);
            }
        };

        fetchData();
    }, [currentAccountId, getTable, t]);

    // 合并数据并计算
    const allEquipments: UIEquipment[] = useMemo(() => {
        return userEquipments.map(eq => {
            const master = equipmentMasterMap[eq.equipmentId];
            const setMaster = master?.equipmentSetId ? setMasterMap[master.equipmentSetId] : undefined;

            // 获取每个插槽的符石 categoryId
            const sphereCategoryIds = [
                eq.sphereId1 ? sphereMasterMap[eq.sphereId1]?.categoryId : undefined,
                eq.sphereId2 ? sphereMasterMap[eq.sphereId2]?.categoryId : undefined,
                eq.sphereId3 ? sphereMasterMap[eq.sphereId3]?.categoryId : undefined,
                eq.sphereId4 ? sphereMasterMap[eq.sphereId4]?.categoryId : undefined,
            ];

            return {
                ...eq,
                master,
                name: master ? t(master.nameKey) : t('CHARACTER_DETAIL_EQUIPMENT_FALLBACK', [eq.equipmentId]),
                rarity: master?.rarityFlags || EquipmentRarityFlags.None,
                slotType: master?.slotType || EquipmentSlotType.Weapon,
                level: master?.equipmentLv || 0,
                setName: setMaster ? t(setMaster.nameKey) : undefined,
                equippedByName: eq.characterGuid ? characterNameMap[eq.characterGuid] : undefined,
                power: calculateEquipmentPower(eq, master),
                sphereCategoryIds
            };
        });
    }, [userEquipments, equipmentMasterMap, setMasterMap, sphereMasterMap, characterNameMap, t]);

    // 筛选和排序装备
    const filteredAndSortedEquipment = useMemo(() => {
        const filtered = allEquipments.filter(eq => {
            // 搜索筛选
            if (searchQuery && !eq.name.toLowerCase().includes(searchQuery.toLowerCase())) {
                return false;
            }

            // 部位筛选
            if (selectedSlot !== 'all' && eq.slotType !== selectedSlot) {
                return false;
            }

            // 稀有度筛选
            if (selectedRarities.length > 0 && !selectedRarities.includes(eq.rarity)) {
                return false;
            }

            // 装备状态筛选
            if (equippedFilter === 'equipped' && !eq.characterGuid) {
                return false;
            }
            if (equippedFilter === 'unequipped' && eq.characterGuid) {
                return false;
            }

            // 套装筛选
            if (setFilter !== 'all') {
                if (setFilter === 'none' && eq.setName) {
                    return false;
                }
                if (setFilter !== 'none' && eq.setName !== setFilter) {
                    return false;
                }
            }

            return true;
        });

        // 排序
        return sortEquipment(filtered, sortBy);
    }, [allEquipments, searchQuery, selectedSlot, selectedRarities, equippedFilter, setFilter, sortBy]);

    // 重置所有筛选
    const handleReset = () => {
        setSearchQuery('');
        setSelectedSlot('all');
        setSelectedRarities([]);
        setEquippedFilter('all');
        setSetFilter('all');
    };

    // 装备操作处理
    const handleViewDetails = (equipment: UIEquipment) => {
        console.log('查看装备详情:', equipment);
    };

    const handleEnhance = (equipment: UIEquipment) => {
        console.log('强化装备:', equipment);
    };

    const handleEquip = (equipment: UIEquipment) => {
        console.log('装备:', equipment);
    };

    const handleUnequip = (equipment: UIEquipment) => {
        console.log('卸下装备:', equipment);
    };

    if (!currentAccountId) {
        return (
            <div className="flex items-center justify-center h-[60vh] text-muted-foreground">
                {t('EQUIPMENT_SELECT_ACCOUNT')}
            </div>
        );
    }

    if (loading && allEquipments.length === 0) {
        return (
            <div className="flex flex-col items-center justify-center h-[60vh]">
                <Loader2 className="h-8 w-8 animate-spin text-primary mb-4" />
                <p className="text-muted-foreground">{t('EQUIPMENT_SYNCING')}</p>
            </div>
        );
    }

    return (
        <div className="space-y-6">
            {/* 筛选器 */}
            <EquipmentFilters
                searchQuery={searchQuery}
                onSearchChange={setSearchQuery}
                selectedSlot={selectedSlot}
                onSlotChange={setSelectedSlot}
                selectedRarities={selectedRarities}
                onRaritiesChange={setSelectedRarities}
                equippedFilter={equippedFilter}
                onEquippedFilterChange={setEquippedFilter}
                setFilter={setFilter}
                onSetFilterChange={setSetFilter}
                sortBy={sortBy}
                onSortChange={setSortBy}
                onReset={handleReset}
            />

            {/* 装备网格 */}
            {filteredAndSortedEquipment.length > 0 ? (
                <div className="grid gap-4 grid-cols-1 sm:grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 2xl:grid-cols-5 3xl:grid-cols-6">
                    {filteredAndSortedEquipment.map((equipment) => (
                        <EquipmentCard
                            key={equipment.guid}
                            equipment={equipment}
                            onViewDetails={handleViewDetails}
                            onEnhance={handleEnhance}
                            onEquip={handleEquip}
                            onUnequip={handleUnequip}
                        />
                    ))}
                </div>
            ) : (
                <div className="flex flex-col items-center justify-center py-16 px-4 text-center">
                    <div className="text-6xl mb-4 opacity-20">📦</div>
                    <h3 className="text-lg font-semibold mb-2">{t('EQUIPMENT_NO_MATCH')}</h3>
                    <p className="text-muted-foreground mb-4">
                        {t('EQUIPMENT_NO_MATCH_HINT')}
                    </p>
                    <button
                        onClick={handleReset}
                        className="text-primary hover:underline"
                    >
                        {t('EQUIPMENT_RESET_FILTERS')}
                    </button>
                </div>
            )}
        </div>
    );
}
