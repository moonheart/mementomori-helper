import React from 'react';
import { Card, CardContent, CardHeader, CardTitle, CardDescription } from '@/components/ui/card';
import { Switch } from '@/components/ui/switch';
import { Label } from '@/components/ui/label';
import { Button } from '@/components/ui/button';
import { Tabs, TabsContent, TabsList, TabsTrigger } from '@/components/ui/tabs';
import { Separator } from '@/components/ui/separator';
import { RefreshCw, HelpCircle } from 'lucide-react';
import { CronPicker } from './CronPicker';
import { AutoJobModel } from '@/api/generated/autoJobModel';
import { useLocalizationStore } from '@/store/localization-store';

interface AutomationSectionProps {
    config: AutoJobModel;
    onChange: (config: AutoJobModel) => void;
    onResetCron: (key: keyof AutoJobModel) => void;
}


// 简单开关配置（没有 Cron）
const simpleSwitches = [
    { key: 'disableAll', labelKey: 'AUTO_SIMPLE_DISABLE_ALL' },
    { key: 'autoDungeonBattle', labelKey: 'AUTO_SIMPLE_DUNGEON' },
    { key: 'autoUseItems', labelKey: 'AUTO_SIMPLE_USE_ITEMS' },
    { key: 'autoFreeGacha', labelKey: 'AUTO_SIMPLE_FREE_GACHA' },
    { key: 'autoRankUpCharacter', labelKey: 'AUTO_SIMPLE_RANK_UP_CHARACTER' },
] as const;

// 带 Cron 的配置项
const cronItems = [
    { switchKey: 'autoPvp', cronKey: 'pvpJobCron', labelKey: 'AUTO_CRON_PVP' },
    { switchKey: 'autoLegendLeague', cronKey: 'legendLeagueJobCron', labelKey: 'AUTO_CRON_LEGEND_LEAGUE' },
    { switchKey: 'autoLocalRaid', cronKey: 'autoLocalRaidJobCron', labelKey: 'AUTO_CRON_LOCAL_RAID' },
    { switchKey: 'autoBuyShopItem', cronKey: 'autoBuyShopItemJobCron', labelKey: 'AUTO_CRON_BUY_SHOP_ITEM' },
    { switchKey: 'autoChangeGachaRelic', cronKey: 'autoChangeGachaRelicJobCron', labelKey: 'AUTO_CRON_CHANGE_GACHA_RELIC' },
    { switchKey: 'autoDrawGachaRelic', cronKey: 'autoDrawGachaRelicJobCron', labelKey: 'AUTO_CRON_DRAW_GACHA_RELIC' },
    { switchKey: 'autoOpenGuildRaid', cronKey: 'guildRaidBossReleaseCron', labelKey: 'AUTO_CRON_OPEN_GUILD_RAID' },
] as const;

// 独立的 Cron 配置（没有对应的开关）
const standaloneCrons = [
    { key: 'dailyJobCron', labelKey: 'AUTO_STANDALONE_DAILY' },
    { key: 'hourlyJobCron', labelKey: 'AUTO_STANDALONE_HOURLY' },
] as const;

export function AutomationSection({ config, onChange, onResetCron }: AutomationSectionProps) {
    const { t } = useLocalizationStore();

    const handleSwitchChange = (key: keyof AutoJobModel, checked: boolean) => {
        onChange({ ...config, [key]: checked });
    };

    const handleCronChange = (key: keyof AutoJobModel, value: string) => {
        onChange({ ...config, [key]: value });
    };

    const handleActionToggle = (listKey: 'disabledDailyActions' | 'disabledHourlyActions', actionKey: string, enabled: boolean) => {
        const currentList = config[listKey] || [];
        let newList: string[];

        if (enabled) {
            newList = currentList.filter((k) => k !== actionKey);
        } else {
            newList = [...currentList, actionKey];
        }

        onChange({ ...config, [listKey]: newList });
    };

    const renderActionList = (listKey: 'disabledDailyActions' | 'disabledHourlyActions') => {
        const disabledList = config[listKey] || [];
        const isDaily = listKey === 'disabledDailyActions';
        const defaultActions = isDaily
            ? ['DailyLoginBonus', 'VipDailyGift', 'MonthlyBoost', 'AutoBattleReward', 'FriendPointTransfer', 'PresentReceive', 'EquipmentReinforcement', 'BossQuickBattle', 'InfiniteTower', 'BossHighSpeedBattle', 'GvgReward', 'GuildCheckin', 'GuildRaidBattle', 'GuildTower', 'FriendManage', 'AchievementReward', 'BountyQuestReward', 'BountyQuestDispatch', 'DungeonBattle', 'MissionReward', 'AutoUseItems', 'FreeGacha', 'CharacterRankUp']
            : ['DailyLoginBonus', 'BountyQuestDispatch', 'PresentReceive', 'AutoBattleReward', 'GuildRaidBattle', 'GuildTower', 'GvgReward', 'FriendPointTransfer', 'BountyQuestReward', 'MissionReward', 'FreeGacha', 'AutoUseItems', 'BookSortAuto'];

        return (
            <div className="space-y-4">
                <div className="grid gap-2 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 xl:grid-cols-6">
                    {defaultActions.map((actionKey) => (
                        <div key={actionKey} className="flex items-center justify-between space-x-2 rounded-lg border p-2">
                            <Label htmlFor={`${listKey}-${actionKey}`} className="flex flex-col space-y-1">
                                <span className="text-sm">{t(`ACTION_${actionKey}`)}</span>
                            </Label>
                            <Switch
                                id={`${listKey}-${actionKey}`}
                                checked={!disabledList.includes(actionKey)}
                                onCheckedChange={(checked) => handleActionToggle(listKey, actionKey, checked)}
                            />
                        </div>
                    ))}
                </div>
            </div>
        );
    };

    return (
        <div className="space-y-6">
            <Card>
                <CardHeader className="flex flex-row items-center justify-between space-y-0">
                    <div>
                        <CardTitle>{t('AUTO_CONFIG_TITLE')}</CardTitle>
                        <CardDescription>{t('AUTO_CONFIG_DESC')}</CardDescription>
                    </div>
                    <Button variant="ghost" size="sm" asChild>
                        <a href="https://www.freeformatter.com/cron-expression-generator-quartz.html" target="_blank" rel="noreferrer">
                            <HelpCircle className="mr-2 h-4 w-4" />
                            {t('AUTO_CRON_HELP')}
                        </a>
                    </Button>
                </CardHeader>
                <CardContent className="space-y-6">
                    <div className="space-y-4">
                        <h4 className="text-sm font-semibold text-muted-foreground">{t('AUTO_BASIC_SWITCHES')}</h4>
                        <div className="grid gap-4 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4">
                            {simpleSwitches.map(({ key, labelKey }) => (
                                <div key={key} className="flex items-center justify-between space-x-2 rounded-lg border p-2">
                                    <Label htmlFor={key} className="flex flex-col space-y-1">
                                        <span>{t(labelKey)}</span>
                                    </Label>
                                    <Switch
                                        id={key}
                                        checked={config[key] as boolean}
                                        onCheckedChange={(checked) => handleSwitchChange(key, checked)}
                                    />
                                </div>
                            ))}
                        </div>
                    </div>

                    <Separator />

                    <div className="space-y-4">
                        <h4 className="text-sm font-semibold text-muted-foreground">{t('AUTO_CRON_TASKS')}</h4>
                        <div className="grid gap-4 sm:grid-cols-1 lg:grid-cols-2">
                            {cronItems.map(({ switchKey, cronKey, labelKey }) => (
                                <div key={switchKey} className="space-y-2 rounded-lg border p-3">
                                    <div className="flex items-center justify-between">
                                        <Label htmlFor={switchKey} className="font-medium">{t(labelKey)}</Label>
                                        <Switch
                                            id={switchKey}
                                            checked={config[switchKey] as boolean}
                                            onCheckedChange={(checked) => handleSwitchChange(switchKey, checked)}
                                        />
                                    </div>
                                    <div className="flex items-center gap-2">
                                        <div className="flex-1">
                                            <CronPicker value={config[cronKey] as string} onChange={(v) => handleCronChange(cronKey, v)} />
                                        </div>
                                        <Button
                                            variant="outline"
                                            size="icon"
                                            onClick={() => onResetCron(cronKey)}
                                            title={t('AUTO_RESET_DEFAULT')}
                                        >
                                            <RefreshCw className="h-4 w-4" />
                                        </Button>
                                    </div>
                                </div>
                            ))}
                        </div>
                    </div>

                    <Separator />

                    <div className="space-y-4">
                        <h4 className="text-sm font-semibold text-muted-foreground">{t('AUTO_OTHER_SCHEDULES')}</h4>
                        <div className="grid gap-x-6 gap-y-4 lg:grid-cols-2">
                            {standaloneCrons.map(({ key, labelKey }) => (
                                <div key={key} className="space-y-2">
                                    <Label htmlFor={key} className="text-xs">{t(labelKey)}</Label>
                                    <div className="flex items-start gap-2">
                                        <div className="flex-1">
                                            <CronPicker value={config[key] as string} onChange={(v) => handleCronChange(key, v)} />
                                        </div>
                                        <Button
                                            variant="outline"
                                            size="icon"
                                            onClick={() => onResetCron(key)}
                                            title={t('AUTO_RESET_DEFAULT')}
                                        >
                                            <RefreshCw className="h-4 w-4" />
                                        </Button>
                                    </div>
                                </div>
                            ))}
                        </div>
                    </div>
                </CardContent>
            </Card>

            <Card>
                <CardHeader className="flex flex-row items-center justify-between space-y-0">
                    <div>
                        <CardTitle>{t('AUTO_TASK_ACTIONS_TITLE')}</CardTitle>
                        <CardDescription>{t('AUTO_TASK_ACTIONS_DESC')}</CardDescription>
                    </div>
                </CardHeader>
                <CardContent>
                    <Tabs defaultValue="daily" className="w-full">
                        <TabsList className="grid w-full grid-cols-2">
                            <TabsTrigger value="daily">{t('AUTO_DAILY_ACTIONS_TITLE')}</TabsTrigger>
                            <TabsTrigger value="hourly">{t('AUTO_HOURLY_ACTIONS_TITLE')}</TabsTrigger>
                        </TabsList>
                        <div className="mt-4">
                            <TabsContent value="daily" className="space-y-4">
                                {renderActionList('disabledDailyActions')}
                            </TabsContent>
                            <TabsContent value="hourly" className="space-y-4">
                                {renderActionList('disabledHourlyActions')}
                            </TabsContent>
                        </div>
                    </Tabs>
                </CardContent>
            </Card>
        </div>
    );
}
