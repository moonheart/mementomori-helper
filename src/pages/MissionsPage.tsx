import { useEffect, useState, useMemo } from 'react';
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from '@/components/ui/card';
import { Tabs, TabsContent, TabsList, TabsTrigger } from '@/components/ui/tabs';
import { Button } from '@/components/ui/button';
import { Badge } from '@/components/ui/badge';
import { Progress } from '@/components/ui/progress';
import { Alert, AlertDescription } from '@/components/ui/alert';
import {
    Trophy,
    Gift,
    CheckCircle2,
    BookOpen,
    Target,
    Loader2
} from 'lucide-react';
import { useAccountStore } from '@/store/accountStore';
import { ortegaApi } from '@/api/ortega-client';
import { MissionGroupType, MissionStatusType, MissionGetMissionInfoResponse, MissionActivityRewardStatusType, MissionInfo, TotalActivityMedalRewardMB, MissionMB } from '@/api/generated';
import { useMasterData, useMasterTable } from '@/hooks/useMasterData';
import { useLocalizationStore } from '@/store/localization-store';

import { MissionRow, UIStoreMission } from '@/components/mission/MissionRow';
import { mapMissionsFromGroupInfo } from '@/components/mission/mission-utils';

function MeritRewardCard({
    title = 'MISSIONS_MERIT_REWARDS',
    totalMerit,
    meritBoxes,
    groupType,
    onClaim,
}: {
    title?: string;
    totalMerit: number;
    meritBoxes: { id: number; required: number; status: number; claimed: boolean }[];
    groupType: MissionGroupType;
    onClaim: (groupType: MissionGroupType, required: number) => void;
}) {
    const t = useLocalizationStore(state => state.t);
    if (meritBoxes.length === 0) return null;

    return (
        <Card className="bg-gradient-to-r from-orange-50 to-yellow-50 dark:from-orange-950 dark:to-yellow-950 mb-6">
            <CardHeader className="pb-2">
                <CardTitle className="flex items-center gap-2 text-lg">
                    <Trophy className="h-5 w-5 text-orange-500" />
                    {t(title)}
                </CardTitle>
            </CardHeader>
            <CardContent className="space-y-4">
                <div className="flex items-center justify-between">
                    <span className="text-sm text-muted-foreground">{t('MISSIONS_CURRENT_MERIT')} <span className="font-bold text-orange-600">{totalMerit}</span></span>
                </div>

                <div className="grid gap-2 grid-cols-5">
                    {meritBoxes.map((box) => {
                        const canOpen = box.status === MissionActivityRewardStatusType.NotReceived;
                        return (
                            <div
                                key={box.id}
                                className={`p-2 rounded-md text-center transition-all border ${box.claimed
                                    ? 'bg-muted opacity-50'
                                    : canOpen
                                        ? 'bg-yellow-100 border-yellow-400 text-yellow-800 cursor-pointer hover:scale-105'
                                        : 'bg-muted border-transparent'
                                    }`}
                                onClick={() => {
                                    if (canOpen) {
                                        onClaim(groupType, box.required);
                                    }
                                }}
                            >
                                <div className="text-xl mb-1">
                                    {box.claimed ? '✅' : canOpen ? '🎁' : '🔒'}
                                </div>
                                <div className="text-[10px] font-semibold">{box.required}</div>
                            </div>
                        );
                    })}
                </div>
            </CardContent>
        </Card>
    );
}

export function MissionsPage() {
    const t = useLocalizationStore(state => state.t);
    const { currentAccountId } = useAccountStore();
    const [loading, setLoading] = useState(false);
    const [missionData, setMissionData] = useState<MissionGetMissionInfoResponse | null>(null);
    const [claiming, setClaiming] = useState<Set<number>>(new Set());
    const [activeTab, setActiveTab] = useState('daily');
    const { data: totalActivityMedalRewardTable } = useMasterTable<TotalActivityMedalRewardMB>('TotalActivityMedalRewardTable');

    // 获取任务信息
    const fetchMissionInfo = async () => {
        if (!currentAccountId) return;

        setLoading(true);
        try {
            const response = await ortegaApi.mission.getMissionInfo({
                targetMissionGroupList: [
                    MissionGroupType.Daily,
                    MissionGroupType.Weekly,
                    MissionGroupType.Main
                ]
            });
            setMissionData(response);
        } catch (error) {
            console.error('Failed to fetch mission info:', error);
        } finally {
            setLoading(false);
        }
    };

    // 领取任务奖励
    const handleClaimMission = async (missionId: number) => {
        setClaiming(prev => new Set(prev).add(missionId));
        try {
            await ortegaApi.mission.rewardMission({
                targetMissionIdList: [missionId]
            });
            await fetchMissionInfo();
        } catch (error) {
            console.error('Failed to claim mission:', error);
        } finally {
            setClaiming(prev => {
                const newSet = new Set(prev);
                newSet.delete(missionId);
                return newSet;
            });
        }
    };

    // 领取功勋奖励
    const handleClaimActivityReward = async (missionGroupType: MissionGroupType, requiredCount: number) => {
        try {
            await ortegaApi.mission.rewardMissionActivity({
                missionGroupType,
                requiredCount
            });
        } catch (error) {
            console.error('Failed to claim activity reward:', error);
        }
    };

    // 一键领取所有可领取的奖励
    const handleClaimAll = async () => {
        if (!missionData || loading) return;

        setLoading(true);
        try {
            // 1. 领取普通任务奖励
            const allClaimableMissions = [...dailyMissions, ...weeklyMissions, ...mainMissions]
                .filter(m => m.status === MissionStatusType.NotReceived)
                .map(m => m.id);

            if (allClaimableMissions.length > 0) {
                await ortegaApi.mission.rewardMission({
                    targetMissionIdList: allClaimableMissions
                });
            }

            // 2. 领取每天功勋奖励
            const dailyGroupInfo = getGroupInfo(MissionGroupType.Daily);
            if (dailyGroupInfo?.userMissionActivityDtoInfo?.rewardStatusDict) {
                const dailyActivityInfo = dailyGroupInfo.userMissionActivityDtoInfo;
                const dailyBoxes = Object.entries(dailyActivityInfo.rewardStatusDict)
                    .map(([rewardIdStr, status]) => {
                        const rewardId = Number(rewardIdStr);
                        const rewardMb = Object.values(totalActivityMedalRewardTable || {}).find(mb => mb.id === rewardId);
                        return {
                            required: rewardMb?.requiredActivityMedalCount ?? 0,
                            status: Number(status)
                        };
                    })
                    .filter(box => box.status === MissionActivityRewardStatusType.NotReceived);

                for (const box of dailyBoxes) {
                    await handleClaimActivityReward(MissionGroupType.Daily, box.required);
                }
            }

            // 3. 尝试领取周常功勋
            const weeklyGroupInfo = getGroupInfo(MissionGroupType.Weekly);
            if (weeklyGroupInfo?.userMissionActivityDtoInfo?.rewardStatusDict) {
                const weeklyActivityInfo = weeklyGroupInfo.userMissionActivityDtoInfo;
                const weeklyBoxes = Object.entries(weeklyActivityInfo.rewardStatusDict)
                    .map(([rewardIdStr, status]) => {
                        const rewardId = Number(rewardIdStr);
                        const rewardMb = Object.values(totalActivityMedalRewardTable || {}).find(mb => mb.id === rewardId);
                        return {
                            required: rewardMb?.requiredActivityMedalCount ?? 0,
                            status: Number(status)
                        };
                    })
                    .filter(box => box.status === MissionActivityRewardStatusType.NotReceived);

                for (const box of weeklyBoxes) {
                    await handleClaimActivityReward(MissionGroupType.Weekly, box.required);
                }
            }

            // 刷新数据
            await fetchMissionInfo();
        } catch (error) {
            console.error('Failed to claim all rewards:', error);
        } finally {
            setLoading(false);
        }
    };

    // 页面加载时获取任务信息
    useEffect(() => {
        if (currentAccountId) {
            fetchMissionInfo();
        }
    }, [currentAccountId]);

    // 辅助函数：安全地从字典获取任务组信息 (兼容数字和字符串 Key)
    const getGroupInfo = (groupType: MissionGroupType): MissionInfo | undefined => {
        if (!missionData?.missionInfoDict) return undefined;
        const dict = missionData.missionInfoDict as Record<string | number, MissionInfo>;
        return dict[groupType] || dict[MissionGroupType[groupType]];
    };

    /**
     * 参考 Blazor 的逻辑解析任务状态（委托给共享工具函数）
     */
    const mapMissions = (groupType: MissionGroupType): UIStoreMission[] =>
        mapMissionsFromGroupInfo(getGroupInfo(groupType), true);

    const dailyMissions = useMemo(() => mapMissions(MissionGroupType.Daily), [missionData]);
    const weeklyMissions = useMemo(() => mapMissions(MissionGroupType.Weekly), [missionData]);
    const mainMissions = useMemo(() => mapMissions(MissionGroupType.Main), [missionData]);

    const getActivityInfo = (groupType: MissionGroupType) => getGroupInfo(groupType)?.userMissionActivityDtoInfo;

    const getMeritBoxes = (activityInfo?: MissionInfo['userMissionActivityDtoInfo']) => {
        if (!activityInfo?.rewardStatusDict || !totalActivityMedalRewardTable) return [];

        const tableArray = Object.values(totalActivityMedalRewardTable);

        return Object.entries(activityInfo.rewardStatusDict)
            .map(([rewardIdStr, status]) => {
                const rewardId = Number(rewardIdStr);
                const rewardMb = tableArray.find(mb => mb.id === rewardId);
                const required = rewardMb?.requiredActivityMedalCount ?? 0;

                return {
                    id: rewardId,
                    required: required,
                    status: Number(status),
                    claimed: Number(status) === MissionActivityRewardStatusType.Received
                };
            })
            .sort((a, b) => a.required - b.required);
    };

    const dailyActivityInfo = useMemo(() => getActivityInfo(MissionGroupType.Daily), [missionData]);
    const dailyMeritBoxes = useMemo(() => getMeritBoxes(dailyActivityInfo), [dailyActivityInfo, totalActivityMedalRewardTable]);
    const dailyTotalMerit = dailyActivityInfo?.progressCount || 0;

    const weeklyActivityInfo = useMemo(() => getActivityInfo(MissionGroupType.Weekly), [missionData]);
    const weeklyMeritBoxes = useMemo(() => getMeritBoxes(weeklyActivityInfo), [weeklyActivityInfo, totalActivityMedalRewardTable]);
    const weeklyTotalMerit = weeklyActivityInfo?.progressCount || 0;

    const onClaimMerit = (groupType: MissionGroupType, required: number) => {
        handleClaimActivityReward(groupType, required).then(() => fetchMissionInfo());
    };

    if (!currentAccountId) {
        return (
            <div className="flex items-center justify-center h-[60vh]">
                <Card className="w-full max-w-md">
                    <CardHeader>
                        <CardTitle>{t('MISSIONS_NOT_LOGGED_IN')}</CardTitle>
                        <CardDescription>{t('MISSIONS_LOGIN_HINT')}</CardDescription>
                    </CardHeader>
                </Card>
            </div>
        );
    }

    if (loading && !missionData) {
        return (
            <div className="flex items-center justify-center h-[60vh]">
                <div className="text-center">
                    <Loader2 className="h-8 w-8 animate-spin mx-auto mb-4" />
                    <p className="text-muted-foreground">{t('MISSIONS_LOADING')}</p>
                </div>
            </div>
        );
    }

    return (
        <div className="space-y-6">
            <div className="flex justify-end gap-2">
                <Button onClick={handleClaimAll} variant="secondary" disabled={loading}>
                    {loading && <Loader2 className="mr-2 h-4 w-4 animate-spin" />}
                    {t('MISSIONS_CLAIM_ALL')}
                </Button>
                <Button onClick={fetchMissionInfo} disabled={loading} variant="outline">
                    {loading ? <Loader2 className="mr-2 h-4 w-4 animate-spin" /> : null}
                    {t('TEMPLE_REFRESH')}
                </Button>
            </div>

            <Tabs value={activeTab} onValueChange={setActiveTab}>
                <TabsList className="grid w-full max-w-md grid-cols-3">
                    <TabsTrigger value="daily">{t('MISSIONS_DAILY')}</TabsTrigger>
                    <TabsTrigger value="weekly">{t('MISSIONS_WEEKLY')}</TabsTrigger>
                    <TabsTrigger value="main">{t('MISSIONS_MAIN')}</TabsTrigger>
                </TabsList>

                <TabsContent value="daily" className="mt-6">
                    <MeritRewardCard
                        title='MISSIONS_DAILY_MERIT'
                        totalMerit={dailyTotalMerit}
                        meritBoxes={dailyMeritBoxes}
                        groupType={MissionGroupType.Daily}
                        onClaim={onClaimMerit}
                    />
                    <div className="grid gap-4 grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 2xl:grid-cols-5">
                        {dailyMissions.length === 0 ? (
                            <div className="col-span-full text-center py-8 text-muted-foreground">{t('MISSIONS_NO_DAILY')}</div>
                        ) : (
                            dailyMissions.map(m => (
                                <MissionRow
                                    key={m.id}
                                    mission={m}
                                    onClaim={() => handleClaimMission(m.id)}
                                    isClaiming={claiming.has(m.id)}
                                />
                            ))
                        )}
                    </div>
                </TabsContent>

                <TabsContent value="weekly" className="mt-6">
                    <MeritRewardCard
                        title='MISSIONS_WEEKLY_MERIT'
                        totalMerit={weeklyTotalMerit}
                        meritBoxes={weeklyMeritBoxes}
                        groupType={MissionGroupType.Weekly}
                        onClaim={onClaimMerit}
                    />
                    <div className="grid gap-4 grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 2xl:grid-cols-5">
                        {weeklyMissions.length === 0 ? (
                            <div className="col-span-full text-center py-8 text-muted-foreground">{t('MISSIONS_NO_WEEKLY')}</div>
                        ) : (
                            weeklyMissions.map(m => (
                                <MissionRow
                                    key={m.id}
                                    mission={m}
                                    onClaim={() => handleClaimMission(m.id)}
                                    isClaiming={claiming.has(m.id)}
                                />
                            ))
                        )}
                    </div>
                </TabsContent>

                <TabsContent value="main" className="grid gap-4 mt-6 grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 2xl:grid-cols-5">
                    {mainMissions.length === 0 ? (
                        <div className="col-span-full text-center py-8 text-muted-foreground">{t('MISSIONS_NO_MAIN')}</div>
                    ) : (
                        mainMissions.map(m => (
                            <MissionRow
                                key={m.id}
                                mission={m}
                                onClaim={() => handleClaimMission(m.id)}
                                isClaiming={claiming.has(m.id)}
                            />
                        ))
                    )}
                </TabsContent>
            </Tabs>
        </div>
    );
}
