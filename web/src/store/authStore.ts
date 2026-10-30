import { create } from 'zustand';

interface AuthState {
    currentAccountId: number | null;
    isAuthenticated: boolean;
    setCurrentAccount: (accountId: number | null) => void;
    logout: () => void;
}

export const useAuthStore = create<AuthState>((set) => ({
    currentAccountId: null,
    isAuthenticated: false,
    setCurrentAccount: (accountId) =>
        set({ currentAccountId: accountId, isAuthenticated: accountId !== null }),
    logout: () => set({ currentAccountId: null, isAuthenticated: false }),
}));
