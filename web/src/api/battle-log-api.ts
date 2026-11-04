import apiClient from './axios-client';

export interface BattleLogSummary {
    battleToken: string;
    createdAt: string;
    battleType: number;
    questId?: number;
    isWin: boolean;
    endTurn: number;
    battleDurationMs: number;
    apiUri: string;
}

export interface BattleLogDetail extends BattleLogSummary {
    battleDataJson: string;
}

export interface BattleLogListResponse {
    data: BattleLogSummary[];
    pagination: {
        page: number;
        pageSize: number;
        total: number;
        totalPages: number;
    };
}

export interface BattleLogQueryParams {
    battleType?: number;
    startDate?: string;
    endDate?: string;
    page?: number;
    pageSize?: number;
}

export const battleLogApi = {
    /**
     * 获取战斗日志列表
     */
    getList: async (params: BattleLogQueryParams = {}): Promise<BattleLogListResponse> => {
        const searchParams = new URLSearchParams();
        if (params.battleType !== undefined) searchParams.set('battleType', params.battleType.toString());
        if (params.startDate) searchParams.set('startDate', params.startDate);
        if (params.endDate) searchParams.set('endDate', params.endDate);
        if (params.page !== undefined) searchParams.set('page', params.page.toString());
        if (params.pageSize !== undefined) searchParams.set('pageSize', params.pageSize.toString());

        const response = await apiClient.get<BattleLogListResponse>(`/api/battle-logs?${searchParams.toString()}`);
        return response.data;
    },

    /**
     * 获取战斗日志详情
     */
    getDetail: async (battleToken: string): Promise<BattleLogDetail> => {
        const response = await apiClient.get<BattleLogDetail>(`/api/battle-logs/${battleToken}`);
        return response.data;
    },

    /**
     * 删除战斗日志
     */
    delete: async (battleToken: string): Promise<void> => {
        await apiClient.delete(`/api/battle-logs/${battleToken}`);
    },
};
