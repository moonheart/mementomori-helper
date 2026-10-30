import { Badge } from '@/components/ui/badge';
import { Button } from '@/components/ui/button';
import { Input } from '@/components/ui/input';
import { Tabs, TabsList, TabsTrigger } from '@/components/ui/tabs';
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from '@/components/ui/select';
import { EquipmentSlotType } from '@/api/generated/equipmentSlotType';
import { EquipmentRarityFlags } from '@/api/generated/equipmentRarityFlags';
import { Search, Filter, X } from 'lucide-react';
import { useState } from 'react';
import { getRarityColor, getRarityName, getSlotIcon, getSlotName } from '@/lib/equipmentUtils';
import { useTranslation } from '@/hooks/useTranslation';

type SortOption = 'level' | 'rarity' | 'enhance' | 'power';
type EquippedFilterOption = 'all' | 'equipped' | 'unequipped';

interface EquipmentFiltersProps {
    searchQuery: string;
    onSearchChange: (query: string) => void;
    selectedSlot: EquipmentSlotType | 'all';
    onSlotChange: (slot: EquipmentSlotType | 'all') => void;
    selectedRarities: EquipmentRarityFlags[];
    onRaritiesChange: (rarities: EquipmentRarityFlags[]) => void;
    equippedFilter: EquippedFilterOption;
    onEquippedFilterChange: (filter: EquippedFilterOption) => void;
    setFilter: string | 'all';
    onSetFilterChange: (setId: string | 'all') => void;
    sortBy: SortOption;
    onSortChange: (sort: SortOption) => void;
    onReset: () => void;
}

export function EquipmentFilters({
    searchQuery,
    onSearchChange,
    selectedSlot,
    onSlotChange,
    selectedRarities,
    onRaritiesChange,
    equippedFilter,
    onEquippedFilterChange,
    setFilter,
    onSetFilterChange,
    sortBy,
    onSortChange,
    onReset
}: EquipmentFiltersProps) {
    const { t } = useTranslation();
    const [showAdvanced, setShowAdvanced] = useState(false);

    const rarityOptions = [
        EquipmentRarityFlags.LR,
        EquipmentRarityFlags.UR,
        EquipmentRarityFlags.SSR,
        EquipmentRarityFlags.SR,
        EquipmentRarityFlags.R,
        EquipmentRarityFlags.S,
        EquipmentRarityFlags.A,
        EquipmentRarityFlags.B,
        EquipmentRarityFlags.C,
        EquipmentRarityFlags.D,
    ];

    const slotOptions = [
        EquipmentSlotType.Weapon,
        EquipmentSlotType.Sub,
        EquipmentSlotType.Gauntlet,
        EquipmentSlotType.Helmet,
        EquipmentSlotType.Armor,
        EquipmentSlotType.Shoes,
    ];

    const toggleRarity = (rarity: EquipmentRarityFlags) => {
        if (selectedRarities.includes(rarity)) {
            onRaritiesChange(selectedRarities.filter(r => r !== rarity));
        } else {
            onRaritiesChange([...selectedRarities, rarity]);
        }
    };

    const hasActiveFilters =
        searchQuery !== '' ||
        selectedSlot !== 'all' ||
        selectedRarities.length > 0 ||
        equippedFilter !== 'all' ||
        setFilter !== 'all';

    return (
        <div className="space-y-4">
            {/* 搜索和基础筛选 */}
            <div className="flex flex-col md:flex-row gap-4">
                {/* 搜索框 */}
                <div className="relative flex-1">
                    <Search className="absolute left-3 top-1/2 h-4 w-4 -translate-y-1/2 text-muted-foreground" />
                    <Input
                        placeholder={t('EQUIPMENT_FILTER_SEARCH')}
                        value={searchQuery}
                        onChange={(e) => onSearchChange(e.target.value)}
                        className="pl-10"
                    />
                    {searchQuery && (
                        <button
                            onClick={() => onSearchChange('')}
                            className="absolute right-3 top-1/2 -translate-y-1/2 text-muted-foreground hover:text-foreground"
                        >
                            <X className="h-4 w-4" />
                        </button>
                    )}
                </div>

                {/* 排序 */}
                <Select value={sortBy} onValueChange={(value: SortOption) => onSortChange(value)}>
                    <SelectTrigger className="w-full md:w-[180px]">
                        <SelectValue placeholder={t('EQUIPMENT_FILTER_SORT')} />
                    </SelectTrigger>
                    <SelectContent>
                        <SelectItem value="power">{t('[RankingGuildBattlePowerLabel]')}</SelectItem>
                        <SelectItem value="rarity">{t('[CharacterSortTypeRarity]')}</SelectItem>
                        <SelectItem value="level">{t('[RankingGuildLevelLabel]')}</SelectItem>
                        <SelectItem value="enhance">{t('[EquipmentInheritanceCheckboxReinforcementLevel]')}</SelectItem>
                    </SelectContent>
                </Select>

                {/* 高级筛选按钮 */}
                <Button
                    variant={showAdvanced ? "default" : "outline"}
                    onClick={() => setShowAdvanced(!showAdvanced)}
                    className="whitespace-nowrap"
                >
                    <Filter className="h-4 w-4 mr-2" />
                    {t('EQUIPMENT_FILTER_ADVANCED')}
                    {hasActiveFilters && (
                        <Badge variant="secondary" className="ml-2">
                            {[searchQuery && 1, selectedSlot !== 'all' && 1, selectedRarities.length, equippedFilter !== 'all' && 1, setFilter !== 'all' && 1].filter(Boolean).length}
                        </Badge>
                    )}
                </Button>
            </div>

            {/* 部位筛选 */}
            <Tabs value={selectedSlot.toString()} onValueChange={(value) => onSlotChange(value === 'all' ? 'all' : parseInt(value))}>
                <TabsList className="grid w-full grid-cols-7">
                    <TabsTrigger value="all">{t('[CommonAllLabel]')}</TabsTrigger>
                    {slotOptions.map(slot => (
                        <TabsTrigger key={slot} value={slot.toString()}>
                            <span className="mr-1">{getSlotIcon(slot)}</span>
                            <span className="hidden sm:inline">{t(getSlotName(slot))}</span>
                        </TabsTrigger>
                    ))}
                </TabsList>
            </Tabs>

            {/* 高级筛选面板 */}
            {showAdvanced && (
                <div className="border rounded-lg p-4 space-y-4 bg-muted/30">
                    {/* 稀有度筛选 */}
                    <div className="space-y-2">
                        <label className="text-sm font-medium">{t('EQUIPMENT_FILTER_RARITY_FILTER')}</label>
                        <div className="flex flex-wrap gap-2">
                            {rarityOptions.map(rarity => (
                                <Badge
                                    key={rarity}
                                    variant={selectedRarities.includes(rarity) ? "default" : "outline"}
                                    className={`cursor-pointer ${selectedRarities.includes(rarity)
                                            ? `${getRarityColor(rarity)} text-white`
                                            : 'hover:bg-muted'
                                        }`}
                                    onClick={() => toggleRarity(rarity)}
                                >
                                    {getRarityName(rarity)}
                                </Badge>
                            ))}
                        </div>
                    </div>

                    {/* 装备状态筛选 */}
                    <div className="space-y-2">
                        <label className="text-sm font-medium">{t('EQUIPMENT_FILTER_EQUIPMENT_STATUS')}</label>
                        <Tabs value={equippedFilter} onValueChange={(value: string) => onEquippedFilterChange(value as EquippedFilterOption)}>
                            <TabsList className="grid w-full grid-cols-3">
                                <TabsTrigger value="all">{t('[CommonAllLabel]')}</TabsTrigger>
                                <TabsTrigger value="equipped">{t('EQUIPMENT_FILTER_EQUIPPED')}</TabsTrigger>
                                <TabsTrigger value="unequipped">{t('[CommonNotEquippingLabel]')}</TabsTrigger>
                            </TabsList>
                        </Tabs>
                    </div>

                    {/* 套装筛选 */}
                    <div className="space-y-2">
                        <label className="text-sm font-medium">{t('EQUIPMENT_FILTER_SET_FILTER')}</label>
                        <Select value={setFilter} onValueChange={onSetFilterChange}>
                            <SelectTrigger>
                                <SelectValue placeholder={t('EQUIPMENT_FILTER_SELECT_SET')} />
                            </SelectTrigger>
                            <SelectContent>
                                <SelectItem value="all">{t('EQUIPMENT_FILTER_ALL_SETS')}</SelectItem>
                                <SelectItem value="none">{t('EQUIPMENT_FILTER_NO_SET')}</SelectItem>
                            </SelectContent>
                        </Select>
                    </div>

                    {/* 重置按钮 */}
                    {hasActiveFilters && (
                        <Button
                            variant="ghost"
                            onClick={onReset}
                            className="w-full"
                        >
                            <X className="h-4 w-4 mr-2" />
                            {t('EQUIPMENT_FILTER_RESET')}
                        </Button>
                    )}
                </div>
            )}
        </div>
    );
}
