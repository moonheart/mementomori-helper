import apiClient from './axios-client';

export const settingsApi = {
    /**
     * 获取玩家所有的配置项
     * @param userId 账户 ID
     */
    getAllSettings: async (userId: number): Promise<Record<string, unknown>> => {
        const response = await apiClient.get<Record<string, unknown>>(`/api/settings/${userId}`);
        return response.data;
    },

    /**
     * 获取玩家特定的配置子项 (例如 autojob, shop, dungeonbattle)
     * @param userId 账户 ID
     * @param key 配置键名
     */
    getSetting: async <T>(userId: number, key: string): Promise<T> => {
        const response = await apiClient.get<T>(`/api/settings/${userId}/${key}`);
        return response.data;
    },

    /**
     * 保存玩家特定的配置子项
     * @param userId 账户 ID
     * @param key 配置键名
     * @param value 配置内容
     */
    saveSetting: async (userId: number, key: string, value: unknown): Promise<{ success: boolean }> => {
        const response = await apiClient.post<{ success: boolean }>(`/api/settings/${userId}/${key}`, value);
        return response.data;
    }
};
