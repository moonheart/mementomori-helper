import apiClient from './axios-client';
import { AutoJobModel } from './generated/autoJobModel';
import { JobStatusDto } from './generated/jobStatusDto';
import { TriggerJobRequest } from './generated/triggerJobRequest';

/**
 * 定时任务管理服务
 */
export interface PauseState {
    paused: boolean;
    until: string | null;
}

export const jobsApi = {
    getPause: async (userId: number) => (await apiClient.get<PauseState>(`/api/jobs/${userId}/pause`)).data,
    /** hours 为 null = 一直暂停到手动恢复 */
    pause: async (userId: number, hours: number | null) => {
        await apiClient.post(`/api/jobs/${userId}/pause`, { hours });
    },
    resume: async (userId: number) => {
        await apiClient.delete(`/api/jobs/${userId}/pause`);
    },

    /**
     * 获取账户的所有任务状态
     * @param userId 账户 ID
     */
    getStatus: async (userId: number): Promise<JobStatusDto[]> => {
        const response = await apiClient.get<JobStatusDto[]>(`/api/jobs/${userId}/status`);
        return response.data;
    },

    /**
     * 获取账户的自动化配置
     * @param userId 账户 ID
     */
    getConfig: async (userId: number): Promise<AutoJobModel> => {
        const response = await apiClient.get<AutoJobModel>(`/api/jobs/${userId}/config`);
        return response.data;
    },

    /**
     * 更新账户的自动化配置并刷新任务
     * @param userId 账户 ID
     * @param config 配置对象
     */
    updateConfig: async (userId: number, config: AutoJobModel): Promise<void> => {
        await apiClient.post(`/api/jobs/${userId}/config`, config);
    },

    /**
     * 立即触发某个任务
     * @param userId 账户 ID
     * @param jobType 任务类型名称
     */
    triggerJob: async (userId: number, jobType: string): Promise<void> => {
        const request: TriggerJobRequest = { jobType };
        await apiClient.post(`/api/jobs/${userId}/trigger`, request);
    }
};

export default jobsApi;
