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
    Sword,
    Zap,
    Trophy,
    Users,
    Star,
    Clock,
    Target,
    TrendingUp,
    Shield,
    Coins,
    BookOpen,
    Loader2,
    Play,
    Square,
    RefreshCw
} from 'lucide-react';
import { ortegaApi } from '@/api/ortega-client';
import { backgroundTaskApi } from '@/api/background-task-service';
import { createJobHubConnection } from '@/api/signalr-client';
import { useMasterTable } from '@/hooks/useMasterData';
import { useTranslation } from '@/hooks/useTranslation';
import { useAccountStore } from '@/store/accountStore';
import { UserSyncData } from '@/api/generated/userSyncData';
import { UserBattleAutoDtoInfo } from '@/api/generated/userBattleAutoDtoInfo';
import { QuestMB } from '@/api/generated/questMB';
import { ChapterMB } from '@/api/generated/chapterMB';
import { BackgroundTaskStatus } from '@/api/generated/backgroundTaskStatus';
import { toast } from '@/hooks/use-toast';
import { QuestQuickExecuteType } from '@/api/generated/questQuickExecuteType';
import { BattleFieldCharacterGroupType } from '@/api/generated/battleFieldCharacterGroupType';
import type { HubConnection } from '@microsoft/signalr';

export function BattlePage() {
    const { t } = useTranslation();
    const { currentAccountId } = useAccountStore();
    const [userData, setUserData] = useState<UserSyncData | null>(null);
    const [battleAutoData, setBattleAutoData] = useState<UserBattleAutoDtoInfo | null>(null);
    const [isLoading, setIsLoading] = useState(true);
    const [currentTime, setCurrentTime] = useState(Date.now());

    // 自动刷主线状态
    const [taskStatus, setTaskStatus] = useState<BackgroundTaskStatus | null>(null);
    const [targetQuestId, setTargetQuestId] = useState<string>('0');
    const [bossLogs, setBossLogs] = useState<string[]>([]);
    const jobHubRef = useRef<HubConnection | null>(null);
    const logEndRef = useRef<HTMLDivElement>(null);

    // 日志自动滚动到底部
    useEffect(() => {
        logEndRef.current?.scrollIntoView({ behavior: 'smooth' });
    }, [bossLogs]);

    // 加载 Master 数据
    const { data: questTable } = useMasterTable<QuestMB>('QuestTable');
    const { data: chapterTable } = useMasterTable<ChapterMB>('ChapterTable');

    const loadUserData = async () => {
        try {
            const [userResponse, autoResponse] = await Promise.all([
                ortegaApi.user.getUserData({}),
                ortegaApi.battle.auto({})
            ]);
            if (userResponse.userSyncData) {
                setUserData(userResponse.userSyncData);
            }
            if (autoResponse.userBattleAuto) {
                setBattleAutoData(autoResponse.userBattleAuto);
            }
        } catch (error) {
            console.error('Failed to load user data:', error);
            toast({
                title: t('BATTLE_LOAD_FAILED'),
                description: t('BATTLE_LOAD_FAILED_DESC'),
                variant: 'destructive',
            });
        } finally {
            setIsLoading(false);
        }
    };

    useEffect(() => {
        loadUserData();
        // 每分钟更新一次当前时间，用于计算挂机收益
        const timer = setInterval(() => setCurrentTime(Date.now()), 60000);
        return () => clearInterval(timer);
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
            setBossLogs(prev => [...prev.slice(-99), message]);
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

    // 开始自动刷主线
    const handleStartAutoBoss = async () => {
        if (!currentAccountId) return;
        try {
            const target = parseInt(targetQuestId) || 0;
            await backgroundTaskApi.startAutoBoss(currentAccountId, target > 0 ? target : undefined);
            toast({ title: t('BATTLE_TASK_STARTED'), description: t('BATTLE_AUTO_MAIN_STARTED') });
            fetchTaskStatus();
        } catch (error) {
            console.error('Failed to start auto boss:', error);
            toast({ title: t('BATTLE_START_FAILED'), description: t('BATTLE_AUTO_MAIN_START_FAILED'), variant: 'destructive' });
        }
    };

    // 停止自动刷主线
    const handleStopAutoBoss = async () => {
        if (!currentAccountId) return;
        try {
            await backgroundTaskApi.stopAutoBoss(currentAccountId);
            toast({ title: t('BATTLE_TASK_STOPPED'), description: t('BATTLE_AUTO_MAIN_STOPPED') });
            fetchTaskStatus();
        } catch (error) {
            console.error('Failed to stop auto boss:', error);
            toast({ title: t('BATTLE_STOP_FAILED'), description: t('BATTLE_AUTO_MAIN_STOP_FAILED'), variant: 'destructive' });
        }
    };

    // 计算逻辑
    const maxQuestId = userData?.userBattleBossDtoInfo?.bossClearMaxQuestId || 0;

    // 当前正在挂机的关卡（通常是已通关的最大关卡）
    const currentQuest = useMemo(() => {
        if (!questTable) return null;
        return questTable.find(q => q.id === maxQuestId) || questTable[0];
    }, [questTable, maxQuestId]);

    // 下一个要挑战的关卡
    const nextQuest = useMemo(() => {
        if (!questTable) return null;
        return questTable.find(q => q.id === maxQuestId + 1);
    }, [questTable, maxQuestId]);

    // 自动战斗时长计算
    const autoBattleStats = useMemo(() => {
        if (!userData || !currentQuest) return { timeStr: t('[CommonMinuteFormat]', [0]), minutes: 0, gold: 0, exp: 0, progress: 0 };

        const lastRewardTime = (userData.receivedAutoBattleRewardLastTime || 0) * 1000;
        const diffMs = Math.max(0, currentTime - lastRewardTime);
        const diffMinutes = Math.floor(diffMs / 60000);

        const hours = Math.floor(diffMinutes / 60);
        const mins = diffMinutes % 60;

        return {
            timeStr: hours > 0 ? t('[CommonRemainTimeHoursAndMinutes]', [hours, mins]) : t('[CommonMinuteFormat]', [mins]),
            minutes: diffMinutes,
            gold: diffMinutes * (currentQuest.goldPerMinute || 0),
            exp: diffMinutes * (currentQuest.minPlayerExp || 0),
            progress: Math.min(100, (diffMinutes / (24 * 60)) * 100) // 假设24小时满溢
        };
    }, [userData, currentQuest, currentTime, t]);

    // 领民数据汇总
    const citizenStats = useMemo(() => {
        if (!questTable || !chapterTable) return { total: 0, byRegion: [] };

        const stats = chapterTable.map(chapter => {
            const chapterQuests = questTable.filter(q => q.chapterId === chapter.id && q.id <= maxQuestId);
            const population = chapterQuests.reduce((sum, q) => sum + (q.population || 0), 0);
            return {
                name: t(chapter.territoryNameKey) || t('BATTLE_CHAPTER', [chapter.id]),
                count: population,
                bonus: t('BATTLE_INCOME_BONUS', [Math.floor(population / 100)]) // 模拟加成逻辑
            };
        }).filter(s => s.count > 0);

        const total = stats.reduce((sum, s) => sum + s.count, 0);
        return { total, byRegion: stats };
    }, [questTable, chapterTable, maxQuestId, t]);

    // 自动刷主线目标关卡选项（显示未来50个关卡）
    const targetQuestOptions = useMemo(() => {
        if (!questTable || questTable.length === 0) return [];

        // 解析 Memo 格式 "章节-关卡号" 用于排序
        const parseMemo = (memo: string): [number, number] => {
            if (!memo) return [0, 0];
            const parts = memo.split('-');
            if (parts.length === 2) {
                return [parseInt(parts[0]) || 0, parseInt(parts[1]) || 0];
            }
            return [0, 0];
        };

        return [...questTable]
            .filter(q => q.baseBattlePower > 0 && q.id > maxQuestId)
            .sort((a, b) => {
                // 先按 chapterId 排序，再按 Memo 中的关卡号排序
                if (a.chapterId !== b.chapterId) {
                    return a.chapterId - b.chapterId;
                }
                const [, stageA] = parseMemo(a.memo);
                const [, stageB] = parseMemo(b.memo);
                return stageA - stageB;
            })
            .slice(0, 50)
            .map(q => ({
                id: q.id,
                name: q.memo || t('BATTLE_STAGE', [q.id])
            }));
    }, [questTable, maxQuestId, t]);

    // 关卡列表显示逻辑（当前关卡前后5关）
    const displayStages = useMemo(() => {
        if (!questTable || questTable.length === 0) return [];

        const sortedQuests = [...questTable].sort((a, b) => a.id - b.id);
        const currentIndex = sortedQuests.findIndex(q => q.id === maxQuestId);
        const centerIndex = currentIndex >= 0 ? currentIndex : 0;
        const start = Math.max(0, centerIndex - 2);
        const end = Math.min(sortedQuests.length, centerIndex + 3);

        return sortedQuests.slice(start, end).map(q => {
            // 优先使用 Memo 字段（格式如 "8-96"）
            const displayName = q.memo || t('BATTLE_STAGE', [q.id]);

            return {
                id: q.id,
                name: displayName,
                cleared: q.id <= maxQuestId,
                stars: q.id <= maxQuestId ? 3 : 0,
                power: q.baseBattlePower
            };
        });
    }, [questTable, maxQuestId, t]);

    // 操作处理
    const handleClaimReward = async () => {
        try {
            await ortegaApi.battle.rewardAutoBattle({});
            toast({ title: t('BATTLE_CLAIM_SUCCESS'), description: t('BATTLE_CLAIM_SUCCESS_DESC') });
            loadUserData(); // 刷新数据
        } catch (error) {
            console.error('Claim reward failed:', error);
            toast({ title: t('BATTLE_CLAIM_FAILED'), description: t('BATTLE_API_ERROR'), variant: 'destructive' });
        }
    };

    const handleQuickBattle = async () => {
        try {
            // 判断使用免费次数还是消耗钻石
            const useFree = battleAutoData && battleAutoData.quickTodayUseCurrencyCount < 1;
            await ortegaApi.battle.quick({
                questQuickExecuteType: useFree ? QuestQuickExecuteType.Currency : QuestQuickExecuteType.Currency,
                quickCount: 1
            });
            toast({ title: t('BATTLE_QUICK_SUCCESS'), description: t('BATTLE_QUICK_SUCCESS_DESC') });
            loadUserData();
        } catch (error) {
            console.error('Quick battle failed:', error);
            toast({ title: t('BATTLE_QUICK_FAILED'), description: t('BATTLE_QUICK_FAILED_DESC'), variant: 'destructive' });
        }
    };

    const handleChallengeBoss = async () => {
        if (!nextQuest) return;
        try {
            const res = await ortegaApi.battle.boss({ questId: nextQuest.id });
            const isWin = res.battleResult?.simulationResult?.battleEndInfo?.winGroupType === BattleFieldCharacterGroupType.Attacker;

            if (isWin) {
                toast({ title: t('BATTLE_CHALLENGE_WIN'), description: t('BATTLE_CLEARED_STAGE', [nextQuest.id]) });
                loadUserData();
            } else {
                toast({ title: t('BATTLE_CHALLENGE_LOSS'), description: t('BATTLE_CHALLENGE_LOSS_DESC'), variant: 'destructive' });
            }
        } catch (error) {
            console.error('Boss battle failed:', error);
            toast({ title: t('BATTLE_CHALLENGE_ERROR'), description: t('BATTLE_SERVER_ERROR'), variant: 'destructive' });
        }
    };

    if (isLoading) {
        return (
            <div className="flex h-[400px] items-center justify-center">
                <Loader2 className="h-8 w-8 animate-spin text-muted-foreground" />
            </div>
        );
    }

    return (
        <div className="space-y-6">
            {/* 主要功能区 */}
            <div className="grid gap-6 md:grid-cols-3">
                {/* 自动战斗 */}
                <Card className="hover:shadow-lg transition-shadow">
                    <CardHeader>
                        <div className="flex items-center gap-3">
                            <div className="p-3 bg-blue-100 dark:bg-blue-900 rounded-lg">
                                <Clock className="h-6 w-6 text-blue-600 dark:text-blue-400" />
                            </div>
                            <div className="flex-1">
                                <CardTitle>{t('[AutoBattleQuestButtonAutoBattle]')}</CardTitle>
                                <CardDescription>{t('BATTLE_AUTO_DESC')}</CardDescription>
                            </div>
                        </div>
                    </CardHeader>
                    <CardContent className="space-y-4">
                        <div className="space-y-2">
                            <div className="flex items-center justify-between text-sm">
                                <span className="text-muted-foreground">{t('BATTLE_ELAPSED')}</span>
                                <span className="font-semibold">{autoBattleStats.timeStr}</span>
                            </div>
                            <Progress value={autoBattleStats.progress} className="h-2" />
                        </div>

                        <div className="grid grid-cols-2 gap-3 text-sm">
                            <div className="flex items-center gap-2 p-2 bg-muted rounded">
                                <Coins className="h-4 w-4 text-yellow-600" />
                                <div>
                                    <div className="text-xs text-muted-foreground">{t('[ItemName5]')}</div>
                                    <div className="font-semibold">{autoBattleStats.gold.toLocaleString()}</div>
                                </div>
                            </div>
                            <div className="flex items-center gap-2 p-2 bg-muted rounded">
                                <Star className="h-4 w-4 text-purple-600" />
                                <div>
                                    <div className="text-xs text-muted-foreground">{t('[ItemName4]')}</div>
                                    <div className="font-semibold">{autoBattleStats.exp.toLocaleString()}</div>
                                </div>
                            </div>
                        </div>

                        <Button className="w-full" onClick={handleClaimReward}>
                            <Target className="mr-2 h-4 w-4" />
                            {t('[CommonClaimLabel]')}
                        </Button>
                    </CardContent>
                </Card>

                {/* 高速战斗 */}
                <Card className="hover:shadow-lg transition-shadow">
                    <CardHeader>
                        <div className="flex items-center gap-3">
                            <div className="p-3 bg-orange-100 dark:bg-orange-900 rounded-lg">
                                <Zap className="h-6 w-6 text-orange-600 dark:text-orange-400" />
                            </div>
                            <div className="flex-1">
                                <CardTitle>{t('[AutoBattleButtonQuickForward]')}</CardTitle>
                                <CardDescription>{t('BATTLE_QUICK_DESC')}</CardDescription>
                            </div>
                        </div>
                    </CardHeader>
                    <CardContent className="space-y-4">
                        <div className="p-3 bg-gradient-to-r from-orange-50 to-yellow-50 dark:from-orange-950 dark:to-yellow-950 rounded-lg border border-orange-200 dark:border-orange-800">
                            <div className="text-sm text-muted-foreground mb-1">{t('BATTLE_EXPECTED')}</div>
                            <div className="flex items-baseline gap-2">
                                <span className="text-2xl font-bold text-orange-600">
                                    ~{Math.round((currentQuest?.goldPerMinute || 0) * 120 / 1000)}K
                                </span>
                                <span className="text-sm text-muted-foreground">{t('[ItemName5]')}</span>
                            </div>
                            <div className="text-xs text-muted-foreground mt-1">
                                {t('BATTLE_OTHER_LOOT')}
                            </div>
                        </div>

                        <div className="space-y-2">
                            <div className="flex items-center justify-between text-sm">
                                <span className="text-muted-foreground">{t('BATTLE_FREE_USES')}</span>
                                <Badge variant={battleAutoData && battleAutoData.quickTodayUseCurrencyCount >= 1 ? "secondary" : "default"}>
                                    {battleAutoData ? (battleAutoData.quickTodayUseCurrencyCount >= 1 ? t('BATTLE_USED_UP') : '1 / 1') : '--'}
                                </Badge>
                            </div>
                            {userData?.isValidContractPrivilege && (
                                <div className="flex items-center justify-between text-sm">
                                    <span className="text-muted-foreground">{t('BATTLE_MONTHLY_PASS')}</span>
                                    <Badge variant="secondary">
                                        {battleAutoData ? `${Math.max(0, 2 - battleAutoData.quickTodayUsePrivilegeCount)} / 2` : '--'}
                                    </Badge>
                                </div>
                            )}
                            <div className="text-xs text-muted-foreground">
                                {t('BATTLE_QUICK_RESET')}
                            </div>
                        </div>

                        <Button className="w-full" variant="default" onClick={handleQuickBattle}>
                            <Zap className="mr-2 h-4 w-4" />
                            {t('BATTLE_START_QUICK')}
                        </Button>
                    </CardContent>
                </Card>

                {/* 挑战首领 */}
                <Card className="hover:shadow-lg transition-shadow">
                    <CardHeader>
                        <div className="flex items-center gap-3">
                            <div className="p-3 bg-red-100 dark:bg-red-900 rounded-lg">
                                <Trophy className="h-6 w-6 text-red-600 dark:text-red-400" />
                            </div>
                            <div className="flex-1">
                                <CardTitle>{t('[AutoBattleButtonBoss]')}</CardTitle>
                                <CardDescription>{t('BATTLE_BOSS_DESC')}</CardDescription>
                            </div>
                        </div>
                    </CardHeader>
                    <CardContent className="space-y-4">
                        <div className="p-3 bg-red-50 dark:bg-red-950 rounded-lg border border-red-200 dark:border-red-800">
                            <div className="flex items-center justify-between mb-2">
                                <span className="text-sm font-medium">{t('BATTLE_CURRENT_BOSS')}</span>
                                <Badge className="bg-red-500">Boss {nextQuest?.id || 'MAX'}</Badge>
                            </div>
                            <div className="flex items-center gap-2">
                                <Shield className="h-4 w-4 text-red-600" />
                                <div className="flex-1">
                                    <div className="text-sm font-semibold">
                                        {nextQuest ? `${nextQuest.chapterId}-${nextQuest.id % 100 || 100}` : t('BATTLE_ALL_CLEARED')}
                                    </div>
                                    <div className="text-xs text-muted-foreground">
                                        {t('CAVE_RECOMMENDED_POWER', [nextQuest?.baseBattlePower.toLocaleString() || '--'])}
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div className="space-y-2">
                            <div className="flex items-center justify-between text-sm">
                                <span className="text-muted-foreground">{t('BATTLE_FREE_CHALLENGES')}</span>
                                <Badge variant="secondary">
                                    {3 - (userData?.userBattleBossDtoInfo?.bossTodayWinCount || 0)} / 3
                                </Badge>
                            </div>
                            <div className="text-xs text-muted-foreground">
                                {t('BATTLE_DAILY_RESET')}
                            </div>
                        </div>

                        <Button
                            className="w-full"
                            variant="destructive"
                            disabled={!nextQuest}
                            onClick={handleChallengeBoss}
                        >
                            <Sword className="mr-2 h-4 w-4" />
                            {t('[AutoBattleButtonBoss]')}
                        </Button>
                    </CardContent>
                </Card>
            </div>

            {/* 关卡列表与领民数据 */}
            <Tabs defaultValue="stages" className="space-y-4">
                <TabsList className="grid w-full grid-cols-4">
                    <TabsTrigger value="stages">{t('BATTLE_TAB_STAGES')}</TabsTrigger>
                    <TabsTrigger value="autoboss">{t('BATTLE_TAB_AUTOBOSS')}</TabsTrigger>
                    <TabsTrigger value="stats">{t('[DialogAutoBattleStatisticsTabBattle]')}</TabsTrigger>
                    <TabsTrigger value="citizens">{t('[DialogAutoBattleStatisticsTabPopulation]')}</TabsTrigger>
                </TabsList>

                {/* 关卡进度 */}
                <TabsContent value="stages" className="space-y-4">
                    <div className="grid gap-4">
                        {displayStages.map((stage) => (
                            <Card key={stage.id} className={stage.cleared ? 'border-green-200 dark:border-green-800' : ''}>
                                <CardContent className="p-4">
                                    <div className="flex items-center justify-between">
                                        <div className="flex items-center gap-4 flex-1">
                                            <div className="flex flex-col items-center">
                                                <div className={`w-12 h-12 rounded-full flex items-center justify-center ${stage.cleared
                                                    ? 'bg-green-100 dark:bg-green-900'
                                                    : 'bg-gray-100 dark:bg-gray-800'
                                                    }`}>
                                                    {stage.cleared ? (
                                                        <TrendingUp className="h-6 w-6 text-green-600" />
                                                    ) : (
                                                        <Target className="h-6 w-6 text-gray-400" />
                                                    )}
                                                </div>
                                            </div>

                                            <div className="flex-1">
                                                <div className="flex items-center gap-2">
                                                    <h3 className="font-semibold">{stage.name}</h3>
                                                    {stage.cleared && (
                                                        <Badge variant="secondary">{t('BATTLE_CLEARED')}</Badge>
                                                    )}
                                                </div>
                                                <div className="flex items-center gap-3 mt-1">
                                                    <span className="text-sm text-muted-foreground">
                                                        {t('CAVE_RECOMMENDED_POWER', [stage.power.toLocaleString()])}
                                                    </span>
                                                    {stage.cleared && (
                                                        <div className="flex items-center gap-1">
                                                            {[1, 2, 3].map((star) => (
                                                                <Star
                                                                    key={star}
                                                                    className={`h-4 w-4 ${star <= stage.stars
                                                                        ? 'text-yellow-500 fill-yellow-500'
                                                                        : 'text-gray-300'
                                                                        }`}
                                                                />
                                                            ))}
                                                        </div>
                                                    )}
                                                </div>
                                            </div>
                                        </div>

                                        <div className="flex gap-2">
                                            {stage.cleared && (
                                                <Button variant="outline" size="sm">
                                                    {t('[AutoBattleQuestButtonAutoBattle]')}
                                                </Button>
                                            )}
                                            <Button
                                                size="sm"
                                                variant={stage.cleared ? 'secondary' : 'default'}
                                                onClick={() => !stage.cleared && handleChallengeBoss()}
                                            >
                                                {stage.cleared ? t('BATTLE_RECHALLENGE') : t('GUILD_START_CHALLENGE')}
                                            </Button>
                                        </div>
                                    </div>
                                </CardContent>
                            </Card>
                        ))}
                    </div>
                </TabsContent>

                {/* 自动刷主线 */}
                <TabsContent value="autoboss" className="space-y-4">
                    <div className="grid gap-6 md:grid-cols-2">
                        {/* 控制面板 */}
                        <Card>
                            <CardHeader>
                                <CardTitle className="flex items-center gap-2">
                                    <Play className="h-5 w-5" />
                                    {t('AUTO_BOSS_TITLE')}
                                </CardTitle>
                                <CardDescription>
                                    {t('AUTO_BOSS_DESC')}
                                </CardDescription>
                            </CardHeader>
                            <CardContent className="space-y-4">
                                <div className="space-y-2">
                                    <Label>{t('AUTO_BOSS_TARGET_QUEST')}</Label>
                                    <div className="flex gap-2 items-center">
                                        <Select
                                            value={targetQuestId}
                                            onValueChange={setTargetQuestId}
                                            disabled={taskStatus?.isAutoBossRunning || false}
                                        >
                                            <SelectTrigger>
                                                <SelectValue placeholder={t('AUTO_BOSS_TARGET_QUEST_PLACEHOLDER')} />
                                            </SelectTrigger>
                                            <SelectContent>
                                                <SelectItem value="0">{t('AUTO_BOSS_UNLIMITED')}</SelectItem>
                                                {targetQuestOptions.map(quest => (
                                                    <SelectItem key={quest.id} value={quest.id.toString()}>
                                                        {quest.name}
                                                    </SelectItem>
                                                ))}
                                            </SelectContent>
                                        </Select>
                                        <Button
                                            variant="outline"
                                            size="icon"
                                            onClick={loadUserData}
                                            title={t('AUTO_BOSS_REFRESH')}
                                        >
                                            <RefreshCw className="h-4 w-4" />
                                        </Button>
                                    </div>
                                    <p className="text-xs text-muted-foreground">
                                        {t('AUTO_BOSS_TARGET_QUEST_HINT')}
                                    </p>
                                </div>

                                <div className="flex gap-2">
                                    <Button
                                        className="flex-1"
                                        onClick={handleStartAutoBoss}
                                        disabled={taskStatus?.isAutoBossRunning || false}
                                    >
                                        <Play className="mr-2 h-4 w-4" />
                                        {t('AUTO_BOSS_START')}
                                    </Button>
                                    <Button
                                        className="flex-1"
                                        variant="destructive"
                                        onClick={handleStopAutoBoss}
                                        disabled={!taskStatus?.isAutoBossRunning}
                                    >
                                        <Square className="mr-2 h-4 w-4" />
                                        {t('AUTO_BOSS_STOP')}
                                    </Button>
                                </div>

                                {/* 状态显示 */}
                                {taskStatus?.autoBossProgress && (
                                    <div className="mt-4 p-3 bg-muted rounded-lg space-y-2">
                                        <div className="flex items-center justify-between text-sm">
                                            <span className="text-muted-foreground">{t('AUTO_BOSS_STATUS')}</span>
                                            <Badge variant={taskStatus.isAutoBossRunning ? "default" : "secondary"}>
                                                {taskStatus.isAutoBossRunning ? t('AUTO_BOSS_RUNNING') : t('AUTO_BOSS_STOPPED')}
                                            </Badge>
                                        </div>
                                        <div className="grid grid-cols-3 gap-2 text-center">
                                            <div>
                                                <div className="text-lg font-bold">{taskStatus.autoBossProgress.totalCount}</div>
                                                <div className="text-xs text-muted-foreground">{t('AUTO_BOSS_TOTAL_COUNT')}</div>
                                            </div>
                                            <div>
                                                <div className="text-lg font-bold text-green-600">{taskStatus.autoBossProgress.winCount}</div>
                                                <div className="text-xs text-muted-foreground">{t('AUTO_BOSS_WIN_COUNT')}</div>
                                            </div>
                                            <div>
                                                <div className="text-lg font-bold text-red-600">{taskStatus.autoBossProgress.errorCount}</div>
                                                <div className="text-xs text-muted-foreground">{t('AUTO_BOSS_ERROR_COUNT')}</div>
                                            </div>
                                        </div>
                                        {taskStatus.autoBossProgress.currentQuestId > 0 && (
                                            <div className="text-sm">
                                                {t('AUTO_BOSS_CURRENT_QUEST').replace('{0}', taskStatus.autoBossProgress.currentQuestId.toString())}
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
                                    {t('AUTO_BOSS_LOG_TITLE')}
                                </CardTitle>
                            </CardHeader>
                            <CardContent>
                                <ScrollArea className="h-[300px]">
                                    {bossLogs.length > 0 ? (
                                        <div className="space-y-1 font-mono text-sm">
                                            {bossLogs.map((log, index) => (
                                                <div key={index} className="p-1 hover:bg-muted rounded">
                                                    {log}
                                                </div>
                                            ))}
                                            <div ref={logEndRef} />
                                        </div>
                                    ) : (
                                        <div className="text-center text-muted-foreground py-8">
                                            {t('AUTO_BOSS_NO_LOGS')}
                                        </div>
                                    )}
                                </ScrollArea>
                            </CardContent>
                        </Card>
                    </div>
                </TabsContent>

                {/* 战斗数据 */}
                <TabsContent value="stats" className="space-y-4">
                    <Card>
                        <CardHeader>
                            <CardTitle>{t('BATTLE_AUTO_STATS')}</CardTitle>
                            <CardDescription>
                                {t('BATTLE_AUTO_STATS_DESC')}
                            </CardDescription>
                        </CardHeader>
                        <CardContent>
                            <div className="grid gap-4 md:grid-cols-2 lg:grid-cols-4">
                                <div className="p-4 bg-gradient-to-br from-yellow-50 to-yellow-100 dark:from-yellow-950 dark:to-yellow-900 rounded-lg">
                                    <div className="flex items-center gap-2 mb-2">
                                        <Coins className="h-5 w-5 text-yellow-600" />
                                        <span className="text-sm font-medium text-muted-foreground">{t('BATTLE_GOLD_PER_HOUR')}</span>
                                    </div>
                                    <div className="text-2xl font-bold">
                                        {((currentQuest?.goldPerMinute || 0) * 60).toLocaleString()}
                                    </div>
                                </div>

                                <div className="p-4 bg-gradient-to-br from-purple-50 to-purple-100 dark:from-purple-950 dark:to-purple-900 rounded-lg">
                                    <div className="flex items-center gap-2 mb-2">
                                        <Star className="h-5 w-5 text-purple-600" />
                                        <span className="text-sm font-medium text-muted-foreground">{t('BATTLE_EXP_PER_HOUR')}</span>
                                    </div>
                                    <div className="text-2xl font-bold">
                                        {((currentQuest?.minPlayerExp || 0) * 60).toLocaleString()}
                                    </div>
                                </div>

                                <div className="p-4 bg-gradient-to-br from-blue-50 to-blue-100 dark:from-blue-950 dark:to-blue-900 rounded-lg">
                                    <div className="flex items-center gap-2 mb-2">
                                        <Target className="h-5 w-5 text-blue-600" />
                                        <span className="text-sm font-medium text-muted-foreground">{t('BATTLE_ORBS_PER_DAY')}</span>
                                    </div>
                                    <div className="text-2xl font-bold">{currentQuest?.potentialJewelPerDay || 0}</div>
                                </div>

                                <div className="p-4 bg-gradient-to-br from-green-50 to-green-100 dark:from-green-950 dark:to-green-900 rounded-lg">
                                    <div className="flex items-center gap-2 mb-2">
                                        <TrendingUp className="h-5 w-5 text-green-600" />
                                        <span className="text-sm font-medium text-muted-foreground">{t('BATTLE_EFFICIENCY')}</span>
                                    </div>
                                    <div className="text-2xl font-bold">100%</div>
                                </div>
                            </div>
                        </CardContent>
                    </Card>
                </TabsContent>

                {/* 领民数据 */}
                <TabsContent value="citizens" className="space-y-4">
                    <Card>
                        <CardHeader>
                            <div className="flex items-center justify-between">
                                <div>
                                    <CardTitle>{t('BATTLE_CITIZENS')}</CardTitle>
                                    <CardDescription>
                                        {t('BATTLE_CITIZENS_DESC')}
                                    </CardDescription>
                                </div>
                                <div className="text-right">
                                    <div className="text-3xl font-bold text-primary">
                                        {citizenStats.total.toLocaleString()}
                                    </div>
                                    <div className="text-sm text-muted-foreground">{t('BATTLE_CITIZENS_TOTAL')}</div>
                                </div>
                            </div>
                        </CardHeader>
                        <CardContent>
                            <div className="grid gap-3 md:grid-cols-2 lg:grid-cols-3">
                                {citizenStats.byRegion.map((region, index) => (
                                    <div
                                        key={index}
                                        className="p-4 border rounded-lg hover:shadow-md transition-shadow"
                                    >
                                        <div className="flex items-center justify-between mb-2">
                                            <div className="flex items-center gap-2">
                                                <Users className="h-4 w-4 text-muted-foreground" />
                                                <span className="font-medium">{region.name}</span>
                                            </div>
                                            <Badge variant="outline">{region.count.toLocaleString()}</Badge>
                                        </div>
                                        <div className="text-sm text-green-600 dark:text-green-400 font-medium">
                                            {region.bonus}
                                        </div>
                                    </div>
                                ))}
                            </div>
                        </CardContent>
                    </Card>
                </TabsContent>
            </Tabs>
        </div>
    );
}
