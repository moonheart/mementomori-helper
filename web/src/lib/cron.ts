// Quartz cron "秒 分 时 日 月 周" ↔ 每小时 / 每天 / 每周; 解析不了的保持自定义文本
export type Mode = 'hourly' | 'daily' | 'weekly' | 'custom';
export const DAYS = ['MON', 'TUE', 'WED', 'THU', 'FRI', 'SAT', 'SUN'] as const;

export interface Parsed { mode: Mode; minute: number; hours: number[]; day: string }

const num = (s: string, max: number) => (/^\d+$/.test(s) && +s <= max ? +s : null);

export function parseCron(cron: string): Parsed {
    const custom: Parsed = { mode: 'custom', minute: 0, hours: [4], day: 'MON' };
    const p = cron.trim().split(/\s+/);
    if (p.length < 6 || p[0] !== '0' || p[3] !== '?' || p[4] !== '*' || (p[6] && p[6] !== '*')) return custom;
    const minute = num(p[1], 59);
    if (minute == null) return custom;
    let hours: number[] | null = null;
    if (p[2] === '*') hours = [];
    else if (/^\d+\/\d+$/.test(p[2])) {
        const [start, step] = p[2].split('/').map(Number); // "0/4" → 0,4,8,...
        hours = step > 0 && start < 24 ? Array.from({ length: Math.ceil((24 - start) / step) }, (_, i) => start + i * step) : null;
    } else {
        const list = p[2].split(',').map((h) => num(h, 23));
        hours = list.every((h) => h != null) ? (list as number[]) : null;
    }
    if (hours == null) return custom;
    if (p[5] === '*') return hours.length === 0 ? { mode: 'hourly', minute, hours: [4], day: 'MON' } : { mode: 'daily', minute, hours, day: 'MON' };
    if ((DAYS as readonly string[]).includes(p[5]) && hours.length === 1) return { mode: 'weekly', minute, hours, day: p[5] };
    return custom;
}

export function buildCron({ mode, minute, hours, day }: Parsed): string {
    const h = [...new Set(hours)].sort((a, b) => a - b).join(',');
    if (mode === 'hourly') return `0 ${minute} * ? * *`;
    if (mode === 'weekly') return `0 ${minute} ${hours[0]} ? * ${day}`;
    return `0 ${minute} ${h} ? * *`;
}
