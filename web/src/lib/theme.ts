// 深色/浅色主题: 存在 localStorage, 没选过就跟系统
export type Theme = 'light' | 'dark';
const KEY = 'app_theme';

// Android 壳 (MementoMori.Android/MainActivity.cs) 注入的桥, 浏览器里没有
declare global {
    interface Window {
        AndroidTheme?: { setTheme(rgb: string, dark: boolean): void };
    }
}

export function getTheme(): Theme {
    try {
        const saved = localStorage.getItem(KEY);
        if (saved === 'light' || saved === 'dark') return saved;
    } catch { /* 隐私模式读不了就跟系统 */ }
    return window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light';
}

/** body 底色 -> "r,g,b"; 主题色是 oklch, 直接读字符串原生解析不了, 借 canvas 转成 sRGB */
function bodyRgb(): string {
    const canvas = document.createElement('canvas');
    canvas.width = canvas.height = 1;
    const ctx = canvas.getContext('2d');
    if (!ctx) return '';
    ctx.fillStyle = '#010203'; // 哨兵: 解析失败时 fillStyle 不会变
    ctx.fillStyle = getComputedStyle(document.body).backgroundColor;
    if (ctx.fillStyle === '#010203') return '';
    ctx.fillRect(0, 0, 1, 1);
    const [r, g, b] = ctx.getImageData(0, 0, 1, 1).data;
    return `${r},${g},${b}`;
}

export function applyTheme(theme: Theme, save = false) {
    document.documentElement.classList.toggle('dark', theme === 'dark');
    document.documentElement.style.colorScheme = theme;
    if (save) {
        try { localStorage.setItem(KEY, theme); } catch { /* ignore */ }
    }
    // 状态栏那条让出来的边是原生画的, 得知道页面底色和明暗 (图标颜色跟着变)
    try {
        window.AndroidTheme?.setTheme(bodyRgb(), theme === 'dark');
    } catch { /* 桥不可用不影响页面 */ }
}
