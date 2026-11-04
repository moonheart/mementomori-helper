import { Button } from '@/components/ui/button';
import { Input } from '@/components/ui/input';
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from '@/components/ui/select';
import { Plus, RotateCcw, X } from 'lucide-react';
import { useTranslation } from '@/hooks/useTranslation';

import { buildCron, DAYS, parseCron, type Mode, type Parsed } from '@/lib/cron';

const hhmm = (h: number, m: number) => `${String(h).padStart(2, '0')}:${String(m).padStart(2, '0')}`;

/**
 * 给普通人用的定时选择: 每小时第几分 / 每天几点 (可多个) / 每周哪天几点 / 自定义 cron
 * 时间是服务器时间 (和顶部的 Server Time 一样)
 */
export function CronPicker({ value, onChange, defaultValue }: { value: string; onChange: (cron: string) => void; defaultValue?: string }) {
    const { t } = useTranslation();
    const parsed = parseCron(value);
    const set = (patch: Partial<Parsed>) => onChange(buildCron({ ...parsed, ...patch }));

    const setMode = (mode: Mode) => {
        if (mode === 'custom') return onChange(value);
        // 从每小时/自定义切到每天/每周时给个合理的默认时间 (04:xx, 刷新后)
        const hours = parsed.mode === 'hourly' || parsed.mode === 'custom' ? [4] : parsed.hours;
        onChange(buildCron({ ...parsed, mode, hours: mode === 'weekly' ? hours.slice(0, 1) : hours }));
    };

    const setTime = (i: number, time: string) => {
        const [h, m] = time.split(':').map(Number);
        if (Number.isNaN(h) || Number.isNaN(m)) return;
        const hours = [...parsed.hours];
        hours[i] = h;
        set({ hours, minute: m }); // 同一条 cron 只能有一个 "分", 所有时间共用
    };

    return (
        <div className="flex flex-wrap items-center gap-2">
            <Select value={parsed.mode} onValueChange={(v) => setMode(v as Mode)}>
                <SelectTrigger className="h-9 w-36"><SelectValue /></SelectTrigger>
                <SelectContent>
                    <SelectItem value="hourly">{t('CRON_HOURLY')}</SelectItem>
                    <SelectItem value="daily">{t('CRON_DAILY')}</SelectItem>
                    <SelectItem value="weekly">{t('CRON_WEEKLY')}</SelectItem>
                    <SelectItem value="custom">{t('CRON_CUSTOM')}</SelectItem>
                </SelectContent>
            </Select>

            {parsed.mode === 'hourly' && (
                <label className="flex items-center gap-2 text-sm text-muted-foreground">
                    {t('CRON_AT_MINUTE')}
                    <Input type="number" min={0} max={59} value={parsed.minute} className="h-9 w-20"
                        onChange={(e) => set({ minute: Math.min(59, Math.max(0, Number(e.target.value) || 0)) })} />
                </label>
            )}

            {parsed.mode === 'daily' && (
                <>
                    {parsed.hours.map((h, i) => (
                        <span key={i} className="flex items-center">
                            <Input type="time" value={hhmm(h, parsed.minute)} onChange={(e) => setTime(i, e.target.value)} className="h-9 w-28" aria-label={t('CRON_TIME')} />
                            {parsed.hours.length > 1 && (
                                <Button variant="ghost" size="icon" className="h-8 w-8" onClick={() => set({ hours: parsed.hours.filter((_, j) => j !== i) })} aria-label={t('CRON_REMOVE_TIME')}>
                                    <X className="h-3.5 w-3.5" />
                                </Button>
                            )}
                        </span>
                    ))}
                    {parsed.hours.length < 24 && (
                        <Button variant="ghost" size="sm" onClick={() => set({ hours: [...parsed.hours, (Math.max(...parsed.hours) + 1) % 24] })}>
                            <Plus className="mr-1 h-4 w-4" />{t('CRON_ADD_TIME')}
                        </Button>
                    )}
                </>
            )}

            {parsed.mode === 'weekly' && (
                <>
                    <Select value={parsed.day} onValueChange={(day) => set({ day })}>
                        <SelectTrigger className="h-9 w-32"><SelectValue /></SelectTrigger>
                        <SelectContent>
                            {DAYS.map((d) => <SelectItem key={d} value={d}>{t(`CRON_DAY_${d}`)}</SelectItem>)}
                        </SelectContent>
                    </Select>
                    <Input type="time" value={hhmm(parsed.hours[0], parsed.minute)} onChange={(e) => setTime(0, e.target.value)} className="h-9 w-28" aria-label={t('CRON_TIME')} />
                </>
            )}

            {parsed.mode === 'custom' && (
                <Input value={value} onChange={(e) => onChange(e.target.value)} className="h-9 w-56 font-mono text-sm" placeholder="0 30 4 ? * *" aria-label={t('CRON_CUSTOM')} />
            )}

            {defaultValue && value !== defaultValue && (
                <Button variant="ghost" size="icon" className="h-8 w-8" onClick={() => onChange(defaultValue)} title={t('AUTO_RESET_DEFAULT')} aria-label={t('AUTO_RESET_DEFAULT')}>
                    <RotateCcw className="h-4 w-4" />
                </Button>
            )}
            <span className="text-xs text-muted-foreground">{t('CRON_SERVER_TIME')}</span>
        </div>
    );
}
