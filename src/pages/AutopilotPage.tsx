import { useCallback, useEffect, useState } from 'react';
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from '@/components/ui/card';
import { Button } from '@/components/ui/button';
import { Switch } from '@/components/ui/switch';
import { Label } from '@/components/ui/label';
import { Input } from '@/components/ui/input';
import { Separator } from '@/components/ui/separator';
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from '@/components/ui/select';
import { Loader2, Play, RefreshCcw, Save } from 'lucide-react';
import { useAccountStore } from '@/store/accountStore';
import { useToast } from '@/hooks/use-toast';
import { useTranslation } from '@/hooks/useTranslation';
import { autopilotApi, type AutopilotConfig, type AutopilotStep, type GrowthEvent, type ProgressSnapshot } from '@/api/autopilot-service';
import { GrowthFeed } from '@/components/autopilot/GrowthFeed';
import { ProgressChart } from '@/components/autopilot/ProgressChart';
import { SchedulePicker } from '@/components/autopilot/SchedulePicker';

// 抽卡和组队会花钻石/改队伍, 界面上标出来
const RISKY_STEPS = new Set(['AutoPullGacha', 'AutoBuildTeam', 'SmartRankUp']);

export function AutopilotPage() {
    const { t } = useTranslation();
    const { toast } = useToast();
    const userId = useAccountStore((s) => s.currentAccountId);
    const [steps, setSteps] = useState<AutopilotStep[]>([]);
    const [config, setConfig] = useState<AutopilotConfig | null>(null);
    const [events, setEvents] = useState<GrowthEvent[]>([]);
    const [snapshots, setSnapshots] = useState<ProgressSnapshot[]>([]);
    const [saving, setSaving] = useState(false);
    const [dirty, setDirty] = useState(false);

    const loadHistory = useCallback(async () => {
        if (!userId) return;
        const [e, s] = await Promise.all([autopilotApi.getEvents(userId), autopilotApi.getSnapshots(userId)]);
        setEvents(e);
        setSnapshots(s);
    }, [userId]);

    useEffect(() => {
        if (!userId) return;
        autopilotApi.getSteps().then(setSteps);
        autopilotApi.getConfig(userId).then((c) => { setConfig(c); setDirty(false); });
        loadHistory();
    }, [userId, loadHistory]);

    // 跑起来以后成长记录会陆续出现, 每 15 秒刷新一次
    useEffect(() => {
        const id = setInterval(loadHistory, 15000);
        return () => clearInterval(id);
    }, [loadHistory]);

    const update = (patch: Partial<AutopilotConfig>) => {
        setConfig((c) => (c ? { ...c, ...patch } : c));
        setDirty(true);
    };

    const toggleStep = (key: string, on: boolean) => {
        if (!config) return;
        update({ steps: on ? [...config.steps, key] : config.steps.filter((s) => s !== key) });
    };

    const save = async () => {
        if (!userId || !config) return;
        setSaving(true);
        try {
            await autopilotApi.saveConfig(userId, config);
            setDirty(false);
            toast({ title: t('AP_SAVED') });
        } catch (e) {
            toast({ title: t('AP_SAVE_FAILED'), description: String(e), variant: 'destructive' });
        } finally {
            setSaving(false);
        }
    };

    const run = async (step?: string) => {
        if (!userId) return;
        try {
            await autopilotApi.run(userId, step);
            toast({ title: t('AP_STARTED'), description: t('AP_STARTED_DESC') });
        } catch (e) {
            toast({ title: t('AP_RUN_FAILED'), description: String(e), variant: 'destructive' });
        }
    };

    if (!config) {
        return <div className="flex justify-center p-12"><Loader2 className="h-6 w-6 animate-spin text-muted-foreground" /></div>;
    }

    return (
        <div className="space-y-6">
            <Card>
                <CardHeader className="flex flex-row items-start justify-between gap-4 space-y-0">
                    <div className="space-y-1">
                        <CardTitle>{t('AP_TITLE')}</CardTitle>
                        <CardDescription>{t('AP_DESC')}</CardDescription>
                    </div>
                    <div className="flex shrink-0 gap-2">
                        <Button variant="outline" onClick={() => run()}>
                            <Play className="mr-2 h-4 w-4" />{t('AP_RUN_NOW')}
                        </Button>
                        <Button onClick={save} disabled={!dirty || saving}>
                            {saving ? <Loader2 className="mr-2 h-4 w-4 animate-spin" /> : <Save className="mr-2 h-4 w-4" />}
                            {t('AP_SAVE')}
                        </Button>
                    </div>
                </CardHeader>
                <CardContent className="space-y-6">
                    <div className="flex flex-wrap items-center gap-x-8 gap-y-4">
                        <div className="flex items-center gap-3">
                            <Switch id="ap-enabled" checked={config.enabled} onCheckedChange={(v) => update({ enabled: v })} />
                            <Label htmlFor="ap-enabled">{t('AP_ENABLED')}</Label>
                        </div>
                        <div className="flex items-center gap-3">
                            <span className="text-sm text-muted-foreground">{t('AP_SCHEDULE')}</span>
                            <SchedulePicker value={config.cron} onChange={(cron) => update({ cron })} />
                        </div>
                    </div>

                    <Separator />

                    <div className="space-y-3">
                        <h4 className="text-sm font-semibold text-muted-foreground">{t('AP_STEPS')}</h4>
                        <ol className="grid gap-2 sm:grid-cols-2 xl:grid-cols-3">
                            {steps.map((s, i) => (
                                <li key={s.key} className="flex items-center gap-3 rounded-lg border p-2 pl-3">
                                    <span className="w-5 text-xs tabular-nums text-muted-foreground">{i + 1}</span>
                                    <Switch
                                        id={`step-${s.key}`}
                                        checked={config.steps.includes(s.key)}
                                        onCheckedChange={(v) => toggleStep(s.key, v)}
                                    />
                                    <Label htmlFor={`step-${s.key}`} className="flex-1 leading-tight">
                                        {t(`AP_STEP_${s.key}`)}
                                        {RISKY_STEPS.has(s.key) && <span className="block text-xs font-normal text-amber-600 dark:text-amber-400">{t(`AP_STEP_${s.key}_NOTE`)}</span>}
                                    </Label>
                                    <Button variant="ghost" size="icon" className="h-8 w-8" onClick={() => run(s.key)} aria-label={`${t('AP_RUN_STEP')}: ${t(`AP_STEP_${s.key}`)}`}>
                                        <Play className="h-4 w-4" />
                                    </Button>
                                </li>
                            ))}
                        </ol>
                    </div>

                    <Separator />

                    <div className="grid gap-4 sm:grid-cols-3">
                        <div className="space-y-2">
                            <Label htmlFor="ap-reserve">{t('AP_DIAMOND_RESERVE')}</Label>
                            <Input
                                id="ap-reserve"
                                type="number"
                                min={0}
                                step={500}
                                value={config.gachaDiamondReserve}
                                onChange={(e) => update({ gachaDiamondReserve: Math.max(0, Number(e.target.value)) })}
                            />
                            <p className="text-xs text-muted-foreground">{t('AP_DIAMOND_RESERVE_HINT')}</p>
                        </div>
                        <div className="space-y-2">
                            <Label>{t('AP_MIN_PICKUP_TIER')}</Label>
                            <Select value={String(config.gachaMinPickupTier)} onValueChange={(v) => update({ gachaMinPickupTier: Number(v) })}>
                                <SelectTrigger className="w-full"><SelectValue /></SelectTrigger>
                                <SelectContent>
                                    <SelectItem value="5">SS</SelectItem>
                                    <SelectItem value="4">S</SelectItem>
                                    <SelectItem value="3">A</SelectItem>
                                </SelectContent>
                            </Select>
                        </div>
                        <div className="flex items-start gap-3 pt-7">
                            <Switch id="ap-fodder" checked={config.rankUpWithFodder} onCheckedChange={(v) => update({ rankUpWithFodder: v })} />
                            <Label htmlFor="ap-fodder" className="leading-tight">
                                {t('AP_RANKUP_FODDER')}
                                <span className="block text-xs font-normal text-muted-foreground">{t('AP_RANKUP_FODDER_HINT')}</span>
                            </Label>
                        </div>
                    </div>
                </CardContent>
            </Card>

            <div className="grid gap-6 lg:grid-cols-5">
                <Card className="lg:col-span-2">
                    <CardHeader>
                        <CardTitle className="text-base">{t('AP_PROGRESS')}</CardTitle>
                        <CardDescription>{t('AP_PROGRESS_DESC')}</CardDescription>
                    </CardHeader>
                    <CardContent><ProgressChart snapshots={snapshots} /></CardContent>
                </Card>
                <Card className="lg:col-span-3">
                    <CardHeader className="flex flex-row items-center justify-between space-y-0">
                        <div className="space-y-1">
                            <CardTitle className="text-base">{t('AP_HISTORY')}</CardTitle>
                            <CardDescription>{t('AP_HISTORY_DESC')}</CardDescription>
                        </div>
                        <Button variant="ghost" size="icon" onClick={loadHistory} aria-label={t('AP_REFRESH')}>
                            <RefreshCcw className="h-4 w-4" />
                        </Button>
                    </CardHeader>
                    <CardContent className="max-h-[480px] overflow-y-auto">
                        <GrowthFeed events={events} />
                    </CardContent>
                </Card>
            </div>
        </div>
    );
}
