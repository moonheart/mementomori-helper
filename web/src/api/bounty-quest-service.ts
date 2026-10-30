import axiosClient from './axios-client';
import { BountyQuestType } from './generated/bountyQuestType';

export interface DispatchResponse {
    successCount: number;
    totalAttempted: number;
    totalRequested: number;
    results: {
        questId: number;
        success: boolean;
        error?: string;
    }[];
}

export const bountyQuestService = {
    /**
     * 自动派遣悬赏任务
     * @param params 
     */
    async dispatch(params: { bountyQuestId?: number; bountyQuestType?: BountyQuestType }): Promise<DispatchResponse> {
        const response = await axiosClient.post<DispatchResponse>('/api/BountyQuest/dispatch', params);
        return response.data;
    }
};
