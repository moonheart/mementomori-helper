import { useState, useEffect } from 'react';
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from '@/components/ui/card';
import { Button } from '@/components/ui/button';
import { Badge } from '@/components/ui/badge';
import { Tabs, TabsContent, TabsList, TabsTrigger } from '@/components/ui/tabs';
import { Swords, Loader2, Trophy, Zap, TrendingUp, BookOpen, Crown, Star } from 'lucide-react';
import { ortegaApi } from '@/api/ortega-client';
import { useMasterTable } from '@/hooks/useMasterData';
import { useTranslation } from '@/hooks/useTranslation';
import { toast } from '@/hooks/use-toast';
import { PvpBattleLogs } from '@/components/pvp/PvpBattleLogs';
import { Progress } from '@/components/ui/progress';
import { Alert, AlertDescription } from '@/components/ui/alert';
import { useAccountStore } from '@/store/accountStore';
import { PvpRankingPlayerInfo } from '@/api/generated/pvpRankingPlayerInfo';
import { PvpRankingRewardMB } from '@/api/generated/pvpRankingRewardMB';
import { PvpRankingRewardType } from '@/api/generated/pvpRankingRewardType';
import { BattleFieldCharacterGroupType } from '@/api/generated/battleFieldCharacterGroupType';
import { LegendLeagueRankingPlayerInfo } from '@/api/generated/legendLeagueRankingPlayerInfo';

export function PVPPage() {
    const { t } = useTranslation();
    const [loading, setLoading] = useState(true);
    const [activeMainTab, setActiveMainTab] = useState('ancient');

    // Ancient Arena (Battle League)
    const [pvpInfo, setPvpInfo] = useState<{
        currentRank: number;
        matchingRivalList: PvpRankingPlayerInfo[];
        topRankerList: PvpRankingPlayerInfo[];
    } | null>(null);

    // Apex Arena (Legend League)
    const [legendInfo, setLegendInfo] = useState<{
        currentPoint: number;
        currentRanking: number;
        matchingRivalList: LegendLeagueRankingPlayerInfo[];
        topRankerList: LegendLeagueRankingPlayerInfo[];
    } | null>(null);

    const currentUserSyncData = useAccountStore((state) => state.getCurrentUserSyncData());
    const userPvpDto = currentUserSyncData?.userBattlePvpDtoInfo ?? null;
    const userLegendDto = currentUserSyncData?.userBattleLegendLeagueDtoInfo ?? null;

    const { data: rewardsMaster } = useMasterTable<PvpRankingRewardMB>('PvpRankingRewardTable');

    const fetchPvpData = async (silent = false) => {
        try {
            if (!silent) setLoading(true);
            const [pvpRes, legendRes] = await Promise.all([
                ortegaApi.battle.getPvpInfo({ isRankingTab: false }),
                ortegaApi.battle.getLegendLeagueInfo({ isRankingTab: false })
            ]);

            setPvpInfo({
                currentRank: pvpRes.currentRank,
                matchingRivalList: pvpRes.matchingRivalList || [],
                topRankerList: pvpRes.topRankerList || []
            });


            setLegendInfo({
                currentPoint: legendRes.currentPoint,
                currentRanking: legendRes.currentRanking,
                matchingRivalList: legendRes.matchingRivalList || [],
                topRankerList: legendRes.topRankerList || []
            });
        } catch (error) {
            console.error('Failed to fetch PVP data:', error);
            toast({
                title: t('PVP_FETCH_FAILED'),
                description: t('PVP_CONNECT_FAILED'),
                variant: 'destructive'
            });
        } finally {
            if (!silent) setLoading(false);
        }
    };

    useEffect(() => {
        fetchPvpData();
    }, []);

    const handleChallenge = async (rivalId: number, rivalRank: number) => {
        try {
            const res = await ortegaApi.battle.pvpStart({
                rivalPlayerId: rivalId,
                rivalPlayerRank: rivalRank
            });

            const isWin = res.battleResult?.simulationResult?.battleEndInfo?.winGroupType === BattleFieldCharacterGroupType.Attacker;

            if (isWin) {
                toast({
                    title: t('PVP_WIN'),
                    description: t('PVP_RANK_UP', [res.afterRank]),
                });
            } else {
                toast({
                    title: t('BATTLE_CHALLENGE_LOSS'),
                    description: t('PVP_LOSS_DESC'),
                    variant: 'destructive'
                });
            }
            fetchPvpData(true);
        } catch (error) {
            console.error('PVP challenge failed:', error);
            toast({
                title: t('BATTLE_CHALLENGE_ERROR'),
                description: t('PVP_REQUEST_FAILED'),
                variant: 'destructive'
            });
        }
    };

    const handleLegendChallenge = async (rivalId: number) => {
        try {
            const res = await ortegaApi.battle.legendLeagueStart({ rivalPlayerId: rivalId });

            const isWin = res.battleResult?.simulationResult?.battleEndInfo?.winGroupType === BattleFieldCharacterGroupType.Attacker;

            if (isWin) {
                toast({
                    title: t('PVP_WIN'),
                    description: t('PVP_RANK_UP_POINTS', [res.ranking, res.point]),
                });
            } else {
                toast({
                    title: t('BATTLE_CHALLENGE_LOSS'),
                    description: t('PVP_POINTS_LOST'),
                    variant: 'destructive'
                });
            }
            fetchPvpData(true);
        } catch (error) {
            console.error('Legend PVP challenge failed:', error);
            toast({
                title: t('BATTLE_CHALLENGE_ERROR'),
                description: t('PVP_REQUEST_FAILED'),
                variant: 'destructive'
            });
        }
    };

    const pvpRewards = (rewardsMaster || [])
        .filter(r => r.pvpRankingRewardType === (activeMainTab === 'ancient'
            ? PvpRankingRewardType.BattleLeagueDailyRankingReward
            : PvpRankingRewardType.LegendLeagueDailyRankingReward))
        .sort((a, b) => a.upperLimitRanking - b.upperLimitRanking);

    if (loading) {
        return (
            <div className="flex items-center justify-center min-h-[400px]">
                <Loader2 className="h-8 w-8 animate-spin text-primary" />
                <span className="ml-2">{t('PVP_LOADING')}</span>
            </div>
        );
    }

    const pvpChallengesUsed = userPvpDto?.pvpTodayCount || 0;
    const freeChallengesRemaining = Math.max(0, 5 - pvpChallengesUsed);
    const freeChallengesRemainingPercent = Math.max(0, Math.min(100, (freeChallengesRemaining / 5) * 100));
    const legendChallengesRemaining = Math.max(0, 10 - (userLegendDto?.legendLeagueTodayCount || 0));
    const legendChallengesRemainingPercent = Math.max(0, Math.min(100, (legendChallengesRemaining / 10) * 100));

    return (
        <div className="space-y-6">
            {/* Main Tabs */}
            <Tabs defaultValue="ancient" onValueChange={setActiveMainTab} className="space-y-6">
                <TabsList className="grid w-full grid-cols-2">
                    <TabsTrigger value="ancient">{t('[CommonHeaderLocalPvpLabel]')}</TabsTrigger>
                    <TabsTrigger value="apex">{t('[CommonHeaderGlobalPvpLabel]')}</TabsTrigger>
                </TabsList>

                {/* Ancient Arena */}
                <TabsContent value="ancient" className="space-y-6">
                    <div className="grid gap-6 md:grid-cols-2">
                        <Card className="relative overflow-hidden">
                            <div className="absolute top-0 right-0 opacity-10">
                                <Trophy className="h-32 w-32" />
                            </div>
                            <CardHeader>
                                <CardTitle className="flex items-center gap-2">
                                    <Trophy className="h-6 w-6 text-yellow-500" />
                                    {t('PVP_MY_RANK')}
                                </CardTitle>
                                <CardDescription>{t('PVP_MY_RANK_DESC')}</CardDescription>
                            </CardHeader>
                            <CardContent className="space-y-4">
                                <div className="text-center py-4">
                                    <div className="text-6xl font-bold text-primary mb-2">
                                        #{pvpInfo?.currentRank || '---'}
                                    </div>
                                    <div className="text-sm text-muted-foreground">
                                        {t('PVP_BEST_RANK', [userPvpDto?.maxRanking || '---'])}
                                    </div>
                                </div>
                            </CardContent>
                        </Card>

                        <Card>
                            <CardHeader>
                                <CardTitle className="flex items-center gap-2">
                                    <Swords className="h-6 w-6" />
                                    {t('GUILD_TODAY_CHALLENGES')}
                                </CardTitle>
                                <CardDescription>{t('PVP_RESET_4')}</CardDescription>
                            </CardHeader>
                            <CardContent className="space-y-4">
                                <div className="space-y-2">
                                    <div className="flex items-center justify-between">
                                        <span className="text-sm text-muted-foreground">{t('PVP_REMAINING')}</span>
                                        <Badge variant="secondary" className="text-base">
                                            {freeChallengesRemaining} / 5
                                        </Badge>
                                    </div>
                                    <Progress value={freeChallengesRemainingPercent} className="h-2" />
                                </div>
                                <div className="pt-2">
                                    <Button className="w-full" size="lg" onClick={() => fetchPvpData(true)}>
                                        <Zap className="mr-2 h-5 w-5" />
                                        {t('PVP_REFRESH_RIVALS')}
                                    </Button>
                                </div>
                            </CardContent>
                        </Card>
                    </div>

                    <Tabs defaultValue="opponents">
                        <TabsList className="grid w-full max-w-md grid-cols-3">
                            <TabsTrigger value="opponents">{t('PVP_CHALLENGE')}</TabsTrigger>
                            <TabsTrigger value="history">{t('PVP_HISTORY')}</TabsTrigger>
                            <TabsTrigger value="rewards">{t('[CommonRewardLabel]')}</TabsTrigger>
                        </TabsList>

                        <TabsContent value="opponents" className="space-y-4 mt-6">
                            <Card>
                                <CardHeader>
                                    <CardTitle>{t('PVP_RIVALS')}</CardTitle>
                                </CardHeader>
                                <CardContent>
                                    <div className="space-y-3">
                                        {pvpInfo?.matchingRivalList.map((rival) => (
                                            <div key={rival.playerInfo.playerId} className="flex items-center justify-between p-4 rounded-lg border hover:border-primary transition-all">
                                                <div className="flex items-center gap-4">
                                                    <div className="flex flex-col items-center">
                                                        <Badge variant="outline" className="mb-1">#{rival.currentRank}</Badge>
                                                        {rival.currentRank < (pvpInfo?.currentRank || 0) && <TrendingUp className="h-4 w-4 text-green-500" />}
                                                    </div>
                                                    <div>
                                                        <div className="font-semibold">{rival.playerInfo.playerName}</div>
                                                        <div className="text-sm text-muted-foreground">{t('CAVE_POWER', [rival.battlePower.toLocaleString()])} • Lv.{rival.playerInfo.playerLevel}</div>
                                                    </div>
                                                </div>
                                                <Button onClick={() => handleChallenge(rival.playerInfo.playerId, rival.currentRank)} disabled={freeChallengesRemaining === 0}>{t('PVP_CHALLENGE')}</Button>
                                            </div>
                                        ))}
                                    </div>
                                </CardContent>
                            </Card>
                        </TabsContent>
                        <TabsContent value="history" className="space-y-4 mt-6">
                            <PvpBattleLogs kind="pvp" playerId={currentUserSyncData?.userStatusDtoInfo?.playerId} />
                        </TabsContent>
                        <TabsContent value="rewards" className="mt-6">
                            <Card>
                                <CardHeader><CardTitle>{t('PVP_DAILY_REWARDS')}</CardTitle></CardHeader>
                                <CardContent>
                                    <div className="space-y-3">
                                        {pvpRewards.map((tier, idx) => (
                                            <div key={idx} className="flex items-center justify-between p-3 rounded-lg border">
                                                <span className="font-medium">{t('PVP_RANK', [tier.upperLimitRanking === tier.lowerLimitRanking ? tier.upperLimitRanking : `${tier.upperLimitRanking} - ${tier.lowerLimitRanking}`])}</span>
                                                <div className="flex gap-2">
                                                    {tier.rewardList?.map((item, i) => (
                                                        <span key={i} className="text-sm text-muted-foreground">{t(`[ItemName_${item.itemId}]`)} x{item.itemCount}{i < (tier.rewardList?.length || 0) - 1 ? ' • ' : ''}</span>
                                                    ))}
                                                </div>
                                            </div>
                                        ))}
                                    </div>
                                </CardContent>
                            </Card>
                        </TabsContent>
                    </Tabs>
                </TabsContent>

                {/* Apex Arena (Legend League) */}
                <TabsContent value="apex" className="space-y-6">
                    <Alert>
                        <Star className="h-4 w-4" />
                        <AlertDescription>
                            <strong>{t('PVP_LEGEND_INFO_TITLE')}</strong>
                            {t('PVP_LEGEND_INFO')}
                        </AlertDescription>
                    </Alert>

                    <div className="grid gap-6 md:grid-cols-3">
                        <Card className="border-2 border-primary">
                            <CardHeader>
                                <CardTitle className="flex items-center gap-2">
                                    <Crown className="h-6 w-6 text-yellow-500" />
                                    {t('PVP_MY_TIER')}
                                </CardTitle>
                            </CardHeader>
                            <CardContent className="text-center py-2">
                                <div className="text-4xl font-bold text-primary mb-2">
                                    {legendInfo?.currentPoint || 0} pt
                                </div>
                                <Badge variant="outline" className="text-base">{t('PVP_RANK_HASH', [legendInfo?.currentRanking || '---'])}</Badge>
                            </CardContent>
                        </Card>

                        <Card>
                            <CardHeader>
                                <CardTitle className="flex items-center gap-2">
                                    <TrendingUp className="h-6 w-6 text-green-500" />
                                    {t('PVP_STREAK')}
                                </CardTitle>
                            </CardHeader>
                            <CardContent className="text-center py-2">
                                <div className="text-4xl font-bold text-green-500 mb-2">
                                    {userLegendDto?.legendLeagueConsecutiveVictoryCount || 0}
                                </div>
                                <div className="text-sm text-muted-foreground">{t('PVP_STREAK_ON')}</div>
                            </CardContent>
                        </Card>

                        <Card>
                            <CardHeader>
                                <CardTitle className="flex items-center gap-2">
                                    <Swords className="h-6 w-6" />
                                    {t('PVP_CHALLENGE_COUNT')}
                                </CardTitle>
                            </CardHeader>
                            <CardContent className="space-y-4">
                                <div className="flex items-center justify-between">
                                    <span className="text-sm text-muted-foreground">{t('PVP_REMAINING_CHALLENGES')}</span>
                                    <Badge variant="secondary">{legendChallengesRemaining} / 10</Badge>
                                </div>
                                <Progress value={legendChallengesRemainingPercent} className="h-2" />
                            </CardContent>
                        </Card>
                    </div>

                    <Tabs defaultValue="opponents">
                        <TabsList className="grid w-full max-w-md grid-cols-3">
                            <TabsTrigger value="opponents">{t('PVP_CHALLENGE')}</TabsTrigger>
                            <TabsTrigger value="history">{t('PVP_HISTORY')}</TabsTrigger>
                            <TabsTrigger value="rewards">{t('[CommonRewardLabel]')}</TabsTrigger>
                        </TabsList>

                        <TabsContent value="opponents" className="space-y-4 mt-6">
                            <Card>
                                <CardHeader><CardTitle>{t('PVP_MATCHED_RIVALS')}</CardTitle></CardHeader>
                                <CardContent>
                                    <div className="space-y-3">
                                        {legendInfo?.matchingRivalList.map((rival) => (
                                            <div key={rival.playerInfo.playerId} className="flex items-center justify-between p-4 rounded-lg border hover:border-primary transition-all">
                                                <div className="flex items-center gap-4">
                                                    <div className="flex flex-col items-center">
                                                        <Badge variant="outline" className="mb-1">#{rival.currentRank}</Badge>
                                                    </div>
                                                    <div>
                                                        <div className="font-semibold">{rival.playerInfo.playerName}</div>
                                                        <div className="text-sm text-muted-foreground">{t('PVP_POINTS', [rival.currentPoint])} • {t('CAVE_POWER', [rival.defenseBattlePower.toLocaleString()])}</div>
                                                    </div>
                                                </div>
                                                <Button onClick={() => handleLegendChallenge(rival.playerInfo.playerId)} disabled={legendChallengesRemaining === 0}>{t('PVP_CHALLENGE')}</Button>
                                            </div>
                                        ))}
                                    </div>
                                </CardContent>
                            </Card>
                        </TabsContent>
                        <TabsContent value="history" className="space-y-4 mt-6">
                            <PvpBattleLogs kind="legend" playerId={currentUserSyncData?.userStatusDtoInfo?.playerId} />
                        </TabsContent>
                        <TabsContent value="rewards" className="mt-6">
                            <Card>
                                <CardHeader><CardTitle>{t('PVP_SEASON_REWARDS')}</CardTitle></CardHeader>
                                <CardContent>
                                    <div className="space-y-3">
                                        {pvpRewards.map((tier, idx) => (
                                            <div key={idx} className="flex items-center justify-between p-3 rounded-lg border">
                                                <span className="font-medium">{t('PVP_RANK', [tier.upperLimitRanking === tier.lowerLimitRanking ? tier.upperLimitRanking : `${tier.upperLimitRanking} - ${tier.lowerLimitRanking}`])}</span>
                                                <div className="flex gap-2">
                                                    {tier.rewardList?.map((item, i) => (
                                                        <span key={i} className="text-sm text-muted-foreground">{t(`[ItemName_${item.itemId}]`)} x{item.itemCount}{i < (tier.rewardList?.length || 0) - 1 ? ' • ' : ''}</span>
                                                    ))}
                                                </div>
                                            </div>
                                        ))}
                                    </div>
                                </CardContent>
                            </Card>
                        </TabsContent>
                    </Tabs>
                </TabsContent>
            </Tabs>
        </div>
    );
}
