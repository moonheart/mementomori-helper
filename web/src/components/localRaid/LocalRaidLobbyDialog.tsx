import { useState, useEffect, useCallback, useRef } from 'react';
import {
    Dialog,
    DialogContent,
    DialogDescription,
    DialogHeader,
    DialogTitle,
} from '@/components/ui/dialog';
import { Button } from '@/components/ui/button';
import { Input } from '@/components/ui/input';
import { Tabs, TabsContent, TabsList, TabsTrigger } from '@/components/ui/tabs';
import {
    Users,
    RefreshCw,
    Plus,
    LogIn,
    Loader2,
    AlertCircle,
    Trophy,
} from 'lucide-react';
import {
    localRaidSignalRService,
    LocalRaidPartyInfo,
    LocalRaidError,
    LocalRaidSessionState,
    CreateRoomParams,
} from '@/api/localRaidSignalRService';
import { useToast } from '@/hooks/use-toast';
import { LocalRaidQuestInfo } from '@/api/generated';
import { RoomCard } from '@/components/localRaid/RoomCard';
import { CreateRoomDialog } from '@/components/localRaid/CreateRoomDialog';
import { RoomWaitingDialog } from '@/components/localRaid/RoomWaitingDialog';
import { BattleLogModal } from '@/components/battle-log/BattleLogModal';
import { BattleSimulationResult } from '@/api/generated/battleSimulationResult';
import { ortegaApi } from '@/api/ortega-client';
import { useTranslation } from '@/hooks/useTranslation';

interface LocalRaidLobbyDialogProps {
    open: boolean;
    onOpenChange: (open: boolean) => void;
    quest: LocalRaidQuestInfo | null;
    myBattlePower?: number;
    myPlayerId?: number;
}

export function LocalRaidLobbyDialog({
    open,
    onOpenChange,
    quest,
    myBattlePower = 0,
    myPlayerId = 0,
}: LocalRaidLobbyDialogProps) {
    const { t } = useTranslation();
    const { toast } = useToast();
    const [loading, setLoading] = useState(false);
    const [connecting, setConnecting] = useState(false);
    const [rooms, setRooms] = useState<LocalRaidPartyInfo[]>([]);
    const [showCreateRoom, setShowCreateRoom] = useState(false);
    const [showWaitingRoom, setShowWaitingRoom] = useState(false);
    const [currentRoom, setCurrentRoom] = useState<LocalRaidPartyInfo | null>(null);
    const [joinRoomId, setJoinRoomId] = useState('');
    const [joinPassword, setJoinPassword] = useState('0');
    const [battleSimResult, setBattleSimResult] = useState<BattleSimulationResult | null>(null);
    const [showBattleResult, setShowBattleResult] = useState(false);
    const initializedRef = useRef(false);
    // 标记房间操作（解散/离开）已由主动按钮路径处理，防止 handleWaitingRoomOpenChange 重复调用
    const roomActionDoneRef = useRef(false);

    // 初始化 SignalR 连接
    const initConnection = useCallback(async () => {
        if (!open || initializedRef.current) return;

        initializedRef.current = true;
        setConnecting(true);
        try {
            // 设置回调
            localRaidSignalRService.setCallbacks({
                onRoomListReceived: (roomList: LocalRaidPartyInfo[]) => {
                    // 过滤掉空房间（roomId 为空的对象）
                    const validRooms = Array.isArray(roomList)
                        ? roomList.filter(room => room && room.roomId)
                        : [];
                    setRooms(validRooms);
                    setLoading(false);
                },
                onRoomJoined: (room: LocalRaidPartyInfo) => {
                    setCurrentRoom(room);
                    setShowWaitingRoom(true);
                    setLoading(false);
                },
                onRoomUpdated: (room: LocalRaidPartyInfo) => {
                    setCurrentRoom(room);
                },
                onRoomLeft: () => {
                    setCurrentRoom(null);
                    setShowWaitingRoom(false);
                },
                onRoomDisbanded: () => {
                    setCurrentRoom(null);
                    setShowWaitingRoom(false);
                    toast({
                        title: t('LOCAL_RAID_ROOM_DISSOLVED'),
                        description: t('LOCAL_RAID_HOST_DISSOLVED'),
                    });
                },
                onBattleStarted: async () => {
                    setShowWaitingRoom(false);
                    setCurrentRoom(null);
                    try {
                        const result = await ortegaApi.localRaid.getLocalRaidBattleResult({});
                        if (result?.battleResult?.simulationResult) {
                            setBattleSimResult(result.battleResult.simulationResult);
                            setShowBattleResult(true);
                        } else {
                            toast({ title: t('LOCAL_RAID_BATTLE_END'), description: t('LOCAL_RAID_NO_RESULT') });
                        }
                    } catch (e) {
                        console.error('Failed to fetch battle result:', e);
                        toast({ title: t('LOCAL_RAID_BATTLE_END'), description: t('LOCAL_RAID_RESULT_FAILED') });
                    }
                },
                onError: (error: LocalRaidError) => {
                    setLoading(false);
                    toast({
                        variant: 'destructive',
                        title: t('LOCAL_RAID_ERROR'),
                        description: error.message || t('LOCAL_RAID_ERROR_CODE', [String(error.errorCode)]),
                    });
                },
                onConnectionStateChanged: (connected: boolean) => {
                    setConnecting(!connected);
                },
            });

            // 连接 SignalR（后端会自动创建 MagicOnion 会话）
            await localRaidSignalRService.start();

            // 获取当前状态
            const state: LocalRaidSessionState = await localRaidSignalRService.getCurrentState();

            // 如果已在房间中，显示等待界面
            if (state.isInRoom && state.currentRoom) {
                setCurrentRoom(state.currentRoom);
                setShowWaitingRoom(true);
            }
        } catch (error) {
            console.error('Failed to connect:', error);
            toast({
                variant: 'destructive',
                title: t('LOCAL_RAID_CONNECT_FAILED'),
                description: t('LOCAL_RAID_CONNECT_SERVER_FAILED'),
            });
            initializedRef.current = false;
        } finally {
            setConnecting(false);
        }
    }, [open, toast, t]);

    // 刷新房间列表
    const refreshRooms = useCallback(async () => {
        if (!quest) return;
        setLoading(true);
        try {
            await localRaidSignalRService.getRoomList(quest.id);
        } catch (error) {
            console.error('Failed to get room list:', error);
            setLoading(false);
        }
    }, [quest]);

    // 随机加入
    const joinRandomRoom = async () => {
        if (!quest) return;
        setLoading(true);
        try {
            await localRaidSignalRService.joinRandomRoom(quest.id);
        } catch (error) {
            console.error('Failed to join random room:', error);
            setLoading(false);
        }
    };

    // 加入指定房间
    const joinSpecificRoom = async () => {
        if (!joinRoomId.trim()) {
            toast({
                variant: 'destructive',
                title: t('LOCAL_RAID_ENTER_ROOM_ID'),
            });
            return;
        }
        setLoading(true);
        try {
            await localRaidSignalRService.joinRoom({
                roomId: joinRoomId.trim(),
                password: parseInt(joinPassword) || 0,
            });
        } catch (error) {
            console.error('Failed to join room:', error);
            setLoading(false);
        }
    };

    // 监听打开事件
    useEffect(() => {
        if (open) {
            initConnection();
        } else {
            // 关闭时断开连接
            localRaidSignalRService.leaveSession();
            localRaidSignalRService.stop();
            setRooms([]);
            setCurrentRoom(null);
            setShowWaitingRoom(false);
            setShowCreateRoom(false);
            initializedRef.current = false;
        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [open]);

    // 连接成功后刷新房间列表
    useEffect(() => {
        if (open && quest && !connecting && !showWaitingRoom) {
            refreshRooms();
        }
    }, [open, quest, connecting, showWaitingRoom, refreshRooms]);

    // 处理加入房间
    const handleJoinRoom = async (room: LocalRaidPartyInfo) => {
        setLoading(true);
        try {
            await localRaidSignalRService.joinRoom({
                roomId: room.roomId,
                password: 0,
            });
        } catch (error) {
            console.error('Failed to join room:', error);
            setLoading(false);
        }
    };

    // 处理创建房间
    const handleCreateRoom = async (params: CreateRoomParams) => {
        setLoading(true);
        try {
            await localRaidSignalRService.createRoom(params);
            setShowCreateRoom(false);
        } catch (error) {
            console.error('Failed to create room:', error);
            setLoading(false);
        }
    };

    // 处理等待房间开关
    // 注意：此函数是关闭等待房间弹窗的「兜底」处理（如点击弹窗外部关闭）。
    // 当用户通过按钮主动解散/离开时，onDisband/onLeave 路径会设置 roomActionDoneRef = true，
    // 这里检测到后跳过重复的 SignalR 调用，避免对已解散的房间发送第二次解散请求。
    const handleWaitingRoomOpenChange = (isOpen: boolean) => {
        setShowWaitingRoom(isOpen);
        if (!isOpen) {
            if (currentRoom && !roomActionDoneRef.current) {
                // 兜底路径（如点击弹窗外部）：主动执行离开/解散
                const isLeader = currentRoom.leaderPlayerId === myPlayerId;
                if (isLeader) {
                    localRaidSignalRService.disbandRoom();
                } else {
                    localRaidSignalRService.leaveRoom();
                }
            }
            // 无论哪条路径，关闭后重置状态
            roomActionDoneRef.current = false;
            setCurrentRoom(null);
        }
    };

    // 处理准备
    const handleReady = async (isReady: boolean) => {
        await localRaidSignalRService.setReady(isReady);
    };

    // 处理更新房间条件
    const handleUpdateCondition = async (params: CreateRoomParams) => {
        await localRaidSignalRService.updateRoomCondition(params);
    };

    return (
        <>
            <Dialog open={open} onOpenChange={onOpenChange}>
                <DialogContent className="max-w-3xl max-h-[80vh] overflow-hidden flex flex-col">
                    <DialogHeader>
                        <DialogTitle className="flex items-center gap-2">
                            <Users className="h-5 w-5" />
                            {t('LOCAL_RAID_LOBBY_TITLE')}
                        </DialogTitle>
                        <DialogDescription>
                            {quest && (
                                <span>
                                    {t('LOCAL_RAID_QUEST_INFO', [String(quest.id), quest.recommendedBattlePower.toLocaleString()])}
                                </span>
                            )}
                        </DialogDescription>
                    </DialogHeader>

                    {connecting ? (
                        <div className="flex items-center justify-center py-12">
                            <Loader2 className="h-8 w-8 animate-spin text-primary" />
                            <span className="ml-2">{t('LOCAL_RAID_CONNECTING')}</span>
                        </div>
                    ) : (
                        <div className="flex-1 overflow-hidden flex flex-col space-y-4">
                            {/* 操作栏 */}
                            <div className="flex items-center gap-2">
                                <Button
                                    variant="outline"
                                    size="sm"
                                    onClick={refreshRooms}
                                    disabled={loading}
                                >
                                    <RefreshCw className={`h-4 w-4 mr-2 ${loading ? 'animate-spin' : ''}`} />
                                    {t('LOCAL_RAID_REFRESH')}
                                </Button>
                                <Button
                                    size="sm"
                                    onClick={() => setShowCreateRoom(true)}
                                    disabled={loading}
                                >
                                    <Plus className="h-4 w-4 mr-2" />
                                    {t('LOCAL_RAID_CREATE_ROOM')}
                                </Button>
                                <Button
                                    variant="secondary"
                                    size="sm"
                                    onClick={joinRandomRoom}
                                    disabled={loading}
                                >
                                    <LogIn className="h-4 w-4 mr-2" />
                                    {t('LOCAL_RAID_RANDOM_JOIN')}
                                </Button>
                            </div>

                            {/* 房间ID加入 */}
                            <div className="flex items-center gap-2">
                                <Input
                                    placeholder={t('LOCAL_RAID_ENTER_ROOM_ID_PLACEHOLDER')}
                                    value={joinRoomId}
                                    onChange={(e) => setJoinRoomId(e.target.value)}
                                    className="flex-1"
                                />
                                <Input
                                    placeholder={t('LOCAL_RAID_PASSWORD_OPTIONAL')}
                                    value={joinPassword}
                                    onChange={(e) => setJoinPassword(e.target.value)}
                                    className="w-24"
                                    type="number"
                                />
                                <Button
                                    size="sm"
                                    onClick={joinSpecificRoom}
                                    disabled={loading}
                                >
                                    {t('LOCAL_RAID_JOIN')}
                                </Button>
                            </div>

                            {/* 我的战力 */}
                            <div className="flex items-center gap-2 text-sm text-muted-foreground">
                                <Trophy className="h-4 w-4" />
                                <span>{t('LOCAL_RAID_MY_POWER', [myBattlePower.toLocaleString()])}</span>
                            </div>

                            {/* 房间列表 */}
                            <Tabs defaultValue="all" className="flex-1 overflow-hidden flex flex-col">
                                <TabsList className="grid w-full grid-cols-2">
                                    <TabsTrigger value="all">{t('LOCAL_RAID_ALL_ROOMS', [String(rooms.length)])}</TabsTrigger>
                                    <TabsTrigger value="friends">{t('LOCAL_RAID_FRIEND_ROOMS')}</TabsTrigger>
                                </TabsList>

                                <TabsContent value="all" className="flex-1 overflow-y-auto space-y-2 mt-2">
                                    {rooms.length === 0 ? (
                                        <div className="text-center py-8 text-muted-foreground">
                                            <AlertCircle className="h-12 w-12 mx-auto mb-3 opacity-50" />
                                            <div>{t('LOCAL_RAID_NO_ROOMS')}</div>
                                            <div className="text-sm mt-1">{t('LOCAL_RAID_CREATE_PROMPT')}</div>
                                        </div>
                                    ) : (
                                        rooms.map((room) => (
                                            <RoomCard
                                                key={room.roomId}
                                                room={room}
                                                myBattlePower={myBattlePower}
                                                onJoin={handleJoinRoom}
                                            />
                                        ))
                                    )}
                                </TabsContent>

                                <TabsContent value="friends" className="flex-1 overflow-y-auto space-y-2 mt-2">
                                    <div className="text-center py-8 text-muted-foreground">
                                        <Users className="h-12 w-12 mx-auto mb-3 opacity-50" />
                                        <div>{t('LOCAL_RAID_FRIEND_ROOMS_UNAVAILABLE')}</div>
                                    </div>
                                </TabsContent>
                            </Tabs>
                        </div>
                    )}
                </DialogContent>
            </Dialog>

            {/* 创建房间弹窗 */}
            <CreateRoomDialog
                open={showCreateRoom}
                onOpenChange={setShowCreateRoom}
                quest={quest}
                myBattlePower={myBattlePower}
                onCreate={handleCreateRoom}
            />

            {/* 等待房间弹窗 */}
            <RoomWaitingDialog
                open={showWaitingRoom}
                onOpenChange={handleWaitingRoomOpenChange}
                room={currentRoom}
                myPlayerId={myPlayerId}
                onReady={handleReady}
                onStartBattle={() => localRaidSignalRService.startBattle()}
                onDisband={async () => {
                    // 标记此次操作已由按钮路径处理，防止 handleWaitingRoomOpenChange 重复调用
                    roomActionDoneRef.current = true;
                    await localRaidSignalRService.disbandRoom();
                    setShowWaitingRoom(false);
                    setCurrentRoom(null);
                }}
                onUpdateCondition={handleUpdateCondition}
            />
            {/* 战斗结果弹窗 */}
            <BattleLogModal
                isOpen={showBattleResult}
                onClose={() => {
                    setShowBattleResult(false);
                    setBattleSimResult(null);
                }}
                battleData={battleSimResult}
            />
        </>
    );
}
