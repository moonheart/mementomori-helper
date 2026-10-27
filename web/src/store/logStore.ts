import { create } from 'zustand';

export interface JobLog {
    time: string;
    level: string;
    message: string;
}

type LogViewState = 'collapsed' | 'compact' | 'expanded';

interface LogState {
    logs: JobLog[];
    isConnected: boolean;
    viewState: LogViewState;
    
    // Actions
    addLog: (log: JobLog) => void;
    clearLogs: () => void;
    setIsConnected: (connected: boolean) => void;
    setViewState: (state: LogViewState) => void;
    toggleViewState: () => void;
}

const MAX_LOGS = 100;

export const useLogStore = create<LogState>()((set, get) => ({
    logs: [],
    isConnected: false,
    viewState: 'collapsed',

    addLog: (log: JobLog) => {
        set((state) => ({
            logs: [...state.logs, log].slice(-MAX_LOGS) // 新日志追加到末尾，保留最近100条
        }));
    },

    clearLogs: () => set({ logs: [] }),

    setIsConnected: (connected: boolean) => set({ isConnected: connected }),

    setViewState: (state: LogViewState) => set({ viewState: state }),

    toggleViewState: () => {
        const { viewState } = get();
        const states: LogViewState[] = ['collapsed', 'compact', 'expanded'];
        const currentIndex = states.indexOf(viewState);
        const nextIndex = (currentIndex + 1) % states.length;
        set({ viewState: states[nextIndex] });
    },
}));
