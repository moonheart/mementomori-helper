import { useState, useEffect, useCallback } from 'react';
import { Tabs, TabsContent, TabsList, TabsTrigger } from '@/components/ui/tabs';
import { Card, CardHeader, CardTitle, CardDescription, CardContent } from '@/components/ui/card';
import { Button } from '@/components/ui/button';
import { Badge } from '@/components/ui/badge';
import {
    Settings,
    Swords,
    ShoppingBag,
    Users,
    User,
    Loader2,
    RefreshCcw,
    Play,
    Clock
} from 'lucide-react';
import { useAccountStore } from '@/store/accountStore';
import { settingsApi } from '@/api/settings-service';
import { jobsApi } from '@/api/jobs-service';
import { useToast } from '@/hooks/use-toast';
import dayjs from 'dayjs';
import { useTranslation } from '@/hooks/useTranslation';

import { AutoJobModel } from '@/api/generated/autoJobModel';
import { PvpOption } from '@/api/generated/pvpOption';
import { DungeonBattleConfig } from '@/api/generated/dungeonBattleConfig';
import { FriendManageOption } from '@/api/generated/friendManageOption';
import { LocalRaidConfig } from '@/api/generated/localRaidConfig';
import { BountyQuestOption } from '@/api/generated/bountyQuestOption';
import { BountyQuestAutoConfig } from '@/api/generated/bountyQuestAutoConfig';
import { ShopConfig } from '@/api/generated/shopConfig';
import { GachaConfigModel } from '@/api/generated/gachaConfigModel';
import { ItemsConfig } from '@/api/generated/itemsConfig';
import { GuildTowerOption } from '@/api/generated/guildTowerOption';
import { JobStatusDto } from '@/api/generated/jobStatusDto';

// 导入子模块组件
import { AutomationSection } from '@/components/settings/AutomationSection';
import { BattleSection } from '@/components/settings/BattleSection';
import { SocialSection } from '@/components/settings/SocialSection';
import { ResourceSection } from '@/components/settings/ResourceSection';
import { MiscSection } from '@/components/settings/MiscSection';

export function AutomationPage() {
    const { currentAccountId, accounts } = useAccountStore();
    const { toast } = useToast();
    const { t } = useTranslation();
    const [loading, setLoading] = useState(false);
    const [activeAccountName, setActiveAccountName] = useState('');
    const [status, setStatus] = useState<JobStatusDto[]>([]);

    // 各模块配置状态
    const [autoJob, setAutoJob] = useState<AutoJobModel>(() => ({ ...new AutoJobModel() }));
    const [battleLeague, setBattleLeague] = useState<PvpOption>(() => ({ ...new PvpOption() }));
    const [legendLeague, setLegendLeague] = useState<PvpOption>(() => ({ ...new PvpOption() }));
    const [dungeonBattle, setDungeonBattle] = useState<DungeonBattleConfig>(() => ({ ...new DungeonBattleConfig() }));
    const [friendManage, setFriendManage] = useState<FriendManageOption>(() => ({ ...new FriendManageOption() }));
    const [localRaid, setLocalRaid] = useState<LocalRaidConfig>(() => ({ ...new LocalRaidConfig() }));
    const [bountyQuest, setBountyQuest] = useState<BountyQuestOption>(() => ({ ...new BountyQuestOption() }));
    const [bountyQuestAuto, setBountyQuestAuto] = useState<BountyQuestAutoConfig>(() => ({ ...new BountyQuestAutoConfig() }));
    const [shop, setShop] = useState<ShopConfig>(() => ({ ...new ShopConfig() }));
    const [gacha, setGacha] = useState<GachaConfigModel>(() => ({ ...new GachaConfigModel() }));
    const [items, setItems] = useState<ItemsConfig>(() => ({ ...new ItemsConfig() }));
    const [guildTower, setGuildTower] = useState<GuildTowerOption>(() => ({ ...new GuildTowerOption() }));

    // 加载配置数据
    const loadSettings = useCallback(async (userId: number) => {
        setLoading(true);
        try {
            const [
                autoJobData,
                battleLeagueData,
                legendLeagueData,
                dungeonBattleData,
                friendManageData,
                localRaidData,
                bountyQuestData,
                bountyQuestAutoData,
                shopData,
                gachaData,
                itemsData,
                guildTowerData,
                statusData
            ] = await Promise.all([
                settingsApi.getSetting<AutoJobModel>(userId, 'autojob'),
                settingsApi.getSetting<PvpOption>(userId, 'battleleague'),
                settingsApi.getSetting<PvpOption>(userId, 'legendleague'),
                settingsApi.getSetting<DungeonBattleConfig>(userId, 'dungeonbattle'),
                settingsApi.getSetting<FriendManageOption>(userId, 'friendmanage'),
                settingsApi.getSetting<LocalRaidConfig>(userId, 'localraid'),
                settingsApi.getSetting<BountyQuestOption>(userId, 'bountyquest'),
                settingsApi.getSetting<BountyQuestAutoConfig>(userId, 'bountyquestauto'),
                settingsApi.getSetting<ShopConfig>(userId, 'shop'),
                settingsApi.getSetting<GachaConfigModel>(userId, 'gachaconfig'),
                settingsApi.getSetting<ItemsConfig>(userId, 'items'),
                settingsApi.getSetting<GuildTowerOption>(userId, 'guildtower'),
                jobsApi.getStatus(userId)
            ]);

            if (autoJobData) setAutoJob(autoJobData);
            if (battleLeagueData) setBattleLeague(battleLeagueData);
            if (legendLeagueData) setLegendLeague(legendLeagueData);
            if (dungeonBattleData) setDungeonBattle(dungeonBattleData);
            if (friendManageData) setFriendManage(friendManageData);
            if (localRaidData) setLocalRaid(localRaidData);
            if (bountyQuestData) setBountyQuest(bountyQuestData);
            if (bountyQuestAutoData) setBountyQuestAuto(bountyQuestAutoData);
            if (shopData) setShop(shopData);
            if (gachaData) setGacha(gachaData);
            if (itemsData) setItems(itemsData);
            if (guildTowerData) setGuildTower(guildTowerData);
            setStatus(statusData);

            const account = accounts.find(a => a.userId === userId);
            setActiveAccountName(account?.name || t('SETTINGS_ACCOUNT_FALLBACK', [userId]));
        } catch (error) {
            console.error('Failed to load settings:', error);
            toast({
                variant: 'destructive',
                title: t('SETTINGS_LOAD_FAILED_TITLE'),
                description: t('SETTINGS_LOAD_FAILED_DESC')
            });
        } finally {
            setLoading(false);
        }
    }, [accounts, toast]);

    useEffect(() => {
        if (currentAccountId) {
            loadSettings(currentAccountId);
        }
    }, [currentAccountId, loadSettings]);

    // 定时刷新任务状态 (每30秒)
    useEffect(() => {
        if (!currentAccountId) return;

        const refreshStatus = async () => {
            try {
                const statusData = await jobsApi.getStatus(currentAccountId);
                setStatus(statusData);
            } catch (error) {
                console.error('Failed to refresh status:', error);
            }
        };

        const interval = setInterval(refreshStatus, 30000);
        return () => clearInterval(interval);
    }, [currentAccountId]);

    // 刷新任务状态
    const refreshJobStatus = async () => {
        if (!currentAccountId) return;
        try {
            const statusData = await jobsApi.getStatus(currentAccountId);
            setStatus(statusData);
        } catch (error) {
            console.error('Failed to refresh job status:', error);
        }
    };

    // 保存配置处理
    const handleSave = async (key: string, value: unknown) => {
        if (!currentAccountId) return;
        try {
            await settingsApi.saveSetting(currentAccountId, key, value);

            // 如果是 autojob 配置，刷新任务状态
            if (key === 'autojob') {
                await refreshJobStatus();
            }

            toast({
                title: t('SETTINGS_SAVE_SUCCESS_TITLE'),
                description: t('SETTINGS_SAVE_SUCCESS_DESC', [key]),
            });
        } catch (error) {
            console.error(`Failed to save setting ${key}:`, error);
            toast({
                variant: 'destructive',
                title: t('SETTINGS_SAVE_FAILED_TITLE'),
                description: t('SETTINGS_SAVE_FAILED_DESC', [key])
            });
        }
    };

    const handleTriggerJob = async (jobType: string) => {
        if (!currentAccountId) return;
        try {
            await jobsApi.triggerJob(currentAccountId, jobType);
            toast({
                title: t('AUTOMATION_TASK_TRIGGERED'),
                description: t('AUTOMATION_TASK_TRIGGERED_DESC', [jobType])
            });
        } catch (error) {
            console.error('Failed to trigger job:', error);
            toast({
                variant: 'destructive',
                title: t('AUTOMATION_TRIGGER_FAILED'),
                description: t('AUTOMATION_TRIGGER_FAILED_DESC')
            });
        }
    };

    if (!currentAccountId) {
        return (
            <div className="flex h-[450px] items-center justify-center">
                <Card className="w-[420px]">
                    <CardHeader className="text-center">
                        <CardTitle>{t('SETTINGS_NO_ACCOUNT_TITLE')}</CardTitle>
                        <CardDescription>{t('SETTINGS_NO_ACCOUNT_DESC')}</CardDescription>
                    </CardHeader>
                </Card>
            </div>
        );
    }

    return (
        <div className="space-y-6 pb-12">
            <div className="flex justify-end items-center">
                <p className="text-muted-foreground mr-4">
                    {t('SETTINGS_CONFIGURING_ACCOUNT')}: <span className="text-primary font-medium">{activeAccountName}</span>
                </p>
                {loading && <Loader2 className="h-6 w-6 animate-spin text-muted-foreground" />}
            </div>

            <Card>
                <CardHeader className="flex flex-row items-center justify-between space-y-0">
                    <div>
                        <CardTitle className="text-lg">{t('AUTOMATION_ACTIVE_TASKS')}</CardTitle>
                        <CardDescription>{t('AUTOMATION_ACTIVE_TASKS_DESC')}</CardDescription>
                    </div>
                    <Button variant="ghost" size="icon" onClick={refreshJobStatus} disabled={loading}>
                        <RefreshCcw className={`h-4 w-4 ${loading ? 'animate-spin' : ''}`} />
                    </Button>
                </CardHeader>
                <CardContent>
                    {status.length === 0 ? (
                        <p className="text-sm text-muted-foreground text-center py-4">{t('AUTOMATION_NO_ACTIVE_TASKS')}</p>
                    ) : (
                        <div className="grid gap-4 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4">
                            {status.map((job) => (
                                <div key={job.jobType} className="flex flex-col p-3 border rounded-lg bg-card">
                                    <div className="flex items-start justify-between gap-2">
                                        <div className="flex items-center gap-2 min-w-0">
                                            <span className="font-medium text-sm truncate">{job.description}</span>
                                        </div>
                                        <Badge variant="outline" className="text-[10px] shrink-0">{job.jobType}</Badge>
                                    </div>
                                    <div className="flex items-center gap-2 text-xs text-muted-foreground mt-2">
                                        <Clock className="h-3 w-3 shrink-0" />
                                        <span className="truncate">{job.nextRunTime ? dayjs(job.nextRunTime).format('MM-DD HH:mm') : t('AUTOMATION_NOT_SCHEDULED')}</span>
                                    </div>
                                    <Button size="sm" variant="outline" className="mt-3 w-full" onClick={() => handleTriggerJob(job.jobType)}>
                                        <Play className="h-3 w-3 mr-1" />
                                        {t('AUTOMATION_EXECUTE')}
                                    </Button>
                                </div>
                            ))}
                        </div>
                    )}
                </CardContent>
            </Card>

            <Tabs defaultValue="automation" className="w-full">
                <TabsList className="grid w-full max-w-3xl grid-cols-5">
                    <TabsTrigger value="automation">
                        <Settings className="mr-2 h-4 w-4" />
                        {t('SETTINGS_TAB_AUTOMATION')}
                    </TabsTrigger>
                    <TabsTrigger value="battle">
                        <Swords className="mr-2 h-4 w-4" />
                        {t('SETTINGS_TAB_BATTLE')}
                    </TabsTrigger>
                    <TabsTrigger value="resources">
                        <ShoppingBag className="mr-2 h-4 w-4" />
                        {t('SETTINGS_TAB_RESOURCES')}
                    </TabsTrigger>
                    <TabsTrigger value="social">
                        <Users className="mr-2 h-4 w-4" />
                        {t('SETTINGS_TAB_SOCIAL')}
                    </TabsTrigger>
                    <TabsTrigger value="account">
                        <User className="mr-2 h-4 w-4" />
                        {t('SETTINGS_TAB_ACCOUNT')}
                    </TabsTrigger>
                </TabsList>

                <div className="mt-6">
                    <TabsContent value="automation">
                        <AutomationSection
                            config={autoJob}
                            onChange={(cfg) => { setAutoJob(cfg); handleSave('autojob', cfg); }}
                            onResetCron={(key) => {
                                const defaultModel = new AutoJobModel();
                                const newCfg = { ...autoJob, [key]: defaultModel[key as keyof AutoJobModel] };
                                setAutoJob(newCfg as AutoJobModel);
                                handleSave('autojob', newCfg);
                            }}
                        />
                    </TabsContent>

                    <TabsContent value="battle">
                        <BattleSection
                            battleLeague={battleLeague}
                            legendLeague={legendLeague}
                            dungeonBattle={dungeonBattle}
                            onUpdatePvp={(key, cfg) => {
                                if (key === 'battleLeague') {
                                    setBattleLeague(cfg);
                                    handleSave('battleleague', cfg);
                                } else {
                                    setLegendLeague(cfg);
                                    handleSave('legendleague', cfg);
                                }
                            }}
                            onUpdateDungeon={(cfg) => {
                                setDungeonBattle(cfg);
                                handleSave('dungeonbattle', cfg);
                            }}
                        />
                    </TabsContent>

                    <TabsContent value="resources">
                        <ResourceSection
                            shop={shop}
                            gacha={gacha}
                            items={items}
                            onUpdateShop={(cfg) => { setShop(cfg); handleSave('shop', cfg); }}
                            onUpdateGacha={(cfg) => { setGacha(cfg); handleSave('gachaconfig', cfg); }}
                            onUpdateItems={(cfg) => { setItems(cfg); handleSave('items', cfg); }}
                        />
                    </TabsContent>

                    <TabsContent value="social">
                        <SocialSection
                            friendManage={friendManage}
                            localRaid={localRaid}
                            bountyQuest={bountyQuest}
                            bountyQuestAuto={bountyQuestAuto}
                            onUpdateFriends={(cfg) => { setFriendManage(cfg); handleSave('friendmanage', cfg); }}
                            onUpdateLocalRaid={(cfg) => { setLocalRaid(cfg); handleSave('localraid', cfg); }}
                            onUpdateBounty={(cfg) => {
                                setBountyQuest(cfg.option);
                                handleSave('bountyquest', cfg.option);
                                setBountyQuestAuto(cfg.auto);
                                handleSave('bountyquestauto', cfg.auto);
                            }}
                        />
                    </TabsContent>

                    <TabsContent value="account">
                        <MiscSection
                            guildTower={guildTower}
                            onUpdateGuildTower={(cfg) => { setGuildTower(cfg); handleSave('guildtower', cfg); }}
                        />
                    </TabsContent>
                </div>
            </Tabs>
        </div>
    );
}

export default AutomationPage;
