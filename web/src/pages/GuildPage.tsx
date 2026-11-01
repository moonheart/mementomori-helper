import { useCallback, useEffect, useState } from 'react';
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from '@/components/ui/card';
import { Badge } from '@/components/ui/badge';
import { Button } from '@/components/ui/button';
import { Tabs, TabsContent, TabsList, TabsTrigger } from '@/components/ui/tabs';
import { Progress } from '@/components/ui/progress';
import { useTranslation } from '@/hooks/useTranslation';
import { useToast } from '@/hooks/use-toast';
import { useAccountStore } from '@/store/accountStore';
import { ortegaApi } from '@/api/ortega-client';
import { jobsApi } from '@/api/jobs-service';
import { GuildGetGuildBaseInfoResponse } from '@/api/generated/GuildgetGuildBaseInfoResponse';
import { GuildRaidInfo } from '@/api/generated/guildRaidInfo';
import { PlayerGuildPositionType } from '@/api/generated/playerGuildPositionType';
import { Users, Crown, Trophy, Swords, TrendingUp, Gift, Award, Loader2, RefreshCw, Play } from 'lucide-react';
import { JSX } from 'react';

// Leader first, then by rank in the guild, then by power.
const POSITION_ORDER: Record<number, number> = {
    [PlayerGuildPositionType.Leader]: 0,
    [PlayerGuildPositionType.SubLeader]: 1,
    [PlayerGuildPositionType.Commander]: 2,
    [PlayerGuildPositionType.Veteran]: 3,
    [PlayerGuildPositionType.Member]: 4,
};

// lastLoginTime comes over the wire as a .NET TimeSpan string ("d.hh:mm:ss.fffffff"): time since last login, not a date.
function sinceLogin(value: unknown): string {
    const m = /^(?:(\d+)\.)?(\d+):(\d+):\d+/.exec(String(value ?? ''));
    if (!m) return '-';
    const minutes = Number(m[1] ?? 0) * 1440 + Number(m[2]) * 60 + Number(m[3]);
    if (minutes < 60) return `${minutes}m`;
    if (minutes < 60 * 24) return `${Math.floor(minutes / 60)}h`;
    return `${Math.floor(minutes / 1440)}d`;
}

export function GuildPage() {
    const { t } = useTranslation();
    const { toast } = useToast();
    const currentAccountId = useAccountStore(state => state.currentAccountId);
    const [base, setBase] = useState<GuildGetGuildBaseInfoResponse | null>(null);
    const [raids, setRaids] = useState<GuildRaidInfo[]>([]);
    const [noGuild, setNoGuild] = useState(false);
    const [loading, setLoading] = useState(true);
    const [running, setRunning] = useState<string | null>(null);

    const load = useCallback(async () => {
        if (!currentAccountId) return;
        setLoading(true);
        try {
            const { guildId } = await ortegaApi.guildInfo.getGuildId({});
            if (!guildId) {
                setNoGuild(true);
                return;
            }
            setNoGuild(false);
            const [baseRes, raidRes] = await Promise.all([
                ortegaApi.guildInfo.getGuildBaseInfo({ belongGuildId: guildId }),
                ortegaApi.guildRaid.getGuildRaidInfo({ belongGuildId: guildId }).catch((error) => {
                    console.warn('Guild: raid info failed to load, continuing without it', error);
                    return null;
                }),
            ]);
            setBase(baseRes);
            setRaids(raidRes?.guildRaidInfos ?? []);
        } catch (error) {
            console.error('Guild: failed to load', error);
            toast({ variant: 'destructive', title: t('GUILD_LOAD_FAILED') });
        } finally {
            setLoading(false);
        }
    }, [currentAccountId, toast, t]);

    useEffect(() => {
        load();
    }, [load]);

    // Buttons run the same actions the daily job runs, then refresh.
    const run = async (actionKey: string) => {
        if (!currentAccountId || running) return;
        setRunning(actionKey);
        try {
            await jobsApi.runAction(currentAccountId, actionKey);
            toast({ title: t('AUTO_RUN_NOW_DONE', [t(`ACTION_${actionKey}`)]) });
            await load();
        } catch (error) {
            console.error('Guild action failed:', error);
            toast({ variant: 'destructive', title: t('AUTOMATION_TRIGGER_FAILED'), description: t('AUTOMATION_TRIGGER_FAILED_DESC') });
        } finally {
            setRunning(null);
        }
    };

    const actionButton = (actionKey: string, label: string, icon: JSX.Element, variant: 'default' | 'outline' = 'default') => (
        <Button className="w-full" variant={variant} disabled={running !== null} onClick={() => run(actionKey)}>
            {running === actionKey ? <Loader2 className="mr-2 h-4 w-4 animate-spin" /> : icon}
            {label}
        </Button>
    );

    const getPositionBadge = (position: PlayerGuildPositionType) => {
        const badges: Record<number, JSX.Element> = {
            [PlayerGuildPositionType.Leader]: <Badge className="bg-gradient-to-r from-yellow-500 to-orange-500"><Crown className="h-3 w-3 mr-1" />{t('[PlayerGuildPositionTypeLeader]')}</Badge>,
            [PlayerGuildPositionType.SubLeader]: <Badge className="bg-gradient-to-r from-purple-500 to-pink-500">{t('[PlayerGuildPositionTypeSubLeader]')}</Badge>,
            [PlayerGuildPositionType.Commander]: <Badge className="bg-blue-500">{t('[PlayerGuildPositionTypeCommander]')}</Badge>,
            [PlayerGuildPositionType.Veteran]: <Badge className="bg-green-500">{t('[PlayerGuildPositionTypeVeteran]')}</Badge>,
            [PlayerGuildPositionType.Member]: <Badge variant="secondary">{t('[PlayerGuildPositionTypeMember]')}</Badge>,
        };
        return badges[position] || null;
    };

    if (loading && !base) {
        return <div className="flex justify-center py-16"><Loader2 className="h-8 w-8 animate-spin text-muted-foreground" /></div>;
    }

    if (noGuild || !base) {
        return (
            <Card className="border-dashed">
                <CardContent className="flex flex-col items-center justify-center py-16 text-center">
                    <Users className="h-16 w-16 mb-4 text-muted-foreground opacity-20" />
                    <h3 className="text-lg font-semibold mb-2">{t(noGuild ? 'GUILD_NOT_IN_GUILD' : 'GUILD_LOAD_FAILED')}</h3>
                    <Button variant="outline" onClick={load}><RefreshCw className="mr-2 h-4 w-4" />{t('GUILD_RELOAD')}</Button>
                </CardContent>
            </Card>
        );
    }

    const sync = base.guildSyncData;
    const info = sync.guildInfo;
    const members = [...(sync.guildPlayerInfoList ?? [])].sort((a, b) =>
        (POSITION_ORDER[a.playerGuildPositionType] ?? 9) - (POSITION_ORDER[b.playerGuildPositionType] ?? 9) || b.battlePower - a.battlePower);
    const myPosition = sync.playerGuildPositionType;
    const canOpenRaid = myPosition !== PlayerGuildPositionType.Member && myPosition !== PlayerGuildPositionType.None;
    const gvg = base.localGuildGvgInfo;

    const stat = (icon: JSX.Element, label: string, value: string, color: string) => (
        <Card>
            <CardContent className="p-4">
                <div className="flex items-center justify-between">
                    <div className="flex items-center gap-2">
                        {icon}
                        <span className="text-sm text-muted-foreground">{label}</span>
                    </div>
                    <span className={`font-bold ${color}`}>{value}</span>
                </div>
            </CardContent>
        </Card>
    );

    return (
        <div className="space-y-6">
            <div className="flex items-center justify-between">
                <div>
                    <h2 className="text-xl font-bold">{info.guildOverView?.guildName}</h2>
                    {info.guildOverView?.guildDescription && <p className="text-sm text-muted-foreground">{info.guildOverView.guildDescription}</p>}
                </div>
                <Button variant="ghost" size="icon" onClick={load} disabled={loading}>
                    <RefreshCw className={`h-4 w-4 ${loading ? 'animate-spin' : ''}`} />
                </Button>
            </div>

            <div className="grid gap-4 md:grid-cols-4">
                {stat(<Users className="h-5 w-5 text-blue-500" />, t('GUILD_MEMBERS'), String(members.length || info.guildMemberCount), 'text-blue-600')}
                {stat(<TrendingUp className="h-5 w-5 text-green-500" />, t('GUILD_LEVEL'), `Lv.${info.guildLevel}`, 'text-green-600')}
                {stat(<Trophy className="h-5 w-5 text-purple-500" />, t('GUILD_TOTAL_POWER'), sync.guildBattlePower.toLocaleString(), 'text-purple-600')}
                {stat(<Award className="h-5 w-5 text-orange-500" />, t('GUILD_FAME_LABEL'), info.guildFame.toLocaleString(), 'text-orange-600')}
            </div>

            <Card>
                <CardContent className="flex items-center justify-between gap-4 p-4">
                    <span className="text-sm text-muted-foreground">{t('GUILD_EXP')}: {info.guildExp.toLocaleString()}</span>
                    <div className="w-56">{actionButton('GuildCheckin', t('GUILD_CHECKIN'), <Gift className="mr-2 h-4 w-4" />)}</div>
                </CardContent>
            </Card>

            <Tabs defaultValue="members" className="space-y-6">
                <TabsList className="grid w-full grid-cols-4">
                    <TabsTrigger value="members">{t('GUILD_TAB_MEMBERS')}</TabsTrigger>
                    <TabsTrigger value="activities">{t('GUILD_TAB_ACTIVITIES')}</TabsTrigger>
                    <TabsTrigger value="raid">{t('[CommonHeaderGuildRaidLabel]')}</TabsTrigger>
                    <TabsTrigger value="war">{t('[BattleTypeGvG]')}</TabsTrigger>
                </TabsList>

                <TabsContent value="members" className="space-y-2">
                    <div className="text-sm text-muted-foreground">{t('GUILD_MEMBER_TOTAL', [members.length])}</div>
                    {members.map((member) => (
                        <Card key={member.playerId}>
                            <CardContent className="p-4">
                                <div className="flex items-center gap-4">
                                    <div className="flex items-center justify-center w-12 h-12 shrink-0 rounded-full bg-gradient-to-br from-blue-500 to-purple-500 text-white font-bold">
                                        {member.playerName.charAt(0)}
                                    </div>
                                    <div className="min-w-0">
                                        <div className="flex flex-wrap items-center gap-2 mb-1">
                                            <span className="font-semibold break-all">{member.playerName}</span>
                                            {getPositionBadge(member.playerGuildPositionType)}
                                        </div>
                                        <div className="flex flex-wrap items-center gap-x-3 text-sm text-muted-foreground">
                                            <span>Lv.{member.playerLevel}</span>
                                            <span>{t('CAVE_POWER', [member.battlePower.toLocaleString()])}</span>
                                            <span>{t('GUILD_CONTRIBUTION', [member.guildPeriodTotalFame.toLocaleString()])}</span>
                                            <span>{t('GUILD_LAST_LOGIN', [sinceLogin(member.lastLoginTime)])}</span>
                                        </div>
                                    </div>
                                </div>
                            </CardContent>
                        </Card>
                    ))}
                </TabsContent>

                <TabsContent value="activities" className="space-y-4">
                    <Card>
                        <CardHeader>
                            <CardTitle className="flex items-center gap-2">
                                <TrendingUp className="h-6 w-6 text-green-500" />
                                {t('[GuildTowerTitle]')}
                            </CardTitle>
                            <CardDescription>{t('GUILD_TREE_DESC')}</CardDescription>
                        </CardHeader>
                        <CardContent className="space-y-3">
                            <div className="text-2xl font-bold text-green-600">{t('GUILD_TREE_FLOOR', [base.guildTowerCurrentFloor])}</div>
                            <div className="text-sm text-muted-foreground">{t('GUILD_TREE_WINS_TODAY', [base.guildTowerTodayWinCount])}</div>
                            {actionButton('GuildTower', t('GUILD_GO_CHALLENGE'), <Swords className="mr-2 h-4 w-4" />)}
                        </CardContent>
                    </Card>
                </TabsContent>

                <TabsContent value="raid" className="space-y-4">
                    {raids.filter(r => r.isOpen).map((raid) => {
                        const boss = raid.guildRaidBossInfo;
                        const hpPercent = boss?.maxHp ? (boss.currentHp / boss.maxHp) * 100 : 0;
                        return (
                            <Card key={boss?.bossGuid ?? raid.guildRaidDtoInfo.bossType}>
                                <CardHeader>
                                    <CardTitle>{boss?.name ?? t('[CommonHeaderGuildRaidLabel]')}</CardTitle>
                                </CardHeader>
                                <CardContent className="space-y-3">
                                    {boss?.maxHp ? (
                                        <>
                                            <div className="flex items-center justify-between text-sm">
                                                <span>HP</span>
                                                <span>{boss.currentHp.toLocaleString()} / {boss.maxHp.toLocaleString()}</span>
                                            </div>
                                            <Progress value={hpPercent} className="h-3" />
                                        </>
                                    ) : (
                                        // Damage-race bosses (Sonia) have no HP: show the guild's total damage instead.
                                        <div className="flex items-center justify-between text-sm">
                                            <span>{t('GUILD_RAID_TOTAL_DAMAGE')}</span>
                                            <span>{(raid.guildRaidDtoInfo?.totalDamage ?? 0).toLocaleString()}</span>
                                        </div>
                                    )}
                                    <div className="flex items-center justify-between text-sm">
                                        <span>{t('GUILD_TODAY_CHALLENGES')}</span>
                                        <Badge>{t('GUILD_TIMES', [raid.userGuildRaidDtoInfo?.challengeCount ?? 0])}</Badge>
                                    </div>
                                    <div className="flex items-center justify-between text-sm">
                                        <span>{t('GUILD_MY_MAX_DAMAGE')}</span>
                                        <span>{(raid.userGuildRaidDtoInfo?.maxDamage ?? 0).toLocaleString()}</span>
                                    </div>
                                </CardContent>
                            </Card>
                        );
                    })}
                    {raids.every(r => !r.isOpen) && <div className="text-sm text-muted-foreground">{t('GUILD_RAID_NONE_OPEN')}</div>}
                    <div className="grid gap-2 md:grid-cols-2">
                        {actionButton('GuildRaidBattle', t('GUILD_START_CHALLENGE'), <Swords className="mr-2 h-4 w-4" />)}
                        {canOpenRaid && actionButton('GuildRaidOpen', t('GUILD_OPEN_RAID'), <Play className="mr-2 h-4 w-4" />, 'outline')}
                    </div>
                </TabsContent>

                <TabsContent value="war" className="space-y-4">
                    <Card>
                        <CardHeader>
                            <CardTitle>{t('GUILD_WAR_INFO')}</CardTitle>
                            <CardDescription>{gvg?.isOpen ? t('GUILD_WAR_QUALIFIED') : t('GUILD_WAR_NOT_QUALIFIED')}</CardDescription>
                        </CardHeader>
                        <CardContent className="space-y-4">
                            {gvg && (
                                <div className="grid grid-cols-2 gap-4 md:grid-cols-4">
                                    <div className="p-3 bg-muted rounded-lg">
                                        <div className="text-sm text-muted-foreground mb-1">{t('GUILD_GVG_TODAY_RANK')}</div>
                                        <div className="font-semibold">#{gvg.todayRanking || '-'}</div>
                                    </div>
                                    <div className="p-3 bg-muted rounded-lg">
                                        <div className="text-sm text-muted-foreground mb-1">{t('GUILD_CURRENT_RANK', [gvg.currentRanking || '-'])}</div>
                                    </div>
                                    <div className="p-3 bg-muted rounded-lg">
                                        <div className="text-sm text-muted-foreground mb-1">{t('GUILD_GVG_CASTLES')}</div>
                                        <div className="font-semibold">{gvg.castleCountLarge} / {gvg.castleCountMedium} / {gvg.castleCountSmall}</div>
                                    </div>
                                    <div className="p-3 bg-muted rounded-lg">
                                        <div className="text-sm text-muted-foreground mb-1">{t('GUILD_GVG_DECLARATIONS')}</div>
                                        <div className="font-semibold">{gvg.remainingDeclarationCount}</div>
                                    </div>
                                </div>
                            )}
                            {actionButton('GvgReward', t('ACTION_GvgReward'), <Gift className="mr-2 h-4 w-4" />, 'outline')}
                        </CardContent>
                    </Card>
                </TabsContent>
            </Tabs>
        </div>
    );
}
