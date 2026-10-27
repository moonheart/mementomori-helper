import { Card, CardContent, CardDescription, CardHeader, CardTitle } from '@/components/ui/card';
import { Badge } from '@/components/ui/badge';
import { Button } from '@/components/ui/button';
import { Tabs, TabsContent, TabsList, TabsTrigger } from '@/components/ui/tabs';
import { Alert, AlertDescription } from '@/components/ui/alert';
import { Progress } from '@/components/ui/progress';
import { useTranslation } from '@/hooks/useTranslation';
import {
    Users,
    Crown,
    Trophy,
    Swords,
    BookOpen,
    TrendingUp,
    Gift,
    Award
} from 'lucide-react';
import { JSX } from 'react';

// Mock数据 - 公会信息
const guildInfo = {
    name: '永恒殿堂',
    level: 18,
    exp: 45000,
    maxExp: 50000,
    members: 28,
    maxMembers: 30,
    totalPower: 3500000,
    guildPoints: 52000,
    maxGuildPoints: 70000,
    ranking: 8
};

// Mock数据 - 成员列表
const members = [
    { id: 1, name: '公会会长', level: 85, power: 145000, position: 'leader', contribution: 15000, online: true },
    { id: 2, name: '副会长', level: 83, power: 138000, position: 'vice', contribution: 12000, online: false },
    { id: 3, name: '指挥官A', level: 82, power: 135000, position: 'commander', contribution: 11500, online: true },
    { id: 4, name: '精锐成员', level: 80, power: 128000, position: 'elite', contribution: 10200, online: false },
    { id: 5, name: '普通成员', level: 75, power: 120000, position: 'member', contribution: 8500, online: true }
];

// Mock数据 - 公会活动
const guildActivities = {
    guildRaidAvailable: true,
    guildWarQualified: true,
    guildTreeActive: true,
    raidChallenges: 2,
    treeFloor: 45,
    warRanking: 8
};

export function GuildPage() {
    const { t } = useTranslation();
    const getPositionBadge = (position: string) => {
        const badges: Record<string, JSX.Element> = {
            'leader': <Badge className="bg-gradient-to-r from-yellow-500 to-orange-500"><Crown className="h-3 w-3 mr-1" />{t('[PlayerGuildPositionTypeLeader]')}</Badge>,
            'vice': <Badge className="bg-gradient-to-r from-purple-500 to-pink-500">{t('[PlayerGuildPositionTypeSubLeader]')}</Badge>,
            'commander': <Badge className="bg-blue-500">{t('[PlayerGuildPositionTypeCommander]')}</Badge>,
            'elite': <Badge className="bg-green-500">{t('[PlayerGuildPositionTypeVeteran]')}</Badge>,
            'member': <Badge variant="secondary">{t('[PlayerGuildPositionTypeMember]')}</Badge>
        };
        return badges[position] || <Badge variant="outline">{t('AUTO_TOWER_UNKNOWN')}</Badge>;
    };

    return (
        <div className="space-y-6">
            {/* 公会状态卡片 */}
            <div className="grid gap-4 md:grid-cols-4">
                <Card>
                    <CardContent className="p-4">
                        <div className="flex items-center justify-between">
                            <div className="flex items-center gap-2">
                                <Users className="h-5 w-5 text-blue-500" />
                                <span className="text-sm text-muted-foreground">{t('GUILD_MEMBERS')}</span>
                            </div>
                            <span className="font-bold text-blue-600">
                                {guildInfo.members}/{guildInfo.maxMembers}
                            </span>
                        </div>
                    </CardContent>
                </Card>

                <Card>
                    <CardContent className="p-4">
                        <div className="flex items-center justify-between">
                            <div className="flex items-center gap-2">
                                <TrendingUp className="h-5 w-5 text-green-500" />
                                <span className="text-sm text-muted-foreground">{t('GUILD_LEVEL')}</span>
                            </div>
                            <span className="font-bold text-green-600">
                                Lv.{guildInfo.level}
                            </span>
                        </div>
                    </CardContent>
                </Card>

                <Card>
                    <CardContent className="p-4">
                        <div className="flex items-center justify-between">
                            <div className="flex items-center gap-2">
                                <Trophy className="h-5 w-5 text-purple-500" />
                                <span className="text-sm text-muted-foreground">{t('GUILD_TOTAL_POWER')}</span>
                            </div>
                            <span className="font-bold text-purple-600">
                                {t('GUILD_TEN_THOUSAND', [(guildInfo.totalPower / 10000).toFixed(1)])}
                            </span>
                        </div>
                    </CardContent>
                </Card>

                <Card>
                    <CardContent className="p-4">
                        <div className="flex items-center justify-between">
                            <div className="flex items-center gap-2">
                                <Award className="h-5 w-5 text-orange-500" />
                                <span className="text-sm text-muted-foreground">{t('GUILD_POINTS')}</span>
                            </div>
                            <span className="font-bold text-orange-600">
                                {(guildInfo.guildPoints / 1000).toFixed(1)}K
                            </span>
                        </div>
                    </CardContent>
                </Card>
            </div>

            {/* 公会经验 */}
            <Card>
                <CardHeader>
                    <CardTitle className="flex items-center gap-2">
                        <TrendingUp className="h-6 w-6 text-green-500" />
                        {t('GUILD_EXP')}
                    </CardTitle>
                </CardHeader>
                <CardContent className="space-y-3">
                    <div className="flex items-center justify-between text-sm">
                        <span>{t('GUILD_EXP_TO_NEXT')}</span>
                        <span className="font-semibold">{guildInfo.maxExp - guildInfo.exp} EXP</span>
                    </div>
                    <Progress value={(guildInfo.exp / guildInfo.maxExp) * 100} className="h-3" />
                    <div className="flex items-center justify-between">
                        <Button>
                            <Gift className="mr-2 h-4 w-4" />
                            {t('GUILD_CHECKIN')}
                        </Button>
                        <span className="text-sm text-muted-foreground">
                            {guildInfo.exp.toLocaleString()} / {guildInfo.maxExp.toLocaleString()}
                        </span>
                    </div>
                </CardContent>
            </Card>

            <Tabs defaultValue="members" className="space-y-6">
                <TabsList className="grid w-full grid-cols-4">
                    <TabsTrigger value="members">{t('GUILD_TAB_MEMBERS')}</TabsTrigger>
                    <TabsTrigger value="activities">{t('GUILD_TAB_ACTIVITIES')}</TabsTrigger>
                    <TabsTrigger value="raid">{t('[CommonHeaderGuildRaidLabel]')}</TabsTrigger>
                    <TabsTrigger value="war">{t('[BattleTypeGvG]')}</TabsTrigger>
                </TabsList>

                {/* 成员列表 */}
                <TabsContent value="members" className="space-y-4">
                    <div className="flex items-center justify-between">
                        <div className="text-sm text-muted-foreground">
                            {t('GUILD_MEMBER_TOTAL', [guildInfo.members])}
                        </div>
                        <Button variant="outline" size="sm">
                            <Users className="mr-2 h-4 w-4" />
                            {t('GUILD_RECRUIT')}
                        </Button>
                    </div>

                    <div className="space-y-2">
                        {members.map((member) => (
                            <Card key={member.id}>
                                <CardContent className="p-4">
                                    <div className="flex items-center justify-between">
                                        <div className="flex items-center gap-4">
                                            <div className="flex items-center justify-center w-12 h-12 rounded-full bg-gradient-to-br from-blue-500 to-purple-500 text-white font-bold">
                                                {member.name.charAt(0)}
                                            </div>
                                            <div>
                                                <div className="flex items-center gap-2 mb-1">
                                                    <span className="font-semibold">{member.name}</span>
                                                    {getPositionBadge(member.position)}
                                                    {member.online && <Badge className="bg-green-500">{t('GUILD_ONLINE')}</Badge>}
                                                </div>
                                                <div className="flex items-center gap-3 text-sm text-muted-foreground">
                                                    <span>Lv.{member.level}</span>
                                                    <span>•</span>
                                                    <span>{t('CAVE_POWER', [member.power.toLocaleString()])}</span>
                                                    <span>•</span>
                                                    <span>{t('GUILD_CONTRIBUTION', [member.contribution.toLocaleString()])}</span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </CardContent>
                            </Card>
                        ))}
                    </div>
                </TabsContent>

                {/* 公会活动 */}
                <TabsContent value="activities" className="space-y-4">
                    <div className="grid gap-4 md:grid-cols-2">
                        <Card className={guildActivities.guildTreeActive ? 'border-2 border-green-500' : ''}>
                            <CardHeader>
                                <CardTitle className="flex items-center gap-2">
                                    <TrendingUp className="h-6 w-6 text-green-500" />
                                    {t('[GuildTowerTitle]')}
                                </CardTitle>
                                <CardDescription>{t('GUILD_TREE_DESC')}</CardDescription>
                            </CardHeader>
                            <CardContent className="space-y-3">
                                <div className="text-2xl font-bold text-green-600">
                                    {t('GUILD_TREE_FLOOR', [guildActivities.treeFloor])}
                                </div>
                                <div className="text-sm text-muted-foreground">
                                    {t('GUILD_TREE_TRIES')}
                                </div>
                                <Button className="w-full">{t('GUILD_GO_CHALLENGE')}</Button>
                            </CardContent>
                        </Card>

                        <Card>
                            <CardHeader>
                                <CardTitle className="flex items-center gap-2">
                                    <Swords className="h-6 w-6 text-purple-500" />
                                    {t('GUILD_MISSIONS')}
                                </CardTitle>
                                <CardDescription>{t('GUILD_MISSIONS_DESC')}</CardDescription>
                            </CardHeader>
                            <CardContent className="space-y-3">
                                <div className="text-sm text-muted-foreground">
                                    {t('GUILD_MISSIONS_HINT')}
                                </div>
                                <Button className="w-full">{t('GUILD_VIEW_MISSIONS')}</Button>
                            </CardContent>
                        </Card>
                    </div>
                </TabsContent>

                {/* 公会讨伐 */}
                <TabsContent value="raid" className="space-y-4">
                    <Card>
                        <CardHeader>
                            <CardTitle>{t('GUILD_SONIA_RAID')}</CardTitle>
                            <CardDescription>{t('GUILD_SONIA_RAID_DESC')}</CardDescription>
                        </CardHeader>
                        <CardContent className="space-y-3">
                            <div className="flex items-center justify-between">
                                <span className="text-sm">{t('GUILD_TODAY_CHALLENGES')}</span>
                                <Badge>{t('GUILD_TIMES', ['2/2'])}</Badge>
                            </div>
                            <Button className="w-full">
                                <Swords className="mr-2 h-4 w-4" />
                                {t('GUILD_START_CHALLENGE')}
                            </Button>
                        </CardContent>
                    </Card>

                    <Card className="border-2 border-orange-500">
                        <CardHeader>
                            <CardTitle className="flex items-center gap-2">
                                {t('GUILD_LIGHT_RAID')}
                                <Badge className="bg-orange-500">{t('GUILD_ADVANCED')}</Badge>
                            </CardTitle>
                            <CardDescription>{t('GUILD_LIGHT_RAID_DESC')}</CardDescription>
                        </CardHeader>
                        <CardContent className="space-y-3">
                            <div className="p-3 bg-orange-50 dark:bg-orange-950 rounded-lg">
                                <div className="text-sm text-muted-foreground">
                                    {t('GUILD_LIGHT_RAID_PERM')}
                                </div>
                            </div>
                            <Button className="w-full" variant="outline">
                                {t('GUILD_OPEN_RAID')}
                            </Button>
                        </CardContent>
                    </Card>
                </TabsContent>

                {/* 公会战 */}
                <TabsContent value="war" className="space-y-4">
                    {guildActivities.guildWarQualified ? (
                        <>
                            <Alert className="bg-gradient-to-r from-blue-50 to-purple-50 dark:from-blue-950 dark:to-purple-950 border-blue-500">
                                <Trophy className="h-4 w-4 text-blue-600" />
                                <AlertDescription className="text-blue-800 dark:text-blue-200">
                                    <strong>{t('GUILD_WAR_QUALIFIED')}</strong>
                                    {t('GUILD_WAR_TOP16')}
                                </AlertDescription>
                            </Alert>

                            <Card>
                                <CardHeader>
                                    <CardTitle>{t('GUILD_WAR_INFO')}</CardTitle>
                                    <CardDescription>{t('GUILD_WAR_INFO_DESC')}</CardDescription>
                                </CardHeader>
                                <CardContent className="space-y-4">
                                    <div className="grid grid-cols-2 gap-4">
                                        <div className="p-3 bg-muted rounded-lg">
                                            <div className="text-sm text-muted-foreground mb-1">{t('GUILD_WAR_PREP')}</div>
                                            <div className="font-semibold">7:45-20:30</div>
                                        </div>
                                        <div className="p-3 bg-muted rounded-lg">
                                            <div className="text-sm text-muted-foreground mb-1">{t('GUILD_WAR_BATTLE')}</div>
                                            <div className="font-semibold">20:45-21:30</div>
                                        </div>
                                    </div>
                                    <div className="text-sm text-muted-foreground">
                                        • {t('GUILD_WAR_RULE_1')}<br />
                                        • {t('GUILD_WAR_RULE_2')}<br />
                                        • {t('GUILD_WAR_RULE_3')}
                                    </div>
                                    <Button className="w-full">
                                        <Swords className="mr-2 h-4 w-4" />
                                        {t('GUILD_WAR_ENTER')}
                                    </Button>
                                </CardContent>
                            </Card>
                        </>
                    ) : (
                        <Card className="border-dashed">
                            <CardContent className="flex flex-col items-center justify-center py-16 text-center">
                                <Trophy className="h-16 w-16 mb-4 text-muted-foreground opacity-20" />
                                <h3 className="text-lg font-semibold mb-2">{t('GUILD_WAR_NOT_QUALIFIED')}</h3>
                                <p className="text-muted-foreground mb-4">
                                    {t('GUILD_WAR_NEED_TOP16')}
                                </p>
                                <div className="text-sm text-muted-foreground">
                                    {t('GUILD_CURRENT_RANK', [guildInfo.ranking])}
                                </div>
                            </CardContent>
                        </Card>
                    )}
                </TabsContent>
            </Tabs>
        </div>
    );
}
