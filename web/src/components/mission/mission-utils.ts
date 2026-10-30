import { MissionStatusType, MissionInfo, UserMissionDtoInfo } from '@/api/generated';
import { UIStoreMission } from './MissionRow';

/**
 * 任务状态优先级排序
 * 可领取 (0) > 进行中 (1) > 未解锁 (2) > 已领取 (3)
 */
export function getMissionStatusPriority(status: MissionStatusType): number {
    if (status === MissionStatusType.NotReceived) return 0;
    if (status === MissionStatusType.Progress) return 1;
    if (status === MissionStatusType.Locked) return 2;
    if (status === MissionStatusType.Received) return 3;
    return 4;
}

/**
 * 从 MissionInfo 中解析出 UIStoreMission 列表。
 *
 * 每个 dto 的 missionStatusHistory 内，同一状态可包含多个 ID，
 * 代表连续推进的任务链（如 1989→1990→1991→1992 均已完成待领取）。
 * 处理策略：
 *  - NotReceived：全部展开 —— 每个都可单独领取，不能丢
 *  - Progress：只取链尾（最后一个）—— 当前推进中的那条
 *  - Locked：只取链头（第一个）—— 仅展示最近待解锁的
 *  - Received：跳过 —— 已完成无需再显示
 *
 * @param groupInfo 任务组信息（来自 missionInfoDict）
 * @param sort 是否按优先级排序（可领取 > 进行中 > 未解锁 > 已领取），默认 false
 */
export function mapMissionsFromGroupInfo(
    groupInfo: MissionInfo | undefined,
    sort = false
): UIStoreMission[] {
    if (!groupInfo?.userMissionDtoInfoDict) return [];

    const missions: UIStoreMission[] = [];

    Object.values(groupInfo.userMissionDtoInfoDict).forEach((dtoList) => {
        if (!dtoList) return;
        (dtoList as UserMissionDtoInfo[]).forEach((dto) => {
            const history = dto.missionStatusHistory as Record<string | number, number[]>;
            if (!history) return;

            // 兼容数字 Key 和字符串 Key（服务端可能返回任一格式）
            const getList = (type: MissionStatusType): number[] =>
                (history[type] as number[]) || (history[MissionStatusType[type]] as number[]) || [];

            const push = (id: number, status: MissionStatusType) =>
                missions.push({
                    id,
                    missionType: dto.missionType,
                    status,
                    progress: dto.progressCount
                });

            // 1. NotReceived —— 全部展开，每个都需要单独领取
            getList(MissionStatusType.NotReceived).forEach(id =>
                push(id, MissionStatusType.NotReceived)
            );

            // 2. Progress —— 只取链尾（当前进行中的那条）
            const progressList = getList(MissionStatusType.Progress);
            if (progressList.length > 0) {
                push(progressList[progressList.length - 1], MissionStatusType.Progress);
            }

            // 3. Locked —— 只取链头（最近待解锁的）
            const lockedList = getList(MissionStatusType.Locked);
            if (lockedList.length > 0) {
                push(lockedList[0], MissionStatusType.Locked);
            }

            // 4. Received —— 跳过，已完成无需显示
        });
    });

    if (sort) {
        missions.sort((a, b) => {
            const diff = getMissionStatusPriority(a.status) - getMissionStatusPriority(b.status);
            return diff !== 0 ? diff : a.id - b.id;
        });
    }

    return missions;
}
