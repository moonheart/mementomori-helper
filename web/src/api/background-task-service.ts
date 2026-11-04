import apiClient from './axios-client';
import { BackgroundTaskStatus } from './generated/backgroundTaskStatus';
import { StartAutoBossRequest } from './generated/startAutoBossRequest';
import { StartAutoTowerRequest } from './generated/startAutoTowerRequest';
import { TowerType } from './generated/towerType';

/**
 * 后台任务管理服务
 * 管理用户的长时间运行任务（自动刷主线、自动爬塔等）
 */
export const backgroundTaskApi = {
    /**
     * 启动自动刷主线任务
     * @param userId 账户 ID
     * @param targetQuestId 目标关卡ID（可选）
     */
    startAutoBoss: async (userId: number, targetQuestId?: number): Promise<void> => {
        const request = new StartAutoBossRequest();
        if (targetQuestId !== undefined) {
            request.targetQuestId = targetQuestId;
        }
        await apiClient.post(`/api/backgroundtask/${userId}/auto-boss/start`, request);
    },

    /**
     * 停止自动刷主线任务
     * @param userId 账户 ID
     */
    stopAutoBoss: async (userId: number): Promise<void> => {
        await apiClient.post(`/api/backgroundtask/${userId}/auto-boss/stop`);
    },

    /**
     * 启动自动爬塔任务
     * @param userId 账户 ID
     * @param towerType 塔类型
     * @param targetFloor 目标层数（可选）
     */
    startAutoTower: async (userId: number, towerType: TowerType, targetFloor?: number): Promise<void> => {
        const request = new StartAutoTowerRequest();
        request.towerType = towerType;
        if (targetFloor !== undefined) {
            request.targetFloor = targetFloor;
        }
        await apiClient.post(`/api/backgroundtask/${userId}/auto-tower/start`, request);
    },

    /**
     * 停止自动爬塔任务
     * @param userId 账户 ID
     */
    stopAutoTower: async (userId: number): Promise<void> => {
        await apiClient.post(`/api/backgroundtask/${userId}/auto-tower/stop`);
    },

    /**
     * 获取后台任务状态
     * @param userId 账户 ID
     */
    getStatus: async (userId: number): Promise<BackgroundTaskStatus> => {
        const response = await apiClient.get<BackgroundTaskStatus>(`/api/backgroundtask/${userId}/status`);
        return response.data;
    }
};

export default backgroundTaskApi;
