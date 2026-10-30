// 深色/浅色主题: 存在 localStorage, 没选过就跟系统
export type Theme = 'light' | 'dark';
const KEY = 'app_theme';

export function getTheme(): Theme {
    try {
        const saved = localStorage.getItem(KEY);
        if (saved === 'light' || saved === 'dark') return saved;
    } catch { /* 隐私模式读不了就跟系统 */ }
    return window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light';
}

export function applyTheme(theme: Theme, save = false) {
    document.documentElement.classList.toggle('dark', theme === 'dark');
    document.documentElement.style.colorScheme = theme;
    if (save) {
        try { localStorage.setItem(KEY, theme); } catch { /* ignore */ }
    }
}
