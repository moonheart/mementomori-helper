import { create } from 'zustand';
import { persist } from 'zustand/middleware';
import type { UserStatusDtoInfo } from '../api/generated/userStatusDtoInfo';

interface UserState {
    userInfo: UserStatusDtoInfo | null;
    setUserInfo: (info: UserStatusDtoInfo | null) => void;
    getPlayerId: () => number;
}

export const useUserStore = create<UserState>()(
    persist(
        (set, get) => ({
            userInfo: null,

            setUserInfo: (info) => set({ userInfo: info }),

            getPlayerId: () => get().userInfo?.playerId || 0,
        }),
        {
            name: 'user-storage',
            partialize: (state) => ({
                userInfo: state.userInfo,
            }),
        }
    )
);
