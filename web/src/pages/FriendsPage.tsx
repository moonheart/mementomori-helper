import { useState, useEffect, useCallback } from 'react';
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from '@/components/ui/card';
import { Badge } from '@/components/ui/badge';
import { Button } from '@/components/ui/button';
import { Input } from '@/components/ui/input';
import { Tabs, TabsContent, TabsList, TabsTrigger } from '@/components/ui/tabs';
import {
    Users,
    UserPlus,
    Heart,
    Gift,
    Search,
    Check,
    X,
    UserX,
    RefreshCw,
    Shield,
    Loader2,
    MessageCircle
} from 'lucide-react';
import { ortegaApi } from '@/api/ortega-client';
import { toast } from '@/hooks/use-toast';
import { FriendInfoType } from '@/api/generated/friendInfoType';
import { LanguageType } from '@/api/generated/languageType';
import { PlayerInfo } from '@/api/generated/playerInfo';
import { useTimeManager } from '@/hooks/useTimeManager';
import { useTranslation } from '@/hooks/useTranslation';
import { useLocalizationStore } from '@/store/localization-store';
import { AssetManager } from '@/lib/asset-manager';
import { parseOrtegaError } from '@/lib/errorHandler';
import { PlayerInfoDialog } from '@/components/player/PlayerInfoDialog';

// 玩家信息扩展类型
interface FriendPlayerInfo extends PlayerInfo {
    giftSent?: boolean;
    giftReceived?: boolean;
    isNewFriend?: boolean;
}

// 好友统计数据
interface FriendStats {
    friendCount: number;
    maxFriends: number;
    blockCount: number;
    maxBlocks: number;
    sentCount: number;
    maxSend: number;
    receivedCount: number;
    maxReceive: number;
    canReceiveCount: number;
}

export function FriendsPage() {
    const timeManager = useTimeManager();
    const { t } = useTranslation();
    const currentLanguage = useLocalizationStore(state => state.currentLanguage);
    const [activeTab, setActiveTab] = useState('friends');
    const [searchId, setSearchId] = useState('');
    const [searching, setSearching] = useState(false);
    const [searchedPlayer, setSearchedPlayer] = useState<PlayerInfo | null>(null);
    const [actionLoading, setActionLoading] = useState<number | string | null>(null);
    const [playerDialogOpen, setPlayerDialogOpen] = useState(false);
    const [playerDialogTarget, setPlayerDialogTarget] = useState<PlayerInfo | null>(null);

    const getLanguageType = useCallback(() => LanguageType[currentLanguage as keyof typeof LanguageType] ?? LanguageType.zhCN, [currentLanguage]);

    // 各类玩家列表
    const [friends, setFriends] = useState<FriendPlayerInfo[]>([]);
    const [pendingRequests, setPendingRequests] = useState<FriendPlayerInfo[]>([]);
    const [sentRequests, setSentRequests] = useState<FriendPlayerInfo[]>([]);
    const [recommendations, setRecommendations] = useState<FriendPlayerInfo[]>([]);
    const [blockedPlayers, setBlockedPlayers] = useState<FriendPlayerInfo[]>([]);

    // 各tab加载状态
    const [loadingTabs, setLoadingTabs] = useState<Record<string, boolean>>({
        friends: false,
        requests: false,
        applying: false,
        blocked: false
    });

    // 统计数据
    const [stats, setStats] = useState<FriendStats>({
        friendCount: 0,
        maxFriends: 50,
        blockCount: 0,
        maxBlocks: 100,
        sentCount: 0,
        maxSend: 0,
        receivedCount: 0,
        maxReceive: 20,
        canReceiveCount: 0
    });

    // 格式化上次登录时间
    const formatLastLogin = (timestamp: number): string => {
        if (!timestamp) return t('[CharacterBloodTypeNone]');
        const now = timeManager.getServerNowMs();
        const diff = now - timestamp;
        const minutes = Math.floor(diff / 60000);
        const hours = Math.floor(diff / 3600000);
        const days = Math.floor(diff / 86400000);

        if (diff < 60000) return t('FRIENDS_JUST_NOW');
        if (minutes < 60) return t('FRIENDS_MINUTES_AGO', [minutes]);
        if (hours < 24) return t('FRIENDS_HOURS_AGO', [hours]);
        if (days < 30) return t('FRIENDS_DAYS_AGO', [days]);
        return t('FRIENDS_LONG_AGO');
    };

    const getLoadingText = () => t('[Loading]');
    const getGuildNameText = (guildName?: string | null) => guildName ? `「${guildName}」` : t('[RankingNotGuild]');
    const getPlayerNameText = (player: PlayerInfo) => player.playerName || player.npcNameKey || t('FRIENDS_UNKNOWN_PLAYER');
    const getRetryLaterText = () => t('FRIENDS_RETRY_LATER');

    // 加载好友列表数据
    const loadFriendsData = useCallback(async () => {
        setLoadingTabs(prev => ({ ...prev, friends: true }));
        try {
            const friendRes = await ortegaApi.friend.getPlayerInfoList({
                friendInfoType: FriendInfoType.Friend,
                languageType: getLanguageType()
            });

            const processedFriends = (friendRes.playerInfoList || []).map(f => {
                const canSend = friendRes.canSendFriendPointPlayerIdList?.includes(f.playerId) ?? false;
                const canReceive = friendRes.canReceiveFriendPointPlayerIdList?.includes(f.playerId) ?? false;
                return {
                    ...f,
                    giftSent: !canSend,
                    giftReceived: canReceive
                };
            });

            setFriends(processedFriends);
            setStats(prev => ({
                ...prev,
                friendCount: friendRes.friendNum || processedFriends.length,
                sentCount: processedFriends.filter(f => f.giftSent).length,
                maxSend: processedFriends.length,
                receivedCount: friendRes.receivedFriendPointCount || 0,
                canReceiveCount: friendRes.canReceiveFriendPointPlayerIdList?.length || 0
            }));
        } catch (error) {
            console.error('Failed to load friends:', error);
            toast({ title: t('FRIENDS_LOAD_FRIENDS_FAILED'), variant: 'destructive' });
        } finally {
            setLoadingTabs(prev => ({ ...prev, friends: false }));
        }
    }, []);

    // 加载待同意申请
    const loadPendingRequests = useCallback(async () => {
        setLoadingTabs(prev => ({ ...prev, requests: true }));
        try {
            const pendingRes = await ortegaApi.friend.getPlayerInfoList({
                friendInfoType: FriendInfoType.ApprovalPending,
                languageType: getLanguageType()
            });
            setPendingRequests(pendingRes.playerInfoList || []);
        } catch (error) {
            console.error('Failed to load pending requests:', error);
            toast({ title: t('FRIENDS_LOAD_REQUESTS_FAILED'), variant: 'destructive' });
        } finally {
            setLoadingTabs(prev => ({ ...prev, requests: false }));
        }
    }, []);

    // 加载已发出申请
    const loadSentRequests = useCallback(async () => {
        setLoadingTabs(prev => ({ ...prev, applying: true }));
        try {
            const [applyingRes, recommendRes] = await Promise.all([
                ortegaApi.friend.getPlayerInfoList({
                    friendInfoType: FriendInfoType.Applying,
                    languageType: getLanguageType()
                }),
                ortegaApi.friend.getPlayerInfoList({
                    friendInfoType: FriendInfoType.Recommend,
                    languageType: getLanguageType()
                })
            ]);
            setSentRequests(applyingRes.playerInfoList || []);
            setRecommendations(recommendRes.playerInfoList || []);
        } catch (error) {
            console.error('Failed to load sent requests:', error);
            toast({ title: t('FRIENDS_LOAD_REQUESTS_FAILED'), variant: 'destructive' });
        } finally {
            setLoadingTabs(prev => ({ ...prev, applying: false }));
        }
    }, []);

    // 加载屏蔽列表
    const loadBlockedPlayers = useCallback(async () => {
        setLoadingTabs(prev => ({ ...prev, blocked: true }));
        try {
            const blockRes = await ortegaApi.friend.getPlayerInfoList({
                friendInfoType: FriendInfoType.Block,
                languageType: getLanguageType()
            });
            setBlockedPlayers(blockRes.playerInfoList || []);
            setStats(prev => ({
                ...prev,
                blockCount: blockRes.currentTypePlayerNum || (blockRes.playerInfoList?.length ?? 0)
            }));
        } catch (error) {
            console.error('Failed to load blocked players:', error);
            toast({ title: t('FRIENDS_LOAD_BLOCKED_FAILED'), variant: 'destructive' });
        } finally {
            setLoadingTabs(prev => ({ ...prev, blocked: false }));
        }
    }, []);

    // Tab切换时加载对应数据
    useEffect(() => {
        switch (activeTab) {
            case 'friends':
                loadFriendsData();
                break;
            case 'requests':
                loadPendingRequests();
                break;
            case 'applying':
                loadSentRequests();
                break;
            case 'blocked':
                loadBlockedPlayers();
                break;
        }
    }, [activeTab, loadFriendsData, loadPendingRequests, loadSentRequests, loadBlockedPlayers]);

    // 搜索好友
    const handleSearch = async () => {
        if (!searchId.trim()) {
            toast({ title: t('[ClientErrorMessage300009]'), variant: 'destructive' });
            return;
        }

        const playerId = parseInt(searchId.trim());
        if (isNaN(playerId) || playerId <= 0) {
            toast({ title: t('FRIENDS_ENTER_VALID_PLAYER_ID'), variant: 'destructive' });
            return;
        }

        setSearching(true);
        try {
            const res = await ortegaApi.friend.searchFriend({ searchPlayerId: playerId });
            if (res.playerInfo) {
                setSearchedPlayer(res.playerInfo);
            } else {
                toast({ title: t('FRIENDS_PLAYER_NOT_FOUND') });
                setSearchedPlayer(null);
            }
        } catch (error) {
            console.error('Search failed:', error);
            toast({ title: t('FRIENDS_SEARCH_FAILED'), description: t('FRIENDS_CHECK_PLAYER_ID'), variant: 'destructive' });
            setSearchedPlayer(null);
        } finally {
            setSearching(false);
        }
    };

    // 申请添加好友
    const handleApplyFriend = async (targetPlayerId: number) => {
        setActionLoading(targetPlayerId);
        try {
            await ortegaApi.friend.applyFriend({ targetPlayerId, isApply: true });
            toast({ title: t('FRIENDS_APPLY_SENT') });
            // 如果是搜索结果，清空搜索
            if (searchedPlayer?.playerId === targetPlayerId) {
                setSearchedPlayer(null);
                setSearchId('');
            }
            // 刷新当前tab数据
            refreshCurrentTab();
        } catch (error) {
            console.error('Apply friend failed:', error);
            toast({ title: t('FRIENDS_APPLY_FAILED'), description: getRetryLaterText(), variant: 'destructive' });
        } finally {
            setActionLoading(null);
        }
    };

    // 批量申请推荐好友
    const handleBulkApply = async () => {
        if (recommendations.length === 0) return;

        setActionLoading('bulk-apply');
        try {
            await ortegaApi.friend.bulkApplyFriends({
                targetPlayerIdList: recommendations.map(r => r.playerId)
            });
            toast({ title: t('FRIENDS_BULK_APPLY_SENT') });
            loadSentRequests();
        } catch (error) {
            console.error('Bulk apply failed:', error);
            toast({ title: t('FRIENDS_BULK_APPLY_FAILED'), description: getRetryLaterText(), variant: 'destructive' });
        } finally {
            setActionLoading(null);
        }
    };

    // 回复好友申请
    const handleReplyFriend = async (targetPlayerId: number, isApproval: boolean) => {
        setActionLoading(`${isApproval ? 'approve' : 'reject'}-${targetPlayerId}`);
        try {
            await ortegaApi.friend.replyFriend({ targetPlayerId, isApproval });
            toast({ title: isApproval ? t('FRIENDS_APPROVED_REQUEST') : t('FRIENDS_REJECTED_REQUEST') });
            loadPendingRequests();
        } catch (error) {
            console.error('Reply friend failed:', error);
            const { message } = parseOrtegaError(error);
            toast({ title: t('FRIENDS_ACTION_FAILED'), description: message, variant: 'destructive' });
        } finally {
            setActionLoading(null);
        }
    };

    // 批量回复好友申请
    const handleReplyAll = async (isApproval: boolean) => {
        if (pendingRequests.length === 0) return;

        setActionLoading(isApproval ? 'approve-all' : 'reject-all');
        try {
            await ortegaApi.friend.replyAllFriend({ isApproval });
            toast({ title: isApproval ? t('FRIENDS_APPROVED_ALL') : t('FRIENDS_REJECTED_ALL') });
            loadPendingRequests();
        } catch (error) {
            console.error('Reply all failed:', error);
            toast({ title: t('FRIENDS_ACTION_FAILED'), description: getRetryLaterText(), variant: 'destructive' });
        } finally {
            setActionLoading(null);
        }
    };

    // 删除好友
    const handleRemoveFriend = async (targetPlayerId: number) => {
        setActionLoading(`remove-${targetPlayerId}`);
        try {
            await ortegaApi.friend.removeFriend({ targetPlayerId });
            toast({ title: t('FRIENDS_REMOVED_FRIEND') });
            loadFriendsData();
        } catch (error) {
            console.error('Remove friend failed:', error);
            toast({ title: t('FRIENDS_REMOVE_FAILED'), description: getRetryLaterText(), variant: 'destructive' });
        } finally {
            setActionLoading(null);
        }
    };

    // 取消所有申请
    const handleCancelAllApply = async () => {
        if (sentRequests.length === 0) return;

        setActionLoading('cancel-all');
        try {
            await ortegaApi.friend.cancelAllApplyFriend({});
            toast({ title: t('FRIENDS_CANCELLED_ALL_APPLIES') });
            loadSentRequests();
        } catch (error) {
            console.error('Cancel all failed:', error);
            toast({ title: t('FRIENDS_ACTION_FAILED'), description: getRetryLaterText(), variant: 'destructive' });
        } finally {
            setActionLoading(null);
        }
    };

    // 赠送友情点数
    const handleSendFriendPoint = async (targetPlayerId: number) => {
        setActionLoading(`send-${targetPlayerId}`);
        try {
            await ortegaApi.friend.sendFriendPoint({ targetPlayerId });
            toast({ title: t('FRIENDS_SENT_FRIEND_POINTS') });
            loadFriendsData();
        } catch (error) {
            console.error('Send friend point failed:', error);
            toast({ title: t('FRIENDS_SEND_FAILED'), description: getRetryLaterText(), variant: 'destructive' });
        } finally {
            setActionLoading(null);
        }
    };

    // 批量赠送/领取友情点数
    const handleBulkTransferPoints = async (isReceive: boolean) => {
        setActionLoading(isReceive ? 'bulk-receive' : 'bulk-send');
        try {
            await ortegaApi.friend.bulkTransferFriendPoint({ isReceive });
            toast({ title: isReceive ? t('FRIENDS_RECEIVED_ALL_POINTS') : t('FRIENDS_SENT_ALL_POINTS') });
            loadFriendsData();
        } catch (error) {
            console.error('Bulk transfer failed:', error);
            toast({ title: t('FRIENDS_ACTION_FAILED'), description: getRetryLaterText(), variant: 'destructive' });
        } finally {
            setActionLoading(null);
        }
    };

    // 领取单个好友的友情点数
    const handleReceiveFriendPoint = async (sendPlayerId: number) => {
        setActionLoading(`receive-${sendPlayerId}`);
        try {
            await ortegaApi.friend.receiveFriendPoint({ sendPlayerId });
            toast({ title: t('FRIENDS_RECEIVED_FRIEND_POINTS') });
            loadFriendsData();
        } catch (error) {
            console.error('Receive friend point failed:', error);
            toast({ title: t('FRIENDS_RECEIVE_FAILED'), description: getRetryLaterText(), variant: 'destructive' });
        } finally {
            setActionLoading(null);
        }
    };

    // 屏蔽/解除屏蔽玩家
    const handleUpdateBlock = async (targetPlayerId: number, isBlock: boolean) => {
        setActionLoading(`${isBlock ? 'block' : 'unblock'}-${targetPlayerId}`);
        try {
            await ortegaApi.friend.updateBlockList({ targetPlayerId, isBlock });
            toast({ title: isBlock ? t('FRIENDS_BLOCKED_PLAYER') : t('FRIENDS_UNBLOCKED_PLAYER') });
            loadBlockedPlayers();
        } catch (error) {
            console.error('Update block list failed:', error);
            toast({ title: t('FRIENDS_ACTION_FAILED'), description: getRetryLaterText(), variant: 'destructive' });
        } finally {
            setActionLoading(null);
        }
    };

    // 玩家头像组件
    const PlayerAvatar = ({ player, size = 'md' }: { player: PlayerInfo; size?: 'sm' | 'md' | 'lg' }) => {
        const sizeClasses = {
            sm: 'w-14 h-14',
            md: 'w-20 h-20',
            lg: 'w-24 h-24'
        };

        const avatarUrl = player.mainCharacterIconId > 0
            ? AssetManager.character.getAvatarUrl(player.mainCharacterIconId)
            : null;

        if (avatarUrl) {
            return (
                <img
                    src={avatarUrl}
                    alt={player.playerName || 'Player'}
                    className={`rounded-lg object-cover ${sizeClasses[size]}`}
                    onError={(e) => {
                        // 图片加载失败时显示默认头像
                        (e.target as HTMLImageElement).style.display = 'none';
                        const parent = (e.target as HTMLImageElement).parentElement;
                        if (parent) {
                            const fallback = document.createElement('div');
                            fallback.className = `flex items-center justify-center rounded-lg bg-gradient-to-br from-blue-500 to-purple-500 text-white font-bold ${sizeClasses[size]}`;
                            fallback.textContent = (player.playerName || player.npcNameKey || '?').charAt(0).toUpperCase();
                            parent.appendChild(fallback);
                        }
                    }}
                />
            );
        }

        const name = player.playerName || player.npcNameKey || '?';
        const initial = name.charAt(0).toUpperCase();

        return (
            <div className={`flex items-center justify-center rounded-lg bg-gradient-to-br from-blue-500 to-purple-500 text-white font-bold ${sizeClasses[size]}`}>
                {initial}
            </div>
        );
    };

    // 通用玩家卡片组件
    interface FriendCardProps {
        player: PlayerInfo;
        topLeft?: React.ReactNode;
        topRight?: React.ReactNode;
        actions?: React.ReactNode;
        cardClassName?: string;
    }

    const FriendCard = ({ player, topLeft, topRight, actions, cardClassName = '' }: FriendCardProps) => (
        <Card
            className={`hover:shadow-lg transition-shadow overflow-hidden ${cardClassName}`}
            onClick={() => {
                setPlayerDialogTarget(player);
                setPlayerDialogOpen(true);
            }}
        >
            {/* 顶部栏 */}
            <div className="flex items-center justify-between px-4 py-2 bg-muted/50 border-b">
                <span className="text-xs text-muted-foreground truncate max-w-[60%]">
                    {topLeft || (getGuildNameText(player.guildName))}
                </span>
                <span className="text-xs text-muted-foreground">{topRight}</span>
            </div>

            <CardContent className="p-4">
                <div className="flex items-start gap-4">
                    {/* 左侧头像 */}
                    <div className="shrink-0">
                        <PlayerAvatar player={player} size="md" />
                    </div>

                    {/* 中间信息 */}
                    <div className="flex-1 min-w-0">
                        <div className="font-semibold text-base mb-2 truncate">
                            {getPlayerNameText(player)}
                        </div>
                        <div className="flex items-center gap-2 mb-1.5">
                            <span className="text-xs text-muted-foreground w-10 shrink-0">{t('[CommonPlayerRankLabel]')}</span>
                            <div className="flex-1 bg-muted rounded-full h-5 flex items-center px-2">
                                <span className="text-xs font-medium">{player.playerLevel}</span>
                            </div>
                        </div>
                        <div className="flex items-center gap-2">
                            <span className="text-xs text-muted-foreground w-10 shrink-0">{t('[CommonBattlePowerLabel]')}</span>
                            <div className="flex-1 bg-muted rounded-full h-5 flex items-center px-2">
                                <span className="text-xs font-medium">{player.battlePower.toLocaleString()}</span>
                            </div>
                        </div>
                    </div>

                    {/* 右侧操作按钮 */}
                    {actions && (
                        <div
                            className="shrink-0 flex flex-col gap-2"
                            onClick={(event) => event.stopPropagation()}
                        >
                            {actions}
                        </div>
                    )}
                </div>

                {/* 底部个性签名 */}
                {player.comment && (
                    <div className="mt-3 pt-3 border-t text-xs text-muted-foreground truncate">
                        {player.comment}
                    </div>
                )}
            </CardContent>
        </Card>
    );

    // 当前tab的加载状态
    const isTabLoading = loadingTabs[activeTab] ?? false;

    // 刷新当前tab数据
    const refreshCurrentTab = () => {
        switch (activeTab) {
            case 'friends':
                loadFriendsData();
                break;
            case 'requests':
                loadPendingRequests();
                break;
            case 'applying':
                loadSentRequests();
                break;
            case 'blocked':
                loadBlockedPlayers();
                break;
        }
    };

    return (
        <div className="space-y-6">
            <PlayerInfoDialog
                open={playerDialogOpen}
                onOpenChange={setPlayerDialogOpen}
                player={playerDialogTarget}
            />
            {/* 统计卡片 */}
            <div className="grid gap-6 md:grid-cols-3">
                <Card>
                    <CardHeader>
                        <CardTitle className="flex items-center gap-2 text-base">
                            <Users className="h-5 w-5 text-blue-500" />
                            {t('[FriendNum]')}
                        </CardTitle>
                    </CardHeader>
                    <CardContent>
                        <div className="text-center">
                            <div className="text-3xl font-bold text-blue-600">
                                {stats.friendCount} / {stats.maxFriends}
                            </div>
                            <div className="text-sm text-muted-foreground mt-1">
                                {t('FRIENDS_CURRENT_FRIEND_COUNT')}
                            </div>
                        </div>
                    </CardContent>
                </Card>

                <Card>
                    <CardHeader>
                        <CardTitle className="flex items-center gap-2 text-base">
                            <Gift className="h-5 w-5 text-pink-500" />
                            {t('FRIENDS_SENT_TODAY')}
                        </CardTitle>
                    </CardHeader>
                    <CardContent>
                        <div className="text-center">
                            <div className="text-3xl font-bold text-pink-600">
                                {stats.sentCount} / {stats.maxSend}
                            </div>
                            <div className="text-sm text-muted-foreground mt-1">
                                {t('FRIENDS_SENT_POINTS_COUNT')}
                            </div>
                        </div>
                    </CardContent>
                </Card>

                <Card>
                    <CardHeader>
                        <CardTitle className="flex items-center gap-2 text-base">
                            <Heart className="h-5 w-5 text-red-500" />
                            {t('FRIENDS_PENDING_RECEIVE')}
                        </CardTitle>
                    </CardHeader>
                    <CardContent>
                        <div className="space-y-3">
                            <div className="text-center">
                                <div className="text-3xl font-bold text-red-600">
                                    {stats.canReceiveCount}
                                </div>
                                <div className="text-sm text-muted-foreground mt-1">
                                    {t('FRIENDS_RECEIVABLE_POINTS_COUNT', [stats.receivedCount, stats.maxReceive])}
                                </div>
                            </div>
                            {stats.canReceiveCount > 0 && (
                                <Button
                                    className="w-full"
                                    size="sm"
                                    onClick={() => handleBulkTransferPoints(true)}
                                    disabled={actionLoading === 'bulk-receive'}
                                >
                                    {actionLoading === 'bulk-receive' ? (
                                        <Loader2 className="mr-2 h-4 w-4 animate-spin" />
                                    ) : (
                                        <Heart className="mr-2 h-4 w-4" />
                                    )}
                                    {t('FRIENDS_RECEIVE_ALL')}
                                </Button>
                            )}
                        </div>
                    </CardContent>
                </Card>
            </div>

            <Tabs value={activeTab} onValueChange={setActiveTab} className="space-y-6">
                <TabsList className="grid w-full grid-cols-5">
                    <TabsTrigger value="friends">
                        {t('[FriendTabFriend]')} ({stats.friendCount})
                    </TabsTrigger>
                    <TabsTrigger value="requests">
                        {t('[FriendTabApprovalPending]')} ({pendingRequests.length})
                    </TabsTrigger>
                    <TabsTrigger value="applying">
                        {t('[FriendTabApplying]')} ({sentRequests.length})
                    </TabsTrigger>
                    <TabsTrigger value="search">
                        {t('[FriendTabSearch]')}
                    </TabsTrigger>
                    <TabsTrigger value="blocked">
                        {t('[FriendTabBlock]')} ({stats.blockCount})
                    </TabsTrigger>
                </TabsList>

                {/* 好友列表 */}
                <TabsContent value="friends" className="space-y-4">
                    <div className="flex items-center justify-between">
                        <div className="text-sm text-muted-foreground">
                            {t('FRIENDS_TOTAL_FRIENDS', [friends.length])}
                        </div>
                        <div className="flex gap-2">
                            <Button
                                variant="outline"
                                size="sm"
                                onClick={refreshCurrentTab}
                                disabled={isTabLoading}
                            >
                                {isTabLoading ? (
                                    <Loader2 className="mr-2 h-4 w-4 animate-spin" />
                                ) : (
                                    <RefreshCw className="mr-2 h-4 w-4" />
                                )}
                                {t('[CommonReloadButtonLabel]')}
                            </Button>
                            {friends.some(f => !f.giftSent) && (
                                <Button
                                    variant="outline"
                                    size="sm"
                                    onClick={() => handleBulkTransferPoints(false)}
                                    disabled={actionLoading === 'bulk-send'}
                                >
                                    {actionLoading === 'bulk-send' ? (
                                        <Loader2 className="mr-2 h-4 w-4 animate-spin" />
                                    ) : (
                                        <Gift className="mr-2 h-4 w-4" />
                                    )}
                                    {t('FRIENDS_SEND_ALL')}
                                </Button>
                            )}
                        </div>
                    </div>

                    {isTabLoading ? (
                        <div className="flex items-center justify-center py-20">
                            <Loader2 className="h-10 w-10 animate-spin text-primary" />
                            <span className="ml-3 text-muted-foreground">{getLoadingText()}</span>
                        </div>
                    ) : friends.length > 0 ? (
                        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-4">
                            {friends.map((friend) => (
                                <FriendCard
                                    key={friend.playerId}
                                    player={friend}
                                    topRight={formatLastLogin(friend.lastLoginTime)}
                                    actions={
                                        <>
                                            {/* 赠送/领取 合一按钮 */}
                                            {friend.giftReceived ? (
                                                <Button
                                                    size="icon"
                                                    className="h-10 w-10 rounded-full bg-gradient-to-br from-pink-400 to-rose-500 hover:from-pink-500 hover:to-rose-600 shadow-md"
                                                    onClick={() => handleReceiveFriendPoint(friend.playerId)}
                                                    disabled={actionLoading === `receive-${friend.playerId}`}
                                                >
                                                    {actionLoading === `receive-${friend.playerId}` ? (
                                                        <Loader2 className="h-5 w-5 animate-spin" />
                                                    ) : (
                                                        <Heart className="h-5 w-5 fill-white" />
                                                    )}
                                                </Button>
                                            ) : !friend.giftSent ? (
                                                <Button
                                                    size="icon"
                                                    variant="outline"
                                                    className="h-10 w-10 rounded-full border-2 border-dashed border-pink-300 hover:border-pink-400 hover:bg-pink-50"
                                                    onClick={() => handleSendFriendPoint(friend.playerId)}
                                                    disabled={actionLoading === `send-${friend.playerId}`}
                                                >
                                                    {actionLoading === `send-${friend.playerId}` ? (
                                                        <Loader2 className="h-5 w-5 animate-spin" />
                                                    ) : (
                                                        <Gift className="h-5 w-5 text-pink-400" />
                                                    )}
                                                </Button>
                                            ) : (
                                                <div className="h-10 w-10 rounded-full bg-green-100 flex items-center justify-center">
                                                    <Check className="h-5 w-5 text-green-600" />
                                                </div>
                                            )}
                                            {/* 删除按钮 */}
                                            <Button
                                                size="icon"
                                                variant="ghost"
                                                className="h-8 w-8 text-muted-foreground hover:text-red-500"
                                                onClick={() => handleRemoveFriend(friend.playerId)}
                                                disabled={actionLoading === `remove-${friend.playerId}`}
                                            >
                                                {actionLoading === `remove-${friend.playerId}` ? (
                                                    <Loader2 className="h-4 w-4 animate-spin" />
                                                ) : (
                                                    <UserX className="h-4 w-4" />
                                                )}
                                            </Button>
                                        </>
                                    }
                                />
                            ))}
                        </div>
                    ) : (
                        <Card className="border-dashed">
                            <CardContent className="flex flex-col items-center justify-center py-16 text-center">
                                <Users className="h-16 w-16 mb-4 text-muted-foreground opacity-20" />
                                <h3 className="text-lg font-semibold mb-2">{t('[FriendListEmptyMessageFriend]')}</h3>
                                <p className="text-muted-foreground text-sm mb-4">
                                    {t('FRIENDS_EMPTY_FRIENDS_DESC')}
                                </p>
                                <Button onClick={() => setActiveTab('search')}>
                                    <UserPlus className="mr-2 h-4 w-4" />
                                    {t('FRIENDS_GO_ADD_FRIEND')}
                                </Button>
                            </CardContent>
                        </Card>
                    )}
                </TabsContent>

                {/* 待同意列表 */}
                <TabsContent value="requests" className="space-y-4">
                    {loadingTabs.requests ? (
                        <div className="flex items-center justify-center py-20">
                            <Loader2 className="h-10 w-10 animate-spin text-primary" />
                            <span className="ml-3 text-muted-foreground">{getLoadingText()}</span>
                        </div>
                    ) : pendingRequests.length > 0 ? (
                        <>
                            <div className="flex items-center justify-between">
                                <div className="text-sm text-muted-foreground">
                                    {t('FRIENDS_PENDING_REQUESTS_COUNT', [pendingRequests.length])}
                                </div>
                                <div className="flex gap-2">
                                    <Button
                                        variant="outline"
                                        size="sm"
                                        onClick={() => handleReplyAll(true)}
                                        disabled={actionLoading === 'approve-all'}
                                    >
                                        {actionLoading === 'approve-all' ? (
                                            <Loader2 className="mr-2 h-4 w-4 animate-spin" />
                                        ) : (
                                            <Check className="mr-2 h-4 w-4" />
                                        )}
                                        {t('[BulkApproval]')}
                                    </Button>
                                    <Button
                                        variant="outline"
                                        size="sm"
                                        onClick={() => handleReplyAll(false)}
                                        disabled={actionLoading === 'reject-all'}
                                    >
                                        {actionLoading === 'reject-all' ? (
                                            <Loader2 className="mr-2 h-4 w-4 animate-spin" />
                                        ) : (
                                            <X className="mr-2 h-4 w-4" />
                                        )}
                                        {t('[MassRejection]')}
                                    </Button>
                                </div>
                            </div>

                            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-4">
                                {pendingRequests.map((request) => (
                                    <FriendCard
                                        key={request.playerId}
                                        player={request}
                                        topRight={t('[FriendTabApplying]')}
                                        actions={
                                            <>
                                                <Button
                                                    size="icon"
                                                    className="h-10 w-10 rounded-full bg-green-600 hover:bg-green-700"
                                                    onClick={() => handleReplyFriend(request.playerId, true)}
                                                    disabled={actionLoading === `approve-${request.playerId}`}
                                                >
                                                    {actionLoading === `approve-${request.playerId}` ? (
                                                        <Loader2 className="h-5 w-5 animate-spin" />
                                                    ) : (
                                                        <Check className="h-5 w-5" />
                                                    )}
                                                </Button>
                                                <Button
                                                    size="icon"
                                                    variant="outline"
                                                    className="h-10 w-10 rounded-full"
                                                    onClick={() => handleReplyFriend(request.playerId, false)}
                                                    disabled={actionLoading === `reject-${request.playerId}`}
                                                >
                                                    {actionLoading === `reject-${request.playerId}` ? (
                                                        <Loader2 className="h-5 w-5 animate-spin" />
                                                    ) : (
                                                        <X className="h-5 w-5" />
                                                    )}
                                                </Button>
                                            </>
                                        }
                                    />
                                ))}
                            </div>
                        </>
                    ) : (
                        <Card className="border-dashed">
                            <CardContent className="flex flex-col items-center justify-center py-16 text-center">
                                <Users className="h-16 w-16 mb-4 text-muted-foreground opacity-20" />
                                <h3 className="text-lg font-semibold mb-2">{t('[FriendListEmptyMessageApprovalPending]')}</h3>
                                <p className="text-muted-foreground text-sm">
                                    {t('FRIENDS_EMPTY_PENDING_DESC')}
                                </p>
                            </CardContent>
                        </Card>
                    )}
                </TabsContent>

                {/* 申请中列表 */}
                <TabsContent value="applying" className="space-y-4">
                    {loadingTabs.applying ? (
                        <div className="flex items-center justify-center py-20">
                            <Loader2 className="h-10 w-10 animate-spin text-primary" />
                            <span className="ml-3 text-muted-foreground">{getLoadingText()}</span>
                        </div>
                    ) : sentRequests.length > 0 ? (
                        <>
                            <div className="flex items-center justify-between">
                                <div className="text-sm text-muted-foreground">
                                    {t('FRIENDS_APPLYING_COUNT', [sentRequests.length])}
                                </div>
                            <Button
                                variant="outline"
                                size="sm"
                                onClick={handleCancelAllApply}
                                    disabled={actionLoading === 'cancel-all'}
                                >
                                    {actionLoading === 'cancel-all' ? (
                                        <Loader2 className="mr-2 h-4 w-4 animate-spin" />
                                    ) : (
                                        <X className="mr-2 h-4 w-4" />
                                    )}
                                    {t('[MassApplyRelease]')}
                                </Button>
                            </div>

                            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-4">
                                {sentRequests.map((request) => (
                                    <FriendCard
                                        key={request.playerId}
                                        player={request}
                                        topRight={t('FRIENDS_WAITING_RESPONSE')}
                                        actions={<Badge variant="secondary">{t('[FriendTabApplying]')}</Badge>}
                                    />
                                ))}
                            </div>
                        </>
                    ) : (
                        <Card className="border-dashed">
                            <CardContent className="flex flex-col items-center justify-center py-16 text-center">
                                <MessageCircle className="h-16 w-16 mb-4 text-muted-foreground opacity-20" />
                                <h3 className="text-lg font-semibold mb-2">{t('[FriendListEmptyMessageApplying]')}</h3>
                            </CardContent>
                        </Card>
                    )}
                </TabsContent>

                {/* 添加好友 */}
                <TabsContent value="search" className="space-y-6">
                    <Card>
                        <CardHeader className="flex flex-row items-center justify-between">
                            <div>
                                <CardTitle>{t('[FriendRecommendPlayerCaption]')}</CardTitle>
                                <CardDescription>{t('FRIENDS_RECOMMEND_DESC')}</CardDescription>
                            </div>
                            <div className="flex items-center gap-2">
                                <Input
                                    placeholder={t('[FriendSearchInputPlayerIdTitle]')}
                                    value={searchId}
                                    onChange={(e) => setSearchId(e.target.value)}
                                    className="w-48"
                                    onKeyDown={(e) => e.key === 'Enter' && handleSearch()}
                                />
                                <Button
                                    size="sm"
                                    onClick={handleSearch}
                                    disabled={searching}
                                >
                                    {searching ? (
                                        <Loader2 className="mr-2 h-4 w-4 animate-spin" />
                                    ) : (
                                        <Search className="mr-2 h-4 w-4" />
                                    )}
                                    {t('[CommonSearchLabel]')}
                                </Button>
                                {recommendations.length > 0 && (
                                    <Button
                                        size="sm"
                                        onClick={handleBulkApply}
                                        disabled={actionLoading === 'bulk-apply'}
                                    >
                                        {actionLoading === 'bulk-apply' ? (
                                            <Loader2 className="mr-2 h-4 w-4 animate-spin" />
                                        ) : (
                                            <UserPlus className="mr-2 h-4 w-4" />
                                        )}
                                        {t('[BulkApplyFriend]')}
                                    </Button>
                                )}
                            </div>
                        </CardHeader>
                        <CardContent className="space-y-4">
                            {searchedPlayer && (
                                <FriendCard
                                    player={searchedPlayer}
                                    topLeft={<span className="text-primary">{getGuildNameText(searchedPlayer.guildName)}</span>}
                                    topRight={<span className="text-primary">{t('FRIENDS_SEARCH_RESULT')}</span>}
                                    cardClassName="border-primary"
                                    actions={
                                        <Button
                                            size="icon"
                                            className="h-10 w-10 rounded-full"
                                            onClick={() => handleApplyFriend(searchedPlayer.playerId)}
                                            disabled={actionLoading === searchedPlayer.playerId}
                                        >
                                            {actionLoading === searchedPlayer.playerId ? (
                                                <Loader2 className="h-5 w-5 animate-spin" />
                                            ) : (
                                                <UserPlus className="h-5 w-5" />
                                            )}
                                        </Button>
                                    }
                                />
                            )}
                            {loadingTabs.applying ? (
                                <div className="flex items-center justify-center py-20">
                                    <Loader2 className="h-10 w-10 animate-spin text-primary" />
                                    <span className="ml-3 text-muted-foreground">{getLoadingText()}</span>
                                </div>
                            ) : recommendations.length > 0 ? (
                                <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-4">
                                    {recommendations.map((player) => (
                                        <FriendCard
                                            key={player.playerId}
                                            player={player}
                                            topRight={t('[SelectWorldRecommend]')}
                                            actions={
                                                <Button
                                                    size="icon"
                                                    className="h-10 w-10 rounded-full"
                                                    onClick={() => handleApplyFriend(player.playerId)}
                                                    disabled={actionLoading === player.playerId}
                                                >
                                                    {actionLoading === player.playerId ? (
                                                        <Loader2 className="h-5 w-5 animate-spin" />
                                                    ) : (
                                                        <UserPlus className="h-5 w-5" />
                                                    )}
                                                </Button>
                                            }
                                        />
                                    ))}
                                </div>
                            ) : (
                                <div className="text-center py-8 text-muted-foreground">
                                    {t('[FriendListEmptyMessageSearch]')}
                                </div>
                            )}
                        </CardContent>
                    </Card>
                </TabsContent>

                {/* 屏蔽列表 */}
                <TabsContent value="blocked" className="space-y-4">
                    <div className="flex items-center justify-between">
                        <div className="text-sm text-muted-foreground">
                            {t('FRIENDS_BLOCKED_COUNT', [stats.blockCount, stats.maxBlocks])}
                        </div>
                    </div>

                    {loadingTabs.blocked ? (
                        <div className="flex items-center justify-center py-20">
                            <Loader2 className="h-10 w-10 animate-spin text-primary" />
                            <span className="ml-3 text-muted-foreground">{getLoadingText()}</span>
                        </div>
                    ) : blockedPlayers.length > 0 ? (
                        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-4">
                            {blockedPlayers.map((player) => (
                                <FriendCard
                                    key={player.playerId}
                                    player={player}
                                    topLeft={<span className="text-destructive">{t('FRIENDS_BLOCKED_STATUS')}</span>}
                                    actions={
                                        <Button
                                            size="icon"
                                            variant="outline"
                                            className="h-10 w-10 rounded-full"
                                            onClick={() => handleUpdateBlock(player.playerId, false)}
                                            disabled={actionLoading === `unblock-${player.playerId}`}
                                        >
                                            {actionLoading === `unblock-${player.playerId}` ? (
                                                <Loader2 className="h-5 w-5 animate-spin" />
                                            ) : (
                                                <Shield className="h-5 w-5" />
                                            )}
                                        </Button>
                                    }
                                />
                            ))}
                        </div>
                    ) : (
                        <Card className="border-dashed">
                            <CardContent className="flex flex-col items-center justify-center py-16 text-center">
                            <Shield className="h-16 w-16 mb-4 text-muted-foreground opacity-20" />
                            <h3 className="text-lg font-semibold mb-2">{t('[FriendListEmptyMessageBlock]')}</h3>
                            <p className="text-muted-foreground text-sm">
                                {t('FRIENDS_BLOCKED_EMPTY_DESC')}
                            </p>
                            </CardContent>
                        </Card>
                    )}
                </TabsContent>
            </Tabs>
        </div>
    );
}
