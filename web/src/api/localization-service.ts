import axiosInstance from './axios-client';

export interface LocalizationManifest {
    language: string;
    hash: string;
    count: number;
    lastUpdated: number;
}

export const localizationApi = {
    /**
     * 获取指定语言的本地化资源
     * @param lang 语言代码，如 'zhCN', 'jaJP', 'enUS'（默认 zhCN）
     */
    getResources: async (lang: string = 'zhCN'): Promise<Record<string, string>> => {
        const response = await axiosInstance.get<Record<string, string>>(`/api/localization/resources`, {
            params: { lang }
        });
        return response.data;
    },

    /**
     * 获取指定语言资源的 manifest (包含 hash 用于版本检查)
     * @param lang 语言代码
     */
    getManifest: async (lang: string = 'zhCN'): Promise<LocalizationManifest> => {
        const response = await axiosInstance.get<LocalizationManifest>(`/api/localization/manifest`, {
            params: { lang }
        });
        return response.data;
    }
};
