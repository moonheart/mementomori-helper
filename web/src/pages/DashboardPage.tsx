import { useState, useEffect } from 'react';
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from '@/components/ui/card';
import { Button } from '@/components/ui/button';
import { Badge } from '@/components/ui/badge';
import { Progress } from '@/components/ui/progress';
import { CheckCircle2, Circle, Zap, Trophy, Users, Swords, MapPin, Gift, Diamond, Coins, Heart, FlaskConical, Package, Loader2, ScrollText, MessageCircle } from 'lucide-react';
import { useNavigate } from 'react-router-dom';
import { ortegaApi } from '@/api/ortega-client';
import { UserSyncData } from '@/api/generated/userSyncData';
import { UserGetUserDataResponse } from '@/api/generated/UsergetUserDataResponse';
import { UserGetMypageResponse } from '@/api/generated/UsergetMypageResponse';
import { useLocalizationStore } from '@/store/localization-store';
import { MissionGroupType } from '@/api/generated/missionGroupType';
import { ItemType } from '@/api/generated/itemType';
import { TowerType } from '@/api/generated/towerType';
import { LanguageType } from '@/api/generated/languageType';
import { TransferSpotType } from '@/api/generated/transferSpotType';
import { MypageIconInfo } from '@/api/generated/mypageIconInfo';
import { getUserItemCount } from '@/lib/itemUtils';
import AssetManager from '@/lib/asset-manager';
import OrtegaAtlasManager from '@/lib/atlas-manager';
import { MonthlyLoginBonusDialog } from '@/components/loginBonus/MonthlyLoginBonusDialog';
import { NoticeDialog } from '@/components/notice/NoticeDialog';
import { BookSortDialog } from '@/components/booksort/BookSortDialog';
import { LeaderboardDialog } from '@/components/leaderboard/LeaderboardDialog';
import { PresentBoxDialog } from '@/components/present/PresentBoxDialog';
import { AutopilotOverview } from '@/components/autopilot/AutopilotOverview';
import { untilReady } from '@/lib/retry';

export function DashboardPage() {
    const navigate = useNavigate();
    const { t } = useLocalizationStore();
    const currentLanguage = useLocalizationStore(state => state.currentLanguage);
    const [userData, setUserData] = useState<UserSyncData | null>(null);
    const [mypageData, setMypageData] = useState<UserGetMypageResponse | null>(null);
    const [isLoading, setIsLoading] = useState(true);
    const [monthlyLoginBonusOpen, setMonthlyLoginBonusOpen] = useState(false);
    const [noticeOpen, setNoticeOpen] = useState(false);
    const [bookSortOpen, setBookSortOpen] = useState(false);
    const [leaderboardOpen, setLeaderboardOpen] = useState(false);
    const [presentBoxOpen, setPresentBoxOpen] = useState(false);

    const getLanguageType = () => LanguageType[currentLanguage as keyof typeof LanguageType] ?? LanguageType.zhCN;

    useEffect(() => {
        let cancelled = false;

        async function fetchData() {
            // 后端启动时还在自动登录账号, 这期间 getUserData 是空的 (缓存的, 很便宜), 轮询到有数据再取 mypage
            const userResponse = await untilReady(
                () => ortegaApi.user.getUserData({}) as Promise<UserGetUserDataResponse>,
                (r) => !!r?.userSyncData
            );
            if (cancelled) return;
            if (!userResponse?.userSyncData) {
                console.error('Failed to fetch user data: backend still had no data');
                setIsLoading(false);
                return;
            }
            setUserData(userResponse.userSyncData);

            try {
                const mypageResponse = await ortegaApi.user.getMypage({ languageType: getLanguageType() }) as UserGetMypageResponse;
                if (!cancelled && mypageResponse) setMypageData(mypageResponse);
            } catch (error) {
                console.error('Failed to fetch mypage:', error);
            }
            if (!cancelled) setIsLoading(false);
        }

        fetchData();
        return () => { cancelled = true; };
    }, []);

    // 从 UserSyncData 派生实时数据
    const bossInfo = userData?.userBattleBossDtoInfo;
    const pvpInfo = userData?.userBattlePvpDtoInfo;
    const towerInfos = userData?.userTowerBattleDtoInfos || [];
    const statusInfo = userData?.userStatusDtoInfo;

    const dailyChecklist = [
        {
            id: 'auto-battle',
            category: t('[BattleTypeAuto]'),
            icon: Zap,
            items: [
                { name: t('[AutoBattleButtonQuickForward]'), current: userData?.shopCurrencyMissionProgressMap?.['HighSpeedBattle'] || 0, max: statusInfo?.vip ? (statusInfo.vip >= 1 ? 5 : 3) : 3, completed: false, link: '/missions' },
                { name: t('[AutoBattleButtonBoss]'), current: bossInfo?.bossTodayWinCount || 0, max: 3, completed: (bossInfo?.bossTodayWinCount || 0) >= 3, link: '/missions' },
            ]
        },
        {
            id: 'arena',
            category: t('[LockEquipmentDeckTypeLeague]'),
            icon: Swords,
            items: [
                { name: t('[CommonHeaderLocalPvpLabel]'), current: pvpInfo?.pvpTodayCount || 0, max: 5, completed: (pvpInfo?.pvpTodayCount || 0) >= 5, link: '/pvp' },
                { name: t('[CommonHeaderGlobalPvpLabel]'), current: 0, max: 10, completed: false, link: '/pvp' },
            ]
        },
        {
            id: 'tower',
            category: t('DASHBOARD_CATEGORY_TOWER'),
            icon: MapPin,
            items: [
                { name: t('[TowerTypeInfinite]'), current: towerInfos.find(t => t.towerType === TowerType.Infinite)?.todayBattleCount || 0, max: 3, completed: (towerInfos.find(t => t.towerType === TowerType.Infinite)?.todayBattleCount || 0) >= 3, link: '/dungeon' },
                { name: t('[LockEquipmentDeckTypeElementTowerBlue]'), current: towerInfos.filter(t => t.towerType !== TowerType.Infinite).reduce((acc, curr) => acc + curr.todayBattleCount, 0), max: 3, completed: (towerInfos.filter(t => t.towerType !== TowerType.Infinite).reduce((acc, curr) => acc + curr.todayBattleCount, 0)) >= 3, link: '/dungeon' },
            ]
        },
        {
            id: 'guild',
            category: t('[CommonFooterGuildButtonLabel]'),
            icon: Users,
            items: [
                { name: t('[MissionName533]'), current: statusInfo?.isFirstVisitGuildAtDay ? 0 : 1, max: 1, completed: !statusInfo?.isFirstVisitGuildAtDay, link: '/guild' },
                { name: t('[CommonHeaderGuildRaidLabel]'), current: 0, max: 2, completed: false, link: '/guild' },
                { name: t('[GuildTowerTitle]'), current: 0, max: 3, completed: false, link: '/guild' },
            ]
        },
        {
            id: 'daily-tasks',
            category: t('[ShopCurrencyMissionTypeActivityMedal]'),
            icon: Trophy,
            items: [
                { name: t('[MyPageMenuButtonMissionLabel]'), current: userData?.userMissionActivityDtoInfos?.find(m => m.missionGroupType === MissionGroupType.Daily)?.progressCount || 0, max: 500, completed: false, link: '/missions' },
                { name: t('DASHBOARD_ITEM_WEEKLY_MISSION'), current: userData?.userMissionActivityDtoInfos?.find(m => m.missionGroupType === MissionGroupType.Weekly)?.progressCount || 0, max: 2000, completed: false, link: '/missions' },
            ]
        },
        {
            id: 'other',
            category: t('[TutorialDescriptionBanner38Text4]'),
            icon: Gift,
            items: [
                { name: t('DASHBOARD_ITEM_RE_SIGN'), current: 0, max: statusInfo?.vip || 0, completed: false, link: '/missions' },
                { name: t('[CommonHeaderBountyQuestLabel]'), current: 0, max: 5, completed: false, link: '/missions' },
                { name: t('DASHBOARD_ITEM_FRIEND_BATTLE'), current: userData?.todayChallengeFriendBattleCount || 0, max: statusInfo?.vip ? (statusInfo.vip >= 12 ? 500 : statusInfo.vip >= 10 ? 200 : statusInfo.vip >= 8 ? 100 : 50) : 50, completed: (userData?.todayChallengeFriendBattleCount || 0) >= (statusInfo?.vip ? (statusInfo.vip >= 12 ? 500 : statusInfo.vip >= 10 ? 200 : statusInfo.vip >= 8 ? 100 : 50) : 50), link: '/missions' },
            ]
        }
    ];

    // 重要提醒逻辑
    const alerts = [];
    if (userData?.existVipDailyGift) {
        alerts.push({ type: 'success', message: t('DASHBOARD_ALERT_VIP_DAILY') });
    }

    const now = new Date();
    const currentHour = now.getHours();
    const currentMinute = now.getMinutes();
    const currentTimeMinutes = currentHour * 60 + currentMinute;

    // 幻影神殿活跃时段: 12:30-13:30, 19:30-20:30
    const isInPhantomTime = (currentTimeMinutes >= 12 * 60 + 30 && currentTimeMinutes <= 13 * 60 + 30) ||
        (currentTimeMinutes >= 19 * 60 + 30 && currentTimeMinutes <= 20 * 60 + 30);

    if (isInPhantomTime) {
        alerts.push({ type: 'info', message: t('DASHBOARD_ALERT_PHANTOM_TEMPLE_ACTIVE') });
    } else {
        alerts.push({ type: 'info', message: t('DASHBOARD_ALERT_PHANTOM_TEMPLE_TIME') });
    }

    // 公会战备战期间提醒: 7:45～20:30
    const isInGvgPrepareTime = currentTimeMinutes >= 7 * 60 + 45 && currentTimeMinutes <= 20 * 60 + 30;
    if (isInGvgPrepareTime) {
        alerts.push({ type: 'warning', message: t('DASHBOARD_ALERT_GVG_PREPARE') });
    }


    const getProgressColor = (current: number, max: number) => {
        const percentage = (current / max) * 100;
        if (percentage >= 100) return 'bg-green-500';
        if (percentage >= 50) return 'bg-yellow-500';
        return 'bg-red-500';
    };

    if (isLoading) {
        return (
            <div className="flex h-[400px] items-center justify-center">
                <Loader2 className="h-8 w-8 animate-spin text-primary" />
                <span className="ml-2">{t('[Loading]')}</span>
            </div>
        );
    }

    const items = userData?.userItemDtoInfo;

    const resourcesReal = [
        {
            name: t('[ItemName4]'), // 钻石
            current: getUserItemCount(items, ItemType.CurrencyFree, 1, true),
            free: getUserItemCount(items, ItemType.CurrencyFree, 1),
            paid: getUserItemCount(items, ItemType.CurrencyPaid, 0),
            icon: <Diamond className="h-5 w-5 text-blue-500" />,
            color: 'text-blue-500'
        },
        {
            name: t('[ItemName5]'), // 金币
            current: getUserItemCount(items, ItemType.Gold, 1),
            icon: <Coins className="h-5 w-5 text-yellow-500" />,
            color: 'text-yellow-500'
        },
        {
            name: t('[ItemName9]'), // 友谊点
            current: getUserItemCount(items, ItemType.FriendPoint, 1),
            icon: <Heart className="h-5 w-5 text-pink-500" />,
            color: 'text-pink-500'
        },
        {
            name: t('[ItemName11]'), // 经验珠
            current: getUserItemCount(items, ItemType.CharacterTrainingMaterial, 2),
            icon: <FlaskConical className="h-5 w-5 text-purple-500" />,
            color: 'text-purple-500'
        },
        {
            name: t('[ItemName10]'), // 潜能果
            current: getUserItemCount(items, ItemType.CharacterTrainingMaterial, 1),
            icon: <Package className="h-5 w-5 text-orange-500" />,
            color: 'text-orange-500'
        },
    ];

    // 徽章通知区域数据
    const notificationBadges = [
        {
            id: 'private-chat',
            label: t('[CommonHeaderChatLabel]'), // 私聊
            visible: mypageData?.existNewPrivateChat ?? false,
            icon: MessageCircle,
            color: 'bg-blue-500'
        },
        {
            id: 'bounty-quest',
            label: t('[CommonHeaderBountyQuestLabel]'), // 悬赏任务
            visible: mypageData?.existNotReceivedBountyQuestReward ?? false,
            icon: ScrollText,
            color: 'bg-amber-500'
        },
        {
            id: 'dungeon-battle',
            label: t('[CommonHeaderDungeonBattleLabel]'), // 地牢战斗
            visible: (userData as unknown as UserGetUserDataResponse)?.isNotClearDungeonBattleMap ?? false,
            icon: MapPin,
            color: 'bg-purple-500'
        }
    ];

    const visibleBadges = notificationBadges.filter(b => b.visible);
    const mypageIconInfos = mypageData?.mypageInfo?.mypageIconInfos ?? [];

    // 处理 MypageIconInfo 点击
    const handleMypageIconClick = (info: MypageIconInfo) => {
        const transferSpotType = info.transferDetailInfo?.transferSpotType;
        if (transferSpotType === TransferSpotType.MonthlyLoginBonus) {
            setMonthlyLoginBonusOpen(true);
        } else if (transferSpotType === TransferSpotType.BookSort) {
            setBookSortOpen(true);
        }
        // 可以在这里添加其他 TransferSpotType 的处理
    };

    // 判断是否可点击
    const isClickableIcon = (info: MypageIconInfo): boolean => {
        const transferSpotType = info.transferDetailInfo?.transferSpotType;
        return transferSpotType === TransferSpotType.MonthlyLoginBonus || transferSpotType === TransferSpotType.BookSort;
    };

    return (
        <div className="space-y-6">
            <AutopilotOverview />

            {/* Resources Grid */}
            <div className="grid gap-4 grid-cols-2 md:grid-cols-3 lg:grid-cols-5">
                {resourcesReal.map((resource) => (
                    <Card key={resource.name}>
                        <CardContent className="p-4 flex flex-col justify-center h-full">
                            <div className="flex items-center gap-2 mb-1">
                                {resource.icon}
                                <span className="text-xs text-muted-foreground font-medium">{resource.name}</span>
                            </div>
                            <div className={`text-xl font-bold truncate ${resource.color}`}>
                                {resource.current.toLocaleString()}
                            </div>
                            {resource.name === t('[ItemName4]') && (
                                <div className="mt-1 text-[10px] text-muted-foreground flex gap-2">
                                    <span>{t('[ShopProductFreeLabel]')}: {resource.free?.toLocaleString()}</span>
                                    <span>{t('[CommonCurrencyPaidLabel]')}: {resource.paid?.toLocaleString()}</span>
                                </div>
                            )}
                        </CardContent>
                    </Card>
                ))}
            </div>

            {/* Notification Badges */}
            {visibleBadges.length > 0 && (
                <div className="flex flex-wrap gap-2">
                    {visibleBadges.map((badge) => {
                        const Icon = badge.icon;
                        return (
                            <Badge key={badge.id} variant="secondary" className="px-3 py-1.5 text-sm flex items-center gap-1.5">
                                <span className={`relative flex h-2 w-2`}>
                                    <span className={`animate-ping absolute inline-flex h-full w-full rounded-full opacity-75 ${badge.color}`}></span>
                                    <span className={`relative inline-flex rounded-full h-2 w-2 ${badge.color}`}></span>
                                </span>
                                <Icon className="h-3.5 w-3.5" />
                                {badge.label}
                            </Badge>
                        );
                    })}
                </div>
            )}

            {/* MyPage Icons */}
            {mypageIconInfos.length > 0 && (
                <div className="flex flex-wrap gap-4">
                    {mypageIconInfos.map((info) => {
                        const clickable = isClickableIcon(info);
                        return (
                            <div
                                key={info.id}
                                className={`relative group flex flex-col items-center justify-center w-[64px] h-[64px] ${clickable ? 'cursor-pointer hover:scale-105 transition-transform' : ''}`}
                                onClick={() => clickable && handleMypageIconClick(info)}
                                title={t(info.iconNameKey)}
                            >
                                <img
                                    src={AssetManager.myPage.getUrl(info.imageId)}
                                    alt={t(info.iconNameKey)}
                                    className="w-full h-full object-contain drop-shadow-md"
                                />
                                <div className="absolute bottom-1 left-1/2 -translate-x-1/2 w-max px-0.5 flex flex-col justify-end pointer-events-none z-10">
                                    <span
                                        className="text-[13px] font-bold text-center leading-[1] text-white"
                                        style={{ textShadow: '-1px -1px 0 #000, 1px -1px 0 #000, -1px 1px 0 #000, 1px 1px 0 #000, 0px 1px 1px rgba(0,0,0,0.8)' }}
                                        dangerouslySetInnerHTML={{ __html: t(info.iconNameKey) }}
                                    />
                                </div>
                                {info.isDisplayBadge && (
                                    <span className="absolute top-0 right-0 flex h-3 w-3">
                                        <span className="animate-ping absolute inline-flex h-full w-full rounded-full bg-red-400 opacity-75"></span>
                                        <span className="relative inline-flex rounded-full h-3 w-3 bg-red-500 border border-background"></span>
                                    </span>
                                )}
                            </div>
                        );
                    })}
                </div>
            )}

            {/* Quick Menu Icons */}
            {(() => {
                const quickMenuItems = [
                    { key: 'ranking', labelKey: '[MyPageMenuButtonRankingLabel]', icon: 'icon_menu_ranking', action: () => setLeaderboardOpen(true), showBadge: false },
                    { key: 'shop', labelKey: '[MyPageMenuButtonTradingPostLabel]', icon: 'icon_menu_shop', action: () => navigate('/shop'), showBadge: false },
                    { key: 'present', labelKey: '[MyPageMenuButtonPresentBoxLabel]', icon: 'icon_menu_present', action: () => setPresentBoxOpen(true), showBadge: (userData?.presentCount ?? 0) > 0 },
                    { key: 'news', labelKey: '[MyPageMenuButtonNewsLabel]', icon: 'icon_menu_news', action: () => setNoticeOpen(true), showBadge: (mypageData?.unreadIndividualNotificationIdList?.length ?? 0) > 0 },
                    { key: 'friend', labelKey: '[MyPageMenuButtonFriendLabel]', icon: 'icon_menu_friend', action: () => navigate('/friends'), showBadge: mypageData?.existNewFriendPointTransfer ?? false },
                    { key: 'mission', labelKey: '[MyPageMenuButtonMissionLabel]', icon: 'icon_menu_mission', action: () => navigate('/missions'), showBadge: mypageData?.existNotReceivedMissionReward ?? false },
                ];
                return (
                    <div className="flex flex-wrap gap-4">
                        {quickMenuItems.map((item) => (
                            <div
                                key={item.key}
                                className="relative group flex flex-col items-center justify-center w-[64px] h-[64px] cursor-pointer hover:scale-105 transition-transform"
                                onClick={item.action}
                                title={t(item.labelKey)}
                            >
                                <img
                                    src={OrtegaAtlasManager.getSprite(item.icon)}
                                    alt={t(item.labelKey)}
                                    className="w-full h-full object-contain drop-shadow-md"
                                />
                                <div className="absolute bottom-1 left-1/2 -translate-x-1/2 w-max px-0.5 flex flex-col justify-end pointer-events-none z-10">
                                    <span
                                        className="text-[13px] font-bold text-center leading-[1] text-white"
                                        style={{ textShadow: '-1px -1px 0 #000, 1px -1px 0 #000, -1px 1px 0 #000, 1px 1px 0 #000, 0px 1px 1px rgba(0,0,0,0.8)' }}
                                    >
                                        {t(item.labelKey)}
                                    </span>
                                </div>
                                {item.showBadge && (
                                    <span className="absolute top-0 right-0 flex h-3 w-3">
                                        <span className="animate-ping absolute inline-flex h-full w-full rounded-full bg-red-400 opacity-75"></span>
                                        <span className="relative inline-flex rounded-full h-3 w-3 bg-red-500 border border-background"></span>
                                    </span>
                                )}
                            </div>
                        ))}
                    </div>
                );
            })()}

            {/* Page Header */}
            <div className="flex items-center justify-between">
                <div>
                    <h1 className="text-2xl font-bold">{t('DASHBOARD_DAILY_CHECKLIST_TITLE')}</h1>
                    <p className="text-muted-foreground">{t('DASHBOARD_DAILY_CHECKLIST_DESC')}</p>
                </div>
            </div>

            {/* Alerts */}
            <div className="space-y-2">
                {alerts.map((alert, idx) => (
                    <div
                        key={idx}
                        className={`rounded-lg border-l-4 p-3 text-sm ${alert.type === 'warning' ? 'border-yellow-500 bg-yellow-50 dark:bg-yellow-950' :
                            alert.type === 'info' ? 'border-blue-500 bg-blue-50 dark:bg-blue-950' :
                                'border-green-500 bg-green-50 dark:bg-green-950'
                            }`}
                    >
                        {alert.message}
                    </div>
                ))}
            </div>


            {/* Daily Checklist */}
            <div className="grid gap-6 grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4">
                {dailyChecklist.map((category) => {
                    const Icon = category.icon;
                    const totalItems = category.items.length;
                    const completedItems = category.items.filter(item => item.completed).length;
                    const categoryProgress = (completedItems / totalItems) * 100;

                    return (
                        <Card key={category.id}>
                            <CardHeader>
                                <div className="flex items-center justify-between">
                                    <div className="flex items-center gap-3">
                                        <div className="rounded-lg bg-primary/10 p-2">
                                            <Icon className="h-5 w-5" />
                                        </div>
                                        <div>
                                            <CardTitle className="text-lg">{category.category}</CardTitle>
                                            <CardDescription>
                                                {t('DASHBOARD_ITEMS_COMPLETED', [completedItems, totalItems])}
                                            </CardDescription>
                                        </div>
                                    </div>
                                    <Badge variant={categoryProgress === 100 ? 'default' : 'secondary'}>
                                        {Math.round(categoryProgress)}%
                                    </Badge>
                                </div>
                            </CardHeader>
                            <CardContent className="space-y-3">
                                {category.items.map((item, idx) => {
                                    const progress = (item.current / item.max) * 100;
                                    const isCompleted = item.completed || progress >= 100;

                                    return (
                                        <div key={idx} className="space-y-2">
                                            <div className="flex items-center justify-between">
                                                <div className="flex items-center gap-2">
                                                    {isCompleted ? (
                                                        <CheckCircle2 className="h-4 w-4 text-green-500" />
                                                    ) : (
                                                        <Circle className="h-4 w-4 text-muted-foreground" />
                                                    )}
                                                    <span className={`text-sm ${isCompleted ? 'text-muted-foreground line-through' : ''}`}>
                                                        {item.name}
                                                    </span>
                                                </div>
                                                <div className="flex items-center gap-2">
                                                    <span className="text-sm font-medium">
                                                        {item.current}/{item.max}
                                                    </span>
                                                    {!isCompleted && (
                                                        <Button
                                                            size="sm"
                                                            variant="ghost"
                                                            onClick={() => navigate(item.link)}
                                                            className="h-6 px-2 text-xs"
                                                        >
                                                            {t('[TutorialDescriptionBanner61Text6]')}
                                                        </Button>
                                                    )}
                                                </div>
                                            </div>
                                            <Progress
                                                value={progress}
                                                className={`h-1 ${getProgressColor(item.current, item.max)}`}
                                            />
                                        </div>
                                    );
                                })}
                            </CardContent>
                        </Card>
                    );
                })}
            </div>


            {/* Monthly Login Bonus Dialog */}
            <MonthlyLoginBonusDialog
                open={monthlyLoginBonusOpen}
                onOpenChange={setMonthlyLoginBonusOpen}
            />

            {/* Notice Dialog */}
            <NoticeDialog
                open={noticeOpen}
                onOpenChange={setNoticeOpen}
            />

            {/* BookSort Dialog */}
            <BookSortDialog
                open={bookSortOpen}
                onOpenChange={setBookSortOpen}
            />

            {/* Leaderboard Dialog */}
            <LeaderboardDialog
                open={leaderboardOpen}
                onOpenChange={setLeaderboardOpen}
            />

            {/* Present Box Dialog */}
            <PresentBoxDialog
                open={presentBoxOpen}
                onOpenChange={setPresentBoxOpen}
            />
        </div>
    );
}
