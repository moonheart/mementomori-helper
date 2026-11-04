import { useState, useEffect, useMemo, useRef } from 'react';
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from '@/components/ui/card';
import { Badge } from '@/components/ui/badge';
import { Button } from '@/components/ui/button';
import { Progress } from '@/components/ui/progress';
import { Tabs, TabsContent, TabsList, TabsTrigger } from '@/components/ui/tabs';
import { Alert, AlertDescription } from '@/components/ui/alert';
import { Label } from '@/components/ui/label';
import { ScrollArea } from '@/components/ui/scroll-area';
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from '@/components/ui/select';
import {
    Building2,
    Flame,
    Droplet,
    Wind,
    Mountain,
    ChevronUp,
    Trophy,
    Lock,
    Unlock,
    BookOpen,
    Sparkles,
    Loader2,
    Zap,
    Play,
    Square,
    RefreshCw
} from 'lucide-react';
import { ortegaApi } from '@/api/ortega-client';
import { backgroundTaskApi } from '@/api/background-task-service';
import { createJobHubConnection } from '@/api/signalr-client';
import { toast } from '@/hooks/use-toast';
import { BattleFieldCharacterGroupType } from '@/api/generated/battleFieldCharacterGroupType';
import { useMasterTable } from '@/hooks/useMasterData';
import { TowerBattleQuestMB } from '@/api/generated/towerBattleQuestMB';
import { TowerType } from '@/api/generated/towerType';
import { UserTowerBattleDtoInfo } from '@/api/generated/userTowerBattleDtoInfo';
import { UserItem } from '@/api/generated/userItem';
import { BackgroundTaskStatus } from '@/api/generated/backgroundTaskStatus';
import { useItemName } from '@/hooks/useItemName';
import { useTranslation } from '@/hooks/useTranslation';
import { useAccountStore } from '@/store/accountStore';
import type { HubConnection } from '@microsoft/signalr';

// 塔类型配置
type TowerConfig = {
    name?: string;
    nameKey?: string;
    icon: any;
    color: string;
    element?: string;
    elementKey?: string;
};

const TOWER_CONFIGS: Record<number, TowerConfig> = {
    [TowerType.Infinite]: {
        nameKey: '[TowerTypeInfinite]',
        icon: Building2,
        color: 'gray',
        elementKey: 'TOWER_ALL_ELEMENTS'
    },
    [TowerType.Blue]: {
        nameKey: '[TowerTypeBlue]',
        icon: Droplet,
        color: 'blue',
        elementKey: '[ElementTypeBlue]'
    },
    [TowerType.Red]: {
        nameKey: '[TowerTypeRed]',
        icon: Flame,
        color: 'red',
        elementKey: '[ElementTypeRed]'
    },
    [TowerType.Green]: {
        nameKey: '[TowerTypeGreen]',
        icon: Wind,
        color: 'green',
        elementKey: '[ElementTypeGreen]'
    },
    [TowerType.Yellow]: {
        nameKey: '[TowerTypeYellow]',
        icon: Mountain,
        color: 'yellow',
        elementKey: '[ElementTypeYellow]'
    },
    [TowerType.None]: {
        nameKey: 'AUTO_TOWER_UNKNOWN',
        icon: Building2,
        color: 'gray',
        elementKey: '[CommonNoneLabel]'
    }
};

export function TowerPage() {
    const { t } = useTranslation();
    const { getItemName } = useItemName();
    const { currentAccountId } = useAccountStore();
    const [userTowerProgress, setUserTowerProgress] = useState<UserTowerBattleDtoInfo[]>([]);
    const [loading, setLoading] = useState(true);
    const { data: towerQuests, loading: masterLoading } = useMasterTable<TowerBattleQuestMB>('TowerBattleQuestTable');

    // 自动爬塔状态
    const [taskStatus, setTaskStatus] = useState<BackgroundTaskStatus | null>(null);
    const [selectedTowerType, setSelectedTowerType] = useState<TowerType>(TowerType.Infinite);
    const [targetFloor, setTargetFloor] = useState<string>('0');
    const [towerLogs, setTowerLogs] = useState<string[]>([]);
    const jobHubRef = useRef<HubConnection | null>(null);
    const logEndRef = useRef<HTMLDivElement>(null);

    // 日志自动滚动到底部
    useEffect(() => {
        logEndRef.current?.scrollIntoView({ behavior: 'smooth' });
    }, [towerLogs]);

    // 获取各塔的所有楼层并按 Floor 升序排列
    const allTowerQuests = useMemo(() => {
        const map: Record<number, TowerBattleQuestMB[]> = {};
        console.log('towerQuests', towerQuests);
        (towerQuests || []).forEach(q => {
            const typeNum = Number(q.towerType);
            if (!map[typeNum]) map[typeNum] = [];
            map[typeNum].push(q);
        });
        Object.values(map).forEach(list => list.sort((a, b) => a.floor - b.floor));
        return map;
    }, [towerQuests]);

    // 获取当前开放的塔
    const availableTowers = useMemo(() => {
        const now = new Date();
        const adjustedNow = new Date(now.getTime() - 4 * 60 * 60 * 1000); // 4:00 AM 重置
        const day = adjustedNow.getDay();

        const available = [TowerType.Infinite];
        switch (day) {
            case 0: available.push(TowerType.Blue, TowerType.Red, TowerType.Green, TowerType.Yellow); break;
            case 1: available.push(TowerType.Blue); break;
            case 2: available.push(TowerType.Red); break;
            case 3: available.push(TowerType.Green); break;
            case 4: available.push(TowerType.Yellow); break;
            case 5: available.push(TowerType.Blue, TowerType.Red); break;
            case 6: available.push(TowerType.Yellow, TowerType.Green); break;
        }
        return available;
    }, []);

    // 获取选中塔的最大已通关层数
    const maxClearedFloor = useMemo(() => {
        const progress = userTowerProgress.find(p => p.towerType === selectedTowerType);
        return progress?.maxTowerBattleId || 0;
    }, [userTowerProgress, selectedTowerType]);

    // 目标层数选项（显示未来50层）
    const targetFloorOptions = useMemo(() => {
        const quests = allTowerQuests[selectedTowerType] || [];
        return quests
            .filter(q => q.floor > maxClearedFloor)
            .slice(0, 50)
            .map(q => ({
                floor: q.floor,
                name: t('TOWER_FLOOR', [q.floor])
            }));
    }, [allTowerQuests, selectedTowerType, maxClearedFloor, t]);

    const fetchUserData = async (silent = false) => {
        try {
            if (!silent) setLoading(true);
            const response = await ortegaApi.user.getUserData({});
            if (response.userSyncData?.userTowerBattleDtoInfos) {
                setUserTowerProgress(response.userSyncData.userTowerBattleDtoInfos);
            }
        } catch (error) {
            console.error('Failed to fetch tower progress:', error);
        } finally {
            if (!silent) setLoading(false);
        }
    };

    useEffect(() => {
        fetchUserData();
    }, []);

    // 获取后台任务状态
    const fetchTaskStatus = async () => {
        if (!currentAccountId) return;
        try {
            const status = await backgroundTaskApi.getStatus(currentAccountId);
            setTaskStatus(status);
        } catch (error) {
            console.error('Failed to fetch task status:', error);
        }
    };

    // SignalR 连接用于接收日志
    useEffect(() => {
        if (!currentAccountId) return;

        const connection = createJobHubConnection(currentAccountId, (message) => {
            setTowerLogs(prev => [...prev.slice(-99), message]);
        });

        connection.start()
            .then(() => {
                jobHubRef.current = connection;
                console.log('JobHub connected');
            })
            .catch(err => console.error('JobHub connection error:', err));

        return () => {
            connection.stop();
            jobHubRef.current = null;
        };
    }, [currentAccountId]);

    useEffect(() => {
        fetchTaskStatus();
        // 每5秒刷新一次状态
        const interval = setInterval(fetchTaskStatus, 5000);
        return () => clearInterval(interval);
    }, [currentAccountId]);

    // 开始自动爬塔
    const handleStartAutoTower = async () => {
        if (!currentAccountId) return;
        try {
            const target = parseInt(targetFloor) || 0;
            await backgroundTaskApi.startAutoTower(currentAccountId, selectedTowerType, target > 0 ? target : undefined);
            toast({ title: t('BATTLE_TASK_STARTED'), description: t('TOWER_AUTO_STARTED') });
            fetchTaskStatus();
        } catch (error) {
            console.error('Failed to start auto tower:', error);
            toast({ title: t('BATTLE_START_FAILED'), description: t('TOWER_AUTO_START_FAILED'), variant: 'destructive' });
        }
    };

    // 停止自动爬塔
    const handleStopAutoTower = async () => {
        if (!currentAccountId) return;
        try {
            await backgroundTaskApi.stopAutoTower(currentAccountId);
            toast({ title: t('BATTLE_TASK_STOPPED'), description: t('TOWER_AUTO_STOPPED') });
            fetchTaskStatus();
        } catch (error) {
            console.error('Failed to stop auto tower:', error);
            toast({ title: t('BATTLE_STOP_FAILED'), description: t('TOWER_AUTO_STOP_FAILED'), variant: 'destructive' });
        }
    };

    const handleChallenge = async (type: TowerType, questId: number) => {
        try {
            const res = await ortegaApi.towerBattle.start({
                targetTowerType: type,
                towerBattleQuestId: questId
            });

            const isWin = res.battleResult?.simulationResult?.battleEndInfo?.winGroupType === BattleFieldCharacterGroupType.Attacker;

            if (isWin) {
                toast({ title: t('BATTLE_CHALLENGE_WIN'), description: t('TOWER_CLEARED_FLOOR', [questId]) });
            } else {
                toast({ title: t('BATTLE_CHALLENGE_LOSS'), description: t('BATTLE_CHALLENGE_LOSS_DESC'), variant: 'destructive' });
            }
            fetchUserData(true);
        } catch (error) {
            console.error('Tower battle failed:', error);
            toast({ title: t('BATTLE_CHALLENGE_ERROR'), description: t('BATTLE_SERVER_ERROR'), variant: 'destructive' });
        }
    };

    const handleQuickChallenge = async (type: TowerType, questId: number) => {
        try {
            await ortegaApi.towerBattle.quick({
                targetTowerType: type,
                towerBattleQuestId: questId,
                quickCount: 1
            });
            toast({ title: t('TOWER_QUICK_SUCCESS'), description: t('TOWER_QUICK_SUCCESS_DESC') });
            fetchUserData(true);
        } catch (error) {
            console.error('Tower quick failed:', error);
            toast({ title: t('BATTLE_CHALLENGE_LOSS'), description: t('TOWER_QUICK_FAILED_DESC'), variant: 'destructive' });
        }
    };

    const getElementColor = (color: string) => {
        const colors: Record<string, string> = {
            blue: 'from-blue-500 to-cyan-500',
            red: 'from-red-500 to-orange-500',
            green: 'from-green-500 to-emerald-500',
            yellow: 'from-yellow-500 to-amber-500'
        };
        return colors[color] || 'from-gray-500 to-gray-600';
    };

    const renderRewards = (rewards: UserItem[]) => {
        if (!rewards || rewards.length === 0) return null;
        return rewards.map((item, idx) => (
            <span key={idx}>
                {getItemName(item.itemType, item.itemId,) || t('TOWER_ITEM_ID', [item.itemId])} x{item.itemCount}
                {idx < rewards.length - 1 ? ' • ' : ''}
            </span>
        ));
    };

    if (loading || masterLoading) {
        return (
            <div className="flex items-center justify-center min-h-[400px]">
                <Loader2 className="h-8 w-8 animate-spin text-primary" />
                <span className="ml-2">{t('TOWER_LOADING')}</span>
            </div>
        );
    }

    const infiniteProgress = userTowerProgress.find(p => p.towerType === TowerType.Infinite);
    const elementalProgress = userTowerProgress.filter(p => p.towerType !== TowerType.Infinite);

    // 根据 Blazor 代码，maxTowerBattleId 实际上就是楼层数 (Floor)
    const currentInfiniteFloor = infiniteProgress?.maxTowerBattleId || 0;
    const infiniteQuests = allTowerQuests[TowerType.Infinite] || [];

    // 快速挑战次数
    const remainingQuickCount = Math.max(0, 3 - (infiniteProgress?.todayBattleCount || 0));

    // 下一关 Master 数据
    const nextInfiniteQuest = infiniteQuests.find(q => q.floor === currentInfiniteFloor + 1);

    // 显示当前楼层及前后几层
    const displayInfiniteQuests = [...infiniteQuests]
        .filter(q => q.floor <= currentInfiniteFloor + 1 && q.floor > currentInfiniteFloor - 4)
        .sort((a, b) => b.floor - a.floor)
        .slice(0, 5);

    return (
        <div className="space-y-6">
            <Tabs defaultValue="infinite" className="space-y-6">
                <TabsList className="grid w-full grid-cols-3">
                    <TabsTrigger value="infinite">{t('[TowerTypeInfinite]')}</TabsTrigger>
                    <TabsTrigger value="elemental">{t('TOWER_TAB_ELEMENTAL')}</TabsTrigger>
                    <TabsTrigger value="auto">{t('TOWER_TAB_AUTO')}</TabsTrigger>
                </TabsList>

                {/* 无穷之塔 */}
                <TabsContent value="infinite" className="space-y-6">
                    <div className="grid gap-6 md:grid-cols-2">
                        <Card className="relative overflow-hidden">
                            <div className="absolute top-0 right-0 opacity-10">
                                <Building2 className="h-48 w-48" />
                            </div>
                            <CardHeader>
                                <CardTitle className="flex items-center gap-2">
                                    <Building2 className="h-6 w-6" />
                                    {t('TOWER_CURRENT_PROGRESS')}
                                </CardTitle>
                                <CardDescription>{t('TOWER_HIGHEST_DESC')}</CardDescription>
                            </CardHeader>
                            <CardContent className="space-y-4">
                                <div className="space-y-2">
                                    <div className="flex items-center justify-between">
                                        <span className="text-sm text-muted-foreground">{t('TOWER_CURRENT_FLOOR')}</span>
                                        <div className="flex items-baseline gap-2">
                                            <span className="text-3xl font-bold text-primary">
                                                {currentInfiniteFloor}
                                            </span>
                                            <span className="text-sm text-muted-foreground">
                                                / {infiniteQuests[infiniteQuests.length - 1]?.floor || '???'}
                                            </span>
                                        </div>
                                    </div>
                                    <Progress
                                        value={(currentInfiniteFloor / (infiniteQuests[infiniteQuests.length - 1]?.floor || 1)) * 100}
                                        className="h-3"
                                    />
                                </div>
                            </CardContent>
                        </Card>

                        <Card>
                            <CardHeader>
                                <CardTitle className="flex items-center gap-2">
                                    <Sparkles className="h-6 w-6" />
                                    {t('TOWER_TODAY_STATUS')}
                                </CardTitle>
                                <CardDescription>{t('TOWER_TODAY_STATS')}</CardDescription>
                            </CardHeader>
                            <CardContent className="space-y-4">
                                <div className="space-y-3">
                                    <div className="flex items-center justify-between">
                                        <span className="text-sm text-muted-foreground">{t('TOWER_TODAY_NEW')}</span>
                                        <Badge variant="secondary" className="text-base">
                                            {t('TOWER_FLOORS', [infiniteProgress?.todayClearNewFloorCount || 0])}
                                        </Badge>
                                    </div>
                                    <div className="flex items-center justify-between">
                                        <span className="text-sm text-muted-foreground">{t('TOWER_TODAY_DONE')}</span>
                                        <Badge variant="secondary" className="text-base">
                                            {t('GUILD_TIMES', [infiniteProgress?.todayBattleCount || 0])}
                                        </Badge>
                                    </div>
                                    <p className="text-xs text-muted-foreground">
                                        {t('TOWER_RESET_HINT')}
                                    </p>
                                </div>

                                <div className="pt-2 space-y-2">
                                    <Button
                                        className="w-full"
                                        size="lg"
                                        onClick={() => {
                                            if (nextInfiniteQuest) {
                                                handleChallenge(TowerType.Infinite, nextInfiniteQuest.id);
                                            }
                                        }}
                                    >
                                        <ChevronUp className="mr-2 h-5 w-5" />
                                        {t('TOWER_CONTINUE', [currentInfiniteFloor + 1])}
                                    </Button>
                                    <Button
                                        className="w-full"
                                        variant="outline"
                                        disabled={remainingQuickCount <= 0}
                                        onClick={() => {
                                            const currentQuest = infiniteQuests.find(q => q.floor === currentInfiniteFloor);
                                            if (currentQuest) {
                                                handleQuickChallenge(TowerType.Infinite, currentQuest.id);
                                            }
                                        }}
                                    >
                                        <Zap className="mr-2 h-4 w-4" />
                                        {t('TOWER_QUICK', [remainingQuickCount])}
                                    </Button>
                                </div>
                            </CardContent>
                        </Card>
                    </div>

                    <Card>
                        <CardHeader>
                            <CardTitle>{t('TOWER_FLOOR_STATUS')}</CardTitle>
                            <CardDescription>{t('TOWER_FLOOR_STATUS_DESC')}</CardDescription>
                        </CardHeader>
                        <CardContent>
                            <div className="space-y-2">
                                {displayInfiniteQuests.map((quest) => {
                                    const isCleared = quest.floor <= currentInfiniteFloor;
                                    const isCurrent = quest.floor === currentInfiniteFloor + 1;

                                    return (
                                        <div
                                            key={quest.id}
                                            className={`p-4 rounded-lg border transition-all ${isCleared
                                                ? 'border-green-200 dark:border-green-800 bg-green-50 dark:bg-green-950/30'
                                                : isCurrent
                                                    ? 'border-primary bg-primary/5'
                                                    : 'border-gray-200 dark:border-gray-800'
                                                }`}
                                        >
                                            <div className="flex items-center justify-between">
                                                <div className="flex items-center gap-4">
                                                    <div className={`flex items-center justify-center w-14 h-14 rounded-full ${isCleared
                                                        ? 'bg-green-500 text-white'
                                                        : isCurrent
                                                            ? 'bg-primary text-primary-foreground'
                                                            : 'bg-gray-200 dark:bg-gray-800'
                                                        }`}>
                                                        <span className="text-lg font-bold">{quest.floor}</span>
                                                    </div>

                                                    <div className="flex-1">
                                                        <div className="flex items-center gap-2 mb-1">
                                                            <span className="font-semibold">{t('TOWER_FLOOR', [quest.floor])}</span>
                                                            {isCleared ? (
                                                                <Badge variant="outline" className="border-green-500 text-green-500">{t('BATTLE_CLEARED')}</Badge>
                                                            ) : isCurrent ? (
                                                                <Badge>{t('TOWER_IN_PROGRESS')}</Badge>
                                                            ) : (
                                                                <Lock className="h-4 w-4 text-muted-foreground" />
                                                            )}
                                                        </div>
                                                        <div className="flex items-center gap-3 text-sm text-muted-foreground">
                                                            <span>{t('CAVE_RECOMMENDED_POWER', [quest.baseClearPartyDeckPower?.toLocaleString()])}</span>
                                                            <span>•</span>
                                                            <span>{renderRewards(quest.battleRewardsFirst)}</span>
                                                        </div>
                                                    </div>
                                                </div>

                                                {isCurrent && (
                                                    <Button size="sm" onClick={() => handleChallenge(TowerType.Infinite, quest.id)}>
                                                        <ChevronUp className="mr-1 h-4 w-4" />
                                                        {t('PVP_CHALLENGE')}
                                                    </Button>
                                                )}
                                                {!isCleared && !isCurrent && (
                                                    <Button size="sm" disabled variant="ghost">
                                                        <Lock className="mr-1 h-4 w-4" />
                                                        {t('[MissionLockedButton]')}
                                                    </Button>
                                                )}
                                            </div>
                                        </div>
                                    );
                                })}
                            </div>
                        </CardContent>
                    </Card>
                </TabsContent>

                {/* 属性塔 */}
                <TabsContent value="elemental" className="space-y-6">
                    <div className="grid gap-6 md:grid-cols-2">
                        {[TowerType.Blue, TowerType.Red, TowerType.Green, TowerType.Yellow].map((type) => {
                            const config = TOWER_CONFIGS[type];
                            const progress = elementalProgress.find(p => p.towerType === type);
                            const isAvailable = availableTowers.includes(type);
                            const Icon = config.icon;

                            const towerQuestsOfType = allTowerQuests[type] || [];
                            const currentFloor = progress?.maxTowerBattleId || 0;
                            const maxFloor = towerQuestsOfType.length > 0 ? towerQuestsOfType[towerQuestsOfType.length - 1].floor : 0;

                            return (
                                <Card
                                    key={type}
                                    className={`relative overflow-hidden transition-all ${isAvailable
                                        ? 'border-2 border-primary shadow-lg'
                                        : 'opacity-60'
                                        }`}
                                >
                                    <div className={`absolute top-0 right-0 w-full h-2 bg-gradient-to-r ${getElementColor(config.color)}`} />

                                    <CardHeader>
                                        <div className="flex items-center justify-between">
                                            <div className="flex items-center gap-3">
                                                <div className={`p-3 rounded-lg bg-gradient-to-br ${getElementColor(config.color)}`}>
                                                    <Icon className="h-6 w-6 text-white" />
                                                </div>
                                                <div>
                                                    <CardTitle>{config.nameKey ? t(config.nameKey) : config.name}</CardTitle>
                                                    <CardDescription>
                                                        {isAvailable ? t('TOWER_OPEN_TODAY') : t('TOWER_CLOSED_TODAY')}
                                                    </CardDescription>
                                                </div>
                                            </div>
                                            {isAvailable ? (
                                                <Badge className="bg-green-500">
                                                    <Unlock className="h-3 w-3 mr-1" />
                                                    {t('TOWER_OPEN')}
                                                </Badge>
                                            ) : (
                                                <Badge variant="secondary">
                                                    <Lock className="h-3 w-3 mr-1" />
                                                    {t('TOWER_CLOSED')}
                                                </Badge>
                                            )}
                                        </div>
                                    </CardHeader>

                                    <CardContent className="space-y-4">
                                        <div className="space-y-2">
                                            <div className="flex items-center justify-between text-sm">
                                                <span className="text-muted-foreground">{t('TOWER_CURRENT_PROGRESS')}</span>
                                                <span className="font-semibold">
                                                    {t('TOWER_FLOOR_PROGRESS', [currentFloor, maxFloor])}
                                                </span>
                                            </div>
                                            <Progress
                                                value={maxFloor > 0 ? (currentFloor / maxFloor) * 100 : 0}
                                                className="h-2"
                                            />
                                        </div>

                                        <div className="flex items-center justify-between p-3 bg-muted rounded-lg">
                                            <span className="text-sm text-muted-foreground">{t('TOWER_TODAY_NEW')}</span>
                                            <Badge variant={(progress?.todayClearNewFloorCount || 0) >= 10 ? "default" : "secondary"}>
                                                {t('TOWER_FLOOR_PROGRESS', [progress?.todayClearNewFloorCount || 0, 10])}
                                            </Badge>
                                        </div>

                                        <div className="p-3 bg-gradient-to-r from-blue-50 to-purple-50 dark:from-blue-950 dark:to-purple-950 rounded-lg border">
                                            <div className="text-xs text-muted-foreground mb-1">{t('TOWER_RULES')}</div>
                                            <div className="text-sm">
                                                • {t('TOWER_RULE_ELEMENT_PREFIX')}<span className="font-semibold text-primary">{config.elementKey ? t(config.elementKey) : config.element}</span>{t('TOWER_RULE_ELEMENT_SUFFIX')}
                                            </div>
                                            <div className="text-sm">
                                                • {t('TOWER_RULE_MAX10')}
                                            </div>
                                        </div>

                                        {isAvailable ? (
                                            <Button
                                                className="w-full"
                                                onClick={() => {
                                                    const nextQuest = towerQuestsOfType.find(q => q.floor === currentFloor + 1);
                                                    if (nextQuest) {
                                                        handleChallenge(type, nextQuest.id);
                                                    }
                                                }}
                                            >
                                                <ChevronUp className="mr-2 h-4 w-4" />
                                                {t('TOWER_START', [currentFloor + 1])}
                                            </Button>
                                        ) : (
                                            <Button className="w-full" disabled>
                                                <Lock className="mr-2 h-4 w-4" />
                                                {t('TOWER_CLOSED_TODAY')}
                                            </Button>
                                        )}
                                    </CardContent>
                                </Card>
                            );
                        })}
                    </div>

                    {/* 属性塔说明 */}
                    <Card>
                        <CardHeader>
                            <CardTitle className="flex items-center gap-2">
                                <Trophy className="h-5 w-5" />
                                {t('TOWER_ROTATION')}
                            </CardTitle>
                        </CardHeader>
                        <CardContent>
                            <div className="grid grid-cols-2 md:grid-cols-4 gap-4 text-xs md:text-sm">
                                <div className="p-3 border rounded-lg text-center">
                                    <div className="text-muted-foreground mb-1">{t('TOWER_DAYS_BLUE')}</div>
                                    <Droplet className="h-6 w-6 mx-auto mb-1 text-blue-500" />
                                    <div className="font-semibold">{t('[TowerTypeBlue]')}</div>
                                </div>
                                <div className="p-3 border rounded-lg text-center">
                                    <div className="text-muted-foreground mb-1">{t('TOWER_DAYS_RED')}</div>
                                    <Flame className="h-6 w-6 mx-auto mb-1 text-red-500" />
                                    <div className="font-semibold">{t('[TowerTypeRed]')}</div>
                                </div>
                                <div className="p-3 border rounded-lg text-center">
                                    <div className="text-muted-foreground mb-1">{t('TOWER_DAYS_GREEN')}</div>
                                    <Wind className="h-6 w-6 mx-auto mb-1 text-green-500" />
                                    <div className="font-semibold">{t('[TowerTypeGreen]')}</div>
                                </div>
                                <div className="p-3 border rounded-lg text-center">
                                    <div className="text-muted-foreground mb-1">{t('TOWER_DAYS_YELLOW')}</div>
                                    <Mountain className="h-6 w-6 mx-auto mb-1 text-yellow-500" />
                                    <div className="font-semibold">{t('[TowerTypeYellow]')}</div>
                                </div>
                            </div>
                        </CardContent>
                    </Card>
                </TabsContent>

                {/* 自动爬塔 */}
                <TabsContent value="auto" className="space-y-6">
                    <div className="grid gap-6 md:grid-cols-2">
                        {/* 控制面板 */}
                        <Card>
                            <CardHeader>
                                <CardTitle className="flex items-center gap-2">
                                    <Play className="h-5 w-5" />
                                    {t('AUTO_TOWER_TITLE')}
                                </CardTitle>
                                <CardDescription>
                                    {t('AUTO_TOWER_DESC')}
                                </CardDescription>
                            </CardHeader>
                            <CardContent className="space-y-4">
                                <div className="space-y-2">
                                    <Label>{t('AUTO_TOWER_SELECT_TYPE')}</Label>
                                    <Select
                                        value={selectedTowerType.toString()}
                                        onValueChange={(value) => setSelectedTowerType(Number(value) as TowerType)}
                                        disabled={taskStatus?.isAutoTowerRunning || false}
                                    >
                                        <SelectTrigger>
                                            <SelectValue placeholder={t('AUTO_TOWER_SELECT_TYPE_PLACEHOLDER')} />
                                        </SelectTrigger>
                                        <SelectContent>
                                            {availableTowers.includes(TowerType.Infinite) && (
                                                <SelectItem value={TowerType.Infinite.toString()}>{t('AUTO_TOWER_INFINITE')}</SelectItem>
                                            )}
                                            {availableTowers.includes(TowerType.Blue) && (
                                                <SelectItem value={TowerType.Blue.toString()}>{t('AUTO_TOWER_BLUE')}</SelectItem>
                                            )}
                                            {availableTowers.includes(TowerType.Red) && (
                                                <SelectItem value={TowerType.Red.toString()}>{t('AUTO_TOWER_RED')}</SelectItem>
                                            )}
                                            {availableTowers.includes(TowerType.Green) && (
                                                <SelectItem value={TowerType.Green.toString()}>{t('AUTO_TOWER_GREEN')}</SelectItem>
                                            )}
                                            {availableTowers.includes(TowerType.Yellow) && (
                                                <SelectItem value={TowerType.Yellow.toString()}>{t('AUTO_TOWER_YELLOW')}</SelectItem>
                                            )}
                                        </SelectContent>
                                    </Select>
                                    <p className="text-xs text-muted-foreground">
                                        {t('AUTO_TOWER_DAILY_LIMIT_HINT')}
                                    </p>
                                </div>

                                <div className="space-y-2">
                                    <Label>{t('AUTO_TOWER_TARGET_FLOOR')}</Label>
                                    <div className="flex gap-2 items-center">
                                        <Select
                                            value={targetFloor}
                                            onValueChange={setTargetFloor}
                                            disabled={taskStatus?.isAutoTowerRunning || false}
                                        >
                                            <SelectTrigger>
                                                <SelectValue placeholder={t('AUTO_TOWER_TARGET_FLOOR_PLACEHOLDER')} />
                                            </SelectTrigger>
                                            <SelectContent>
                                                <SelectItem value="0">{t('AUTO_TOWER_UNLIMITED')}</SelectItem>
                                                {targetFloorOptions.map(opt => (
                                                    <SelectItem key={opt.floor} value={opt.floor.toString()}>
                                                        {opt.name}
                                                    </SelectItem>
                                                ))}
                                            </SelectContent>
                                        </Select>
                                        <Button
                                            variant="outline"
                                            size="icon"
                                            onClick={() => fetchUserData(true)}
                                            title={t('AUTO_TOWER_REFRESH')}
                                        >
                                            <RefreshCw className="h-4 w-4" />
                                        </Button>
                                    </div>
                                    <p className="text-xs text-muted-foreground">
                                        {t('AUTO_TOWER_CLEARED_FLOOR').replace('{0}', maxClearedFloor.toString())}
                                    </p>
                                </div>

                                <div className="flex gap-2">
                                    <Button
                                        className="flex-1"
                                        onClick={handleStartAutoTower}
                                        disabled={taskStatus?.isAutoTowerRunning || false}
                                    >
                                        <Play className="mr-2 h-4 w-4" />
                                        {t('AUTO_TOWER_START')}
                                    </Button>
                                    <Button
                                        className="flex-1"
                                        variant="destructive"
                                        onClick={handleStopAutoTower}
                                        disabled={!taskStatus?.isAutoTowerRunning}
                                    >
                                        <Square className="mr-2 h-4 w-4" />
                                        {t('AUTO_TOWER_STOP')}
                                    </Button>
                                </div>

                                {/* 状态显示 */}
                                {taskStatus?.autoTowerProgress && (
                                    <div className="mt-4 p-3 bg-muted rounded-lg space-y-2">
                                        <div className="flex items-center justify-between text-sm">
                                            <span className="text-muted-foreground">{t('AUTO_TOWER_STATUS')}</span>
                                            <Badge variant={taskStatus.isAutoTowerRunning ? "default" : "secondary"}>
                                                {taskStatus.isAutoTowerRunning ? t('AUTO_TOWER_RUNNING') : t('AUTO_TOWER_STOPPED')}
                                            </Badge>
                                        </div>
                                        <div className="flex items-center justify-between text-sm">
                                            <span className="text-muted-foreground">{t('AUTO_TOWER_TOWER_TYPE')}</span>
                                            <span className="font-medium">
                                                {taskStatus.autoTowerProgress.towerType === TowerType.Infinite ? t('AUTO_TOWER_INFINITE') :
                                                 taskStatus.autoTowerProgress.towerType === TowerType.Blue ? t('AUTO_TOWER_BLUE') :
                                                 taskStatus.autoTowerProgress.towerType === TowerType.Red ? t('AUTO_TOWER_RED') :
                                                 taskStatus.autoTowerProgress.towerType === TowerType.Green ? t('AUTO_TOWER_GREEN') :
                                                 taskStatus.autoTowerProgress.towerType === TowerType.Yellow ? t('AUTO_TOWER_YELLOW') : t('AUTO_TOWER_UNKNOWN')}
                                            </span>
                                        </div>
                                        <div className="grid grid-cols-3 gap-2 text-center">
                                            <div>
                                                <div className="text-lg font-bold">{taskStatus.autoTowerProgress.totalCount}</div>
                                                <div className="text-xs text-muted-foreground">{t('AUTO_TOWER_TOTAL_COUNT')}</div>
                                            </div>
                                            <div>
                                                <div className="text-lg font-bold text-green-600">{taskStatus.autoTowerProgress.winCount}</div>
                                                <div className="text-xs text-muted-foreground">{t('AUTO_TOWER_WIN_COUNT')}</div>
                                            </div>
                                            <div>
                                                <div className="text-lg font-bold text-red-600">{taskStatus.autoTowerProgress.errorCount}</div>
                                                <div className="text-xs text-muted-foreground">{t('AUTO_TOWER_ERROR_COUNT')}</div>
                                            </div>
                                        </div>
                                        {taskStatus.autoTowerProgress.currentFloor > 0 && (
                                            <div className="text-sm">
                                                {t('AUTO_TOWER_CURRENT_FLOOR').replace('{0}', taskStatus.autoTowerProgress.currentFloor.toString())}
                                            </div>
                                        )}
                                        {taskStatus.autoTowerProgress.towerType !== TowerType.Infinite && (
                                            <div className="text-sm text-muted-foreground">
                                                {t('AUTO_TOWER_TODAY_CLEAR').replace('{0}', taskStatus.autoTowerProgress.todayClearCount.toString())}
                                            </div>
                                        )}
                                    </div>
                                )}
                            </CardContent>
                        </Card>

                        {/* 日志面板 */}
                        <Card>
                            <CardHeader>
                                <CardTitle className="flex items-center gap-2">
                                    <BookOpen className="h-5 w-5" />
                                    {t('AUTO_TOWER_LOG_TITLE')}
                                </CardTitle>
                            </CardHeader>
                            <CardContent>
                                <ScrollArea className="h-[400px]">
                                    {towerLogs.length > 0 ? (
                                        <div className="space-y-1 font-mono text-sm">
                                            {towerLogs.map((log, index) => (
                                                <div key={index} className="p-1 hover:bg-muted rounded">
                                                    {log}
                                                </div>
                                            ))}
                                            <div ref={logEndRef} />
                                        </div>
                                    ) : (
                                        <div className="text-center text-muted-foreground py-8">
                                            {t('AUTO_TOWER_NO_LOGS')}
                                        </div>
                                    )}
                                </ScrollArea>
                            </CardContent>
                        </Card>
                    </div>
                </TabsContent>
            </Tabs>
        </div>
    );
}
