import { useCallback, useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import dayjs from 'dayjs';
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
import { Button } from '@/components/ui/button';
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from '@/components/ui/select';
import { ChevronRight, Flag, Link2, Medal, Play, Shield, Castle, Swords } from 'lucide-react';
import { cn } from '@/lib/utils';
import { useAccountStore } from '@/store/accountStore';
import { useToast } from '@/hooks/use-toast';
import { useTranslation } from '@/hooks/useTranslation';
import { autopilotApi, type AutopilotOverview as Overview, type GrowthEvent, type ProgressSnapshot } from '@/api/autopilot-service';
import { GrowthFeed } from './GrowthFeed';

const tierStyle: Record<string, string> = {
    SS: 'bg-rose-500 text-white', S: 'bg-amber-500 text-white', A: 'bg-emerald-500 text-white', B: 'bg-sky-500 text-white', C: 'bg-slate-400 text-white',
};

const short = (v: number) =>
    v >= 1e9 ? `${(v / 1e9).toFixed(2)}B` : v >= 1e6 ? `${(v / 1e6).toFixed(2)}M` : v >= 1e4 ? `${(v / 1e3).toFixed(1)}K` : v.toLocaleString();

type Metric = 'link' | 'quest' | 'tower' | 'rank' | 'power' | 'gold' | 'diamonds';
const METRICS: { key: Metric; labelKey: string; value: (s: ProgressSnapshot) => number }[] = [
    { key: 'link', labelKey: 'AP_SERIES_LINK', value: (s) => s.linkLevel },
    { key: 'quest', labelKey: 'AP_SERIES_QUEST', value: (s) => s.questId },
    { key: 'tower', labelKey: 'AP_SERIES_TOWER', value: (s) => s.towers?.Infinite ?? 0 },
    { key: 'rank', labelKey: 'HOME_RANK', value: (s) => s.playerRank },
    { key: 'power', labelKey: 'HOME_POWER', value: (s) => s.power },
    { key: 'gold', labelKey: 'HOME_GOLD', value: (s) => s.gold },
    { key: 'diamonds', labelKey: 'HOME_DIAMONDS', value: (s) => s.diamonds },
];

function Kpi({ icon: Icon, label, value, delta }: { icon: React.ElementType; label: string; value: string; delta?: number }) {
    return (
        <Card>
            <CardContent className="flex h-full flex-col justify-center p-4">
                <div className="mb-1 flex items-center gap-2 text-sm text-muted-foreground">
                    <Icon className="h-4 w-4" />{label}
                </div>
                <div className="truncate text-xl font-bold tabular-nums">{value}</div>
                {delta != null && delta !== 0 && (
                    <div className={cn('text-xs tabular-nums', delta > 0 ? 'text-emerald-600 dark:text-emerald-400' : 'text-rose-600 dark:text-rose-400')}>
                        {delta > 0 ? '▲' : '▼'} {short(Math.abs(delta))} · 24h
                    </div>
                )}
            </CardContent>
        </Card>
    );
}

function MetricChart({ snapshots }: { snapshots: ProgressSnapshot[] }) {
    const { t } = useTranslation();
    const [metric, setMetric] = useState<Metric>('link');
    const m = METRICS.find((x) => x.key === metric)!;
    const points = snapshots.slice(-72);
    const W = 600, H = 180, P = 8;
    const values = points.map(m.value);
    const min = Math.min(...values), max = Math.max(...values);
    const x = (i: number) => P + (i / Math.max(1, points.length - 1)) * (W - P * 2);
    const y = (v: number) => H - P - ((v - min) / Math.max(1, max - min)) * (H - P * 2);
    const d = values.map((v, i) => `${i ? 'L' : 'M'}${x(i).toFixed(1)},${y(v).toFixed(1)}`).join(' ');

    return (
        <Card>
            <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
                <CardTitle className="text-base">{t('AP_PROGRESS')}</CardTitle>
                <Select value={metric} onValueChange={(v) => setMetric(v as Metric)}>
                    <SelectTrigger className="h-8 w-40"><SelectValue /></SelectTrigger>
                    <SelectContent>
                        {METRICS.map((x) => <SelectItem key={x.key} value={x.key}>{t(x.labelKey)}</SelectItem>)}
                    </SelectContent>
                </Select>
            </CardHeader>
            <CardContent>
                {points.length < 2 ? (
                    <p className="py-10 text-center text-sm text-muted-foreground">{t('AP_NO_SNAPSHOTS')}</p>
                ) : (
                    <>
                        <svg viewBox={`0 0 ${W} ${H}`} className="h-44 w-full" preserveAspectRatio="none" role="img" aria-label={t(m.labelKey)}>
                            <path d={`${d} L${x(points.length - 1)},${H} L${x(0)},${H} Z`} fill="#8b5cf6" opacity={0.12} />
                            <path d={d} fill="none" stroke="#8b5cf6" strokeWidth={2} vectorEffect="non-scaling-stroke" />
                        </svg>
                        <div className="mt-1 flex justify-between text-xs tabular-nums text-muted-foreground">
                            <span>{dayjs(points[0].time).format('MM-DD HH:mm')} · {short(values[0])}</span>
                            <span>{short(values[values.length - 1])} · {dayjs(points[points.length - 1].time).format('MM-DD HH:mm')}</span>
                        </div>
                    </>
                )}
            </CardContent>
        </Card>
    );
}

/**
 * Home 页面顶部: autopilot 状态, 24h 变化, 主队, 曲线, 最近成长记录
 */
export function AutopilotOverview() {
    const { t } = useTranslation();
    const { toast } = useToast();
    const userId = useAccountStore((s) => s.currentAccountId);
    const [overview, setOverview] = useState<Overview | null>(null);
    const [snapshots, setSnapshots] = useState<ProgressSnapshot[]>([]);
    const [events, setEvents] = useState<GrowthEvent[]>([]);

    const load = useCallback(async () => {
        if (!userId) return;
        try {
            const [o, s, e] = await Promise.all([autopilotApi.getOverview(userId), autopilotApi.getSnapshots(userId, 7), autopilotApi.getEvents(userId, 15)]);
            setOverview(o);
            setSnapshots(s);
            setEvents(e);
        } catch {
            setOverview(null); // 没登录时不显示
        }
    }, [userId]);

    useEffect(() => {
        const first = setTimeout(load, 0);
        const id = setInterval(load, 30000);
        return () => { clearTimeout(first); clearInterval(id); };
    }, [load]);

    if (!overview) return null;
    const { now, dayAgo } = overview;
    const delta = (k: keyof typeof now) => (dayAgo ? (now[k] as number) - (dayAgo[k] as number) : undefined);

    const run = async () => {
        if (!userId) return;
        await autopilotApi.run(userId);
        toast({ title: t('AP_STARTED'), description: t('AP_STARTED_DESC') });
    };

    return (
        <div className="space-y-4">
            <Card>
                <CardContent className="flex flex-wrap items-center justify-between gap-4 p-4">
                    <div className="flex items-center gap-3">
                        <span className={cn('h-3 w-3 rounded-full', overview.enabled ? 'bg-emerald-500 shadow-[0_0_0_4px] shadow-emerald-500/20' : 'bg-muted-foreground/40')} />
                        <div>
                            <p className="font-semibold">{overview.enabled ? t('HOME_AUTOPILOT_ON') : t('HOME_AUTOPILOT_OFF')}</p>
                            <p className="text-sm text-muted-foreground">
                                {overview.enabled && overview.nextRun
                                    ? t('HOME_NEXT_RUN', [dayjs(overview.nextRun).format('HH:mm')])
                                    : t('HOME_AUTOPILOT_OFF_HINT')}
                            </p>
                        </div>
                    </div>
                    <div className="flex gap-2">
                        <Button variant="outline" asChild>
                            <Link to="/autopilot">{t('HOME_CONFIGURE')}<ChevronRight className="ml-1 h-4 w-4" /></Link>
                        </Button>
                        <Button onClick={run}><Play className="mr-2 h-4 w-4" />{t('AP_RUN_NOW')}</Button>
                    </div>
                </CardContent>
            </Card>

            <div className="grid grid-cols-2 gap-4 md:grid-cols-3 lg:grid-cols-5">
                <Kpi icon={Flag} label={t('AP_SERIES_QUEST')} value={now.questName} delta={delta('questId')} />
                <Kpi icon={Link2} label={t('AP_SERIES_LINK')} value={String(now.linkLevel)} delta={delta('linkLevel')} />
                <Kpi icon={Castle} label={t('AP_SERIES_TOWER')} value={String(now.tower)} delta={delta('tower')} />
                <Kpi icon={Medal} label={t('HOME_RANK')} value={String(now.rank)} delta={delta('rank')} />
                <Kpi icon={Swords} label={t('HOME_POWER')} value={short(now.power)} delta={delta('power')} />
            </div>

            <div className="grid gap-4 lg:grid-cols-2">
                <Card>
                    <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
                        <CardTitle className="text-base">{t('HOME_MAIN_TEAM')}</CardTitle>
                        <Button variant="ghost" size="sm" asChild>
                            <Link to="/team-tiers">{t('SIDEBAR_TEAM_TIERS')}<ChevronRight className="ml-1 h-4 w-4" /></Link>
                        </Button>
                    </CardHeader>
                    <CardContent>
                        <ul className="grid grid-cols-2 gap-2 sm:grid-cols-3 xl:grid-cols-5">
                            {overview.team.map((c) => (
                                <li key={c.characterId} className="space-y-1 rounded-lg border p-2 text-center">
                                    <p className="truncate text-sm font-medium">{c.name}</p>
                                    <p className="text-xs text-muted-foreground">
                                        <span className={cn('mr-1 rounded px-1 font-bold', c.tier ? tierStyle[c.tier] : 'bg-muted')}>{c.tier ?? '?'}</span>
                                        {c.rarity} · Lv{c.level}
                                    </p>
                                    <p className="flex items-center justify-center gap-1 text-xs tabular-nums text-muted-foreground" title={t('HOME_GEAR')}>
                                        <Shield className="h-3 w-3" />{c.gear}/6 · +{c.avgReinforce}
                                    </p>
                                </li>
                            ))}
                        </ul>
                    </CardContent>
                </Card>
                <MetricChart snapshots={snapshots} />
            </div>

            <Card>
                <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
                    <CardTitle className="text-base">{t('HOME_WHAT_BOT_DID')}</CardTitle>
                    <Button variant="ghost" size="sm" asChild>
                        <Link to="/autopilot">{t('HOME_FULL_HISTORY')}<ChevronRight className="ml-1 h-4 w-4" /></Link>
                    </Button>
                </CardHeader>
                <CardContent className="max-h-80 overflow-y-auto">
                    <GrowthFeed events={events} />
                </CardContent>
            </Card>
        </div>
    );
}
