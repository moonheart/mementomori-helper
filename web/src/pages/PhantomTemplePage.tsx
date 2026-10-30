import { useState } from 'react';
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
import { Badge } from '@/components/ui/badge';
import { Button } from '@/components/ui/button';
import { Progress } from '@/components/ui/progress';
import { Tabs, TabsContent, TabsList, TabsTrigger } from '@/components/ui/tabs';
import { Alert, AlertDescription } from '@/components/ui/alert';
import { ScrollArea } from '@/components/ui/scroll-area';
import {
    Dialog,
    DialogContent,
    DialogDescription,
    DialogHeader,
    DialogTitle,
} from '@/components/ui/dialog';
import {
    Users,
    Trophy,
    Clock,
    Star,
    Crown,
    Zap,
    Loader2,
    Swords,
    RefreshCw,
    History,
    Sword,
    Target,
} from 'lucide-react';
import { useLocalRaidInfo, useLocalRaidBattleLogs } from '@/hooks/useLocalRaidInfo';
import { BattleLogModal } from '@/components/battle-log';
import { BattleSimulationResult } from '@/api/generated/battleSimulationResult';
import { useItemName } from '@/hooks/useItemName';
import { useTranslation } from '@/hooks/useTranslation';
import { LocalRaidQuestInfo } from '@/api/generated/localRaidQuestInfo';
import { LocalRaidEnemyInfo } from '@/api/generated/localRaidEnemyInfo';
import { ElementType } from '@/api/generated/elementType';
import { BattleFieldCharacterGroupType } from '@/api/generated/battleFieldCharacterGroupType';
import { clsx } from 'clsx';
import { LocalRaidLobbyDialog } from '@/components/localRaid';
import { ortegaApi } from '@/api/ortega-client';
import { toast } from '@/hooks/use-toast';

// 获取属性颜色
function getElementColor(elementType: ElementType): string {
    const colors: Record<number, string> = {
        [ElementType.Blue]: 'text-blue-500',
        [ElementType.Red]: 'text-red-500',
        [ElementType.Green]: 'text-green-500',
        [ElementType.Yellow]: 'text-yellow-500',
        [ElementType.Light]: 'text-amber-300',
        [ElementType.Dark]: 'text-purple-500',
    };
    return colors[elementType] || 'text-gray-500';
}

// 获取属性名称
function getElementName(elementType: ElementType): string {
    const names: Record<number, string> = {
        [ElementType.Blue]: '[ElementTypeBlue]',
        [ElementType.Red]: '[ElementTypeRed]',
        [ElementType.Green]: '[ElementTypeGreen]',
        [ElementType.Yellow]: '[ElementTypeYellow]',
        [ElementType.Light]: '[ElementTypeLight]',
        [ElementType.Dark]: '[ElementTypeDark]',
    };
    return names[elementType] || '[CommonNoneLabel]';
}

export function PhantomTemplePage() {
    const { t } = useTranslation();
    const { getItemName } = useItemName();
    const {
        loading,
        questInfos,
        remainingCount,
        bonusTimeInfo,
        bonusScheduleTimes,
        getTempleName,
        getQuestEnemies,
        isQuestCleared,
        localRaidLevel,
        playerId,
        refresh
    } = useLocalRaidInfo();

    const { logs: battleLogs, loading: logsLoading, refresh: refreshLogs } = useLocalRaidBattleLogs();
    const bonusExtraRate = Math.max(bonusTimeInfo.bonusRate - 100, 0);
    const eventTimeLabel = bonusScheduleTimes.length > 0
        ? bonusScheduleTimes
            .map((time) => {
                const startHours = Math.floor(time.startTime / 10000);
                const startMinutes = Math.floor((time.startTime % 10000) / 100);
                const endHours = Math.floor(time.endTime / 10000);
                const endMinutes = Math.floor((time.endTime % 10000) / 100);
                return t('[LocalRaidEventTimeFormat]', [startHours, startMinutes, endHours, endMinutes]);
            })
            .join(' / ')
        : '--:--';
    const localRaidLevelKey = '[LocalRaidTrainingLevelFormat]';
    const localRaidLevelText = t(localRaidLevelKey, [localRaidLevel ?? '--']);
    const hasLocalRaidLevelText = localRaidLevelText !== localRaidLevelKey;
    const bonusMultiplier = bonusTimeInfo.bonusRate / 100;
    const bonusMultiplierText = Number.isInteger(bonusMultiplier)
        ? bonusMultiplier.toString()
        : bonusMultiplier.toFixed(1);
    const nextEventTimeText = bonusTimeInfo.nextBonusTime !== null
        ? t('[LocalRaidEventTimeFormat]', [
            Math.floor(bonusTimeInfo.nextBonusTime / 100),
            bonusTimeInfo.nextBonusTime % 100,
            0,
            0,
        ]).split('~')[0]
        : '--:--';
    const formatRichText = (text: string) => text
        .replace(/<br>/g, '<br/>')
        .replace(/<color=([^>]+)>/g, '<span style="color:$1">')
        .replace(/<\/color>/g, '</span>');
    const inBonusMessage = formatRichText(
        t('[LocalRaidCurrentEventTimeMessageFormat]', [bonusMultiplierText, eventTimeLabel])
    );
    const nextBonusMessage = formatRichText(
        t('[LocalRaidNextEventTimeMessageFormat]', [nextEventTimeText])
    );

    // 战斗记录详情弹窗
    const [selectedLog, setSelectedLog] = useState<BattleSimulationResult | null>(null);
    const [isModalOpen, setIsModalOpen] = useState(false);

    const handleViewLog = async (battleToken: string, leaderPlayerId: number) => {
        try {
            const result = await ortegaApi.localRaid.getLocalRaidBattleResultOld({
                battleToken,
                leaderPlayerId,
            });
            if (result?.battleResult?.simulationResult) {
                setSelectedLog(result.battleResult.simulationResult);
                setIsModalOpen(true);
            } else {
                toast({ title: t('TEMPLE_NO_BATTLE_DETAIL'), variant: 'destructive' });
            }
        } catch (e) {
            console.error('Failed to fetch battle result old:', e);
            toast({ title: t('TEMPLE_BATTLE_DETAIL_FAILED'), variant: 'destructive' });
        } finally {
            // no-op
        }
    };

    const formatTime = (battleTime: number) => {
        const date = new Date(battleTime * 1000);
        return date.toLocaleString('zh-CN', {
            month: '2-digit',
            day: '2-digit',
            hour: '2-digit',
            minute: '2-digit',
        });
    };

    const [selectedQuest, setSelectedQuest] = useState<LocalRaidQuestInfo | null>(null);
    const [detailOpen, setDetailOpen] = useState(false);
    const [lobbyOpen, setLobbyOpen] = useState(false);
    const [lobbyQuest, setLobbyQuest] = useState<LocalRaidQuestInfo | null>(null);

    // 打开组队大厅
    const openLobby = (quest: LocalRaidQuestInfo) => {
        setLobbyQuest(quest);
        setLobbyOpen(true);
    };

    // 打开任务详情
    const openDetail = (quest: LocalRaidQuestInfo) => {
        setSelectedQuest(quest);
        setDetailOpen(true);
    };

    // 渲染奖励
    const renderRewards = (rewards: { itemType: number; itemId: number; itemCount: number }[]) => {
        if (!rewards || rewards.length === 0) return null;
        return rewards.map((item, idx) => (
            <Badge key={idx} variant="secondary" className="text-xs">
                {getItemName(item.itemType, item.itemId)} x{item.itemCount.toLocaleString()}
            </Badge>
        ));
    };

    // 渲染敌人列表
    const renderEnemies = (enemies: LocalRaidEnemyInfo[]) => {
        return enemies.map((enemy) => (
            <div
                key={enemy.id}
                className="flex items-center gap-3 p-3 border rounded-lg bg-muted/30"
            >
                <div className="flex items-center justify-center w-12 h-12 rounded-full bg-primary/10 text-lg font-bold">
                    {enemy.enemyRank}
                </div>
                <div className="flex-1 min-w-0">
                    <div className="font-medium truncate">{t(enemy.nameKey)}</div>
                    <div className="text-sm text-muted-foreground">
                        Lv.{enemy.enemyRank} • {t('CAVE_POWER', [enemy.battlePower.toLocaleString()])}
                    </div>
                </div>
                <Badge variant="outline" className={getElementColor(enemy.elementType)}>
                    {t(getElementName(enemy.elementType))}
                </Badge>
            </div>
        ));
    };

    // 渲染星级
    const renderStars = (level: number) => {
        return Array.from({ length: Math.min(level, 9) }).map((_, i) => (
            <Star key={i} className="h-4 w-4 text-red-500 fill-red-500" />
        ));
    };

    if (loading) {
        return (
            <div className="flex items-center justify-center min-h-[400px]">
                <Loader2 className="h-8 w-8 animate-spin text-primary" />
                <span className="ml-2">{t('TEMPLE_LOADING')}</span>
            </div>
        );
    }

    return (
        <div className="space-y-6">
            <div className="flex justify-end">
                <Button variant="outline" size="sm" onClick={() => { refresh(); refreshLogs(); }}>
                    <RefreshCw className="h-4 w-4 mr-2" />
                    {t('TEMPLE_REFRESH')}
                </Button>
            </div>

            {/* 加成时段提示 */}
            {bonusTimeInfo.inBonus && (
                <Alert className="bg-gradient-to-r from-yellow-50 to-orange-50 dark:from-yellow-950 dark:to-orange-950 border-yellow-500">
                    <Zap className="h-4 w-4 text-yellow-600" />
                    <AlertDescription className="text-yellow-800 dark:text-yellow-200">
                        <strong>{t('TEMPLE_BONUS_TITLE')}</strong>
                        {t('TEMPLE_BONUS_DESC', [bonusTimeInfo.bonusRate, bonusExtraRate])}
                    </AlertDescription>
                </Alert>
            )}

            {/* 状态卡片 */}
            <div className="grid gap-6 md:grid-cols-3">
                <Card>
                    <CardHeader className="pb-2">
                        <CardTitle className="flex items-center gap-2 text-base">
                            <Swords className="h-5 w-5" />
                            {t('GUILD_TODAY_CHALLENGES')}
                        </CardTitle>
                    </CardHeader>
                    <CardContent>
                        <div className="space-y-2">
                            <div className="flex items-center justify-between">
                                <span className="text-sm text-muted-foreground">{t('PVP_REMAINING')}</span>
                                <Badge variant="secondary" className="text-base">
                                    {remainingCount} / 6
                                </Badge>
                            </div>
                            <Progress
                                value={(remainingCount / 6) * 100}
                                className="h-2"
                            />
                        </div>
                    </CardContent>
                </Card>

                <Card>
                    <CardHeader className="pb-2">
                        <CardTitle className="flex items-center gap-2 text-base">
                            <Crown className="h-5 w-5 text-purple-500" />
                            {t('TEMPLE_LEVEL')}
                        </CardTitle>
                    </CardHeader>
                    <CardContent>
                        <div className="text-center py-2">
                            <div className="text-2xl font-bold text-purple-600 dark:text-purple-400">
                                {localRaidLevelText}
                            </div>
                            <div className="text-xs text-muted-foreground mt-1">
                                {hasLocalRaidLevelText ? null : t('TEMPLE_LEVEL_HINT')}
                            </div>
                        </div>
                    </CardContent>
                </Card>

                <Card>
                    <CardHeader className="pb-2">
                        <CardTitle className="flex items-center gap-2 text-base">
                            <Clock className="h-5 w-5 text-orange-500" />
                            {t('[LocalRaidQuestEventRewardLabel]')}
                        </CardTitle>
                    </CardHeader>
                    <CardContent>
                        <div className="text-center py-2">
                            {bonusTimeInfo.inBonus ? (
                                <div
                                    className="text-sm font-semibold text-green-600"
                                    dangerouslySetInnerHTML={{
                                        __html: inBonusMessage,
                                    }}
                                />
                            ) : (
                                <>
                                    <div
                                        className="text-sm font-semibold"
                                        dangerouslySetInnerHTML={{
                                            __html: nextBonusMessage,
                                        }}
                                    />
                                </>
                            )}
                            <div className="text-xs text-muted-foreground mt-1">
                                {eventTimeLabel}
                            </div>
                            <div className="text-xs text-muted-foreground mt-1">
                                {t('[LocalRaidQuestEventRewardLabel]')}: x{bonusMultiplierText}（+{bonusExtraRate}%）
                            </div>
                        </div>
                    </CardContent>
                </Card>
            </div>

            <Tabs defaultValue="quests" className="space-y-6">
                <TabsList className="grid w-full grid-cols-2">
                    <TabsTrigger value="quests">{t('TEMPLE_TAB_QUESTS')}</TabsTrigger>
                    <TabsTrigger value="logs">{t('[BattleReportTitle]')}</TabsTrigger>
                </TabsList>

                {/* 探索任务 */}
                <TabsContent value="quests" className="space-y-4">
                    {questInfos.length === 0 ? (
                        <Card>
                            <CardContent className="py-8 text-center text-muted-foreground">
                                {t('TEMPLE_NO_QUESTS')}
                            </CardContent>
                        </Card>
                    ) : (
                        <div className="grid gap-4 md:grid-cols-2">
                            {[...questInfos].sort((a, b) => b.level - a.level).map((quest) => {
                                const templeName = t(getTempleName(quest.localRaidBannerId));
                                const isCleared = isQuestCleared(quest.id);
                                const enemies = getQuestEnemies(quest);

                                return (
                                    <Card
                                        key={quest.id}
                                        className={clsx(
                                            "hover:shadow-lg transition-all",
                                            isCleared && "border-green-200 dark:border-green-800"
                                        )}
                                    >
                                        <CardHeader>
                                            <div className="flex items-center justify-between">
                                                <div className="flex items-center gap-3">
                                                    <div className="p-2 rounded-lg bg-gradient-to-br from-purple-500 to-pink-500">
                                                        <Swords className="h-6 w-6 text-white" />
                                                    </div>
                                                    <div>
                                                        <div className="flex items-center gap-2 mb-1">
                                                            <CardTitle className="text-lg">{templeName}</CardTitle>
                                                            {!isCleared && (
                                                                <Badge className="bg-red-500 text-xs">{t('[CommonFirstRewardLabel]')}</Badge>
                                                            )}
                                                            {isCleared && (
                                                                <Badge variant="outline" className="border-green-500 text-green-500 text-xs">
                                                                    {t('BATTLE_CLEARED')}
                                                                </Badge>
                                                            )}
                                                        </div>
                                                    </div>
                                                </div>
                                                <div className="flex items-center gap-1">
                                                    {renderStars(quest.level)}
                                                </div>
                                            </div>
                                        </CardHeader>
                                        <CardContent className="space-y-4">
                                            {/* 敌人预览 */}
                                            <div className="flex items-center gap-2 text-sm text-muted-foreground">
                                                <Users className="h-4 w-4" />
                                                <span>{t('TEMPLE_ENEMY_COUNT', [enemies.length])}</span>
                                                <span>•</span>
                                                <span>
                                                    {t('TEMPLE_TOTAL_POWER', [enemies.reduce((sum, e) => sum + e.battlePower, 0).toLocaleString()])}
                                                </span>
                                            </div>

                                            {/* 奖励预览 */}
                                            <div>
                                                <div className="text-sm font-medium mb-2">{t('TEMPLE_FIXED_REWARDS')}</div>
                                                <div className="flex flex-wrap gap-2">
                                                    {renderRewards(quest.fixedBattleRewards)}
                                                </div>
                                            </div>

                                            {/* 首次通关奖励 */}
                                            {!isCleared && quest.firstBattleRewards.length > 0 && (
                                                <div className="p-3 bg-gradient-to-r from-yellow-50 to-orange-50 dark:from-yellow-950 dark:to-orange-950 rounded-lg border border-yellow-200">
                                                    <div className="text-sm font-medium mb-2 text-yellow-800 dark:text-yellow-200">
                                                        {t('TEMPLE_FIRST_CLEAR')}
                                                    </div>
                                                    <div className="flex flex-wrap gap-2">
                                                        {renderRewards(quest.firstBattleRewards)}
                                                    </div>
                                                </div>
                                            )}

                                            {/* 操作按钮 */}
                                            <div className="flex gap-2 pt-2">
                                            <Button
                                                variant="outline"
                                                className="flex-1"
                                                onClick={() => openDetail(quest)}
                                            >
                                                <Trophy className="mr-2 h-4 w-4" />
                                                {t('[CommonDetailLabel]')}
                                            </Button>
                                                <Button
                                                    className="flex-1"
                                                    onClick={() => openLobby(quest)}
                                                    disabled={remainingCount <= 0}
                                                >
                                                    <Users className="mr-2 h-4 w-4" />
                                                    {remainingCount <= 0 ? t('TEMPLE_NO_TRIES') : t('[CommonChallengeLabel]')}
                                                </Button>
                                            </div>
                                        </CardContent>
                                    </Card>
                                );
                            })}
                        </div>
                    )}
                </TabsContent>

                {/* 战斗记录 */}
                <TabsContent value="logs" className="space-y-4">
                    {logsLoading ? (
                        <div className="flex items-center justify-center py-8">
                            <Loader2 className="h-6 w-6 animate-spin text-primary" />
                            <span className="ml-2">{t('TEMPLE_LOADING_LOGS')}</span>
                        </div>
                    ) : battleLogs.length === 0 ? (
                        <Card>
                            <CardContent className="py-8 text-center text-muted-foreground">
                                <History className="h-12 w-12 mx-auto mb-3 opacity-50" />
                                <div>{t('BATTLE_LOG_EMPTY')}</div>
                            </CardContent>
                        </Card>
                    ) : (
                        <Card>
                            <CardContent className="p-0">
                                <ScrollArea className="h-[500px]">
                                    <div className="space-y-0 divide-y">
                                        {battleLogs.map((log, index) => {
                                            const isWin = log.battleEndInfo?.winGroupType === BattleFieldCharacterGroupType.Attacker;
                                            return (
                                                <div
                                                    key={log.battleToken || index}
                                                    className={clsx(
                                                        "flex items-center gap-4 p-3 cursor-pointer transition-colors hover:bg-muted/50",
                                                        isWin
                                                            ? "bg-green-50/30 dark:bg-green-900/10"
                                                            : "bg-red-50/30 dark:bg-red-900/10"
                                                    )}
                                                    onClick={() => {
                                                        if (log.battleToken && log.localRaidPartyInfo?.leaderPlayerId) {
                                                            handleViewLog(log.battleToken, log.localRaidPartyInfo.leaderPlayerId);
                                                        }
                                                    }}
                                                >
                                                    {/* 胜负标识 */}
                                                    <div className={clsx(
                                                        "w-10 h-10 rounded-lg flex items-center justify-center shrink-0",
                                                        isWin
                                                            ? "bg-green-100 text-green-700 dark:bg-green-900 dark:text-green-300"
                                                            : "bg-red-100 text-red-700 dark:bg-red-900 dark:text-red-300"
                                                    )}>
                                                        {isWin ? (
                                                            <Trophy className="h-5 w-5" />
                                                        ) : (
                                                            <Sword className="h-5 w-5" />
                                                        )}
                                                    </div>

                                                    {/* 信息 */}
                                                    <div className="flex-1 min-w-0">
                                                        <div className="flex items-center gap-2">
                                                            <Badge
                                                                variant={isWin ? 'default' : 'destructive'}
                                                                className="text-xs"
                                                            >
                                                                {isWin ? t('BATTLE_LOG_WIN') : t('BATTLE_LOG_LOSS')}
                                                            </Badge>
                                                            <span className="text-sm font-medium">
                                                                {t('TEMPLE_QUEST', [log.questId])}
                                                            </span>
                                                        </div>
                                                        <div className="flex items-center gap-3 mt-1 text-xs text-muted-foreground">
                                                            <span className="flex items-center gap-1">
                                                                <Target className="w-3 h-3" />
                                                                {t('BATTLE_LOG_TURNS', [log.battleEndInfo?.endTurn || 0])}
                                                            </span>
                                                            {log.battleTime > 0 && (
                                                                <span className="flex items-center gap-1">
                                                                    <Clock className="w-3 h-3" />
                                                                    {formatTime(log.battleTime)}
                                                                </span>
                                                            )}
                                                            {log.clearLevel > 0 && (
                                                                <span>{t('TEMPLE_CLEAR_LEVEL', [log.clearLevel])}</span>
                                                            )}
                                                        </div>
                                                    </div>

                                                    {/* 层级 */}
                                                    <div className="text-xs text-muted-foreground shrink-0">
                                                        {log.isAutoStart && (
                                                            <Badge variant="outline" className="text-xs">{t('TEMPLE_AUTO')}</Badge>
                                                        )}
                                                    </div>
                                                </div>
                                            );
                                        })}
                                    </div>
                                </ScrollArea>
                            </CardContent>
                        </Card>
                    )}
                </TabsContent>
            </Tabs>

            {/* 任务详情弹窗 */}
            <Dialog open={detailOpen} onOpenChange={setDetailOpen}>
                <DialogContent className="max-w-2xl max-h-[80vh] overflow-y-auto">
                    <DialogHeader>
                        <DialogTitle>
                            {selectedQuest && t(getTempleName(selectedQuest.localRaidBannerId))}
                        </DialogTitle>
                        <DialogDescription />
                    </DialogHeader>

                    {selectedQuest && (
                        <div className="space-y-6">
                            {/* 敌方阵容 */}
                            <div>
                                <h3 className="font-semibold mb-3 flex items-center gap-2">
                                    <Swords className="h-5 w-5" />
                                    {t('[TowerBattleEnemyPartyLabel]')}
                                </h3>
                                <div className="space-y-2">
                                    {renderEnemies(getQuestEnemies(selectedQuest))}
                                </div>
                            </div>

                            {/* 战利品 */}
                            <div>
                                <h3 className="font-semibold mb-3 flex items-center gap-2">
                                    <Trophy className="h-5 w-5" />
                                    {t('[BattleResultRewardTitle]')}
                                </h3>
                                <div className="space-y-4">
                                    {/* 首次奖励 */}
                                    <div className="p-4 bg-gradient-to-r from-yellow-50 to-orange-50 dark:from-yellow-950 dark:to-orange-950 rounded-lg border border-yellow-200">
                                        <div className="text-sm font-medium mb-2 text-yellow-800 dark:text-yellow-200">
                                            {t('TEMPLE_FIRST_CLEAR')}
                                        </div>
                                        <div className="flex flex-wrap gap-2">
                                            {renderRewards(selectedQuest.firstBattleRewards)}
                                        </div>
                                    </div>

                                    {/* 确定奖励 */}
                                    <div className="p-4 bg-muted rounded-lg">
                                        <div className="text-sm font-medium mb-2">{t('TEMPLE_FIXED_REWARDS')}</div>
                                        <div className="flex flex-wrap gap-2">
                                            {renderRewards(selectedQuest.fixedBattleRewards)}
                                        </div>
                                    </div>
                                </div>
                            </div>

                            {/* 组队大厅入口 */}
                            <div className="flex justify-center">
                                <Button
                                    size="lg"
                                    onClick={() => {
                                        setDetailOpen(false);
                                        openLobby(selectedQuest);
                                    }}
                                    disabled={remainingCount <= 0}
                                >
                                    <Users className="mr-2 h-5 w-5" />
                                    {remainingCount <= 0 ? t('TEMPLE_NO_TRIES') : t('TEMPLE_GO_PARTY')}
                                </Button>
                            </div>
                        </div>
                    )}
                </DialogContent>
            </Dialog>

            {/* 组队大厅弹窗 */}
            <LocalRaidLobbyDialog
                open={lobbyOpen}
                onOpenChange={setLobbyOpen}
                quest={lobbyQuest}
                myPlayerId={playerId}
            />

            {/* 战斗记录详情弹窗 */}
            <BattleLogModal
                isOpen={isModalOpen}
                onClose={() => {
                    setIsModalOpen(false);
                    setSelectedLog(null);
                }}
                battleData={selectedLog}
            />
        </div>
    );
}
