import React, { useState, useEffect, useCallback } from 'react';
import { Tabs, TabsContent, TabsList, TabsTrigger } from '@/components/ui/tabs';
import { Card, CardHeader, CardTitle, CardDescription } from '@/components/ui/card';
import { User, Settings, Swords, Users, ShoppingBag, Loader2 } from 'lucide-react';
import { useAccountStore } from '@/store/accountStore';
import { settingsApi } from '@/api/settings-service';
import { useToast } from '@/hooks/use-toast';
import { useTranslation } from '@/hooks/useTranslation';

// 导入生成的配置类型
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

// 导入子模块组件
import { AutomationSection } from '@/components/settings/AutomationSection';
import { BattleSection } from '@/components/settings/BattleSection';
import { SocialSection } from '@/components/settings/SocialSection';
import { ResourceSection } from '@/components/settings/ResourceSection';
import { MiscSection } from '@/components/settings/MiscSection';

export function SettingsPage() {
    const { currentAccountId, accounts } = useAccountStore();
    const { toast } = useToast();
    const { t } = useTranslation();
    const [loading, setLoading] = useState(false);
    const [activeAccountName, setActiveAccountName] = useState('');

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
                guildTowerData
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
                settingsApi.getSetting<GuildTowerOption>(userId, 'guildtower')
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
    }, [accounts, t, toast]);

    useEffect(() => {
        if (currentAccountId) {
            loadSettings(currentAccountId);
        }
    }, [currentAccountId, loadSettings]);

    // 保存配置处理
    const handleSave = async (key: string, value: unknown) => {
        if (!currentAccountId) return;
        try {
            await settingsApi.saveSetting(currentAccountId, key, value);
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
                <p className="text-muted-foreground mr-4">{t('SETTINGS_CONFIGURING_ACCOUNT')}: <span className="text-primary font-medium">{activeAccountName}</span></p>
                {loading && <Loader2 className="h-6 w-6 animate-spin text-muted-foreground" />}
            </div>

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
