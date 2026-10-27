import apiClient from './axios-client';

export interface AutopilotStep {
    key: string;
    name: string;
}

export interface AutopilotConfig {
    enabled: boolean;
    cron: string;
    steps: string[];
    gachaDiamondReserve: number;
    gachaMinPickupTier: number;
    rankUpWithFodder: boolean;
}

export type GrowthKind = 'Level' | 'LevelLink' | 'Equip' | 'Reinforce' | 'Quest' | 'Tower' | 'RankUp' | 'Gacha' | 'Team' | 'Info';

export interface GrowthEvent {
    id: number;
    time: string;
    kind: GrowthKind;
    text: string;
}

export interface ProgressSnapshot {
    time: string;
    questId: number;
    questName: string;
    linkLevel: number;
    playerRank: number;
    gold: number;
    diamonds: number;
    towers: Record<string, number>;
}

export interface TierRow {
    characterId: number;
    name: string;
    tier: string | null;
    overridden: boolean;
    role: string | null;
    notes: string | null;
    rarity: string;
    element: string;
    level: number;
    inTeam: boolean;
    recommended: boolean;
}

export interface BannerAdvice {
    gachaCaseId: number;
    name: string;
    pickupCharacterId: number;
    pickupTier: string | null;
    diamondButtonId: number | null;
    diamondCost: number;
    ticketButtonId: number | null;
    verdict: string;
    worth: boolean;
}

export const TIERS = ['SS', 'S', 'A', 'B', 'C'] as const;

/**
 * Autopilot: 自动成长/推图/抽卡 + 成长记录 + 强度表
 */
export const autopilotApi = {
    getSteps: async () => (await apiClient.get<AutopilotStep[]>('/api/autopilot/steps')).data,
    getConfig: async (userId: number) => (await apiClient.get<AutopilotConfig>(`/api/autopilot/${userId}/config`)).data,
    saveConfig: async (userId: number, config: AutopilotConfig) => {
        await apiClient.post(`/api/autopilot/${userId}/config`, config);
    },
    run: async (userId: number, step?: string) => {
        await apiClient.post(`/api/autopilot/${userId}/run`, null, { params: step ? { step } : undefined });
    },
    getEvents: async (userId: number, limit = 200) =>
        (await apiClient.get<GrowthEvent[]>(`/api/autopilot/${userId}/events`, { params: { limit } })).data,
    getSnapshots: async (userId: number, days = 30) =>
        (await apiClient.get<ProgressSnapshot[]>(`/api/autopilot/${userId}/snapshots`, { params: { days } })).data,
    getTiers: async (userId: number) => (await apiClient.get<TierRow[]>(`/api/autopilot/${userId}/tiers`)).data,
    setTier: async (characterId: number, tier: string | null) => {
        await apiClient.put(`/api/autopilot/tiers/${characterId}`, JSON.stringify(tier ?? ''), {
            headers: { 'Content-Type': 'application/json' }
        });
    },
    getBanners: async (userId: number) => (await apiClient.get<BannerAdvice[]>(`/api/autopilot/${userId}/banners`)).data,
    draw: async (userId: number, buttonId: number, name: string) => {
        await apiClient.post(`/api/autopilot/${userId}/banners/draw`, { buttonId, name });
    },
};
