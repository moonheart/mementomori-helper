import { useState, useEffect, useMemo } from 'react';
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from '@/components/ui/card';
import { Badge } from '@/components/ui/badge';
import { Button } from '@/components/ui/button';
import { Progress } from '@/components/ui/progress';
import { Tabs, TabsContent, TabsList, TabsTrigger } from '@/components/ui/tabs';
import { Alert, AlertDescription } from '@/components/ui/alert';
import { Dialog, DialogContent, DialogDescription, DialogHeader, DialogTitle } from '@/components/ui/dialog';
import {
    MapPin,
    Sparkles,
    Heart,
    Clock,
    Trophy,
    Users,
    Skull,
    Loader2,
    Zap,
    Flame,
    Droplets,
    Wind,
    Sun,
    Moon
} from 'lucide-react';
import { ortegaApi } from '@/api/ortega-client';
import { useMasterStore } from '@/store/masterStore';
import { useLocalizationStore } from '@/store/localization-store';
import { useAccountStore } from '@/store/accountStore';
import { useTimeManager } from '@/hooks/useTimeManager';
import { useItemName } from '@/hooks/useItemName';
import { CharacterIcon } from '@/components/character/CharacterIcon';
import {
    DungeonBattleGetDungeonBattleInfoResponse,
    DungeonBattleRelicMB,
    DungeonBattleGridMB,
    DungeonBattleGridType,
    CharacterMB,
    DungeonBattleRelicRarityType,
    DungeonBattleGetBattleGridDataResponse,
    DungeonBattleGuestMB,
    ElementType,
    CharacterRarityFlags,
    DungeonBattleGridState,
    UserCharacterInfo,
    BattleFieldCharacterGroupType
} from '@/api/generated';

export function TimeSpaceCavePage() {
    const [loading, setLoading] = useState(true);
    const [battleInfo, setBattleInfo] = useState<DungeonBattleGetDungeonBattleInfoResponse | null>(null);
    const [error, setError] = useState<string | null>(null);
    const [relicTable, setRelicTable] = useState<DungeonBattleRelicMB[]>([]);
    const [gridTable, setGridTable] = useState<DungeonBattleGridMB[]>([]);
    const [characterTable, setCharacterTable] = useState<CharacterMB[]>([]);
    const [guestTable, setGuestTable] = useState<DungeonBattleGuestMB[]>([]);
    const [refreshTimeLeft, setRefreshTimeLeft] = useState<string>('');

    // 石台详情弹窗状态
    const [selectedPlatform, setSelectedPlatform] = useState<typeof platformChoices[0] | null>(null);
    const [platformDetails, setPlatformDetails] = useState<DungeonBattleGetBattleGridDataResponse | null>(null);
    const [loadingDetails, setLoadingDetails] = useState(false);

    // 当前石台操作弹窗状态
    const [currentGridAction, setCurrentGridAction] = useState<typeof platformChoices[0] | null>(null);
    const [isActionSubmitting, setIsActionSubmitting] = useState(false);

    // 增援角色选择状态
    const [selectedGuestCharacterId, setSelectedGuestCharacterId] = useState<number | null>(null);

    const { currentAccountId, getCurrentUserSyncData } = useAccountStore();
    const timeManager = useTimeManager();
    const sync = useMasterStore(state => state.sync);
    const getTable = useMasterStore(state => state.getTable);
    const t = useLocalizationStore(state => state.t);
    const { getItemName } = useItemName();

    // 加载数据
    const loadData = async () => {
        try {
            setLoading(true);
            setError(null);

            // 确保 Master 数据已加载
            await sync();

            // 获取时空洞窟信息
            const response = await ortegaApi.dungeonBattle.getDungeonBattleInfo({});
            setBattleInfo(response);

            // 加载 Master 数据表
            const [relics, grids, characters, guests] = await Promise.all([
                getTable('DungeonBattleRelicTable'),
                getTable('DungeonBattleGridTable'),
                getTable('CharacterTable'),
                getTable('DungeonBattleGuestTable')
            ]);

            setRelicTable((relics as any) || []);
            setGridTable((grids as any) || []);
            setCharacterTable((characters as any) || []);
            setGuestTable((guests as any) || []);
        } catch (err) {
            console.error('Failed to load dungeon battle info:', err);
            setError(t('CAVE_LOAD_FAILED'));
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        if (!currentAccountId) {
            setLoading(false);
            return;
        }
        loadData();
    }, [currentAccountId]);

    // 更新刷新倒计时
    useEffect(() => {
        if (!battleInfo) return;

        const updateTimer = () => {
            const now = timeManager.getServerNowMs();
            const remaining = battleInfo.utcEndTimeStamp - now;
            if (remaining > 0) {
                setRefreshTimeLeft(timeManager.formatTimeSpan(remaining));
            } else {
                setRefreshTimeLeft(t('CAVE_REFRESHED'));
            }
        };

        updateTimer();
        const interval = setInterval(updateTimer, 1000);
        return () => clearInterval(interval);
    }, [battleInfo]);

    // 获取加护稀有度颜色
    const getBlessingTierColor = (tier: DungeonBattleRelicRarityType) => {
        const colors: Record<number, string> = {
            [DungeonBattleRelicRarityType.None]: 'bg-gray-500',
            [DungeonBattleRelicRarityType.R]: 'bg-blue-500',
            [DungeonBattleRelicRarityType.SR]: 'bg-purple-500',
            [DungeonBattleRelicRarityType.SSR]: 'bg-orange-500'
        };
        return colors[tier] || 'bg-gray-500';
    };

    // 获取石台状态名称
    const getGridStateName = (state?: DungeonBattleGridState) => {
        switch (state) {
            case DungeonBattleGridState.Done:
                return t('CAVE_STATE_DONE');
            case DungeonBattleGridState.Selected:
                return t('CAVE_STATE_SELECTED');
            case DungeonBattleGridState.Reward:
                return t('CAVE_STATE_WAIT_REWARD');
            case DungeonBattleGridState.SkipShop:
                return t('CAVE_STATE_SKIP_SHOP');
            default:
                return t('CAVE_STATE_UNKNOWN');
        }
    };

    // 处理当前石台动作
    const handleCurrentGridAction = async (action: string) => {
        if (!battleInfo) return;

        setIsActionSubmitting(true);
        try {
            const currentGridGuid = battleInfo.userDungeonDtoInfo?.currentGridGuid;
            const currentTermId = battleInfo.currentTermId;

            switch (action) {
                case 'battle': {
                    // 获取当前队伍角色 GUID 列表
                    const characterGuids = battleInfo.userDungeonBattleCharacterDtoInfos?.map(c => c.guid) || [];
                    if (characterGuids.length === 0) {
                        console.error('No characters available for battle');
                        break;
                    }

                    const res = await ortegaApi.dungeonBattle.execBattle({
                        dungeonGridGuid: currentGridGuid,
                        currentTermId,
                        characterGuids
                    });

                    // 判断战斗结果
                    const isWin = res.battleSimulationResult?.battleEndInfo?.winGroupType === BattleFieldCharacterGroupType.Attacker;
                    if (isWin) {
                        console.log('战斗胜利');
                    } else {
                        console.log('战斗失败');
                    }
                    break;
                }
                case 'recovery':
                    await ortegaApi.dungeonBattle.execRecovery({
                        dungeonGridGuid: currentGridGuid,
                        currentTermId,
                        isHealed: true
                    });
                    break;
                case 'reinforce':
                    await ortegaApi.dungeonBattle.execReinforceRelic({
                        dungeonGridGuid: currentGridGuid,
                        currentTermId
                    });
                    break;
                case 'revive':
                    await ortegaApi.dungeonBattle.execRevive({
                        dungeonGridGuid: currentGridGuid,
                        currentTermId,
                        isRevived: true
                    });
                    break;
                case 'leaveShop':
                    await ortegaApi.dungeonBattle.leaveShop({
                        dungeonGridGuid: currentGridGuid,
                        currentTermId
                    });
                    break;
            }

            // 刷新数据
            await loadData();
            setCurrentGridAction(null);
        } catch (err) {
            console.error('Action failed:', err);
        } finally {
            setIsActionSubmitting(false);
        }
    };

    // 处理选择加护奖励
    const handleSelectRelicReward = async (relicId: number) => {
        if (!battleInfo) return;

        setIsActionSubmitting(true);
        try {
            await ortegaApi.dungeonBattle.rewardBattleReceiveRelic({
                dungeonGridGuid: battleInfo.userDungeonDtoInfo?.currentGridGuid,
                currentTermId: battleInfo.currentTermId,
                selectedRelicId: relicId
            });

            // 刷新数据
            await loadData();
            setCurrentGridAction(null);
        } catch (err) {
            console.error('Failed to select relic reward:', err);
        } finally {
            setIsActionSubmitting(false);
        }
    };

    // 处理石台点击 - 根据状态决定打开哪个弹窗
    const handlePlatformClick = async (platform: typeof platformChoices[0]) => {
        const currentGridGuid = battleInfo?.userDungeonDtoInfo?.currentGridGuid;
        const currentState = battleInfo?.userDungeonDtoInfo?.currentGridState;

        // 如果点击的是当前石台
        if (platform.guid === currentGridGuid) {
            // 如果当前石台已完成（Done状态），则打开选择弹窗（虽然通常不应该点击已完成石台）
            if (currentState === DungeonBattleGridState.Done) {
                setSelectedPlatform(platform);
                setPlatformDetails(null);
                return;
            }
            // 否则打开当前石台操作弹窗
            setCurrentGridAction(platform);
            return;
        }

        // 如果当前状态不是 Done，点击其他石台无效（需要先处理当前石台）
        if (currentState !== DungeonBattleGridState.Done) {
            // 提示用户先处理当前石台
            return;
        }

        // Done 状态下，点击可选择石台打开选择弹窗
        setSelectedPlatform(platform);
        setPlatformDetails(null);

        // 如果是战斗石台，获取详细数据
        if (isBattleGrid(platform.type) && battleInfo) {
            setLoadingDetails(true);
            try {
                const data = await ortegaApi.dungeonBattle.getBattleGridData({
                    dungeonGridGuid: platform.guid,
                    currentTermId: battleInfo.currentTermId
                });
                setPlatformDetails(data);
            } catch (err) {
                console.error('Failed to load platform details:', err);
            } finally {
                setLoadingDetails(false);
            }
        }
    };

    // 获取石台类型颜色
    const getGridTypeColor = (type: DungeonBattleGridType) => {
        if (type === DungeonBattleGridType.EventBattleNormal ||
            type === DungeonBattleGridType.EventBattleElite ||
            type === DungeonBattleGridType.EventBattleSpecial) {
            return 'text-yellow-500';
        }
        if (type === DungeonBattleGridType.BattleBoss || type === DungeonBattleGridType.BattleBossNoRelic) {
            return 'text-red-500';
        }
        if (type === DungeonBattleGridType.BattleElite) {
            return 'text-orange-500';
        }
        return 'text-green-500';
    };


    // 获取属性图标和颜色
    const getElementInfo = (elementType: ElementType) => {
        switch (elementType) {
            case ElementType.Blue:
                return { icon: Droplets, color: 'text-blue-500', bgColor: 'bg-blue-500/10', nameKey: '[ElementTypeBlue]' };
            case ElementType.Red:
                return { icon: Flame, color: 'text-red-500', bgColor: 'bg-red-500/10', nameKey: '[ElementTypeRed]' };
            case ElementType.Green:
                return { icon: Wind, color: 'text-green-500', bgColor: 'bg-green-500/10', nameKey: '[ElementTypeGreen]' };
            case ElementType.Yellow:
                return { icon: Zap, color: 'text-yellow-500', bgColor: 'bg-yellow-500/10', nameKey: '[ElementTypeYellow]' };
            case ElementType.Light:
                return { icon: Sun, color: 'text-amber-400', bgColor: 'bg-amber-400/10', nameKey: '[ElementTypeLight]' };
            case ElementType.Dark:
                return { icon: Moon, color: 'text-purple-500', bgColor: 'bg-purple-500/10', nameKey: '[ElementTypeDark]' };
            default:
                return { icon: Users, color: 'text-gray-500', bgColor: 'bg-gray-500/10', nameKey: '[ElementTypeNone]' };
        }
    };

    // 获取稀有度名称
    const getRarityName = (rarity: CharacterRarityFlags) => {
        const rarityMap: Record<number, string> = {
            [CharacterRarityFlags.N]: 'N',
            [CharacterRarityFlags.R]: 'R',
            [CharacterRarityFlags.RPlus]: 'R+',
            [CharacterRarityFlags.SR]: 'SR',
            [CharacterRarityFlags.SRPlus]: 'SR+',
            [CharacterRarityFlags.SSR]: 'SSR',
            [CharacterRarityFlags.SSRPlus]: 'SSR+',
            [CharacterRarityFlags.UR]: 'UR',
            [CharacterRarityFlags.URPlus]: 'UR+',
            [CharacterRarityFlags.LR]: 'LR',
            [CharacterRarityFlags.LRPlus]: 'LR+',
            [CharacterRarityFlags.LRPlus2]: 'LR+2',
            [CharacterRarityFlags.LRPlus3]: 'LR+3',
            [CharacterRarityFlags.LRPlus4]: 'LR+4',
            [CharacterRarityFlags.LRPlus5]: 'LR+5',
            [CharacterRarityFlags.LRPlus6]: 'LR+6',
            [CharacterRarityFlags.LRPlus7]: 'LR+7',
            [CharacterRarityFlags.LRPlus8]: 'LR+8',
            [CharacterRarityFlags.LRPlus9]: 'LR+9',
            [CharacterRarityFlags.LRPlus10]: 'LR+10',
        };
        return rarityMap[rarity] || '?';
    };

    // 获取稀有度颜色
    const getRarityColor = (rarity: CharacterRarityFlags) => {
        if (rarity >= CharacterRarityFlags.LR) return 'text-pink-500 border-pink-500 bg-pink-500/10';
        if (rarity >= CharacterRarityFlags.UR) return 'text-purple-500 border-purple-500 bg-purple-500/10';
        if (rarity >= CharacterRarityFlags.SSR) return 'text-orange-500 border-orange-500 bg-orange-500/10';
        if (rarity >= CharacterRarityFlags.SR) return 'text-yellow-500 border-yellow-500 bg-yellow-500/10';
        if (rarity >= CharacterRarityFlags.R) return 'text-blue-500 border-blue-500 bg-blue-500/10';
        return 'text-gray-500 border-gray-500 bg-gray-500/10';
    };

    // 判断是否是战斗类型石台
    const isBattleGrid = (type: DungeonBattleGridType) => {
        return [
            DungeonBattleGridType.BattleNormal,
            DungeonBattleGridType.BattleElite,
            DungeonBattleGridType.BattleBoss,
            DungeonBattleGridType.BattleBossNoRelic,
            DungeonBattleGridType.BattleAndRelicReinforce,
            DungeonBattleGridType.EventBattleNormal,
            DungeonBattleGridType.EventBattleElite,
            DungeonBattleGridType.EventBattleSpecial
        ].includes(type);
    };



    // 判断石台是否可选择
    const isSelectable = (platform: typeof platformChoices[0]) => {
        if (!battleInfo) return false;
        const currentGridGuid = battleInfo.userDungeonDtoInfo?.currentGridGuid;
        const doneGridGuids = battleInfo.userDungeonDtoInfo?.doneGridGuids || [];
        const currentPlatform = platformChoices.find(p => p.guid === currentGridGuid);
        const currentY = currentPlatform?.y ?? -1;

        // 按Y坐标分组
        const groupedByY = platformChoices.reduce((acc, p) => {
            if (!acc[p.y]) acc[p.y] = [];
            acc[p.y].push(p);
            return acc;
        }, {} as Record<number, typeof platformChoices>);

        if (doneGridGuids.includes(platform.guid)) return false;
        if (platform.guid === currentGridGuid) return false;
        if (!currentGridGuid) return platform.y === 0;
        if (platform.y !== currentY + 1) return false;

        const currentRow = groupedByY[currentY];
        const nextRow = groupedByY[platform.y];
        const currentX = currentPlatform?.x ?? 0;

        if (nextRow.length > currentRow.length) {
            return platform.x === currentX || platform.x === currentX + 1;
        }
        return platform.x === currentX || platform.x === currentX - 1;
    };

    // 处理确认选择石台
    const [isSubmitting, setIsSubmitting] = useState(false);

    const handleConfirmSelection = async () => {
        if (!selectedPlatform || !battleInfo) return;

        // 检查是否是可选择石台
        if (!isSelectable(selectedPlatform)) return;

        setIsSubmitting(true);
        try {
            if (selectedPlatform.type === DungeonBattleGridType.JoinCharacter) {
                // JoinCharacter 类型需要选择增援角色
                if (!selectedGuestCharacterId) return;
                await ortegaApi.dungeonBattle.execGuest({
                    dungeonGridGuid: selectedPlatform.guid,
                    guestMBId: selectedGuestCharacterId,
                    currentTermId: battleInfo.currentTermId
                });
            } else {
                // 其他类型直接选择石台
                await ortegaApi.dungeonBattle.selectGrid({
                    dungeonGridGuid: selectedPlatform.guid,
                    currentTermId: battleInfo.currentTermId
                });
            }

            // 刷新数据
            await loadData();

            // 关闭弹窗
            setSelectedPlatform(null);
            setSelectedGuestCharacterId(null);
        } catch (err) {
            console.error('Failed to select grid:', err);
        } finally {
            setIsSubmitting(false);
        }
    };

    // 计算当前进度数据
    const progressData = useMemo(() => {
        if (!battleInfo) return null;

        const currentLayer = battleInfo.currentDungeonBattleLayer?.layerCount || 0;
        const recoveryUsed = battleInfo.userDungeonDtoInfo?.useDungeonRecoveryItemCount || 0;
        const missedCount = battleInfo.userDungeonBattleMissedCount || 0;

        return {
            currentLayer,
            totalLayers: 3,
            refreshIn: refreshTimeLeft,
            recoveryFruitsUsed: recoveryUsed,
            maxRecoveryFruits: 20,
            missedCount,
            bonusPercentage: missedCount * 80
        };
    }, [battleInfo, refreshTimeLeft]);

    // 转换加护数据
    const blessings = useMemo(() => {
        if (!battleInfo || !relicTable.length) return [];

        const relicIds = battleInfo.userDungeonDtoInfo?.relicIds || [];
        const relicMap: Record<number, number> = {};

        // 统计每个加护的数量
        relicIds.forEach(id => {
            relicMap[id] = (relicMap[id] || 0) + 1;
        });

        return Object.entries(relicMap).map(([id, count]) => {
            const relicMB = relicTable.find(r => r.id === parseInt(id));
            return {
                id: parseInt(id),
                name: relicMB ? t(relicMB.nameKey) : t('CAVE_UNKNOWN_BLESSING'),
                effect: relicMB ? t(relicMB.descriptionKey) : '',
                tier: relicMB?.dungeonRelicRarityType || DungeonBattleRelicRarityType.None,
                count
            };
        });
    }, [battleInfo, relicTable, t]);

    // 转换增援角色数据
    const reinforcements = useMemo(() => {
        if (!battleInfo || !characterTable.length) return [];

        return (battleInfo.userDungeonBattleGuestCharacterDtoInfos || []).map(char => {
            const characterMB = characterTable.find(c => c.id === char.characterId);
            const guestMap = battleInfo.userDungeonDtoInfo?.guestCharacterMap || {};
            const isSelected = Object.values(guestMap).flat().includes(char.guid as unknown as number);

            return {
                id: char.guid,
                characterId: char.characterId,
                name: characterMB ? t(characterMB.nameKey) : t('CAVE_UNKNOWN_CHARACTER'),
                rarity: characterMB?.rarityFlags || 0,
                battlePower: char.battlePower,
                selected: isSelected
            };
        });
    }, [battleInfo, characterTable, t]);

    // 转换石台选择数据
    const platformChoices = useMemo(() => {
        if (!battleInfo || !gridTable.length) return [];

        const grids = battleInfo.currentDungeonBattleLayer?.dungeonGrids || [];
        return grids.map(grid => {
            const gridMB = gridTable.find(g => g.id === grid.dungeonGridId);
            const power = battleInfo.gridBattlePowerDict?.[grid.dungeonGridGuid] || 0;

            return {
                id: grid.dungeonGridId,
                guid: grid.dungeonGridGuid,
                type: gridMB?.dungeonGridType || DungeonBattleGridType.BattleNormal,
                typeName: t(gridMB?.dungeonGridNameKey || ''),
                power,
                x: grid.x,
                y: grid.y
            };
        });
    }, [battleInfo, gridTable]);

    // 加载中
    if (loading) {
        return (
            <div className="flex items-center justify-center min-h-[400px]">
                <Loader2 className="w-8 h-8 animate-spin text-primary" />
            </div>
        );
    }

    // 错误状态
    if (error) {
        return (
            <div className="container mx-auto p-6">
                <Alert variant="destructive">
                    <AlertDescription>{error}</AlertDescription>
                </Alert>
            </div>
        );
    }

    // 未登录状态
    if (!currentAccountId || !battleInfo) {
        return (
            <div className="container mx-auto p-6">
                <Alert>
                    <AlertDescription>{t('CAVE_LOGIN_FIRST')}</AlertDescription>
                </Alert>
            </div>
        );
    }

    return (
        <div className="container mx-auto p-6 space-y-6">
            {/* 当前进度概览 */}
            {progressData && (
                <Card>
                    <CardHeader>
                        <CardTitle className="flex items-center gap-2">
                            <Trophy className="w-5 h-5" />
                            {t('CAVE_EXPLORE_PROGRESS')}
                        </CardTitle>
                    </CardHeader>
                    <CardContent className="space-y-4">
                        {/* 层数进度 */}
                        <div className="space-y-2">
                            <div className="flex justify-between text-sm">
                                <span>{t('CAVE_LAYER_PROGRESS', [progressData.currentLayer, progressData.totalLayers])}</span>
                                <span>{Math.round((progressData.currentLayer / progressData.totalLayers) * 100)}%</span>
                            </div>
                            <Progress
                                value={(progressData.currentLayer / progressData.totalLayers) * 100}
                                className="h-3"
                            />
                        </div>

                        {/* 刷新倒计时 */}
                        <div className="flex items-center justify-between p-2 bg-secondary/50 rounded">
                            <div className="flex items-center gap-2">
                                <Clock className="w-4 h-4 text-muted-foreground" />
                                <span className="text-sm">{t('CAVE_REFRESH_COUNTDOWN')}</span>
                            </div>
                            <Badge variant="outline">{progressData.refreshIn}</Badge>
                        </div>

                        {/* 未探索补偿 */}
                        {progressData.missedCount > 0 && (
                            <Alert>
                                <Trophy className="h-4 w-4" />
                                <AlertDescription>
                                    {t('CAVE_MISSED_PREFIX', [progressData.missedCount])}{' '}
                                    <span className="font-semibold text-orange-500">
                                        {progressData.bonusPercentage}%
                                    </span>{' '}
                                    {t('CAVE_MISSED_SUFFIX')}
                                </AlertDescription>
                            </Alert>
                        )}

                        {/* 回复果实 */}
                        <div className="flex items-center justify-between">
                            <div className="flex items-center gap-2">
                                <Heart className="w-4 h-4 text-red-500" />
                                <span className="text-sm">{t('CAVE_FRUIT_USES')}</span>
                            </div>
                            <span className="text-sm">
                                {progressData.recoveryFruitsUsed} / {progressData.maxRecoveryFruits}
                            </span>
                        </div>
                    </CardContent>
                </Card>
            )}

            {/* 标签页内容 */}
            <Card>
                <Tabs defaultValue="explore" className="p-6">
                    <TabsList className="grid w-full grid-cols-3">
                        <TabsTrigger value="explore">{t('CAVE_TAB_EXPLORE')}</TabsTrigger>
                        <TabsTrigger value="team">{t('CAVE_TAB_TEAM')}</TabsTrigger>
                        <TabsTrigger value="blessings">{t('[DungeonBattleMapProtectionButtonLabel]')}</TabsTrigger>
                    </TabsList>

                    {/* 探索石台 */}
                    <TabsContent value="explore" className="space-y-4">
                        <CardHeader className="px-0">
                            <CardTitle>{t('CAVE_AVAILABLE_ENCOUNTERS')}</CardTitle>
                            <CardDescription>
                                {t('CAVE_AVAILABLE_ENCOUNTERS_DESC')}
                            </CardDescription>
                        </CardHeader>

                        {platformChoices.length > 0 ? (
                            <div className="bg-gradient-to-b from-secondary/10 to-secondary/5 p-8 rounded-lg relative">
                                {(() => {

                                    // 按Y坐标分组
                                    const groupedByY = platformChoices.reduce((acc, platform) => {
                                        if (!acc[platform.y]) {
                                            acc[platform.y] = [];
                                        }
                                        acc[platform.y].push(platform);
                                        return acc;
                                    }, {} as Record<number, typeof platformChoices>);

                                    // 获取所有Y坐标并排序（从小到大）
                                    const yLevels = Object.keys(groupedByY).map(Number).sort((a, b) => a - b);

                                    return (
                                        <div className="relative">
                                            {/* SVG连接线层 */}
                                            <svg className="absolute inset-0 w-full h-full pointer-events-none" style={{ zIndex: 0 }}>
                                                {yLevels.map((y, yIndex) => {
                                                    if (yIndex === yLevels.length - 1) return null;

                                                    const currentRow = groupedByY[y];
                                                    const nextY = yLevels[yIndex + 1];
                                                    const nextRow = groupedByY[nextY];

                                                    return currentRow.map((platform, pIndex) => {
                                                        return nextRow.map((nextPlatform, nIndex) => {
                                                            const fromX = (pIndex + 1) / (currentRow.length + 1) * 100;
                                                            const fromY = (yIndex * 180 + 90);
                                                            const toX = (nIndex + 1) / (nextRow.length + 1) * 100;
                                                            const toY = ((yIndex + 1) * 180 + 90);

                                                            return (
                                                                <line
                                                                    key={`${platform.guid}-${nextPlatform.guid}`}
                                                                    x1={`${fromX}%`}
                                                                    y1={fromY}
                                                                    x2={`${toX}%`}
                                                                    y2={toY}
                                                                    stroke="currentColor"
                                                                    strokeWidth="2"
                                                                    strokeDasharray="4 4"
                                                                    className="text-muted-foreground/30"
                                                                />
                                                            );
                                                        });
                                                    });
                                                })}
                                            </svg>

                                            {/* 石台层 */}
                                            <div className="relative space-y-6" style={{ zIndex: 1 }}>
                                                {yLevels.map((y) => {
                                                    const platforms = groupedByY[y].sort((a, b) => a.x - b.x);

                                                    return (
                                                        <div key={y} className="flex justify-center items-center" style={{ minHeight: '160px' }}>
                                                            <div className="flex gap-12 justify-center items-center">
                                                                {platforms.map((platform) => {
                                                                    const currentGridGuid = battleInfo?.userDungeonDtoInfo?.currentGridGuid;
                                                                    const doneGridGuids = battleInfo?.userDungeonDtoInfo?.doneGridGuids || [];
                                                                    const isCurrent = platform.guid === currentGridGuid;
                                                                    const isDone = doneGridGuids.includes(platform.guid);
                                                                    const canSelect = isSelectable(platform);

                                                                    return (
                                                                        <div
                                                                            key={platform.guid}
                                                                            className="flex flex-col items-center gap-2"
                                                                        >
                                                                            {/* 石台卡片 */}
                                                                            <div className="relative">
                                                                                <div
                                                                                    className={`w-28 h-28 border-3 rounded-full flex flex-col items-center justify-center cursor-pointer transition-all ${isCurrent
                                                                                        ? 'border-primary bg-primary/10 shadow-xl shadow-primary/50 scale-110'
                                                                                        : isDone
                                                                                            ? 'border-muted bg-muted/20 opacity-60'
                                                                                            : canSelect
                                                                                                ? `hover:scale-110 hover:shadow-xl ${getGridTypeColor(platform.type).replace('text-', 'border-')} bg-card shadow-lg`
                                                                                                : 'border-muted-foreground/20 bg-muted/10 opacity-40'
                                                                                        }`}
                                                                                    onClick={() => handlePlatformClick(platform)}
                                                                                >
                                                                                    <Skull className={`w-8 h-8 ${isCurrent
                                                                                        ? 'text-primary'
                                                                                        : isDone
                                                                                            ? 'text-muted-foreground'
                                                                                            : getGridTypeColor(platform.type)
                                                                                        }`} />
                                                                                    <div className="text-[10px] font-medium text-center line-clamp-1 px-1 mt-0.5">
                                                                                        {platform.typeName}
                                                                                    </div>
                                                                                    <div className="text-[9px] text-muted-foreground font-semibold">
                                                                                        {(platform.power / 1000).toFixed(0)}K
                                                                                    </div>
                                                                                </div>

                                                                                {/* 当前位置标识 */}
                                                                                {isCurrent && (
                                                                                    <div className="absolute -top-2 left-1/2 -translate-x-1/2 bg-primary text-primary-foreground text-[10px] px-2 py-0.5 rounded-full shadow-lg whitespace-nowrap font-semibold">
                                                                                        {t('CAVE_CURRENT_POSITION')}
                                                                                    </div>
                                                                                )}

                                                                                {/* 已完成标识 */}
                                                                                {isDone && !isCurrent && (
                                                                                    <div className="absolute -top-2 left-1/2 -translate-x-1/2 bg-muted text-muted-foreground text-[10px] px-2 py-0.5 rounded-full shadow-sm">
                                                                                        {t('CAVE_COMPLETED')}
                                                                                    </div>
                                                                                )}

                                                                                {/* 坐标标签 */}
                                                                                <div className="absolute -bottom-2 -right-2 bg-secondary text-secondary-foreground text-[9px] px-1.5 py-0.5 rounded-full shadow-sm">
                                                                                    {platform.x},{platform.y}
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    );
                                                                })}
                                                            </div>
                                                        </div>
                                                    );
                                                })}
                                            </div>
                                        </div>
                                    );
                                })()}
                            </div>
                        ) : (
                            <p className="text-center text-muted-foreground py-8">
                                {t('CAVE_NO_ENCOUNTERS')}
                            </p>
                        )}
                    </TabsContent>

                    {/* 队伍状态 */}
                    <TabsContent value="team" className="space-y-4">
                        <CardHeader className="px-0">
                            <CardTitle>{t('CAVE_CURRENT_TEAM')}</CardTitle>
                            <CardDescription>
                                {t('CAVE_CURRENT_TEAM_DESC')}
                            </CardDescription>
                        </CardHeader>
                        <div className="grid grid-cols-3 md:grid-cols-4 lg:grid-cols-5 gap-4">
                            {(() => {
                                const userSyncData = getCurrentUserSyncData();
                                return battleInfo.userDungeonBattleCharacterDtoInfos?.map((dungeonChar) => {
                                    // 从 accountStore 的 userSyncData 中查找完整的角色信息
                                    const userCharInfo = userSyncData?.userCharacterDtoInfos?.find(
                                        c => c.guid === dungeonChar.guid
                                    );

                                    // 如果没有找到角色信息，跳过
                                    if (!userCharInfo) return null;

                                    // 构建 UserCharacterInfo 对象
                                    const characterInfo: UserCharacterInfo = {
                                        guid: userCharInfo.guid,
                                        playerId: userCharInfo.playerId,
                                        characterId: userCharInfo.characterId,
                                        level: userCharInfo.level,
                                        subLevel: 0,
                                        exp: userCharInfo.exp,
                                        rarityFlags: userCharInfo.rarityFlags,
                                        isLocked: userCharInfo.isLocked
                                    };

                                    // 计算 HP 百分比 (PerMill = 千分比，1000 = 100%)
                                    const hpPercent = (dungeonChar.currentHpPerMill / 10).toFixed(1);
                                    const isLowHp = dungeonChar.currentHpPerMill < 300; // 30% 以下

                                    return (
                                        <div key={dungeonChar.guid} className="flex flex-col items-center gap-2">
                                            <CharacterIcon
                                                userCharacterInfo={characterInfo}
                                                size={80}
                                                userSyncData={userSyncData}
                                            />
                                            <div className="w-full space-y-1">
                                                {/* HP 条 */}
                                                <div className="relative h-2 bg-secondary rounded-full overflow-hidden">
                                                    <div
                                                        className={`absolute inset-y-0 left-0 rounded-full transition-all ${
                                                            isLowHp ? 'bg-red-500' : 'bg-green-500'
                                                        }`}
                                                        style={{ width: `${hpPercent}%` }}
                                                    />
                                                </div>
                                                <p className={`text-xs text-center ${isLowHp ? 'text-red-500' : 'text-muted-foreground'}`}>
                                                    HP: {hpPercent}%
                                                </p>
                                            </div>
                                        </div>
                                    );
                                });
                            })()}
                            {(!battleInfo.userDungeonBattleCharacterDtoInfos || battleInfo.userDungeonBattleCharacterDtoInfos.length === 0) && (
                                <p className="col-span-full text-center text-muted-foreground py-8">
                                    {t('CAVE_NO_TEAM')}
                                </p>
                            )}
                        </div>
                    </TabsContent>

                    {/* 加护列表 */}
                    <TabsContent value="blessings" className="space-y-4">
                        <CardHeader className="px-0">
                            <CardTitle>{t('CAVE_OBTAINED_BLESSINGS')}</CardTitle>
                            <CardDescription>
                                {t('CAVE_OBTAINED_BLESSINGS_DESC')}
                            </CardDescription>
                        </CardHeader>
                        <div className="grid gap-3">
                            {blessings.map((blessing) => (
                                <Card key={blessing.id} className="relative overflow-hidden">
                                    <div className={`absolute left-0 top-0 bottom-0 w-1 ${getBlessingTierColor(blessing.tier)}`} />
                                    <CardContent className="pt-6 pl-6">
                                        <div className="flex items-start justify-between gap-3">
                                            <div className="flex items-start gap-3 flex-1">
                                                <Sparkles className={`w-5 h-5 mt-0.5 ${getBlessingTierColor(blessing.tier).replace('bg-', 'text-')}`} />
                                                <div className="space-y-1 flex-1">
                                                    <div className="flex items-center gap-2">
                                                        <h4 className="font-semibold">{blessing.name}</h4>
                                                        {blessing.count > 1 && (
                                                            <Badge variant="secondary" className="text-xs">
                                                                x{blessing.count}
                                                            </Badge>
                                                        )}
                                                    </div>
                                                    <p className="text-sm text-muted-foreground">{blessing.effect}</p>
                                                </div>
                                            </div>
                                        </div>
                                    </CardContent>
                                </Card>
                            ))}
                            {blessings.length === 0 && (
                                <p className="text-center text-muted-foreground py-8">
                                    {t('[DungeonBattleRelicListEmptyMessage]')}
                                </p>
                            )}
                        </div>
                    </TabsContent>
                </Tabs>
            </Card>

            {/* 当前石台操作弹窗 - 当 CurrentGridState 不是 Done 时显示 */}
            <Dialog open={!!currentGridAction} onOpenChange={() => setCurrentGridAction(null)}>
                <DialogContent className="max-w-2xl max-h-[80vh] overflow-y-auto">
                    {currentGridAction && battleInfo && (
                        <>
                            <DialogHeader>
                                <DialogTitle className="flex items-center gap-2">
                                    <Skull className={`w-5 h-5 ${getGridTypeColor(currentGridAction.type)}`} />
                                    {currentGridAction.typeName}
                                </DialogTitle>
                                <DialogDescription>
                                    {t('CAVE_GRID_STATE', [getGridStateName(battleInfo.userDungeonDtoInfo?.currentGridState)])} | {t('CAVE_RECOMMENDED_POWER', [currentGridAction.power.toLocaleString()])}
                                </DialogDescription>
                            </DialogHeader>

                            <div className="space-y-4">
                                {/* 根据状态和类型显示不同内容 */}
                                {(() => {
                                    const state = battleInfo.userDungeonDtoInfo?.currentGridState;
                                    const type = currentGridAction.type;

                                    // Selected 状态下的不同操作
                                    if (state === DungeonBattleGridState.Selected) {
                                        // 战斗类型
                                        if (isBattleGrid(type)) {
                                            return (
                                                <div className="space-y-3">
                                                    <Alert>
                                                        <AlertDescription>{t('CAVE_BATTLE_HINT')}</AlertDescription>
                                                    </Alert>
                                                    <Button
                                                        className="w-full"
                                                        disabled={isActionSubmitting}
                                                        onClick={() => handleCurrentGridAction('battle')}
                                                    >
                                                        {isActionSubmitting ? (
                                                            <><Loader2 className="w-4 h-4 mr-2 animate-spin" />{t('CAVE_RUNNING')}</>
                                                        ) : (
                                                            t('[LocalRaidRoomManagementChallengeButtonLabel]')
                                                        )}
                                                    </Button>
                                                </div>
                                            );
                                        }

                                        // 恢复类型
                                        if (type === DungeonBattleGridType.Recovery) {
                                            return (
                                                <div className="space-y-3">
                                                    <Alert>
                                                        <AlertDescription>{t('CAVE_FRUIT_HINT')}</AlertDescription>
                                                    </Alert>
                                                    <Button
                                                        className="w-full"
                                                        disabled={isActionSubmitting}
                                                        onClick={() => handleCurrentGridAction('recovery')}
                                                    >
                                                        {isActionSubmitting ? (
                                                            <><Loader2 className="w-4 h-4 mr-2 animate-spin" />{t('CAVE_RUNNING')}</>
                                                        ) : (
                                                            t('CAVE_USE_FRUIT')
                                                        )}
                                                    </Button>
                                                </div>
                                            );
                                        }

                                        // 强化圣遗物类型
                                        if (type === DungeonBattleGridType.RelicReinforce) {
                                            return (
                                                <div className="space-y-3">
                                                    <Alert>
                                                        <AlertDescription>{t('CAVE_ENHANCE_HINT')}</AlertDescription>
                                                    </Alert>
                                                    <Button
                                                        className="w-full"
                                                        disabled={isActionSubmitting}
                                                        onClick={() => handleCurrentGridAction('reinforce')}
                                                    >
                                                        {isActionSubmitting ? (
                                                            <><Loader2 className="w-4 h-4 mr-2 animate-spin" />{t('CAVE_RUNNING')}</>
                                                        ) : (
                                                            t('[DungeonBattleGridRelicReinforcementTitle]')
                                                        )}
                                                    </Button>
                                                </div>
                                            );
                                        }

                                        // 复活类型
                                        if (type === DungeonBattleGridType.Revival) {
                                            return (
                                                <div className="space-y-3">
                                                    <Alert>
                                                        <AlertDescription>{t('CAVE_REVIVE_HINT')}</AlertDescription>
                                                    </Alert>
                                                    <Button
                                                        className="w-full"
                                                        disabled={isActionSubmitting}
                                                        onClick={() => handleCurrentGridAction('revive')}
                                                    >
                                                        {isActionSubmitting ? (
                                                            <><Loader2 className="w-4 h-4 mr-2 animate-spin" />{t('CAVE_RUNNING')}</>
                                                        ) : (
                                                            t('CAVE_REVIVE')
                                                        )}
                                                    </Button>
                                                </div>
                                            );
                                        }

                                        // 商店类型
                                        if (type === DungeonBattleGridType.Shop) {
                                            return (
                                                <div className="space-y-3">
                                                    <Alert>
                                                        <AlertDescription>{t('CAVE_SHOP_HINT')}</AlertDescription>
                                                    </Alert>
                                                    <Button
                                                        className="w-full"
                                                        disabled={isActionSubmitting}
                                                        onClick={() => handleCurrentGridAction('leaveShop')}
                                                    >
                                                        {isActionSubmitting ? (
                                                            <><Loader2 className="w-4 h-4 mr-2 animate-spin" />{t('CAVE_RUNNING')}</>
                                                        ) : (
                                                            t('CAVE_LEAVE_SHOP')
                                                        )}
                                                    </Button>
                                                </div>
                                            );
                                        }

                                        return (
                                            <Alert>
                                                <AlertDescription>{t('CAVE_UNSUPPORTED')}</AlertDescription>
                                            </Alert>
                                        );
                                    }

                                    // Reward 状态 - 选择加护奖励
                                    if (state === DungeonBattleGridState.Reward) {
                                        return (
                                            <div className="space-y-3">
                                                <h4 className="text-sm font-semibold">{t('[DungeonBattleGridSelectRelicTitle]')}</h4>
                                                <div className="grid gap-2">
                                                    {battleInfo.rewardRelicIds?.map((relicId) => {
                                                        const relic = relicTable.find(r => r.id === relicId);
                                                        if (!relic) return null;
                                                        return (
                                                            <Card
                                                                key={relicId}
                                                                className="cursor-pointer hover:border-primary/50 transition-all"
                                                                onClick={() => handleSelectRelicReward(relicId)}
                                                            >
                                                                <CardContent className="pt-4 pb-3">
                                                                    <div className="flex items-center gap-3">
                                                                        <Sparkles className={`w-5 h-5 ${getBlessingTierColor(relic.dungeonRelicRarityType).replace('bg-', 'text-')}`} />
                                                                        <div>
                                                                            <h5 className="font-semibold">{t(relic.nameKey)}</h5>
                                                                            <p className="text-sm text-muted-foreground">{t(relic.descriptionKey)}</p>
                                                                        </div>
                                                                    </div>
                                                                </CardContent>
                                                            </Card>
                                                        );
                                                    })}
                                                </div>
                                                {(!battleInfo.rewardRelicIds || battleInfo.rewardRelicIds.length === 0) && (
                                                    <Alert>
                                                        <AlertDescription>{t('CAVE_NO_BLESSING_REWARDS')}</AlertDescription>
                                                    </Alert>
                                                )}
                                            </div>
                                        );
                                    }

                                    return null;
                                })()}
                            </div>
                        </>
                    )}
                </DialogContent>
            </Dialog>

            {/* 石台详情弹窗 - 用于选择下一个石台 */}
            <Dialog open={!!selectedPlatform} onOpenChange={() => {
                setSelectedPlatform(null);
                setSelectedGuestCharacterId(null);
            }}>
                <DialogContent className="max-w-2xl max-h-[80vh] overflow-y-auto">
                    {selectedPlatform && (
                        <>
                            <DialogHeader>
                                <DialogTitle className="flex items-center gap-2">
                                    <Skull className={`w-5 h-5 ${getGridTypeColor(selectedPlatform.type)}`} />
                                    {selectedPlatform.typeName}
                                </DialogTitle>
                                <DialogDescription>
                                    {t('CAVE_COORDS', [selectedPlatform.x, selectedPlatform.y])} | {t('CAVE_RECOMMENDED_POWER', [selectedPlatform.power.toLocaleString()])}
                                </DialogDescription>
                            </DialogHeader>

                            <div className="space-y-4">
                                {/* 战斗石台显示敌人信息 */}
                                {isBattleGrid(selectedPlatform.type) && (
                                    <div className="space-y-3">
                                        {loadingDetails ? (
                                            <div className="flex items-center justify-center py-8">
                                                <Loader2 className="w-6 h-6 animate-spin text-primary" />
                                            </div>
                                        ) : platformDetails ? (
                                            <>
                                                {/* 敌人列表 */}
                                                {platformDetails.enemyInfos && platformDetails.enemyInfos.length > 0 && (
                                                    <div>
                                                        <h4 className="text-sm font-semibold mb-2">{t('[DungeonBattleGridBattleHeadlineEnemy]')}</h4>
                                                        <div className="grid gap-2">
                                                            {platformDetails.enemyInfos.map((enemy, idx) => (
                                                                <Card key={idx}>
                                                                    <CardContent className="pt-4 pb-3">
                                                                        <div className="flex items-center justify-between">
                                                                            <div className="flex items-center gap-2">
                                                                                <Badge variant="outline">Lv.{enemy.level}</Badge>
                                                                                <span className="text-sm">{t('CAVE_POWER', [enemy.battlePower.toLocaleString()])}</span>
                                                                            </div>
                                                                        </div>
                                                                    </CardContent>
                                                                </Card>
                                                            ))}
                                                        </div>
                                                    </div>
                                                )}

                                                {/* 奖励列表 */}
                                                {(platformDetails.normalRewardItemList?.length > 0 || platformDetails.specialRewardItemList?.length > 0) && (
                                                    <div>
                                                        <h4 className="text-sm font-semibold mb-2">{t('[DungeonBattleGridBattleHeadlineReward]')}</h4>
                                                        <div className="space-y-2">
                                                            {platformDetails.normalRewardItemList && platformDetails.normalRewardItemList.length > 0 && (
                                                                <div>
                                                                    <p className="text-xs text-muted-foreground mb-1">{t('CAVE_NORMAL_REWARDS')}</p>
                                                                    <div className="flex flex-wrap gap-2">
                                                                        {platformDetails.normalRewardItemList.map((item, idx) => (
                                                                            <Badge key={idx} variant="secondary">
                                                                                {getItemName(item.itemType, item.itemId)} ×{item.itemCount}
                                                                            </Badge>
                                                                        ))}
                                                                    </div>
                                                                </div>
                                                            )}
                                                            {platformDetails.specialRewardItemList && platformDetails.specialRewardItemList.length > 0 && (
                                                                <div>
                                                                    <p className="text-xs text-muted-foreground mb-1">{t('CAVE_SPECIAL_REWARDS')}</p>
                                                                    <div className="flex flex-wrap gap-2">
                                                                        {platformDetails.specialRewardItemList.map((item, idx) => (
                                                                            <Badge key={idx} variant="default">
                                                                                {getItemName(item.itemType, item.itemId)} ×{item.itemCount}
                                                                            </Badge>
                                                                        ))}
                                                                    </div>
                                                                </div>
                                                            )}
                                                        </div>
                                                    </div>
                                                )}
                                            </>
                                        ) : null}
                                    </div>
                                )}

                                {/* JoinCharacter 石台显示增援角色列表 */}
                                {selectedPlatform.type === DungeonBattleGridType.JoinCharacter && battleInfo && (
                                    <div className="space-y-3">
                                        <h4 className="text-sm font-semibold">{t('CAVE_SELECT_ASSIST')}</h4>
                                        <div className="grid gap-2">
                                            {(() => {
                                                // 从 guestCharacterMap 获取当前石台的 4 个增援角色 MB ID
                                                const guestMbIds = battleInfo.userDungeonDtoInfo?.guestCharacterMap?.[selectedPlatform.guid] || [];

                                                // 从 DungeonBattleGuestTable 查询角色信息
                                                return guestMbIds
                                                    .map(guestMbId => {
                                                        const guestMB = guestTable.find(g => g.id === guestMbId);
                                                        if (!guestMB) return null;
                                                        return {
                                                            guestMbId,
                                                            characterId: guestMB.characterId,
                                                        };
                                                    })
                                                    .filter((item): item is { guestMbId: number; characterId: number } => item !== null)
                                                    .map(({ guestMbId, characterId }) => {
                                                        const characterMB = characterTable.find(c => c.id === characterId);
                                                        // 从 userDungeonBattleGuestCharacterDtoInfos 获取详细数据
                                                        const guestCharInfo = battleInfo.userDungeonBattleGuestCharacterDtoInfos?.find(
                                                            g => g.characterId === characterId
                                                        );
                                                        const isSelected = selectedGuestCharacterId === guestMbId;

                                                        // 获取属性信息
                                                        const elementType = characterMB?.elementType ?? ElementType.None;
                                                        const elementInfo = getElementInfo(elementType);
                                                        const ElementIcon = elementInfo.icon;

                                                        // 获取稀有度信息
                                                        const rarity = guestCharInfo?.rarityFlags ?? CharacterRarityFlags.N;
                                                        const rarityName = getRarityName(rarity);
                                                        const rarityColor = getRarityColor(rarity);

                                                        return (
                                                            <Card
                                                                key={guestMbId}
                                                                className={`cursor-pointer transition-all ${isSelected ? 'border-primary bg-primary/5' : 'hover:border-primary/50'
                                                                    }`}
                                                                onClick={() => setSelectedGuestCharacterId(guestMbId)}
                                                            >
                                                                <CardContent className="pt-4 pb-3">
                                                                    <div className="flex items-center justify-between">
                                                                        <div className="flex items-center gap-3">
                                                                            {/* 属性图标 */}
                                                                            <div className={`w-10 h-10 rounded-full flex items-center justify-center ${elementInfo.bgColor}`}>
                                                                                <ElementIcon className={`w-5 h-5 ${elementInfo.color}`} />
                                                                            </div>
                                                                            <div className="space-y-1">
                                                                                {/* 角色名称和稀有度 */}
                                                                                <div className="flex items-center gap-2">
                                                                                    <h5 className="font-semibold">
                                                                                        {characterMB ? t(characterMB.nameKey) : t('CAVE_UNKNOWN_CHARACTER')}
                                                                                    </h5>
                                                                                    <Badge
                                                                                        variant="outline"
                                                                                        className={`text-xs ${rarityColor}`}
                                                                                    >
                                                                                        {rarityName}
                                                                                    </Badge>
                                                                                </div>
                                                                                {/* 等级和战斗力 */}
                                                                                <div className="flex items-center gap-3 text-sm">
                                                                                    <span className="text-muted-foreground">
                                                                                        Lv.{guestCharInfo?.level ?? '?'}
                                                                                    </span>
                                                                                    <span className="text-muted-foreground">|</span>
                                                                                    <span className="flex items-center gap-1">
                                                                                        <Zap className="w-3.5 h-3.5 text-yellow-500" />
                                                                                        <span className="font-medium text-foreground">
                                                                                            {(guestCharInfo?.battlePower ?? 0).toLocaleString()}
                                                                                        </span>
                                                                                    </span>
                                                                                    <span className="text-muted-foreground">|</span>
                                                                                    <span className={`text-xs ${elementInfo.color}`}>
                                                                                        {t(elementInfo.nameKey)}
                                                                                    </span>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        {isSelected && (
                                                                            <Badge variant="default">{t('CAVE_SELECTED')}</Badge>
                                                                        )}
                                                                    </div>
                                                                </CardContent>
                                                            </Card>
                                                        );
                                                    });
                                            })()}
                                        </div>

                                    </div>
                                )}

                                {/* 非战斗且非 JoinCharacter 石台显示说明 */}
                                {!isBattleGrid(selectedPlatform.type) && selectedPlatform.type !== DungeonBattleGridType.JoinCharacter && (
                                    <div className="text-sm text-muted-foreground py-4 text-center">
                                        {t('CAVE_NON_BATTLE')}
                                    </div>
                                )}

                                {/* 可选择石台显示确认按钮 - 仅在 Done 状态时显示 */}
                                {selectedPlatform && isSelectable(selectedPlatform) && battleInfo?.userDungeonDtoInfo?.currentGridState === DungeonBattleGridState.Done && (
                                    <Button
                                        className="w-full"
                                        disabled={
                                            isSubmitting ||
                                            (selectedPlatform.type === DungeonBattleGridType.JoinCharacter && !selectedGuestCharacterId)
                                        }
                                        onClick={handleConfirmSelection}
                                    >
                                        {isSubmitting ? (
                                            <>
                                                <Loader2 className="w-4 h-4 mr-2 animate-spin" />
                                                {t('CAVE_CONFIRMING')}
                                            </>
                                        ) : (
                                            t('CAVE_CONFIRM_SELECTION')
                                        )}
                                    </Button>
                                )}
                            </div>
                        </>
                    )}
                </DialogContent>
            </Dialog>
        </div>
    );
}
