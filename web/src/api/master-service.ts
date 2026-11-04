import axiosClient from './axios-client';
import { MasterBookBase } from './generated/masterBookBase';

export interface MasterTableManifest {
    name: string;
    hash: string;
    count: number;
}

export interface MasterManifest {
    version: string;
    tables: MasterTableManifest[];
}

export const masterService = {
    /**
     * 获取 Master 清单
     */
    getManifest: async (): Promise<MasterManifest> => {
        const response = await axiosClient.get<MasterManifest>('/api/master/manifest');
        return response.data;
    },

    /**
     * 获取单表全量数据
     */
    getTableData: async <T extends MasterBookBase>(tableName: string): Promise<T[]> => {
        const response = await axiosClient.get<T[]>(`/api/master/table/${tableName}`);
        return response.data;
    }
};
