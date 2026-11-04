import React from 'react';
import { Card, CardContent, CardHeader, CardTitle, CardDescription } from '@/components/ui/card';
import { Switch } from '@/components/ui/switch';
import { Label } from '@/components/ui/label';
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from '@/components/ui/select';
import { Input } from '@/components/ui/input';
import { Button } from '@/components/ui/button';
import { Badge } from '@/components/ui/badge';
import { Plus, X, Swords, Mountain } from 'lucide-react';
import { useItemName } from '@/hooks/useItemName';
import { ItemType } from '@/api/generated/itemType';
import { PvpOption } from '@/api/generated/pvpOption';
import { DungeonBattleConfig } from '@/api/generated/dungeonBattleConfig';
import { TargetSelectStrategy } from '@/api/generated/targetSelectStrategy';
import { CharacterFilterStrategy } from '@/api/generated/characterFilterStrategy';
import { useTranslation } from '@/hooks/useTranslation';

interface BattleSectionProps {
    battleLeague: PvpOption;
    legendLeague: PvpOption;
    dungeonBattle: DungeonBattleConfig;
    onUpdatePvp: (key: 'battleLeague' | 'legendLeague', config: PvpOption) => void;
    onUpdateDungeon: (config: DungeonBattleConfig) => void;
}

export function BattleSection({
    battleLeague,
    legendLeague,
    dungeonBattle,
    onUpdatePvp,
    onUpdateDungeon,
}: BattleSectionProps) {
   const { getItemName } = useItemName();
   const { t } = useTranslation();

   const getStrategyText = (strategy: TargetSelectStrategy) => {
       switch (strategy) {
           case TargetSelectStrategy.Random:
               return t('SETTINGS_BATTLE_STRATEGY_RANDOM');
           case TargetSelectStrategy.LowestBattlePower:
               return t('SETTINGS_BATTLE_STRATEGY_LOWEST_BP');
           case TargetSelectStrategy.HighestBattlePower:
               return t('SETTINGS_BATTLE_STRATEGY_HIGHEST_BP');
           default:
               return t('SETTINGS_BATTLE_STRATEGY_RANDOM');
       }
   };

   const renderPvpSettings = (title: string, config: PvpOption, type: 'battleLeague' | 'legendLeague') => {
        const handleStrategyChange = (val: string) => {
            onUpdatePvp(type, { ...config, selectStrategy: parseInt(val) });
        };

        const handleExcludeIdsChange = (val: string) => {
            const ids = val.split(',').map(id => parseInt(id.trim())).filter(id => !isNaN(id));
            onUpdatePvp(type, { ...config, excludePlayerIds: ids });
        };

        const removeFilter = (index: number) => {
            const newFilters = [...config.characterFilters];
            newFilters.splice(index, 1);
            onUpdatePvp(type, { ...config, characterFilters: newFilters });
        };

        const addFilter = () => {
            const newFilters = [...config.characterFilters, { characterId: 0, filterStrategy: CharacterFilterStrategy.Character }];
            onUpdatePvp(type, { ...config, characterFilters: newFilters });
        };

        return (
            <Card>
                <CardHeader>
                    <div className="flex items-center space-x-2">
                        <Swords className="h-5 w-5 text-primary" />
                        <CardTitle>{title}</CardTitle>
                    </div>
                    <CardDescription>{t('SETTINGS_BATTLE_PVP_DESC')}</CardDescription>
                </CardHeader>
                <CardContent className="space-y-4">
                    <div className="space-y-2">
                        <Label>{t('SETTINGS_BATTLE_OPPONENT_STRATEGY')}</Label>
                        <Select
                            value={config.selectStrategy?.toString() ?? TargetSelectStrategy.Random.toString()}
                            onValueChange={handleStrategyChange}
                        >
                            <SelectTrigger>
                                <SelectValue />
                            </SelectTrigger>
                            <SelectContent>
                                <SelectItem value={TargetSelectStrategy.Random.toString()}>{t('SETTINGS_BATTLE_STRATEGY_RANDOM')}</SelectItem>
                                <SelectItem value={TargetSelectStrategy.LowestBattlePower.toString()}>{t('SETTINGS_BATTLE_STRATEGY_LOWEST_BP')}</SelectItem>
                                <SelectItem value={TargetSelectStrategy.HighestBattlePower.toString()}>{t('SETTINGS_BATTLE_STRATEGY_HIGHEST_BP')}</SelectItem>
                            </SelectContent>
                        </Select>
                    </div>

                    <div className="space-y-2">
                        <Label>{t('SETTINGS_BATTLE_EXCLUDE_PLAYER_IDS')}</Label>
                        <Input
                            value={(config.excludePlayerIds || []).join(', ')}
                            onChange={(e) => handleExcludeIdsChange(e.target.value)}
                            placeholder={t('SETTINGS_BATTLE_EXCLUDE_PLACEHOLDER')}
                        />
                    </div>

                    <div className="space-y-2">
                        <div className="flex items-center justify-between">
                            <Label>{t('SETTINGS_BATTLE_CHARACTER_FILTER')}</Label>
                            <Button variant="outline" size="sm" onClick={addFilter}>
                                <Plus className="mr-2 h-4 w-4" />
                                {t('SETTINGS_BATTLE_ADD_CHARACTER')}
                            </Button>
                        </div>
                        <div className="flex flex-wrap gap-2 pt-1">
                            {(config.characterFilters || []).map((filter, idx) => (
                                <Badge key={idx} variant="secondary" className="flex items-center space-x-1 py-1.5 px-3">
                                    <span>{getItemName(ItemType.Character, filter.characterId)}</span>
                                    <X
                                        className="ml-2 h-3 w-3 cursor-pointer hover:text-destructive"
                                        onClick={() => removeFilter(idx)}
                                    />
                                </Badge>
                            ))}
                            {(config.characterFilters || []).length === 0 && (
                                <p className="text-sm text-muted-foreground italic">{t('SETTINGS_BATTLE_NO_CHARACTER_FILTER')}</p>
                            )}
                        </div>
                    </div>
                </CardContent>
            </Card>
        );
    };

    return (
        <div className="space-y-6">
            <div className="grid gap-6 md:grid-cols-2">
                {renderPvpSettings(t('[LockEquipmentDeckTypeLeague]'), battleLeague, 'battleLeague')}
                {renderPvpSettings(t('SETTINGS_BATTLE_LEGEND_TITLE'), legendLeague, 'legendLeague')}
            </div>

            <Card>
                <CardHeader>
                    <div className="flex items-center space-x-2">
                        <Mountain className="h-5 w-5 text-primary" />
                        <CardTitle>{t('[CommonHeaderDungeonBattleLabel]')}</CardTitle>
                    </div>
                    <CardDescription>{t('SETTINGS_BATTLE_DUNGEON_DESC')}</CardDescription>
                </CardHeader>
                <CardContent className="space-y-6">
                    <div className="flex items-center justify-between space-x-2 rounded-lg border p-3">
                        <Label className="flex flex-col space-y-1">
                            <span>{t('SETTINGS_BATTLE_PREFER_CHEST')}</span>
                            <span className="font-normal text-xs text-muted-foreground text-wrap">{t('SETTINGS_BATTLE_PREFER_CHEST_DESC')}</span>
                        </Label>
                        <Switch
                            checked={dungeonBattle.preferTreasureChest}
                            onCheckedChange={(checked) => onUpdateDungeon({ ...dungeonBattle, preferTreasureChest: checked })}
                        />
                    </div>

                    <div className="flex items-center justify-between space-x-2 rounded-lg border p-3">
                        <Label className="flex flex-col space-y-1">
                            <span>{t('SETTINGS_BATTLE_AUTO_UNEQUIP')}</span>
                            <span className="font-normal text-xs text-muted-foreground text-wrap">{t('SETTINGS_BATTLE_AUTO_UNEQUIP_DESC')}</span>
                        </Label>
                        <Switch
                            checked={dungeonBattle.autoRemoveEquipment}
                            onCheckedChange={(checked) => onUpdateDungeon({ ...dungeonBattle, autoRemoveEquipment: checked })}
                        />
                    </div>

                    <div className="space-y-2">
                        <Label>{t('SETTINGS_BATTLE_MAX_RECOVERY')}</Label>
                        <Input
                            type="number"
                            min={0}
                            max={99}
                            value={dungeonBattle.maxUseRecoveryItem}
                            onChange={(e) => onUpdateDungeon({ ...dungeonBattle, maxUseRecoveryItem: parseInt(e.target.value) || 0 })}
                        />
                    </div>

                    <div className="space-y-2">
                        <Label>{t('SETTINGS_BATTLE_SHOP_TARGETS')}</Label>
                        <div className="grid gap-2">
                            {(dungeonBattle.shopTargetItems || []).map((item, idx) => (
                                <div key={idx} className="flex items-center justify-between rounded border p-2 text-sm">
                                    <span>{getItemName(item.itemType, item.itemId)} | {t('SETTINGS_BATTLE_MIN_DISCOUNT', [item.minDiscountPercent])}</span>
                                    <Button
                                        variant="ghost"
                                        size="sm"
                                        onClick={() => {
                                            const next = [...dungeonBattle.shopTargetItems];
                                            next.splice(idx, 1);
                                            onUpdateDungeon({ ...dungeonBattle, shopTargetItems: next });
                                        }}
                                    >
                                        <X className="h-4 w-4" />
                                    </Button>
                                </div>
                            ))}
                            <Button
                                variant="outline"
                                size="sm"
                                className="w-full"
                                onClick={() => {
                                    const next = [...(dungeonBattle.shopTargetItems || []), { itemId: 1, itemType: 34, minDiscountPercent: 0, itemCount: 1 }];
                                    onUpdateDungeon({ ...dungeonBattle, shopTargetItems: next });
                                }}
                            >
                                <Plus className="mr-2 h-4 w-4" />
                                {t('SETTINGS_BATTLE_ADD_SHOP_TARGET')}
                            </Button>
                        </div>
                    </div>
                </CardContent>
            </Card>
        </div>
    );
}
