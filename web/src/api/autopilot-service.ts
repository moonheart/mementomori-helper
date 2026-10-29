import apiClient from './axios-client';
import type { UserCharacterInfo } from './generated/userCharacterInfo';

export interface AutopilotStep {
    key: string;
    name: string;
}

export interface AutopilotConfig {
    enabled: boolean;
    cron: string;
    steps: string[];
    gachaSpendDiamonds: boolean;
    gachaDiamondReserve: number;
    gachaMinPickupTier: number;
    rankUpWithFodder: boolean;
    gachaSurplusAbove: number;
}

export type GrowthKind = 'Level' | 'LevelLink' | 'Equip' | 'Reinforce' | 'Quest' | 'Tower' | 'RankUp' | 'Gacha' | 'Team' | 'Friend' | 'Reward' | 'Info';

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
    power: number;
    towers: Record<string, number>;
}

export interface TierRow {
    characterId: number;
    guid: string;
    rarityFlags: number;
    name: string;
    tier: string | null;
    overridden: boolean;
    role: string | null;
    notes: string | null;
    rarity: string;
    element: string;
    level: number;
    linked: boolean;
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
    ticketName: string | null;
    ticketsOwned: number;
    ticketCost: number;
    freeDiamonds: number;
}

export interface OverviewStats {
    questId: number;
    questName: string;
    linkLevel: number;
    tower: number;
    rank: number;
    power: number;
    gold: number;
    diamonds: number;
}

export interface AutopilotOverview {
    enabled: boolean;
    nextRun: string | null;
    now: OverviewStats;
    dayAgo: OverviewStats | null;
    team: { characterId: number; guid: string; rarityFlags: number; name: string; tier: string | null; rarity: string; level: number; gear: number; avgReinforce: number }[];
}

/** CharacterIcon 只用到 characterId / guid / level / rarityFlags */
export const toCharacterInfo = (c: { characterId: number; guid: string; rarityFlags: number; level: number }) =>
    ({ characterId: c.characterId, guid: c.guid, level: c.level, rarityFlags: c.rarityFlags, subLevel: 0, exp: 0, playerId: 0, isLocked: false }) as unknown as UserCharacterInfo;

export interface TamamoCharacter { characterId: number; name: string; rarityFlags: number; tier: string | null; owned: boolean }
export interface TamamoTeam { characters: TamamoCharacter[]; clears: number; minPower: number }
export interface TamamoStageTeams { stage: string; dataStage: string | null; stuck: number; teams: TamamoTeam[]; lowest: TamamoTeam | null; link: string;
    usageStage: string | null; usage: { character: TamamoCharacter; count: number }[] | null }

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
    getOverview: async (userId: number) => (await apiClient.get<AutopilotOverview>(`/api/autopilot/${userId}/overview`)).data,
    getEvents: async (userId: number, limit = 200) =>
        (await apiClient.get<GrowthEvent[]>(`/api/autopilot/${userId}/events`, { params: { limit } })).data,
    getSnapshots: async (userId: number, days = 30) =>
        (await apiClient.get<ProgressSnapshot[]>(`/api/autopilot/${userId}/snapshots`, { params: { days } })).data,
    // 204 = 没配 Tamamo key
    getTamamo: async (userId: number, refresh = false) =>
        (await apiClient.get<TamamoStageTeams | ''>(`/api/autopilot/${userId}/tamamo`, { params: refresh ? { refresh } : undefined })).data || null,
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
