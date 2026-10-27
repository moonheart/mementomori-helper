import { create } from 'zustand';
import { persist } from 'zustand/middleware';
import type { AccountDto } from '../api/generated/accountDto';
import type { UserSyncData } from '@/api/generated/userSyncData';
import { mergeUserSyncData } from '@/lib/user-sync-data';

interface AccountState {
    accounts: AccountDto[];
    currentAccountId: number | null;
    isLoading: boolean;
    error: string | null;
    userSyncDataByAccountId: Record<number, UserSyncData | null>;

    // Actions
    setAccounts: (accounts: AccountDto[]) => void;
    addAccount: (account: AccountDto) => void;
    removeAccount: (userId: number) => void;
    setCurrentAccount: (userId: number | null) => void;
    setLoading: (loading: boolean) => void;
    setError: (error: string | null) => void;
    clearError: () => void;
    setUserSyncData: (userId: number, data: UserSyncData | null) => void;
    mergeUserSyncData: (userId: number, data: UserSyncData | null | undefined) => void;
    getCurrentUserSyncData: () => UserSyncData | null;
}

export const useAccountStore = create<AccountState>()(
    persist(
        (set, get) => ({
            accounts: [],
            currentAccountId: null,
            isLoading: false,
            error: null,
            userSyncDataByAccountId: {},

            setAccounts: (accounts) => set({ accounts }),

            addAccount: (account) =>
                set((state) => ({
                    accounts: [...state.accounts, account],
                })),

            removeAccount: (userId) =>
                set((state) => {
                    const { [userId]: removedUserSyncData, ...rest } = state.userSyncDataByAccountId;
                    void removedUserSyncData;
                    return {
                        accounts: state.accounts.filter((a) => a.userId !== userId),
                        currentAccountId:
                            state.currentAccountId === userId ? null : state.currentAccountId,
                        userSyncDataByAccountId: rest,
                    };
                }),

            setCurrentAccount: (userId) => set({ currentAccountId: userId }),

            setLoading: (loading) => set({ isLoading: loading }),

            setError: (error) => set({ error }),

            clearError: () => set({ error: null }),

            setUserSyncData: (userId, data) =>
                set((state) => ({
                    userSyncDataByAccountId: {
                        ...state.userSyncDataByAccountId,
                        [userId]: data,
                    },
                })),

            mergeUserSyncData: (userId, data) =>
                set((state) => ({
                    userSyncDataByAccountId: {
                        ...state.userSyncDataByAccountId,
                        [userId]: mergeUserSyncData(state.userSyncDataByAccountId[userId], data),
                    },
                })),

            getCurrentUserSyncData: () => {
                const currentId = get().currentAccountId;
                if (!currentId) return null;
                return get().userSyncDataByAccountId[currentId] ?? null;
            },
        }),
        {
            name: 'account-storage',
            partialize: (state) => ({
                accounts: state.accounts,
                currentAccountId: state.currentAccountId,
                userSyncDataByAccountId: state.userSyncDataByAccountId,
            }),
        }
    )
);
