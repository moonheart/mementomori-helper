import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
import { Badge } from '@/components/ui/badge';
import { Button } from '@/components/ui/button';
import {
    UIEquipment,
    getRarityColor,
    getRarityName,
    getSlotIcon,
    getSlotName,
    formatStatName,
    formatStatValue,
    getRuneSlotUsage,
    canEnhance
} from '@/lib/equipmentUtils';
import { EquipmentIconManager, SphereIconManager } from '@/lib/asset-manager';
import { Swords, TrendingUp, XCircle } from 'lucide-react';
import { useTranslation } from '@/hooks/useTranslation';

interface EquipmentCardProps {
    equipment: UIEquipment;
    onViewDetails?: (equipment: UIEquipment) => void;
    onEnhance?: (equipment: UIEquipment) => void;
    onEquip?: (equipment: UIEquipment) => void;
    onUnequip?: (equipment: UIEquipment) => void;
}

export function EquipmentCard({
    equipment,
    onViewDetails,
    onEnhance,
    onEquip,
    onUnequip
}: EquipmentCardProps) {
    const { t } = useTranslation();
    const runeUsage = getRuneSlotUsage(equipment);
    const rarityName = getRarityName(equipment.rarity);

    return (
        <Card
            className="hover:shadow-lg transition-all cursor-pointer group relative overflow-hidden"
            onClick={() => onViewDetails?.(equipment)}
        >
            {/* 套装标识 */}
            {equipment.setName && (
                <div className="absolute top-2 right-2 z-10">
                    <Badge variant="secondary">
                        {equipment.setName}
                    </Badge>
                </div>
            )}

            <CardHeader className="pb-3">
                <div className="flex items-start gap-3">
                    <div className="w-16 h-16 rounded-lg bg-muted flex items-center justify-center overflow-hidden border-2 border-border/50 shrink-0">
                        <img
                            src={EquipmentIconManager.getUrl(equipment.equipmentId)}
                            alt={equipment.name}
                            className="w-full h-full object-contain"
                            onError={(e) => {
                                (e.target as HTMLImageElement).style.display = 'none';
                                (e.target as HTMLImageElement).parentElement!.innerHTML = getSlotIcon(equipment.slotType);
                            }}
                        />
                    </div>
                    <div className="flex-1 min-w-0">
                        <CardTitle className="text-lg truncate">
                            {equipment.name}
                        </CardTitle>
                        <div className="mt-1 flex flex-wrap items-center gap-1.5">
                            <span className="text-sm text-muted-foreground">{t(getSlotName(equipment.slotType))}</span>
                            <Badge className={`${getRarityColor(equipment.rarity)} text-white text-xs px-1.5 py-0`}>
                                {rarityName}
                            </Badge>
                            <Badge variant="outline" className="text-xs px-1.5 py-0">
                                Lv.{equipment.level}
                            </Badge>
                        </div>
                        {equipment.equippedByName && (
                            <div className="text-sm text-green-600 mt-0.5">{t('EQUIPMENT_EQUIPPED_BY', [equipment.equippedByName])}</div>
                        )}
                    </div>
                </div>
            </CardHeader>

            <CardContent className="space-y-3">
                {/* 战斗力 */}
                <div className="flex items-center justify-between p-2 bg-gradient-to-r from-blue-50 to-purple-50 dark:from-blue-950 dark:to-purple-950 rounded-lg">
                    <span className="text-sm font-medium flex items-center gap-1">
                        <Swords className="w-4 h-4" />
                        {t('[RankingGuildBattlePowerLabel]')}
                    </span>
                    <span className="text-lg font-bold text-blue-600 dark:text-blue-400">
                        {equipment.power.toLocaleString()}
                    </span>
                </div>

                {/* 强化等级 */}
                <div className="grid grid-cols-3 gap-2 text-xs">
                    <div className="flex flex-col items-center p-1.5 bg-muted rounded">
                        <span className="text-muted-foreground">{t('[CommonReinforcementStrengthenLabel]')}</span>
                        <span className="font-semibold">{equipment.reinforcementLv || 0}</span>
                    </div>
                    <div className="flex flex-col items-center p-1.5 bg-muted rounded">
                        <span className="text-muted-foreground">{t('EQUIPMENT_SACRED')}</span>
                        <span className="font-semibold text-yellow-600">Lv.{equipment.legendSacredTreasureLv || 0}</span>
                    </div>
                    <div className="flex flex-col items-center p-1.5 bg-muted rounded">
                        <span className="text-muted-foreground">{t('EQUIPMENT_MAGIC')}</span>
                        <span className="font-semibold text-purple-600">Lv.{equipment.matchlessSacredTreasureLv || 0}</span>
                    </div>
                </div>

                {/* 基础属性和符石插槽 - 左右布局 */}
                {(equipment.master?.battleParameterChangeInfo || runeUsage.total > 0) && (
                    <div className="flex gap-2">
                        {/* 基础属性 */}
                        {equipment.master?.battleParameterChangeInfo && (
                            <div className="flex-1 space-y-1">
                                <div className="text-xs text-muted-foreground font-medium">{t('[CharacterEquipmentBasicEffect]')}</div>
                                <div className="flex justify-between text-xs bg-muted/50 px-2 py-1 rounded">
                                    <span className="text-muted-foreground truncate">
                                        {t(formatStatName(equipment.master.battleParameterChangeInfo.battleParameterType))}
                                    </span>
                                    <span className="font-semibold ml-1">
                                        {formatStatValue(
                                            equipment.master.battleParameterChangeInfo.battleParameterType,
                                            equipment.master.battleParameterChangeInfo.value
                                        )}
                                    </span>
                                </div>
                            </div>
                        )}

                        {/* 符石插槽 */}
                        {runeUsage.total > 0 && (
                            <div className={`space-y-1 ${equipment.master?.battleParameterChangeInfo ? 'w-auto' : 'flex-1'}`}>
                                <div className="text-xs text-muted-foreground font-medium">{t('[CommonSphereLabel]')}</div>
                                <div className="flex items-center gap-1">
                                    {[...Array(runeUsage.total)].map((_, index) => {
                                        const categoryId = equipment.sphereCategoryIds?.[index];
                                        const hasSphere = index < runeUsage.used && categoryId !== undefined;
                                        return (
                                            <div
                                                key={index}
                                                className={`w-5 h-5 rounded border-2 flex items-center justify-center overflow-hidden ${hasSphere
                                                    ? 'border-purple-500 bg-purple-100 dark:bg-purple-900'
                                                    : 'border-gray-300 bg-gray-100 dark:bg-gray-800'
                                                    }`}
                                            >
                                                {hasSphere ? (
                                                    <img
                                                        src={SphereIconManager.getTinyUrl(categoryId)}
                                                        alt={t('[CommonSphereLabel]')}
                                                        className="w-4 h-4 object-contain"
                                                        onError={(e) => {
                                                            (e.target as HTMLImageElement).style.display = 'none';
                                                        }}
                                                    />
                                                ) : (
                                                    <span className="text-[10px] text-gray-400">◇</span>
                                                )}
                                            </div>
                                        );
                                    })}
                                </div>
                            </div>
                        )}
                    </div>
                )}

                {/* 操作按钮 */}
                <div className="grid grid-cols-2 gap-2 pt-2">
                    {equipment.characterGuid ? (
                        <Button
                            variant="outline"
                            size="sm"
                            className="w-full"
                            onClick={(e) => {
                                e.stopPropagation();
                                onUnequip?.(equipment);
                            }}
                        >
                            <XCircle className="w-4 h-4 mr-1" />
                            {t('[EquipmentRemoveButton]')}
                        </Button>
                    ) : (
                        <Button
                            variant="default"
                            size="sm"
                            className="w-full"
                            onClick={(e) => {
                                e.stopPropagation();
                                onEquip?.(equipment);
                            }}
                        >
                            {t('[CommonEquipmentLabel]')}
                        </Button>
                    )}

                    {canEnhance(equipment) && (
                        <Button
                            variant="secondary"
                            size="sm"
                            className="w-full"
                            onClick={(e) => {
                                e.stopPropagation();
                                onEnhance?.(equipment);
                            }}
                        >
                            <TrendingUp className="w-4 h-4 mr-1" />
                            {t('EQUIPMENT_ENHANCE_BTN')}
                        </Button>
                    )}
                </div>
            </CardContent>
        </Card>
    );
}
