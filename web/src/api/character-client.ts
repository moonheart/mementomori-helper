import apiClient from './axios-client';
import type { CharacterDetailInfo } from '@/api/generated';

export async function getCalculatedCharacterDetail(userCharacterGuid: string): Promise<CharacterDetailInfo> {
    const response = await apiClient.get<CharacterDetailInfo>(`/api/character/${userCharacterGuid}/calculated-detail`);
    return response.data;
}
