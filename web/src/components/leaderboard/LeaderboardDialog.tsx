import { useState, useEffect } from 'react';
import { useTranslation } from '@/hooks/useTranslation';
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from '@/components/ui/card';
import { Badge } from '@/components/ui/badge';
import { Tabs, TabsContent, TabsList, TabsTrigger } from '@/components/ui/tabs';
import { Dialog, DialogContent, DialogHeader, DialogTitle } from '@/components/ui/dialog';
import { PlayerInfoDialog } from '@/components/player/PlayerInfoDialog';
import {
    Trophy,
    Crown,
    TrendingUp,
    Star,
    Award,
    Loader2,
    Zap,
    MapPin,
    Shield,
    Gem
} from 'lucide-react';
import { ortegaApi } from '@/api/ortega-client';
import { RankingGetPlayerRankingResponse } from '@/api/generated/RankinggetPlayerRankingResponse';
import { RankingGetGuildRankingResponse } from '@/api/generated/RankinggetGuildRankingResponse';
import { RankingGetTowerRankingResponse } from '@/api/generated/RankinggetTowerRankingResponse';
import { PlayerInfo } from '@/api/generated/playerInfo';
import { GuildRank } from '@/api/generated/guildRank';
import { TowerType } from '@/api/generated/towerType';
import { UserSyncData } from '@/api/generated/userSyncData';
import { CharacterIconManager } from '@/lib/asset-manager';
import { useMasterData } from '@/hooks/useMasterData';
import { SpecialIconItemMB } from '@/api/generated/specialIconItemMB';

// ─── 类型定义 ───────────────────────────────────────────────

interface PlayerRankingData {
    battlePowerList: PlayerInfo[];
    levelList: PlayerInfo[];
    questList: PlayerInfo[];
    towerBattleList: PlayerInfo[];
    battlePowerRanking: number;
    levelRanking: number;
    questRanking: number;
    towerBattleRanking: number;
    towerBattleRankingToday: number;
}

interface GuildRankingData {
    levelList: GuildRank[];
    stockList: GuildRank[];
    battlePowerList: GuildRank[];
    levelRanking: number;
    stockRanking: number;
    battlePowerRanking: number;
}

interface TowerRankingData {
    rankingsMap: { [key: string]: PlayerInfo[] };
    nowRankingMap: { [key: string]: number };
    todayRankingMap: { [key: string]: number };
    maxClearQuestIdMap: { [key: string]: { [key: number]: number } };
}

// ─── 工具函数 ───────────────────────────────────────────────

function getTowerTypeKey(type: TowerType): string {
    switch (type) {
        case TowerType.Infinite: return 'Infinite';
        case TowerType.Blue: return 'Blue';
        case TowerType.Red: return 'Red';
        case TowerType.Green: return 'Green';
        case TowerType.Yellow: return 'Yellow';
        default: return String(type);
    }
}

function getRankColor(rank: number) {
    if (rank === 1) return 'text-yellow-500';
    if (rank === 2) return 'text-gray-400';
    if (rank === 3) return 'text-orange-600';
    return 'text-muted-foreground';
}

function formatLeaderboardRank(value?: number) {
    return value || '-';
}

// ─── PlayerCard ─────────────────────────────────────────────

interface PlayerCardProps {
    player: PlayerInfo;
    rank: number;
    currentUserId?: number;
    onClick: (player: PlayerInfo) => void;
    highlightColor: 'yellow' | 'green' | 'blue' | 'purple';
    valueLabel: string;
    value: string | number;
}

function PlayerCard({ player, rank, currentUserId, onClick, highlightColor, valueLabel, value }: PlayerCardProps) {
    const { t } = useTranslation();

    const getGradientColors = () => {
        switch (highlightColor) {
            case 'yellow': return 'from-amber-400 to-orange-500 border-amber-300 from-yellow-50 to-orange-50 dark:from-yellow-950 dark:to-orange-950';
            case 'green': return 'from-green-400 to-emerald-500 border-green-300 from-green-50 to-emerald-50 dark:from-green-950 dark:to-emerald-950';
            case 'blue': return 'from-blue-400 to-cyan-500 border-blue-300 from-blue-50 to-cyan-50 dark:from-blue-950 dark:to-cyan-950';
            case 'purple': return 'from-purple-400 to-pink-500 border-purple-300 from-purple-50 to-pink-50 dark:from-purple-950 dark:to-pink-950';
            default: return 'from-amber-400 to-orange-500 border-amber-300 from-yellow-50 to-orange-50 dark:from-yellow-950 dark:to-orange-950';
        }
    };

    const getValueColor = () => {
        switch (highlightColor) {
            case 'yellow': return 'text-yellow-600 border-yellow-200/50';
            case 'green': return 'text-green-600 border-green-200/50';
            case 'blue': return 'text-blue-600 border-blue-200/50';
            case 'purple': return 'text-purple-600 border-purple-200/50';
            default: return 'text-yellow-600 border-yellow-200/50';
        }
    };

    const [avatarGradient, borderColor, bgGradientFrom, bgGradientTo, darkBgGradientFrom, darkBgGradientTo] = getGradientColors().split(' ');

    const decodedIcon = CharacterIconManager.decodeIconId(player.mainCharacterIconId);
    const { data: specialIconItem } = useMasterData<SpecialIconItemMB>(
        'SpecialIconItemTable',
        decodedIcon.isSpecialIcon ? decodedIcon.specialIconId : undefined
    );
    const avatarUrl = decodedIcon.isSpecialIcon && specialIconItem
        ? CharacterIconManager.getSpecialIconUrl(specialIconItem.characterId)
        : decodedIcon.characterId > 0
            ? CharacterIconManager.getAvatarUrl(decodedIcon.characterId)
            : null;

    return (
        <div
            className={`flex flex-col p-4 rounded-lg border cursor-pointer transition-all hover:bg-accent ${player.playerId === currentUserId
                ? 'border-2 border-primary bg-primary/5'
                : rank <= 3
                    ? `bg-gradient-to-r ${bgGradientFrom} ${bgGradientTo} dark:${darkBgGradientFrom} dark:${darkBgGradientTo}`
                    : ''
                }`}
            onClick={() => onClick(player)}
        >
            <div className="flex items-center gap-3 mb-3">
                <div className={`text-2xl font-bold ${getRankColor(rank)} w-8 text-center`}>
                    {rank <= 3 ? (
                        <div className="flex items-center justify-center">
                            {rank === 1 && <Crown className="h-6 w-6 fill-current" />}
                            {rank === 2 && <Award className="h-6 w-6 fill-current" />}
                            {rank === 3 && <Star className="h-6 w-6 fill-current" />}
                        </div>
                    ) : `#${rank}`}
                </div>
                {avatarUrl ? (
                    <img
                        src={avatarUrl}
                        alt={player.playerName}
                        className={`w-10 h-10 rounded-lg ${borderColor} border-2 shadow-sm object-cover`}
                        onError={(e) => {
                            const target = e.target as HTMLImageElement;
                            target.style.display = 'none';
                            const parent = target.parentElement;
                            if (parent) {
                                const fallback = document.createElement('div');
                                fallback.className = `w-10 h-10 rounded-lg bg-gradient-to-br ${avatarGradient} flex items-center justify-center text-white font-bold text-base ${borderColor} border-2 shadow-sm`;
                                fallback.textContent = player.playerName.charAt(0);
                                parent.appendChild(fallback);
                            }
                        }}
                    />
                ) : (
                    <div className={`w-10 h-10 rounded-lg bg-gradient-to-br ${avatarGradient} flex items-center justify-center text-white font-bold text-base ${borderColor} border-2 shadow-sm`}>
                        {player.playerName.charAt(0)}
                    </div>
                )}
                <div className="flex-1 min-w-0">
                    <div className="flex items-center gap-1">
                        <span className="font-semibold text-base truncate">{player.playerName}</span>
                        {player.playerId === currentUserId && <Badge className="text-xs">{t('LEADERBOARD_ME_BADGE')}</Badge>}
                    </div>
                </div>
            </div>
            <div className={`flex items-center gap-2 text-sm font-semibold ${getValueColor().split(' ')[0]} border-b ${getValueColor().split(' ')[1]} pb-1 mb-1`}>
                <span className="text-muted-foreground font-normal">{valueLabel}</span>
                <span>{value}</span>
            </div>
            <div className="text-xs text-muted-foreground truncate">
                {player.comment || t('LEADERBOARD_NO_SIGNATURE')}
            </div>
        </div>
    );
}

// ─── GuildCard ───────────────────────────────────────────────

interface GuildCardProps {
    guild: GuildRank;
    rank: number;
    onClick: (guild: GuildRank) => void;
    highlightColor: 'yellow' | 'green' | 'blue' | 'purple' | 'orange';
    valueLabel: string;
    value: string | number;
}

function GuildCard({ guild, rank, onClick, highlightColor, valueLabel, value }: GuildCardProps) {
    const { t } = useTranslation();
    const getGradientClass = () => {
        if (guild.isApplying) return 'border-2 border-primary bg-primary/5';
        if (rank > 3) return '';
        switch (highlightColor) {
            case 'green': return 'bg-gradient-to-r from-green-50 to-emerald-50 dark:from-green-950 dark:to-emerald-950';
            case 'blue': return 'bg-gradient-to-r from-blue-50 to-cyan-50 dark:from-blue-950 dark:to-cyan-950';
            case 'orange': return 'bg-gradient-to-r from-orange-50 to-red-50 dark:from-orange-950 dark:to-red-950';
            default: return '';
        }
    };

    const getValueColor = () => {
        switch (highlightColor) {
            case 'green': return 'text-green-600';
            case 'blue': return 'text-blue-600';
            case 'orange': return 'text-orange-600';
            default: return 'text-primary';
        }
    };

    const getLeaderAvatarUrl = (): string => {
        const iconId = guild.guildInfo?.leaderPlayerInfo?.mainCharacterIconId;
        if (!iconId) return '';
        const { isSpecialIcon, characterId } = CharacterIconManager.decodeIconId(iconId);
        if (isSpecialIcon) return CharacterIconManager.getSpecialIconUrl(characterId);
        return CharacterIconManager.getAvatarUrl(characterId);
    };

    const avatarUrl = getLeaderAvatarUrl();

    return (
        <div
            className={`flex flex-col p-4 rounded-lg border cursor-pointer transition-all hover:bg-accent ${getGradientClass()}`}
            onClick={() => onClick(guild)}
        >
            <div className="flex items-center gap-3 mb-3">
                <div className={`text-2xl font-bold ${getRankColor(rank)} min-w-10 text-center`}>
                    {rank <= 3 ? (
                        <div className="flex items-center justify-center">
                            {rank === 1 && <Crown className="h-7 w-7 fill-current" />}
                            {rank === 2 && <Award className="h-7 w-7 fill-current" />}
                            {rank === 3 && <Star className="h-7 w-7 fill-current" />}
                        </div>
                    ) : `#${rank}`}
                </div>
                {avatarUrl && (
                    <img src={avatarUrl} alt={t('LEADERBOARD_GUILD_LEADER_AVATAR_ALT')} className="w-10 h-10 rounded-full object-cover border" />
                )}
                <div className="flex-1 min-w-0">
                    <div className="flex items-center gap-2">
                        <span className="font-semibold text-base truncate">{guild.guildInfo?.guildOverView?.guildName || t('LEADERBOARD_UNKNOWN_GUILD')}  {t('[CommonCurrentMaxFormatWithBrackets]', [guild.guildInfo.guildMemberCount, 50])}</span>
                        {guild.isApplying && <Badge className="shrink-0">{t('LEADERBOARD_MY_GUILD')}</Badge>}
                    </div>
                </div>
            </div>
            <div className="flex items-center justify-between mt-auto">
                <span className="text-sm text-muted-foreground">{valueLabel}</span>
                <span className={`font-bold ${getValueColor()}`}>{value}</span>
            </div>
            <div className="flex items-center gap-2 mt-2 text-xs text-muted-foreground">
                <span>Lv.{guild.guildInfo?.guildLevel}</span>
                <span>•</span>
                <span>{guild.guildInfo?.guildMemberCount}/50 {t('[Member]')}</span>
            </div>
        </div>
    );
}

// ─── LeaderboardDialog ───────────────────────────────────────

interface LeaderboardDialogProps {
    open: boolean;
    onOpenChange: (open: boolean) => void;
}

export function LeaderboardDialog({ open, onOpenChange }: LeaderboardDialogProps) {
    const { t } = useTranslation();
    const [playerData, setPlayerData] = useState<PlayerRankingData | null>(null);
    const [guildData, setGuildData] = useState<GuildRankingData | null>(null);
    const [towerData, setTowerData] = useState<TowerRankingData | null>(null);
    const [userData, setUserData] = useState<UserSyncData | null>(null);
    const [loading, setLoading] = useState(false);
    const [fetched, setFetched] = useState(false);
    const [playerDialogOpen, setPlayerDialogOpen] = useState(false);
    const [guildDialogOpen, setGuildDialogOpen] = useState(false);
    const [selectedPlayer, setSelectedPlayer] = useState<PlayerInfo | null>(null);
    const [selectedGuild, setSelectedGuild] = useState<GuildRank | null>(null);
    const [mainTab, setMainTab] = useState('player');
    const [playerSubTab, setPlayerSubTab] = useState('player-power');
    const [guildSubTab, setGuildSubTab] = useState('guild-level');

    // 打开时才加载数据（懒加载，只加载一次）
    useEffect(() => {
        if (!open || fetched) return;

        async function fetchRankingData() {
            try {
                setLoading(true);
                const [playerResponse, guildResponse, towerResponse] = await Promise.all([
                    ortegaApi.ranking.getPlayerRanking({}) as Promise<RankingGetPlayerRankingResponse>,
                    ortegaApi.ranking.getGuildRanking({}) as Promise<RankingGetGuildRankingResponse>,
                    ortegaApi.ranking.getTowerRanking({}) as Promise<RankingGetTowerRankingResponse>
                ]);

                if (playerResponse) {
                    setPlayerData({
                        battlePowerList: playerResponse.battlePowerRankingPlayerList || [],
                        levelList: playerResponse.playerRankRankingPlayerList || [],
                        questList: playerResponse.autoRankingPlayerList || [],
                        towerBattleList: playerResponse.towerBattleRankingPlayerList || [],
                        battlePowerRanking: playerResponse.battlePowerRanking || 0,
                        levelRanking: playerResponse.playerRankRanking || 0,
                        questRanking: playerResponse.autoRanking || 0,
                        towerBattleRanking: playerResponse.towerBattleRanking || 0,
                        towerBattleRankingToday: playerResponse.towerBattleRankingToday || 0
                    });
                    if (playerResponse.userSyncData) setUserData(playerResponse.userSyncData);
                }

                if (guildResponse) {
                    setGuildData({
                        levelList: guildResponse.guildLvRankingGuildList || [],
                        stockList: guildResponse.guildStockRankingGuildList || [],
                        battlePowerList: guildResponse.guildBattlePowerRankingGuildList || [],
                        levelRanking: guildResponse.guildLvRanking || 0,
                        stockRanking: guildResponse.guildStockRanking || 0,
                        battlePowerRanking: guildResponse.guildBattlePowerRanking || 0
                    });
                }

                if (towerResponse) {
                    setTowerData({
                        rankingsMap: towerResponse.topPlayerInfoListMap || {},
                        nowRankingMap: towerResponse.nowRankingMap || {},
                        todayRankingMap: towerResponse.todayRankingMap || {},
                        maxClearQuestIdMap: towerResponse.topPlayerMaxClearQuestIdMap || {}
                    });
                    if (!userData && towerResponse.userSyncData) setUserData(towerResponse.userSyncData);
                }

                setFetched(true);
            } catch (error) {
                console.error('Failed to fetch ranking data:', error);
            } finally {
                setLoading(false);
            }
        }

        fetchRankingData();
    }, [open, fetched]);

    const currentUserId = userData?.userStatusDtoInfo?.playerId;

    const getPlayerRankingDescription = (label: string, ranking?: number) => t('LEADERBOARD_PLAYER_RANKING_DESC', [label, formatLeaderboardRank(ranking)]);
    const getGuildRankingDescription = (label: string, ranking?: number) => t('LEADERBOARD_GUILD_RANKING_DESC', [label, formatLeaderboardRank(ranking)]);
    const getTodayRankingSuffix = (ranking?: number) => t('LEADERBOARD_TODAY_RANKING_SUFFIX', [formatLeaderboardRank(ranking)]);
    const getTowerRankingDescription = (ranking?: number, todayRanking?: number) => t('LEADERBOARD_TOWER_RANKING_DESC', [formatLeaderboardRank(ranking), getTodayRankingSuffix(todayRanking)]);

    const handlePlayerClick = (player: PlayerInfo) => {
        setSelectedPlayer(player);
        setPlayerDialogOpen(true);
    };

    const handleGuildClick = (guild: GuildRank) => {
        setSelectedGuild(guild);
        setGuildDialogOpen(true);
    };

    return (
        <>
            <Dialog open={open} onOpenChange={onOpenChange}>
                <DialogContent className="max-w-5xl w-full max-h-[90vh] overflow-y-auto">
                    <DialogHeader>
                        <DialogTitle className="flex items-center gap-2 text-2xl">
                            <Trophy className="h-6 w-6 text-yellow-500" />
                            {t('[RankingMenuTitle]')}
                        </DialogTitle>
                    </DialogHeader>

                    {loading ? (
                        <div className="flex h-48 items-center justify-center">
                            <Loader2 className="h-8 w-8 animate-spin text-primary" />
                            <span className="ml-2">{t('[Loading]')}</span>
                        </div>
                    ) : (
                        <div className="space-y-4">
                            {/* 主 Tabs */}
                            <Tabs value={mainTab} onValueChange={setMainTab} className="space-y-4">
                                <TabsList className="grid w-full grid-cols-2">
                                    <TabsTrigger value="player">{t('[RankingMenuButtonPlayer]')}</TabsTrigger>
                                    <TabsTrigger value="guild">{t('[RankingMenuButtonGuild]')}</TabsTrigger>
                                </TabsList>

                                {/* 玩家排行榜 */}
                                <TabsContent value="player" className="space-y-4">
                                    <Tabs value={playerSubTab} onValueChange={setPlayerSubTab} className="space-y-4">
                                        <TabsList className="grid w-full grid-cols-5">
                                            <TabsTrigger value="player-power">{t('[PlayerRankingTypeBattlePower]')}</TabsTrigger>
                                            <TabsTrigger value="player-level">{t('[PlayerRankingTypePlayerRank]')}</TabsTrigger>
                                            <TabsTrigger value="player-quest">{t('[PlayerRankingTypeStage]')}</TabsTrigger>
                                            <TabsTrigger value="player-tower-infinite">{t('[PlayerRankingTypeTowerBattle]')}</TabsTrigger>
                                            <TabsTrigger value="player-tower">{t('[RankingGroupTypeElementTower]')}</TabsTrigger>
                                        </TabsList>

                                        <TabsContent value="player-power">
                                            <Card>
                                                <CardHeader>
                                                    <CardTitle className="flex items-center gap-2">
                                                        <Trophy className="h-6 w-6 text-yellow-500" />{t('[PlayerRankingTypeBattlePower]')}
                                                    </CardTitle>
                                                    <CardDescription>{getPlayerRankingDescription(t('LEADERBOARD_PLAYER_TOTAL_BATTLE_POWER'), playerData?.battlePowerRanking)}</CardDescription>
                                                </CardHeader>
                                                <CardContent>
                                                    <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
                                                        {playerData?.battlePowerList.map((player, index) => (
                                                            <PlayerCard key={player.playerId || index} player={player} rank={index + 1} currentUserId={currentUserId} onClick={handlePlayerClick} highlightColor="yellow" valueLabel={t('[CommonBattlePowerLabel]')} value={player.battlePower.toLocaleString()} />
                                                        ))}
                                                    </div>
                                                </CardContent>
                                            </Card>
                                        </TabsContent>

                                        <TabsContent value="player-level">
                                            <Card>
                                                <CardHeader>
                                                    <CardTitle className="flex items-center gap-2">
                                                        <TrendingUp className="h-6 w-6 text-green-500" />{t('[PlayerRankingTypePlayerRank]')}
                                                    </CardTitle>
                                                    <CardDescription>{getPlayerRankingDescription(t('LEADERBOARD_PLAYER_ACCOUNT_LEVEL'), playerData?.levelRanking)}</CardDescription>
                                                </CardHeader>
                                                <CardContent>
                                                    <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
                                                        {playerData?.levelList.map((player, index) => (
                                                            <PlayerCard key={player.playerId || index} player={player} rank={index + 1} currentUserId={currentUserId} onClick={handlePlayerClick} highlightColor="green" valueLabel={t('[CommonPlayerRankLabel]')} value={`Lv.${player.playerLevel}`} />
                                                        ))}
                                                    </div>
                                                </CardContent>
                                            </Card>
                                        </TabsContent>

                                        <TabsContent value="player-quest">
                                            <Card>
                                                <CardHeader>
                                                    <CardTitle className="flex items-center gap-2">
                                                        <Zap className="h-6 w-6 text-blue-500" />{t('[PlayerRankingTypeStage]')}
                                                    </CardTitle>
                                                    <CardDescription>{getPlayerRankingDescription(t('LEADERBOARD_PLAYER_MAIN_STAGE_PROGRESS'), playerData?.questRanking)}</CardDescription>
                                                </CardHeader>
                                                <CardContent>
                                                    <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
                                                        {playerData?.questList.map((player, index) => (
                                                            <PlayerCard key={player.playerId || index} player={player} rank={index + 1} currentUserId={currentUserId} onClick={handlePlayerClick} highlightColor="blue" valueLabel={t('[RankingStageLabel]')} value={player.latestQuestId} />
                                                        ))}
                                                    </div>
                                                </CardContent>
                                            </Card>
                                        </TabsContent>

                                        <TabsContent value="player-tower-infinite">
                                            <Card>
                                                <CardHeader>
                                                    <CardTitle className="flex items-center gap-2">
                                                        <MapPin className="h-6 w-6 text-indigo-500" />{t('[PlayerRankingTypeTowerBattle]')}
                                                    </CardTitle>
                                                    <CardDescription>{getTowerRankingDescription(playerData?.towerBattleRanking, playerData?.towerBattleRankingToday)}</CardDescription>
                                                </CardHeader>
                                                <CardContent>
                                                    <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
                                                        {playerData?.towerBattleList.map((player, index) => (
                                                            <PlayerCard key={player.playerId || index} player={player} rank={index + 1} currentUserId={currentUserId} onClick={handlePlayerClick} highlightColor="purple" valueLabel={t('[RankingTowerBattleLabel]')} value={player.latestTowerBattleQuestId} />
                                                        ))}
                                                    </div>
                                                </CardContent>
                                            </Card>
                                        </TabsContent>

                                        <TabsContent value="player-tower">
                                            <Card>
                                                <CardHeader>
                                                    <CardTitle className="flex items-center gap-2">
                                                        <MapPin className="h-6 w-6 text-purple-500" />{t('[RankingGroupTypeElementTower]')}
                                                    </CardTitle>
                                                    <CardDescription>{t('LEADERBOARD_ELEMENT_TOWER_DESC')}</CardDescription>
                                                </CardHeader>
                                                <CardContent>
                                                    <Tabs defaultValue={String(TowerType.Blue)} className="space-y-4">
                                                        <TabsList className="grid w-full grid-cols-4">
                                                            <TabsTrigger value={String(TowerType.Blue)}>{t('[TowerTypeBlue]')}</TabsTrigger>
                                                            <TabsTrigger value={String(TowerType.Red)}>{t('[TowerTypeRed]')}</TabsTrigger>
                                                            <TabsTrigger value={String(TowerType.Green)}>{t('[TowerTypeGreen]')}</TabsTrigger>
                                                            <TabsTrigger value={String(TowerType.Yellow)}>{t('[TowerTypeYellow]')}</TabsTrigger>
                                                        </TabsList>
                                                        {[TowerType.Blue, TowerType.Red, TowerType.Green, TowerType.Yellow].map(towerType => (
                                                            <TabsContent key={towerType} value={String(towerType)}>
                                                                <div className="mb-4 text-sm text-muted-foreground">
                                                                    {getTowerRankingDescription(towerData?.nowRankingMap[getTowerTypeKey(towerType)], towerData?.todayRankingMap[getTowerTypeKey(towerType)])}
                                                                </div>
                                                                <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
                                                                    {towerData?.rankingsMap[getTowerTypeKey(towerType)]?.map((player, index) => (
                                                                        <PlayerCard
                                                                            key={player.playerId || index}
                                                                            player={player}
                                                                            rank={index + 1}
                                                                            currentUserId={currentUserId}
                                                                            onClick={handlePlayerClick}
                                                                            highlightColor="purple"
                                                                            valueLabel={t('[RankingTowerBattleLabel]')}
                                                                            value={towerData?.maxClearQuestIdMap[getTowerTypeKey(towerType)]?.[player.playerId ?? 0] ?? '-'}
                                                                        />
                                                                    ))}
                                                                </div>
                                                            </TabsContent>
                                                        ))}
                                                    </Tabs>
                                                </CardContent>
                                            </Card>
                                        </TabsContent>
                                    </Tabs>
                                </TabsContent>

                                {/* 公会排行榜 */}
                                <TabsContent value="guild" className="space-y-4">
                                    <Tabs value={guildSubTab} onValueChange={setGuildSubTab} className="space-y-4">
                                        <TabsList className="grid w-full grid-cols-3">
                                            <TabsTrigger value="guild-level">{t('[GuildRankingTypeLevel]')}</TabsTrigger>
                                            <TabsTrigger value="guild-stock">{t('[GuildRankingTypeStock]')}</TabsTrigger>
                                            <TabsTrigger value="guild-battle-power">{t('[GuildRankingTypeBattlePower]')}</TabsTrigger>
                                        </TabsList>

                                        <TabsContent value="guild-level">
                                            <Card>
                                                <CardHeader>
                                                    <CardTitle className="flex items-center gap-2">
                                                        <TrendingUp className="h-6 w-6 text-green-500" />{t('[GuildRankingTypeLevel]')}
                                                    </CardTitle>
                                                    <CardDescription>{getGuildRankingDescription(t('[GuildRankingTypeLevel]'), guildData?.levelRanking)}</CardDescription>
                                                </CardHeader>
                                                <CardContent>
                                                    <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
                                                        {guildData?.levelList.map((guild, index) => (
                                                            <GuildCard key={guild.rank} guild={guild} rank={index + 1} onClick={handleGuildClick} highlightColor="green" valueLabel={t('[RankingGuildLevelLabel]')} value={guild.guildInfo.guildLevel.toLocaleString()} />
                                                        ))}
                                                    </div>
                                                </CardContent>
                                            </Card>
                                        </TabsContent>

                                        <TabsContent value="guild-stock">
                                            <Card>
                                                <CardHeader>
                                                    <CardTitle className="flex items-center gap-2">
                                                        <Gem className="h-6 w-6 text-blue-500" />{t('[GuildRankingTypeStock]')}
                                                    </CardTitle>
                                                    <CardDescription>{getGuildRankingDescription(t('LEADERBOARD_GUILD_TODAY_POINTS'), guildData?.stockRanking)}</CardDescription>
                                                </CardHeader>
                                                <CardContent>
                                                    <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
                                                        {guildData?.stockList.map((guild, index) => (
                                                            <GuildCard key={guild.rank} guild={guild} rank={index + 1} onClick={handleGuildClick} highlightColor="blue" valueLabel={t('[RankingGuildStockLabel]')} value={guild.guildStock.toLocaleString()} />
                                                        ))}
                                                    </div>
                                                </CardContent>
                                            </Card>
                                        </TabsContent>

                                        <TabsContent value="guild-battle-power">
                                            <Card>
                                                <CardHeader>
                                                    <CardTitle className="flex items-center gap-2">
                                                        <Shield className="h-6 w-6 text-orange-500" />{t('[GuildRankingTypeBattlePower]')}
                                                    </CardTitle>
                                                    <CardDescription>{getGuildRankingDescription(t('LEADERBOARD_GUILD_TOTAL_BATTLE_POWER'), guildData?.battlePowerRanking)}</CardDescription>
                                                </CardHeader>
                                                <CardContent>
                                                    <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
                                                        {guildData?.battlePowerList.map((guild, index) => (
                                                            <GuildCard key={guild.rank} guild={guild} rank={index + 1} onClick={handleGuildClick} highlightColor="orange" valueLabel={t('[RankingGuildBattlePowerLabel]')} value={guild.battlePower.toLocaleString()} />
                                                        ))}
                                                    </div>
                                                </CardContent>
                                            </Card>
                                        </TabsContent>
                                    </Tabs>
                                </TabsContent>
                            </Tabs>
                        </div>
                    )}
                </DialogContent>
            </Dialog>

            {/* 玩家详情弹窗 */}
            <PlayerInfoDialog
                open={playerDialogOpen}
                onOpenChange={setPlayerDialogOpen}
                player={selectedPlayer}
            />

            {/* 公会详情弹窗 */}
            <Dialog open={guildDialogOpen} onOpenChange={setGuildDialogOpen}>
                <DialogContent>
                    <DialogHeader>
                        <DialogTitle className="flex items-center gap-2">
                            <Shield className="h-5 w-5" />{t('[GuildDetailDialogTitle]')}
                        </DialogTitle>
                    </DialogHeader>
                    {selectedGuild && (
                        <div className="space-y-4">
                            <div className="flex items-center gap-4">
                                <div className={`text-4xl font-bold ${getRankColor(selectedGuild.rank)}`}>#{selectedGuild.rank}</div>
                                <div>
                                    <div className="text-2xl font-bold">{selectedGuild.guildInfo?.guildOverView?.guildName || t('LEADERBOARD_UNKNOWN_GUILD')}</div>
                                    <div className="text-sm text-muted-foreground">Lv.{selectedGuild.guildInfo?.guildLevel}</div>
                                </div>
                            </div>
                            <div className="grid gap-3">
                                <div className="flex justify-between items-center p-3 bg-muted rounded-lg">
                                    <span className="text-sm text-muted-foreground">{t('[CommonTotalBattlePowerLabel]')}</span>
                                    <span className="font-semibold">{selectedGuild.battlePower.toLocaleString()}</span>
                                </div>
                                <div className="flex justify-between items-center p-3 bg-muted rounded-lg">
                                    <span className="text-sm text-muted-foreground">{t('[GuildStockRankingLabel]')}</span>
                                    <span className="font-semibold">{selectedGuild.guildStock.toLocaleString()}</span>
                                </div>
                                <div className="flex justify-between items-center p-3 bg-muted rounded-lg">
                                    <span className="text-sm text-muted-foreground">{t('[MemberNumber]')}</span>
                                    <span className="font-semibold">{selectedGuild.guildInfo?.guildMemberCount}/50</span>
                                </div>
                                <div className="flex justify-between items-center p-3 bg-muted rounded-lg">
                                    <span className="text-sm text-muted-foreground">{t('[GuildFameLabel]')}</span>
                                    <span className="font-semibold">{selectedGuild.guildInfo?.guildFame.toLocaleString()}</span>
                                </div>
                                {selectedGuild.guildTowerMaxFloor > 0 && (
                                    <div className="flex justify-between items-center p-3 bg-muted rounded-lg">
                                        <span className="text-sm text-muted-foreground">{t('[GuildTowerMaxFloorLabel]')}</span>
                                        <span className="font-semibold">{selectedGuild.guildTowerMaxFloor}</span>
                                    </div>
                                )}
                                {selectedGuild.guildInfo?.guildOverView?.guildDescription && (
                                    <div className="p-3 bg-muted rounded-lg">
                                        <div className="text-sm text-muted-foreground mb-1">{t('[GuildDescriptionLabel]')}</div>
                                        <div className="text-sm">{selectedGuild.guildInfo.guildOverView.guildDescription}</div>
                                    </div>
                                )}
                            </div>
                        </div>
                    )}
                </DialogContent>
            </Dialog>
        </>
    );
}
