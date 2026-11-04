import { useState, useEffect, useCallback, useMemo } from 'react';
import { ortegaApi } from '@/api/ortega-client';
import { useMasterTable } from '@/hooks/useMasterData';
import { LocalRaidBannerMB } from '@/api/generated/localRaidBannerMB';
import { LocalRaidBonusScheduleMB } from '@/api/generated/localRaidBonusScheduleMB';
import { LocalRaidQuestInfo } from '@/api/generated/localRaidQuestInfo';
import { LocalRaidQuestGroupMB } from '@/api/generated/localRaidQuestGroupMB';
import { LocalRaidEnemyInfo } from '@/api/generated/localRaidEnemyInfo';
import { LocalRaidBattleLogInfo } from '@/api/generated/localRaidBattleLogInfo';
import { useTimeManager } from '@/hooks/useTimeManager';
import { useTranslation } from '@/hooks/useTranslation';
import { useAccountStore } from '@/store/accountStore';

/**
 * 幻影神殿信息 Hook
 * 获取幻影神殿的任务、敌人、奖励等信息
 */
export function useLocalRaidInfo() {
    const { t } = useTranslation();
    const [loading, setLoading] = useState(true);
    const [questInfos, setQuestInfos] = useState<LocalRaidQuestInfo[]>([]);
    const [enemyInfos, setEnemyInfos] = useState<LocalRaidEnemyInfo[]>([]);
    const [openQuestIds, setOpenQuestIds] = useState<number[]>([]);
    const [eventQuestIds, setEventQuestIds] = useState<number[]>([]);
    const [clearCountDict, setClearCountDict] = useState<Record<number, number>>({});
    const [challengeCount, setChallengeCount] = useState(0);
    const getCurrentUserSyncData = useAccountStore((state) => state.getCurrentUserSyncData);
    const currentUserSyncData = getCurrentUserSyncData();
    const playerId = currentUserSyncData?.userStatusDtoInfo?.playerId ?? 0;
    const timeManager = useTimeManager();

    // 获取 Master 数据
    const { data: bannerTable, loading: bannerLoading } = useMasterTable<LocalRaidBannerMB>('LocalRaidBannerTable');
    const { data: bonusScheduleTable, loading: bonusLoading } = useMasterTable<LocalRaidBonusScheduleMB>('LocalRaidBonusScheduleTable');
    const { data: questGroupTable, loading: questGroupLoading } = useMasterTable<LocalRaidQuestGroupMB>('LocalRaidQuestGroupTable');

    // 获取 API 数据
    const fetchData = useCallback(async () => {
        try {
            setLoading(true);
            const response = await ortegaApi.localRaid.getLocalRaidInfo({});
            setQuestInfos(response.localRaidQuestInfos || []);
            setEnemyInfos(response.localRaidEnemyInfos || []);
            setOpenQuestIds(response.openLocalRaidQuestIds || []);
            setEventQuestIds(response.openEventLocalRaidQuestIds || []);
            setClearCountDict(response.clearCountDict || {});
            setChallengeCount(response.userSyncData?.localRaidChallengeCount || 0);
        } catch (error) {
            console.error('Failed to fetch local raid info:', error);
        } finally {
            setLoading(false);
        }
    }, []);

    useEffect(() => {
        fetchData();
    }, [fetchData]);

    // 剩余次数（localRaidChallengeCount 就是剩余可用次数）
    const remainingCount = challengeCount;

    // 判断当前是否在加成时段
    const bonusTimeInfo = useMemo(() => {
        if (!bonusScheduleTable || bonusScheduleTable.length === 0) {
            return { inBonus: false, nextBonusTime: null, bonusRate: 0 };
        }

        const schedule = bonusScheduleTable[0];
        const now = new Date(timeManager.getServerNowMs());
        const currentTime = now.getHours() * 100 + now.getMinutes();
        const toHHMM = (timeValue: number) => Math.floor(timeValue / 100);

        for (const time of schedule.localRaidStartEndTimes) {
            const startTime = toHHMM(time.startTime);
            const endTime = toHHMM(time.endTime);
            if (currentTime >= startTime && currentTime <= endTime) {
                return {
                    inBonus: true,
                    nextBonusTime: null,
                    bonusRate: schedule.rewardBonusRate / 100
                };
            }
        }

        // 找下一个加成时段
        let nextTime: number | null = null;
        for (const time of schedule.localRaidStartEndTimes) {
            const startTime = toHHMM(time.startTime);
            if (startTime > currentTime) {
                nextTime = startTime;
                break;
            }
        }
        // 如果今天没有了，取明天的第一个
        if (nextTime === null && schedule.localRaidStartEndTimes.length > 0) {
            nextTime = toHHMM(schedule.localRaidStartEndTimes[0].startTime);
        }

        return {
            inBonus: false,
            nextBonusTime: nextTime,
            bonusRate: schedule.rewardBonusRate / 100
        };
    }, [bonusScheduleTable]);

    const bonusScheduleTimes = useMemo(() => {
        return bonusScheduleTable?.[0]?.localRaidStartEndTimes ?? [];
    }, [bonusScheduleTable]);

    const localRaidLevel = useMemo(() => {
        const worldCreateTimestamp = currentUserSyncData?.createWorldLocalTimeStamp;
        if (!questGroupTable || questGroupTable.length === 0 || !worldCreateTimestamp) {
            return null;
        }

        const nowMs = timeManager.getServerNowMs();
        const worldCreateMs = worldCreateTimestamp < 1_000_000_000_000
            ? worldCreateTimestamp * 1000
            : worldCreateTimestamp;
        const diffDays = Math.max(0, Math.floor((nowMs - worldCreateMs) / (1000 * 60 * 60 * 24)));

        let maxLevel: number | null = null;
        for (const group of questGroupTable) {
            if (group.overDayFromCreateWorldTime <= diffDays) {
                if (maxLevel === null || group.localRaidLevel > maxLevel) {
                    maxLevel = group.localRaidLevel;
                }
            }
        }

        return maxLevel;
    }, [currentUserSyncData?.createWorldLocalTimeStamp, questGroupTable, timeManager]);

    // 获取殿堂名称
    const getTempleName = useCallback((bannerId: number): string => {
        const banner = bannerTable?.find(b => b.id === bannerId);
        return banner?.nameKey || t('LOCAL_RAID_TEMPLE_FALLBACK', [bannerId]);
    }, [bannerTable, t]);

    // 获取敌人信息
    const getEnemyInfo = useCallback((enemyId: number): LocalRaidEnemyInfo | undefined => {
        return enemyInfos.find(e => e.id === enemyId);
    }, [enemyInfos]);

    // 获取任务的敌人列表
    const getQuestEnemies = useCallback((quest: LocalRaidQuestInfo): LocalRaidEnemyInfo[] => {
        return quest.localRaidEnemyIds
            .map(id => getEnemyInfo(id))
            .filter((e): e is LocalRaidEnemyInfo => e !== undefined);
    }, [getEnemyInfo]);

    // 判断任务是否已通关
    const isQuestCleared = useCallback((questId: number): boolean => {
        return (clearCountDict[questId] || 0) > 0;
    }, [clearCountDict]);

    return {
        loading: loading || bannerLoading || bonusLoading || questGroupLoading,
        questInfos,
        enemyInfos,
        openQuestIds,
        eventQuestIds,
        clearCountDict,
        remainingCount,
        challengeCount,
        bonusTimeInfo,
        bonusScheduleTimes,
        localRaidLevel,
        bannerTable,
        playerId,
        getTempleName,
        getEnemyInfo,
        getQuestEnemies,
        isQuestCleared,
        refresh: fetchData
    };
}

/**
 * 幻影神殿战斗记录 Hook
 */
export function useLocalRaidBattleLogs() {
    const [logs, setLogs] = useState<LocalRaidBattleLogInfo[]>([]);
    const [loading, setLoading] = useState(true);

    const fetchLogs = useCallback(async () => {
        try {
            setLoading(true);
            const response = await ortegaApi.localRaid.getLocalRaidBattleLogs({});
            setLogs(response.localRaidBattleLogInfoList || []);
        } catch (error) {
            console.error('Failed to fetch battle logs:', error);
        } finally {
            setLoading(false);
        }
    }, []);

    useEffect(() => {
        fetchLogs();
    }, [fetchLogs]);

    return { logs, loading, refresh: fetchLogs };
}
