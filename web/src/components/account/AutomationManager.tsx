import { useState, useEffect } from 'react';
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from '@/components/ui/card';
import { Button } from '@/components/ui/button';
import { Switch } from '@/components/ui/switch';
import { Label } from '@/components/ui/label';
import { Input } from '@/components/ui/input';
import { Badge } from '@/components/ui/badge';
import {
    Play,
    Settings2,
    Terminal,
    Clock,
    Save,
    RefreshCcw,
    CheckCircle2
} from 'lucide-react';
import { useAccountStore } from '@/store/accountStore';
import { jobsApi } from '@/api/jobs-service';
import { AutoJobModel } from '@/api/generated/autoJobModel';
import { JobStatusDto } from '@/api/generated/jobStatusDto';
import { useJobLogs } from '@/hooks/useJobLogs';
import dayjs from 'dayjs';
import { useTranslation } from '@/hooks/useTranslation';

export function AutomationManager() {
    const { currentAccountId } = useAccountStore();
    const { t } = useTranslation();
    const [config, setConfig] = useState<AutoJobModel | null>(null);
    const [status, setStatus] = useState<JobStatusDto[]>([]);
    const [loading, setLoading] = useState(false);
    const [saving, setSaving] = useState(false);
    const { logs, isConnected, clearLogs } = useJobLogs(currentAccountId);

    const fetchData = async () => {
        if (!currentAccountId) return;
        setLoading(true);
        try {
            const [configData, statusData] = await Promise.all([
                jobsApi.getConfig(currentAccountId),
                jobsApi.getStatus(currentAccountId)
            ]);
            setConfig(configData);
            setStatus(statusData);
        } catch (error) {
            console.error('Failed to fetch automation data:', error);
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchData();
    }, [currentAccountId]);

    const handleSaveConfig = async () => {
        if (!currentAccountId || !config) return;
        setSaving(true);
        try {
            await jobsApi.updateConfig(currentAccountId, config);
            await fetchData();
        } catch (error) {
            console.error('Failed to save config:', error);
        } finally {
            setSaving(false);
        }
    };

    const handleTriggerJob = async (jobType: string) => {
        if (!currentAccountId) return;
        try {
            await jobsApi.triggerJob(currentAccountId, jobType);
        } catch (error) {
            console.error('Failed to trigger job:', error);
        }
    };

    if (!currentAccountId) return null;

    return (
        <div className="space-y-6">
            <div className="grid gap-6 md:grid-cols-2">
                <Card>
                    <CardHeader className="flex flex-row items-center justify-between space-y-0">
                        <div>
                            <CardTitle className="text-lg">{t('AUTOMATION_ACTIVE_TASKS')}</CardTitle>
                            <CardDescription>{t('AUTOMATION_ACTIVE_TASKS_DESC')}</CardDescription>
                        </div>
                        <Button variant="ghost" size="icon" onClick={fetchData} disabled={loading}>
                            <RefreshCcw className={`h-4 w-4 ${loading ? 'animate-spin' : ''}`} />
                        </Button>
                    </CardHeader>
                    <CardContent>
                        <div className="space-y-4">
                            {status.length === 0 ? (
                                <p className="text-sm text-muted-foreground text-center py-4">{t('AUTOMATION_NO_ACTIVE_TASKS')}</p>
                            ) : (
                                status.map((job) => (
                                    <div key={job.jobType} className="flex items-center justify-between p-3 border rounded-lg bg-card">
                                        <div className="space-y-1">
                                            <div className="flex items-center gap-2">
                                                <span className="font-medium text-sm">{job.description}</span>
                                                <Badge variant="outline" className="text-[10px]">{job.jobType}</Badge>
                                            </div>
                                            <div className="flex items-center gap-2 text-xs text-muted-foreground">
                                                <Clock className="h-3 w-3" />
                                                <span>{t('AUTOMATION_NEXT_RUN')}: {job.nextRunTime ? dayjs(job.nextRunTime).format('MM-DD HH:mm') : t('AUTOMATION_NOT_SCHEDULED')}</span>
                                            </div>
                                        </div>
                                        <Button size="sm" variant="outline" onClick={() => handleTriggerJob(job.jobType)}>
                                            <Play className="h-3 w-3 mr-1" />
                                            {t('AUTOMATION_EXECUTE')}
                                        </Button>
                                    </div>
                                ))
                            )}
                        </div>
                    </CardContent>
                </Card>

                <Card className="flex flex-col h-[400px]">
                    <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
                        <div className="flex items-center gap-2">
                            <Terminal className="h-4 w-4" />
                            <CardTitle className="text-sm font-medium">{t('AUTOMATION_EXECUTION_LOG')}</CardTitle>
                            {isConnected ? (
                                <Badge variant="secondary" className="bg-green-100 text-green-700 hover:bg-green-100 text-[10px]">{t('AUTOMATION_CONNECTED')}</Badge>
                            ) : (
                                <Badge variant="destructive" className="text-[10px]">{t('AUTOMATION_DISCONNECTED')}</Badge>
                            )}
                        </div>
                        <Button variant="ghost" size="sm" className="h-8 text-xs text-muted-foreground" onClick={clearLogs}>
                            {t('[EquipmentRemoveAllButton]')}
                        </Button>
                    </CardHeader>
                    <CardContent className="flex-1 overflow-auto bg-black rounded-b-lg p-3 font-mono text-[11px]">
                        <div className="space-y-1">
                            {logs.length === 0 ? (
                                <p className="text-zinc-500 italic">{t('AUTOMATION_WAITING_LOGS')}</p>
                            ) : (
                                logs.map((log, i) => (
                                    <div key={i} className="flex gap-2">
                                        <span className="text-zinc-500 shrink-0">[{log.time.split(' ')[1]}]</span>
                                        <span className={log.level === 'Error' ? 'text-red-400' : log.level === 'Warning' ? 'text-yellow-400' : 'text-zinc-300'}>
                                            {log.message}
                                        </span>
                                    </div>
                                ))
                            )}
                        </div>
                    </CardContent>
                </Card>
            </div>

            <Card>
                <CardHeader className="flex flex-row items-center justify-between">
                    <div>
                        <CardTitle className="text-lg flex items-center gap-2">
                            <Settings2 className="h-5 w-5" />
                            {t('AUTOMATION_HOSTING_SETTINGS')}
                        </CardTitle>
                        <CardDescription>{t('AUTOMATION_HOSTING_SETTINGS_DESC')}</CardDescription>
                    </div>
                    <Button onClick={handleSaveConfig} disabled={saving || !config}>
                        {saving ? <RefreshCcw className="mr-2 h-4 w-4 animate-spin" /> : <Save className="mr-2 h-4 w-4" />}
                        {t('AUTOMATION_SAVE_CONFIG')}
                    </Button>
                </CardHeader>
                <CardContent>
                    {config ? (
                        <div className="space-y-8">
                            <div className="flex items-center justify-between p-4 bg-muted/50 rounded-lg border border-dashed">
                                <div className="space-y-0.5">
                                    <Label className="text-base">{t('AUTOMATION_DISABLE_ALL')}</Label>
                                    <p className="text-sm text-muted-foreground">{t('AUTOMATION_DISABLE_ALL_DESC')}</p>
                                </div>
                                <Switch
                                    checked={config.disableAll}
                                    onCheckedChange={(checked) => setConfig({ ...config, disableAll: checked })}
                                />
                            </div>

                            <div className="grid gap-8 md:grid-cols-2">
                                <div className="space-y-4">
                                    <h3 className="font-semibold flex items-center gap-2">
                                        <Clock className="h-4 w-4 text-primary" />
                                        {t('AUTOMATION_CRON_FREQUENCY')}
                                    </h3>
                                    <div className="space-y-4">
                                        <div className="grid gap-2">
                                            <Label htmlFor="daily-cron">{t('AUTOMATION_DAILY_CRON')}</Label>
                                            <Input
                                                id="daily-cron"
                                                value={config.dailyJobCron}
                                                onChange={(e) => setConfig({ ...config, dailyJobCron: e.target.value })}
                                            />
                                        </div>
                                        <div className="grid gap-2">
                                            <Label htmlFor="hourly-cron">{t('AUTOMATION_HOURLY_CRON')}</Label>
                                            <Input
                                                id="hourly-cron"
                                                value={config.hourlyJobCron}
                                                onChange={(e) => setConfig({ ...config, hourlyJobCron: e.target.value })}
                                            />
                                        </div>
                                    </div>
                                </div>

                                <div className="space-y-4">
                                    <h3 className="font-semibold flex items-center gap-2">
                                        <CheckCircle2 className="h-4 w-4 text-primary" />
                                        {t('AUTOMATION_FEATURE_SWITCHES')}
                                    </h3>
                                    <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">
                                        <div className="flex items-center justify-between gap-4">
                                            <Label htmlFor="auto-pvp" className="text-sm">{t('AUTOMATION_AUTO_PVP')}</Label>
                                            <Switch
                                                id="auto-pvp"
                                                checked={config.autoPvp}
                                                onCheckedChange={(checked) => setConfig({ ...config, autoPvp: checked })}
                                            />
                                        </div>
                                        <div className="flex items-center justify-between gap-4">
                                            <Label htmlFor="auto-use-items" className="text-sm">{t('AUTOMATION_AUTO_USE_ITEMS')}</Label>
                                            <Switch
                                                id="auto-use-items"
                                                checked={config.autoUseItems}
                                                onCheckedChange={(checked) => setConfig({ ...config, autoUseItems: checked })}
                                            />
                                        </div>
                                        <div className="flex items-center justify-between gap-4">
                                            <Label htmlFor="auto-free-gacha" className="text-sm">{t('AUTOMATION_AUTO_FREE_GACHA')}</Label>
                                            <Switch
                                                id="auto-free-gacha"
                                                checked={config.autoFreeGacha}
                                                onCheckedChange={(checked) => setConfig({ ...config, autoFreeGacha: checked })}
                                            />
                                        </div>
                                        <div className="flex items-center justify-between gap-4">
                                            <Label htmlFor="auto-local-raid" className="text-sm">{t('AUTOMATION_AUTO_LOCAL_RAID')}</Label>
                                            <Switch
                                                id="auto-local-raid"
                                                checked={config.autoLocalRaid}
                                                onCheckedChange={(checked) => setConfig({ ...config, autoLocalRaid: checked })}
                                            />
                                        </div>
                                        <div className="flex items-center justify-between gap-4">
                                            <Label htmlFor="auto-buy-shop" className="text-sm">{t('AUTOMATION_AUTO_BUY_SHOP')}</Label>
                                            <Switch
                                                id="auto-buy-shop"
                                                checked={config.autoBuyShopItem}
                                                onCheckedChange={(checked) => setConfig({ ...config, autoBuyShopItem: checked })}
                                            />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    ) : (
                        <div className="text-center py-8">
                            <RefreshCcw className="h-8 w-8 animate-spin mx-auto mb-2 text-muted-foreground" />
                            <p className="text-sm text-muted-foreground">{t('AUTOMATION_LOADING_CONFIG')}</p>
                        </div>
                    )}
                </CardContent>
            </Card>
        </div>
    );
}
